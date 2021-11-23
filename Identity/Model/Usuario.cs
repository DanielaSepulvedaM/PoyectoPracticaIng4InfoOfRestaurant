using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Model
{
    public class Usuario : IdentityUser
    {
        public string Documento { get; set; }
    }
}
