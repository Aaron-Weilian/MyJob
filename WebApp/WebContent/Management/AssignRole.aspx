<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/Main.Master" AutoEventWireup="true" CodeBehind="AssignRole.aspx.cs" Inherits="WebApp.WebContent.Management.AssignRole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <fieldset>
            <legend>Assign Role</legend>
            <asp:Repeater runat="server" ID="userList">
                <HeaderTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" >
                        <caption>Roles</caption>
                        <thead>
                            <tr>
                                <th class="span-4">Role Name</th>
                                <th>Discription</th>
                                <th class="span-2">CreationDate</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("RoleName") %></td>
                        <td><%# Eval("Discription") %></td>
                        <td><%# Eval("Created") %></td>
                        <td><input type ="radio" name="RoleNum" title="<%# Eval("RoleNum") %>"  value='<%# Eval("RoleNum") %>' /></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="even">
                        <td><%# Eval("RoleName") %></td>
                        <td><%# Eval("Discription") %></td>
                        <td><%# Eval("Created") %></td>
                        <td><input type ="radio" name="RoleNum" title="<%# Eval("RoleNum") %>"  value='<%# Eval("RoleNum") %>' /></td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="left">Current Page：<asp:Label ID="current" runat="server" />&nbsp; Total Page：<asp:Label ID="total" runat="server" /></div>
            <div class="right">
                <asp:LinkButton ID="F" runat="server" OnClick="Button1_Click">[ |< ]</asp:LinkButton>&nbsp;
                <asp:LinkButton ID="P" runat="server" OnClick="Button3_Click">[ < ]</asp:LinkButton>&nbsp;
                <asp:LinkButton ID="N" runat="server" OnClick="Button2_Click">[ > ]</asp:LinkButton>&nbsp;
                <asp:LinkButton ID="L" runat="server" OnClick="Button4_Click">[ >| ]</asp:LinkButton>
            </div>
        </fieldset>
            <asp:LinkButton ID="back" runat="server" OnClick="back_Click" >
                <img alt="" style="border:0px; margin-top:-10px; " src="../../Styles/Resource/b9.JPG" /> Back
            </asp:LinkButton>
            <asp:LinkButton ID="role" runat="server" OnClick="role_Click" >
                <img alt="" style="border:0px; margin-top:-10px; " src="../../Styles/Resource/b9.JPG" /> Assign Role
            </asp:LinkButton>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
     <label id="mes" runat="server">
        </label>
</asp:Content>
