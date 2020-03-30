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
            var posts = GetListKendoUI().Where(s => s.Issushi == true);
            var data = posts.Select(t => new
            {
                t.Id,
                t.Name,
                t.Description,
                t.Images,
                t.Price,
                t.Active,
                t.Ishot,
                t.Issushi,
                t.Ismilktea
            });
            return Json(data.ToDataSourceResult(take, skip, sort, filter), new Newtonsoft.Json.JsonSerializerSettings());
        }
        //public ActionResult GetListKendoUI()
        //{
        //    var data = 
        //}
        private IQueryable<Menu> GetListKendoUI()
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
        public ActionResult EditForm(int id = 0, string renew = null)
        {
            //var NumberRecord = WebConfig.NumberRecord;
            //var NumberRecordCategory = WebConfig.NumberRecordCategory;
            int total = 0;
            var obj = new AdminModel();
            //if (id > 0)
            //{
            //    obj = GetArticleById(id);
            //    //ViewBag.AllowEdit = CheckEditCTV(obj);
            //    if (obj.Menu != null && obj.Menu.EventID != null)
            //    {
            //        var eventRef = eventRepository.GetEventsById(obj.Menu.EventID.Value);
            //        obj.listEvents = new List<EventItems>();//eventRepository.GetList(out total, 0, NumberRecord, "ID", true, false);
            //        if (eventRef != null)
            //            obj.listEvents.Add(eventRef);
            //    }
            //    if (obj.NewsItem != null && renew != null)
            //    {
            //        var authorID = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey;
            //        var newid = articleRepository.GetDraft(authorID);
            //        obj.NewsItem.OldID = obj.NewsItem.ID;
            //        obj.NewsItem.ID = newid.NewsItem.ID;
            //        obj.NewsItem.TitleAscii = obj.NewsItem.Title.Trim().GetSeName();
            //        obj.NewsItem.IsRenew = true;
            //    }
            //}
            //else
            //{
            //    var authorID = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey;
            //    obj = articleRepository.GetDraft(authorID);
            //    obj.listEvents = new List<EventItems>();//eventRepository.GetList(out total, 0, NumberRecord, "ID", true, false);
            //}
            //if (obj.NewsItem == null)
            //    return HttpNotFound();
            //obj.listCategory = articleTypeRepository.GetList(out total, 0, NumberRecordCategory, "ID", false, false);
            //obj.listCampaign = articleRepository.GetListCampaign();
            //obj.listProductRef = GetListProductReferent(id.ToString());
            //obj.listTagsRef = GetListTagsReferent(id.ToString());
            //obj.listNewsRef = GetListNewsReferent(id.ToString());
            return View(obj);
        }



        /// <summary>
        ///   
        /// </summary>
        /// <returns></returns>

        public ActionResult Index()
        {
            return View(db.Menu.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDoctor(Menu menu)
        {
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
                db.Menu.Remove(doctor);
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
        public ActionResult UpdateDoctor(Menu doctor)
        {
            Menu d = db.Menu.Where(s => s.Id == doctor.Id).First();
            d.Price = doctor.Price;
            d.Description = doctor.Description;
            db.SaveChanges();
            return RedirectToAction("Index", "Menu");
        }
    }
}