using iDotolWebApi.Libraries.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;

namespace iDotolWebApi.Libraries
{
    public static class hpt
    {
        public static String Conexion()
        {

            string conexion = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
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

        public static bool GetDbValue2(string sql, string cs)
        {
            SqlConnection conexion = new SqlConnection(cs);
            bool paso = false;
            try
            {
                conexion.Open();

                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.CommandType = System.Data.CommandType.Text;
                int a = comando.ExecuteNonQuery();
                if (a > 0)
                    paso = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
            return paso;
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


        public static string UpdateObjectdb(string tabla, Type Tipo, string WhereClause, params object[] t)
        {
            PropertyInfo[] propiedades = Tipo.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            string campos = $"update {tabla} set ";
            int count = 0;
            foreach (PropertyInfo pi in propiedades.OrderBy(x => x.Name))
            {
                if (pi.Name != "Id")
                {
                    if (t[count] != null)
                        if (!string.IsNullOrEmpty(t[count].ToString()))
                            campos += pi.Name + "= " + t[count].ToString().ToSql() + ",";
                    ++count;
                }
            }
            var resultado = campos.Substring(0, campos.Length - 1);
            resultado += $" where {WhereClause} ";
            return resultado;
        }


        public static bool UploadPictureToFTP(string ItemCode, string PathToFile, string Departamento, ref string NuevaRuta)
        {
            bool retorno = false;

            string Username = hpt.GetDbValue("select top(1) FTPUser from configuraciones", Conexion());
            string Password = hpt.GetDbValue("select top(1) FTPPass from configuraciones", Conexion());

            string Dominio = hpt.GetDbValue("select top(1) DominioFTPImagenes from configuraciones", Conexion());


            string PathCompleto = PathToFile;
            string PathNube = "";

            PathCompleto = PathCompleto.ToLower();
            var FileName = Path.GetFileName(PathCompleto);
            var FileExtension = Path.GetExtension(PathCompleto);
            string RutaNube = "";

            if (FileExtension.ToLower().Equals("jpg") || FileExtension.ToLower().Equals("jpeg") || FileExtension.ToLower().Equals("png"))
                RutaNube = "/imagenes/" + Departamento;
            else if (FileExtension.ToLower().Equals("pdf"))
                RutaNube = "/pdf/" + Departamento;

            string FTPAddress = "ftp://ftp." + Dominio;

            PathNube = RutaNube + "/" + ItemCode + FileExtension;
            NuevaRuta = @"\" + Departamento + @"\" + ItemCode + FileExtension;
            var NewFileName = ItemCode + FileExtension;

            string PathValidacion = "ftp://ftp." + Dominio + PathNube;
            // If CheckIfFtpFileExists("ftp://ftp.domain.com/filename.txt") Then
            // ' Do something
            // End If

            // If Not CheckIfFtpFileExists(PathValidacion, Username, Password) Then




            var FinalPath = "ftp://ftp." + Dominio + PathNube;
            // FTP://server/foldername

            try
            {
                FtpFolderCreate(FinalPath.Replace(NewFileName, ""), Username, Password);
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
            try
            {
                
                if (UploadFTPFiles(FTPAddress, Username, Password, PathCompleto, FinalPath, false, null/* TODO Change to default(_) if this is not a reference type */))
                    retorno = true;
            }


            // Dim client As WebClient = New WebClient
            // client.Credentials = New NetworkCredential(Username, Password)

            // client.UploadFile(FinalPath, PathCompleto)

            catch (Exception ex)
            {
                string a = ex.Message;
                retorno = false;
            }
            // End If


            return retorno;
        }

        private static bool UploadFTPFiles(string ftpAddress, string ftpUser, string ftpPassword, string fileToUpload, string targetFileName, bool deleteAfterUpload, Exception ExceptionInfo)
        {
            NetworkCredential credential;

            try
            {
                credential = new NetworkCredential(ftpUser, ftpPassword);

                if (ftpAddress.EndsWith("/") == false)
                    ftpAddress = ftpAddress + "/";

                string sFtpFile = ftpAddress + fileToUpload;

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(targetFileName);

                request.KeepAlive = false;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = credential;
                request.UsePassive = false;
                request.Timeout = (60 * 1000) * 5; // 3 mins

                using (FileStream reader = new FileStream(fileToUpload, FileMode.Open))
                {
                    byte[] buffer = new byte[Convert.ToInt32(reader.Length - 1) + 1];
                    reader.Read(buffer, 0, buffer.Length);
                    reader.Close();

                    request.ContentLength = buffer.Length;
                    Stream stream = request.GetRequestStream();
                    stream.Write(buffer, 0, buffer.Length);
                    stream.Close();

                    using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                    {
                        if (deleteAfterUpload)
                        {
                        }

                        response.Close();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ExceptionInfo = ex;
                return false;
            }
            finally
            {
            }
        }


        private static bool FtpFolderCreate(string folder_name, string username, string password)
        {
            System.Net.FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(folder_name);

            request.Credentials = new NetworkCredential(username, password);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;

            try
            {
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                }
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                // an error occurred
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    return false;
            }
            return true;
        }


        public static bool CheckIfFtpFileExists(string fileUri, string Username, string Password)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(fileUri);
            request.Credentials = new NetworkCredential(Username, Password);
            request.Method = WebRequestMethods.Ftp.GetFileSize;
            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            }
            // THE FILE EXISTS
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (FtpStatusCode.ActionNotTakenFileUnavailable == response.StatusCode | FtpStatusCode.Undefined == response.StatusCode)
                    // THE FILE DOES NOT EXIST
                    return false;
            }
            return true;
        }
    }
}