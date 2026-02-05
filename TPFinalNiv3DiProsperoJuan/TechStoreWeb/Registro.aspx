<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="TechStoreWeb.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       
   <script>
       function validarRegistro() {
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
            <h2>Creá tu perfil de Usuario.</h2>
            <div class="mb-3">                       
                <label class="form-label">Email</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" ClientIDMode="Static" placeholder="xxxx@xxxx.com" />            
                <asp:RegularExpressionValidator ErrorMessage="El e-mail es requerido" ControlToValidate="txtEmail" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" runat="server" CcsClass="validacion"/>

            </div>
            <div class="mb-3">
                <label class="form-label">Password</label>
                <asp:TextBox runat="server" cssclass="form-control" ID="txtPassword" ClientIDMode="Static" TextMode="Password" placeholder="*********"/>
            </div>
            <asp:Button Text="Registrarse" cssclass="btn btn-primary" ID="btnRegistrarse" OnClientClick="return validarRegistro()" OnClick="btnRegistrarse_Click" runat="server" />
            <a href="/">Cancelar</a>

        </div>
    </div>







</asp:Content>
