using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FrontEnd.Models;

namespace FrontEnd.Controllers
{
    public class MenuController : Controller
    {
        public MenuController(BobaDA bobaDA)
        {
            this.bobaDA = bobaDA; ;
        }

        private BobaDA bobaDA;
        public IActionResult Index()
        {
            var data = bobaDA.GetListShopeeItem();
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
