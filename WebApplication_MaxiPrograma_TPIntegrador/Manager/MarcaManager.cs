using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dominio;

namespace Manager {
    public class MarcaManager {

        private AccesoDatos datos = new AccesoDatos();
        private List<Marca> marcas = new List<Marca>();
        public List<Marca> ListarMarcas() {
            try {
                string consulta = "select Id, Descripcion from MARCAS";
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while(datos.Lector.Read()) {
                    marcas.Add(ReadMarcaFromDB(datos.Lector));
                }
                return marcas;
            } catch(Exception) {
                throw;
            } finally { datos.cerrarConexion(); }
        }
        private Marca ReadMarcaFromDB(SqlDataReader Lector) {
            Marca aux = new Marca();
            aux.Id=(int)datos.Lector["Id"];
            if(Lector["Descripcion"]!=DBNull.Value) { aux.Descripcion=Lector["Descripcion"].ToString(); }
            return aux;
        }
        public void Agregar(string dato) {
            try {
                string consulta = "INSERT INTO MARCAS (Descripcion) VALUES (@desc)";
                datos.setearConsulta(consulta);
                datos.agregarParametros("@desc", (object)dato??DBNull.Value);
                datos.ejecutarAccion();
            } catch(Exception) {
                throw;
            } finally { datos.cerrarConexion(); }
        }
        public void Modificar(Marca marca) {
            try {
                string consulta = "UPDATE MARCAS set Descripcion=@Descripcion WHERE Id=@Id";
                datos.setearConsulta(consulta);
                datos.agregarParametros("@Descripcion", (object)marca.Descripcion??DBNull.Value);
                datos.agregarParametros("@Id", (object)marca.Id??DBNull.Value);
                datos.ejecutarAccion();
            } catch(Exception) {
                throw;
            } finally { datos.cerrarConexion(); }
        }
        public void Eliminar(Marca marca) {
            try {
                string consulta = "DELETE FROM MARCAS WHERE Id=@Id";
                datos.setearConsulta(consulta);
                datos.agregarParametros("@Id", (object)marca.Id??DBNull.Value);
                datos.ejecutarAccion();
            } catch(Exception) {
                throw;
            } finally { datos.cerrarConexion(); }

        }
        public bool BrandExist(string text, out int anyValue) {
            try {
                anyValue=0;
                string consulta = "Select * FROM MARCAS WHERE Descripcion=@Desc";
                datos.setearConsulta(consulta);
                datos.agregarParametros("@Desc", text);
                datos.ejecutarLectura();
                if(datos.Lector.Read()) {
                    if(datos.Lector["Id"]!=DBNull.Value) { anyValue=int.Parse(datos.Lector["Id"].ToString()); }
                    return true;
                }
                return false;
            } catch(Exception) {

                throw;
            }
        }
        public Marca GetBrandById(int idMarca) {
            Marca marca = null;
            try {
                string consulta = "SELECT * FROM MARCAS WHERE Id=@Id";
                datos.setearConsulta(consulta);
                datos.agregarParametros("@Id", idMarca);
                datos.ejecutarLectura();
                if(datos.Lector.Read()) {
                    marca=ReadMarcaFromDB(datos.Lector);
                    return marca;
                }
                return marca;
            } catch(Exception) {
                throw;
            }

        }
    }
}

