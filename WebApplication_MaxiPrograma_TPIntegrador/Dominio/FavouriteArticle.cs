namespace Dominio {
    public class FavouriteArticle {
        public FavouriteArticle() { }
        public FavouriteArticle(int idUser, int idArticulo) {
            IdUser=idUser;
            IdArticulo=idArticulo;
        }
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdArticulo { get; set; }

    }
}
