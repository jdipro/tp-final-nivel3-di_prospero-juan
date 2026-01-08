<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ArticulosLista.aspx.cs" Inherits="TechStoreWeb.ArticulosLista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <h1>Lista de Articulos</h1>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-6">
                    <div class="mb-3">                
                        <asp:Label Text="filtrar" runat="server" />
                        <asp:TextBox runat="server" ID="txtfiltro" CssClass="form-control" AutoPostBack="true" OnTextChanged="filtro_TextChanged" />  
                    </div>
                </div>
            </div>
            <asp:GridView ID="dgvArticulos" runat="server" DataKeyNames="Id" CssClass="table" AutoGenerateColumns="false"
                    OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged"
                    OnPageIndexChanging="dgvArticulos_PageIndexChanging"
                    AllowPaging="true" PageSize="4">

                <Columns>
                    <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                    <asp:BoundField HeaderText="Empresa" DataField="Empresa.Descripcion" />
            
            
                    <asp:BoundField HeaderText="Clasificacion" DataField="Clasificacion.Descripcion" />

                    <asp:BoundField HeaderText="Precio" DataField="Precio" />
                    <asp:CommandField HeaderText="Editar" ShowSelectButton="true" SelectText="✍" />
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>

    <a href="FormularioArticulos.aspx" class="btn btn-primary">Agregar</a>

</asp:Content>
