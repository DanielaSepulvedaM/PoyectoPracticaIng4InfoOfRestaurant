using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Datos.Models
{
    public partial class FuncionarioRestaurante
    {
        [Key]
        public int FuncionarioRestauranteID { get; set; }
        public int RestauranteID { get; set; }
        [Required]
        [StringLength(450)]
        public string FuncionarioID { get; set; }

        [ForeignKey(nameof(RestauranteID))]
        [InverseProperty("FuncionarioRestaurante")]
        public virtual Restaurante Restaurante { get; set; }
    }
}
