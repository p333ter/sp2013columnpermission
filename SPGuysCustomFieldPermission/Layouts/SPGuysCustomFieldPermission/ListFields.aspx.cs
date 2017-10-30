using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Text;
using SPGuysCustomFieldPermissionLibrary;

namespace SPGuysCustomFieldPermission.Layouts.SPGuysCustomFieldPermission
{
    public partial class ListFields : LayoutsPageBase
    {
        Guid listGuid;
        SPList currentList;

        SPContext spContext = SPContext.Current;
        SPSite currentSite = SPContext.Current.Site;
        SPWeb spWeb = SPContext.Current.Web;

        string currentListSettingsLink;

        string separatorFirst = " <span class=\"s4-nothome s4-bcsep s4-titlesep\" id=\"onetidPageTitleSeparator\"><span><span style=\"POSITION: relative; WIDTH: 11px; DISPLAY: inline-block; HEIGHT: 11px; OVERFLOW: hidden\"><IMG style=\"POSITION: absolute; BORDER-RIGHT-WIDTH: 0px; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; TOP: -585px !important; LEFT: 0px !important\" alt=: src=\"/_layouts/images/fgimg.png\"></span></span></span> ";
        string separatorNext = " <span><SPAN style=\"POSITION: relative; WIDTH: 11px; DISPLAY: inline-block; HEIGHT: 11px; OVERFLOW: hidden\"><IMG style=\"POSITION: absolute; BORDER-RIGHT-WIDTH: 0px; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; TOP: -585px !important; LEFT: 0px !important\" alt=: src=\"/_layouts/images/fgimg.png\"></SPAN></span> ";

