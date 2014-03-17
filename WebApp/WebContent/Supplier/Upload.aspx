<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/Supplier.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="WebApp.WebContent.Supplier.Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <fieldset>
            <legend>Upload Excel File</legend>
            <div>Please Select Message Type ,Then Upload Excel File.</div>
            <div class="span-10">
                <label for="type" class="span-3">Message Type:</label>
                <select id="type" runat="server">
                    <option value="ROSConfirm">ROS Confirm</option>
                    <option value="POConfirm">PO Confirm</option>
                </select>
            </div>
            <div class="span-10 last" style="display:none;">
                <label for="type" class="span-3">Site Name:</label>
                <asp:DropDownList runat="server" ID="siteList" ></asp:DropDownList>
            </div>
            <div class="span-20">
                <label for="type" class="span-3" style="padding-top: 10px;">Select File: </label>
                <asp:FileUpload ID="file" Width="500px" runat="server" CssClass="button" />
                <div style="padding-top: 15px;">
                <asp:LinkButton  ID="upload"  runat="server"  OnClick="upload_Click">
                    <img alt="" style="border:0px; margin-top:-10px; " src='<%=ResolveUrl("../../Styles/Resource/b9.JPG")%>' /> Upload File
                </asp:LinkButton>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <label id="information"  runat="server"  />

</asp:Content>