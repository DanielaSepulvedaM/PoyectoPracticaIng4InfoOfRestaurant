using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Datos.Models
{
    public partial class Orden
    {
        public Orden()
        {
            Servicio = new HashSet<Servicio>();
        }

        [Key]
        public int OrdenID { get; set; }
        [Column(TypeName = "date")]
        public DateTime FechaCreacion { get; set; }
        public int CuentaID { get; set; }

        [ForeignKey(nameof(CuentaID))]
        [InverseProperty("Orden")]
        public virtual Cuenta Cuenta { get; set; }
        [InverseProperty("Orden")]
        public virtual ICollection<Servicio> Servicio { get; set; }
    }
}
