using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FrontEnd.Models;

namespace FrontEnd.Controllers
{
    public class Order : Controller
    {
        public Order(BobaDA bobaDA)
        {
            this.bobaDA = bobaDA; ;
        }

        private BobaDA bobaDA;
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult GetMenu()
        {
            var view = "GetMenu";
            var data = bobaDA.GetListItem();
            return View(view, data);
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
