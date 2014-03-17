<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierList.aspx.cs" Inherits="WebApp.Account.SupplierList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self" />
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />

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
<body >
<center>
    <form id="form1" runat="server">
    <div  class="selectList" >
     <table width="650" style="text-align:left;">
            <tr>
                <td align="left" style="width:100px;">
                    Supplier Name :
                </td>
                <td align="left" colspan="2">
                    <input name="suppliername" type="text" />
                </td>
                <td align="right" valign="bottom" >
                    <asp:Button ID="Search" CssClass="btn2" Text="Search" runat="server" onclick="Search_Click" />
                </td>
            </tr>

    </table>
    <hr />
   <asp:DataList runat="server" ID="supplierList">
        <HeaderTemplate>
            <table border="0" cellpadding="1" cellspacing="1" style="text-align:left;"  >
                <tr class="tableHeader" >
                    <td style="width:400px" >
                        Supplier Name
                    </td>
                    <td style="width:100px">
                        DUNS
                    </td>
                    <td style="width:100px">
                        Status
                    </td>
                    <td style="width:50px">
                        
                    </td>
                </tr>
            </table>
        </HeaderTemplate>
        <ItemTemplate>
            <table border="0" cellpadding="1" cellspacing="1" style="text-align:left;" >
                <tr style="background-color:#f9f9f9" >
                    <td style="width:400px">
                        <asp:Label ID="SupplierName" runat="server" Text='<%# Eval("SupplierName") %>'></asp:Label>
                    </td>
                    <td style="width:100px">
                        <asp:Label ID="DUNS" runat="server" Text='<%# Eval("DUNS") %>'></asp:Label>
                    </td>
                     <td style="width:100px">
                        <asp:Label ID="Status" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                    </td>
                     <td style="width:50px">
                       
                        <input type ="radio" name="SupplierID" onclick="Get()"  value='<%# Eval("SupplierNum")+"~"+Eval("SupplierName") %>' />
                       
                      
                    </td>
                </tr>
               
            </table>
        </ItemTemplate>
    </asp:DataList>
    <table width="660px" class="tableHeader">
     <tr align="center">
                    <td >

                    Current Page：<asp:Label ID="current" runat="server" ></asp:Label>&nbsp;

                    Total Page：<asp:Label ID="total" runat="server" ></asp:Label> &nbsp;
                    </td>
                    <td>
                    <asp:LinkButton ID="F" runat="server" OnClick="Button1_Click" >[ |< ]</asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="P" runat="server" OnClick="Button3_Click" >[ < ]</asp:LinkButton>&nbsp;
                   
                    <asp:LinkButton ID="N" runat="server" OnClick="Button2_Click" >[ > ]</asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="L" runat="server" OnClick="Button4_Click" >[ >| ]</asp:LinkButton>
       
                    </td>
                </tr>
                </table>
             <hr />
    </div>
    </form>
</center>
</body>
</html>
