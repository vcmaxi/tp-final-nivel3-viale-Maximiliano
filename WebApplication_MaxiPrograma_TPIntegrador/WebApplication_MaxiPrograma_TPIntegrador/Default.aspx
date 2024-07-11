<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication_MaxiPrograma_TPIntegrador.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <div class="mt-3">
    <asp:Label Text="Sort By:" runat="server" ID="lblSortBy" CssClass="small text-secondary-emphasis btn-link" />
    </div>
    <div>
        <asp:DropDownList runat="server" ID="ddlSortBy" OnSelectedIndexChanged="ddlSortBy_SelectedIndexChanged" AutoPostBack="true" 
            CssClass="btn btn-secondary btn-sm dropdown-toggle" />
    </div>
    <div class="row row-cols-1 row-cols-md-3 g-3 mt-1 mb-3">
        <asp:Repeater runat="server" ID="repeaterArt">
            <ItemTemplate>
                <div class="col">
                    <div class="card">
                        <img src='<%# Eval("imagenUrl") %>'
                            class="card-img-top imageSize card-body" alt="Image"
                            onerror="this.onerror=null;this.src='<%= WebApplication_MaxiPrograma_TPIntegrador.GlobalVariables.errorImgPath%>';" />
                        <div class="card-body">
                            <h5 class="card-title"><%#Eval("Marca.Descripcion") %></h5>
                            <h5 class="card-title"><%#Eval("Nombre") %></h5>
                            <asp:Button Text="View More.." runat="server" ID="btnDetalle" OnClick="btnDetalle_Click"  CommandArgument='<%#Eval("Id") %>' 
                                CssClass="btn btn-outline-dark btn-sm mt-3"/>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
