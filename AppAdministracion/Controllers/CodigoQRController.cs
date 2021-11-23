using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAdministracion.Controllers
{
    public class CodigoQRController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
