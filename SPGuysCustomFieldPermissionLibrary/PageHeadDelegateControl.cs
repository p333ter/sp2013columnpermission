using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.WebPartPages;
using System;
using System.Reflection;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Xml;
using System.Collections;


namespace SPGuysCustomFieldPermissionLibrary
{

    //
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal), SharePointPermission(SecurityAction.InheritanceDemand, ObjectModel = true), AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal), SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
    public class PageHeadDelegateControl : WebControl
    {
        SPContext spContext = SPContext.Current;
        SPWeb spWeb = SPContext.Current.Web;
        SPSite spSite = SPContext.Current.Site;
        SPFeature spFeatureSite;

        //public static List<XsltListViewWebPart> xsltListCollection = new List<XsltListViewWebPart>();
        private bool _visible;
        private bool viewSelector = false;

        #region - Base Methods -

        [SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
        protected override void OnLoad(EventArgs e)
        {
            spFeatureSite = spSite.Features[Definitions.featureGuid];
            if (spFeatureSite != null)
            {
                try
                {
                    SPWeb contextWeb = SPControl.GetContextWeb(this.Context);
                    if ((contextWeb != null) && (contextWeb.UIVersion <= 3))
                    {
                        this._visible = false;
                        return;
                    }

                    Type baseType = this.Page.GetType().BaseType;
                    if ((baseType == typeof(WebPartPage)) || (baseType == typeof(Page)))
                    {
                        Control control = this.findControl(this.Page, "LTViewSelectorMenu");
                        if (control != null)
                        {
                            if (control.Visible)
                            {
                                viewSelector = true;
                                
                            }
                        }


                        this._visible = true;
                    }

                    #region - Ribbon block functions -

                    //Control control = this.findControl(this.Page, "WebPartWPQ2");
                    //if (control != null)
                    //{
                    //    //control.Visible = false;
                    //}

                    //WebPartWPQ2                               STSListControlWPQ2
                    //Control control = this.findControl(this.Page, "Ribbon.List.ViewFormat.Datasheet-Large");

                    //if (control != null)
                    //{
                    //    SPRibbonButton spRibbonDataSheet = control as SPRibbonButton;
                    //    spRibbonDataSheet.Enabled = false;
                    //}

                    //Ribbon.List.ViewFormat.Datasheet
                    //SPList.DisableGridEditing 

                    //var menuItems = document.getElementsByTagName('ie:menuitem'); 
                    //for (i=0; i<menuItems.length; i++)   
                    //    if (menuItems[i].text == 'Edit in Datasheet')     
                    //        menuItems[i].removeNode(); 

                    //$("ie\\:menuitem[text='Edit in Datasheet']").each(function()
                    //{     this.hidden = true; }); 


                    //if this is a System.Web.UI.Page  
                    //SPRibbon ribbon = SPRibbon.GetCurrent(this.Page);
                    //ribbon.TrimById("Ribbon.List.Settings.ListPermissions-Large");
                    //STSListControlWPQ2


                    //Control control2 = this.findControl(this.Page, "STSListControlWPQ2");
                    //control2.Visible = false;

                    #endregion
                }
                catch
                {
                    this._visible = false;
                }
            }

            base.OnLoad(e);
        }

        protected override void CreateChildControls()
        {
            SPView excelView = null;
            SPList excelList = null;
            string excelListSchema = String.Empty;

            spFeatureSite = spSite.Features[Definitions.featureGuid];
            if (spFeatureSite != null)
            {
                try
                {
                    //Ziskam vsetky XsltListViewWebPart na stranke ak tam su
                    List<XsltListViewWebPart> xsltListCollection = GetListViewWebParts();
                    UserPermission userPermission = new UserPermission();

                    if (xsltListCollection != null)
                    {
                        foreach (XsltListViewWebPart xsltWebPart in xsltListCollection)
                        {
                            SPView currentView = SPContext.Current.Web.Lists[xsltWebPart.ListId].Views[new Guid(xsltWebPart.ViewGuid)];
                            SPList currentList = SPContext.Current.Web.Lists[xsltWebPart.ListId];
                            List<SPField> listFieldsInProperties = new List<SPField>();

                            excelView = currentView;
                            excelList = currentList;

                            //ToDo
                            //Schovanie View tam kde je View pouzite len vo webparzone!
                            //Ak na View nema pravo tak ten control ani nevykresli v pripade ze sa jedna o List tak
                            //sa to osetri v CustomViewSelectorMenu.cs presmerovanom na error page
                            //Nefunguje to pri vyklikanom sposobe vytvorenia listview vo webparzone lebo sa stale vytvara nove VIEW s novym guid
                            if (!viewSelector)
                            {
                                if (userPermission.GetViewPermissionForCurrentUser(currentView))
                                {
                                    xsltWebPart.Hidden = true;
                                    continue;
                                }
                            }

                            listFieldsInProperties = FindFieldsInProperties(currentView, currentList);

                            if (listFieldsInProperties != null)
                            {
                                if (listFieldsInProperties.Count != 0)
                                {
                                    string newXmlDefinition = HideFieldsInXsltDefinition(listFieldsInProperties, xsltWebPart.XmlDefinition);

                                    xsltWebPart.AsyncRefresh = true;
                                    xsltWebPart.XmlDefinition = newXmlDefinition;

                                    //Toto naplnam pre RenderActiveX prvok
                                    excelListSchema = newXmlDefinition;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //ToDo
                }
            }

            base.CreateChildControls();

            #region - DataSheet -

            //if (Page.Request.QueryString["ShowInGrid"] == "True")
            //{
            //    spFeatureSite = spSite.Features[Definitions.featureGuid];
            //    if (spFeatureSite != null)
            //    {
            //        if (!excelListSchema.Equals(String.Empty))
            //        {
            //            try
            //            {
            //                Control controlPlaceHolderMain = this.findControl(this.Page, "PlaceHolderMain");
            //                if (controlPlaceHolderMain != null)
            //                {
            //                    Control controlParent = controlPlaceHolderMain.Parent;
            //                    if (controlParent != null)
            //                    {

            //                        //Schovame stary ActiveX prvok
            //                        Literal newStyle = new Literal();
            //                        newStyle.Text = "<style type=\"text/css\">#STSListControlWPQ2{ display:none }</style>";
            //                        controlPlaceHolderMain.Controls.Add(newStyle);

            //                        Guid webGuid = spWeb.ID;
            //                        Guid listGuid = excelList.ID;
            //                        Guid viewGuid = excelView.ID;

            //                        string ListData = EncodeString(excelList.Items.Xml);
            //                        string ViewSchema = EncodeString(excelListSchema);
            //                        string ListSchema = EncodeString(excelList.SchemaXml);
            //                        string UrlLink = EncodeString(spWeb.Url);

            //                        string csScript = "<script type='text/javascript'>RenderActiveX('\u003cobject id=\u0022STSListControlWPQ3\u0022 name=\u0022STSListControlWPQ3\u0022 classid=CLSID:65BCBEE4-7728-41a0-97BE-14E1CAE36AAE class=\u0022ms-dlgDisable\u0022 width=\u002299\u0025\u0022 tabindex=\u00221\u0022\u003e\u003cparam name=\u0022ListName\u0022 value=\u0022{" + listGuid + "}\u0022\u003e\u003cparam name=\u0022ViewGuid\u0022 value=\u0022{" + viewGuid + "}\u0022\u003e\u003cparam name=\u0022ListWeb\u0022 value=\u0022" + UrlLink + "\u002f_vti_bin\u0022\u003e\u003cparam name=\u0022ListData\u0022 value=\u0022" + ListData + "\u0022\u003e\u003cparam name=\u0022ViewSchema\u0022 value=\u0022" + ViewSchema + "\u0022\u003e\u003cparam name=\u0022ListSchema\u0022 value=\u0022" + ListSchema + "\u0022\u003e\u003cparam name=\u0022ControlName\u0022 value=\u0022STSListControlWPQ3\u0022\u003e\u003cp class=\u0022ms-descriptiontext\u0022\u003eThe Datasheet view of this list cannot be displayed. Please wait while the page is redirected to Standard view. If your list does not appear in a few moments, \u003ca href=\u0022?ShowInGrid=False\u0022 target=_self\u003eopen the list in Standard view\u003c\u002fa\u003e\u003c\u002fp\u003e\u003c\u002fOBJECT\u003e');</script>";

            //                        //Funkcie na ovladanie DataSheetu
            //                        string csScriptFunctions = "<script type='text/javascript'>function WPQ2ShowHideTaskPane() { CoreInvoke('GCShowHideTaskPane', document.STSListControlWPQ3 );} function WPQ2ShowHideTotalsRow() { CoreInvoke('GCShowHideTotalsRow', document.STSListControlWPQ3 );} function WPQ2Refresh() { CoreInvoke('GCRefresh', document.STSListControlWPQ3 );} function WPQ2GridNewRow() { CoreInvoke('GCGridNewRow', document.STSListControlWPQ3 );CoreInvoke('GCActivateAndFocus',document.STSListControlWPQ3);} function WPQ2AddColumn() { CoreInvoke('GCAddNewColumn', document.STSListControlWPQ3 , \"" + spWeb.Url + "\" );} function WPQ2ChangeColumn() { CoreInvoke('GCEditDeleteColumn', document.STSListControlWPQ3 , \"" + spWeb.Url + "\" );} function WPQ2GridNewFolder() { CoreInvoke('GCNewFolder', document.STSListControlWPQ3 );}</script> <script type='text/javascript' for='STSListControlWPQ3' event='onresize'>CoreInvoke('GCOnResizeGridControl',document.STSListControlWPQ3);</script> <script type='text/javascript' for='window' event='onresize'>CoreInvoke('GCWindowResize',document.STSListControlWPQ3);</script> <script type='text/javascript'> function _spBodyOnLoad(){CoreInvoke('GCWindowResize',document.STSListControlWPQ3);CoreInvoke('GCActivateAndFocus',document.STSListControlWPQ3);} </script> <script type='text/javascript' for='document' event='onreadystatechange'>if (document.readyState == 'complete') {if (CoreInvoke('TestGCObject', document.STSListControlWPQ3)) { CoreInvoke('GCActivateAndFocus',document.STSListControlWPQ3);} else { CoreInvoke('GCNavigateToNonGridPage'); } }</script>";

            //                        LiteralControl RenderActiveX = new LiteralControl();
            //                        RenderActiveX.Text = csScript + csScriptFunctions;

            //                        controlParent.Controls.Add(RenderActiveX);
            //                    }
            //                }

            //            }
            //            catch
            //            {
            //                //todo
            //            }
            //        }
            //    }
            //}

            #endregion
        }

        #endregion

        private string EncodeString(string p)
        {
            p = p.Replace("\"", "'");
            p = p.Replace("\\", "\\\\");
            p = p.Replace("\r", "");
            p = p.Replace("\n", "");
            p = p.Replace("+", "\\u002b");
            p = p.Replace("'", "\\u0027");
            p = p.Replace(">", "\\u003e");
            p = p.Replace("<", "\\u003c");
            p = p.Replace("/", "\\u002f");
            p = p.Replace("/", "\\u002f");
            return p;
        }

        public override bool Visible
        {
            [SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
            get
            {
                return this._visible;
            }
        }      

        private List<SPField> FindFieldsInProperties(SPView currentView, SPList currentList)
        {
            try
            {
                SPUser currentUser = SPContext.Current.Web.CurrentUser;
                UserPermission userPermission = new UserPermission();

                List<SPField> listFields = new List<SPField>();
                List<SPField> listFieldsInProperties = new List<SPField>();

                if (currentView.ViewFields.Count != 0)
                {
                    foreach (string viewField in currentView.ViewFields)
                    {
                        SPField spfield = currentList.Fields.GetField(viewField);
                        listFields.Add(spfield);
                    }
                }

                if (listFields.Count != 0)
                {
                    foreach (SPField field in listFields)
                    {
                        //Zatial nastavene iba pre DisplayMode
                        if (userPermission.GetFieldPermissionForCurrentUser(field, "Hidden", SPControlMode.Display, true, currentUser, null))
                        {
                            listFieldsInProperties.Add(field);
                            continue;
                        }

                        //if (UserPermission.GetFieldPermissionFromCurrentUser(field, "Hidden", SPControlMode.New, true, currentUser, null))
                        //{
                        //    listFieldsInProperties.Add(field);
                        //    continue;
                        //}

                        //if (UserPermission.GetFieldPermissionFromCurrentUser(field, "Hidden", SPControlMode.Edit, true, currentUser, null))
                        //{
                        //    listFieldsInProperties.Add(field);
                        //    continue;
                        //}
                    }
                }

                return listFieldsInProperties;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private string HideFieldsInXsltDefinition(List<SPField> spFields, string xsltWebPartDefinition)
        {
            try
            {
                string defaultXmlDefinition = xsltWebPartDefinition;
                List<string> xmlTags = new List<string> { "View/ViewFields", "View/Query/OrderBy", "View/Query/GroupBy", "View/Aggregations" };

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(defaultXmlDefinition);

                foreach (SPField field in spFields)
                {
                    string fieldTitle = field.Title;
                    string fieldStaticName = field.StaticName;

                    #region - Remove Fields From XML -

                    //XmlNodeList xnList = xmlDocument.SelectNodes("View/ViewFields/FieldRef[@Name = '" + fieldTitle + "']");
                    //foreach (XmlNode rowToDelete in xnList)
                    //xmlDocument.DocumentElement.RemoveChild(xmlNode);
                    foreach (string xmlTag in xmlTags)
                    {
                        if (fieldStaticName == "Title" || fieldStaticName == "LinkTitle" || fieldStaticName == "LinkTitleNoMenu")
                        {
                            XmlNode xmlNodeTitle = xmlDocument.SelectSingleNode(xmlTag + "/FieldRef[@Name ='Title']");
                            XmlNode xmlNodeLinkTitle = xmlDocument.SelectSingleNode(xmlTag + "/FieldRef[@Name ='LinkTitle']");
                            XmlNode xmlNodeLinkTitleNoMenu = xmlDocument.SelectSingleNode(xmlTag + "/FieldRef[@Name ='LinkTitleNoMenu']");

                            if (xmlNodeTitle != null)
                                xmlDocument.SelectSingleNode(xmlTag).RemoveChild(xmlNodeTitle);

                            if (xmlNodeLinkTitle != null)
                                xmlDocument.SelectSingleNode(xmlTag).RemoveChild(xmlNodeLinkTitle);

                            if (xmlNodeLinkTitleNoMenu != null)
                                xmlDocument.SelectSingleNode(xmlTag).RemoveChild(xmlNodeLinkTitleNoMenu);
                        }
                        else
                        {
                            XmlNode xmlNode = xmlDocument.SelectSingleNode(xmlTag + "/FieldRef[@Name ='" + fieldStaticName + "']");

                            if (xmlNode != null)
                                xmlDocument.SelectSingleNode(xmlTag).RemoveChild(xmlNode);
                        }

                        XmlNode xmlNodeTag = xmlDocument.SelectSingleNode(xmlTag);

                        if (xmlNodeTag != null)
                            if (xmlNodeTag.ChildNodes.Count == 0)
                                xmlNodeTag.ParentNode.RemoveChild(xmlNodeTag);
                    }

                    #endregion

                }

                return xmlDocument.InnerXml.ToString();
            }
            catch(Exception ex)
            {
                return xsltWebPartDefinition;
            }

             //<View Name="{ECB79B25-C2EC-44DA-A963-C50DF95BE2CB}" DefaultView="TRUE" MobileView="TRUE" MobileDefaultView="TRUE" Type="HTML" DisplayName="Všetky položky" Url="/Lists/List1/AllItems.aspx" Level="1" BaseViewID="1" ContentTypeID="0x" ImageUrl="/_layouts/images/generic.png">
             //   <Query>
             //       <OrderBy>
             //           <FieldRef Name="ID"/>
             //       </OrderBy>
             //   </Query>
             //   <ViewFields>
             //       <FieldRef Name="LinkTitle"/>
             //       <FieldRef Name="Date"/>
             //       <FieldRef Name="People"/>
             //       <FieldRef Name="Title"/>
             //   </ViewFields>
             //   <RowLimit Paged="TRUE">30</RowLimit>
             //   <Aggregations Value="Off"/>
             //   <Toolbar Type="Standard"/>
             //</View>

        }

        private Control findControl(Control parent, string id)
        {
            if (parent.HasControls())
            {
                Control control = parent.FindControl(id);
                if (control != null)
                {
                    return control;
                }
                foreach (Control control2 in parent.Controls)
                {
                    control = this.findControl(control2, id);
                    if (control != null)
                    {
                        return control;
                    }
                }
            }
            return null;
        }

        private List<XsltListViewWebPart> GetListViewWebParts()
        {
            List<XsltListViewWebPart> xsltListWebPartCollecion = new List<XsltListViewWebPart>();

            WebPartManager currentWebPartManager = WebPartManager.GetCurrentWebPartManager(this.Page);
            if (currentWebPartManager != null)
            {
                foreach (System.Web.UI.WebControls.WebParts.WebPart webPpart in currentWebPartManager.WebParts)
                {
                    XsltListViewWebPart xsltWebPart = webPpart as XsltListViewWebPart;
                    if (xsltWebPart != null)
                    {
                        xsltListWebPartCollecion.Add(xsltWebPart);
                    }
                }

                return xsltListWebPartCollecion;
            }
            return null;
        }

        #region - Custom Methods -

        //Ci dany list ma konkretne View
        public bool HasView(SPList list, string viewName)
        {
            if (string.IsNullOrEmpty(viewName))
                return false;
            foreach (SPView view in list.Views)
                if (view.Title.ToLowerInvariant() == viewName.ToLowerInvariant())
                    return true;
            return false;
        }


        //Zistenie vsetkych controlov na stranke
        private void ListControlCollections()
        {
            ArrayList controlList = new ArrayList();
            AddControls(Page.Controls, controlList);

            string control = "";
            string countControls = "";

            foreach (string str in controlList)
            {
                control = str;
            }

            countControls = controlList.Count.ToString();
        }

        private void AddControls(ControlCollection page, ArrayList controlList)
        {
            foreach (Control c in page)
            {
                if (c.ID != null)
                {
                    controlList.Add(c.ID);
                }
                if (c.HasControls())
                {
                    AddControls(c.Controls, controlList);
                }
            }
        }

        


        #endregion
    }

    #region - Custom Code -

    //[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal), SharePointPermission(SecurityAction.InheritanceDemand, ObjectModel = true), AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal), SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
    //public class PageHeadDelegateControl : WebControl
    //{
    //    private bool _incompatibleUIVersion;
    //    private bool _ltvsmAlreadyPresent;
    //    private string _trace = "";
    //    private bool _visible;

    //    [SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
    //    protected override void CreateChildControls()
    //    {
    //        try
    //        {
    //            if (!this._ltvsmAlreadyPresent && !this._incompatibleUIVersion)
    //            {
    //                XsltListViewWebPart part = this.getFirstListViewWebPart();
    //                if (part == null)
    //                {
    //                    this._visible = false;
    //                }
    //                else
    //                {
    //                    Control control = this.findControl(this.Page, "PlaceHolderPageTitleInTitleArea");
    //                    if (control == null)
    //                    {
    //                        this._visible = false;
    //                    }
    //                    else
    //                    {
    //                        Image child = new Image();
    //                        child.Attributes.Add("style", "POSITION: absolute; BORDER-RIGHT-WIDTH: 0px; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; TOP: -585px !important; LEFT: 0px !important");
    //                        child.ImageUrl = "/_layouts/images/fgimg.png";
    //                        HtmlGenericControl control2 = new HtmlGenericControl("span");
    //                        control2.Attributes.Add("style", "POSITION: relative; WIDTH: 11px; DISPLAY: inline-block; HEIGHT: 11px; OVERFLOW: hidden");
    //                        control2.Controls.Add(child);
    //                        HtmlGenericControl control3 = new HtmlGenericControl("span");
    //                        control3.Controls.Add(control2);
    //                        HtmlGenericControl control4 = new HtmlGenericControl("span")
    //                        {
    //                            ID = "pl-onetidPageTitleSeparator"
    //                        };
    //                        control4.Controls.Add(control3);
    //                        SPContext renderContext = SPContext.GetContext(HttpContext.Current, part.ViewGuid, part.ListId, SPContext.Current.Web);
    //                        CustomViewSelectorMenu menu = new CustomViewSelectorMenu(renderContext);
    //                        SPView view = menu.View;
    //                        if ((view != null) && string.IsNullOrEmpty(view.Title))
    //                        {
    //                            view.Title = renderContext.List.Title;
    //                        }
    //                        HtmlGenericControl control5 = new HtmlGenericControl("span");
    //                        control5.Attributes.Add("class", "ms-ltviewselectormenuheader");
    //                        control5.Controls.Add(control4);
    //                        control5.Controls.Add(menu);
    //                        control.Controls.Add(control5);
    //                    }
    //                }
    //            }
    //        }
    //        catch (Exception exception)
    //        {
    //            this._trace = this._trace + exception.ToString();
    //        }
    //        base.CreateChildControls();
    //    }

    //    private Control findControl(Control parent, string id)
    //    {
    //        if (parent.HasControls())
    //        {
    //            Control control = parent.FindControl(id);
    //            if (control != null)
    //            {
    //                return control;
    //            }
    //            foreach (Control control2 in parent.Controls)
    //            {
    //                control = this.findControl(control2, id);
    //                if (control != null)
    //                {
    //                    return control;
    //                }
    //            }
    //        }
    //        return null;
    //    }

    //    private XsltListViewWebPart getFirstListViewWebPart()
    //    {
    //        WebPartManager currentWebPartManager = WebPartManager.GetCurrentWebPartManager(this.Page);
    //        if (currentWebPartManager != null)
    //        {
    //            foreach (System.Web.UI.WebControls.WebParts.WebPart part in currentWebPartManager.WebParts)
    //            {
    //                XsltListViewWebPart part2 = part as XsltListViewWebPart;
    //                if (part2 != null)
    //                {
    //                    return part2;
    //                }
    //            }
    //        }
    //        return null;
    //    }

    //    [SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
    //    protected override void OnLoad(EventArgs e)
    //    {
    //        try
    //        {
    //            SPWeb contextWeb = SPControl.GetContextWeb(this.Context);
    //            if ((contextWeb != null) && (contextWeb.UIVersion <= 3))
    //            {
    //                this._incompatibleUIVersion = true;
    //                return;
    //            }
    //            Type baseType = this.Page.GetType().BaseType;
    //            if ((baseType == typeof(WebPartPage)) || (baseType == typeof(Page)))
    //            {
    //                Control control = this.findControl(this.Page, "LTViewSelectorMenu");
    //                if (control != null)
    //                {
    //                    if (!control.Visible)
    //                    {
    //                        typeof(ListTitleViewSelectorMenu).GetField("m_wpSingleInit", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(control, true);
    //                        typeof(ListTitleViewSelectorMenu).GetField("m_wpSingle", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(control, true);
    //                    }
    //                    this._ltvsmAlreadyPresent = true;
    //                }
    //                else
    //                {
    //                    this._visible = true;
    //                }
    //            }
    //            else if (baseType == typeof(WikiEditPage))
    //            {
    //                this._visible = true;
    //            }
    //        }
    //        catch (Exception exception)
    //        {
    //            this._trace = this._trace + exception.ToString();
    //        }
    //        base.OnLoad(e);
    //    }

    //    [SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
    //    protected override void Render(HtmlTextWriter writer)
    //    {
    //        if (!string.IsNullOrEmpty(this._trace))
    //        {
    //            writer.Write(this._trace);
    //        }
    //        base.Render(writer);
    //    }

    //    public override bool Visible
    //    {
    //        [SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
    //        get
    //        {
    //            return this._visible;
    //        }
    //    }
    //}

    #endregion
}
