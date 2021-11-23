using AppAdministracion.Infraestructura.ControllerBase;
using Entidades;
using Entidades.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QRCoder;
using System;
using System.Collections.Generic;
using System.IO;
using static QRCoder.PayloadGenerator;

namespace AppAdministracion.Controllers
{
    public class MesaController : FuncionarioBaseController
    {
        private readonly IRepositorioMesa repositorioMesa;
        private readonly IConfiguration configuration;

        public MesaController(IRepositorioMesa repositorioMesa, IConfiguration configuration)
        {
            this.repositorioMesa = repositorioMesa;
            this.configuration = configuration;
        }

        public ActionResult Index()
        {
            IEnumerable<Mesa> mesas = repositorioMesa.Listar(this.RestauranteID);
            return View(mesas);
        }


        public ActionResult Crear()
        {
            return View(new Mesa());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Mesa model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.Restaurante = new Restaurante {
                RestauranteID = this.RestauranteID
            };

            repositorioMesa.Crear(model);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Editar(int id)
        {
            Mesa mesa = repositorioMesa.Obtener(id);
            if (mesa is null)
                return View("NoExiste");

            if(mesa.Restaurante.RestauranteID != this.RestauranteID)
                return RedirectToPage("/Account/Unauthorized");

            return View(mesa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, Mesa model)
        {
            if (!ModelState.IsValid)
                return View(model);

            repositorioMesa.Editar(id, model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int id)
        {
            Mesa mesa = repositorioMesa.Obtener(id);
            if (mesa is null)
                return View("NoExiste");

            if (mesa.Restaurante.RestauranteID != this.RestauranteID)
                return RedirectToPage("/Account/Unauthorized");

            repositorioMesa.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult GenerarQR(int id) {
            var action = Url.Action("Index", "Home", new { this.RestauranteID, mesaId = id }, "https", configuration["urlAppComensales"]);

            Url generator = new Url(action);
            string payload = generator.ToString();

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            var qrCodeAsBitmap = qrCode.GetGraphic(20);

            byte[] image = null;
            using (var memStream = new MemoryStream())
            {
                qrCodeAsBitmap.Save(memStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                memStream.Position = 0;
                image = memStream.ToArray();
            }
            return File(image, "image/jpeg");
        }


    }
}
