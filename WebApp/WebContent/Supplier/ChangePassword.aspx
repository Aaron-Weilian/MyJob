<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Templates/Supplier.Master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="WebApp.WebContent.Supplier.ChangePassword" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"/>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <div class="container">
        <fieldset>
            <legend>Change Password</legend>
            <div class="span-15">
                <label class="span-5" for="">Current Password:</label>
                <asp:TextBox ID="CurrentPassword" runat="server" CssClass="" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword"
                    CssClass="failureNotification" ErrorMessage="Old password is required." ToolTip="Old Password is required."
                    ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                <asp:CustomValidator ID="ValidatorOldPassword" runat="server" ErrorMessage="Old password is invalid"
                    ControlToValidate="CurrentPassword" OnServerValidate="CustomValidator_ServerValidate"
                    ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CustomValidator>
            </div>
            <div class="span-15">
                <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword" CssClass="span-5">New Password:</asp:Label>
                <asp:TextBox ID="NewPassword" runat="server" CssClass="" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword"
                    CssClass="failureNotification" ErrorMessage="New Password is required." ToolTip="New Password is required."
                    ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
            </div>
            <div class="span-15">
                <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword" CssClass="span-5">Confirm New Password:</asp:Label>
                <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword"
                    CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirm New Password is required."
                    ToolTip="Confirm New Password is required." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword"
                    ControlToValidate="ConfirmNewPassword" CssClass="failureNotification" Display="Dynamic"
                    ErrorMessage="The Confirm New Password must match the New Password entry." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CompareValidator>
                <asp:CustomValidator ID="CustomValidator" runat="server" ErrorMessage="New password of at least eight letters"
                    ControlToValidate="ConfirmNewPassword" OnServerValidate="ValidationPasswordLength"
                    ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CustomValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="New password must be composed of numbers and letters that contains upper and lower case. "
                    ControlToValidate="ConfirmNewPassword" OnServerValidate="ValidationPasswordValid"
                    ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CustomValidator>
            </div>
        </fieldset>
    <asp:LinkButton ID="CancelPushButton" runat="server" CommandName="Cancel" CausesValidation="False">
        <img alt="" style="border:0px; margin-top:-10px; "  src='<%=ResolveUrl("~/Styles/Resource/b9.JPG")%>' /> Cancel 
    </asp:LinkButton>&nbsp;&nbsp;
    <asp:LinkButton ID="ChangePasswordPushButton" runat="server" ValidationGroup="ChangeUserPasswordValidationGroup"
        OnClick="ChangeUserPassword">
        <img alt="" style="border:0px; margin-top:-10px; "  src='<%=ResolveUrl("~/Styles/Resource/b9.JPG")%>' /> Change Password
    </asp:LinkButton>
    <asp:ValidationSummary ID="ChangeUserPasswordValidationSummary" runat="server" CssClass="failureNotification"
        ValidationGroup="ChangeUserPasswordValidationGroup" />
    <label id="mes" runat="server"/>
    </div>
</asp:Content>
