<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Contacto.aspx.cs" Inherits="TechStoreWeb.Contacto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="row">
        <div class="col-2"></div>
        <div class="col">
            <div class="mb-3">
                <label class="form-label">Ingresa tu Email</label>
                <asp:textbox runat="server" ID="txtEmail" cssclass="form-control"/>
            </div>
            <div class="mb-3">
                <label class="form-label">Ingresa tu Nombre</label>
                <asp:textbox runat="server" ID="txtNombre" cssclass="form-control"/>
            </div>
            <div class="mb-3">
                <label class="form-label">Ingresa tu Apellido</label>
                <asp:textbox runat="server" ID="txtApellido" cssclass="form-control"/>
            </div>
            <div class="mb-3">
                <label class="form-label">Asunto</label>
                <asp:textbox runat="server" ID="txtAsunto" cssclass="form-control"/>
            </div>
            <div class="mb-3">
                <label class="form-label">Mensaje</label>
                <asp:TextBox TextMode="MultiLine" runat="server" ID="txtMensaje" cssclass="form-control"/>
            </div>
            <asp:Button Text="Aceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" ID="btnAceptar" runat="server" />
            <asp:Label ID="lblMensaje" runat="server" Visible="false" CssClass="text-success" />

        </div>
        <div class="col"></div>
    </div>
</asp:Content>
