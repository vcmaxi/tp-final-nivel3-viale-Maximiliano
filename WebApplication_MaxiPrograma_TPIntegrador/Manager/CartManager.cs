using System;
using System.Collections.Generic;
using Dominio;
namespace Manager {
    public class CartManager {
        AccesoDatos datos = new AccesoDatos();
        string consulta;

        public List<Cart> cartListByUserId(int idUser) {
            try {
                List<Cart> list = new List<Cart>();
                consulta="Select * from CARRITO WHERE IdUser=@idUser ";
                datos.setearConsulta(consulta);
                datos.agregarParametros("@idUser", idUser);
                datos.ejecutarLectura();
                while(datos.Lector.Read()) {
                    Cart singleArticle = new Cart();
                    if(datos.Lector["Id"]!=DBNull.Value) { singleArticle.Id=int.Parse(datos.Lector["Id"].ToString()); }
                    if(datos.Lector["IdUser"]!=DBNull.Value) { singleArticle.IdUser=int.Parse(datos.Lector["IdUser"].ToString()); }
                    if(datos.Lector["IdArticulo"]!=DBNull.Value) { singleArticle.IdArticulo=int.Parse(datos.Lector["IdArticulo"].ToString()); }
                    list.Add(singleArticle);
                }
                return list;
            } catch(System.Exception) {
                throw;
            }
        }
        public void AddArticleToCart(int idArticulo, int idUser) {
            try {
                consulta="insert into CARRITO (idUser , idArticulo) values (@IdUser, @IdArticulo)";
                datos.setearConsulta(consulta);
                datos.agregarParametros("@IdUser", idUser);
                datos.agregarParametros("@IdArticulo", idArticulo);
                datos.ejecutarAccion();
            } catch(Exception) {
                throw;
            } finally { datos.cerrarConexion(); }
        }
        public void DeleteFromCart(Cart singleArticle) {
            try {
                consulta="DELETE FROM CARRITO WHERE Id=@Id";
                datos.setearConsulta(consulta);
                datos.agregarParametros("@Id", (object)singleArticle.Id??DBNull.Value);
                datos.ejecutarAccion();
            } catch(Exception) {
                throw;
            } finally { datos.cerrarConexion(); }
        }
        public bool IsArticleInCart(int idArticle, int idUser) {
            try {
                consulta="SELECT * FROM CARRITO WHERE IdUser = @IdUser AND IdArticulo = @IdArticulo";
                datos.setearConsulta(consulta);
                datos.agregarParametros("@IdUser", idUser);
                datos.agregarParametros("@IdArticulo", idArticle);
                datos.ejecutarLectura();
                if(datos.Lector.Read()) { return true; }
                return false;
            } catch(Exception) {
                throw;
            }

        }
        public Cart GetArticleCart(int idArticle, int idUser) {
            try {
                Cart cart = new Cart();
                consulta="SELECT * FROM CARRITO WHERE IdUser = @IdUser AND IdArticulo = @IdArticulo";
                datos.setearConsulta(consulta);
                datos.agregarParametros("@IdUser", idUser);
                datos.agregarParametros("@IdArticulo", idArticle);
                datos.ejecutarLectura();
                if(datos.Lector.Read()) {
                    if(datos.Lector["Id"]!=DBNull.Value) { cart.Id=int.Parse(datos.Lector["Id"].ToString()); }
                    if(datos.Lector["IdArticulo"]!=DBNull.Value) { cart.IdArticulo=int.Parse(datos.Lector["IdArticulo"].ToString()); }
                    if(datos.Lector["IdUser"]!=DBNull.Value) { cart.IdUser=int.Parse(datos.Lector["IdUser"].ToString()); }
                }
                return cart;
            } catch(Exception) {
                throw;
            }
        }
    }
}
