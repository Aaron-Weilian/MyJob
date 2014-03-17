<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierList.aspx.cs" Inherits="WebApp.WebContent.Buyer.SupplierList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Supplier Select</title>
    <base target="_self" />
    <link href="~/Styles/blueprint/screen.css" rel="stylesheet" type="text/css" media="screen, projection"/>
    <link href="~/Styles/blueprint/plugins/buttons/screen.css" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="~/Styles/blueprint/main.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
// <![CDATA[
         function Get() {
             var chkObjs = document.getElementsByName("SupplierID");
             for (var i = 0; i < chkObjs.length; i++) {
                 if (chkObjs[i].checked) {
                     var arr = new Array();
                     arr = chkObjs[i].value.split('~');
                     window.returnValue = arr;
                     break;
                 }
             }
             window.close();  
         }
// ]]>
    </script>
</head>
<body>
<div class="container" style="width: 700px">
    <form id="form1" runat="server">
        <div class="span-10 last">
            <label for="suppliername">Supplier Name</label>
            <input name="suppliername" type="text" />
            <asp:LinkButton ID="Search"   runat="server" OnClick="Search_Click" >
                    <img alt="" style="border:0px; margin-top:-10px; "  src='<%=ResolveUrl("~/Styles/Resource/b9.JPG") %>' /> Search
            </asp:LinkButton>
        </div>
        <asp:Repeater runat="server" ID="supplierList">
            <HeaderTemplate>
            <table border="0" cellspacing="0" cellpadding="0" style="table-layout:fixed; width: 700px;">
                <thead>
                <tr>
                    <th style="width: 500px">Supplier Name</th>
                    <th style="width: 60px">DUNS</th>
                    <th style="width: 30px">Status</th>
                    <th style="width: 10px;"></th>
                </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("SupplierName") %></td>
                <td><%# Eval("DUNS") %></td>
                <td><%# Eval("Status")%></td>
                <td><input type ="radio" name="SupplierID" onclick="Get()"  value='<%# Eval("SupplierNum")+"~"+Eval("SupplierName") %>' /></td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="even">
                <td><%# Eval("SupplierName") %></td>
                <td><%# Eval("DUNS") %></td>
                <td><%# Eval("Status")%></td>
                <td><input type ="radio" name="SupplierID" onclick="Get()"  value='<%# Eval("SupplierNum")+"~"+Eval("SupplierName") %>' /></td>
            </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
            </tbody>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <div class="gridNav" style="width: 680px;">
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
    </form>
</div>
</body>
</html>
