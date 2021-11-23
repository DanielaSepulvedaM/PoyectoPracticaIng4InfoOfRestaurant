using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppComensal.Server.Infraestructura.Pasarela
{
    public class MensajePasarela
    {
        public int CuentaID { get; }
        public decimal Total { get; }
        public string Nombre { get; }
        public string Numero { get; }
        public string CVV { get; }
        public int Mes { get; }
        public int Año { get; }

        public MensajePasarela(int cuentaID, decimal total, string nombre, string numero, string cVV, int mes, int año)
        {
            CuentaID = cuentaID;
            Total = total;
            Nombre = nombre;
            Numero = numero;
            CVV = cVV;
            Mes = mes;
            Año = año;
        }
    }
}
