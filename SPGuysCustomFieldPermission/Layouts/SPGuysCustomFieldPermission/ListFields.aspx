<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListFields.aspx.cs" Inherits="SPGuysCustomFieldPermission.Layouts.SPGuysCustomFieldPermission.ListFields"
    DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <table class="ms-settingsframe ms-listedit" style="width: 99%; height: 99%;" border="0"
        cellspacing="0" cellpadding="0">
        <tbody>
            <tr>
                <td width="100%">
                    <table cellspacing="3" cellpadding="0" width="100%">
                        <tbody>
                            <tr height="10">
                                <td style="padding-bottom: 4px; padding-left: 4px; padding-right: 4px; padding-top: 4px"
                                    id="1400" class="ms-linksectionheader" colspan="4">
                                    <h3 class="ms-standardheader">
                                        Columns</h3>
                                </td>
                            </tr>
                            <tr>
                                
                                <td id="1500" class="ms-descriptiontext ms-listedit-sectiondescription" valign="top"
                                    colspan="4">
                                    Set Column permission for each visible Column in forms:
                                </td>
                            </tr>
                            <tr>
                                <td class="ms-gb" colspan="4" style="border-bottom-width: 0px;">
                                    <table border="0" cellspacing="0" summary="List1" cellpadding="0" width="50%">
                                        <tbody>
                                            <tr>
                                                <th id="1600" class="ms-vh2-nofilter" width="50%" scope="col">
                                                    Column (click to edit)
                                                </th>
                                                <th id="1700" class="ms-vh2-nofilter" width="50%" scope="col">
                                                    Unique Permission
                                                </th>
                                            </tr>
                                            <!-- <tr>
                                                <td class="ms-vb2">
                                                    <a id="LinkEditField1" href="FldEdit.aspx?List=%7BB56A3E56%2D3E48%2D4D12%2DBD99%2D46ED81178C0B%7D&amp;Field=Title">
                                                        Nadpis</a>
                                                </td>
                                                <td class="ms-vb2" colspan="2">
                                                    <img alt="Checked" src="/_layouts/images/check.gif">
                                                </td>
                                            </tr>
                                            <tr class="ms-alternating">
                                                <td class="ms-vb2">
                                                    <a id="LinkEditField4" href="FldEdit.aspx?List=%7BB56A3E56%2D3E48%2D4D12%2DBD99%2D46ED81178C0B%7D&amp;Field=Number">
                                                        Number</a>
                                                </td>
                                                <td class="ms-vb2" colspan="2">
                                                </td>
                                            </tr> -->
                                            <asp:Literal ID="innerTable" runat="server"></asp:Literal>
                                        </tbody>
                                    </table>
                                </td>
                                </tr>
                                </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>


                                <table class="ms-settingsframe ms-listedit" style="width: 99%; height: 99%;" border="0"
        cellspacing="0" cellpadding="0">
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
<td class="ms-descriptiontext ms-inputformdescription">Check this CheckBox if you want that permissions for fields work "ViceVersa".</td>
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
                                                    <td nowrap>
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
<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
</asp:Content>
