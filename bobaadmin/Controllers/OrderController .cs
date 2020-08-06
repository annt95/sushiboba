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
            this.bobaDA = bobaDA; 
        }

        private BobaDA bobaDA;
        //MyDbContext db = new MyDbContext();
        bobachaContext db = new bobachaContext();
        
        public ActionResult Index()
        {
            var data = bobaDA.CountOrder();
            ViewBag.NewOrders = data.Where(st => st.StatusID == 1).Select(s => s.Count).FirstOrDefault();
            ViewBag.Processing= data.Where(s => s.StatusID == 2).Select(s => s.Count).FirstOrDefault();
            ViewBag.Picked= data.Where(s => s.StatusID == 3).Select(s => s.Count).FirstOrDefault();
            ViewBag.Completed = data.Where(s => s.StatusID == 4).Select(s => s.Count).FirstOrDefault();
            return View();
        }
        public IActionResult ViewCate(string stt)
        {
            var data = bobaDA.GetListOrder(stt);
            return View(data);
        }
        public IActionResult Update(int id)
        {
            var dataItem = new OrderDetail();
            var dataOrder = bobaDA.GetOrder(id);
            ViewBag.OrderID = dataOrder.ID;
            ViewBag.OrderTime = dataOrder.OrderTime;
            ViewBag.CustomerName = dataOrder.CustomerName;
            ViewBag.CustomerPhone = dataOrder.CustomerPhone;
            ViewBag.TotalMoney = dataOrder.TotalMoney;
            ViewBag.Note = dataOrder.Note;
            ViewBag.ShippingType = dataOrder.ShippingType;
            ViewBag.StatusID = dataOrder.StatusID;
            dataItem.items = bobaDA.GetListOrderItembyId(id);
            return View(dataItem);
        }
        [HttpPost]
        public ActionResult UpdateOrder(Menu menu)
        {
            Menu d = db.Menu.Where(s => s.Id == menu.Id).First();
            d.Price = menu.Price;
            d.Description = menu.Description;
            d.Images = menu.Images;
            d.Name = menu.Name;
            d.Issushi = menu.Issushi;
            d.Ismilktea = menu.Ismilktea;
            db.SaveChanges();
            return RedirectToAction("Index", "Order");
        }

        [HttpPost]
        public ActionResult Accept(int id, int sttid)
        {
            var statusid = sttid + 1;
            bobaDA.UpdateOrder(id, statusid);
            return RedirectToAction("Index", "Order");
        }
    }
}