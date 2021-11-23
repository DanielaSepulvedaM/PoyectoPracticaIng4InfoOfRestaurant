using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Datos.Models
{
    public partial class ConsumibleMenu
    {
        [Key]
        public int ConsumibleMenuID { get; set; }
        public int ConsumibleID { get; set; }
        public int MenuID { get; set; }

        [ForeignKey(nameof(ConsumibleID))]
        [InverseProperty("ConsumibleMenu")]
        public virtual Consumible Consumible { get; set; }
        [ForeignKey(nameof(MenuID))]
        [InverseProperty("ConsumibleMenu")]
        public virtual Menu Menu { get; set; }
    }
}
