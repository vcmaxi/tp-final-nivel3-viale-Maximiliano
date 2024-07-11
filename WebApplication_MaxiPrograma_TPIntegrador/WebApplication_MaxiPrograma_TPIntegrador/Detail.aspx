<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="WebApplication_MaxiPrograma_TPIntegrador.Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%if(articulo!=null) { %>

    <div class="card" style="width: 15rem;">
        <asp:Image class="card-img-top card-body" runat="server" ID="imageArticle" />
        <div class="card-body">
            <h4 class="card-title mb-3"><%= articulo.Categoria.Descripcion %></h4>
            <div class="d-flex">
                <h5 class="card-title"><%= articulo.Marca.Descripcion +"  "+articulo.Codigo %></h5>

                <%if(Helper.Help.IsSessionActive(this.Context, WebApplication_MaxiPrograma_TPIntegrador.GlobalVariables.userString)) {
                        if(!(Helper.Help.IsUserAdmin(this.Context, WebApplication_MaxiPrograma_TPIntegrador.GlobalVariables.userString))) { %>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:ImageButton ImageUrl="./Images/favouriteIcon" runat="server" ID="favouriteIcon" OnClick="favouriteIcon_Click" CssClass="imageSizeForIcon mx-3" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <% }%>
                <%} %>
            </div>
            <div class="d-flex align-items-baseline">
                <p class="card-title"><%= articulo.Nombre %></p>
            </div>
            <p class="card-text" aria-multiline="true"><%=articulo.Descripcion %></p>
            <h5 class="card-text text-decoration-underline text-danger">Price: <%=articulo.precio.ToString("C")%></h5>
            <div class="mt-3">
                <%if(!Helper.Help.IsUserAdmin(this.Context, WebApplication_MaxiPrograma_TPIntegrador.GlobalVariables.userString)) { %>
                <asp:Button Text="Add To Cart" runat="server" CssClass="btn btn-dark btn-sm " ID="btnAddToCart" OnClick="btnAddToCart_Click" />
                <% } %>
                <a href='<%= (Helper.Help.IsSessionContaining(this.Context, WebApplication_MaxiPrograma_TPIntegrador.GlobalVariables.isFromDefault) ? "Default.aspx" : "ArticleList.aspx") %>' class="btn btn-dark btn-sm">Return</a>
            </div>
        </div>
    </div>

    <% } %>
</asp:Content>
