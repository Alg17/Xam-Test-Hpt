using RSFWebApi.Libraries;
using RSFWebApi.Libraries.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi.Models
{
    public class Clientes
    {
        public Int64 Codigo { get; set; }
        public string Nombre { get; set; }
        public string Rnc { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Sector { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }


        public static int Guardado(Clientes c)
        {
            var query = hpt.CadenaInsert("Clientes", "Nombre,Rnc,Direccion,Ciudad,Sector,Telefono,Correo",
                $"{c.Nombre},{c.Rnc},{c.Direccion},{c.Ciudad},{c.Sector},{c.Telefono},{c.Correo}");
            return hpt.GetDbValue(query, hpt.Conexion()).Toint();
        }

        public static bool Eliminar(int codigo)
        {
            var query = $"Delete from Usuarios where codigo = {codigo}";
            return hpt.GetDbValue(query, hpt.Conexion()).ToBool();
        }

    }
}
