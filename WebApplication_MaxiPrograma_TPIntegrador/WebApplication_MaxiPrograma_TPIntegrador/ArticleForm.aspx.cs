using System;
using Dominio;
using Helper;
using Manager;

namespace WebApplication_MaxiPrograma_TPIntegrador {
    public partial class ArticleForm: System.Web.UI.Page {
        public Articulo articulo = null;
        protected void Page_Load(object sender, EventArgs e) {
            if(!Help.IsUserAdmin(this.Context, GlobalVariables.userString)) {
                Response.Redirect("Default.aspx", false); return;
            }
            lblPanel.Text="";

            if(!IsPostBack) {
                fillDropDowns();
                string id = Request.QueryString["id"];
                if((Help.ValidateString(id))) { //SIGNIFICA QUE ES UN ARTICULO SELECCIONADO POR EL ADMIN
                    btnAddArticle.Visible=false;
                    btnAddArticle.Enabled=false;
                    articulo=new ArticuloManager().ObtenerArticuloPorId(int.Parse(id));
                    if(articulo!=null) {
                        txtCodigo.Text=articulo.Codigo;
                        txtNombre.Text=articulo.Nombre;
                        txtDesc.Text=articulo.Descripcion;
                        txtPrecio.Text=articulo.precio.ToString();
                        ddlMarca.SelectedValue=articulo.Marca.Id.ToString();
                        ddlTipo.SelectedValue=articulo.Categoria.Id.ToString();
                        Help.ValidateImg(imageArticle, articulo.imagenUrl, GlobalVariables.errorImgPath);
                    } else {
                        Help.RedirectToErrorPage(this.Context, "The item you are looking for, does not exist."); return;
                    }
                } else {
                    btnModify.Enabled=false;
                    btnModify.Visible=false;
                    btnDelete.Visible=false;
                    btnDelete.Enabled=false;
                    articulo=new Articulo();
                }
            }

        }
        protected void txtImagen_TextChanged(object sender, EventArgs e) {
            try {
                Help.ValidateImg(imageArticle, txtImagen.Text, GlobalVariables.errorImgPath);
            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());
            }

        }
        protected void btnAddArticle_Click(object sender, EventArgs e) {
            lblArticleForm.Visible=false;
            if(Page.IsValid) {
                try {
                    articulo=new Articulo();
                    articulo.Codigo=txtCodigo.Text;
                    if(!(new ArticuloManager().CodeExist(articulo.Codigo))) {
                        articulo.Nombre=txtNombre.Text;
                        articulo.Descripcion=txtDesc.Text;
                        articulo.Marca.Descripcion=ddlMarca.Text;
                        articulo.Marca.Id=int.Parse(ddlMarca.SelectedValue.ToString());
                        articulo.Categoria.Descripcion=ddlTipo.Text;
                        articulo.Categoria.Id=int.Parse(ddlTipo.SelectedValue.ToString());
                        decimal price;
                        if(decimal.TryParse(txtPrecio.Text, out price)) { articulo.precio=price; }
                        Help.ValidateImg(imageArticle, txtImagen.Text, GlobalVariables.errorImgPath);
                        articulo.imagenUrl=imageArticle.ImageUrl;
                        new ArticuloManager().Agregar(articulo);
                        lblArticleForm.Text="Article has been added to list.";
                        lblArticleForm.CssClass="successfullyValidated";
                        btnAddArticle.Enabled=false;
                    } else {
                        Help.RedirectToErrorPage(this.Context, "The item already exist. Please try another one."); return;
                    }
                    lblArticleForm.Visible=true;
                } catch(Exception ex) {
                    Help.RedirectToErrorPage(this.Context, ex.ToString());
                }
            }
        }
        protected void btnModify_Click(object sender, EventArgs e) {
            lblArticleForm.Visible=false;
            if(Page.IsValid) {
                try {
                    string id = Request.QueryString["id"];
                    if((Help.ValidateString(id))) {
                        articulo=new ArticuloManager().ObtenerArticuloPorId(int.Parse(id));
                        if(articulo!=null) {
                            articulo.Codigo=txtCodigo.Text;
                            articulo.Nombre=txtNombre.Text;
                            articulo.Descripcion=txtDesc.Text;
                            articulo.Marca.Id=int.Parse(ddlMarca.SelectedValue.ToString());
                            articulo.Categoria.Id=int.Parse(ddlTipo.SelectedItem.Value.ToString());
                            decimal price;
                            if(decimal.TryParse(txtPrecio.Text, out price)) { articulo.precio=price; }
                            articulo.imagenUrl=imageArticle.ImageUrl;
                            new ArticuloManager().Modificar(articulo);
                            lblArticleForm.Text="Article has been modified in the list.";
                            lblArticleForm.CssClass="successfullyValidated";
                        } else {
                            Help.RedirectToErrorPage(this.Context, "The item you are looking for, does not exist."); return;
                        }
                    } else {
                        lblArticleForm.Text="Article could not be modified.";
                        lblArticleForm.CssClass="validatingForms";
                    }
                    lblArticleForm.Visible=true;
                } catch(Exception ex) {
                    Help.RedirectToErrorPage(this.Context, ex.ToString());
                }
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e) {
            if(Page.IsValid) {
                try {
                    panelDeleteConfirm.Visible=true;
                    lblArticleForm.CssClass="validatingForms";
                    lblArticleForm.Visible=true;
                    lblArticleForm.Text="You are about to permanently delete this article.<br>Are you sure you want to proceed?";
                } catch(Exception ex) {
                    Help.RedirectToErrorPage(this.Context, ex.ToString());
                }
            }
        }
        protected void btnReturn_Click(object sender, EventArgs e) {
            Response.Redirect("ArticleList.aspx", false);
        }
        protected void btnConfirmYes_Click(object sender, EventArgs e) {
            try {
                string id = Request.QueryString["id"];
                if((Help.ValidateString(id))) {
                    articulo=new ArticuloManager().ObtenerArticuloPorId(int.Parse(id));
                    if(articulo!=null) {
                        new ArticuloManager().Eliminar(articulo);
                        FavouriteArticle articuloFavorito = new FavManager().SearchFavouriteByIdArticle(articulo.Id);
                        if(articuloFavorito!=null) { new FavManager().Delete(articuloFavorito); }//debo eliminar los de favorito
                        lblArticleForm.CssClass="successfullyValidated";
                        lblArticleForm.Text="Article has been successfully deleted.";
                        btnDelete.Enabled=false;
                        btnModify.Enabled=false;
                        btnAddArticle.Enabled=false;
                        btnAddCat.Enabled=false;
                    } else {
                        Help.RedirectToErrorPage(this.Context, "The item you are looking for, does not exist."); return;
                    }
                } else {
                    lblArticleForm.Text="Article has not been deleted.";
                    lblArticleForm.CssClass="validatingForms";
                }
                lblError.Visible=false;
                panelDeleteConfirm.Visible=false;
                //fillDropDowns();
            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());
            }
        }
        protected void btnConfirmNo_Click(object sender, EventArgs e) {
            lblArticleForm.Visible=false;
            panelDeleteConfirm.Visible=false;
        }
        protected void btnAddCat_Click(object sender, EventArgs e) {
            try {
                if(!(string.IsNullOrEmpty(txtAddCat.Text))) {
                    int idCat;
                    int ddlMarcaOriginaValue = ddlMarca.SelectedIndex;
                    int ddlCatOriginaValue = ddlTipo.SelectedIndex;
                    if(!(new CategoriaManager().CategoryExist(txtAddCat.Text, out idCat))) {
                        new CategoriaManager().Agregar(txtAddCat.Text);
                        lblError.Text="Category has been added to list.";
                        lblError.CssClass="successfullyValidated";
                    } else {
                        lblError.Text="The Category already exist.";
                        lblError.CssClass=" validatingForms";
                        panelDeleteBrandOrCat.Visible=true;
                        Session["idCat"]=idCat;
                    }
                    fillDropDownsWithSelectedItem(ddlMarcaOriginaValue, ddlCatOriginaValue);
                }
            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());
            }
        }
        protected void btnAddMarca_Click(object sender, EventArgs e) {
            try {
                if(!(string.IsNullOrEmpty(txtAddMarca.Text))) {
                    int idMarca;
                    int ddlMarcaOriginaValue = ddlMarca.SelectedIndex;
                    int ddlCatOriginaValue = ddlTipo.SelectedIndex;
                    if(!(new MarcaManager().BrandExist(txtAddMarca.Text, out idMarca))) {
                        new MarcaManager().Agregar(txtAddMarca.Text);
                        lblError.Text="Brand has been added to list.";
                        lblError.CssClass="successfullyValidated";
                    } else {
                        lblError.CssClass=" validatingForms";
                        lblError.Text="The Brand already exist.";
                        panelDeleteBrandOrCat.Visible=true;
                        Session["idMarca"]=idMarca;
                    }
                    fillDropDownsWithSelectedItem(ddlMarcaOriginaValue, ddlCatOriginaValue);
                }
            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());
            }
        }
        protected void btnYes_Click(object sender, EventArgs e) {
            try {
                if(sender==btnYes) {
                    int ddlMarcaOriginaValue = ddlMarca.SelectedIndex;
                    int ddlCatOriginaValue = ddlTipo.SelectedIndex;
                    if(Session["idMarca"]!=null) {
                        if(new ArticuloManager().ArticleHasBrandId(int.Parse(Session["idMarca"].ToString()))) {
                            lblPanel.Text="Brand is being used so it can´t be deleted.";
                            lblPanel.CssClass="validatingForms";
                        } else {
                            new MarcaManager().Eliminar(new MarcaManager().GetBrandById(int.Parse(Session["idMarca"].ToString())));
                            lblPanel.Text="Brand "+txtAddMarca.Text+" has has been successfully deleted.";
                            lblPanel.CssClass="successfullyValidated";
                            Session.Remove("idMarca");
                        }
                    } else {
                        if(new ArticuloManager().ArticleHasCategoryId(int.Parse(Session["idCat"].ToString()))) {
                            lblPanel.Text="Category is being used so it can´t be deleted.";
                            lblPanel.CssClass="validatingForms";
                        } else {
                            new CategoriaManager().Eliminar(new CategoriaManager().GetCategoryById(int.Parse(Session["idCat"].ToString())));
                            lblPanel.Text="Category "+txtAddCat.Text+" has has been successfully deleted.";
                            lblPanel.CssClass="successfullyValidated";
                            Session.Remove("idCat");
                        }
                    }
                    lblError.Text="";
                    txtAddMarca.Text="";
                    txtAddCat.Text="";
                    panelDeleteBrandOrCat.Visible=false;
                    fillDropDownsWithSelectedItem(ddlMarcaOriginaValue, ddlCatOriginaValue);
                }
            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());
            }
        }
        protected void btnNo_Click(object sender, EventArgs e) {
            txtAddCat.Text="";
            txtAddMarca.Text="";
            panelDeleteBrandOrCat.Visible=false;
            lblError.Text="";
            lblPanel.Text="";
        }
        private void fillDropDowns() {
            ddlMarca.DataSource=new MarcaManager().ListarMarcas();
            ddlMarca.DataTextField="Descripcion";
            ddlMarca.DataValueField="Id";
            ddlMarca.DataBind();

            ddlTipo.DataSource=new CategoriaManager().ListarCategorias();
            ddlTipo.DataTextField="Descripcion";
            ddlTipo.DataValueField="Id";
            ddlTipo.DataBind();
        }
        private void fillDropDownsWithSelectedItem(int ddlMarcaSelected, int ddlTipoSelected) {
            fillDropDowns();
            ddlMarca.SelectedIndex=(ddlMarcaSelected);
            ddlTipo.SelectedIndex=(ddlTipoSelected);
        }
    }
}