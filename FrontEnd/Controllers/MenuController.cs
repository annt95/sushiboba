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
using System.Net;
using System.Text;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json.Linq;

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
            var cart = HttpContext.Session.GetString("cart");//get key cart
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                if (dataCart.Count > 0)
                {
                    foreach (var item in data)
                    {
                        item.Count = dataCart.Where(s => s.Product.Id == item.Id).Select(s => s.Quantity).FirstOrDefault();
                    }
                }
            }
            return View(data);
        }
        public Menu getDetailProduct(int id)
        {
            var data = bobaDA.GetItembyID(id);
            return data;
        }
        //public ActionResult GetMenu()
        //{
        //    var view = "GetMenu";
        //    var data = bobaDA.GetListItem();
            
           
        //    return View(view, data);
        //}
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
        [HttpPost]
        public CartModel deleteItemCart(int id)
        {
            var p = 0;
            var cart = HttpContext.Session.GetString("cart");
            var cModel = new CartModel();
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
                foreach (var item in dataCart)
                {
                    cModel.count += item.Quantity;

                    p += Int32.Parse(item.Product.Price);
                }
                cModel.totalPrice = p.ToString();
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                
            }
            return cModel;

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

                    p += Int32.Parse(item.Product.Price) * item.Quantity;
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
        [HttpPost]
        public ActionResult AddOrder(OrderItem o)
        {
            var cart = HttpContext.Session.GetString("cart");//get key cart
            List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
            //int pid = 1;
            double total = 0;
            var rs = new JsonStatus();
            double distance;
            foreach (var item in dataCart)
            {
                total = total + Int32.Parse(item.Product.Price) * item.Quantity;
            }
            var a = new OrderView();

            a.CustomerName = o.fname;
            a.CustomerPhone = o.fphone;
            a.TotalMoney = total.ToString();
            a.Address = "Pickup at Store";
            a.ShippingTypeID = 1;
            a.ShippingFee =  "0";
            a.Note = o.note; 

            if (o.optdeli == "Ship")
            {
                string postapi = "https://maps.googleapis.com/maps/api/distancematrix/json?origins=Skippergata 32, 0154, Oslo&des&mode=driving&key=AIzaSyCACKAd_ox9H0i3o6CU45Ayp9iEK0PrZz4";
                string des = "&destinations=" + o.location;
                postapi = postapi.Replace("&des", des);

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var jsonString = client.GetStringAsync(postapi).Result;

                Root data = JsonConvert.DeserializeObject<Root>(jsonString);
                var distancedata = "";
                a.Address = o.location;
                if (data.rows.FirstOrDefault().elements.FirstOrDefault().status == "OK")
                {
                    distancedata = data.rows.FirstOrDefault().elements.FirstOrDefault().distance.text.Replace(" km", " ").Trim();
                    distance = Convert.ToDouble(distancedata);
                    if (distance > 10 && total < 1000)
                    {
                        rs.errorcode = 3;
                        rs.msg = "if distance > 10km, bill total at least 1000kr";
                        return Json(rs);
                    }
                    a.ShippingTypeID = 2;
                    a.ShippingFee = "249";
                    a.Note = "Distance: " + distance.ToString() + "Km. " +a.Note;
                    a.TotalMoney = (total + 249).ToString();
                }
                else
                {
                    distancedata = "Error";
                    rs.errorcode = 2;
                    rs.msg = "Location not found, Please try again";
                    return Json(rs);
                    
                }
            }




            var idorder = bobaDA.AddOrder(a);
            foreach (var item in dataCart)
            {
                bobaDA.AddOrderItem(idorder,item.Product.Id,item.Quantity,item.Product.Price);
            }
            rs.errorcode = 0;
            rs.msg = idorder.ToString();

           

            return Json(rs);

        }
        public class JsonStatus
        {
            public int errorcode { get; set; }
            public string msg { get; set; }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Distance
        {
            public string text { get; set; }
            public int value { get; set; }
        }

        public class Duration
        {
            public string text { get; set; }
            public int value { get; set; }
        }

        public class Element
        {
            public Distance distance { get; set; }
            public Duration duration { get; set; }
            public string status { get; set; }
        }

        public class Row
        {
            public List<Element> elements { get; set; }
        }

        public class Root
        {
            public List<string> destination_addresses { get; set; }
            public List<string> origin_addresses { get; set; }
            public List<Row> rows { get; set; }
            public string status { get; set; }
        }


        public List<string> PostData(string url, string myParameters)
        {
            //var cache = new CredentialCache();
            //cache.Add(new Uri(url), "Digest", new NetworkCredential(user, pass));
            //var data = "templateid=" + tempID;
            var request = (HttpWebRequest)WebRequest.Create(url);
            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Create POST data and convert it to a byte array.
            var byteArray = Encoding.UTF8.GetBytes(myParameters);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/json";
            //request.Credentials = cache;
            request.PreAuthenticate = true;
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            var dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            WebResponse response;
            var responseFromServer = new List<string>();
            try
            {
                response = request.GetResponse() as HttpWebResponse;
                // Display the status.
                //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                if (response != null)
                {
                    dataStream = response.GetResponseStream();

                    // Open the stream using a StreamReader for easy access.
                    if (dataStream != null)
                    {
                        var reader = new StreamReader(dataStream);
                        // Read the content.
                        var httpWebResponse = (HttpWebResponse)response;
                        responseFromServer.Add(httpWebResponse.StatusCode.ToString());
                        responseFromServer.Add(reader.ReadToEnd());
                        // Display the content.

                        // Clean up the streams.
                        reader.Close();

                        dataStream.Close();

                    }
                    response.Close();
                }
                return responseFromServer;
            }
            catch (WebException ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                response = ex.Response;
                responseFromServer.Add(((HttpWebResponse)response).StatusCode.ToString());
                responseFromServer.Add(((HttpWebResponse)response).StatusDescription);
            }
            if (!responseFromServer.Any())
            {
                var httpWebResponse = (HttpWebResponse)response;
                if (httpWebResponse != null)
                {
                    responseFromServer.Add(httpWebResponse.StatusCode.ToString());
                    responseFromServer.Add(httpWebResponse.StatusDescription);
                }
            }
            return responseFromServer;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
