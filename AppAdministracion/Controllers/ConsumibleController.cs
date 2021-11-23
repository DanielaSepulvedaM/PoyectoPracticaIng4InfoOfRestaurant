using AppAdministracion.Infraestructura.ControllerBase;
using AppAdministracion.Models;
using Entidades;
using Entidades.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppAdministracion.Controllers
{
    public class ConsumibleController : FuncionarioBaseController
    {
        private readonly IRepositorioConsumible repositorioConsumible;

        public ConsumibleController(IRepositorioConsumible repositorioConsumible)
        {
            this.repositorioConsumible = repositorioConsumible;
        }

        public async Task<IActionResult> Index()
        {
            var consumibles = await repositorioConsumible.Listar(this.RestauranteID);
            return View(consumibles);
        }

        [HttpGet]
        public IActionResult Crear() {
            return View(new ConsumibleViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ConsumibleViewModel consumibleModel) {
            if (!ModelState.IsValid)
                return View(consumibleModel);

            if (consumibleModel.Foto != null)
            {
                using (var memStream = new MemoryStream())
                {
                    await consumibleModel.Foto.CopyToAsync(memStream);
                    consumibleModel.Imagen = memStream.ToArray();
                }
            }

            await repositorioConsumible.Crear(this.RestauranteID, consumibleModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Foto(int id) {
            var imgBytes = await repositorioConsumible.ObtenerFoto(id);
            if (imgBytes == null)
                return BadRequest();

            return File(imgBytes, "image/jpg");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var c = await repositorioConsumible.Obtener(id);
            var model = new ConsumibleViewModel
            {
                ConsumibleID = c.ConsumibleID,
                Descripcion = c.Descripcion,
                Nombre = c.Nombre,
                Precio = c.Precio
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ConsumibleViewModel consumibleModel)
        {
            if (!ModelState.IsValid)
                return View(consumibleModel);

            if (consumibleModel.Foto != null)
            {
                using (var memStream = new MemoryStream())
                {
                    await consumibleModel.Foto.CopyToAsync(memStream);
                    consumibleModel.Imagen = memStream.ToArray();
                }
            }

            await repositorioConsumible.Editar(consumibleModel);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id) {
            await repositorioConsumible.Eliminar(id);
            return RedirectToAction("Index");
        }
    }
}
