using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Dominio;
using Helper;
using Manager;

namespace WebApplication_MaxiPrograma_TPIntegrador {
    public partial class Favourites: System.Web.UI.Page {
        public List<Articulo> favArticulos = new List<Articulo>();
        protected void Page_Load(object sender, EventArgs e) {
            if(Help.IsUserAdmin(this.Context, GlobalVariables.userString)) {
                Response.Redirect("Default.aspx", false); return;
            }
            if(!IsPostBack) {
                Session[GlobalVariables.isInFavourite]=true; //como muestro los articulos favoritos, en la primer vuelta va a ser true
                if(Help.IsSessionActive(this.Context, GlobalVariables.userString)) {
                    object obj = Help.ActiveUser(this.Context, GlobalVariables.userString);
                    User user = obj as User;
                    if(user!=null) {
                        favArticulos=GetArticleListByUserId(user.Id); //lista de articulos favoritos por id de usuario
                        Session[GlobalVariables.favArticleList]=favArticulos;
                        repeaterFavourites.DataSource=Session[GlobalVariables.favArticleList];
                        repeaterFavourites.DataBind();
                        WebAPPHelper.IsRepeaterEmpty(repeaterFavourites, lblNoResultsFound, this.Context);
                    }
                }
            }
        }
        protected void favouriteIcon_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            try {
                if(Help.ValidateString(((ImageButton)sender).CommandArgument)) {
                    int idArticulo = int.Parse(((ImageButton)sender).CommandArgument);
                    FavouriteArticle fav = new FavManager().SearchFavouriteByIdArticle(idArticulo);
                    if(fav!=null) {
                        new FavManager().Delete(fav);
                        Session[GlobalVariables.favArticleList]=GetArticleListByUserId(fav.IdUser);
                        repeaterFavourites.DataSource=Session[GlobalVariables.favArticleList];
                        repeaterFavourites.DataBind();
                    }

                }

            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());
            }
        }
        private List<Articulo> GetArticleListByUserId(int id) {
            favArticulos.Clear();
            List<FavouriteArticle> favListById = new FavManager().FavouriteListByUserId(id); //lista de favoritos por id de usuario
            foreach(FavouriteArticle fav in favListById) {
                favArticulos.Add(new ArticuloManager().ObtenerArticuloPorId(fav.IdArticulo));
            }
            return favArticulos; //lista de articulos cuyos id se encuentran en la lista de favoritos, por id de usuario

        }
        protected void repeaterFavourites_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e) {
            if(e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem) {
                Button btnAddToCart = (Button)e.Item.FindControl("btnAddToCart");
                int idArticulo = Convert.ToInt32(btnAddToCart.CommandArgument);
                btnAddToCart.Enabled=!WebAPPHelper.IsArticleInCart(this.Context, idArticulo);
            }
            if(e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem) {
                ImageButton btnFavouriteIcon = (ImageButton)e.Item.FindControl("favouriteIcon");
                Help.ValidateImgButton(btnFavouriteIcon, "", GlobalVariables.favouriteBlueIcon);
                //   btnFavouriteIcon.ImageUrl=GlobalVariables.favouriteBlueIcon;
            }

        }
        protected void repeaterFavourites_ItemCommand(object sender, RepeaterCommandEventArgs e) {
            try {
                if(e.CommandName=="AddToCart") {
                    Button btnAddToCart = e.CommandSource as Button;
                    if(btnAddToCart!=null) {
                        string id = btnAddToCart.CommandArgument;
                        if(Help.ValidateString(id)) {
                            int idArticulo = int.Parse(id);
                            if(Help.IsSessionActive(this.Context, GlobalVariables.userString)) {
                                User user = Help.ActiveUser(this.Context, GlobalVariables.userString) as User;
                                new CartManager().AddArticleToCart(idArticulo, user.Id);
                                btnAddToCart.Enabled=false;
                            }
                        }
                    }

                }
            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());
            }

        }
    }
}

