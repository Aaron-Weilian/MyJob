<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/Management.Master" AutoEventWireup="true"
    CodeBehind="AplicationUserList.aspx.cs" Inherits="WebApp.WebContent.Management.AplicationUserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 183px;
        }
        a{text-decoration: none;}
    </style>
    <script language="javascript" type="text/javascript">
// <![CDATA[

        function change(objID) {

           var obj = document.getElementById(objID);
           if ("Supplier" == obj.value) {

               var ret = window.showModalDialog("SupplierList.aspx", '', 'dialogHeight:400px;dialogWidth:700px;status:0');
                if (ret != null) {
                    document.getElementById("supplierName").value = ret[1];
                    document.getElementById("supplier").value = ret[0];

                  
                }
            }
            else {
                document.getElementById("supplierName").value = "--";
                document.getElementById("supplier").value = "";
            }
            
        }

        

     

// ]]>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="left span-7">
        <label for="UserName" style="margin-right: 26px">
            User Name:
        </label>
        <input name="UserName"  value="<%=Request["UserName"] %>" type="text" />
    </div>
    <div class="left span-5">
        <label for="UsersType">
            User Type:
        </label>
        <asp:DropDownList ID="UsersType" runat="server" >
                        <asp:ListItem>--</asp:ListItem>
                        <asp:ListItem>Supplier</asp:ListItem>
                 </asp:DropDownList>
    </div>
   
    <div class="span-17 last">
        <label for="supplierName">
            Supplier Name:</label>
       <input type="text" name="supplierName" readonly="readonly"   id="supplierName" size="80"   />
                <input type="hidden"   id="supplier"   />
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="Search_Click" Text="Search" />
    </div>
    <div class="right">
        <asp:LinkButton ID="cancel" runat="server" OnClick="disable_Click" >
                <img alt="" style="border:0px; margin-top:-10px; " src="../../Styles/Resource/b9.JPG" /> Cancel
             </asp:LinkButton>
        <asp:LinkButton ID="enable"  runat="server" OnClick="enable_Click" >
                <img alt="" style="border:0px; margin-top:-10px; " src="../../Styles/Resource/b9.JPG" /> Pass
            </asp:LinkButton>
        &nbsp;
    </div>
    
    <asp:Repeater runat="server" ID="userList">
        <HeaderTemplate>
            <table border="0" cellspacing="0" cellpadding="0">
                <caption>Users</caption>
                <thead>
                    <tr>
                        <th class="span-3">
                            User Name
                        </th>
                        <th class="span-2">
                            User Type
                        </th>
                        <th class="span-3">
                            User Role
                        </th>
                        <th class="span-8">
                            Supplier Name
                        </th>
                         <th class="span-3">
                            Email
                        </th>
                        <th class="span-3">
                            Date Modified
                        </th>
                        <th class="span-1">
                            Status
                        </th>
                        <th>
                        </th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%# Eval("UserName") %>
                </td>
                <td>
                    <%# Eval("UserType") %>
                </td>
                <td>
                    <%# Eval("RoleName") %>
                </td>
                <td>
                    <%# Eval("Supplier") %>
                </td>
                <td>
                    <%# Eval("Email") %>
                </td>
                <td>
                    <%# Eval("Updated") %>
                </td>
                <td>
                    <%# Eval("Status") %>
                </td>
                <td>
                   <input type ="radio" name="UserID" title="<%# Eval("Status") %>" onclick="button(this)" value='<%# Eval("UserID") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="even">
                <td>
                    <%# Eval("UserName") %>
                </td>
                <td>
                    <%# Eval("UserType") %>
                </td>
                <td>
                    <%# Eval("RoleName") %>
                </td>
                <td>
                    <%# Eval("Supplier") %>
                </td>
                <td>
                    <%# Eval("Email") %>
                </td>
                <td>
                    <%# Eval("Updated") %>
                </td>
                <td>
                    <%# Eval("Status") %>
                </td>
                <td>
                    <input type ="radio" name="UserID" title="<%# Eval("Status") %>" onclick="button(this)" value='<%# Eval("UserID") %>' />
                 </td>
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
     <label id="mes" runat="server">
        </label>
</asp:Content>
