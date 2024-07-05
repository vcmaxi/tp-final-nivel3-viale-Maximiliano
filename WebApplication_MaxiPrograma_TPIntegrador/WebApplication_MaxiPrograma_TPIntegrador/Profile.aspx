<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="WebApplication_MaxiPrograma_TPIntegrador.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <asp:UpdatePanel runat="server" Class="d-flex">
            <ContentTemplate>
                <div class="col-6 m-2">
                    <div class="mb-3">
                        <asp:Label Text="Email:" runat="server" CssClass="form-label" ID="lblEmail" />
                        <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" Enabled="false" />
                    </div>

                    <div class="mb-3">
                        <asp:Label Text="Nombre:" runat="server" CssClass="form-label" ID="lblNombre" />
                        <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <asp:Label Text="Apellido:" runat="server" CssClass="form-label" ID="lblApellido" />
                        <asp:TextBox runat="server" ID="txtApellido" CssClass="form-control" />
                    </div>

                    <div class="mb-3">
                        <asp:Button Text="Save" ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" CssClass="btn btn-dark" />
                        <asp:HyperLink NavigateUrl="Default.aspx" runat="server" Text="Return" CssClass="btn btn-dark " />
                    </div>
                </div>

                <div class="col-3 mx-3">
                    <div class="mb-3">
                        <label class="form-label">Profile Image </label>
                        <asp:FileUpload ID="profileImgUpload" runat="server" CssClass="form-control" />
                    </div>
                    <div>
                        <asp:Image runat="server" ID="imgProfile" CssClass="img-fluid mb-3 imageSize" />
                        <div>
                            <asp:Label runat="server" CssClass="form-label validatingForms" Visible="false" ID="lblExtensionError" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
