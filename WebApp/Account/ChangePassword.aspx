<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Templates/Main.master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="WebApp.Account.ChangePassword" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script language="javascript" type="text/javascript">

     
    
    </script>

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        <span style="color:Black">First Login Please Change Password </span> 
    </h2>
    
        <div style="float:left;">
           
            <div style="float:left;" >
                <fieldset class="changePassword">
                    <legend>Account Information</legend>
                    <p>
                        <asp:Label ID="CurrentPasswordLabel"  runat="server" AssociatedControlID="CurrentPassword">Old Password:</asp:Label>
                        <asp:TextBox ID="CurrentPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword" 
                             CssClass="failureNotification" ErrorMessage="Old password is required." ToolTip="Old Password is required." 
                             ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                   
                         <asp:CustomValidator ID="ValidatorOldPassword"   runat="server" 
                            ErrorMessage="Old password is invalid" ControlToValidate="CurrentPassword" 
                              OnServerValidate="CustomValidator_ServerValidate" ValidationGroup="ChangeUserPasswordValidationGroup" 
                             >*</asp:CustomValidator>
                   
                   
                   
                    </p>
                    <p>
                        <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">New Password:</asp:Label>
                        <asp:TextBox ID="NewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" 
                             CssClass="failureNotification" ErrorMessage="New Password is required." ToolTip="New Password is required." 
                             ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                        

                    </p>
                    <p>
                        <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Confirm New Password:</asp:Label>
                        <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        
                        <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" 
                             CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirm New Password is required."
                             ToolTip="Confirm New Password is required." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" 
                             CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry."
                             ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CompareValidator>                  
                         
                        <asp:CustomValidator ID="CustomValidator1"   runat="server" 
                            ErrorMessage="New password must be composed of numbers and letters that contains upper and lower case. " ControlToValidate="ConfirmNewPassword" 
                             OnServerValidate="ValidationPasswordValid" ValidationGroup="ChangeUserPasswordValidationGroup" 
                            >*</asp:CustomValidator>

                         <asp:CustomValidator ID="CustomValidator2"   runat="server" 
                            ErrorMessage="New password of at least eight letters " ControlToValidate="ConfirmNewPassword" 
                             OnServerValidate="ValidationPasswordLength" ValidationGroup="ChangeUserPasswordValidationGroup" 
                            >*</asp:CustomValidator>

                    </p>
                </fieldset>
                <br />
                <p class="submitButton">
                    <asp:LinkButton ID="CancelPushButton"   runat="server" CommandName="Cancel" CausesValidation="False" >
                        <img alt="" style="border:0px; margin-top:-10px; "  src='<%=ResolveUrl("~/Styles/Resource/b9.JPG")%>' /> Cancel
                   </asp:LinkButton>
                   
                   <asp:LinkButton ID="ChangePasswordPushButton" runat="server" ValidationGroup="ChangeUserPasswordValidationGroup" OnClick="ChangeUserPassword" >
                        <img alt="" style="border:0px; margin-top:-10px; "  src='<%=ResolveUrl("~/Styles/Resource/b9.JPG")%>' /> Change Password
                   </asp:LinkButton>
                </p>
            </div>

            <div >
             <asp:ValidationSummary ID="ChangeUserPasswordValidationSummary" runat="server" CssClass="failureNotification" 
                 ValidationGroup="ChangeUserPasswordValidationGroup"/>
            
            
            </div>
            
            </div>
          
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
    <center>
                    <label id="mes" runat="server" ></label>
                    <asp:HyperLink  ID="Login" runat="server" >Return Login Page</asp:HyperLink>
                </center>
</asp:Content>