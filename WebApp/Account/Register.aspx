<%@ Page Title="Register" Language="C#" MasterPageFile="~/Templates/Main.Master" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="WebApp.Account.Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <script language="javascript" type="text/javascript">
// <![CDATA[

        function change(objID) {
            var obj = document.getElementById(objID);
            if ("Supplier" == obj.value) {

                var ret = window.showModalDialog("SupplierList.aspx", '', 'dialogHeight:400px;dialogWidth:700px;status:0');
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
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="padding-top: 20px; padding-left: 15px; text-align: left;">
        <b>User Detail</b>
        <hr style="width: 500px; padding-left: 15px;" />
        User Name * :&nbsp;&nbsp;
        <input type="text" name="UserName" value="<%=Request["UserName"]%>" />
       
        <br />
        <br />
        User Type * :&nbsp;&nbsp;&nbsp;&nbsp;

                <asp:DropDownList ID="UsersType" runat="server" >
                        <asp:ListItem>--</asp:ListItem>
                        <asp:ListItem>Supplier</asp:ListItem>
                 </asp:DropDownList>

        
        <br />
        <br />
        <div  >
            Supplier *:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <input type="text" name="supplierName" readonly="readonly" value="<%=Request["supplierName"]%>" id="supplierName"
                size="80" />
            <input type="hidden" id="supplierNum" name="supplierNum" />
            <br />
            <br />
        </div>
        Email *:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input
            type="text" name="Email" value="<%=Request["Email"]%>" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        Password Mode <input type="radio" ID="D" runat="server" checked  name="mode" value="D" />Generate Default Password
        <br />
        <br />
        Phone :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="text"
            name="Phone" value="<%=Request["Phone"]%>" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <input type="radio" ID="G" runat="server" name="mode" value="G" />Random Generate Password<br />
        <br />
        Discription :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="text"
            name="Discription" value="<%=Request["Discription"]%>" /> 
        <br />
        <br />
        <hr />

       

        <asp:LinkButton ID="save" runat="server"  OnClick="save_Click" >
                <img alt="" style="border:0px; margin-top:-10px; " src='<%=ResolveUrl("~/Styles/Resource/b9.JPG") %>' /> SAVE
        </asp:LinkButton>


        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
     <label id="mes" runat="server">
     </label>
     
</asp:Content>