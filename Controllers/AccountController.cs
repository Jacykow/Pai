using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pai.Areas.Identity.Data;
using Pai.Models;

namespace Pai.Controllers
{
    [Route("Identity/Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<PaiUser> _userManager;

        public AccountController(
            UserManager<PaiUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("ConfirmEmail")]
        public IActionResult ConfirmEmail([FromQuery] string userid,[FromQuery] string code)
        {
            PaiUser user = _userManager.FindByIdAsync(userid).Result;
            IdentityResult result = _userManager.
                        ConfirmEmailAsync(user, code).Result;
            if (result.Succeeded)
            {
                ViewBag.Message = "Email confirmed successfully!";
                return View("Success");
            }
            else
            {
                ViewBag.Message = "Error while confirming your email!";
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
}
