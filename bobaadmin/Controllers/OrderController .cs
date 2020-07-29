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
            return View();
        }

        //public ActionResult Index()
        //{
        //    var a = db.Menu.Where(s => s.Isdelete != true).ToList();
        //    return View(a);
        //}
        
    }
}