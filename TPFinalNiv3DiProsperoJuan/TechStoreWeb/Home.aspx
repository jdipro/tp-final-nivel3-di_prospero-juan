<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TechStoreWeb.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="color:red">ESTA ES HOME.ASPX</h1>
    <h2>Esta es la home para la prueba</h2>

    <asp:UpdatePanel ID="updHome" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <div class="row row-cols-1 row-cols-md-3 g-4">
                <asp:Repeater ID="repRepetidor" runat="server" OnItemDataBound="repRepetidor_ItemDataBound" >
                    <ItemTemplate>
                        <div class="col">
                         <div class="card">
                             <img src="<%#Eval("ImagenUrl") %>" class="card-img-top" alt="...">
                             <div class="card-body">
                                <h5 class="card-title"><%#Eval("Nombre") %></h5>
                                <p class="card-text"><%#Eval("Descripcion") %></p>
                                <p class="card-text"><%#Eval("Precio") %></p>

                                <a href="DetalleArticulo.aspx?id=<%#Eval("Id") %>">Info</a>

                                <asp:LinkButton ID="btnFavorito" runat="server" CssClass="btn btn-sm ms-2" CommandArgument='<%# Eval("Id") %>' OnCommand="btnFavorito_Command" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.blur();" >
                                    <i runat="server" id="iconHeartEmpty" class="bi bi-heart text-danger"></i>
                                    <i runat="server" id="iconHeartFill" class="bi bi-heart-fill text-danger"></i>
                                </asp:LinkButton>
                        
                             </div>
                         </div>
                     </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div> 

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
