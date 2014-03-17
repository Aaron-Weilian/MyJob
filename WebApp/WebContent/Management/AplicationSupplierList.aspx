<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/Management.Master" AutoEventWireup="true" CodeBehind="AplicationSupplierList.aspx.cs" Inherits="WebApp.WebContent.Management.AplicationSupplierList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <script language="javascript" type="text/javascript">
// <![CDATA[


// ]]>
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div class="left span-8">
        <label for="suppliername" style="margin-right:50px">Supplier Name: </label>
          <input name="suppliername" value="<%=Request["suppliername"]%>" type="text" />
    </div>
    <div class="left span-6">
        <label for="duns">DUNS: </label>
        <input name="duns" value="<%=Request["duns"]%>" type="text" />
    </div>
    <div class="last span-8">
        <label for="suppliersite">Supplier Site</label>
        <input name="suppliersite" value="<%=Request["suppliersite"]%>"  type="text" />
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="Search_Click" Text="Search" />
    </div>

     <div class="right">
         <asp:LinkButton ID="cancel" runat="server" OnClick="disable_Click" >
                <img alt="" style="border:0px; margin-top:-10px; " src="../../Styles/Resource/b9.JPG" /> Cancel
         </asp:LinkButton>
         <asp:LinkButton ID="enable"  runat="server" OnClick="enable_Click" >
                <img alt="" style="border:0px; margin-top:-10px; " src="../../Styles/Resource/b9.JPG" /> Pass
         </asp:LinkButton>
    </div>
     
     <asp:Repeater runat="server" ID="supplierList" >
        <HeaderTemplate>
        <table border="0" cellspacing="0" cellpadding="0">
            <caption>Users</caption>
            <thead>
                <tr>
                    <th class="span-10">Supplier Name</th>
                    <th class="span-3">DUNS</th>
                    <th class="span-6">Supplier Site</th>
                    <th class="span-3">Date Modified</th>
                    <th class="span-1">Status</th>
                    <th ></th>
                </tr>
            </thead>
            <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("SupplierName")%></td>
                <td><%# Eval("DUNS")%></td>
                <td><%# Eval("SiteName")%></td>
                <td><%# Eval("Updated")%></td>
                <td><%# Eval("Status")%></td>
                <td>
                    <input type ="radio" name="SupplierID" title="<%# Eval("Status") %>" onclick="button(this)" value='<%# Eval("SupplierID") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="even">
                <td><%# Eval("SupplierName")%></td>
                <td><%# Eval("DUNS")%></td>
                <td><%# Eval("SiteName")%></td>
                <td><%# Eval("Updated")%></td>
                <td><%# Eval("Status")%></td>
                <td>
                    <input type ="radio" name="SupplierID" title="<%# Eval("Status") %>" onclick="button(this)" value='<%# Eval("SupplierID") %>' />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
            </tbody>
        </table>
        </FooterTemplate>
    </asp:Repeater>

    <div class="gridNav">
        <div class="left">Current Page：<asp:Label ID="current" runat="server" />&nbsp; Total Page：<asp:Label ID="total" runat="server" /></div>
        <div class="right">
            <asp:LinkButton ID="F" runat="server" OnClick="Button1_Click">[ |< ]</asp:LinkButton>&nbsp;
            <asp:LinkButton ID="P" runat="server" OnClick="Button3_Click">[ < ]</asp:LinkButton>&nbsp;
            <asp:LinkButton ID="N" runat="server" OnClick="Button2_Click">[ > ]</asp:LinkButton>&nbsp;
            <asp:LinkButton ID="L" runat="server" OnClick="Button4_Click">[ >| ]</asp:LinkButton>
        </div>
    </div>
      
    


</asp:Content>
