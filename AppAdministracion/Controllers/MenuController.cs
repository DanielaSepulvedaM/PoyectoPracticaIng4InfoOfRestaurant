using AppAdministracion.Infraestructura.ControllerBase;
using AppAdministracion.Models;
using Datos.Models;
using Entidades.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAdministracion.Controllers
{
    public class MenuController : FuncionarioBaseController
    {
        private readonly IRepositorioMenu repositorioMenu;
        private readonly IRepositorioConsumible repositorioConsumible;

        public MenuController(IRepositorioMenu repositorioMenu, IRepositorioConsumible repositorioConsumible)
        {
            this.repositorioMenu = repositorioMenu;
            this.repositorioConsumible = repositorioConsumible;
        }

        [HttpGet]
        public IActionResult Index() {
            var menu = repositorioMenu.Listar(this.RestauranteID);
            return View(menu);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(MenuViewModel menuModel)
        {
            if (!ModelState.IsValid)
                return View(menuModel);

            var menu = new Entidades.Menu {
                Nombre = menuModel.Nombre,
                Descripcion = menuModel.Descripcion,
                Consumibles = menuModel.ConsumiblesIds.Select(c => new Entidades.Consumible { ConsumibleID = c }).ToList()
            };

            repositorioMenu.Crear(menu, this.RestauranteID);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(int id)
        {
            repositorioMenu.Eliminar(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var menu = repositorioMenu.Obtener(id);
            if (menu.Restaurante.RestauranteID != this.RestauranteID)
                return RedirectToPage("/Account/Unauthorized");

            var viewModel = new MenuViewModel
            {
                Nombre = menu.Nombre,
                Descripcion = menu.Descripcion,
                MenuID = menu.MenuID
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int id, MenuViewModel menuModel)
        {
            if (!ModelState.IsValid)
                return View(menuModel);

            var menu = new Entidades.Menu
            {
                Nombre = menuModel.Nombre,
                Descripcion = menuModel.Descripcion,
                Consumibles = menuModel.ConsumiblesIds.Select(c => new Entidades.Consumible { ConsumibleID = c }).ToList()
            };

            repositorioMenu.Editar(menu, id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Listar(int? menuId = null)
        {
            var consumibles = await repositorioConsumible.Listar(this.RestauranteID);
            if (menuId.HasValue)
            {
                var consumiblesActuales = repositorioMenu.Obtener(menuId.Value).Consumibles;
                var dict = consumiblesActuales.ToDictionary(c => c.ConsumibleID);
                var filtrados = consumibles.Where(c => !dict.ContainsKey(c.ConsumibleID));

                var data = new
                {
                    disponibles = filtrados.Select(c => new ConsumibleViewModel(c.ConsumibleID, c.Nombre)),
                    agregados = consumiblesActuales.Select(c => new ConsumibleViewModel(c.ConsumibleID, c.Nombre))
                };
                return Json(data);
            }
            else
            {
                var data = new
                {
                    disponibles = consumibles.Select(c => new ConsumibleViewModel(c.ConsumibleID, c.Nombre)),
                    agregados = new List<ConsumibleViewModel>()
                };
                return Json(data);
            }
        }

        public class ConsumibleViewModel
        {
            public int ConsumibleId { get; set; }
            public string Nombre { get; set; }

            public ConsumibleViewModel(int consumibleId, string nombre)
            {
                ConsumibleId = consumibleId;
                Nombre = nombre;
            }
        }
    }
}
