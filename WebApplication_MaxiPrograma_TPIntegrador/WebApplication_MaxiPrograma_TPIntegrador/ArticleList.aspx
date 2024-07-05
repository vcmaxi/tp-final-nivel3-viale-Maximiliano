<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ArticleList.aspx.cs" Inherits="WebApplication_MaxiPrograma_TPIntegrador.ArticleList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="text-center h1 mb-5">
        <asp:Label runat="server" Visible="false" ID="lblNoResultsFound" CssClass="" />
    </div>
    <asp:Panel runat="server" DefaultButton="btnSearch">
        <asp:UpdatePanel runat="server" ID="updpArticleList">
            <ContentTemplate>
                <asp:CheckBox Text="Filter" runat="server" ID="ckbFiltro" AutoPostBack="true" CssClass="small text-secondary-emphasis" />
                <%if(ckbFiltro.Checked) {%>
                <div class="row">
                    <div class="col-2">
                        <asp:Label Text="Field" runat="server" ID="Campo" />
                        <asp:DropDownList runat="server" ID="ddlCampo" CssClass="form-select" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged">
                            <asp:ListItem Text="Name" Value="Name" />
                            <asp:ListItem Text="Description" Value="Description" />
                            <asp:ListItem Text="Code" Value="Code" />
                            <asp:ListItem Text="Price" Value="Price" />
                        </asp:DropDownList>
                    </div>

                    <div class="col-2">
                        <asp:Label Text="Brand" runat="server" ID="lblMarca" />
                        <asp:DropDownList runat="server" ID="ddlMarca" CssClass="form-select" />
                    </div>
                    <div class="col-2">
                        <asp:Label Text="Category" runat="server" ID="lblCategoria" />
                        <asp:DropDownList runat="server" ID="ddlCategoria" CssClass="form-select" />
                    </div>

                    <div class="col-2">
                        <asp:Label Text="Rule" runat="server" ID="lblCriterio" />
                        <asp:DropDownList runat="server" ID="ddlCriterio" CssClass="form-select" />
                    </div>

                    <div class="col-2">
                        <asp:Label Text="Filter" runat="server" ID="lblFiltro" />
                        <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control" />
                    </div>
                </div>
                <div>
                    <asp:Button Text="Search" runat="server" CssClass="btn btn-dark btn-sm mt-3 " ID="btnSearch" OnClick="btnSearch_Click" />
                </div>
                <%} %>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <div class="mt-3">
        <asp:Label Text="Sort By:" runat="server" ID="lblSortBy" CssClass="small text-secondary-emphasis btn-link" />
    </div>
    <div>
        <asp:DropDownList runat="server" ID="ddlSortBy" OnSelectedIndexChanged="ddlSortBy_SelectedIndexChanged1" AutoPostBack="true"
            CssClass="btn btn-secondary btn-sm dropdown-toggle mb-3" />
    </div>
    <asp:GridView runat="server" ID="dgvArticles" CssClass="table table-sm table-striped " AutoGenerateColumns="false" DataKeyNames="Id"
        OnSelectedIndexChanged="dgvArticles_SelectedIndexChanged"
        AllowPaging="true" PageSize="7" OnPageIndexChanging="dgvArticles_PageIndexChanging" PagerSettings-Mode="NextPrevious">
        <Columns>
            <asp:BoundField HeaderText="Code" DataField="Codigo" />
            <asp:BoundField HeaderText="Name" DataField="Nombre" />
            <asp:BoundField HeaderText="Brand" DataField="Marca.Descripcion" />
            <asp:BoundField HeaderText="Category" DataField="Categoria.Descripcion" />
            <asp:BoundField HeaderText="Price" DataField="Precio" />
            <asp:TemplateField HeaderText="Image">
                <ItemTemplate>
                    <img src='<%# Eval("ImagenUrl") %>' alt="Imagen"
                        class="imageSizeForListTable"
                        onerror="this.onerror=null;this.src='<%= WebApplication_MaxiPrograma_TPIntegrador.GlobalVariables.errorImgPath%>';" />
                    <asp:Button Text="View More.." runat="server" CssClass="btn btn-outline-dark ms-5 btn-sm" ID="btnDetail" OnClick="btnDetail_Click" CommandArgument='<%#Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField HeaderText="" SelectText="&#x2714" ShowSelectButton="true" Visible="false" />
        </Columns>
    </asp:GridView>

    <asp:Button Text="Add Article" runat="server" ID="btnAddArticle" CssClass="btn btn-dark btn-sm" OnClick="btnAddArticle_Click" Visible="false" />
    <asp:Button Text="Return" runat="server" ID="btnReturn" CssClass="btn btn-dark btn-sm" OnClick="btnReturn_Click" />
</asp:Content>
