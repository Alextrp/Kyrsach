using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using Kyrsach.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;

namespace Kyrsach.Controllers
{
    public class AdminController : Controller
    {
        public IService<PaymentDTO> _servicePayment;
        public IService<UserDTO> _serviceUserDTO;
        private readonly UserManager<User> _userManager;

        public AdminController(IService<PaymentDTO> servicePayment, IService<UserDTO> serviceUserDTO, UserManager<User> userManager)
        {
            _servicePayment = servicePayment;
            _serviceUserDTO = serviceUserDTO;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var payments = _servicePayment.GetAll();

            var list = payments.Where(p => p.PaymentDate.Month == DateTime.Now.Month && p.PaymentDate.Year == DateTime.Now.Year); 

            ViewBag.TotalAmountThisMonth = list.Sum(p => p.Amount);

            ViewBag.TotalSalesVolume = payments.Sum(p => p.Amount);

            ViewBag.MostUsedPaymentMethod = list
            .GroupBy(p => p.PaymentMethod)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .FirstOrDefault();

            ViewBag.MostUsedPaymentMethodCount = list
            .Where(m => m.PaymentMethod == ViewBag.MostUsedPaymentMethod)
            .Count();

            ViewBag.AnotherPaymentMethodCount = list.Count() - ViewBag.MostUsedPaymentMethodCount;

            return View(list);
        }

        public async Task<IActionResult> UserControl()
        {
            var userList = _serviceUserDTO.GetAll();

            var adminViewModelList = new List<AdminViewModel>();

            foreach (var user in userList)
            {
                var userr = await _userManager.FindByNameAsync(user.UserName);
                var roles = await _userManager.GetRolesAsync(userr); 

                var adminViewModel = new AdminViewModel
                {
                    UserID = user.Id,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Role = roles.FirstOrDefault()
                };

                adminViewModelList.Add(adminViewModel);
            }

            return View(adminViewModelList);
        }
    }
}
