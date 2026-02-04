<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="TechStoreWeb.LogIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        function validarLogin() {

            //capturar el control. 
            const txtEmail = document.getElementById("txtEmail");
            const txtPassword = document.getElementById("txtPassword");
            if (txtEmail.value == "") {
                txtEmail.classList.add("is-invalid");
                txtEmail.classList.remove("is-valid");
                txtPassword.classList.add("is-valid");
                return false;
            }
            txtEmail.classList.remove("is-invalid");
            txtEmail.classList.add("is-valid");
            return true;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-4">
            <h2>Login</h2>
            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox runat="server" cssclass="form-control" placeholder="e-mail" required ID="txtEmail"/>
            </div>
            <div class="mb-3">
                <label class="form-label">Password</label>
                <asp:TextBox runat="server" cssclass="form-control" placeholder="********" ID="txtPassword" TextMode="Password"/>
            </div>
            <asp:Button Text="Ingresar" cssclass="btn btn-primary" ID="btnLogin" OnClientClick="return validarLogin()"  OnClick="btnLogin_Click" runat="server" />
            <a href="/">Cancelar</a>
            

        </div>
    </div>


</asp:Content>
