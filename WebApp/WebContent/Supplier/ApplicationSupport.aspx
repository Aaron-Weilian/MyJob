<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Templates/Supplier.Master" CodeBehind="ApplicationSupport.aspx.cs" Inherits="WebApp.WebContent.Supplier.ApplicationSupport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table style="width:100%;height:100%" cellpadding="0" cellspacing="0">
		<tr>
			<td width="989">
				<table >
					<tr>
						<td width="989" valign="top">
<table style="width:100%;height:100%" cellpadding="0" cellspacing="0">
	<tr>
		<td width="989" valign="top">
			<table width="100%" cellpadding="0" cellspacing="0">
				<tr>
					<td style="padding-left:20px;padding-right:20px;padding-top:13px" valign="top">
						<table width="100%" cellpadding="0" cellspacing="0" border="0">
							<tr>
								<td width="150" valign="top" style="vertical-align: top">
									<h1 style="color:black; font-weight:bold;" dir="ltr">Helpdesk</h1>
									We are always glad to be of service. Do not hesitate to contact us if you need any further <br />
									<img alt="" src='<%=ResolveUrl("~/Styles/Resource/helpdesk.jpg")%>'/><br/><br/>
									Or email: <a href='mailto:'>****@sonymobile.com</a>
								</td>
								<td width="40">
									<img alt="" src='<%=ResolveUrl("~/Styles/Resource/spacer.gif")%>' width="1" height="1" />
								</td>
								<td>
									<h1 style="color:black; font-weight:bold;">Application Support</h1>
									Our goal is to make the partnership as smooth as possible. We hope that you find the portal and 
									<table width="100%" cellpadding="0" cellspacing="0" border="0">
										<tr>
											<td>
												<b>Frequently asked questions</b><br />
											</td>
										</tr>
<%--
										<tr>
											<td>
											    <div class="span-1 left">
											        <b>Q:</b>
											    </div>
                                                <div class="span-15 last">
												    <a href="#" onclick="switchNextDiv(this)">Downloading content from the BmcICPortal is blocked by my browser. What should I do? </a>
												    <div>
													    There are 2 ways to fix this problem. Either you press the 'ctrl' button while clicking on the link 
												    </div>
                                                </div>
											</td>
										</tr>
										<tr>
											<td>
											    <div class="span-1 left">
											        <b>Q:</b>
											    </div>
                                                <div class="span-15 last">
												    <a href="#" class="arrow" onclick="switchNextDiv(this)">Where can I find the Sony Mobile User Guides and Implementation Guides? </a>
												    <div>
													    These documents can be downloaded from the Partner Documentation section.
												    </div>
                                                </div>
											</td>
										</tr>
										<tr>
											<td>
											    <div class="span-1 left">
											        <b>Q:</b>
											    </div>
                                                <div class="span-15 last">
												    <a href="#" class="arrow" onclick="switchNextDiv(this)">How can I check that my Excel file is correctly filled in? </a>
												    <div>
													    On each spreadsheet there is a button named 'Check Form Fields'. Click this button to receive a 
												    </div>
                                                </div>
											</td>
										</tr>
										<tr>
											<td>
											    <div class="span-1 left">
											        <b>Q:</b>
											    </div>
                                                <div class="span-15 last">
												    <a href="#" class="arrow" onclick="switchNextDiv(this)">What information is requested by the Helpdesk when I report an incident? </a>
												    <div>
													    When reporting an incident, please be prepared to leave information such as:<br><li>Company name</li><li>E-mail address</li><li>Name</li><li>Contact number (office)</li><li>Contact number (mobile)</li><li>Which activity of which Form/Message where the error occurs</li><li>Reference number for the message: PO#, Invoice#, etc</li><li>Screen shot of the situation where the error occurs</li>
												    </div>
                                                </div>
											</td>
										</tr>
										<tr>
											<td>
											    <div class="span-1 left">
											        <b>Q:</b>
											    </div>
                                                <div class="span-15 last">
												    <a href="#" class="arrow" onclick="switchNextDiv(this)">What does the * character mean? </a>
												    <div>
													    The * character is used to mark that a field is mandatory and needs to be filled with data.<br />If 
												    </div>
                                                </div>
											</td>
										</tr>
										<tr>
											<td>
											    <div class="span-1 left">
											        <b>Q:</b>
											    </div>
                                                <div class="span-15 last">
												    <a href="#" class="arrow" onclick="switchNextDiv(this)">What does the Exception code &#39;ValidationErrorEvent&#39; mean? </a>
												    <div>
													    ValidationErrorEvent means that the message has failed in the WPG, this could be because of a data 
												    </div>
                                                </div>
											</td>
										</tr>
										<tr>
											<td>
											    <div class="span-1 left">
											        <b>Q:</b>
											    </div>
                                                <div class="span-15 last">
												    <a href="#" class="arrow" onclick="switchNextDiv(this)">What version of Excel (MS Office) do I need to view the excel files? </a>
												    <div>
													    <a href="#" class="arrow" onclick="switchNextDiv(this)">What version of Excel (MS Office) do I need to view the excel files? </a>
												    </div>
                                                </div>
											</td>
										</tr>--%>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
						</td>
					</tr>
				</table>
			</td>
			<td style="background-image:url('url(<%=ResolveUrl("~/Styles/Resource/rightwing.gif")%>');background-position:left;background-repeat: repeat-y;">
				&nbsp;
			</td>
		</tr>
	</table>

</asp:Content>
