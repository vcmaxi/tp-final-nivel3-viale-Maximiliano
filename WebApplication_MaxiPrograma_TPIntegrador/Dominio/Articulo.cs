using System.ComponentModel;

namespace Dominio {
    public class Articulo {

        public Articulo() {
            Marca=new Marca();
            Categoria=new Categoria();
        }
        public int Id { get; set; }
        [DisplayName("Código")]
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        [DisplayName("Marca")]
        public Marca Marca { get; set; }
        [DisplayName("Categoria")]
        public Categoria Categoria { get; set; }
        [DisplayName("url")]
        public string imagenUrl { get; set; }
        public decimal precio { get; set; }

    }
}

