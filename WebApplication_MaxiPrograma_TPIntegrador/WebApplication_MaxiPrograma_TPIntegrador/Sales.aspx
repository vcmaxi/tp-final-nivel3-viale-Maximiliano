<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="WebApplication_MaxiPrograma_TPIntegrador.Sales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" DefaultButton="btnSearch">
        <asp:UpdatePanel runat="server" ID="updpArticleList">
            <ContentTemplate>
                <div class="text-center h1 mb-5">
                    <asp:Label runat="server" Visible="false" ID="lblNoResultsFound" CssClass="" />
                </div>
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
                            <asp:ListItem Text="Date" Value="Date" />
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
                        <asp:DropDownList runat="server" ID="ddlCriterio" CssClass="form-select" AutoPostBack="true" />
                    </div>
                    <div class="d-flex col-2">
                        <div>
                            <asp:Label Text="Filter" runat="server" ID="lblFiltro" />
                            <%if(ddlCampo.SelectedItem.Text=="Date") {%>
                            <input type="date" name="name" value="" id="CalDate1" runat="server" required class="form-control" />
                        </div>
                        <%if(ddlCriterio.SelectedItem.Text=="Between") {%>
                        <div class="mx-3">
                            <asp:Label Text="Filter Second Date" runat="server" ID="lblFiltro2" Visible="false" />
                            <input type="date" name="name" value="" id="calDate2" runat="server" required class="form-control"/>
                        </div>

                        <% } %>
                        <% } else {%>
                        <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control" />
                        <% } %>
                    </div>
                </div>
                <div>
                    <asp:Button Text="Search" runat="server" CssClass="btn btn-dark btn-sm mt-3 " ID="btnSearch" OnClick="btnSearch_Click" />
                </div>
                <%} %>
                <div>
                    <asp:Label Text="Sort By:" runat="server" ID="lblSortBy" CssClass="small text-secondary-emphasis btn-link" />
                    <div>
                        <asp:DropDownList runat="server" ID="ddlSortBy" OnSelectedIndexChanged="ddlSortBy_SelectedIndexChanged" AutoPostBack="true" CssClass="btn btn-secondary btn-sm dropdown-toggle mb-3" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Label runat="server" ID="lblCart" />
            <asp:GridView runat="server" ID="dgvSalesList" CssClass="table table-sm table-striped " AutoGenerateColumns="false" DataKeyNames="Id"
                OnRowDataBound="dgvSalesList_RowDataBound">
                <Columns>
                    <asp:BoundField HeaderText="Code" DataField="Codigo" />
                    <asp:BoundField HeaderText="Brand" DataField="Marca.Descripcion" />
                    <asp:BoundField HeaderText="Category" DataField="Categoria.Descripcion" />
                    <asp:TemplateField HeaderText="Price">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# "$" + Eval("Precio").ToString() %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label Text="" runat="server" ID="lblFechaVenta" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="userName">
                        <ItemTemplate>
                            <asp:Label Text="" runat="server" ID="lblUserName" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="userLastName">
                        <ItemTemplate>
                            <asp:Label Text="" runat="server" ID="lblUserLastName" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Button Text="Return" runat="server" ID="btnReturn" CssClass="btn btn-dark btn-sm" OnClick="btnReturn_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
