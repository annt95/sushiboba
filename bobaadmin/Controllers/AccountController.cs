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
using Microsoft.AspNetCore.Authentication.Cookies;

namespace bobaadmin
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly ILogger<LogoutModel> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        //MyDbContext db = new MyDbContext();
        bobachaContext db = new bobachaContext();
        public IActionResult Login()
        {
            //ViewBag.Message = HttpContext.Session.GetString("username");
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
        
        [HttpPost]
        public async Task<IActionResult> Validate(AdminsItem admin)
        {

            //if (ModelState.IsValid)
            //{
            //    // This doesn't count login failures towards account lockout
            //    // To enable password failures to trigger account lockout, 
            //    // set lockoutOnFailure: true
            //    var result = await _signInManager.PasswordSignInAsync(admin.Email,
            //                       admin.Password, true, lockoutOnFailure: true);
            //    if (result.Succeeded)
            //    {
            //        var principal = new ClaimsPrincipal(new ClaimsIdentity( "admin"));
            //        HttpContext.Authentication.SignInAsync("admin", principal);
            //        _logger.LogInformation("User logged in.");
            //        return Json(new { status = true, message = "Login Successfull!" });
            //    }

            //    else
            //    {
            //        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            //        return Json(new { status = false, message = "Invalid Password!" });
            //    }
            //}
            //else
            //{
            //    ModelState.AddModelError("", "Invalid login attempt.");
            //    return Json(new { status = false, message = "Invalid login attempt." });
            //}



            //if (ModelState.IsValid)
            //{

                var _admin = db.Admins.Where(s => s.Email == admin.Email);
                if (_admin.Any())
                {
                    if (_admin.Where(s => s.Password == admin.Password).Any())
                    {
                        // Create the identity from the user info
                        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, admin.Email));
                        identity.AddClaim(new Claim(ClaimTypes.Name, admin.Email));
                        // You can add roles to use role-based authorization
                        // identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));

                        // Authenticate using the identity
                        var principal = new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = true });

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
            //}
            //else
            //{
            //    var errors = ModelState.Values.SelectMany(v => v.Errors);
            //    System.Diagnostics.Debug.WriteLine(errors);

            //    ModelState.AddModelError("", "Invalid login attempt.");
            //    return Json(new { status = false, message = "Invalid login attempt." });
            //}
        }
        public ActionResult checkLogin(AdminsItem admin)
        {
            var _admin = db.Admins.Where(s => s.Email == admin.Email);

            if (_admin.Any())
            {
                if (_admin.Where(s => s.Password == admin.Password).Any())
                {
                    //HttpContext.Session.SetString("username", admin.Email);
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

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

    }

}