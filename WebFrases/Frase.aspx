<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMestre.Master" AutoEventWireup="true" CodeBehind="Frase.aspx.cs" Inherits="WebFrases.Frase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" GroupingText="Cadastro/Alteração de Frases">
        <asp:Label ID="Label1" runat="server" Text="ID"></asp:Label>
        <br />
        <asp:TextBox ID="txtId" runat="server" Enabled="False"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Frase"></asp:Label>
        <br />
        <asp:TextBox ID="txtFrase" runat="server" Width="570px"></asp:TextBox>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Autor"></asp:Label>
        <br />
        <asp:DropDownList ID="ddlAutor" runat="server">
        </asp:DropDownList>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Categoria"></asp:Label>
        <br />
        <asp:DropDownList ID="ddlCategoria" runat="server">
        </asp:DropDownList>
        <br />
        <asp:Button ID="btnSalvar" runat="server" Text="Inserir" OnClick="btnSalvar_Click" />
        <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" Text="Cancelar" OnClick="btnCancelar_Click" />
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" GroupingText="Registro das Frases">
    <asp:GridView ID="gvDados" runat="server" AutoGenerateColumns="False" Width="570px" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" OnRowDeleting="gvDados_RowDeleting" OnSelectedIndexChanged="gvDados_SelectedIndexChanged"  >
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:CommandField ShowDeleteButton="True" />
                <asp:BoundField DataField="id" HeaderText="Id" />
                <asp:BoundField DataField="frase" HeaderText="Frase" />
                <asp:BoundField DataField="nome" HeaderText="Autor" />
                <asp:BoundField DataField="nomeCategoria" HeaderText="Categoria" />
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#487575" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#275353" />
        </asp:GridView>
    </asp:Panel>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
</asp:Content>
