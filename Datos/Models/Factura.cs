using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Datos.Models
{
    public partial class Factura
    {
        [Key]
        public int FacturaID { get; set; }
        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }
        [StringLength(60)]
        public string NombreCliente { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal? TotalCuenta { get; set; }
        public int CodAutorizacionPago { get; set; }
        [StringLength(60)]
        public string NombreFuncionario { get; set; }
        public int? CuentaID { get; set; }
        public int? UsuarioID { get; set; }

        [ForeignKey(nameof(CuentaID))]
        [InverseProperty("Factura")]
        public virtual Cuenta Cuenta { get; set; }
    }
}
