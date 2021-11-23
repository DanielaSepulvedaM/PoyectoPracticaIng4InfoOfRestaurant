using Identity.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAdministracion.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class RolesController : Controller
    {
        private readonly RoleManager<Rol> roleManager;

        public RolesController(RoleManager<Rol> roleManager)
        {
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        public IActionResult Crear() {
            return View(new Rol());
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Rol rol) {
            if (!ModelState.IsValid)
                return View(rol);

            var r = await roleManager.CreateAsync(rol);
            if (r.Succeeded)
                return RedirectToAction("Index");
            else
                return View(rol);
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(string RolID)
        {
            var rol = await roleManager.FindByIdAsync(RolID);
            var r = await roleManager.DeleteAsync(rol);
            if (r.Succeeded)
                return RedirectToAction("Index");
            else
                return View(rol);
        }

        [HttpGet("{RolId}")]
        public async Task<IActionResult> Editar(string RolId) {
            var rol = await roleManager.FindByIdAsync(RolId);
            return View(rol);
        }

        [HttpPost("{RolId}")]
        public async Task<IActionResult> Editar(string RolId, Rol rolModel)
        {
            if (!ModelState.IsValid)
                return View(rolModel);

            var rol = await roleManager.FindByIdAsync(RolId);
            rol.Name = rolModel.Name;
            await roleManager.UpdateAsync(rol);
            return RedirectToAction("Index");
        }
    }
}
