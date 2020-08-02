using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Kendo.DynamicLinq;
using bobaadmin.Models;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace bobaadmin
{
    [Authorize]
    public class OrderController  : Controller
    {
        public OrderController (BobaDA bobaDA)
        {
            this.bobaDA = bobaDA; ;
        }

        private BobaDA bobaDA;
        //MyDbContext db = new MyDbContext();
        bobachaContext db = new bobachaContext();
        
        public ActionResult Index()
        {
            var data = bobaDA.CountOrder();
            ViewBag.NewOrders = data.Count(a => a.StatusID == 1);
            ViewBag.Making = data.Count(a => a.StatusID == 2);
            ViewBag.Waiting = data.Count(a => a.StatusID == 3);
            ViewBag.Done = data.Count(a => a.StatusID == 4);
            return View();
        }

        
        
    }
}