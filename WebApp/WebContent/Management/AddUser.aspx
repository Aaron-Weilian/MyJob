<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/Management.Master" AutoEventWireup="true"
    CodeBehind="AddUser.aspx.cs" Inherits="WebApp.WebContent.Management.AddUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript">
// <![CDATA[
        function change(objID) {
            var obj = document.getElementById(objID);
            if ("Supplier" == obj.value) {

                var ret = window.showModalDialog("SupplierList.aspx", '', 'dialogHeight:400px;dialogWidth:700px;status:0');
                if (ret != null) {

                    document.getElementById("supplierName").value = ret[1];
                    document.getElementById("supplierNum").value = ret[0];
                    alert("Note the supplier information cannot be modified after save");
                }
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
    <div class="container">
        <fieldset>
            <legend>User Details</legend>
            <div class="span-10">
                <label for="UserName" class="span-3">User Name * :</label>
                <input type="text" name="UserName" value="<%=Request["UserName"]%>" />
            </div>
            <div class="span-10 last">
                <label for="UsersType" class="span-3">User Type * :</label>
                <asp:DropDownList ID="UsersType" runat="server" >
                        <asp:ListItem>Admin</asp:ListItem>
                        <asp:ListItem>Supplier</asp:ListItem>
                        <asp:ListItem>Buyer</asp:ListItem>
                 </asp:DropDownList>
            </div>
            <div class="span-10">
                <label class="span-3">Email *:</label>
                <input type="text" name="Email" value="<%=Request["Email"]%>" />
            </div>
            <div class="span-10 last">
                <label for=" G" class="span-3">Password Mode</label>
                <input type="radio" ID="D" runat="server" name="mode"  checked />Default
                <input type="radio" ID="G" runat="server" name="mode"/>Auto
            </div>
            <div style="clear: both">
                <label class="span-3" for="Phone">Phone :</label>
                <input type="text"  name="Phone" value="<%=Request["Phone"]%>" /> 
            </div>
            <div style="clear: both">
                <label for="supplierName" class="span-3">Supplier *:</label>
                <input type="text" name="supplierName" readonly="readonly"  id="supplierName" size="70" />
            </div>
            <div style="clear: both">
                <label for="Discription">Discription :</label><br/>
                <textarea type="text" name="Discription" rows="3" cols="4"><%=Request["Discription"]%></textarea>
            </div>
        </fieldset>
        <input type="hidden" id="supplierNum" name="supplierNum" />
        <asp:LinkButton ID="back"  runat="server" OnClick="back_Click" >
                <img alt="" style="border:0px; margin-top:-10px; " src='<%=ResolveUrl("~/Styles/Resource/b9.JPG") %>' /> BACK
        </asp:LinkButton>

        <asp:LinkButton ID="save" runat="server"  OnClick="save_Click" >
                <img alt="" style="border:0px; margin-top:-10px; " src='<%=ResolveUrl("~/Styles/Resource/b9.JPG") %>' /> SAVE
        </asp:LinkButton>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
     <label id="mes" runat="server">
        </label>
</asp:Content>