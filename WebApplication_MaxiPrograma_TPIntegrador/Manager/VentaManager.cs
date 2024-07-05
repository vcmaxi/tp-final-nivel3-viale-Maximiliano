using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dominio;
namespace Manager {
    public class VentaManager {
        public AccesoDatos datos = new AccesoDatos();

        string consulta = null;

        public List<Venta> GetVentas() {
            try {
                List<Venta> ventas = new List<Venta>();
                consulta="Select * from VENTAS";
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while(datos.Lector.Read()) {
                    ventas.Add(ReadVentaFromDB(datos.Lector));
                }
                return ventas;
            } catch(System.Exception) {
                throw;
            }
        }
        private Venta ReadVentaFromDB(SqlDataReader Lector) {
            Venta venta = new Venta();
            if(Lector["Id"]!=DBNull.Value) { venta.id=int.Parse(Lector["Id"].ToString()); }
            if(Lector["IdArticulo"]!=DBNull.Value) { venta.idArticle=int.Parse(Lector["IdArticulo"].ToString()); }
            if(Lector["IdUser"]!=DBNull.Value) { venta.idUser=int.Parse(Lector["IdUser"].ToString()); }
            if(Lector["FechaVenta"]!=DBNull.Value) { venta.FechaVenta=(DateTime)Lector["FechaVenta"]; }
            return venta;
        }
        private void SetParameters(Venta venta) {
            try {
                datos.agregarParametros("@IdArticulo", venta.idArticle);
                datos.agregarParametros("@IdUser", venta.idUser);
                datos.agregarParametros("@FechaVenta", venta.FechaVenta);
            } catch(Exception) {
                throw;
            }

        }
        public void AddVenta(Venta venta) {
            try {
                consulta="Insert into VENTAS values (@IdArticulo, @IdUser, @FechaVenta)";
                datos.setearConsulta(consulta);
                SetParameters(venta);
                datos.ejecutarAccion();
            } catch(Exception) {
                throw;
            }
        }
        public List<Venta> GetVentasByUserId(int idUser) {
            try {
                List<Venta> ventasByUserId = new List<Venta>();
                consulta="Select * from VENTAS WHERE IdUser=@IdUser";
                datos.setearConsulta(consulta);
                datos.agregarParametros("@IdUser", idUser);
                datos.ejecutarLectura();
                while(datos.Lector.Read()) {
                    ventasByUserId.Add(ReadVentaFromDB(datos.Lector));
                }
                return ventasByUserId;
            } catch(System.Exception) {
                throw;
            }

        }

    }
}
