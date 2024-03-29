﻿// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System;
using TNMarketplace.Core.Entities;
using Microsoft.AspNetCore.Http.Features;
using TNMarketplace.Service;

namespace TNMarketplace.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApplicationDataService _applicationDataService;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager, IApplicationDataService applicationDataService)
        {
            _userManager = userManager;
            _applicationDataService = applicationDataService;
        }

        [HttpPost("api/setlanguage")]
        public IActionResult SetLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect("~/");
        }

        [HttpGet("api/applicationdata")]
        public async Task<IActionResult> Get()
        {
            var appData = await _applicationDataService.GetApplicationData(Request.HttpContext);

            return Ok(appData);
        }
    }
}
