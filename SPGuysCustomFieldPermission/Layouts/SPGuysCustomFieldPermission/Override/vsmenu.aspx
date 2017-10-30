<%@ Assembly Name="Microsoft.SharePoint.ApplicationPages, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"%>
<%@ Page Language="C#" Inherits="Microsoft.SharePoint.ApplicationPages.ViewSelectorMenuPage" %>
<%@ Register Tagprefix="wssawc" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Assembly="SPGuysCustomFieldPermissionLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a8a8bdc8ce8bd55f" Namespace="SPGuysCustomFieldPermissionLibrary" TagPrefix="SPGuysCustomFieldPermissionLibrary" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<html dir="<SharePoint:EncodedLiteral runat='server' text='<%$Resources:wss,multipages_direction_dir_value%>' EncodeMethod='HtmlEncode'/>">
<head>
	<meta name="GENERATOR" content="Microsoft SharePoint"/>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
	<meta http-equiv="Expires" content="0"/>
	
	<title id="onetidTitle"><SharePoint:EncodedLiteral runat="server" text="<%$Resources:wss,pagetitle_sharepoint%>" EncodeMethod='HtmlEncode'/></title>
<SharePoint:CssLink runat="server"/>
<SharePoint:ScriptLink name="init.js" language="javascript" runat="server" />
<SharePoint:ScriptLink name="core.js" language="javascript" runat="server" />
<SharePoint:CustomJSUrl runat="server" />
<link type="text/xml" rel='alternate' href="_vti_bin/spdisco.aspx" />
</head>
	<body oncontextmenu = "return false;" onclick = "if(event.shiftKey) {event.returnValue = false; event.cancelBubble = true;}">
	<form runat="server">
			<asp:ScriptManager id="ScriptManager" runat="server" EnablePageMethods="false" EnablePartialRendering="true" EnableScriptGlobalization="false" EnableScriptLocalization="true" />
		<SharePoint:DelegateControl runat="server" ControlId="ViewSelectorMenuDelegate" Id="ViewSelectorMenuDelegateControl">
			<Template_Controls>
				<SPGuysCustomFieldPermissionLibrary:CustomViewSelectorMenu runat="server" id="idVSMenu" />
			</Template_Controls>
		</SharePoint:DelegateControl>
	</form>
	</body>
</html>
