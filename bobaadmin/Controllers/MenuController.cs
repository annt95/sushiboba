using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
namespace bobaadmin
{
    public class MenuController : Controller
    {
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
    }
}