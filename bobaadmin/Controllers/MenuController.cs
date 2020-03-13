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
        protected MyDbContext dbct;
        public IActionResult Index()
        {
            return View();
        }
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
        public IQueryable<Menu> GetListKendoUI()
        {
            return dbct.MenuView;
        }
        //public IQueryable<Menu> GetListKendoUI(bool issushi)
        //{

        //    var data = dbct.LoadStoredProc("SelectMenu")
        //               .WithSqlParam("@issushi", issushi)
        //               .ExecuteStoredProc<Menu>();
        //    return data;
        //}
    }
}