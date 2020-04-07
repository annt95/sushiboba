using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Kendo.DynamicLinq;
using bobaadmin.Models;
using System.Linq;

namespace bobaadmin
{
    public class MenuController : Controller
    {
        MyDbContext db = new MyDbContext();
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
        public AdminModel GetArticleById(int Id)
        {
            var result = new AdminModel();

           // var query = db.Database.SqlQuery<Menu>("exec [dbo].[db] @Id", new SqlParameter("Id", Id));
            var query = db.LoadStoredProc("[GetMenuId]")                    
                       .WithSqlParam("@Id", Id)
                       .ExecuteStoredProc<Menu>();
            result.Menu = query.Select(p => new Menu()
            {
                Id = p.Id,

            }).FirstOrDefault();
            return result;
        }
        

        public ActionResult Index()
        {
            var a = db.Menu.Where(s => s.Isdelete != true).ToList();
            return View(a);
        }
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