using AppComensal.Server.Infraestructura.Pasarela;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppComensal.Server.Infraestructura.Pasarela4
{
    public class PasarelaDePrueba : IPasarela
    {
        private static Random random = new Random();
        const string chars = "0123456789";

        HashSet<TC> TarjetasRegistradas = new HashSet<TC> { 
            new TC("Jason Blanchard", "4543960071881291557", "05/29", "889"),
            new TC("Nicholas Palmer", "3505141843024395", "07/28", "048"),
            new TC("Tracy Rodriguez", "213139681404884", "10/24", "037"),
            new TC("Scott Mullins", "30040596642215", "07/22", "606"),
            new TC("Lisa Arnold", "4079387603986885", "12/30", "036")
        };


        public async Task<RespuestaPasarela> ProcesarPago(MensajePasarela mensajePasarela)
        {
            var tc = new TC(mensajePasarela.Nombre, mensajePasarela.Numero, mensajePasarela.CVV, mensajePasarela.Mes, mensajePasarela.Año);
            if (!TarjetasRegistradas.Contains(tc))
            {
                await Task.Delay(1000 * 1);
                return new RespuestaPasarela { Mensaje = "Tarjeta no válida", CodigoAprobacion = "00000000", Aprobado = false };
            }
                
            await Task.Delay(1000 * 3);

            return new RespuestaPasarela { Aprobado = true, CodigoAprobacion = GenerarCodigoAprobacion(8) };
        }
        
        public static string GenerarCodigoAprobacion(int length)
        {
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

    record TC {
        public string Nombre { get; init; }
        public string Numero { get; init; }
        public string CVV { get; init; }
        public int Mes { get; init; }
        public int Año { get; init; }

        public TC(string nombre, string numero, string due, string cVV)
        {
            Nombre = nombre;
            Numero = numero;
            CVV = cVV;

            var p = due.Split('/');
            Mes = int.Parse(p[0]);
            Año = int.Parse(p[1]);
        }

        public TC(string nombre, string numero, string cVV, int mes, int año)
        {
            Nombre = nombre;
            Numero = numero;
            CVV = cVV;
            Mes = mes;
            Año = año;
        }
    }
}
