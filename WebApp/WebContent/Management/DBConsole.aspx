<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/Management.Master" AutoEventWireup="true" CodeBehind="DBConsole.aspx.cs" Inherits="WebApp.WebContent.Management.DBConsole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



<div style="vertical-align:top; margin-top:10px; margin-left:10px;">
    <asp:TextBox ID="txtSQL" runat="server" TextMode="MultiLine" Height="80px" Width="90%" BorderStyle="Inset" ></asp:TextBox>
    <br />
     <div class="last">
    <asp:Button ID="btnExeSql" runat="server" CssClass="button" Text="Execute SQL" OnClick="btnExeSql_Click" />
    </div>
</div>
<div>
    <asp:GridView ID="grdSQL" runat="server" Width="100%" >
        <RowStyle HorizontalAlign="Center" CssClass="tItem" />
        <PagerStyle CssClass="tPage" />
        <HeaderStyle CssClass="tHeader" />
        <AlternatingRowStyle CssClass="tAlter" />
        <SelectedRowStyle BackColor="#F1F5FB" />
    </asp:GridView>
     <div class="gridNav">
        <div class="left">
            Current Page：<asp:Label ID="current" runat="server" />&nbsp; Total Page：<asp:Label ID="total" runat="server" />
        </div>
        <div class="right">
            <asp:LinkButton ID="F" runat="server" OnClick="Button1_Click">[ |< ]</asp:LinkButton>&nbsp;
            <asp:LinkButton ID="P" runat="server" OnClick="Button3_Click">[ < ]</asp:LinkButton>&nbsp;
            <asp:LinkButton ID="N" runat="server" OnClick="Button2_Click">[ > ]</asp:LinkButton>&nbsp;
            <asp:LinkButton ID="L" runat="server" OnClick="Button4_Click">[ >| ]</asp:LinkButton>
        </div>
    </div>
</div>
    <input type="hidden" id="whereSQL" value="" runat="server"/>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
<label id="m" runat="server">
        </label>

</asp:Content>
