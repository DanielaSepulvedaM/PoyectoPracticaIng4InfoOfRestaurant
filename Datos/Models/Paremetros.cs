using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Datos.Models
{
    public partial class Paremetros
    {
        [Key]
        public int ParametroID { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal? PorcentajeIVA { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal? ImpuestoProductos { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal? ImpuestoConsumo { get; set; }
        public int? RestauranteID { get; set; }

        [ForeignKey(nameof(RestauranteID))]
        [InverseProperty("Paremetros")]
        public virtual Restaurante Restaurante { get; set; }
    }
}
