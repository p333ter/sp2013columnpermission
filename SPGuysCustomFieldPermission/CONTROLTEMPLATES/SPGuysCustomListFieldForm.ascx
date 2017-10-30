<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBar" src="~/_controltemplates/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" src="~/_controltemplates/ToolBarButton.ascx" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Assembly="SPGuysCustomFieldPermissionLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a8a8bdc8ce8bd55f" Namespace="SPGuysCustomFieldPermissionLibrary" TagPrefix="SPGuysCustomFieldPermissionLibrary" %>
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SPGuysCustomListFieldForm.ascx.cs" Inherits="SPGuysCustomFieldPermission.CONTROLTEMPLATES.SPGuysCustomListFieldForm" %>
<!-- SHarePoint:FieldIterator -->
<SharePoint:RenderingTemplate id="GenericForm" runat="server">
	<Template>
		<table class="ms-formtable" border="0" cellpadding="2">
			<SPGuysCustomFieldPermissionLibrary:CustomFieldIterator ID="CustomFieldIterator4" runat="server" />
		</table>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate id="ListForm" runat="server">
	<Template>
		<table>
			<tr>
				<td>
					<span id='part1'>
						<SharePoint:InformationBar ID="InformationBar1" runat="server"/>
						<div id="listFormToolBarTop">
						<wssuc:ToolBar CssClass="ms-formtoolbar" id="toolBarTbltop" RightButtonSeparator="&amp;#160;" runat="server">
								<Template_RightButtons>
									<SharePoint:NextPageButton runat="server"/>
									<SharePoint:SaveButton runat="server"/>
									<SharePoint:GoBackButton runat="server"/>
								</Template_RightButtons>
						</wssuc:ToolBar>
						</div>
						<SharePoint:FormToolBar ID="FormToolBar1" runat="server"/>
						<SharePoint:ItemValidationFailedMessage ID="ItemValidationFailedMessage1" runat="server"/>
						<table class="ms-formtable" style="margin-top: 8px;" border="0" cellpadding="0" cellspacing="0" width="100%">
						<SharePoint:ChangeContentType ID="ChangeContentType1" runat="server"/>
						<SharePoint:FolderFormFields ID="FolderFormFields1" runat="server"/>
                        <SPGuysCustomFieldPermissionLibrary:CustomFieldIterator ID="CustomFieldIterator1" runat="server" />
						<SharePoint:ApprovalStatus ID="ApprovalStatus1" runat="server"/>
						<SharePoint:FormComponent ID="FormComponent1" TemplateName="AttachmentRows" ComponentRequiresPostback="false" runat="server"/>
						</table>
						<table cellpadding="0" cellspacing="0" width="100%" style="padding-top: 7px"><tr><td width="100%">
						<SharePoint:ItemHiddenVersion ID="ItemHiddenVersion1" runat="server"/>
						<SharePoint:ParentInformationField ID="ParentInformationField1" runat="server"/>
						<SharePoint:InitContentType ID="InitContentType1" runat="server"/>
						<wssuc:ToolBar CssClass="ms-formtoolbar" id="toolBarTbl" RightButtonSeparator="&amp;#160;" runat="server">
								<Template_Buttons>
									<SharePoint:CreatedModifiedInfo runat="server"/>
								</Template_Buttons>
								<Template_RightButtons>
									<SharePoint:SaveButton runat="server"/>
									<SharePoint:GoBackButton runat="server"/>
								</Template_RightButtons>
						</wssuc:ToolBar>
						</td></tr></table>
					</span>
				</td>
				<td valign="top">
					<SharePoint:DelegateControl ID="DelegateControl1" runat="server" ControlId="RelatedItemsPlaceHolder"/>
				</td>
			</tr>
		</table>
		<SharePoint:AttachmentUpload ID="AttachmentUpload1" runat="server"/>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate id="TaskForm" runat="server">
	<Template>
		<table>
			<tr>
				<td style="height:350px; vertical-align:top">
					<span id='part1'>
						<SharePoint:EditDatesSelector ID="EditDatesSelector1" RenderInEditDatesMode="false" runat="server">
							<SharePoint:InformationBar ID="InformationBar2" runat="server"/>
							<div id="listFormToolBarTop">
								<wssuc:ToolBar CssClass="ms-formtoolbar" id="toolBarTbltop" RightButtonSeparator="&amp;#160;" runat="server">
										<Template_RightButtons>
											<SharePoint:NextPageButton runat="server"/>
											<SharePoint:SaveButton runat="server"/>
											<SharePoint:GoBackButton runat="server"/>
										</Template_RightButtons>
								</wssuc:ToolBar>
							</div>
							<SharePoint:FormToolBar ID="FormToolBar2" runat="server"/>
						</SharePoint:EditDatesSelector>
						<SharePoint:ItemValidationFailedMessage ID="ItemValidationFailedMessage2" runat="server"/>
						<SharePoint:EditDatesSelector ID="EditDatesSelector2" RenderInEditDatesMode="true" runat="server">
							<div><SharePoint:EncodedLiteral ID="EncodedLiteral2" runat="server" text="<%$Resources:wss,BeautifulTimeline_HelperText%>" EncodeMethod='HtmlEncode'/></div>
						</SharePoint:EditDatesSelector>
						<table class="ms-formtable" style="margin-top: 8px;" border="0" cellpadding="0" cellspacing="0" width="100%">
							<SharePoint:EditDatesSelector ID="EditDatesSelector3" RenderInEditDatesMode="false" runat="server">
								<SharePoint:ChangeContentType ID="ChangeContentType2" runat="server"/>
								<SharePoint:FolderFormFields ID="FolderFormFields2" runat="server"/>
								<SPGuysCustomFieldPermissionLibrary:CustomFieldIterator ID="CustomFieldIterator1" runat="server" />
								<SharePoint:ApprovalStatus ID="ApprovalStatus2" runat="server"/>
								<SharePoint:FormComponent ID="FormComponent2" TemplateName="AttachmentRows" ComponentRequiresPostback="false" runat="server"/>
							</SharePoint:EditDatesSelector>
							<SharePoint:EditDatesSelector ID="EditDatesSelector4" RenderInEditDatesMode="true" runat="server">
								<SharePoint:SpecifiedListFieldIterator ID="SpecifiedListFieldIterator1" ShownFields="StartDate;#DueDate" runat="server"/>
							</SharePoint:EditDatesSelector>
						</table>
						<table cellpadding="0" cellspacing="0" width="100%" style="padding-top: 7px"><tr><td width="100%">
							<SharePoint:EditDatesSelector ID="EditDatesSelector5" RenderInEditDatesMode="false" runat="server">
								<SharePoint:ItemHiddenVersion ID="ItemHiddenVersion2" runat="server"/>
								<SharePoint:ParentInformationField ID="ParentInformationField2" runat="server"/>
								<SharePoint:InitContentType ID="InitContentType2" runat="server"/>
							</SharePoint:EditDatesSelector>
							<wssuc:ToolBar CssClass="ms-formtoolbar" id="toolBarTbl" RightButtonSeparator="&amp;#160;" runat="server">
								<Template_Buttons>
									<SharePoint:EditDatesSelector RenderInEditDatesMode="false" runat="server">
										<SharePoint:CreatedModifiedInfo runat="server"/>
									</SharePoint:EditDatesSelector>
								</Template_Buttons>
								<Template_RightButtons>
									<SharePoint:SaveButton runat="server"/>
									<SharePoint:GoBackButton runat="server"/>
								</Template_RightButtons>
						</wssuc:ToolBar>
						</td></tr></table>
					</span>
				</td>
				<SharePoint:EditDatesSelector ID="EditDatesSelector7" RenderInEditDatesMode="false" runat="server">
					<td valign="top">
						<SharePoint:DelegateControl ID="DelegateControl2" runat="server" ControlId="RelatedItemsPlaceHolder"/>
					</td>
				</SharePoint:EditDatesSelector>
			</tr>
		</table>
		<SharePoint:EditDatesSelector ID="EditDatesSelector8" RenderInEditDatesMode="false" runat="server">
			<SharePoint:AttachmentUpload ID="AttachmentUpload2" runat="server"/>
		</SharePoint:EditDatesSelector>
	</Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate id="FileFormFields" runat="server">
	<Template>
			<SPGuysCustomFieldPermissionLibrary:CustomFieldIterator ID="CustomFieldIterator3" runat="server" />
	</Template>
</SharePoint:RenderingTemplate>


<!-- SharePoint:ViewSelectorMenu -->
<SharePoint:RenderingTemplate ID="ViewSelector" runat="server">
	<Template>
		<table border="0" cellpadding="0" cellspacing="0" style='margin-right: 4px'>
		<tr>
		   <td nowrap="nowrap" class="ms-listheaderlabel"><SharePoint:EncodedLiteral ID="EncodedLiteral1" runat="server" text="<%$Resources:wss,view_selector_view%>" EncodeMethod='HtmlEncode'/>&#160;</td>
		   <td nowrap="nowrap" class="ms-viewselector" id="onetViewSelector" onmouseover="this.className='ms-viewselectorhover'" onmouseout="this.className='ms-viewselector'" runat="server">
				<SPGuysCustomFieldPermissionLibrary:CustomViewSelectorMenu MenuAlignment="Right" AlignToParent="true" runat="server" id="ViewSelectorMenu" />
			</td>
		</tr>
		</table>
	</Template>
</SharePoint:RenderingTemplate>

