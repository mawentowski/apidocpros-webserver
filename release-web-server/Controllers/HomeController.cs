using Microsoft.AspNetCore.Mvc;
using ReleaseWebServer.Models;
using System.Diagnostics;

namespace ReleaseWebServer
{
    public class HomeController : Controller
    {
        [ResponseCache(NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}