using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Datos.Models
{
    public partial class Comanda
    {
        public Comanda()
        {
            Servicio = new HashSet<Servicio>();
        }

        [Key]
        public int ComandaID { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Fecha { get; set; }
        public int Cantidad { get; set; }
        public int? OrdenID { get; set; }

        [ForeignKey(nameof(OrdenID))]
        [InverseProperty("Comanda")]
        public virtual Orden Orden { get; set; }
        [InverseProperty("ComandaNavigation")]
        public virtual ICollection<Servicio> Servicio { get; set; }
    }
}
