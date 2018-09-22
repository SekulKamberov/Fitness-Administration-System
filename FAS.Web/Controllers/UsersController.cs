namespace FAS.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;

    using FAS.Services.Contracts;
    using FAS.Services.Models;

    using FAS.Web.Models;
    using FAS.Web.Models.Users;
    using FAS.Web.Infrastructure;
    using FAS.Web.Infrastructure.Extensions;

    using FAS.Data.Models;

    [Authorize(Roles =  WebConstants.AdministratorRole)]
    public class UsersController : Controller
    {
        private const int PageSize = 10;
        private const string UsersView = "Members";

        private readonly IUserService userService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public UsersController(IUserService userService, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.userService = userService;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await this.userService.AllAsync();

            var roles = await this.roleManager
                .Roles
                .OrderBy(r => r.Name)
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name 
                })
                .ToListAsync();

            return this.View(new UserListingViewModel
            {
                Users = users.OrderBy(u => u.FirstName).ThenBy(u => u.LastName),
                Roles = roles
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(AddUserToRoleFormModel model)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(model.Role);
            var user = await this.userManager.FindByIdAsync(model.UserId);
            var userExists = user != null;

            if (!roleExists || !userExists)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid identity details.");
            }

            if (!this.ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            await this.userManager.AddToRoleAsync(user, model.Role);

            this.TempData.AddSuccessMessage($"User {user.UserName} added to role {model.Role}");

            return RedirectToAction(nameof(Index));
        }

        
        public IActionResult All(int page = 1)
             => View(new UserPageListModel()
             {
                 Users = this.userService.All(page, PageSize),
                 CurrentPage = page,
                 TotalPages = (int)Math.Ceiling(this.userService.UserCount() / (double)PageSize)
             });

        public IActionResult Registration()
        {
            ViewData["Add new mamber"] = "Add new mamber page";

            return View();
        }
        
        [Route("gander/{id}")]
        public IActionResult Gander(int id)
        {
            return View("All", new UserPageListModel()
            {
                Users = this.userService.SortByGander(id, 1, PageSize),
                CurrentPage = 1,
                TotalPages = (int)Math.Ceiling(this.userService.UserCount() / (double)PageSize)
            });
        }

        public IActionResult Blocked()
        {
            return View("All", new UserPageListModel()
            {
                Users = this.userService.BlockedUsers(WebConstants.StartPage, WebConstants.PageSize),
                CurrentPage = WebConstants.StartPage,
                TotalPages = (int)Math.Ceiling(this.userService.UserCount() / (double)PageSize)
            });
        }

        [Route("profile/{id}")]
        public IActionResult Profile(string id)
        {
            var user = this.userService.UserById(id);
            if (user == null)
            {
                return NotFound();
            }

            ViewData["Title"] = $"{user.FirstName} {user.LastName} Details";

            return View("Profile", user);
        }

        public IActionResult Edit(string id)
        {
            var user = this.userService.UserById(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult NotPaidYet()
        {
            return View(new UserPageListModel()
            {
                Users = this.userService.NotPaid(WebConstants.StartPage, WebConstants.PageSize),
                CurrentPage = WebConstants.StartPage,
                TotalPages = (int)Math.Ceiling(this.userService.UserCount() / (double)PageSize)
            });
        }

        [HttpPost]
        public IActionResult Edit(UserDetailsModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            this.userService.Edit(user);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
