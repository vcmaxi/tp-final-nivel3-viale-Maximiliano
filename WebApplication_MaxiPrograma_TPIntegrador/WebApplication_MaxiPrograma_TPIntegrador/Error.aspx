<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="WebApplication_MaxiPrograma_TPIntegrador.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <asp:Label Text="Error:" runat="server" CssClass="form-label validatingForms"/>
        <div>
            <asp:TextBox runat="server" ID="txtError" TextMode="MultiLine" CssClass="validatingForms form-control" />
        </div>

    </div>
</asp:Content>
