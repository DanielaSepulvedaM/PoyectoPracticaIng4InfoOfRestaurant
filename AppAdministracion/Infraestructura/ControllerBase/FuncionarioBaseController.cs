using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAdministracion.Infraestructura.ControllerBase
{
    [Authorize(Roles = "Funcionario")]
    public class FuncionarioBaseController : Controller
    {
        protected int RestauranteID { get {
                if(!HttpContext.User.HasClaim(c => c.Type == "RestauranteID"))
                    return 0;
                return int.Parse(HttpContext.User.FindFirst("RestauranteID").Value);
            } 
        }
    }
}
