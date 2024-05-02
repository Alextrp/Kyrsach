using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;
using DAL.Entities;
using DAL.IRepositories;
using Kyrsach.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Kyrsach.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IService<OrderDTO> _cargoService;
        IService<PaymentDTO> _paymentService;
        public HomeController(ILogger<HomeController> logger)
        { 
            //_paymentService = paymentService;
            //_paymentService.GetAll();

            //_cargoService = service;
            //_cargoService.GetAll();
            _logger = logger;  
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
