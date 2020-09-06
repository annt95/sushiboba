using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

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
            var data = bobaDA.GetListItem();
            return View(data);
        }
        public Menu getDetailProduct(int id)
        {
            var data = bobaDA.GetItembyID(id);
            return data;
        }
        public ActionResult GetMenu()
        {
            var view = "GetMenu";
            var data = bobaDA.GetListItem();
            return View(view, data);
        }
        //ADD CART
        public CartModel addCart(int id,int price)
        {
            var p = 0;
            var cart = HttpContext.Session.GetString("cart");//get key cart
            var cModel = new CartModel();
            if (cart == null)
            {
                var product = getDetailProduct(id);
                List<Cart> listCart = new List<Cart>()
               {
                   new Cart
                   {
                       Product = product,
                       Quantity = 1
                   }
               };
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(listCart));
                cModel.count = 1;
                cModel.totalPrice = product.Price;
                return cModel;
            }
            else
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                bool check = true;
                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].Product.Id == id)
                    {
                        dataCart[i].Quantity++;
                        check = false;
                    }
                }
                if (check)
                {
                    dataCart.Add(new Cart
                    {
                        Product = getDetailProduct(id),
                        Quantity = 1
                    });
                    
                }
                foreach (var item in dataCart)
                {
                    cModel.count += item.Quantity;
                    
                    p += Int32.Parse(item.Product.Price);
                }
                cModel.totalPrice = p.ToString();
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                // var cart2 = HttpContext.Session.GetString("cart");//get key cart
                 return cModel;
            }
        }
        [HttpGet]
        public CartModel checkCart()
        {
            var p = 0;
            var cart = HttpContext.Session.GetString("cart");//get key cart
            var cModel = new CartModel();
            if (cart == null)
            {
                cModel.count = 0;
                cModel.totalPrice = 0.ToString();
                return cModel;
            }
            else
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                bool check = true;
                foreach (var item in dataCart)
                {
                    cModel.count += item.Quantity;

                    p += Int32.Parse(item.Product.Price);
                }
                cModel.totalPrice = p.ToString();
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                // var cart2 = HttpContext.Session.GetString("cart");//get key cart
                return cModel;
            }
        }
        [HttpGet]
        public int checkDistace(double llat, double llong)
        {
            // boba 59.9112227,10.7463083
            // 009 10.7254846,106.7106487

            return 1;

        }
        public class CartModel
        {
            public int count { get; set; }
            public string totalPrice { get; set; }
        }
        public void UpdateCartItem()
        {
            var cart = HttpContext.Session.GetString("cart");//get key cart
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                if (dataCart.Count > 0)
                {
                    ViewBag.carts = dataCart;                   
                }
            }
        }
        public IActionResult ListCart()
        {
            var cart = HttpContext.Session.GetString("cart");//get key cart
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                if (dataCart.Count > 0)
                {
                    ViewBag.carts = dataCart;
                    return View();
                }
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public string ListStringItem()
        {
            string odata = "<div class=\"order-list\">";
            
            var cart = HttpContext.Session.GetString("cart");//get key cart
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                if (dataCart.Count > 0)
                {
                    ViewBag.carts = dataCart;
                    foreach (var item in dataCart)
                    {
                        odata += "<div class=\"order-list__item\">";
                        odata += "<div class=\"number\">";
                        odata += item.Quantity + "</div>";
                        odata += "<div class=\"info txt-bold\">";
                        odata += item.Product.Name + " </div>";
                        odata += "<div class=\"modal-price price\">";
                        odata += item.Product.Price + "<span> kr</span>";
                        odata += "</div></div>";               

                    }
                    odata += "</div>";
                    return odata;
                }
            }
            return null;
        }
        [HttpGet]
        public List<Cart> ListItem()
        {
            var cart = HttpContext.Session.GetString("cart");//get key cart
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                if (dataCart.Count > 0)
                {
                    ViewBag.carts = dataCart;
                    return dataCart.ToList();
                }
            }
            return null;
        }
        [HttpGet]
        public ActionResult ListCartItem()
        {
            var cart = HttpContext.Session.GetString("cart");//get key cart
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                if (dataCart.Count > 0)
                {
                    ViewBag.carts = dataCart;
                    return PartialView("~/Views/Menu/GetOrder.cshtml",dataCart);
                }
            }
            return PartialView("~/Views/Menu/GetOrder.cshtml",null);
        }
        [HttpPost]
        public IActionResult updateCart(int id, int quantity)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                if (quantity > 0)
                {
                    for (int i = 0; i < dataCart.Count; i++)
                    {
                        if (dataCart[i].Product.Id == id)
                        {
                            dataCart[i].Quantity = quantity;
                        }
                    }


                    HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                }
                var cart2 = HttpContext.Session.GetString("cart");
                return Ok(quantity);
            }
            return BadRequest();

        }
        public IActionResult deleteCart(int id)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);

                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].Product.Id == id)
                    {
                        dataCart.RemoveAt(i);
                    }
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                return RedirectToAction(nameof(ListCart));
            }
            return RedirectToAction(nameof(Index));
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
