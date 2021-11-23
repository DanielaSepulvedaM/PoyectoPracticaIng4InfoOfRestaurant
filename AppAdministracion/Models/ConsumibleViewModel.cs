using Entidades;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace AppAdministracion.Models
{
    public class ConsumibleViewModel : Consumible, IValidatableObject
    {
        public IFormFile Foto { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var photo = ((ConsumibleViewModel)validationContext.ObjectInstance).Foto;
            if (photo != null)
            {
                var extension = Path.GetExtension(photo.FileName);
                var size = photo.Length;

                if (!extension.ToLower().Equals(".jpg"))
                    yield return new ValidationResult("Extension de archivo no valida.");

                if (size > (2 * 1024 * 1024))
                    yield return new ValidationResult("El tamaño del archivo es mayor a 2MB.");
            }
        }
    }
}
