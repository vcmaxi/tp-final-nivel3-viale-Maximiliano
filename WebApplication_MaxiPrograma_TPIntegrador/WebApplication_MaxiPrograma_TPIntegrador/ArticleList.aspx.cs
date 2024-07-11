using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Dominio;
using Helper;
using Manager;

namespace WebApplication_MaxiPrograma_TPIntegrador {
    public partial class ArticleList: System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            WebAPPHelper.AddToDdl(ddlMarca, "All");
            WebAPPHelper.AddToDdl(ddlCategoria, "All");
            if(!IsPostBack) {
                PrepareFiltroScheme(sender, e);
                TableDGV();
                WebAPPHelper.ddlSortByConfiguration(ddlSortBy, this.Context);
                ddlSortBy_SelectedIndexChanged1(sender, e);
            }
        }
        private void TableDGV() {
            if(Help.IsSessionContaining(this.Context, GlobalVariables.isFilterList)) {
                dgvArticles.DataSource=Session[GlobalVariables.articleList]; //lista filtrada 
            } else {
                Session[GlobalVariables.articleList]=new ArticuloManager().ListarArticulos();
                dgvArticles.DataSource=Session[GlobalVariables.articleList];
            }
            if(WebAPPHelper.IsDgvEmpty(dgvArticles, lblNoResultsFound, this.Context)) {
                lblSortBy.Visible=false;
                ddlSortBy.Visible=false;
                ckbFiltro.Visible=false;
            }

            DataBind();
            Session[GlobalVariables.articleList]=dgvArticles.DataSource;
            if(Help.IsSessionActive(this.Context, GlobalVariables.userString)) {
                if(((User)Session[GlobalVariables.userString]).Admin) {
                    dgvArticles.Columns[6].Visible=true;
                    btnAddArticle.Visible=true;
                }
            }
        }
        private void PrepareFiltroScheme(object sender, EventArgs e) {
            WebAPPHelper.PrepareFiltroScheme(ddlMarca, ddlCategoria, ckbFiltro);
            ddlCampo_SelectedIndexChanged(sender, e);
        }
        protected void dgvArticles_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                int id = int.Parse(dgvArticles.SelectedDataKey.Value.ToString());
                Articulo articulo = new ArticuloManager().ObtenerArticuloPorId(id);
                if(articulo!=null) { Response.Redirect("ArticleForm.aspx?Id="+articulo.Id, false); return; }
                Help.RedirectToErrorPage(this.Context, "Artículo Inexistente."); return;
            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());
            }
        }
        protected void btnAddArticle_Click(object sender, EventArgs e) {
            try {
                Response.Redirect("ArticleForm.aspx", false); return;
            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());
            }

        }
        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e) {
            WebAPPHelper.FilterRule(ddlCampo, ddlCriterio);
        }
        protected void btnSearch_Click(object sender, EventArgs e) {
            try {
                List<Articulo> filterList = new ArticuloManager().listaFiltrada(ddlCampo.SelectedValue, ddlMarca.SelectedValue,
                    ddlCategoria.SelectedValue, ddlCriterio.SelectedValue, txtFiltro.Text);
                Session[GlobalVariables.articleList]=filterList;
                Session[GlobalVariables.isFilterList]=true;
                Response.Redirect("ArticleList.aspx", false);

            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());
            }
        }
        protected void btnReturn_Click(object sender, EventArgs e) {
            WebAPPHelper.RemoveFilterFromDGV(this.Context);
        }
        protected void dgvArticles_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            dgvArticles.PageIndex=e.NewPageIndex;
            dgvArticles.DataSource=Session[GlobalVariables.articleList];
            dgvArticles.DataBind();
            ddlSortBy_SelectedIndexChanged1(sender, e);
        }
        protected void btnDetail_Click(object sender, EventArgs e) {
            try {
                if(Help.ValidateString(((Button)sender).CommandArgument)) {
                    int id = int.Parse(((Button)sender).CommandArgument);
                    Articulo art = new ArticuloManager().ObtenerArticuloPorId(id);
                    if(art!=null) { Response.Redirect("Detail.aspx?id="+art.Id, false); Session[GlobalVariables.isFromDefault]=false; ; return; }
                    Help.RedirectToErrorPage(this.Context, "Artículo inexistente."); return;
                }
            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());

            }
        }
        protected void ddlSortBy_SelectedIndexChanged1(object sender, EventArgs e) {
            dgvArticles.DataSource=Session[GlobalVariables.articleList];
            WebAPPHelper.ddlSortByManipulationForArticles(ddlSortBy.SelectedIndex, dgvArticles, this.Context);
        }
    }
}