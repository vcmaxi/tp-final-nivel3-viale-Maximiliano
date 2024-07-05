using System;
using System.Data.SqlClient;

namespace Manager {
    public class AccesoDatos {

        SqlConnection conexion;
        SqlCommand comando;
        SqlDataReader lector;

        public SqlDataReader Lector {
            get { return lector; }
        }

        public AccesoDatos() { //conexion a la base de datos
            conexion=new SqlConnection();
            comando=new SqlCommand();
            try {
                conexion.ConnectionString=System.Configuration.ConfigurationManager.AppSettings["cadenaConexion"];
                //conexion.ConnectionString="server=.\\SQLEXPRESS; database=CATALOGO_WEB_DB; integrated security=true;";
            } catch(Exception) {

                throw;
            }
        }
        public void setearConsulta(string consulta) {
            comando.CommandType=System.Data.CommandType.Text;
            comando.CommandText=consulta;
            comando.Parameters.Clear();

        }
        public void ejecutarLectura() {
            comando.Connection=conexion;
            try {
                conexion.Open();
                lector=comando.ExecuteReader();
            } catch(Exception) {
                throw;
            }
        }
        public void ejecutarAccion() {
            comando.Connection=conexion;
            try {
                conexion.Open();
                comando.ExecuteNonQuery();
            } catch(Exception) {
                throw;
            }
        }
        public int ejecutarAccionScalar() {
            comando.Connection=conexion;
            try {
                conexion.Open();
                return int.Parse(comando.ExecuteScalar().ToString());
            } catch(Exception) {
                throw;
            }
        }
        public void cerrarConexion() {
            if(lector!=null) {
                lector.Close();
            }
            conexion.Close();
        }
        public void agregarParametros(string titulo, object value) {
            comando.Parameters.AddWithValue(titulo, value);
        }

    }
}

