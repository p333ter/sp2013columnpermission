using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;
using System.Web.UI;
using System.Text;
using System.Web.UI.HtmlControls;
using SPGuysCustomFieldPermissionLibrary;
using System.Web;
using System.Web.UI.WebControls;

namespace SPGuysCustomFieldPermission.Layouts
{
    public partial class ListViews : LayoutsPageBase
    {
        Guid listGuid;
        SPList currentList;

        SPContext spContext = SPContext.Current;
        SPWeb spWeb = SPContext.Current.Web;
        SPSite currentSite = SPContext.Current.Site;

        string separatorFirst = " <span class=\"s4-nothome s4-bcsep s4-titlesep\" id=\"onetidPageTitleSeparator\"><span><span style=\"POSITION: relative; WIDTH: 11px; DISPLAY: inline-block; HEIGHT: 11px; OVERFLOW: hidden\"><IMG style=\"POSITION: absolute; BORDER-RIGHT-WIDTH: 0px; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; TOP: -585px !important; LEFT: 0px !important\" alt=: src=\"/_layouts/images/fgimg.png\"></span></span></span> ";
        string separatorNext = " <span><SPAN style=\"POSITION: relative; WIDTH: 11px; DISPLAY: inline-block; HEIGHT: 11px; OVERFLOW: hidden\"><IMG style=\"POSITION: absolute; BORDER-RIGHT-WIDTH: 0px; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; TOP: -585px !important; LEFT: 0px !important\" alt=: src=\"/_layouts/images/fgimg.png\"></SPAN></span> ";


        protected void Page_Load(object sender, EventArgs e)
        {
            SAVEbutton1.Click += new EventHandler(SAVEbutton1_Click);
            CancelButton1.Click += new EventHandler(CancelButton1_Click);

                if (Request.QueryString["List"] != null)
                {
                    listGuid = new Guid(Request.QueryString["List"]);
                    currentList = spWeb.Lists[listGuid];

                    if (CheckCustomRights(currentList))
                    {
                        Control tagTitleControl = Page.Header.FindControl("PlaceHolderPageTitle").Parent;
                        if (tagTitleControl != null)
                            AddTagTitle(tagTitleControl);

                        Control titleLinkControl = FindInnerControl(Page, "PlaceHolderPageTitleInTitleArea");
                        if (titleLinkControl != null)
                            AddLinkTitle(titleLinkControl);

                        PageDescriptionText.Text = "View Permission (Powered by SPGuys)";

                        GenerateInnerHtml();

                        if (!Page.IsPostBack)
                        {
                            SPViewCollection viewCollection = currentList.Views;
                            ViewState["CheckViewsCount"] = viewCollection.Count.ToString();

                            int i = 0;
                            foreach (SPView currentView in viewCollection)
                            {
                                if (currentView.Title != "")
                                {
                                    Control peopleEditorControl = MainPanel.FindControl("pe" + i.ToString());
                                    if (peopleEditorControl != null)
                                    {
                                        PeopleEditor pe = (PeopleEditor)peopleEditorControl;
                                        pe.CommaSeparatedAccounts = SelectFromProperties(currentView);
                                    }

                                    i++;
                                }

                                
                            }

                            if (SelectViceVersaFromProperties(currentSite, currentList, true))
                                this.chbxPermission.Checked = true;
                            else
                                this.chbxPermission.Checked = false;


                        }
                    }
                    else
                        SPUtility.HandleAccessDenied(new UnauthorizedAccessException());
                }
                else
                    SPUtility.HandleAccessDenied(new UnauthorizedAccessException());
        }

