using iDotolWebApi.Libraries;
using iDotolWebApi.Libraries.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class Clientes
    {
        public Int64 Codigo { get; set; }
        public string Nombre { get; set; }
        public string Rnc { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }


        public static int Guardar(Clientes c)
        {
            string query = hpt.CadenaInsert("Clientes", "Nombre,rnc,direccion,ciudad,telefono,correo",
                $"{c.Nombre.ToSql()},{c.Rnc.ToSql()},{c.Direccion.ToSql()},{c.Ciudad.ToSql()},{c.Telefono.ToSql()},{c.Correo.ToSql()}");

            return hpt.GetDbValue(query, hpt.Conexion()).Toint();
        }


        public static bool Eliminar(int codigo)
        {
            return hpt.GetDbValue2($"delete from Clientes where codigo = {codigo}", hpt.Conexion());
        }

        public static bool Modificar(Clientes codigo)
        {
            var query = hpt.UpdateObjectdb("Clientes", typeof(Clientes), $"codigo = {codigo.Codigo}",
                codigo.Ciudad.ToSql(), null, codigo.Correo.ToSql(), codigo.Direccion.ToSql(), codigo.Nombre.ToSql(), codigo.Rnc.ToSql(), codigo.Telefono.ToSql());
            return hpt.GetDbValue2(query, hpt.Conexion());
        }

        public static Clientes GetCliente(int codigo)
        {
            string QUERY = hpt.QuerySelect("*", "Clientes", $"codigo = {codigo}");
            DataTable dt = new DataTable();
            hpt.llenadt(QUERY, dt, hpt.Conexion());
            Clientes m = new Clientes();

            if (dt.Rows.Count > 0)
            {
                m.Codigo = dt.Rows[0]["codigo"].Toint();
                m.Nombre = dt.Rows[0]["Nombre"].ToString();
                m.Rnc = dt.Rows[0]["Rnc"].ToString();
                m.Ciudad = dt.Rows[0]["Ciudad"].ToString();
                m.Direccion = dt.Rows[0]["Direccion"].ToString();
                m.Telefono = dt.Rows[0]["Telefono"].ToString();
                m.Correo = dt.Rows[0]["Correo"].ToString();

            }

            return m;
        }


        public static List<Clientes> GetCliente()
        {
            string QUERY = hpt.QuerySelect("*", "Clientes", $"1=1");
            DataTable dt = new DataTable();
            hpt.llenadt(QUERY, dt, hpt.Conexion());
            List<Clientes> lista = new List<Clientes>();

            foreach (DataRow row in dt.Rows)
            {
                Clientes m = new Clientes();

                m.Codigo = row["codigo"].Toint64();
                m.Nombre = row["nombre"].ToString();
                m.Rnc = row["rnc"].ToString();
                m.Direccion = row["direccion"].ToString();
                m.Ciudad = row["ciudad"].ToString();
                m.Telefono = row["Telefono"].ToString();
                m.Correo = row["Correo"].ToString();
               
                lista.Add(m);
            }

            return lista;
        }

    }
}