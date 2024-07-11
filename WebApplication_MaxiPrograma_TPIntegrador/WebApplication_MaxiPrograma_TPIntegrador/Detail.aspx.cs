using System;
using Dominio;
using Helper;
using Manager;

namespace WebApplication_MaxiPrograma_TPIntegrador {
    public partial class Detail: System.Web.UI.Page {
        public Articulo articulo = null;
        protected void Page_Load(object sender, EventArgs e) {

            if(WebAPPHelper.IsArticleInCart(this.Context)) {
                btnAddToCart.Enabled=false;
            }
            if(!Help.IsSessionActive(Context, GlobalVariables.userString)) {
                btnAddToCart.Enabled=false;
            }

            if(!IsPostBack) {
                string id = Request.QueryString["id"];
                if(Help.ValidateString(id)) {
                    articulo=new ArticuloManager().ObtenerArticuloPorId(int.Parse(id));
                    if(articulo!=null) {
                        Help.ValidateImg(imageArticle, articulo.imagenUrl, GlobalVariables.errorImgPath);
                        User user = Help.ActiveUser(Context, GlobalVariables.userString) as User;
                        if(user!=null) {
                            FavouriteArticle fav = new FavouriteArticle(user.Id, articulo.Id);
                            if(new FavManager().FavouriteExist(fav)) {
                                Help.ValidateImg(favouriteIcon, "", GlobalVariables.favouriteBlueIcon);
                                Session[GlobalVariables.isInFavourite]=true;
                            } else {
                                Help.ValidateImg(favouriteIcon, "", GlobalVariables.favouriteIcon);
                                Session[GlobalVariables.isInFavourite]=false;
                            }
                        }
                    } else {
                        Help.RedirectToErrorPage(this.Context, "The item you are looking for, does not exist."); return;
                    }
                } else {
                    Help.RedirectToErrorPage(this.Context, "The item you are looking for, does not exist."); return;
                }

            }
        }
        protected void favouriteIcon_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            try {
                string id = Request.QueryString["id"];
                if(Help.ValidateString(id)) {
                    articulo=new ArticuloManager().ObtenerArticuloPorId(int.Parse(id));
                    if(articulo!=null) {
                        User user = Help.ActiveUser(Context, GlobalVariables.userString) as User;
                        if(user!=null) {
                            FavouriteArticle fav = new FavouriteArticle(user.Id, articulo.Id);
                            if(favouriteIcon.ImageUrl==GlobalVariables.favouriteBlueIcon) {
                                new FavManager().Delete(fav);
                                favouriteIcon.ImageUrl=GlobalVariables.favouriteIcon;
                                Help.ValidateImg(favouriteIcon, "", GlobalVariables.favouriteIcon);
                            } else {
                                new FavManager().Add(fav);
                                favouriteIcon.ImageUrl=GlobalVariables.favouriteBlueIcon;
                                Help.ValidateImg(favouriteIcon, "", GlobalVariables.favouriteBlueIcon);
                            }
                        }
                    }
                }
            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());
            }
        }
        protected void btnAddToCart_Click(object sender, EventArgs e) {
            try {
                string id = Request.QueryString["id"]; //id del articulo
                if((Help.ValidateString(id))) {
                    articulo=new ArticuloManager().ObtenerArticuloPorId(int.Parse(id));
                    if(Help.IsSessionActive(this.Context, GlobalVariables.userString)) {
                        User user = Help.ActiveUser(this.Context, GlobalVariables.userString) as User;
                        new CartManager().AddArticleToCart(int.Parse(id), user.Id);
                        btnAddToCart.Enabled=false;
                    }
                }
            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());
            }
        }
    }
}