﻿using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace StudentCourseManagement.Controllers
{
    public class CultureController : Controller
    {
        public IActionResult SetCulture(string culture, string returnUrl)
        {
            Response.Cookies.Append(
           CookieRequestCultureProvider.DefaultCookieName,
           CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
           new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(2) }
       );

            return LocalRedirect(returnUrl);
        }
    }
}
