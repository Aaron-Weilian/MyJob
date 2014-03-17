<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/Management.Master" AutoEventWireup="true" CodeBehind="MonitorFile.aspx.cs" Inherits="WebApp.WebContent.Management.MonitorFile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <asp:Button CssClass="button" runat="server" ID="btnInbound" Text="InBound" OnClick="btnInbound_Click"/> &nbsp;&nbsp; <asp:Button CssClass="button" runat="server" ID="btnOutbound" Text="OutBound" OnClick="btnOutbound_Click"/> &nbsp;&nbsp;<asp:Button CssClass="button" runat="server" ID="btnBackup" Text="Backup" OnClick="btnBackup_Click"/>&nbsp;&nbsp;<asp:FileUpload ID="fileUpload" Width="500px" runat="server" CssClass="button" /><asp:TextBox runat="server" ID="txgPath"/><asp:Button runat="server" ID="btnUpgradeWeb" Text="Upgrade web" CssClass="button" OnClick="btnUpgradeWeb_Click"/>
        <asp:Button runat="server" ID="createFolder" Text="Create Folder" CssClass="button" OnClick="createFolder_Click"/>
    </div>

            <table border="0" cellspacing="0" cellpadding="0">
                <caption><asp:Literal runat="server" Text="" ID="litCurrentPath"></asp:Literal></caption>
                 <thead>
                    <tr>
                        <th class="span-14">File Name</th>
                        <th class="span-4">LastWrite Time</th>
                        <th class="span-4">Creation Time</th>
                        <th class="span-3"></th>
                    </tr>
                </thead>
                <tbody>
    <asp:Repeater runat="server" ID="listFolder" OnItemCommand="Dir_ItemCommand">
        <HeaderTemplate>
            <tr>
                <td colspan="3"><asp:LinkButton runat="server" Text='..'/></td>
            </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td colspan="3"><asp:LinkButton runat="server" Text='<%# Eval("Name")%>'/></td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr>
                <td colspan="3"><asp:LinkButton runat="server" Text='<%# Eval("Name")%>'/></td>
            </tr>
        </AlternatingItemTemplate>
    </asp:Repeater>
    <asp:Repeater runat="server" ID="FileList">
        <ItemTemplate>
            <tr>
                <td><asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("Name")%>'  OnClick="Name_Click"><%# Eval("Name")%></asp:LinkButton></td>
                <td><%# Eval("LastWriteTime")%></td>
                <td><%# Eval("CreationTime")%></td>
                <td><asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Eval("Name")%>'  OnClick="Delete_Click">Delete</asp:LinkButton></td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="even">
                <td><asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("Name")%>'  OnClick="Name_Click"><%# Eval("Name")%></asp:LinkButton></td>
                <td><%# Eval("LastWriteTime")%></td>
                <td><%# Eval("CreationTime")%></td>
                <td><asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Eval("Name")%>'  OnClick="Delete_Click">Delete</asp:LinkButton></td>
            </tr>
        </AlternatingItemTemplate>
    </asp:Repeater>
            </tbody>
          </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
    <asp:Literal runat="server" ID="literalMsg"></asp:Literal>
</asp:Content>
