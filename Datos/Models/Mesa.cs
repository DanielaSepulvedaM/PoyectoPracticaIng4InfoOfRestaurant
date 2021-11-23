using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Datos.Models
{
    public partial class Mesa
    {
        public Mesa()
        {
            Cuenta = new HashSet<Cuenta>();
        }

        [Key]
        public int MesaID { get; set; }
        [Required]
        [StringLength(10)]
        public string Identificador { get; set; }
        public int RestauranteID { get; set; }

        [ForeignKey(nameof(RestauranteID))]
        [InverseProperty("Mesa")]
        public virtual Restaurante Restaurante { get; set; }
        [InverseProperty("Mesa")]
        public virtual ICollection<Cuenta> Cuenta { get; set; }
    }
}
