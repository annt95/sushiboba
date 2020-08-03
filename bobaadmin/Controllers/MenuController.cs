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
    public class MenuController : Controller
    {
        public MenuController(BobaDA bobaDA)
        {
            this.bobaDA = bobaDA; ;
        }

        private BobaDA bobaDA;
        //MyDbContext db = new MyDbContext();
        bobachaContext db = new bobachaContext();
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult Sushi()
        {
            return View();
        }public IActionResult Milktea()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetListKendoUI(int take = 20, int skip = 0, IEnumerable<Kendo.DynamicLinq.Sort> sort = null, Kendo.DynamicLinq.Filter filter = null)
        {
            var posts = GetListKendoUI().Where(s => s.issushi == true);
            var data = posts.Select(t => new
            {
                t.id,
                t.name,
                t.description,
                t.images,
                t.price,
                t.active,
                t.ishot,
                t.issushi,
                t.ismilktea
            });
            return Json(data.ToDataSourceResult(take, skip, sort, filter), new Newtonsoft.Json.JsonSerializerSettings());
        }
        //public ActionResult GetListKendoUI()
        //{
        //    var data = 
        //}
        private IQueryable<MenuItems> GetListKendoUI()
        {
            var results = db.vw_menuadmin;
            return results;
        }
        public Menu GetArticleById(int Id)
        {

            var data = bobaDA.GetListAdminItembyId(Id);
            return data;
        }

        public IActionResult Index()
        {
            var data = bobaDA.GetListAdminItem();
            return View(data);
        }

        //public ActionResult Index()
        //{
        //    var a = db.Menu.Where(s => s.Isdelete != true).ToList();
        //    return View(a);
        //}
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateMenu(Menu menu)
        //public ActionResult CreateMenu(string Name, string Description, string Images, string Price, bool Issushi = false, bool Ismilktea = false)
        {
            //Menu menu = new Menu();
            menu.Active = true;
            menu.Isdelete = false;
            db.Menu.Add(menu);
            db.SaveChanges();
            return RedirectToAction("Index", "Menu");
        }
        [HttpPost]
        public bool Delete(int id)
        {
            try
            {
                Menu doctor = db.Menu.Where(s => s.Id == id).First();
                doctor.Isdelete = true;
                //db.Menu.Remove(doctor);
                //var query = db.LoadStoredProc("[GetMenuId]")
                //      .WithSqlParam("@Id", Id)
                //      .ExecuteStoredProc<Menu>();

                db.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }

        }
        public ActionResult Update(int id)
        {
            var data = bobaDA.GetListAdminItembyId(id);
            return View(data);
            return View(db.Menu.Where(s => s.Id == id).First());
        }
        [HttpPost]
        public ActionResult UpdateMenu(Menu menu)
        {
            Menu d = db.Menu.Where(s => s.Id == menu.Id).First();
            d.Price = menu.Price;
            d.Description = menu.Description;
            d.Images = menu.Images;
            d.Name = menu.Name;
            d.Issushi = menu.Issushi;
            d.Ismilktea = menu.Ismilktea;
            db.SaveChanges();
            return RedirectToAction("Index", "Menu");
        }
    }
}