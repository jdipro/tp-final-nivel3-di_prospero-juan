<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="TechStoreWeb.LogIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
         .validacion{
            color: red;
            font-size: 12px;
        }
    </style>
    <script>
        function validarLogIn() {
            const txtEmail = document.getElementById("txtEmail");
            const txtPassword = document.getElementById("txtPassword");

            let valido = true;

            if (txtEmail.value.trim() === "") {
                txtEmail.classList.add("is-invalid");
                txtEmail.classList.remove("is-valid");
                valido = false;
            } else {
                txtEmail.classList.remove("is-invalid");
                txtEmail.classList.add("is-valid");
            }

            if (txtPassword.value.trim() === "") {
                txtPassword.classList.add("is-invalid");
                txtPassword.classList.remove("is-valid");
                valido = false;
            } else {
                txtPassword.classList.remove("is-invalid");
                txtPassword.classList.add("is-valid");
            }

            return valido;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-4">
            <h2>Login</h2>
            <div class="mb-3">                            
                <label class="form-label">Email</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" ClientIDMode="Static" placeholder="xxxx@xxxx.com"/>
                <asp:RequiredFieldValidator ErrorMessage="El Email es requerido" CcsClas="validacion" ControlToValidate="txtEmail" runat="server"  />
                <asp:RegularExpressionValidator ErrorMessage="la entrada debe tener formato de E-mail" ControlToValidate="txtEmail" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" runat="server" CcsClass="validacion"/>           
            </div>
            <div class="mb-3">
                <label class="form-label">Password</label>
                <asp:TextBox runat="server" cssclass="form-control" placeholder="********" ID="txtPassword" ClientIDMode="Static" TextMode="Password"/>
            </div>
            <asp:Button Text="Ingresar" cssclass="btn btn-primary" ID="btnLogin" OnClientClick="return validarLogIn()"  OnClick="btnLogin_Click" runat="server" />
            <a href="/">Cancelar</a>
            

        </div>
    </div>


</asp:Content>
