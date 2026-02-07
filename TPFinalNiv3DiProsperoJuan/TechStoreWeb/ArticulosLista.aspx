<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ArticulosLista.aspx.cs" Inherits="TechStoreWeb.ArticulosLista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> --%>
    
    <h1>Lista de Articulos</h1>

  
            <div class="row">
                <div class="col-6">
                    <div class="mb-3">                
                        <asp:Label Id="lblFiltro" Text="filtrar" runat="server" />
                        <asp:TextBox ID="txtfiltro" runat="server"  CssClass="form-control" AutoPostBack="true" OnTextChanged="filtro_TextChanged" placeholder="Buscar por nombre" />                                                           
                    </div>
                    <div class="mb-3">
                        <div>   
                            <asp:Button ID="btnBorrarFiltro" Text="Borrar" runat="server" CssClass="btn btn-primary"  OnClick="btnBorrarFiltro_Click"  />
                        </div> 
                    </div>
                 </div>              
                <div class="col-6" style="display: flex; flex-direction: column; justify-content: flex-end;">
                    <div class="mb-3">
                        <asp:CheckBox Text="Filtro Avanzado" 
                        CssClass="" ID="chkAvanzado" runat="server" 
                        AutoPostBack="true"
                        OnCheckedChanged="chkAvanzado_CheckedChanged" />
                    </div>
                </div>
            

            <% if (chkAvanzado.Checked)
                { %>
            <div class="row">
                    <div class="col-3">
                        <div class="mb-3">
                            <asp:Label Text="Campo" ID="lblCampo" runat="server" />
                            <asp:DropDownList runat="server" AutoPostback="true" CssClass="form-control" id="ddlCampo" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged">
                                <asp:ListItem Text="Clasificación" />
                                <asp:ListItem Text="Empresa" />
                            </asp:DropDownList>
                        </div>
                    </div>
            
                    <div class="col-3">
                        <div class="mb-3">
                            <asp:Label Text="Criterio"  runat="server" />
                            <asp:DropDownList runat="server" ID="ddlCriterio" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-3">
                        <div class="mb-3">
                            <asp:Label Text="Filtro"  runat="server" />
                            <asp:TextBox runat="server" ID="txtFiltroAvanzado" CssClass="form-control" />
                        </div>
                    </div>
            </div>
            <div class="row">
                    <div class="col-3">
                        <div class="mb-3">
                            <asp:Button Text="Buscar" runat="server" CssClass="btn btn-primary" ID="btnBuscar" OnClick="btnBuscar_Click" />
                        </div>
                    </div>
            </div>

            <% }%>
            </div>


            <asp:GridView ID="dgvArticulos" runat="server" DataKeyNames="Id" CssClass="table" AutoGenerateColumns="false"
                    OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged"
                    OnPageIndexChanging="dgvArticulos_PageIndexChanging"
                    AllowPaging="true" PageSize="4">

                <Columns>
                    <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                    <asp:BoundField HeaderText="Empresa" DataField="Empresa.Descripcion" />
            
            
                    <asp:BoundField HeaderText="Clasificación" DataField="Clasificacion.Descripcion" />

                    <asp:BoundField HeaderText="Precio" DataField="Precio" />
                    <asp:CommandField HeaderText="Editar" ShowSelectButton="true" SelectText="✍" />
                </Columns>
            </asp:GridView>
     

    <a href="FormularioArticulos.aspx" class="btn btn-primary">Agregar</a>

</asp:Content>
