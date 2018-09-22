namespace FAS.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using FAS.Data.Models;
    using FAS.Services.Contracts;
    using FAS.Services.Models;
    using FAS.Web.Infrastructure;
    using FAS.Web.Infrastructure.Extensions;
    using FAS.Web.Models.Sports;
    using Microsoft.AspNetCore.Authorization;

  
    public class SportController : Controller
    {
        private const int PageSize = 25;
        private const string UsersView = "Members";

        private readonly ISportService sportService;
        private readonly UserManager<User> userManager;

        public SportController(ISportService sportService, UserManager<User> userManager)
        {
            this.sportService = sportService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index() => View();

        public async Task<IActionResult> Details(int id)
        {
            var model = new SportDetailsViewModel
            {
                Sport = await this.sportService.ByIdAsync<SportDetailsServiceModel>(id)
            };

            if (model.Sport == null)
            {
                return this.NotFound();
            }

            if (this.User.Identity.IsAuthenticated)
            {
                var userId = this.userManager.GetUserId(this.User);

                model.IsUserEnrolledInSport = await this.sportService.IsUserSignedInSportAsync(id, userId);
            }

            return this.View(model);
        }

        [Authorize(Roles = WebConstants.AdministratorRole)]
        public async Task<IActionResult> Create()
        {
            return View(new AddSportFormModel
            {
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(20),
                Trainers = await GetTrainers()
            });
        }

        [HttpPost]
        [Authorize(Roles = WebConstants.AdministratorRole)]
        public async Task<IActionResult> Create(AddSportFormModel model)
        {
            var user = await this.userManager.FindByIdAsync(model.TrainerId);
            if (user == null)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid user.");
            }

            if (!this.ModelState.IsValid)
            {
                model.Trainers = await GetTrainers();
                return View(model);
            }

            await this.sportService.Create(
                model.Name,
                model.Description,
                model.StartDate,
                model.EndDate,
                model.TrainerId);

            this.TempData.AddSuccessMessage("Sport created successfully.");

            return this.RedirectToAction(
                nameof(HomeController.Index),
                "Home",
                routeValues: new { area = string.Empty });
        }
         
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SignUpUser(int id)
        {
            var userId = this.userManager.GetUserId(this.User);
            var success = await this.sportService.SignUpUserAsync(id, userId);

            if (!success)
            {
                return BadRequest();
            }

            this.TempData.AddSuccessMessage("You successfully enrolled in this sport.");

            return this.RedirectToAction(nameof(Details), new { id });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SignOutUser(int id)
        {
            var userId = this.userManager.GetUserId(this.User);
            var success = await this.sportService.SignOutUserAsync(id, userId);

            if (!success)
            {
                return BadRequest();
            }

            this.TempData.AddSuccessMessage("You successfully signed out of this sport.");

            return this.RedirectToAction(nameof(Details), new { id });
        }

        private async Task<IEnumerable<SelectListItem>> GetTrainers()
        {
            var trainers = await this.userManager.GetUsersInRoleAsync(WebConstants.TrainerRole);
            return trainers
                 .Select(t => new SelectListItem
                 {
                     Text = $"{t.FirstName} ({t.LastName})",
                     Value = t.Id
                 })
                 .OrderBy(t => t.Text)
                 .ToList();
        }

    }
}