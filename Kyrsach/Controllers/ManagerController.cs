using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kyrsach.Controllers
{
    public class ManagerController : Controller
    {
        public IService<CargoTypeDTO> _serviceCargoType;
        public IOrderService _serviceOrder;
        public IService<OrderStatusDTO> _serviceOrderStatus;
        public IService<PaymentDTO> _servicePayment;
        public IService<UserDTO> _userService;
        public IService<VehicleDTO> _vehicleService;

        public ManagerController(IService<CargoTypeDTO> serviceCargoType, IOrderService serviceOrder, 
            IService<OrderStatusDTO> serviceOrderStatus, IService<PaymentDTO> servicePayment,
            IService<UserDTO> userService, IService<VehicleDTO> vehicleService)
        {
            _serviceCargoType = serviceCargoType;
            _serviceOrder = serviceOrder;
            _serviceOrderStatus = serviceOrderStatus;
            _servicePayment = servicePayment;
            _userService = userService;
            _vehicleService = vehicleService;
        }

        public IActionResult Index()
        {
            var list = _serviceOrder.GetOrdersForManager(1).Join(
                _servicePayment.GetAll(),
                order => order.OrderID,
                payment => payment.OrderID,
                (order, payment) => new
                {
                    OrderId = order.OrderID,
                    Amount = payment.Amount,
                    Status = payment.Status,
                    PickupLocation = order.PickupLocation,
                    DropoffLocation = order.DropoffLocation,
                    OrderDate = order.OrderDate,
                    DeliveryDate = order.DeliveryDate,
                    Description = order.DeliveryDate
                }).ToList();

            var users = _userService.GetAll();
            var vehicles = _vehicleService.GetAll();
            var orders = _serviceOrder.GetAll();

            var inactiveStatusIds = new List<int> { 1, 2, 3, 4 };

            ViewBag.Users = users
                .Where(user => vehicles.Any(vehicle => vehicle.UserID == user.Id)) // пользователи с машинами
                .Where(user => !orders.Any(order => order.UserID == user.Id && !inactiveStatusIds.Contains(order.StatusID))) // без активных заказов
                .Select(user => new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                }).ToList();

            return View(list);
        }

        public IActionResult AssignDriver(int orderId, string userId)
        {
            // Ваша логика назначения водителя на заказ
            return RedirectToAction("Index");
        }

        public IActionResult RejectOrder(int orderId)
        {
            // Ваша логика отклонения заказа
            return RedirectToAction("Index");
        }

    }
}
