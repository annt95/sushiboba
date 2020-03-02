using bobaadmin.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
namespace bobaadmin
{
    public class AccountController : Controller
    {
        MyDbContext db = new MyDbContext();
        public IActionResult Login()
        {
            ViewBag.Message = HttpContext.Session.GetString("username");
            return View();
        }
        public ActionResult Validate(Admins admin)
        {
            var _admin = db.Admins.Where(s => s.Email == admin.Email);
            if (_admin.Any())
            {
                if (_admin.Where(s => s.Password == admin.Password).Any())
                {

                    return Json(new { status = true, message = "Login Successfull!" });
                }
                else
                {
                    return Json(new { status = false, message = "Invalid Password!" });
                }
            }
            else
            {
                return Json(new { status = false, message = "Invalid User!" });
            }
        }
        public ActionResult checkLogin(Admins admin)
        {
            var _admin = db.Admins.Where(s => s.Email == admin.Email);

            if (_admin.Any())
            {
                if (_admin.Where(s => s.Password == admin.Password).Any())
                {
                    HttpContext.Session.SetString("username", admin.Email);
                    return Json(new { status = true, message = "Login Successfull!" });
                }
                else
                {
                    return Json(new { status = false, message = "Invalid Password!" });
                }
            }
            else
            {
                return Json(new { status = false, message = "Invalid User!" });
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }

    }

}