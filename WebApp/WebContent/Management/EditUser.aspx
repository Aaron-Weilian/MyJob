<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/Management.Master" AutoEventWireup="true"  CodeBehind="EditUser.aspx.cs" Inherits="WebApp.WebContent.Management.EditUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript">
// <![CDATA[
        function change(objID) {
            var obj = document.getElementById(objID);
            if ("Supplier" == obj.value) {
                var ret = window.showModalDialog("SupplierList.aspx", '', 'dialogHeight:400px;dialogWidth:700px;status:0');
                alert("The user supplier information changed,will can not receive pre-suplier message, need manager to audit .");
                if (ret != null) {
                    document.getElementById("supplierName").value = ret[1];
                    document.getElementById("supplierNum").value = ret[0];
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
                <input id="UserName" type="text" name="UserName" runat="server" />
            </div>
            <div class="span-6 last">
                <label for="UsersType" class="span-3">User Type * :</label>
                <asp:DropDownList ID="UsersType" runat="server" Enabled="false" >
                    <asp:ListItem>Admin</asp:ListItem>
                    <asp:ListItem>Supplier</asp:ListItem>
                    <asp:ListItem>Buyer</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="span-10">
                <label class="span-3">Email *:</label>
                <input id="Email" type="text" name="Email" runat="server" />
            </div>
            <div class="span-10 last">
                <label class="span-3" for="Phone">Phone :</label>
                <input id="Phone" type="text" name="Phone" runat="server" />
            </div>
            <div style="clear: both" class="span-14">
                <label for="supplierName" class="span-3">Supplier *:</label>
                <input type="text"    readonly  runat="server" id="supplierName" size="60" />
                <input type="hidden"   runat="server" id="supplierNum"  />
            </div>
            <div class="last span-5">
                <asp:Button ID="init" CssClass="button" Text="Init Password" runat="server" OnClick="init_Click" />
               
            </div>
            <div style="clear: both">
                <label for="Discription">Discription :</label><br/>
                <textarea type="text" id="Discription" name="Discription" rows="3" cols="5" runat="server"></textarea>
            </div>
        </fieldset>
        <asp:LinkButton ID="back"  runat="server" OnClick="back_Click" > <img alt="" style="border:0px; margin-top:-10px; " src="../../Styles/Resource/b9.JPG" />BACK </asp:LinkButton>
        <asp:LinkButton ID="save" runat="server"  OnClick="save_Click" > <img alt="" style="border:0px; margin-top:-10px; " src="../../Styles/Resource/b9.JPG" />SAVE </asp:LinkButton>
        <input id="UserID" type="hidden" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
     <label id="Label1" runat="server">
        </label>
         <label id="mes" style="color: red;" runat="server"/>
</asp:Content>
