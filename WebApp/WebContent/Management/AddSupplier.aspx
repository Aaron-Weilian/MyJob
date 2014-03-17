<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/Management.Master" AutoEventWireup="true" CodeBehind="AddSupplier.aspx.cs" Inherits="WebApp.WebContent.Management.AddSupplier" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <fieldset>
            <legend>Supplier Details</legend>
            <div class="span-12">
                <div class="span-12">
                    <label class="span-3" for="">Supplier Name * :</label>
                    <input id="SupplierName" type="text" name="SupplierName" runat="server" size="45"/>
                </div>
                <div class="span-12">
                    <label class="span-3" for="">Supplier NUM * :</label>
                    <input id="SupplierNUM" type="text" name="SupplierNUM" runat="server"   />
                </div>
                <div class="span-12">
                    <label class="span-3" for="">DUNS  :</label>
                    <input id="Duns" type="text" name="Duns" runat="server"/>
                </div>
                <div class="span-12">
                    <label class="span-3" for="">Contact Name  :</label>
                    <input id="ContactName" type="text" name="ContactName" runat="server"/>
                </div>
                <div class="span-12">
                    <label class="span-3" for="">Email *:</label>
                    <input id="Email" type="text" name="Email" runat="server"/>
                </div>
                <div class="span-12">
                    <label class="span-3" for="">Phone :</label>
                    <input id="Phone" type="text" name="Phone" runat="server"/>
                </div>
                <div class="span-12">
                    <label class="span-3" for="">Site NUM *:</label>
                    <input id="CountryCode" type="text" name="CountryCode" runat="server"/>
                </div>
                <div class="span-12">
                    <label class="span-3" for="">Address1:</label>
                    <input id="Address1" type="text" name="Address1" runat="server" size="45" />
                </div>
                <div class="span-10">
                    <label class="span-3" for="">
                    </label>
                </div>
            </div>
            <div class="span-10 last">
                <div class="span-10">
                    <label for="SiteList">Supplier Site List :</label>
                </div>
                <div class="span-10">
                    <asp:ListBox runat="server"  Rows="5" Width="280px" ID="SiteList"/>
                </div>
                <div class="span-10">
                    <label class="span-3" for="">Site Name *:</label>
                    <input id="SiteName"  type="text" name="SiteName" runat="server"/>
                </div>
                <div class="span-10">
                    <label class="span-3" for="">Site NUM *:</label>
                    <input id="SiteNUM"  type="text" name="SiteNUM" runat="server"/>
                </div>
                <div class="span-10">
                    <asp:Button ID="Addsite" CssClass="button" Text="Add Supplier Site" runat="server" onclick="Addsite_Click" />
                    <asp:Button ID="Remocesite" CssClass="button" Text="Remove Selected Site" runat="server" onclick="Remocesite_Click"/>
                </div>
            </div>
        </fieldset>
        <div class="span-20">
            <asp:LinkButton ID="back" runat="server"  OnClick="back_Click" >
                <img alt="" style="border:0px; margin-top:-10px; " src="../../Styles/Resource/b9.JPG" /> BACK
            </asp:LinkButton>
            <asp:LinkButton ID="save" runat="server"  OnClick="save_Click" >
                <img alt="" style="border:0px; margin-top:-10px; " src="../../Styles/Resource/b9.JPG" /> SAVE
            </asp:LinkButton>
            <input id="UserID" type="hidden" runat ="server" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
     <label id="mes1" runat="server" ></label> 
     <label id="mes2" runat="server" ></label>
     <label id="mes" runat="server">
        </label>
</asp:Content>