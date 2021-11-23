using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entidades
{
    public class Mesa
    {
        public int MesaId { get; set; }
        [Required]
        [MaxLength(10)]
        public string Identificador { get; set; }

        public Restaurante Restaurante { get; set; }
    }
}
