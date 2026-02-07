<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="TechStoreWeb.Favoritos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:UpdatePanel ID="updFavoritos" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Label ID="lblVacio" runat="server" CssClass="alert alert-info" Visible="false" Text="No tenés artículos favoritos aún." /> 
            <div class="row row-cols-1 row-cols-md-3 g-4">
                <asp:Repeater ID="repFavoritos" runat="server">
                <ItemTemplate>
                    <div class="col">
                        <div class="card">
                            <img src="<%# Eval("ImagenUrl") %>" class="card-img-top" alt="">
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("Nombre") %></h5>
                                <p class="card-text"><%# Eval("Descripcion") %></p>
                                <p class="card-text"><%# Eval("Precio") %></p>

                                <%--Quitar favorito --%>
                                <asp:LinkButton
                                    ID="btnQuitarFavorito"
                                    runat="server"
                                    CssClass="btn btn-sm text-danger"
                                    CommandArgument='<%# Eval("Id") %>'
                                    OnCommand="btnQuitarFavorito_Command"
                                    CausesValidation="false"
                                    UseSubmitBehavior="false"
                                    OnClientClick="this.blur();"
                                    ToolTip="Quitar de favoritos">

                                    <i class="bi bi-heart-fill"></i>
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
