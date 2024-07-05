<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Favourites.aspx.cs" Inherits="WebApplication_MaxiPrograma_TPIntegrador.Favourites" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="text-center h1 mb-5">
        <asp:Label runat="server" Visible="false" ID="lblNoResultsFound" CssClass="" />
    </div>

    <div class="row row-cols-1 row-cols-md-5 g-3">
        <asp:Repeater runat="server" ID="repeaterFavourites" OnItemDataBound="repeaterFavourites_ItemDataBound" OnItemCommand="repeaterFavourites_ItemCommand" >
            <ItemTemplate>
                <div class="col mx-4">
                    <div class="card" style="width: 18rem;">
                        <img src='<%# Eval("imagenUrl") %>'
                            class="card-img-top imageSize card-body " alt="Image"
                            onerror="this.onerror=null;this.src='<%= WebApplication_MaxiPrograma_TPIntegrador.GlobalVariables.errorImgPath%>';" />
                        <h4 class="card-title mx-3"><%#Eval("Categoria.Descripcion")  %></h4>
                        <div class="card-body">
                            <div class="d-flex ">
                                <h5 class="card-title"><%#Eval("Marca.Descripcion") +"  "+Eval("Codigo") %></h5>
                                <asp:ImageButton runat="server" ID="favouriteIcon" OnClick="favouriteIcon_Click" CssClass="imageSizeForIcon mx-3"
                                    CommandArgument='<%#Eval("Id")%>' />
                            </div>
                            <div class="d-flex align-items-baseline">
                                <p class="card-title"><%# Eval("Nombre") %></p>
                            </div>
                            <p class="card-text" aria-multiline="true"><%# Eval("Descripcion")%></p>
                            <h5 class="card-text text-decoration-underline text-danger">Price: <%#Eval("Precio")%>$</p>
                                <div class="mt-3">
                                    <%if(!Helper.Help.IsUserAdmin(this.Context, WebApplication_MaxiPrograma_TPIntegrador.GlobalVariables.userString)) { %>
                                    <asp:Button Text="Add To Cart" runat="server" CssClass="btn btn-dark btn-sm" CommandArgument='<%#Eval("Id")%>' ID="btnAddToCart" CommandName="AddToCart" />
                                    <% } %>
                                </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>
