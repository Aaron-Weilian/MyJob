<%@ Page Title="" Language="C#" enableEventValidation="true" MasterPageFile="~/Templates/Supplier.Master" AutoEventWireup="true" CodeBehind="Down.aspx.cs" Inherits="WebApp.WebContent.Supplier.Down" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript"  src="../../Scripts/setday.js" language="javascript" ></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="span-6">
        <label for="FromDate">From Date</label>
        <input name="FromDate" type="text" id="FromDate" value="<%=Request["FromDate"] %>" onclick="new Calendar().show(this);" readonly="readonly" />
    </div>
    <div class="span-6">
        <label for="ToDate">To Date</label>
        <input name="ToDate" type="text" id="ToDate" value="<%=Request["ToDate"] %>" onclick="new Calendar().show(this);" readonly="readonly" />
    </div>
    <div class="span-4">
        <label>Status</label>
        <select name="status">
            <option value="--">--</option>
            <option value="Read">Read</option>
            <option value="Unread">Unread</option>
        </select>
    </div>
    <div class="span-5 last">
        <label for="">Message Type</label>
            <select name="messagetype">
                <option value="--">--</option>
                <option value="NROS">NROS</option>
                <option value="PO">PO</option>
            </select>
    </div>
    <div class="span-17 left">
        <label for="reference">Message Reference</label>
        <input type="text" name="reference" value="<%=Request["reference"] %>" size="80" />
    </div>
    <div class="span-7 right">
        <asp:LinkButton ID="search" runat="server"  OnClick="search_Click"  >
            <img alt="" style="border:0px; margin-top:-10px; " src='<%=ResolveUrl("~/Styles/Resource/b9.JPG")%>' /> Search
        </asp:LinkButton>
    </div>
    <asp:Repeater runat="server" ID="MessageList" >
        <HeaderTemplate>
            <table border="0" cellspacing="0" cellpadding="0">
                <caption>Inbox</caption>
                <thead>
                <tr>
                    <th class="span-9">Message Reference</th>
                    <th class="span-3">Message Type</th>
                    <th class="span-5">Site Name</th>
                    <th class="span-4">Creation Date</th>
                    <th class="span-2">Status</th>
                    <th></th>
                </tr>
                </thead>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
            
                <%--<td><asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#Eval("MessageID")%>'  OnClick="Name_Click"><%# Eval("MessageName") %></asp:LinkButton></td>--%>
                <td><%# Eval("MessageName") %></td>
                <td><%# Eval("MessageType") %></td>
                <td><%# Eval("vender_site") %></td>
                <td><%# Eval("CreationDatetime") %></td>
                <td><%# Eval("Status") %></td>
                <td><asp:LinkButton runat="server" ID='down'  CommandArgument='<%#Eval("MessageID") %>' OnClick="download_Click">Download</asp:LinkButton></td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="even">
               <%-- <td><asp:LinkButton  runat="server" CommandArgument='<%#Eval("MessageID")%>'  OnClick="Name_Click"><%# Eval("MessageName") %></asp:LinkButton></td>
               --%>
                <td><%# Eval("MessageName") %></td>
                <td><%# Eval("MessageType") %></td>
                <td><%# Eval("vender_site") %></td>
                <td><%# Eval("CreationDatetime") %></td>
                <td><%# Eval("Status") %></td>
                <td><asp:LinkButton runat="server" ID='down'  CommandArgument='<%#Eval("MessageID") %>' OnClick="download_Click">Download</asp:LinkButton></td>
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
     <label id="mes" runat="server"/>
</asp:Content>