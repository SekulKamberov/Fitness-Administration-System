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
    using FAS.Web.Models.HomeViewModels;

    [Authorize]
    public class HomeController : Controller
    {
        private const int pageSize = 10;
        private const int page = 1;

        private readonly ISportService sportService;
        private readonly IUserService userService;

        public HomeController(ISportService sportService, IUserService userService)
        {
            this.sportService = sportService;
            this.userService = userService;
        }

        public async Task<IActionResult> Index() => View(await this.sportService.AllActiveAsync());

        public IActionResult Search(string search)
        {
            var users = this.userService.AllWithBlocked();

            if (!string.IsNullOrWhiteSpace(search))
            {
                users = users.Where(s => s.FirstName.ToLower().Contains(search.ToLower()));

            }

            return View(new HomeIndexUserPagingModel
            {
                Users = users.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                Search = search,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(users.Count() / (double)pageSize)
            });
        }

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
