using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dominio;
namespace Manager {
    public class FavManager {
        private AccesoDatos datos = new AccesoDatos();
        private List<FavouriteArticle> favourites = new List<FavouriteArticle>();
        public List<FavouriteArticle> FavouriteList() {
            try {
                string consulta = "select Id, IdUser, IdArticulo from FAVORITOS";
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while(datos.Lector.Read()) {
                    favourites.Add(ReadFavouriteFromDB(datos.Lector));
                }
                return favourites;
            } catch(Exception) {
                throw;
            } finally { datos.cerrarConexion(); }
        }
        private FavouriteArticle ReadFavouriteFromDB(SqlDataReader Lector) {
            FavouriteArticle aux = new FavouriteArticle();
            aux.Id=(int)datos.Lector["Id"];
            if(Lector["IdUser"]!=DBNull.Value) { aux.IdUser=int.Parse(Lector["IdUser"].ToString()); }
            if(Lector["IdArticulo"]!=DBNull.Value) { aux.IdArticulo=int.Parse(Lector["IdArticulo"].ToString()); }
            return aux;
        }
        public void Add(FavouriteArticle favourite) {
            try {
                string consulta = "INSERT INTO FAVORITOS (IdUser, IdArticulo) VALUES(@IdUser, @IdArticulo)";
                datos.setearConsulta(consulta);
                datos.agregarParametros("@IdUser", (object)favourite.IdUser??DBNull.Value);
                datos.agregarParametros("@IdArticulo", (object)favourite.IdArticulo??DBNull.Value);
                datos.ejecutarAccion();
            } catch(Exception) {
                throw;
            } finally { datos.cerrarConexion(); }
        }
        public void Delete(FavouriteArticle Favourite) {
            try {
                string consulta = "DELETE FROM FAVORITOS WHERE IdUser=@IdUser AND IdArticulo=@IdArticulo";
                datos.setearConsulta(consulta);
                datos.agregarParametros("@IdUser", (object)Favourite.IdUser??DBNull.Value);
                datos.agregarParametros("@IdArticulo", (object)Favourite.IdArticulo??DBNull.Value);
                datos.ejecutarAccion();
            } catch(Exception) {
                throw;
            } finally { datos.cerrarConexion(); }

        }
        public FavouriteArticle SearchFavouriteById(int Id) {
            try {
                string consulta = "Select * from FAVORITOS where Id=@Id";
                datos.setearConsulta(consulta);
                datos.agregarParametros("Id", Id);
                datos.ejecutarLectura();
                if(datos.Lector.Read()) {
                    FavouriteArticle favourite = new FavouriteArticle();
                    favourite.Id=Id;
                    if(datos.Lector["IdUser"]!=DBNull.Value) { favourite.IdUser=int.Parse(datos.Lector["IdUser"].ToString()); }
                    if(datos.Lector["IdArticulo"]!=DBNull.Value) { favourite.IdArticulo=int.Parse(datos.Lector["IdArticulo"].ToString()); }
                    return favourite;
                }
                return null;
            } catch(Exception) {
                throw;
            }
        }
        public FavouriteArticle SearchFavouriteByIdArticle(int idArticulo) {
            try {
                string consulta = "Select * from FAVORITOS where IdArticulo=@IdArticulo";
                datos.setearConsulta(consulta);
                datos.agregarParametros("@IdArticulo", idArticulo);
                datos.ejecutarLectura();
                if(datos.Lector.Read()) {
                    FavouriteArticle favourite = new FavouriteArticle();
                    favourite.Id=idArticulo;
                    if(datos.Lector["IdUser"]!=DBNull.Value) { favourite.IdUser=int.Parse(datos.Lector["IdUser"].ToString()); }
                    if(datos.Lector["IdArticulo"]!=DBNull.Value) { favourite.IdArticulo=int.Parse(datos.Lector["IdArticulo"].ToString()); }
                    return favourite;
                }
                return null;
            } catch(Exception) {
                throw;
            }
        }
        public bool FavouriteExist(FavouriteArticle fav) {
            try {
                string consulta = "Select * from FAVORITOS WHERE IdUser=@IdUser AND IdArticulo=@IdArticulo";
                datos.setearConsulta(consulta);
                datos.agregarParametros("@IdUser", fav.IdUser);
                datos.agregarParametros("@IdArticulo", fav.IdArticulo);
                datos.ejecutarLectura();
                if(datos.Lector.Read()) {
                    fav.Id=int.Parse(datos.Lector["Id"].ToString());
                    return true;
                }
                return false;
            } catch(Exception) {
                throw;
            }
        }
        public List<FavouriteArticle> FavouriteListByUserId(int idUser) {
            try {
                List<FavouriteArticle> favouritesByUserId = FavouriteList();
                favouritesByUserId=favouritesByUserId.Where(x => x.IdUser==idUser).ToList();
                return favouritesByUserId;
            } catch(Exception) {
                throw;
            }
        }
    }
}

