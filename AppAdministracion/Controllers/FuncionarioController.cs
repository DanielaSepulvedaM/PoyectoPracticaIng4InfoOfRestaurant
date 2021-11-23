using AppAdministracion.Models;
using Entidades.Interfaces;
using Identity.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AppAdministracion.Controllers
{
    [Route("Restaurante/{restauranteID}/Funcionario")]
    [Authorize(Roles = "Administrador")]
    public class FuncionarioController : Controller
    {
        private readonly UserManager<Usuario> usrManager;
        private readonly RoleManager<Rol> roleManager;
        private readonly IRepositorioFuncionario repositorioFunc;

        public FuncionarioController(UserManager<Usuario> usrManager, RoleManager<Rol> roleManager, IRepositorioFuncionario repositorioFunc)
        {
            this.usrManager = usrManager;
            this.roleManager = roleManager;
            this.repositorioFunc = repositorioFunc;
        }

        [HttpGet("Listar")]
        public IActionResult Listar([FromRoute]int restauranteID)
        {
            var funcRestauranteIds = repositorioFunc.Listar(restauranteID).ToArray();
            var funcionarios = usrManager.Users.Where(u => funcRestauranteIds.Contains(u.Id));
            return View(funcionarios);
        }

        [HttpGet("Crear")]
        public IActionResult Crear()
        {
            return View(new FuncionarioViewModel());
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> Crear([FromRoute]int restauranteID, FuncionarioViewModel usuario)
        {
            if (!ModelState.IsValid)
                return View(usuario);

            var result = await usrManager.CreateAsync(usuario, "Secret123.");
            if (!result.Succeeded)
            {
                // TODO: mostrar error
                foreach(IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View(usuario);
            }

            var r = await usrManager.AddToRoleAsync(usuario, usuario.Rol);
            if (!r.Succeeded)
            {
                // TODO: mostrar error
                return View(usuario);
            }

            repositorioFunc.AsociarRestaurante(restauranteID, usuario.Id);
            return RedirectToAction("Listar", new { restauranteID = restauranteID });
        }

        [HttpPost("Eliminar")]
        public async Task<IActionResult> Eliminar([FromRoute] int restauranteID, string funcionarioID)
        {
            repositorioFunc.DesasociarRestaurante(restauranteID, funcionarioID);
            var usuario = await usrManager.FindByIdAsync(funcionarioID);
            await usrManager.DeleteAsync(usuario);
            return RedirectToAction("Listar", new { restauranteID = restauranteID });
        }

        [HttpGet("Editar/{funcionarioID}")]
        public async Task<IActionResult> Editar([FromRoute]string funcionarioID)
        {
            ViewBag.Roles = roleManager.Roles.Select(r => r.Name);

            var usuario = await usrManager.FindByIdAsync(funcionarioID);
            var usuModel = new FuncionarioViewModel(usuario);

            var usrRoles = await usrManager.GetRolesAsync(usuario);
            if (usrRoles.Any())
                usuModel.Rol = usrRoles.First();

            return View(usuModel);
        }

        [HttpPost("Editar/{funcionarioID}")]
        public async Task<IActionResult> Editar([FromRoute] int restauranteID, [FromRoute] string funcionarioID, FuncionarioViewModel usuModel)
        {
            if(!ModelState.IsValid)
                return View(usuModel);

            var usuario = await usrManager.FindByIdAsync(funcionarioID);
            usuario.UserName = usuModel.UserName;
            usuario.Documento = usuModel.Documento;
            usuario.Email = usuModel.Email;
            usuario.PhoneNumber = usuModel.PhoneNumber;

            IdentityResult result = await usrManager.UpdateAsync(usuario);
            if(!result.Succeeded)
                return View(usuModel);

            var usrRoles = await usrManager.GetRolesAsync(usuario);
            if (!usrRoles.Any())
            {
                await usrManager.AddToRoleAsync(usuario, usuModel.Rol);
            }
            else if (usrRoles.First() != usuModel.Rol)
            {
                await usrManager.RemoveFromRoleAsync(usuario, usrRoles.First());
                await usrManager.AddToRoleAsync(usuario, usuModel.Rol);
            }

            return RedirectToAction("Listar", new { restauranteID });
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewBag.RestauranteId = context.HttpContext.Request.RouteValues["restauranteID"];
            ViewBag.Roles = roleManager.Roles.Select(r => r.Name);
            base.OnActionExecuted(context);
        }
    }
}