        void CancelButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        void SAVEbutton1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SPViewCollection viewCollection = currentList.Views;
                if (viewCollection.Count.ToString() == ViewState["CheckViewsCount"].ToString())
                {
                    int i = 0;
                    foreach (SPView currentView in viewCollection)
                    {
                        if (currentView.Title != "")
                        {
                            Control peopleEditorControl = MainPanel.FindControl("pe" + i.ToString());
                            if (peopleEditorControl != null)
                            {
                                PeopleEditor pe = (PeopleEditor)peopleEditorControl;

                                //if (pe.IsValid == false && pe.Entities.Count != 0)
                                //    return;

                                SaveToProperties(currentView, pe.CommaSeparatedAccounts, pe.Entities.Count);
                            }

                            i++;
                        }
                    }


                    if (this.chbxPermission.Checked)
                        SaveViceVersaToProperties(currentList, false);
                    else
                        SaveViceVersaToProperties(currentList, true);

                    Close();
                }
                else
                {
                    ViewState["CheckViewsCount"] = viewCollection.Count.ToString();
                    ClientScript.RegisterStartupScript(this.GetType(), "StatusScript",
                                                        @"<script type='text/javascript'>ExecuteOrDelayUntilScriptLoaded(Initialize, 'sp.js');
                                                        function Initialize() {
                                                            statusId = SP.UI.Status.addStatus('Please Save this Page Again. Incorrect number of Views!','', true); 
                                                            SP.UI.Status.setStatusPriColor(statusId, 'red');
                                                            }</script>");
                }
            } 
        }

        private string SelectFromProperties(SPView currentView)
        {
            try
            {
                Guid currentViewId = currentView.ID;
                Guid currentListId = currentView.ParentList.ID;

                StringBuilder propertyValue = new StringBuilder();
                propertyValue.Append(Definitions.prefix + Definitions.separator);
                propertyValue.Append(currentListId + Definitions.separator);
                propertyValue.Append(currentViewId);

                //SPGuys_ListId_ViewId 
                string propertyToSite = propertyValue.ToString();

                if (currentSite.RootWeb.AllProperties.ContainsKey(propertyToSite))
                    return currentSite.RootWeb.AllProperties[propertyToSite].ToString();
                else
                    return String.Empty;

            }
            catch (Exception ex)
            {
                errorText.Text = "Error on Load View:<br />" + ex.Message;
                return String.Empty;
            }
        }


        private void SaveToProperties(SPView currentView, string value, int clear)
        {
            try
            {
                Guid currentViewId = currentView.ID;
                Guid currentListId = currentView.ParentList.ID;

                StringBuilder propertyValue = new StringBuilder();
                propertyValue.Append(Definitions.prefix + Definitions.separator);
                propertyValue.Append(currentListId + Definitions.separator);
                propertyValue.Append(currentViewId);

                //SPGuys_ListId_ViewId 
                string propertyToSite = propertyValue.ToString();

                if (clear == 0)
                    currentSite.RootWeb.AllProperties.Remove(propertyToSite);
                else
                {
                    if (currentSite.RootWeb.AllProperties.ContainsKey(propertyToSite))
                    {
                        currentSite.RootWeb.AllProperties[propertyToSite] = value;
                    }
                    else
                        currentSite.RootWeb.AllProperties.Add(propertyToSite, value);
                }

                currentSite.RootWeb.Update();
                currentSite.RootWeb.Properties.Update();
            }
            catch (Exception ex)
            {
                errorText.Text = "Error on Save View:<br />" + ex.Message;
            }
        }

        public bool SelectViceVersaFromProperties(SPSite currentSite, SPList currentList, bool isView) {

            string typeP;

            if (isView)
                typeP = "Views";
            else
                typeP = "Fields";

            StringBuilder propertyValue = new StringBuilder();
            propertyValue.Append(Definitions.prefix + Definitions.separator + typeP + Definitions.separator);
            propertyValue.Append(currentList.ID);

            string propertyToSite = propertyValue.ToString();

            if (currentSite.RootWeb.AllProperties.ContainsKey(propertyToSite))
                return true;
            else
                return false;
        }

