using WebApplication_MaxiPrograma_TPIntegrador;

namespace Dominio {
    public class Marca: IDescription {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public override string ToString() {
            return Descripcion;
        }
    }
}
