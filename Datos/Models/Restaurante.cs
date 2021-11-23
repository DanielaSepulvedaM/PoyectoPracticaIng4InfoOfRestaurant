using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Datos.Models
{
    public partial class Restaurante
    {
        public Restaurante()
        {
            Consumible = new HashSet<Consumible>();
            Cuenta = new HashSet<Cuenta>();
            FuncionarioRestaurante = new HashSet<FuncionarioRestaurante>();
            Menu = new HashSet<Menu>();
            Mesa = new HashSet<Mesa>();
            Paremetros = new HashSet<Paremetros>();
        }

        [Key]
        public int RestauranteID { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [StringLength(15)]
        public string Nit { get; set; }
        [StringLength(10)]
        public string Telefono { get; set; }
        [StringLength(100)]
        public string Direccion { get; set; }
        [StringLength(50)]
        public string Barrio { get; set; }
        public bool? Eliminado { get; set; }

        [InverseProperty("Restaurante")]
        public virtual ICollection<Consumible> Consumible { get; set; }
        [InverseProperty("Restaurante")]
        public virtual ICollection<Cuenta> Cuenta { get; set; }
        [InverseProperty("Restaurante")]
        public virtual ICollection<FuncionarioRestaurante> FuncionarioRestaurante { get; set; }
        [InverseProperty("Restaurante")]
        public virtual ICollection<Menu> Menu { get; set; }
        [InverseProperty("Restaurante")]
        public virtual ICollection<Mesa> Mesa { get; set; }
        [InverseProperty("Restaurante")]
        public virtual ICollection<Paremetros> Paremetros { get; set; }
    }
}
