using bobaadmin.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
namespace bobaadmin
{
    public class AccountController : Controller
    {
        MyDbContext db = new MyDbContext();
        public IActionResult Login()
        {
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

    }

}