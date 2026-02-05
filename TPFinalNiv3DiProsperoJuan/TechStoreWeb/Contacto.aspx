<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Contacto.aspx.cs" Inherits="TechStoreWeb.Contacto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        function validarContacto() {

            //capturar el control. 
            const txtEmail = document.getElementById("txtEmail");
            const txtNombre = document.getElementById("txtNombre");
            const txtApellido = document.getElementById("txtApellido");
            const txtAsunto = document.getElementById("txtAsunto");
            const txtMensaje = document.getElementById("txtMensaje");

            let valido = true;

            if (txtEmail.value.trim() === "") {
                txtEmail.classList.add("is-invalid");
                txtEmail.classList.remove("is-valid");
                valido = false;
            } else {
                txtEmail.classList.remove("is-invalid");
                txtEmail.classList.add("is-valid");
            }

            if (txtNombre.value.trim() === "") {
                txtNombre.classList.add("is-invalid");
                txtNombre.classList.remove("is-valid");
                valido = false;
            } else {
                txtNombre.classList.remove("is-invalid");
                txtNombre.classList.add("is-valid");
            }

            if (txtApellido.value.trim() === "") {
                txtApellido.classList.add("is-invalid");
                txtApellido.classList.remove("is-valid");
                valido = false;
            } else {
                txtApellido.classList.remove("is-invalid");
                txtApellido.classList.add("is-valid");
            }

            if (txtAsunto.value.trim() === "") {
                txtAsunto.classList.add("is-invalid");
                txtAsunto.classList.remove("is-valid");
                valido = false;
            } else {
                txtAsunto.classList.remove("is-invalid");
                txtAsunto.classList.add("is-valid");
            }

            if (txtMensaje.value.trim() === "") {
                txtMensaje.classList.add("is-invalid");
                txtMensaje.classList.remove("is-valid");
                valido = false;
            } else {
                txtMensaje.classList.remove("is-invalid");
                txtMensaje.classList.add("is-valid");
            }



            return valido;
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="row">
        <div class="col-2"></div>
        <div class="col">
            <div class="mb-3">
                <label class="form-label">Ingresa tu Email</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" ClientIDMode="Static" placeholder="xxxx@xxxx.com - *requerido" />
                <asp:RegularExpressionValidator ErrorMessage="la entrada debe tener formato de E-mail" ControlToValidate="txtEmail" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" runat="server" CcsClass="validacion"/>
            </div>
            <div class="mb-3">
                <label class="form-label">Ingresa tu Nombre</label>
                <asp:textbox runat="server" ID="txtNombre" ClientIDMode="Static" cssclass="form-control" placeholder="*requerido" />
            </div>
            <div class="mb-3">
                <label class="form-label">Ingresa tu Apellido</label>
                <asp:textbox runat="server" ID="txtApellido" ClientIDMode="Static" cssclass="form-control" placeholder="*requerido" />
            </div>
            <div class="mb-3">
                <label class="form-label">Asunto</label>
                <asp:textbox runat="server" ID="txtAsunto" ClientIDMode="Static" cssclass="form-control" placeholder="*requerido" />
            </div>
            <div class="mb-3">
                <label class="form-label">Mensaje</label>
                <asp:TextBox TextMode="MultiLine" runat="server" ID="txtMensaje" ClientIDMode="Static" cssclass="form-control" placeholder="*requerido" />
            </div>
            <asp:Button Text="Aceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" ID="btnAceptar" OnClientClick="return validarContacto()" runat="server" />
            <asp:Button Text="Volver" CssClass="btn btn-primary" OnClick="btnHome_Click" ID="btnHome" runat="server" />
            <asp:Label ID="lblMensaje" runat="server" Visible="false" CssClass="text-success" />

        </div>
        <div class="col"></div>
    </div>
</asp:Content>
