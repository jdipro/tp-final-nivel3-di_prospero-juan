<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="TechStoreWeb.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Error</h3>
    <asp:Label Text="text" ID="lblError" runat="server" />

      <div class="row">
        <div class="col-md-4">
            <asp:Button Text="Volver" CssClass="btn btn-primary"  ID="btnvolverLogin" OnClick="btnvolverLogin_Click" runat="server" />
            
        </div>
    </div>



</asp:Content>
