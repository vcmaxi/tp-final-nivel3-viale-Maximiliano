<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication_MaxiPrograma_TPIntegrador.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">

        <%if((bool)Session["isFromLogin"]) {%>
        <h4 class=" text-decoration-underline">Please Login:</h4>
        <%} else { %>
        <h4 class=" text-decoration-underline">Please Register:</h4>
        <%} %>

        <asp:HiddenField ID="hfFocus" runat="server" />

        <asp:Panel runat="server">
            <div class="mb-3 col-3">
                <asp:Label Text="Email address" runat="server" ID="lblEmail" CssClass="form-label" />
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" aria-describedby="emailHelp"
                    ClientIDMode="Static" AutoPostBack="true" OnTextChanged="txtEmail_TextChanged" />

                <asp:RequiredFieldValidator ControlToValidate="txtEmail" runat="server" ValidateEmptyText="true"
                    ErrorMessage="Please fill out this field" CssClass="validatingForms" Display="Dynamic" />

                <asp:RegularExpressionValidator ControlToValidate="txtEmail" runat="server" CssClass="validatingForms"
                    ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                    ErrorMessage="Must be a valid email address" Display="Dynamic" />
                <div id="emailHelp" class="form-text">We'll never share your email with anyone else.</div>
                <asp:Label Text="" runat="server" Visible="false" ID="lblEmailExist" CssClass="validatingForms" />
            </div>
        </asp:Panel>

        <asp:Panel runat="server" DefaultButton="btnSubmit">
            <div class="mb-3 col-3">
                <div>
                    <asp:Label Text="Password" runat="server" ID="lblPass" CssClass="form-label" />
                    <asp:TextBox runat="server" ID="txtPass" CssClass="form-control" type="password" ClientIDMode="Static" />
                </div>

                <asp:RequiredFieldValidator ControlToValidate="txtPass" runat="server" ValidateEmptyText="true"
                    ErrorMessage="Please fill out this field" CssClass="validatingForms" Display="Dynamic" />

                <asp:RegularExpressionValidator CssClass="validatingForms" runat="server" ErrorMessage="Password needs to be at least 4 characters long."
                    ValidationExpression=".{4,}" ControlToValidate="txtPass" Display="Dynamic"> </asp:RegularExpressionValidator>
                <asp:Label Text="" runat="server" Visible="false" ID="lblErrorLogin" CssClass="validatingForms" />
                <div>
                    <asp:Button Text="Submit" runat="server" ID="btnSubmit" CssClass="btn btn-dark mt-3" OnClick="btnSubmit_Click" />
                </div>
            </div>
        </asp:Panel>

        <asp:Panel runat="server" ID="panelRegister" CssClass="modalPopup" Visible="false">
            <div class="confirmDialog">
                <p class="text-dark">Do you want to create a new user?</p>
                <asp:Button ID="btnConfirmYes" runat="server" Text="Yes" OnClick="btnConfirmYes_Click" CssClass="btn btn-success" />
                <asp:Button ID="btnConfirmNo" runat="server" Text="No" OnClick="btnConfirmNo_Click" CssClass="btn btn-danger" />
            </div>
        </asp:Panel>
    </div>
    <script>
        window.onload = function () {
            var focusControl = document.getElementById('<%= hfFocus.ClientID %>').value;
            if (focusControl) {
                document.getElementById(focusControl).focus();

            }
        };
    </script>
</asp:Content>
