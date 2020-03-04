using bobaadmin.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace bobaadmin
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly ILogger<LogoutModel> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        MyDbContext db = new MyDbContext();
        public IActionResult Login()
        {
            ViewBag.Message = HttpContext.Session.GetString("username");
            return View();
        }
        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Validate(Admins admin)
        {
           
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, 
                // set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(admin.Email,
                                   admin.Password, true, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    var principal = new ClaimsPrincipal(new ClaimsIdentity( "admin"));
                    HttpContext.Authentication.SignInAsync("admin", principal);
                    _logger.LogInformation("User logged in.");
                    return Json(new { status = true, message = "Login Successfull!" });
                }
                
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Json(new { status = false, message = "Invalid Password!" });
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return Json(new { status = false, message = "Invalid login attempt." });
            }





            //var _admin = db.Admins.Where(s => s.Email == admin.Email);
            //if (_admin.Any())
            //{
            //    if (_admin.Where(s => s.Password == admin.Password).Any())
            //    {

            //        return Json(new { status = true, message = "Login Successfull!" });
            //    }
            //    else
            //    {
            //        return Json(new { status = false, message = "Invalid Password!" });
            //    }
            //}
            //else
            //{
            //    return Json(new { status = false, message = "Invalid User!" });
            //}
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