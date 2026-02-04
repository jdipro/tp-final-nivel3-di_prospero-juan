<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="TechStoreWeb.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
         .validacion{
            color: red;
            font-size: 12px;
        }
    </style>   
    <script>
        function validarRegistro() {

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
            <h2>Creá tu perfil de Usuario.</h2>
            <div class="mb-3">                       
                <label class="form-label">Email</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" placeholder="xxxx@xxxx.com"/>
                <asp:RequiredFieldValidator ErrorMessage="El Email es requerido" CcsClas="validacion" ControlToValidate="txtEmail" runat="server" />
                <asp:RegularExpressionValidator ErrorMessage="la entrada debe tener formato de E-mail" ControlToValidate="txtEmail" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" runat="server" CcsClass="validacion"/>

            </div>
            <div class="mb-3">
                <label class="form-label">Password</label>
                <asp:TextBox runat="server" cssclass="form-control" ID="txtPassword" TextMode="Password"/>
            </div>
            <asp:Button Text="Registrarse" cssclass="btn btn-primary" ID="btnRegistrarse" OnClientClick="return validarRegistro()" OnClick="btnRegistrarse_Click" runat="server" />
            <a href="/">Cancelar</a>

        </div>
    </div>







</asp:Content>
