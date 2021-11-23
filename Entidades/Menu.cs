using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entidades
{
    public class Menu
    {
        public int MenuID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(150)]
        public string Descripcion { get; set; }
        public Restaurante Restaurante { get; set; }
        public List<Consumible> Consumibles { get; set; }

        public Menu()
        {
            this.Consumibles = new List<Consumible>();
        }
    }
}
