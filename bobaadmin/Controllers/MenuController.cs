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
                id = p.id,

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
            return View(db.MenuItems.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult CreateDoctor(MenuItems menu)
        //{
        //    db.MenuItems.Add(menu);
        //    db.SaveChanges();
        //    return RedirectToAction("Index", "Doctors");
        //}
        //[HttpPost]
        //public bool Delete(int id)
        //{
        //    try
        //    {
        //        Doctors doctor = db.Doctors.Where(s => s.Id == id).First();
        //        db.Doctors.Remove(doctor);
        //        db.SaveChanges();
        //        return true;
        //    }
        //    catch (System.Exception)
        //    {
        //        return false;
        //    }

        //}
        //public ActionResult Update(int id)
        //{
        //    return View(db.Doctors.Where(s => s.Id == id).First());
        //}
        //[HttpPost]
        //public ActionResult UpdateDoctor(Doctors doctor)
        //{
        //    Doctors d = db.Doctors.Where(s => s.Id == doctor.Id).First();
        //    d.Name = doctor.Name;
        //    d.Phone = doctor.Phone;
        //    d.Specialist = doctor.Specialist;
        //    db.SaveChanges();
        //    return RedirectToAction("Index", "Doctors");
        //}
    }
}