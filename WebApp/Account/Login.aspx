<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Templates/Login.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="WebApp.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="span-24 top" style="height: 100px">&nbsp;</div>
    <div class="span-24" style="height: 400px">
        <div class="span-7 first">&nbsp;</div>
        <div class="span-8 loginForm" >
            <div class="header">Login bmc ic portal</div>
            <div style="margin-top: 15px; margin-left:45px; ">
                <div>
                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
                </div>
                <div>
                    <asp:TextBox ID="UserName" runat="server" CssClass="title"></asp:TextBox><asp:RequiredFieldValidator
                        ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                        ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                </div>
                <div>
                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                </div>
                <div>
                    <asp:TextBox ID="Password" runat="server" CssClass="title" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator
                        ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required."
                        ToolTip="Password is required." ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                </div>
                <div class="bottom">
                    <div class="last right" style="padding-right:40px">
                        <asp:Button CssClass="button" ID="LoginButton" runat="server" OnClick="LoginSystem"  ValidationGroup="LoginUserValidationGroup" Text="Login" runat="server"/>
                    </div>
                </div>
            </div>
            <div class="last" style=" margin-left:45px; width:220px;">
                <asp:Label ID="mes" runat="server" ></asp:Label>
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="error" ValidationGroup="LoginUserValidationGroup"/>
            </div>
        </div>
        <div class="last">&nbsp;</div>
    </div>
<script language="javascript">
    document.getElementById("MainContent_UserName").focus();
</script>
</asp:Content>
