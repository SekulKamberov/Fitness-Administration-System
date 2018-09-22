namespace FAS.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Diagnostics;
    using System.Collections.Generic;

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using FAS.Web.Models;
    using FAS.Services.Contracts;
    using FAS.Services.Models;

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ISportService sportService;

        public HomeController(ISportService sportService)
        {
            this.sportService = sportService;
        }

        public async Task<IActionResult> Index() => View(await this.sportService.AllActiveAsync());

        public IActionResult About()
        {
            ViewData["Message"] = "sechko about.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "sechko contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
