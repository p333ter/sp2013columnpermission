<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListViews.aspx.cs" Inherits="SPGuysCustomFieldPermission.Layouts.ListViews" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <asp:Label ID="errorText" runat="server" Font-Bold="true" ForeColor="Red" ></asp:Label>
    <asp:Panel ID="MainPanel" runat="server">
    </asp:Panel>
    
    <table border="0" cellspacing="0" cellpadding="0" class="ms-v4propertysheetspacing" width="99%" style="margin-top:0px;">
        <tr>
            <td height="2px" class="ms-sectionline" colspan="3">
                <img src="/_layouts/images/blank.gif" width='1' height='1' alt="" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table cellspacing="0" cellpadding="0" width="100%">
                    <colgroup>
                        <col width="0" />
                        <col width="405" />
                    </colgroup>
                    <tbody>
                    <tr>
                            

                        <td class=ms-descriptiontext vAlign=top >
<table border=0 cellSpacing=0 cellPadding=1 width="100%">
<tbody>
<tr>
<td style="PADDING-TOP: 4px" class=ms-sectionheader height=22 vAlign=top>
<H3 class="ms-standardheader ms-inputformheader">ViceVersa </H3></td></tr>
<tr>
<td class="ms-descriptiontext ms-inputformdescription">Check this CheckBox if you want that permissions for views work "ViceVersa".</td>
<td><IMG alt="" src="/_layouts/images/blank.gif" width=8 height=1></td></tr>
<tr>
<td><IMG alt="" src="/_layouts/images/blank.gif" width=150 height=19></td></tr></tbody></table></td>
<td width="40%" align="left" class="ms-authoringcontrols ms-inputformcontrols" vAlign="top">
<table border=0 cellSpacing=0 cellPadding=0 width="100%">
<tbody style="width:400px;">
<tr>
<td width=9><img alt="" src="/_layouts/images/blank.gif" width=9 height=7></td>
<td><img alt="" src="/_layouts/images/blank.gif" width=150 height=7></td>
<td width=10><img alt="" src="/_layouts/images/blank.gif" width=10 height=1></td></tr>
<tr>
<td class=ms-authoringcontrols>
<table class=ms-authoringcontrols border=0 cellSpacing=0 cellPadding=0 width="100%">
<tbody>
<tr>
<td vAlign=top width=11><asp:CheckBox runat="server" ID="chbxPermission" /><IMG alt="" src="/_layouts/images/blank.gif" width=1 height=1></td>
<td class=ms-authoringcontrols><label for=ctl00_PlaceHolderMain_ctl00_enableTargeting>Enable ViceVersa</label></td></tr>
<tr>
<td height=2 colSpan=2><img alt="" src="/_layouts/images/blank.gif" width=1 height=2></td></tr></tbody></table></td>
<td width=10><img alt="" src="/_layouts/images/blank.gif" width=10 height=1></td></tr>
<tr>
<td>
<td><IMG alt="" src="/_layouts/images/blank.gif" width=150 height=13></td>
<td></td></tr></tbody></table></td>
<tr>
<td class=ms-sectionline height=2 colSpan=2><img alt="" src="/_layouts/images/blank.gif" width=1 height=1></td></tr>
<tr>
<td class=ms-descriptiontext height=10 colSpan=2><img alt="" src="/_layouts/images/blank.gif" width=1 height=10></td>
</tr>

</tr>





                        <tr>
                            <td>
                                &nbsp;
                            </td>

                            <td nowrap style="margin-top:15px">
                                <asp:Button runat="server" AccessKey="S" ID="SAVEbutton1" class="ms-ButtonHeightWidth"
                                    name="SAVEbutton1" Text="Save" />
                                <asp:Button runat="server" AccessKey="C" ID="CancelButton1" class="ms-ButtonHeightWidth"
                                    Text="Cancel" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="PageDescription" ContentPlaceHolderID="PlaceHolderPageDescription"
    runat="server">
    <asp:Label ID="PageDescriptionText" runat="server" />
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
</asp:Content>
