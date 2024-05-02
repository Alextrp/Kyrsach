using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using Kyrsach.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Ninject.Activation;

namespace Kyrsach.Controllers
{
    public class TransportOrderController : Controller
    {
        public IService<CargoDTO> _serviceCargo;
        public IService<CargoTypeDTO> _serviceCargoType;
        public IOrderService _serviceOrder;
        public IService<OrderStatusDTO> _serviceOrderStatus;
        public IService<PaymentDTO> _servicePayment;
        public IService<UserDTO> _serviceUserDTO;
        private readonly UserManager<User> _userManager;

        public TransportOrderController(UserManager<User> userManager, IService<CargoDTO> serviceCargo, IService<CargoTypeDTO> serviceCargoType,
            IOrderService serviceOrder, IService<OrderStatusDTO> serviceOrderStatus,
            IService<PaymentDTO> servicePayment, IService<UserDTO> serviceUserDTO)
        {
            _userManager = userManager;
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
            model.CargoTypes = _serviceCargoType.GetAll().Select(ct => new SelectListItem
            {
                Value = ct.CargoTypeID.ToString(),
                Text = ct.TypeName
            });

            if (!ModelState.IsValid)
            {
                double price = await CalculatedPrice(Double.Parse(model.Distance.Replace('.',',')), model.Weight, model.Volume);
                ViewBag.Price = $"Estimated price: ${price}";
            }

            HttpContext.Session.SetString("OrderData", JsonConvert.SerializeObject(model));

            return View("Index", model);
        }


        private async Task<double> CalculatedPrice(double distance, double weight, double volume)
        {
            double basePrice = 50.0;  // Базовая цена
           
            double pricePerKm = 0.5;  // Цена за километр
            double weightFactor = 1.5;  // Фактор веса
            double volumeFactor = 2.0;  // Фактор объема

            return Math.Round(basePrice + (distance * pricePerKm / 1000) + (weight * weightFactor) + (volume * volumeFactor), 2);
        }



        [HttpPost]
        public async Task<IActionResult> CreateOrder(string paymentMethod)
        {
            var orderDataString = HttpContext.Session.GetString("OrderData");
            var model = JsonConvert.DeserializeObject<TransportOrderViewModel>(orderDataString);
            if (HttpContext.Session.GetString("UserName") != null)
            {
                if (ModelState.IsValid)
                {

                    var user = await _userManager.FindByNameAsync(HttpContext.Session.GetString("UserName"));

                    model.CargoTypes = _serviceCargoType.GetAll().Select(ct => new SelectListItem
                    {
                        Value = ct.CargoTypeID.ToString(),
                        Text = ct.TypeName
                    });

                    CreateCargo(user.Id, model);
                    var order = new OrderDTO
                    {
                        OrderID = _serviceOrder.GetAll()
                            .OrderByDescending(o => o.OrderID)
                            .Select(o => o.OrderID)
                            .FirstOrDefault()+1,
                        PickupLocation = model.PickupLocation,
                        DropoffLocation = model.DropoffLocation,
                        OrderDate = model.OrderDate,
                        DeliveryDate = model.DeliveryDate,
                        CargoID = _serviceCargo.GetAll()
                            .OrderByDescending(o => o.CargoID)
                            .Select(o => o.CargoID)
                            .FirstOrDefault(),
                        UserID = null, // Assumes identity management is in place
                        StatusID = 1 // Assuming 1 is the ID for 'new'
                    };
                    _serviceOrder.Add(order);

                    CreatePayment(paymentMethod);

                    ViewBag.Flaf = 1;
                }
            }
            else
            {
                return RedirectToAction("Login", "Account"); // Redirect to login
            }

            
            return View("Index", model);
        }

        private CargoDTO CreateCargo(string id, TransportOrderViewModel model)
        {
            var cargo = new CargoDTO
            {
                Description = model.Description,
                Weight = model.Weight,
                Volume = model.Volume,
                CargoTypeID = model.CargoTypeID,
                UserID = id // Assumes identity management
            };
            _serviceCargo.Add(cargo);
            return cargo;
        }

        private async void CreatePayment(string paymentMethod)
        {
            var orderDataString = HttpContext.Session.GetString("OrderData");
            var model = JsonConvert.DeserializeObject<TransportOrderViewModel>(orderDataString);

               var lastOrderId = _serviceOrder.GetAll()
                .OrderByDescending(o => o.OrderID)
                .Select(o => o.OrderID)
                .FirstOrDefault();
            _servicePayment.GetAll();
                

            double calculatedPrice = await CalculatedPrice(Double.Parse(model.Distance.Replace('.', ',')), model.Weight, model.Volume);
            var payment = new PaymentDTO
            {

                OrderID = lastOrderId,
                Amount = (int)Math.Round(calculatedPrice),
                PaymentDate = DateTime.Now.Date,
                PaymentMethod = paymentMethod,
                Status = "Оплачено"
            };

            _servicePayment.Add(payment);
        }

    }
}
