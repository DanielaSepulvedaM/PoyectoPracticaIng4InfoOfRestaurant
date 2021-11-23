using Entidades.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppComensal.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConsumibleController : ControllerBase
    {
        private readonly IRepositorioConsumible repositorioConsumible;

        public ConsumibleController(IRepositorioConsumible repositorioConsumible)
        {
            this.repositorioConsumible = repositorioConsumible;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Foto(int id)
        {
            var imgBytes = await repositorioConsumible.ObtenerFoto(id);
            if (imgBytes == null)
                return BadRequest();

            return File(imgBytes, "image/jpg");
        }
    }
}
