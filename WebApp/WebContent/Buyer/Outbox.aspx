<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/Buyer.Master" AutoEventWireup="true" CodeBehind="Outbox.aspx.cs" Inherits="WebApp.WebContent.Buyer.Outbox" %>
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
        <input type="text" name="reference" value="<%=Request["reference"] %>"  size="80" />
    </div>
    <div class="span-7 right">
        <asp:LinkButton ID="search" runat="server"  OnClick="search_Click"  >
            <img alt="" style="border:0px; margin-top:-10px; " src='<%=ResolveUrl("~/Styles/Resource/b9.JPG")%>' /> Search
        </asp:LinkButton>
    </div>
    <asp:Repeater runat="server" ID="MessageList" >
        <HeaderTemplate>
            <table border="0" cellspacing="0" cellpadding="0">
                <caption>Outbox</caption>
                <thead>
                <tr>
                    <th class="span-3">Message Type</th>
                    <th class="span-7">Message Reference</th>
                    <th class="span-5">Supplier Name</th>
                    <th class="span-3">Supplier Site</th>
                    <th class="span-3">CreationDate</th>
                    <th >Status</th>
                </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("MessageType") %></td>
                <td><%# Eval("MessageName") %></td>
                <td><%# Eval("vender_name")%></td>
                <td><%# Eval("vender_site")%></td>
                <td><%# Eval("CreationDateTime") %></td>
                <td><%# Eval("NOTES").Equals("Creation")?"Success":"Fail" %></td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="even">
                <td><%# Eval("MessageType") %></td>
                <td><%# Eval("MessageName") %></td>
                <td><%# Eval("vender_name")%></td>
                <td><%# Eval("vender_site")%></td>
                <td><%# Eval("CreationDateTime") %></td>
                <td><%# Eval("NOTES").Equals("Creation")?"Success":"Fail" %></td>
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