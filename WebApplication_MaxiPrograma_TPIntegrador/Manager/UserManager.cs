using System;
using System.Data.SqlClient;
using Dominio;
namespace Manager {
    public class UserManager {
        public User GetuserById(int id) {
            User user = null;
            AccesoDatos AD = new AccesoDatos();
            try {
                string consulta = "Select * from USERS WHERE Id=@Id";
                user=new User();
                AD.setearConsulta(consulta);
                AD.agregarParametros("@Id", id);
                AD.ejecutarLectura();
                if(AD.Lector.Read()) CompleteuserFromDB(AD.Lector, user);
                return user;
            } catch(Exception) {
                throw;
            } finally { AD.cerrarConexion(); }
        }
        public int InsertUser(User user) {
            AccesoDatos AD = new AccesoDatos();
            try {
                string consulta = "Insert into USERS (email,pass,admin) "+
                    "output inserted.Id "+
                    "values (@email, @pass,0)";
                AD.setearConsulta(consulta);
                AD.agregarParametros("@email", user.Email);
                AD.agregarParametros("@pass", user.Pass);
                return AD.ejecutarAccionScalar();
            } catch(System.Exception ex) {
                throw ex;
            } finally { AD.cerrarConexion(); }
        }
        public bool HasLogin(User user) {
            try {
                AccesoDatos AD = new AccesoDatos();

                AD.setearConsulta("SELECT * FROM USERS WHERE email = @email AND pass = @pass COLLATE Latin1_General_CS_AS");

                AD.agregarParametros("@email", user.Email);
                AD.agregarParametros("@pass", user.Pass);

                AD.ejecutarLectura();

                if(AD.Lector.Read()) {
                    user.Id=(int)AD.Lector["Id"]; //no es null
                    if(AD.Lector["nombre"]!=DBNull.Value) { user.Nombre=AD.Lector["nombre"].ToString(); }
                    if(AD.Lector["apellido"]!=DBNull.Value) { user.Apellido=AD.Lector["apellido"].ToString(); }
                    if(AD.Lector["urlImagenPerfil"]!=DBNull.Value) { user.ImagenPerfil=AD.Lector["urlImagenPerfil"].ToString(); }
                    user.Admin=string.IsNullOrEmpty(AD.Lector["admin"].ToString()) ? false : (bool)AD.Lector["admin"];
                    return true;
                }
                return false;

            } catch(System.Exception) {

                throw;
            }

        }
        public void Updateuser(User user) {
            AccesoDatos AD = new AccesoDatos();
            try {
                string consulta = "UPDATE USERS SET nombre=@nombre, apellido=@apellido, urlImagenPerfil=@imagenPerfil "+
                    "WHERE Id=@Id";
                AD.setearConsulta(consulta);
                AD.agregarParametros("@Id", user.Id);
                AD.agregarParametros("@nombre", (object)user.Nombre??DBNull.Value);
                AD.agregarParametros("@apellido", (object)user.Apellido??DBNull.Value);
                AD.agregarParametros("@imagenPerfil", (object)user.ImagenPerfil??DBNull.Value);
                AD.ejecutarAccion();

            } catch(Exception) {

                throw;
            } finally { AD.cerrarConexion(); }
        }
        private void CompleteuserFromDB(SqlDataReader Lector, User user) {
            if(!(Lector["Id"] is DBNull)) { user.Id=int.Parse(Lector["Id"].ToString()); }
            if(!(Lector["apellido"] is DBNull)) { user.Apellido=Lector["apellido"].ToString(); }
            if(!(Lector["nombre"] is DBNull)) { user.Nombre=Lector["nombre"].ToString(); }
            if(!(Lector["email"] is DBNull)) { user.Email=Lector["email"].ToString(); }
            if(!(Lector["pass"] is DBNull)) { user.Pass=Lector["pass"].ToString(); }
            if(!(Lector["admin"] is DBNull)) { user.Admin=bool.Parse(Lector["admin"].ToString()); }
            if(!(Lector["UrlImagenPerfil"] is DBNull)) { user.ImagenPerfil=Lector["UrlImagenPerfil"].ToString(); }

        }
        public bool UserExistInDb(string email) {
            AccesoDatos AD = new AccesoDatos();
            try {
                string consulta = "select * from USERS WHERE email=@email";
                AD.setearConsulta(consulta);
                AD.agregarParametros("@email", email);
                AD.ejecutarLectura();
                if(AD.Lector.Read()) {
                    return true;
                }
                return false;
            } catch(Exception) {
                throw;
            }
        }
    }

}

