using Entidades.Interfaces;
using Identity.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppAdministracion.Infraestructura.Identity
{
    public class AdditionalUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<Usuario, Rol>
    {
        private readonly IRepositorioFuncionario repositorioFunc;

        public AdditionalUserClaimsPrincipalFactory(UserManager<Usuario> userManager,
            RoleManager<Rol> roleManager,
            IOptions<IdentityOptions> options,
            IRepositorioFuncionario repositorioFunc
            ) : base(userManager, roleManager, options)
        {
            this.repositorioFunc = repositorioFunc;
        }

        public async override Task<ClaimsPrincipal> CreateAsync(Usuario user)
        {
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;

            if (await UserManager.IsInRoleAsync(user, "Administrador"))
                return principal;

            var restauranteID = repositorioFunc.ObtenerRestaurante(user.Id)?.RestauranteID;
            identity.AddClaim(new Claim("RestauranteID", restauranteID?.ToString(), ClaimValueTypes.Integer));
            return principal;
        }
    }
}
