using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using oblig2_Yevhen_Verkhalantsev.Web.Models;

namespace oblig1_Yevhen_Verkhalantsev.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}