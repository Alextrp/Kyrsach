using BLL.DTO;
using BLL.Interfaces;
using Kyrsach.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Kyrsach.Controllers
{
    public class TransportOrderController : Controller
    {
        public IService<CargoDTO> _serviceCargo;
        public IService<CargoTypeDTO> _serviceCargoType;
        public IService<OrderDTO> _serviceOrder;
        public IService<OrderStatusDTO> _serviceOrderStatus;
        public IService<PaymentDTO> _servicePayment;
        public IService<UserDTO> _serviceUserDTO;

        public TransportOrderController(IService<CargoDTO> serviceCargo, IService<CargoTypeDTO> serviceCargoType,
            IService<OrderDTO> serviceOrder, IService<OrderStatusDTO> serviceOrderStatus,
            IService<PaymentDTO> servicePayment, IService<UserDTO> serviceUserDTO)
        {
            _serviceCargo = serviceCargo;
            _serviceCargoType = serviceCargoType;
            _serviceOrder = serviceOrder;
            _serviceOrderStatus = serviceOrderStatus;
            _servicePayment = servicePayment;
            _serviceUserDTO = serviceUserDTO;
        }

        public IActionResult Index()
        {
            var model = new TransportOrderViewModel
            {
                CargoTypes = _serviceCargoType.GetAll().Select(ct => new SelectListItem
                {
                    Value = ct.CargoTypeID.ToString(),
                    Text = ct.TypeName
                })
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CalculatePrice(TransportOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                double price = await CalculatePrice(model.PickupLocation, model.DropoffLocation, model.Weight,model.Volume);
                ViewBag.Price = $"Estimated price: ${price}";
            }
            model.CargoTypes = _serviceCargoType.GetAll().Select(ct => new SelectListItem
            {
                Value = ct.CargoTypeID.ToString(),
                Text = ct.TypeName
            });
            return View("Index", model);
        }

        private async Task<double> CalculatePrice(string pickupLocation, string dropoffLocation, double weight, double volume)
        {
            double basePrice = 50.0;  // Базовая цена
            double distance = await GetDistance(pickupLocation, dropoffLocation);
            double pricePerKm = 0.5;  // Цена за километр
            double weightFactor = 1.5;  // Фактор веса
            double volumeFactor = 2.0;  // Фактор объема

            return basePrice + (distance * pricePerKm) + (weight * weightFactor) + (volume * volumeFactor);
        }


        private async Task<double> GetDistance(string pickupLocation, string dropoffLocation)
        {
            var apiKey = "AIzaSyAOVYRIgupAurZup5y1PRh8Ismb1A3lLao";
            var url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={Uri.EscapeDataString(pickupLocation)}&destinations={Uri.EscapeDataString(dropoffLocation)}&key={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();
                var distanceMatrix = JsonConvert.DeserializeObject<GoogleDistanceMatrixResponse>(content);

                if (distanceMatrix.Rows.Any() && distanceMatrix.Rows.First().Elements.Any())
                {
                    return distanceMatrix.Rows.First().Elements.First().Distance.Value / 1000.0;  // Преобразование из метров в километры
                }
                return 0;
            }
        }


        [HttpPost]
        public IActionResult CreateOrder(TransportOrderViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var order = new OrderDTO
                    {
                        PickupLocation = model.PickupLocation,
                        DropoffLocation = model.DropoffLocation,
                        OrderDate = model.OrderDate,
                        DeliveryDate = model.DeliveryDate,
                        CargoID = CreateCargo(model).CargoID,
                        UserID = User.Identity.GetUserId(), // Assumes identity management is in place
                        StatusID = 1 // Assuming 1 is the ID for 'new'
                    };
                    _serviceOrder.Add(order);
                    return RedirectToAction("OrderSuccess");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account"); // Redirect to login
            }

            model.CargoTypes = _serviceCargoType.GetAll().Select(ct => new SelectListItem
            {
                Value = ct.CargoTypeID.ToString(),
                Text = ct.TypeName
            });
            return View("Index", model);
        }

        private CargoDTO CreateCargo(TransportOrderViewModel model)
        {
            var cargo = new CargoDTO
            {
                Description = model.Description,
                Weight = model.Weight,
                Volume = model.Volume,
                CargoTypeID = model.CargoTypeID,
                UserID = User.Identity.GetUserId() // Assumes identity management
            };
            _serviceCargo.Add(cargo);
            return cargo;
        }

    }
}
