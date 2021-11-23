using Entidades;
using Entidades.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppAdministracion.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class RestauranteController : Controller
    {
        private readonly IRepositorioRestaurante repositorio;

        public RestauranteController(IRepositorioRestaurante repositorio)
        {
            this.repositorio = repositorio;
        }
        [HttpGet]
        public IActionResult ListarRestaurantes()
        {
            var restaurantes = repositorio.ListarRestaurantes();
            return View(restaurantes);
        }

        //Devuelve la vista de formulario 
        [HttpGet]
        public IActionResult CrearRestaurante()
        {
            return View();
        }

        //Crea el restaurante enviando el Post de un restaurante
        [HttpPost]
        public IActionResult CrearRestaurante(Restaurante restaurante) {
            repositorio.CrearRestaurante(restaurante);
            return RedirectToAction("ListarRestaurantes");
        }

        [HttpPost]
        public IActionResult EliminarRestaurantes(int restauranteID)//cuando se envia parametos van por el cuerpo del formulario
        {
            repositorio.EliminarRestaurante(restauranteID);
            return RedirectToAction("ListarRestaurantes");
        }

        [HttpGet]
        public IActionResult EditarRestaurante(int restauranteID)
        {
            var restaurante = repositorio.ObtenerRestaurante(restauranteID);
            return View(restaurante);
        }

        [HttpPost]
        public IActionResult EditarRestaurante([FromQuery]int restauranteID, Restaurante restaurante)//[FromQuery]int restauranteID: viene de la forma EditarRestaurante?restauranteID=3
        {
            repositorio.EditarRestaurante(restauranteID,restaurante);
            return RedirectToAction("ListarRestaurantes");
        }
    }
}
