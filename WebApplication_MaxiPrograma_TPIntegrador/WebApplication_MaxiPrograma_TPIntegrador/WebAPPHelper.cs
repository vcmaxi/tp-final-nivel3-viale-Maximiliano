using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Helper;
using Manager;
namespace WebApplication_MaxiPrograma_TPIntegrador {
    public static class WebAPPHelper {
        public static bool IsArticleInCart(HttpContext context) {
            try {
                string id = context.Request.QueryString["id"]; //id del articulo
                if((Help.ValidateString(id))) {
                    if(Help.IsSessionActive(context, GlobalVariables.userString)) {
                        User user = Help.ActiveUser(context, GlobalVariables.userString) as User;
                        if(new CartManager().IsArticleInCart(int.Parse(id), user.Id)) {
                            return true;
                        }
                    }
                }
                return false;
            } catch(Exception ex) {
                Help.RedirectToErrorPage(context, ex.ToString()); return false;
            }
        }
        public static bool IsArticleInCart(HttpContext context, int idArticulo) {
            try {
                if(Help.IsSessionActive(context, GlobalVariables.userString)) {
                    User user = Help.ActiveUser(context, GlobalVariables.userString) as User;
                    if(new CartManager().IsArticleInCart(idArticulo, user.Id)) {
                        return true;
                    }
                }
                return false;
            } catch(Exception ex) {
                Help.RedirectToErrorPage(context, ex.ToString()); return false;
            }
        }
        public static bool IsDgvEmpty(GridView dgv, Label lblNoResultsFound, HttpContext context) {
            try {
                List<Articulo> list = dgv.DataSource as List<Articulo>;
                if(list!=null) {
                    if(list.Count==0) {
                        dgv.Visible=false;
                        lblNoResultsFound.Visible=true;
                        lblNoResultsFound.Text="😿 <br> No results found";
                        return true;
                    }
                }
                return false;
            } catch(Exception ex) {
                Help.RedirectToErrorPage(context, ex.ToString()); return false;
            }
        }
        public static bool IsRepeaterEmpty(Repeater repeater, Label lblNoResultsFound, HttpContext context) {
            try {
                List<Articulo> list = repeater.DataSource as List<Articulo>;
                if(list!=null) {
                    if(list.Count==0) {
                        repeater.Visible=false;
                        lblNoResultsFound.Visible=true;
                        lblNoResultsFound.Text="😿 <br> No results found";
                        return true;
                    }
                }
                return false;
            } catch(Exception ex) {
                Help.RedirectToErrorPage(context, ex.ToString()); return false;
            }
        }
        public static void ddlSortByConfiguration(DropDownList ddlSortBy, HttpContext context) {
            try {
                ddlSortBy.Items.Clear();
                ddlSortBy.Items.Add("Price: Lowest to Highest ");
                ddlSortBy.Items.Add("Price: Highest to Lowest");
                ddlSortBy.Items.Add("Brand");
                ddlSortBy.Items.Add("Cathegory");
                if(Help.IsSessionContaining(context, GlobalVariables.isFromSales)) {
                    ddlSortBy.Items.Add("Date: Oldest to Newest");
                    ddlSortBy.Items.Add("Date: Newest to Oldest");
                }
            } catch(Exception) {
                throw;
            }
        }
        public static void ddlSortByManipulationForSales(int selectedIndex, Control container, HttpContext context) {
            try {
                GridView dgv = container as GridView;
                if(dgv!=null) {
                    List<Venta> ventaList;
                    if(!Help.DoesSessionContainValue(context, GlobalVariables.ventaList)) {
                        if(Help.IsUserAdmin(context, GlobalVariables.userString)) {
                            ventaList=new VentaManager().GetVentas();
                        } else {
                            ventaList=new VentaManager().GetVentasByUserId(((User)Help.ActiveUser(context, GlobalVariables.userString)).Id);
                        }
                    } else {
                        ventaList=context.Session[GlobalVariables.ventaList] as List<Venta>;
                    }
                    if(ventaList!=null) { //esta lista de ventas tiene la cantidad de filas que el datasource, es decir de articulos
                        List<Articulo> articleList = new List<Articulo>();
                        List<User> userList = new List<User>();
                        foreach(Venta venta in ventaList) {
                            articleList.Add(new ArticuloManager().ObtenerArticuloPorId(venta.idArticle));
                            userList.Add(new UserManager().GetuserById(venta.idUser));
                        }
                        List<int> indicesOrdenados = new List<int>();
                        SwitchForSorting(ref indicesOrdenados, selectedIndex, articleList, ventaList);
                        articleList=indicesOrdenados.Select(index => articleList[index]).ToList();
                        ventaList=indicesOrdenados.Select(index => ventaList[index]).ToList();
                        userList=indicesOrdenados.Select(index => userList[index]).ToList();

                        dgv.DataSource=articleList;
                        dgv.DataBind();

                        foreach(GridViewRow row in dgv.Rows) {
                            GridViewRowEventArgs e = new GridViewRowEventArgs(row);
                            ConcatenateArticlesAndUserForSales(e, dgv, context, ventaList, userList);
                        }
                    }
                }
            } catch(Exception) {
                throw;
            }
        }
        public static void ddlSortByManipulationForArticles(int selectedIndex, Control container, HttpContext context) {
            try {
                if(container!=null) {
                    List<Articulo> articleList = context.Session[GlobalVariables.articleList] as List<Articulo>;
                    if(articleList!=null) {
                        List<int> indicesOrdenados = new List<int>();
                        SwitchForSorting(ref indicesOrdenados, selectedIndex, articleList);
                        articleList=indicesOrdenados.Select(index => articleList[index]).ToList();
                        if(container is GridView) {
                            GridView gv = (GridView)container;
                            gv.DataSource=articleList;
                        } else if(container is Repeater) {
                            Repeater rep = (Repeater)container;
                            rep.DataSource=articleList;
                        }
                        container.DataBind();
                    }
                }
            } catch(Exception) {
                throw;
            }
        }
        private static void SwitchForSorting(ref List<int> indicesOrdenados, int selectedIndex, List<Articulo> articleList = null, List<Venta> ventaList = null) {
            switch(selectedIndex) {
                case 0: {
                    indicesOrdenados=articleList.Select((articulo, index) => new { Index = index, Value = articulo.precio })
                        .OrderBy(x => x.Value)
                        .Select(x => x.Index)
                        .ToList();
                }
                break;
                case 1: {
                    indicesOrdenados=articleList.Select((articulo, index) => new { Index = index, Value = articulo.precio })
                        .OrderByDescending(x => x.Value)
                        .Select(x => x.Index)
                        .ToList();
                }
                break;
                case 2: {
                    indicesOrdenados=articleList.Select((articulo, index) => new { Index = index, Value = articulo.Marca.Descripcion })
                        .OrderBy(x => x.Value)
                        .Select(x => x.Index)
                        .ToList();

                }
                break;
                case 3: {
                    indicesOrdenados=articleList.Select((articulo, index) => new { Index = index, Value = articulo.Categoria.Descripcion })
                        .OrderBy(x => x.Value)
                        .Select(x => x.Index)
                        .ToList();
                }
                break;
                case 4: {
                    indicesOrdenados=ventaList.Select((venta, index) => new { Index = index, Value = venta.FechaVenta })
                        .OrderBy(x => x.Value)
                        .Select(x => x.Index)
                        .ToList();
                }
                break;
                case 5: {
                    indicesOrdenados=ventaList.Select((venta, index) => new { Index = index, Value = venta.FechaVenta })
                        .OrderByDescending(x => x.Value)
                        .Select(x => x.Index)
                        .ToList();
                }
                break;
                default: { } break;
            }

        }
        public static void ConcatenateArticlesAndUserForSales(GridViewRowEventArgs e, GridView dgv, HttpContext context, List<Venta> ventaList = null, List<User> userList = null) {
            try {
                if(e.Row.RowType==DataControlRowType.DataRow) {
                    if(ventaList==null&&userList==null) { //ocurre cuando no es postback
                        userList=new List<User>();
                        if(Help.IsUserAdmin(context, GlobalVariables.userString)) {
                            ventaList=new VentaManager().GetVentas();
                        } else {
                            User user = Help.ActiveUser(context, GlobalVariables.userString) as User;
                            if(user!=null) { ventaList=new VentaManager().GetVentasByUserId(user.Id); }
                        }
                        foreach(Venta venta in ventaList) { userList.Add(new UserManager().GetuserById(venta.idUser)); }
                    }
                    Label lblFechaVenta = (Label)e.Row.FindControl("lblFechaVenta");
                    if(lblFechaVenta!=null) {
                        if(Help.IsUserAdmin(context, GlobalVariables.userString)) {
                            Label lblUserName = (Label)e.Row.FindControl("lblUserName");
                            Label lblUserLastName = (Label)e.Row.FindControl("lblUserLastName");
                            if(lblUserName!=null&&lblUserLastName!=null) {
                                lblFechaVenta.Text=ventaList[e.Row.DataItemIndex].FechaVenta.ToString("dd-MM-yyyy");
                                lblUserName.Text=userList[e.Row.DataItemIndex].Nombre;
                                lblUserLastName.Text=userList[e.Row.DataItemIndex].Apellido;
                            }
                        } else {
                            User user = Help.ActiveUser(context, GlobalVariables.userString) as User;
                            if(user!=null) {
                                lblFechaVenta.Text=ventaList[e.Row.DataItemIndex].FechaVenta.ToString("dd-MM-yyyy");
                                dgv.Columns[5].Visible=false;
                                dgv.Columns[6].Visible=false;
                            }
                        }
                    }
                }
            } catch(Exception) {
                throw;
            }
        }
        public static void PrepareFiltroScheme(DropDownList ddlMarca, DropDownList ddlCategoria, CheckBox ckbFiltro) {
            ckbFiltro.Checked=false;
            ddlMarca.DataSource=new MarcaManager().ListarMarcas();
            ddlMarca.DataTextField="Descripcion";
            ddlMarca.DataValueField="Id";
            ddlMarca.DataBind();

            ddlCategoria.DataSource=new CategoriaManager().ListarCategorias();
            ddlCategoria.DataTextField="Descripcion";
            ddlCategoria.DataValueField="Id";
            ddlCategoria.DataBind();
        }
        public static void FilterRule(DropDownList ddlCampo, DropDownList ddlCriterio) {
            ddlCriterio.Items.Clear();
            if(ddlCampo.SelectedValue=="Price"||ddlCampo.SelectedValue=="Date") {
                ddlCriterio.Items.Add("=");
                ddlCriterio.Items.Add(">");
                ddlCriterio.Items.Add(">=");
                ddlCriterio.Items.Add("<");
                ddlCriterio.Items.Add("<=");
                if(ddlCampo.SelectedValue=="Date") { ddlCriterio.Items.Add("Between"); }
            } else {
                ddlCriterio.Items.Add("Contains");
                ddlCriterio.Items.Add("Starts with");
                ddlCriterio.Items.Add("Ends with");
            }
        }
        public static void AddToDdl(DropDownList ddl, string Todas) {
            if(ddl.Items.FindByValue("-1")==null) {
                ddl.Items.Insert(0, new ListItem(Todas, "-1"));
            }
        }
        public static List<Venta> filterVentaListByDate(string rule, List<Venta> ventaList, string date1String, string date2String) {
            List<Venta> ventaFilterList = new List<Venta>();
            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now;
            if(Help.ValidateString(date1String)) {
                date1=DateTime.Parse(date1String);
            }
            if(Help.ValidateString(date2String)) {
                date2=DateTime.Parse(date2String);
            }
            switch(rule) {
                case "=": {
                    ventaFilterList=ventaList.Where(venta => venta.FechaVenta.Date==date1)
                    .ToList();
                }
                break;
                case ">": {
                    ventaFilterList=ventaList.Where(venta => venta.FechaVenta.Date>date1)
                    .ToList();
                }
                break;
                case ">=": {
                    ventaFilterList=ventaList.Where(venta => venta.FechaVenta.Date>=date1)
                    .ToList();
                }
                break;
                case "<": {
                    ventaFilterList=ventaList.Where(venta => venta.FechaVenta.Date<date1)
                    .ToList();
                }
                break;
                case "<=": {
                    ventaFilterList=ventaList.Where(venta => venta.FechaVenta.Date<=date1)
                    .ToList();
                }
                break;
                case "Between": {
                    ventaFilterList=ventaList.Where(venta => date1<=venta.FechaVenta.Date&&date2>=venta.FechaVenta.Date).ToList();
                }
                break;
                default:
                break;
            }
            return ventaFilterList;
        }
        public static void RemoveFilterFromDGV(HttpContext context) {
            if(Help.IsSessionContaining(context, GlobalVariables.isFromSales)) {
                List<Venta> ventaList = context.Session[GlobalVariables.ventaList] as List<Venta>;
                int cantVentas = (int)context.Session[GlobalVariables.cantVentas];
                context.Session.Remove(GlobalVariables.articleList);
                context.Session.Remove(GlobalVariables.ventaList);
                context.Session.Remove(GlobalVariables.isFilterList);
                context.Session.Remove(GlobalVariables.isFromSales);
                if(ventaList.Count==cantVentas) {
                    context.Response.Redirect("Default.aspx", false); return;
                } else {
                    context.Response.Redirect("Sales.aspx", false); return;
                }
            } else {
                if(((List<Articulo>)context.Session[GlobalVariables.articleList]).Count==new ArticuloManager().ListarArticulos().Count) {
                    context.Session.Remove(GlobalVariables.articleList);
                    context.Session.Remove(GlobalVariables.ventaList);
                    context.Session.Remove(GlobalVariables.isFilterList);
                    context.Response.Redirect("Default.aspx", false); return;
                }
                context.Session.Remove(GlobalVariables.articleList);
                context.Session.Remove(GlobalVariables.ventaList);
                context.Session.Remove(GlobalVariables.isFilterList);
                context.Response.Redirect("ArticleList.aspx", false);

            }
        }
    }
}
