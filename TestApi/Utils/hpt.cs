using RSFWebApi.Libraries.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace RSFWebApi.Libraries
{
    public static class hpt
    {
        public static String Conexion()
        {

            string conexion = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            return conexion;
        }
        public static String ConexionTsim()
        {
            string conexion = ConfigurationManager.ConnectionStrings["ConexionT_sim"].ConnectionString;
            return conexion;
        }

        public static String ConexionAccesos()
        {
            string conexion = ConfigurationManager.ConnectionStrings["ConexionAcceso"].ConnectionString;
            return conexion;
        }
        public static Boolean UQuery(string sql, string cs)
        {
            SqlConnection cn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cn.Open();
                cmd = new SqlCommand(sql, cn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
            finally
            {
                cn.Close();
                cn.Dispose();
                cmd.Dispose();
            }
        }
        public static Boolean loopUQuery(string sql, SqlConnection cn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public static void llenadt(string sql, DataTable dtt, string cs)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection(cs);
            try
            {
                cn.Open();
                cmd = new SqlCommand(sql, cn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtt);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cn.Close();
                cn.Dispose();
                cmd.Dispose();
            }
        }
        public static void loopllenadt(string sql, DataTable dtt, SqlConnection cn)
        {

            try
            {
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtt);
                cmd.Dispose();
            }
            catch (Exception)
            {
                cn.Close();
                cn.Dispose();
                throw;
            }
        }
        public static string GetDbValue(string sql, string cs)
        {
            SqlCommand cmd = new SqlCommand();
            string retorno = "";
            SqlConnection cn = new SqlConnection(cs);
            try
            {
                cn.Open();
                cmd = new SqlCommand(sql, cn);
                cmd.CommandType = CommandType.Text;
                retorno = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                retorno = "0";
            }
            finally
            {
                cn.Close();
                cn.Dispose();
                cmd.Dispose();
            }

            if (retorno == null)
            {
                retorno = "0";
            }
            return retorno;

        }
        public static string loopGetDbValue(string sql, SqlConnection cn)
        {

            string retorno = "";

            try
            {
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandType = CommandType.Text;
                retorno = cmd.ExecuteScalar().ToString();
            }
            catch (Exception)
            {
                retorno = "0";
            }

            if (retorno == null)
            {
                retorno = "";
            }
            return retorno;

        }
        public static int ExtraerNumeros(string str)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] >= '0' && str[i] <= '9')
                {
                    sb.Append(str[i]);
                }
            }

            return Convert.ToInt32(sb.ToString());
        }
        public static string ExtraerTextos(string str)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] >= 'A' && str[i] <= 'z' || str[i] == '0')
                {
                    sb.Append(str[i]);
                }
            }

            return sb.ToString();
        }
        public static bool isDate(string inputDate)
        {
            bool isDate = true;
            try
            {
                DateTime dt = DateTime.Parse(inputDate);
            }
            catch (Exception)
            {
                isDate = false;
            }
            return isDate;
        }
        public static bool IsNumeric(this string valor)
        {
            float output;
            return float.TryParse(valor, out output);
        }
        public static string CadenaInsert(string tabla, string campos, string valores)
        {
            return $"INSERT INTO {tabla} ({campos}) VALUES ({valores}); Select IsNull(SCOPE_IDENTITY(), 0)";
        }
        public static string QuerySelect(string campos, string tabla, string condicion, string order)
        {
            return $"SELECT {campos}  FROM {tabla} WHERE {condicion} Order by {order}";
        }

        public static string QuerySelect(string campos, string tabla, string condicion)
        {
            return $"SELECT {campos}  FROM {tabla} WHERE {condicion}";
        }

        public static float FuncionITBIS(string Codigo, string Tabla = "Productos")
        {
            DataTable dt = new DataTable();
            float Itbis = 0;
            hpt.llenadt("SELECT p.Itbis, i.itbis as Porciento FROM " + Tabla + " p INNER JOIN ClasificacionITBIS i ON p.TipoItbis=i.Codigo  WHERE p.Codigo='" + Codigo + "'", dt, Conexion());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Itbis"].ToBool() == true)
                    Itbis = dt.Rows[0]["Porciento"].Tofloat();
            }
            return Itbis;
        }


    }
}