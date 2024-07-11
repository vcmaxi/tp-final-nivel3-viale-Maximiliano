using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Dominio;
using Helper;
using Manager;

namespace WebApplication_MaxiPrograma_TPIntegrador {
    public partial class Sales: System.Web.UI.Page {
        List<Articulo> ArticleCartList;
        List<Venta> PurchasingList;
        protected void Page_Load(object sender, EventArgs e) {
            if(!Help.IsSessionActive(Context, GlobalVariables.userString)) { return; }
            WebAPPHelper.AddToDdl(ddlMarca, "All");
            WebAPPHelper.AddToDdl(ddlCategoria, "All");
            if(!IsPostBack) {
                Session[GlobalVariables.isFromSales]=true;
                PrepareFiltroScheme(sender, e);
                WebAPPHelper.ddlSortByConfiguration(ddlSortBy, this.Context);
                TableForDGV();
                ddlSortBy_SelectedIndexChanged(sender, e);
            }
        }
        private void PrepareFiltroScheme(object sender, EventArgs e) {
            WebAPPHelper.PrepareFiltroScheme(ddlMarca, ddlCategoria, ckbFiltro);
            ddlCampo_SelectedIndexChanged(sender, e);
        }
        private void TableForDGV() {
            if(!(Help.IsUserAdmin(this.Context, GlobalVariables.userString))) {
                User user = Help.ActiveUser(this.Context, GlobalVariables.userString) as User;
                if(user!=null) { PurchasingList=new VentaManager().GetVentasByUserId(user.Id); } else { Response.Redirect("Default.aspx", true); }
            } else {
                PurchasingList=new VentaManager().GetVentas();
            }
            ArticleCartList=PurchasingList.Select(x => new ArticuloManager().ObtenerArticuloPorId(x.idArticle)).ToList();
            Session[GlobalVariables.articleList]=ArticleCartList;
            Session[GlobalVariables.ventaList]=PurchasingList; //ya sea de admin o de user
            Session[GlobalVariables.cantVentas]=PurchasingList.Count;
            dgvSalesList.DataSource=ArticleCartList;
            dgvSalesList.DataBind();
            if(WebAPPHelper.IsDgvEmpty(dgvSalesList, lblNoResultsFound, this.Context)) {
                lblSortBy.Visible=false;
                ddlSortBy.Visible=false;
                ckbFiltro.Visible=false;
            }
        }
        protected void dgvSalesList_RowDataBound(object sender, GridViewRowEventArgs e) {
            WebAPPHelper.ConcatenateArticlesAndUserForSales(e, dgvSalesList, this.Context);
        }
        protected void ddlSortBy_SelectedIndexChanged(object sender, EventArgs e) {
            WebAPPHelper.ddlSortByManipulationForSales(ddlSortBy.SelectedIndex, dgvSalesList, this.Context);
        }
        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e) {
            lblFiltro.Text="Filter";
            txtFiltro.Text="";
            WebAPPHelper.FilterRule(ddlCampo, ddlCriterio);
            if(ddlCampo.Text=="Date") {
                lblFiltro.Text="Filter First Date";
                txtFiltro.Text=DateTime.Now.ToString("dd-MM-yyyy");
                lblFiltro2.Visible=true;

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e) {
            try {
                List<Venta> ventaList = Session[GlobalVariables.ventaList] as List<Venta>;
                List<Venta> ventaFilterList;

                if(ddlCampo.SelectedValue!="Date") {
                    List<Articulo> articleFilterList = new ArticuloManager().listaFiltrada(ddlCampo.SelectedValue, ddlMarca.SelectedValue,
                        ddlCategoria.SelectedValue, ddlCriterio.SelectedValue, txtFiltro.Text);
                    ventaFilterList=ventaList
                         .Where(venta => articleFilterList.Any(articulo => articulo.Id==venta.idArticle))
                         .ToList();
                } else {
                    string rule = ddlCriterio.SelectedItem.Text;
                    ventaFilterList=WebAPPHelper.filterVentaListByDate(rule, ventaList, CalDate1.Value, calDate2.Value);

                }
                List<Articulo> articleList = new List<Articulo>();
                List<User> userList = new List<User>();
                foreach(Venta venta in ventaFilterList) {
                    articleList.Add(new ArticuloManager().ObtenerArticuloPorId(venta.idArticle));
                    userList.Add(new UserManager().GetuserById(venta.idUser));
                }

                dgvSalesList.DataSource=articleList;
                dgvSalesList.DataBind();
                foreach(GridViewRow row in dgvSalesList.Rows) {
                    GridViewRowEventArgs evForGV = new GridViewRowEventArgs(row);
                    WebAPPHelper.ConcatenateArticlesAndUserForSales(evForGV, dgvSalesList, this.Context, ventaList, userList);
                }
                Session[GlobalVariables.ventaList]=ventaFilterList;
                Session[GlobalVariables.isFilterList]=true;
                Session[GlobalVariables.isFromSales]=true;
                ddlSortBy_SelectedIndexChanged(sender, e);
                if(WebAPPHelper.IsDgvEmpty(dgvSalesList, lblNoResultsFound, this.Context)) {
                    ckbFiltro.Checked=false;
                    ckbFiltro.Visible=false;
                    lblSortBy.Visible=false;
                    ddlSortBy.Visible=false;
                }

            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());
            }
        }
        protected void btnReturn_Click(object sender, EventArgs e) {
            WebAPPHelper.RemoveFilterFromDGV(this.Context);
        }
    }
}