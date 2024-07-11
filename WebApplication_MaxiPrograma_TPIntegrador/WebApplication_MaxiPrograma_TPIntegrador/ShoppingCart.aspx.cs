using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Dominio;
using Helper;
using Manager;

namespace WebApplication_MaxiPrograma_TPIntegrador {
    public partial class ShoppingCart: System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if(Help.IsUserAdmin(this.Context, GlobalVariables.userString)||!Help.IsSessionActive(Context, GlobalVariables.userString)) {
                if(Help.IsUserAdmin(this.Context, GlobalVariables.userString)) { Response.Redirect("Default.aspx", false); } else {
                    return;
                }
            }
            if(!IsPostBack) {
                TableForDGV();
            }

        }

        private void TableForDGV() {
            User user = Help.ActiveUser(this.Context, GlobalVariables.userString) as User;
            List<Cart> CartList = new CartManager().cartListByUserId(user.Id);
            List<Articulo> ArticleCartList = CartList.Select(x => new ArticuloManager().ObtenerArticuloPorId(x.IdArticulo)).ToList();
            dgvShoppingCartList.DataSource=ArticleCartList;
            dgvShoppingCartList.DataBind();
            Session[GlobalVariables.articlesInCart]=ArticleCartList;
            if(ArticleCartList.Count==0) {
                btnBuy.Visible=false; btnBuy.Enabled=false; lblCart.Text="😿<br>Shopping Cart is Empty!";
            } else { lblCart.Text="😽<br>Articles in Shopping Cart!"; }
        }

        protected void dgvShoppingCartList_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                string idArticle = dgvShoppingCartList.SelectedDataKey.Value.ToString();
                if(Help.ValidateString(idArticle)) {
                    User user = Help.ActiveUser(this.Context, GlobalVariables.userString) as User;
                    new CartManager().DeleteFromCart(new CartManager().GetArticleCart(int.Parse(idArticle), user.Id));
                };
                TableForDGV();
            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());
            }
        }

        protected void btnBuy_Click(object sender, EventArgs e) {
            pnlConfirmPurchase.Visible=true;

        }

        protected void btnConfirmPurchase_Click(object sender, EventArgs e) {
            try {
                List<Articulo> ArticleCartList = Session[GlobalVariables.articlesInCart] as List<Articulo>;
                if(ArticleCartList!=null) {
                    User user = Help.ActiveUser(this.Context, GlobalVariables.userString) as User;
                    foreach(Articulo Article in ArticleCartList) {
                        new CartManager().DeleteFromCart(new CartManager().GetArticleCart(Article.Id, user.Id));
                        Venta venta = new Venta();
                        venta.idArticle=Article.Id;
                        venta.idUser=user.Id;
                        venta.FechaVenta=DateTime.Now;
                        new VentaManager().AddVenta(venta);
                    }
                }
                pnlConfirmPurchase.Enabled=false;
                lblThankYou.Visible=true;
                string script = "setTimeout(function () { window.location.href = 'Sales.aspx'; }, 4000);";
                ScriptManager.RegisterStartupScript(this, GetType(), "redirectScript", script, true);
            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());
            }
        }

        protected void btnCancelPurchase_Click(object sender, EventArgs e) {
            pnlConfirmPurchase.Visible=false;
        }

        protected void btnReturn_Click(object sender, EventArgs e) {
            Response.Redirect("Default.aspx", false);
        }
    }

}