<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/Main.Master" AutoEventWireup="true" CodeBehind="SelectSite.aspx.cs" Inherits="WebContent.Supplier.SelectSite" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"/>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container">
        <fieldset>
            <legend>Supplier User Please Select Supplier Site :</legend>
            <div class="span-15">
                <label class="span-5" for="">Supplier Site Name :</label>
                <asp:DropDownList runat="server" ID="siteList" ></asp:DropDownList>
            </div>
            <div class="span-15">
                <asp:LinkButton  ID="login"  runat="server"  OnClick="login_Click"  >
                   <img alt="" style="border:0px; "  src="../../Styles/Resource/b9.JPG" /> Confirm
                </asp:LinkButton>
            </div>
            <div class="span-15"><asp:Label ID="lab" runat="server" Visible="false"></asp:Label></div>
        </fieldset>
     </div>
</asp:Content>
