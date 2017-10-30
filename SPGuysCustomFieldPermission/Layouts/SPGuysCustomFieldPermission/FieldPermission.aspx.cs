using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web;
using Microsoft.SharePoint.Utilities;
using System.Text;

namespace SPGuysCustomFieldPermission.Layouts.SPGuysCustomFieldPermission
{
    public partial class FieldPermission : LayoutsPageBase
    {
        Guid listGuid;
        Guid fieldGuid;

        SPList currentList;
        SPField currentField;

        SPContext spContext = SPContext.Current;
        SPSite currentSite = SPContext.Current.Site;
        SPWeb spWeb = SPContext.Current.Web;

        protected void Page_Load(object sender, EventArgs e)
        {
            SAVEbutton1.Click += new EventHandler(SAVEbutton1_Click);
            CancelButton1.Click += new EventHandler(CancelButton1_Click);
            
                if (Request.QueryString["List"] != null && Request.QueryString["Field"] != null)
                {
                    listGuid = new Guid(Request.QueryString["List"]);
                    fieldGuid = new Guid(Request.QueryString["Field"]);

                    currentList = spWeb.Lists[listGuid];
                    currentField = spWeb.Lists[listGuid].Fields[fieldGuid];

                    lPageTitle.Text = currentField.Title;
                    LPageTitleInTitleArea.Text = currentField.Title;

                    if (CheckCustomRights(currentList))
                    {
                        if (!Page.IsPostBack)
                        {
                            //Nastavime hodnoty do PeoplePickerov
                            peopleEditorNewReadOnly.CommaSeparatedAccounts = SelectFromProperties(currentList, currentField, SPControlMode.New, "ReadOnly");
                            peopleEditorNewHidden.CommaSeparatedAccounts = SelectFromProperties(currentList, currentField, SPControlMode.New, "Hidden");

                            peopleEditorEditReadOnly.CommaSeparatedAccounts = SelectFromProperties(currentList, currentField, SPControlMode.Edit, "ReadOnly");
                            peopleEditorEditHidden.CommaSeparatedAccounts = SelectFromProperties(currentList, currentField, SPControlMode.Edit, "Hidden");

                            peopleEditorDisplayReadOnly.CommaSeparatedAccounts = SelectFromProperties(currentList, currentField, SPControlMode.Display, "ReadOnly");
                            peopleEditorDisplayHidden.CommaSeparatedAccounts = SelectFromProperties(currentList, currentField, SPControlMode.Display, "Hidden");

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
            CloseModalDialog(false);
        }

        void SAVEbutton1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SaveToProperties(currentList, currentField, SPControlMode.New, "ReadOnly", peopleEditorNewReadOnly.CommaSeparatedAccounts, peopleEditorNewReadOnly.Entities.Count);
                SaveToProperties(currentList, currentField, SPControlMode.New, "Hidden", peopleEditorNewHidden.CommaSeparatedAccounts, peopleEditorNewHidden.Entities.Count);
                SaveToProperties(currentList, currentField, SPControlMode.Edit, "ReadOnly", peopleEditorEditReadOnly.CommaSeparatedAccounts, peopleEditorEditReadOnly.Entities.Count);
                SaveToProperties(currentList, currentField, SPControlMode.Edit, "Hidden", peopleEditorEditHidden.CommaSeparatedAccounts, peopleEditorEditHidden.Entities.Count);
                SaveToProperties(currentList, currentField, SPControlMode.Display, "ReadOnly", peopleEditorDisplayReadOnly.CommaSeparatedAccounts, peopleEditorDisplayReadOnly.Entities.Count);
                SaveToProperties(currentList, currentField, SPControlMode.Display, "Hidden", peopleEditorDisplayHidden.CommaSeparatedAccounts, peopleEditorDisplayHidden.Entities.Count);

                CloseModalDialog(true);
            }
        }


        private void CloseModalDialog(bool status)
        {
            HttpContext context = HttpContext.Current;
            if (HttpContext.Current.Request.QueryString["IsDlg"] != null)
            {
                int returnValue = 0;

                this.Page.Response.Clear();
                if (status)
                    returnValue = 1;

                this.Page.Response.Write("<script type='text/javascript'>window.frameElement.commonModalDialogClose(1, " + returnValue + ");</script>"); 
                this.Page.Response.End();
                //context.Response.Write("<script type='text/javascript'>window.frameElement.commitPopup()</script>");
                //context.Response.Flush();
                //context.Response.End();
            }
        }


        private string SelectFromProperties(SPList currentList, SPField currentField, SPControlMode controlMode, string fieldStatus)
        {
            try
            {
                StringBuilder propertyValue = new StringBuilder();
                propertyValue.Append(Definitions.prefix + Definitions.separator);
                propertyValue.Append(currentList.ID + Definitions.separator);
                propertyValue.Append(currentField.Id + Definitions.separator);
                propertyValue.Append(controlMode.ToString() + Definitions.separator);
                propertyValue.Append(fieldStatus);

                string propertyToSite = propertyValue.ToString();

                if (currentSite.RootWeb.AllProperties.ContainsKey(propertyToSite))
                    return currentSite.RootWeb.AllProperties[propertyToSite].ToString();
                else
                    return String.Empty;
            }
            catch (Exception ex)
            {
                errorText.Text = "Error on Load Column:<br />" + ex.Message;
                return String.Empty;
            }

        }

        private void SaveToProperties(SPList currentList, SPField currentField, SPControlMode controlMode, string fieldStatus, string value, int clear)
        {
            try
            {
                StringBuilder propertyValue = new StringBuilder();
                propertyValue.Append(Definitions.prefix + Definitions.separator);
                propertyValue.Append(currentList.ID + Definitions.separator);
                propertyValue.Append(currentField.Id + Definitions.separator);
                propertyValue.Append(controlMode.ToString() + Definitions.separator);
                propertyValue.Append(fieldStatus);

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
                errorText.Text = "Error on Save Column:<br />" + ex.Message;
            }
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
