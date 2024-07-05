using System;

namespace Dominio {
    public class Venta {
        public int id { get; set; }
        public int idArticle { get; set; }
        public int idUser { get; set; }
        public DateTime FechaVenta { get; set; }
    }
}