        protected void Page_Load(object sender, EventArgs e) {
            SAVEbutton1.Click += new EventHandler(SAVEbutton1_Click);

            if (Request.QueryString["List"] != null) {
                listGuid = new Guid(Request.QueryString["List"]);
                currentList = spWeb.Lists[listGuid];

                currentListSettingsLink = spWeb.Url + "/_layouts/listedit.aspx?List=" + currentList.ID.ToString();

                if (CheckCustomRights(currentList)) {
                    Control tagTitleControl = Page.Header.FindControl("PlaceHolderPageTitle").Parent;
                    if (tagTitleControl != null)
                        AddTagTitle(tagTitleControl);

                    Control titleLinkControl = FindInnerControl(Page, "PlaceHolderPageTitleInTitleArea");
                    if (titleLinkControl != null)
                        AddLinkTitle(titleLinkControl);

                    PageDescriptionText.Text = "Column Permission (Powered by SPGuys)";

                    GenerateInnerHtml();

                    CancelButton1.PostBackUrl = currentListSettingsLink;

                    if (!Page.IsPostBack) {
                        if (SelectViceVersaFromProperties(currentSite, currentList, false))
                            this.chbxPermission.Checked = true;
                        else
                            this.chbxPermission.Checked = false;
                    }
                } else
                    SPUtility.HandleAccessDenied(new UnauthorizedAccessException());
            } else
                SPUtility.HandleAccessDenied(new UnauthorizedAccessException());
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


        void SAVEbutton1_Click(object sender, EventArgs e) {
            if (this.chbxPermission.Checked)
                SaveViceVersaToProperties(currentList, false);
            else
                SaveViceVersaToProperties(currentList, true);

            Close();
        }

        private void Close() {
            Page.Response.Redirect(currentListSettingsLink, true);
        }


        private void SaveViceVersaToProperties(SPList currentList, bool clear) {
           try {
               StringBuilder propertyValue = new StringBuilder();
               propertyValue.Append(Definitions.prefix + Definitions.separator + "Fields" + Definitions.separator);
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

        private void GenerateInnerHtml()
        {
            StringBuilder innerHtmlBuilder = new StringBuilder();

            SPFieldCollection fieldCol = currentList.Fields;
            foreach (SPField field in fieldCol)
            {
                if (!field.Hidden && !field.ReadOnlyField && field.Type != SPFieldType.Attachments && field.StaticName != "ContentType")
                //if (!field.FromBaseType || field.StaticName == "Title")
                {
                    innerHtmlBuilder.AppendLine("<tr>");
                    innerHtmlBuilder.AppendLine("<td class=\"ms-vb2\">");
                    innerHtmlBuilder.AppendLine("<a id=\"LinkEditField1\" href=\"#\" onclick=\"OpenEditDialog('" + currentList.ID + "','" + field.Id + "');\">" + field.Title + "</a>");
                    innerHtmlBuilder.AppendLine("</td>");

                    if (checkFieldPermission(field))
                    {
                        innerHtmlBuilder.AppendLine("<td class=\"ms-vb2\" colspan=\"2\">");
                        innerHtmlBuilder.AppendLine("<img alt=\"Checked\" src=\"/_layouts/images/check.gif\">");
                        innerHtmlBuilder.AppendLine("</td>");                    
                    }
                    else
                    {
                        innerHtmlBuilder.AppendLine("<td class=\"ms-vb2\" colspan=\"2\">");
                        innerHtmlBuilder.AppendLine("</td>");    
                    }

                    innerHtmlBuilder.AppendLine("</tr>");
                    
                }

                /* function showSPDialog(pageUrl) {
                                                var options = { url: pageUrl, width: 400, height: 300 };
                                                SP.SOD.execute('sp.ui.dialog.js', 'SP.UI.ModalDialog.showModalDialog', options);
                                            } */


                /*
                 
                 
                                                function OpenEditDialog(list_id, field_id){
                                                    var options = {  
                                                        url:'FieldPermission.aspx?List=' + list_id + '&Field=' + field_id + '&IsDlg=1',  
                                                        width: 600,  
                                                        height: 630,  
                                                        dialogReturnValueCallback: DialogCallback
                                                    };  
                                                    SP.UI.ModalDialog.showModalDialog(options);
                                                } 
                 * */



                innerHtmlBuilder.AppendLine(@"<script type='text/javascript'>

                                                var statusId = '';
                                            
                                               function OpenEditDialog(list_id, field_id){
                                                    var options = {  
                                                        url:'FieldPermission.aspx?List=' + list_id + '&Field=' + field_id + '&IsDlg=1',  
                                                        width: 600,  
                                                        height: 630,  
                                                        dialogReturnValueCallback: DialogCallback
                                                    };  
                                                    SP.SOD.execute('sp.ui.dialog.js', 'SP.UI.ModalDialog.showModalDialog', options);
                                                }
                                                
                                                function DialogCallback(dialogResult, returnValue)
                                                {  
                                       
                                                    if (returnValue == '1') {
                                                        SP.UI.Notify.addNotification('Field Permission Changed.', false);
                                                        SP.UI.ModalDialog.RefreshPage(SP.UI.DialogResult.OK);

                                                        /* statusId = SP.UI.Status.addStatus('Field Permission Changed.','', true);
                                                        SP.UI.Status.setStatusPriColor(statusId, 'blue'); */

                                                    }

                                                   /* setTimeout(RemoveStatus, 2000); */
             
                                                }

                                                function RemoveStatus() {
                                                    window.location = window.location;
                                                }
                                        
                                              </script>");
 
                //!SPBuiltInFieldId.Contains(field.Id)
                //string fieldTypeClass = field.FieldTypeDefinition.FieldTypeClass;  
                //if (!(string.IsNullOrEmpty(fieldTypeClass) || fieldTypeClass.StartsWith("Microsoft.SharePoint"))) 
                //{ 
                //    //Only custom fields here
                //    innerHtmlBuilder.Append("<br />");
                //    innerHtmlBuilder.AppendLine(field.Title);
                //} 
            }

            innerTable.Text = innerHtmlBuilder.ToString();
        }
        
        private bool checkFieldPermission(SPField currentField)
        {
            UserPermission userPerm = new UserPermission();
            if (userPerm.GetValueFromProperty(currentField, "Hidden", SPControlMode.Display, true) != String.Empty)
                return true;

            if (userPerm.GetValueFromProperty(currentField, "Hidden", SPControlMode.Edit, true) != String.Empty)
                return true;

            if (userPerm.GetValueFromProperty(currentField, "Hidden", SPControlMode.New, true) != String.Empty)
                return true;

            if (userPerm.GetValueFromProperty(currentField, "ReadOnly", SPControlMode.Display, true) != String.Empty)
                return true;

            if (userPerm.GetValueFromProperty(currentField, "ReadOnly", SPControlMode.Edit, true) != String.Empty)
                return true;

            if (userPerm.GetValueFromProperty(currentField, "ReadOnly", SPControlMode.New, true) != String.Empty)
                return true;

            return false;
        }

        

        private void AddTagTitle(Control tagTitle)
        {
            HtmlTitle htmlTagTitle = new HtmlTitle();
            htmlTagTitle.Text = "Column Permission";
            
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
                string currentPermissionName = "Column Permission";

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

        private bool CheckCustomRights(SPList currentList)    {

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
