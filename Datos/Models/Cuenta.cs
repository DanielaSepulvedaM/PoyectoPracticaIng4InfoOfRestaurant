using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Datos.Models
{
    public partial class Cuenta
    {
        public Cuenta()
        {
            Factura = new HashSet<Factura>();
            Servicio = new HashSet<Servicio>();
        }

        [Key]
        public int CuentaID { get; set; }
        [Column(TypeName = "date")]
        public DateTime FechaCreacion { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal PorcentajePropina { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal? Subtotal { get; set; }
        public int MesaID { get; set; }
        public int RestauranteID { get; set; }
        public bool Cerrada { get; set; }
        [StringLength(10)]
        public string MetodoDePago { get; set; }
        [StringLength(100)]
        public string CodigoDeAprobacion { get; set; }

        [ForeignKey(nameof(MesaID))]
        [InverseProperty("Cuenta")]
        public virtual Mesa Mesa { get; set; }
        [ForeignKey(nameof(RestauranteID))]
        [InverseProperty("Cuenta")]
        public virtual Restaurante Restaurante { get; set; }
        [InverseProperty("Cuenta")]
        public virtual ICollection<Factura> Factura { get; set; }
        [InverseProperty("Cuenta")]
        public virtual ICollection<Servicio> Servicio { get; set; }
    }
}
