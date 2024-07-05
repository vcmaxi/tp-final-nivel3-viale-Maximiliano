using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Dominio;
using Helper;
using Manager;

namespace WebApplication_MaxiPrograma_TPIntegrador {
    public partial class Default: System.Web.UI.Page {

        public Articulo art = new Articulo();
        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                WebAPPHelper.ddlSortByConfiguration(ddlSortBy, this.Context);
                List<Articulo> articleList = new ArticuloManager().ListarArticulos();
                Session[GlobalVariables.articleList]=articleList;
                repeaterArt.DataSource=Session[GlobalVariables.articleList];
                repeaterArt.DataBind();
                ddlSortBy_SelectedIndexChanged(sender, e);
            }

        }
        protected void btnDetalle_Click(object sender, EventArgs e) {
            try {
                if(Help.ValidateString(((Button)sender).CommandArgument)) {
                    int id = int.Parse(((Button)sender).CommandArgument);
                    Articulo art = new ArticuloManager().ObtenerArticuloPorId(id);
                    if(art!=null) { Response.Redirect("Detail.aspx?id="+art.Id, false); Session[GlobalVariables.isFromDefault]=true; return; }
                    Help.RedirectToErrorPage(this.Context, "Artículo inexistente."); return;
                }
            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());

            }
        }

        protected void ddlSortBy_SelectedIndexChanged(object sender, EventArgs e) {
            WebAPPHelper.ddlSortByManipulationForArticles(ddlSortBy.SelectedIndex, repeaterArt, this.Context);
        }
    }
}