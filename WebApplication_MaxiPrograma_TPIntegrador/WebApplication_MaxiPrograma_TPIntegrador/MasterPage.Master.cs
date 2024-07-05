using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Dominio;
using Helper;
using Manager;

namespace WebApplication_MaxiPrograma_TPIntegrador {
    public partial class MasterPage: System.Web.UI.MasterPage {

        protected void Page_init() {

            //User user = new UserManager().GetuserById(1);
            //Session[GlobalVariables.userString]=user;
        }
        protected void Page_Load(object sender, EventArgs e) {
            if(Page is ArticleForm||Page is Favourites||Page is Profile||Page is ShoppingCart||Page is Sales) {
                if(!(Help.IsSessionActive(this.Context, GlobalVariables.userString))) {
                    Response.Redirect("Login.aspx", false);
                }
            }
            if(Help.IsSessionActive(this.Context, GlobalVariables.userString)) {
                User activeUser = (User)Session[GlobalVariables.userString];
                if(!(string.IsNullOrEmpty(activeUser.ImagenPerfil))) {
                    Help.ValidateImg(imgProfile, activeUser.ImagenPerfil, GlobalVariables.profileImage);
                } else {
                    imgProfile.ImageUrl=GlobalVariables.profileImage;
                }
                if(Help.IsUserAdmin(this.Context, GlobalVariables.userString)) {
                    btnShopping.Text="Sales";
                } else { btnShopping.Text="My Shopping"; }
            } else {
                imgProfile.ImageUrl=GlobalVariables.profileImage;
            }
            if(!IsPostBack) {
                repeaterCategory.DataSource=new CategoriaManager().ListarCategorias();
                repeaterCategory.DataBind();
                repeaterBrand.DataSource=new MarcaManager().ListarMarcas();
                repeaterBrand.DataBind();
                shoppingCart.Text="\uD83D\uDED2";
            }

        }
        protected void btnLogin_Click(object sender, EventArgs e) {
            Session[GlobalVariables.isFromLogin]=true;
            Response.Redirect("Login.aspx", false);
        }
        protected void btnRegister_Click(object sender, EventArgs e) {
            Session[GlobalVariables.isFromLogin]=false;
            Response.Redirect("Login.aspx", false);
        }
        protected void btnSearch_Click(object sender, EventArgs e) {
            try {
                string searchTerm = txtSearch.Text.ToUpper();
                List<Articulo> articleListFilter = new ArticuloManager().ListarArticulos();
                if(!(string.IsNullOrEmpty(searchTerm))) {
                    articleListFilter=articleListFilter.Where(x => x.Nombre.ToString().ToUpper().Contains(searchTerm)||
                                    x.Descripcion.ToString().ToUpper().Contains(searchTerm)||
                                    x.Marca.ToString().ToUpper().Contains(searchTerm)||
                                    x.Categoria.ToString().ToUpper().Contains(searchTerm)||
                                    x.Codigo.ToString().ToUpper().Contains(searchTerm))
                        .Distinct().ToList();
                }
                Session[GlobalVariables.articleList]=articleListFilter;
                Session[GlobalVariables.isFilterList]=true;
                Response.Redirect("ArticleList.aspx", false); return;
            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());
            }
        }
        protected void btnLogout_Click(object sender, EventArgs e) {
            if((Help.IsSessionActive(this.Context, GlobalVariables.userString))) {
                Session.Remove(GlobalVariables.userString);
                Session[GlobalVariables.isFromLogin]=true;
                Response.Redirect("Login.aspx", false);
            }
        }
        protected void btnCat_Click(object sender, EventArgs e) {
            try {
                string descp = ((Button)sender).CommandArgument;
                List<Articulo> listFilterByCategory = new ArticuloManager().ListarArticulos();
                listFilterByCategory=listFilterByCategory.Where(x => x.Categoria.Descripcion.ToUpper()==descp.ToUpper()).ToList();
                Session[GlobalVariables.articleList]=listFilterByCategory;
                Session[GlobalVariables.isFilterList]=true;
                Response.Redirect("ArticleList.aspx", false);
            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());
            }
        }
        protected void btnBrand_Click(object sender, EventArgs e) {
            try {
                string descp = ((Button)sender).CommandArgument;
                List<Articulo> listFilterByBrand = new ArticuloManager().ListarArticulos();
                listFilterByBrand=listFilterByBrand.Where(x => x.Marca.Descripcion.ToUpper()==descp.ToUpper()).ToList();
                Session[GlobalVariables.articleList]=listFilterByBrand;
                Session[GlobalVariables.isFilterList]=true;
                Response.Redirect("ArticleList.aspx", false);
            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());
            }
        }
        protected void btnAllProducts_Click(object sender, EventArgs e) {
            try {
                Session.Remove(GlobalVariables.articleList);
                Session.Remove(GlobalVariables.isFilterList);
                Response.Redirect("ArticleList.aspx", false);
            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());
            }
        }
        protected void btnFavourites_Click(object sender, EventArgs e) {
            try {
                Response.Redirect("Favourites.aspx", false);
            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());
            }
        }
        protected void btnShopping_Click(object sender, EventArgs e) {
            try {
                Response.Redirect("Sales.aspx", false);
            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());
            }
        }
        protected void shoppingCart_Click(object sender, EventArgs e) {
            try {
                Response.Redirect("ShoppingCart.aspx", false);
            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());
            }
        }
    }
}