using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Model
{
    public class AppIdentityDbContext : IdentityDbContext<Usuario, Rol, string>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options): base(options){ }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("identity");
        }
    }
}
