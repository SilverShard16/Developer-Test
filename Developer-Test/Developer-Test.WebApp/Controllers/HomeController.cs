using Developer_Test.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Developer_Test.Services.Services.Interfaces;

namespace Developer_Test.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVouchersService _vouchersService;

        public HomeController(IVouchersService vouchersService)
        {
            _vouchersService = vouchersService ?? throw new ArgumentNullException(nameof(vouchersService));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<double> CouponSubmitted(string couponCode, double basketTotal)
            => await _vouchersService.SubmitCouponAsync(basketTotal, couponCode);

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
