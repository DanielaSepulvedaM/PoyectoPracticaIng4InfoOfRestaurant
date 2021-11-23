using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entidades
{
    public class Consumible
    {
        public int ConsumibleID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(150)]
        public string Descripcion { get; set; }
        [Range(0, 1000000.00)]
        public decimal Precio { get; set; }
        public byte[] Imagen { get; set; }
        public bool TieneFoto { get; set; }
    }
}
