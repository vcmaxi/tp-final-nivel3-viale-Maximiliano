using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dominio;

namespace Manager {
    public class CategoriaManager {
        private AccesoDatos datos = new AccesoDatos();
        private List<Categoria> categorias = new List<Categoria>();
        public List<Categoria> ListarCategorias() {
            try {
                string consulta = "select Id, Descripcion from CATEGORIAS";
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while(datos.Lector.Read()) {
                    ReadCategoryFromDB(datos.Lector);
                    categorias.Add(ReadCategoryFromDB(datos.Lector));
                }
                return categorias;

            } catch(Exception) {
                throw;
            } finally { datos.cerrarConexion(); }

        }
        private Categoria ReadCategoryFromDB(SqlDataReader Lector) {
            Categoria aux = new Categoria();
            aux.Id=(int)datos.Lector["Id"];
            if(Lector["Descripcion"]!=DBNull.Value) { aux.Descripcion=Lector["Descripcion"].ToString(); }
            return aux;
        }
        public void Agregar(string dato) {
            try {
                string consulta = "INSERT INTO CATEGORIAS (Descripcion) VALUES (@desc)";
                datos.setearConsulta(consulta);
                datos.agregarParametros("@desc", (object)dato??DBNull.Value);
                datos.ejecutarAccion();
            } catch(Exception) {
                throw;
            } finally { datos.cerrarConexion(); }
        }
        public void Modificar(Categoria categoria) {
            try {
                string consulta = "UPDATE CATEGORIAS set Descripcion=@Descripcion WHERE Id=@Id";
                datos.setearConsulta(consulta);
                datos.agregarParametros("@Descripcion", (object)categoria.Descripcion??DBNull.Value);
                datos.agregarParametros("@Id", (object)categoria.Id??DBNull.Value);
                datos.ejecutarAccion();
            } catch(Exception) {
                throw;
            } finally { datos.cerrarConexion(); }
        }
        public void Eliminar(Categoria categoria) {
            try {
                string consulta = "DELETE FROM CATEGORIAS WHERE Id=@Id";
                datos.setearConsulta(consulta);
                datos.agregarParametros("@Id", (object)categoria.Id??DBNull.Value);
                datos.ejecutarAccion();
            } catch(Exception) {
                throw;
            } finally { datos.cerrarConexion(); }
        }
        public bool CategoryExist(string text, out int anyValue) {
            anyValue=0;
            try {
                string consulta = "Select * FROM CATEGORIAS WHERE Descripcion=@Desc";
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
        public Categoria GetCategoryById(int idCat) {
            Categoria cat = null;
            try {
                string consulta = "SELECT * FROM CATEGORIAS WHERE Id=@Id";
                datos.setearConsulta(consulta);
                datos.agregarParametros("@Id", idCat);
                datos.ejecutarLectura();
                if(datos.Lector.Read()) {
                    cat=ReadCategoryFromDB(datos.Lector);
                    return cat;
                }
                return cat;
            } catch(Exception) {
                throw;
            }
        }
    }
}
