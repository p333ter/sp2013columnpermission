<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FieldPermission.aspx.cs"
    Inherits="SPGuysCustomFieldPermission.Layouts.SPGuysCustomFieldPermission.FieldPermission"
    DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
<asp:Label ID="errorText" runat="server" Font-Bold="true" ForeColor="Red" ></asp:Label>
    <table border="0" cellspacing="0" cellpadding="0" class="ms-propertysheet">
        <tr id="trMain1">
            <td class="ms-sectionline" height="1" colspan="3">
                <img src="/_layouts/images/blank.gif" width='1' height='1' alt="" />
            </td>
        </tr>
        <tr id="tr1">
            <td class="ms-descriptiontext" valign="top" width="60%">
                <table border="0" cellpadding="1" cellspacing="0" width="100%">
                    <tr>
                        <td class="ms-sectionheader" style="padding-top: 4px;" height="22" valign="top">
                            <h3 class="ms-standardheader ms-inputformheader">
                                New Form
                            </h3>
                        </td>
                    </tr>
                    <tr>
                        <td class="ms-descriptiontext ms-inputformdescription">
                            You can enter User names, Group names, Active Directory group names or e-mail addresses <b>which will have Read Only or Hidden permission</b>.
                            Separate them with semicolons.
                            <br />
                        </td>
                        <td>
                            <img src="/_layouts/images/blank.gif" width='8' height='1' alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img src="/_layouts/images/blank.gif" width='150' height='19' alt="" />
                        </td>
                    </tr>
                </table>
            </td>
            <td class="ms-authoringcontrols ms-inputformcontrols" valign="top" align="left" width="40%">
                <table border="0" width="100%" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="9px">
                            <img src="/_layouts/images/blank.gif" width='9' height='7' alt="" />
                        </td>
                        <td>
                            <img src="/_layouts/images/blank.gif" width='150' height='7' alt="" />
                        </td>
                        <td width="10px">
                            <img src="/_layouts/images/blank.gif" width='10' height='1' alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td />
                        <td class="ms-authoringcontrols">
                            <table class="ms-authoringcontrols" border="0" width="100%" cellspacing="0" cellpadding="0">
                                <tr id="ctl00_PlaceHolderMain_ctl00_ctl01_tablerow1">
                                    <td class="ms-authoringcontrols" colspan="2">
                                        <span id="ctl00_PlaceHolderMain_ctl00_ctl01_LiteralLabelText">Read Only - Users/Groups:</span>
                                    </td>
                                </tr>
                                <tr id="ctl00_PlaceHolderMain_ctl00_ctl01_tablerow2">
                                    <td>
                                        <img src="/_layouts/images/blank.gif" width='1' height='3' style="display: block"
                                            alt="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="" valign="top">
                                        <SharePoint:PeopleEditor ID="peopleEditorNewReadOnly" runat="server" SelectionSet="User,DL,SecGroup,SPGroup" Rows="3" AllowEmpty="true" ValidatorEnabled="true" MultiSelect="true" Width="300" >
                                        </SharePoint:PeopleEditor>
                                    </td>
                                </tr>
                                <tr id="tr2">
                                    <td class="ms-authoringcontrols" colspan="2">
                                        <span id="Span1">Hidden - Users/Groups:</span>
                                    </td>
                                </tr>
                                <tr id="tr14">
                                    <td>
                                        <img src="/_layouts/images/blank.gif" width='1' height='3' style="display: block"
                                            alt="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="" valign="top">
                                        <SharePoint:PeopleEditor ID="peopleEditorNewHidden" runat="server" SelectionSet="User,DL,SecGroup,SPGroup" Rows="3" AllowEmpty="true" ValidatorEnabled="true" MultiSelect="true" Width="300" >
                                        </SharePoint:PeopleEditor>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="tr3">
            <td class="ms-sectionline" height="1" colspan="3">
                <img src="/_layouts/images/blank.gif" width='1' height='1' alt="" />
            </td>
        </tr>
        <tr id="tr4">
            <td class="ms-descriptiontext" valign="top" width="60%">
                <table border="0" cellpadding="1" cellspacing="0" width="100%">
                    <tr>
                        <td class="ms-sectionheader" style="padding-top: 4px;" height="22" valign="top">
                            <h3 class="ms-standardheader ms-inputformheader">
                                Edit Form
                            </h3>
                        </td>
                    </tr>
                    <tr>
                        <td class="ms-descriptiontext ms-inputformdescription">
                            You can enter User names, Group names, Active Directory group names or e-mail addresses <b>which will have Read Only or Hidden permission</b>.
                            Separate them with semicolons.
                            <br />
                        </td>
                        <td>
                            <img src="/_layouts/images/blank.gif" width='8' height='1' alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img src="/_layouts/images/blank.gif" width='150' height='19' alt="" />
                        </td>
                    </tr>
                </table>
            </td>
            <td class="ms-authoringcontrols ms-inputformcontrols" valign="top" align="left" width="40%">
                <table border="0" width="100%" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="9px">
                            <img src="/_layouts/images/blank.gif" width='9' height='7' alt="" />
                        </td>
                        <td>
                            <img src="/_layouts/images/blank.gif" width='150' height='7' alt="" />
                        </td>
                        <td width="10px">
                            <img src="/_layouts/images/blank.gif" width='10' height='1' alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td />
                        <td class="ms-authoringcontrols">
                            <table class="ms-authoringcontrols" border="0" width="100%" cellspacing="0" cellpadding="0">
                                <tr id="tr5">
                                    <td class="ms-authoringcontrols" colspan="2">
                                        <span id="Span2">Read Only - Users/Groups:</span>
                                    </td>
                                </tr>
                                <tr id="tr6">
                                    <td>
                                        <img src="/_layouts/images/blank.gif" width='1' height='3' style="display: block"
                                            alt="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="" valign="top">
                                        <SharePoint:PeopleEditor ID="peopleEditorEditReadOnly" runat="server" SelectionSet="User,DL,SecGroup,SPGroup" Rows="3" AllowEmpty="true" ValidatorEnabled="true" MultiSelect="true" Width="300" >
                                        </SharePoint:PeopleEditor>
                                    </td>
                                </tr>
                                <tr id="tr7">
                                    <td class="ms-authoringcontrols" colspan="2">
                                        <span id="Span3">Hidden - Users/Groups:</span>
                                    </td>
                                </tr>
                                <tr id="tr13">
                                    <td>
                                        <img src="/_layouts/images/blank.gif" width='1' height='3' style="display: block"
                                            alt="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="" valign="top">
                                        <SharePoint:PeopleEditor ID="peopleEditorEditHidden" runat="server" SelectionSet="User,DL,SecGroup,SPGroup" Rows="3" AllowEmpty="true" ValidatorEnabled="true" MultiSelect="true" Width="300" >
                                        </SharePoint:PeopleEditor>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="tr8">
            <td class="ms-sectionline" height="1" colspan="3">
                <img src="/_layouts/images/blank.gif" width='1' height='1' alt="" />
            </td>
        </tr>
        <tr id="tr9">
            <td class="ms-descriptiontext" valign="top" width="60%">
                <table border="0" cellpadding="1" cellspacing="0" width="100%">
                    <tr>
                        <td class="ms-sectionheader" style="padding-top: 4px;" height="22" valign="top">
                            <h3 class="ms-standardheader ms-inputformheader">
                                Display Form / Views / Alerts
                            </h3>
                        </td>
                    </tr>
                    <tr>
                        <td class="ms-descriptiontext ms-inputformdescription">
                            You can enter User names, Group names, Active Directory group names or e-mail addresses <b>which will have Read Only or Hidden permission</b>.
                            Separate them with semicolons.
                            <br />
                        </td>
                        <td>
                            <img src="/_layouts/images/blank.gif" width='8' height='1' alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img src="/_layouts/images/blank.gif" width='150' height='19' alt="" />
                        </td>
                    </tr>
                </table>
            </td>
            <td class="ms-authoringcontrols ms-inputformcontrols" valign="top" align="left" width="40%">
                <table border="0" width="100%" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="9px">
                            <img src="/_layouts/images/blank.gif" width='9' height='7' alt="" />
                        </td>
                        <td>
                            <img src="/_layouts/images/blank.gif" width='150' height='7' alt="" />
                        </td>
                        <td width="10px">
                            <img src="/_layouts/images/blank.gif" width='10' height='1' alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td />
                            <td class="ms-authoringcontrols">
                                <table class="ms-authoringcontrols" border="0" width="100%" cellspacing="0" cellpadding="0">
                                    <tr id="tr10">
                                        <td class="ms-authoringcontrols" colspan="2">
                                            <span id="Span4">Read Only - Users/Groups:</span>
                                        </td>
                                    </tr>
                                    <tr id="tr11">
                                        <td>
                                            <img src="/_layouts/images/blank.gif" width='1' height='3' style="display: block"
                                                alt="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="" valign="top">
                                            <SharePoint:PeopleEditor ID="peopleEditorDisplayReadOnly" runat="server" SelectionSet="User,DL,SecGroup,SPGroup" Rows="3" AllowEmpty="true" ValidatorEnabled="true" MultiSelect="true" Width="300" >
                                            </SharePoint:PeopleEditor>
                                        </td>
                                    </tr>
                                    <tr id="tr15">
                                        <td class="ms-authoringcontrols" colspan="2">
                                            <span id="Span5">Hidden - Users/Groups:</span>
                                        </td>
                                    </tr>
                                    <tr id="tr12">
                                        <td>
                                            <img src="/_layouts/images/blank.gif" width='1' height='3' style="display: block"
                                                alt="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="" valign="top">
                                            <SharePoint:PeopleEditor ID="peopleEditorDisplayHidden" runat="server" SelectionSet="User,DL,SecGroup,SPGroup" Rows="3" AllowEmpty="true" ValidatorEnabled="true" MultiSelect="true" Width="300" >
                                            </SharePoint:PeopleEditor>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="2px" class="ms-sectionline" colspan="3">
                <img src="/_layouts/images/blank.gif" width='1' height='1' alt="" />
            </td>
        </tr>
        <tr>
            <td class="ms-descriptiontext" height="10" colspan="2">
                <img alt="" src="/_layouts/images/blank.gif" width="1" height="10">
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <table cellspacing="0" cellpadding="0" width="100%">
                    <colgroup>
                        <col width="99%" />
                        <col width="1%" />
                    </colgroup>
                    <tbody>
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
<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Column Permission -
    <asp:Literal runat="server" ID="lPageTitle"></asp:Literal>
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    Column Permission -
    <asp:Literal runat="server" ID="LPageTitleInTitleArea"></asp:Literal>
</asp:Content>
