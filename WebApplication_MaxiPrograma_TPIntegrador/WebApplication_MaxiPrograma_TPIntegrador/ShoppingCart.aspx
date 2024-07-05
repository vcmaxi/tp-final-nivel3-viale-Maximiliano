<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="WebApplication_MaxiPrograma_TPIntegrador.ShoppingCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="text-center mb-3">
                <asp:Label runat="server" ID="lblCart" CssClass="h3" />
            </div>
            <asp:GridView runat="server" ID="dgvShoppingCartList" CssClass="table table-sm table-striped " AutoGenerateColumns="false" DataKeyNames="Id"
                OnSelectedIndexChanged="dgvShoppingCartList_SelectedIndexChanged">
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
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="" SelectText="&#x1F5D1;" ShowSelectButton="true" />
                </Columns>
            </asp:GridView>
            <asp:Button Text="BUY ALL" runat="server" CssClass="btn btn-dark" ID="btnBuy" OnClick="btnBuy_Click" />
            <asp:Panel ID="pnlConfirmPurchase" runat="server" CssClass="confirm-purchase-panel" Visible="false">
                <div class="text-center">
                    <h4>Confirm Purchase</h4>
                    <p>Are you sure you want to buy all items in your cart?</p>
                    <asp:Button ID="btnConfirmPurchase" runat="server" Text="Confirm" CssClass="btn btn-success" OnClick="btnConfirmPurchase_Click" />
                    <asp:Button ID="btnCancelPurchase" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancelPurchase_Click" />
                </div>
            </asp:Panel>
            <div class="text-center mt-3">
                <asp:Label ID="lblThankYou" runat="server" Text="Thanks for your purchasing. You will be redirected to My Shopping section"
                    Visible="false" CssClass="successfullyValidated text-capitalize fst-italic"></asp:Label>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