        private void SaveViceVersaToProperties(SPList currentList, bool clear) {
            try {
                StringBuilder propertyValue = new StringBuilder();
                propertyValue.Append(Definitions.prefix + Definitions.separator + "Views" + Definitions.separator);
                propertyValue.Append(currentList.ID);

                string propertyToSite = propertyValue.ToString();

                if (clear)
                    currentSite.RootWeb.AllProperties.Remove(propertyToSite);
                else {
                    currentSite.RootWeb.AllProperties.Add(propertyToSite, "viceversa");
                }

                currentSite.RootWeb.Update();
                currentSite.RootWeb.Properties.Update();
            } catch (Exception ex) {
                //ToDo
            }
        }

        private void Close()
        {
            ViewState["CheckViewsCount"] = null;
            string currentListSettingsLink = spWeb.Url + "/_layouts/listedit.aspx?List=" + currentList.ID.ToString();
            Page.Response.Redirect(currentListSettingsLink, true);
        }

        private void GenerateInnerHtml()
        {
            StringBuilder innerHtml0 = new StringBuilder();
            innerHtml0.Remove(0, innerHtml0.Length);

            SPViewCollection viewCollection = currentList.Views;

            innerHtml0.AppendLine("<table width='99%' class='ms-v4propertysheetspacing' border='0' cellSpacing='0' cellPadding='0'>");
            innerHtml0.AppendLine("<tbody>");
            innerHtml0.AppendLine(@"");
          

            LiteralControl nullSection = new LiteralControl(innerHtml0.ToString());
            MainPanel.Controls.Add(nullSection);

            int i = 0;

            foreach (SPView currentView in viewCollection)
            {
                if (currentView.Title != "")
                {

                    StringBuilder innerHtml = new StringBuilder();
                    innerHtml.Remove(0, innerHtml.Length);
                    innerHtml.AppendLine("<td class='ms-sectionline' height='1' colSpan='2'><img alt='' src='/_layouts/images/blank.gif' width='1' height='1'></td>");
                    innerHtml.AppendLine(@"
                    <tr>
                    <td class='ms-descriptiontext' vAlign='top'>
                        <table border='0' cellpadding='1' cellspacing='0' width='100%'>
                            <tr>
                                <td class='ms-sectionheader' style='padding-top: 4px;' height='22' valign='top'>
                                    <h3 class='ms-standardheader ms-inputformheader'>
                                        " + currentView.Title  + @"
                                    </h3>
                                </td>
                            </tr>
                            <tr>
                                <td class='ms-descriptiontext ms-inputformdescription'>
                                        You can enter User names, Group names, Active Directory group names or e-mail addresses <b>which will not have the permission for this View</b>.
                                        <br />Separate them with semicolons.
                                        <br />
                                </td>
                                <td>
                                    <img src='/_layouts/images/blank.gif' width='8' height='1' alt='' />
                                </td>
                            </tr>
                        </table>
                    </td>");


                    innerHtml.AppendLine(@"<td class='ms-authoringcontrols ms-inputformcontrols' valign='top' align='left' width='40%'>");
                    innerHtml.AppendLine(@"
                <table border='0' width='100%' cellspacing='0' cellpadding='0'>
                    <tr>
                        <td width='9px'>
                            <img src='/_layouts/images/blank.gif' width='9' height='7' alt='' />
                        </td>
                        <td>
                            <img src='/_layouts/images/blank.gif' width='150' height='7' alt='' />
                        </td>
                        <td width='10px'>
                            <img src='/_layouts/images/blank.gif' width='10' height='1' alt='' />
                        </td>
                    </tr>
                    <tr>
                        <td />
                        <td class='ms-authoringcontrols'>
                            <table class='ms-authoringcontrols' border='0' width='100%' cellspacing='0' cellpadding='0'>
                                <tr>
                                    <td class='ms-authoringcontrols' colspan='2'>
                                        <span>Users/Groups:</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <img src='/_layouts/images/blank.gif' width='1' height='3' style='display: block'
                                            alt='' />
                                    </td>
                                </tr>
                                <tr>
                                    <td align='left' class='' valign='top'>");

                    LiteralControl firstSection = new LiteralControl(innerHtml.ToString());
                    MainPanel.Controls.Add(firstSection);

                    PeopleEditor peopleEditor = new PeopleEditor();
                    peopleEditor.ID = "pe" + i.ToString();

                    peopleEditor.SelectionSet = "User,DL,SecGroup,SPGroup";
                    peopleEditor.AllowEmpty = true;
                    peopleEditor.Rows = 3;
                    peopleEditor.ValidatorEnabled = true;
                    peopleEditor.MultiSelect = true;
                    
                    MainPanel.Controls.Add(peopleEditor);
                    
                    StringBuilder innerHtml2 = new StringBuilder();
                    innerHtml2.Remove(0, innerHtml2.Length);
                    innerHtml2.AppendLine(@"
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        </td>
                       </tr>");
                    LiteralControl secondSection = new LiteralControl(innerHtml2.ToString());
                    MainPanel.Controls.Add(secondSection);

                    i++;
                }
            }

            StringBuilder innerHtml3 = new StringBuilder();
            innerHtml3.Remove(0, innerHtml3.Length);
            innerHtml3.AppendLine("</tbody>");
            innerHtml3.AppendLine("</table>");

            LiteralControl thirdSection = new LiteralControl(innerHtml3.ToString());
            MainPanel.Controls.Add(thirdSection);

        }

       
        private void AddTagTitle(Control tagTitle)
        {
            HtmlTitle htmlTagTitle = new HtmlTitle();
            htmlTagTitle.Text = "View Permission";

            //Iny sposob zmeny

            Page.Header.Controls.Remove(Page.Header.FindControl("PlaceHolderPageTitle").Parent);
            Page.Header.Controls.Add(htmlTagTitle);
        }

        private void AddLinkTitle(Control titleLinks)
        {
            try
            {
                StringBuilder newLinksBuilder = new StringBuilder();

                //Zistenie nastavenie jazyka konkretnej WEB
                string locSettingsWeb = SPUtility.GetLocalizedString("$Resources:listsettings", "core", spWeb.Language);

                //Zistenie nastavenie jazyka prihlaseneho uzivatela
                string locSettingsUser = SPUtility.GetLocalizedString("$Resources:listsettings", "core", (uint)System.Threading.Thread.CurrentThread.CurrentUICulture.LCID);

                string currentListLink = " <a href=\"" + currentList.DefaultViewUrl + "\">" + currentList.Title + "</a>";
                string currentListSettingsLink = " <a href=\"" + spWeb.Url + "/_layouts/listedit.aspx?List=" + currentList.ID.ToString() + "\">" + locSettingsUser + "</a>";
                string currentPermissionName = "View Permission";

                newLinksBuilder.AppendLine(currentListLink);
                newLinksBuilder.AppendLine(separatorNext);
                newLinksBuilder.AppendLine(currentListSettingsLink);
                newLinksBuilder.AppendLine(separatorNext);
                newLinksBuilder.AppendLine(currentPermissionName);

                LiteralControl newLinks = new LiteralControl(newLinksBuilder.ToString());
                titleLinks.Controls.Add(newLinks);

            }
            catch (Exception ex)
            {

            }
        }

        private Control FindInnerControl(Control parent, string id)
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
                    control = this.FindInnerControl(control2, id);
                    if (control != null)
                    {
                        return control;
                    }
                }
            }
            return null;
        }

        private bool CheckCustomRights(SPList currentList)
        {
            //SPUser currentUser = spWeb.SiteUsers[HttpContext.Current.User.Identity.Name.ToString()];
            SPUser currentUser = spWeb.CurrentUser;
            bool userPermission = currentList.DoesUserHavePermissions(currentUser, SPBasePermissions.ManageLists);

            if (userPermission)
                return true;
            else
                return false;
        }
    }
}
