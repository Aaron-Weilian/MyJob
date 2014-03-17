<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Templates/Management.Master" CodeBehind="LogTrace.aspx.cs" Inherits="WebApp.WebContent.Management.LogTrace" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="span-6">
        <label for="FromDate">From Date</label>
        <input name="FromDate" type="text" id="FromDate" onclick="new Calendar().show(this);" readonly="readonly" />
    </div>
    <div class="span-6">
        <label for="ToDate">To Date</label>
        <input name="ToDate" type="text" id="ToDate" onclick="new Calendar().show(this);" readonly="readonly" />
    </div>
     <div class="span-6">
        <label for="EvenName">EvenName</label>
        <input name="EvenName" type="text"   />
    </div>
    <div class="span-6">
        <label for="Logger">Logger</label>
        <input name="Logger" type="text"   />
    </div>
    <div class="span-4">
        <label>Log Level</label>
        <select name="loglevel">
            <option >All</option>
            <option value="Debug">DEBUG</option>
            <option value="Info">INFO</option>
            <option value="Warn">WARN</option>
            <option value="Error">ERROR</option>
            <option value="Fault">FAULT</option>
        </select>
    </div>
    <div class="last">
        <asp:LinkButton ID="Search" runat="server" OnClick="Search_Click" Text="Search" />
    </div>
    <asp:Repeater runat="server" ID="logList">
        <HeaderTemplate>
            <table border="0" cellspacing="0" cellpadding="0">
                <caption>Users</caption>
                <thead>
                    <tr>
                        <th class="span-2">Date</th>
                        <th class="span-1">Level</th>
                        <th class="span-3">EventName</th>
                        <th class="span-5">Message</th>
                        <th class="span-9">Exception</th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("Date", "{0:yyyy-MM-dd HH:mm:ss}")%></td>
                <td><%# Eval("Level") %></td>
                <td><%# Eval("EvenName")%></td>
                <td><%# Eval("Message")%></td>
                <td><%# Eval("Exception")%></td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="even">
                <td><%# Eval("Date","{0:yyyy-MM-dd HH:mm:ss}") %></td>
                <td><%# Eval("Level") %></td>
                <td><%# Eval("EvenName")%></td>
                <td><%# Eval("Message")%></td>
                <td><%# Eval("Exception")%></td>
            </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
            </tbody>
          </table>
        </FooterTemplate>
    </asp:Repeater>
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
    <input type="hidden" id="whereSQL" value="1=1" runat="server"/>
</asp:Content>
