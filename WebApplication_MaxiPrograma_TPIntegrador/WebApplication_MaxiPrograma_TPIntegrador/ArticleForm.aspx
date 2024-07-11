<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ArticleForm.aspx.cs" Inherits="WebApplication_MaxiPrograma_TPIntegrador.ArticleForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <asp:Label Text="Code:" runat="server" CssClass="form-label" ID="lblCodigo" />
                <asp:TextBox runat="server" ID="txtCodigo" CssClass="form-control" />
                <asp:RequiredFieldValidator ErrorMessage="Please fill out this field" ControlToValidate="txtCodigo" runat="server"
                    CssClass="validatingForms" Display="Dynamic" />
            </div>
            <div class="mb-3">
                <asp:Label Text="Name:" runat="server" CssClass="form-label" ID="lblNombre" />
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <asp:Label Text="Description:" runat="server" CssClass="form-label" ID="lblDesc" />
                <asp:TextBox runat="server" ID="txtDesc" CssClass="form-control" TextMode="MultiLine" />
            </div>
            <div class="mb-3">
                <asp:Label Text="Brand:" runat="server" CssClass="form-label" ID="lblMarca" />
                <asp:DropDownList runat="server" ID="ddlMarca" CssClass="form-select" />
            </div>
            <div class="mb-3">
                <asp:Label Text="Category:" runat="server" CssClass="form-label" ID="lblTipo" />
                <asp:DropDownList runat="server" ID="ddlTipo" CssClass="form-select" />
            </div>
            <div class="mb-3">
                <asp:Label Text="Price:" runat="server" CssClass="form-label" ID="lblPrecio" />
                <asp:TextBox runat="server" ID="txtPrecio" CssClass="form-control" />
                <asp:RegularExpressionValidator ErrorMessage="Not a correct format (Ej: 3455.99)." ControlToValidate="txtPrecio"
                    runat="server" CssClass="validatingForms" Display="Dynamic" ValidationExpression="^\d*\.?\d+$" />
            </div>

        </div>
        <div class="col-4">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <br />
                    <asp:TextBox runat="server" AutoPostBack="true" ID="txtImagen" OnTextChanged="txtImagen_TextChanged"
                        CssClass="form-control" Text="Image Link" onfocus="if (this.value=='Image Link') this.value='';"      onblur="if (this.value=='') this.value='Image Link';" /> 
                    <asp:Image runat="server" ID="imageArticle" CssClass="imageSize mt-4 " />

                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="mt-4">
                <asp:Panel runat="server" DefaultButton="btnAddMarca">
                    <asp:Label Text="Add Brand:" runat="server" CssClass="form-label" ID="lblAddMarca" />
                    <div class="d-flex">
                        <asp:TextBox runat="server" Text="" ID="txtAddMarca" CssClass="form-control" />
                        <asp:Button Text="Add" runat="server" ID="btnAddMarca" CssClass="btn btn-dark mx-2 btn-sm" OnClick="btnAddMarca_Click" CausesValidation="false" />
                    </div>
                </asp:Panel>
                <div class="mt-3 ">
                    <asp:Panel runat="server" DefaultButton="btnAddCat">
                        <asp:Label Text="Add Category:" runat="server" CssClass="form-label" ID="lblAddCat" />
                        <div class="d-flex">
                            <asp:TextBox runat="server" Text="" ID="txtAddCat" CssClass="form-control" />
                            <asp:Button Text="Add" runat="server" ID="btnAddCat" CssClass="btn btn-dark mx-2 btn-sm" OnClick="btnAddCat_Click" CausesValidation="false" />
                        </div>
                    </asp:Panel>
                </div>
                <asp:Label Text="" runat="server" ID="lblError" CssClass="form-label" />

                <asp:Panel runat="server" Visible="false" ID="panelDeleteBrandOrCat" CssClass="modalPopup">
                    <div class="confirmDialog">
                        <p class="validatingForms">
                            You can delete the parameter if no article is using it.<br>
                            Are you sure you want to proceed?
                        </p>
                        <asp:Button ID="btnYes" runat="server" Text="Yes" OnClick="btnYes_Click" CssClass="btn btn-dark btn-sm" CausesValidation="false" />
                        <asp:Button ID="btnNo" runat="server" Text="No" OnClick="btnNo_Click" CssClass="btn btn-dark btn-sm" CausesValidation="false" />
                        <div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Label Text="" runat="server" ID="lblPanel" />

            </div>
        </div>

    </div>
    <div class="mt-2">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div>
                </div>
                <asp:Button Text="Add" runat="server" ID="btnAddArticle" CssClass="btn btn-dark btn-sm" OnClick="btnAddArticle_Click" />
                <asp:Button Text="Modify" runat="server" ID="btnModify" CssClass="btn btn-dark btn-sm" OnClick="btnModify_Click" />
                <asp:Button Text="Delete" runat="server" ID="btnDelete" CssClass="btn btn-dark btn-sm" OnClick="btnDelete_Click" />

                <div>
                    <asp:Button Text="Return" runat="server" ID="btnReturn" CssClass="btn btn-dark mt-2 btn-sm" OnClick="btnReturn_Click"
                        CausesValidation="false" />
                </div>
                <asp:Panel runat="server" Visible="false" ID="panelDeleteConfirm" CssClass="modalPopup mt-3">
                    <div class="confirmDialog">
                        <asp:Button ID="btnConfirmYes" runat="server" Text="Yes" OnClick="btnConfirmYes_Click" CssClass="btn btn-dark btn-sm" CausesValidation="false" />
                        <asp:Button ID="btnConfirmNo" runat="server" Text="No" OnClick="btnConfirmNo_Click" CssClass="btn btn-dark btn-sm" CausesValidation="false" />
                    </div>
                </asp:Panel>
                <asp:Label Text="" runat="server" Visible="false" ID="lblArticleForm" />
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

</asp:Content>
