namespace Dominio {
    public class User {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string ImagenPerfil { get; set; }
        public bool Admin { get; set; }
    }
}
