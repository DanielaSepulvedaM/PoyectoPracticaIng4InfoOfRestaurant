using System;
using System.ComponentModel.DataAnnotations;

namespace AppComensal.Shared
{
    public class TarjetaDeCredito
    {
        [Required(ErrorMessage = "El numero de la tarjeta es obligatorio")]
        [CreditCard(ErrorMessage = "Debe ser un numero de 16 dígitos")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "El codigo de seguridad es obligatorio")]
        [RegularExpression("^[0-9]{3,4}$", ErrorMessage = "El cvc debe tener entre 3 y 4 digitos")]
        public string CVV { get; set; }

        [Required(ErrorMessage = "El nombre en la tarjeta es obligatorio")]
        [MaxLength(50, ErrorMessage = "El nombre no puede contener mas de 50 digitos")]
        public string Nombre { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "Debe ser un mes valido")]
        public int Mes { get; set; }

        [Required(ErrorMessage = "El año es obligatorio")]
        [Range(21, 41, ErrorMessage = "Debe estar entre los proximos 20 años")]
        public int Año { get; set; }

        [Required(ErrorMessage = "El numero de cuotas es obligatorio")]
        [Range(1,48, ErrorMessage = "Debe ser un valor entre 1 y 48")]
        public int Cuotas { get; set; }
    }
}
