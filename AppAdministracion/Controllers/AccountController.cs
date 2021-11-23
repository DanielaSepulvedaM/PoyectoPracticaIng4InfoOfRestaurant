using Identity.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAdministracion.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Usuario> signInManager;

        public AccountController(SignInManager<Usuario> signInManager)
        {
            this.signInManager = signInManager;
        }


        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToPage("/Account/Login");
        }
    }
}
