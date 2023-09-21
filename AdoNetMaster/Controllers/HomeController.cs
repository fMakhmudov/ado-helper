using AdoNetMaster.Core.Interfaces;
using AdoNetMaster.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace AdoNetMaster.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAppLogger _logger;
        private readonly IConfiguration _config;

        public HomeController(IAppLogger logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

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

    class Station
    {
        public string begin_station_code { get; set; }
        public string end_station_code { get; set; }
    }
}
