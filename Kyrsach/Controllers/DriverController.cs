using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using Kyrsach.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kyrsach.Controllers
{
    public class DriverController : Controller
    {
        public IOrderService _serviceOrder;
        public IService<OrderStatusDTO> _serviceOrderStatus;
        public IService<UserDTO> _userService;
        public IService<VehicleDTO> _vehicleService;

        public DriverController(IOrderService serviceOrder, IService<OrderStatusDTO> serviceOrderStatus,
            IService<UserDTO> userService, IService<VehicleDTO> vehicleService
            )
        {
            _serviceOrder = serviceOrder;
            _serviceOrderStatus = serviceOrderStatus;
            _userService = userService;
            _vehicleService = vehicleService;
            
        }

        public IActionResult Index()
        {
            var userName = HttpContext.Session.GetString("UserName");
            var user = _userService.GetAll().FirstOrDefault(u => u.UserName == userName);

            var orders = _serviceOrder.GetOrdersForManager(2)
                                      .Where(o => o.UserID == user.Id)
                                      .Select(o => new DriverViewModel
                                      {
                                          OrderId = o.OrderID,
                                          PickupLocation = o.PickupLocation,
                                          DropoffLocation = o.DropoffLocation,
                                          OrderDate = o.OrderDate,
                                          DeliveryDate = o.DeliveryDate
                                      })
                                      .ToList();

            return View(orders);
        }

        public IActionResult EndOrder()
        {
            var userName = HttpContext.Session.GetString("UserName");
            var user = _userService.GetAll().FirstOrDefault(u => u.UserName == userName);

            var orders = _serviceOrder.GetOrdersForManager(4)
                                      .Where(o => o.UserID == user.Id)
                                      .Select(o => new DriverViewModel
                                      {
                                          OrderId = o.OrderID,
                                          PickupLocation = o.PickupLocation,
                                          DropoffLocation = o.DropoffLocation,
                                          OrderDate = o.OrderDate,
                                          DeliveryDate = o.DeliveryDate
                                      })
                                      .ToList();

            return View(orders);
        }

        public IActionResult AssignOrder(int orderId)
        {
            OrderDTO order = _serviceOrder.GetById(orderId);

            order.StatusID = 4;
            _serviceOrder.Update(order);

            return RedirectToAction("Index");
        }

        public IActionResult OrderEnding(int orderId)
        {
            OrderDTO order = _serviceOrder.GetById(orderId);

            order.StatusID = 5;
            _serviceOrder.Update(order);

            return RedirectToAction("Index");
        }
    }
}
