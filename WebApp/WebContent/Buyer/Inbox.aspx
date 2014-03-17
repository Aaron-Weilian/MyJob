<%@ Page Title="" Language="C#" enableEventValidation="true" MasterPageFile="~/Templates/Buyer.Master" AutoEventWireup="true" CodeBehind="Inbox.aspx.cs" Inherits="WebApp.WebContent.Buyer.Inbox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript"  src="../../Scripts/setday.js" language="javascript" ></script>
    <script language="javascript" type="text/javascript">
// <![CDATA[
        function Select() {
            var ret = window.showModalDialog("SupplierList.aspx", '', 'dialogHeight:400px;dialogWidth:700px');
            if (ret != null) {
                document.getElementById("supplierName").value = ret[1];
                document.getElementById("supplierNum").value = ret[0];
            }
            else {
                document.getElementById("supplierName").value = "--";
                document.getElementById("supplierNum").value = "";
            }
        }
// ]]>
    </script>
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
    <div class="span-6 last">
        <label for="">Message Type</label>
            <select name="messagetype">
                <option value="--">--</option>
                <option value="ROSConfirm">ROS Confirm</option>
                <option value="POConfirm">PO Confirm</option>
            </select>
    </div>
    <div class="span-14">
        <label for="reference">Message Reference</label>
        <input type="text" name="reference" value="<%=Request["reference"] %>"  size="60" />
    </div>
    <div class="span-7 last">
         <label for="supplierName">Supplier Name</label>
         <input type="text" id="supplierName" name="supplierName" value="<%=Request["supplierName"] %>" onclick="Select()" readonly="readonly" />
         <input type="hidden"   id="supplierNum" name="supplierNum" value="<%=Request["supplierNum"] %>"  />
     </div>
    <div class="right" style="margin-right: 20px; margin-top: 10px">
        <asp:LinkButton ID="search" runat="server"  OnClick="search_Click"  >
            <img alt="" style="border:0px; margin-top:-10px;" src='<%=ResolveUrl("~/Styles/Resource/b9.JPG")%>' /> Search
        </asp:LinkButton>
    </div>
    <asp:Repeater runat="server" ID="MessageList">
        <HeaderTemplate>
           <table border="0" cellspacing="0" cellpadding="0">
               <caption>Inbox</caption>
               <thead>
                <tr>
                    <th class="span-7">Message Reference</th>
                    <th class="span-5">Supplier Name</th>
                    <th class="span-3">Message Type</th>
                    <th class="span-3">Site Name</th>
                    <th class="span-4">ConfirmDate</th>
                    <th class="span-2">Status</th>
                    <th ></th>
                </tr>
               </thead>
               <tbody>
        </HeaderTemplate>
        <ItemTemplate >
            <tr>
                <td><%# Eval("MessageName") %></td>
                <td><%# Eval("Vender_Name") %></td>
                <td><%# Eval("MessageType").Equals("POConfirm") ? "PO Confirm" : Eval("MessageType").Equals("ROSConfirm")?"ROS Confirm": ""%></td>
                <td><%# Eval("Vender_Site") %></td>
                <td><%# Eval("CreationDatetime") %></td>
                <td><%# Eval("Segment3") %></td>
                <td><asp:LinkButton runat="server" ID='down'  CommandArgument='<%# Eval("segment5").ToString() +":"+ Eval("messageName").ToString() %>' OnClick="download_Click">Download</asp:LinkButton></td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="even">
                <td><%# Eval("MessageName") %></td>
                <td><%# Eval("Vender_Name") %></td>
                <td><%# Eval("MessageType").Equals("POConfirm") ? "PO Confirm" : Eval("MessageType").Equals("ROSConfirm")?"ROS Confirm": ""%></td>
                <td><%# Eval("Vender_Site") %></td>
                <td><%# Eval("CreationDatetime") %></td>
                <td><%# Eval("Segment3") %></td>
                <td><asp:LinkButton runat="server" ID='down'  CommandArgument='<%# Eval("segment5").ToString() +":"+ Eval("messageName").ToString() %>' OnClick="download_Click">Download</asp:LinkButton></td>
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