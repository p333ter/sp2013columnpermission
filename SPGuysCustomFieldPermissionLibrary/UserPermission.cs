using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;

namespace SPGuysCustomFieldPermissionLibrary
{
    public class UserPermission
    {

        #region - Definitions -

        private SPWeb currentWeb;
        private SPSite currentSite; 
        private SPUser currentUser;

        private List<string> usersAndGroups = new List<string>();
        private List<string> sharepointUsers = new List<string>();
        private List<string> sharepointGroups = new List<string>();
        private List<string> activedirectoriesGroups = new List<string>();
        private List<string> innerADGroups = new List<string>();

        #endregion

        #region - Custom Methods -

        public bool  GetFieldPermissionForCurrentUser(SPField field, string fieldStatus, SPControlMode controlFormMode, bool isViewOrAlert, SPUser spUser, SPSite spSite)
        {
            try {
                //MUSIM to robit pretoze SPContext je NULL ked sa jedna o alert pretoze predsa timerjob spusta alerty!
                if (spSite == null) {
                    currentSite = SPContext.Current.Site;
                    currentWeb = SPContext.Current.Web;
                } else {
                    currentSite = spSite;
                    currentWeb = currentSite.OpenWeb();
                }

                //Kedze sa tato metoda vola vzdy tak tu prepisem usera ktory ju vola pretoze pri alertoch je potrebne vediet uzivatela ktory vytvoril dany alert a nie prihlasenho uzivatela
                currentUser = spUser; //Je to globalny objekt preto ho inicializujem az tu!!!

                string propertyValue = GetValueFromProperty(field, fieldStatus, controlFormMode, isViewOrAlert);

                if (propertyValue != String.Empty) {
                    //Uz sa to uklada inak cez ciarku a bez 8;# atd
                    //string docasnyZoznam = "8;#tester1;#9;#tester10;#4;#Intranet – návštevníci;#10;#tester2;#5;#Intranet – členovia;#9;#sp2010\\Administrator";
                    //string docasnyZoznamAD = "8;#sp2010\\group1;#9;#shpgroup1;#9;#tester10";

                    if (IsUserIncludedInProperties(propertyValue)) {
                        ClearAllLists();

                        if (ViceVersaSetting(currentSite, field.ParentList, false))
                            return false;
                        else
                            return true;
                    } else {
                        ClearAllLists();
                        if (ViceVersaSetting(currentSite, field.ParentList, false))
                            return true;
                        else
                            return false;
                    }
                }
                return false;

            } catch (Exception ex) {
                //ToDo
                ClearAllLists();
                return false;
            }
        }

        public bool GetViewPermissionForCurrentUser(SPView currentView) {
            try {
                currentSite = SPContext.Current.Site;
                currentWeb = SPContext.Current.Web;
                currentUser = SPContext.Current.Web.CurrentUser;

                string propertyValue = GetViewValueFromProperty(currentView);

                if (propertyValue != String.Empty) {
                    if (IsUserIncludedInProperties(propertyValue)) {
                        ClearAllLists();
                        if (ViceVersaSetting(currentSite, currentView.ParentList, true))
                            return false;
                        else
                            return true;
                    } else {
                        ClearAllLists();
                        if (ViceVersaSetting(currentSite, currentView.ParentList, true))
                            return true;
                        else
                            return false;
                    }
                }
                return false;

            } catch (Exception ex) {
                //ToDo
                ClearAllLists();
                return false;
            }
        }

        public bool ViceVersaSetting(SPSite currentSite, SPList currentList, bool isView) {

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

        public string GetViewValueFromProperty(SPView currentView)
        {
            Guid currentViewId = currentView.ID;
            Guid currentListId = currentView.ParentList.ID;
            
            StringBuilder propertyValue = new StringBuilder();
            propertyValue.Append(Definitions.prefix + Definitions.separator);
            propertyValue.Append(currentListId + Definitions.separator);
            propertyValue.Append(currentViewId);

            //SPGuys_ListId_ViewId 
            string propertyFromWeb = propertyValue.ToString();

            if (currentSite.RootWeb.AllProperties[propertyFromWeb] != null)
            {
                //Musim replacovat \\\\ aj ked do property ulozim len \\ ale on tam doplni \\ potrebujem to mat v stave sp2010\\Administrator nie sp2010\\\\Administrator 
                //return currentSite.RootWeb.AllProperties[propertyFromWeb].ToString().Replace("\\\\", "\\"); //Tu sa to zapisuje inak preto nam to nie je treba replacovat
                
                return currentSite.RootWeb.AllProperties[propertyFromWeb].ToString();
            }

            return String.Empty;
        }


        public string GetValueFromProperty(SPField field, string fieldStatus, SPControlMode controlMode, bool isViewOrAlert)
        {
            SPList currentList;
            SPItem currentItem;
            SPField currentField;

            if (isViewOrAlert)
            {
                currentList = field.ParentList;
                currentField = field;
            }
            else
            {
                BaseFieldControl webControl = field.FieldRenderingControl;
                currentList = webControl.List;
                currentItem = webControl.Item;
                currentField = webControl.Field;

                Guid? currentListId = webControl.ListId;
                int? currentItemIdInt = webControl.ItemId;
                string currentItemId = webControl.Field.Id.ToString();
                string currentValue = webControl.ItemFieldValue == null ? "" : webControl.ItemFieldValue.ToString();
            }
            

            StringBuilder propertyValue = new StringBuilder();
            propertyValue.Append(Definitions.prefix + Definitions.separator);
            propertyValue.Append(currentList.ID + Definitions.separator);
            propertyValue.Append(currentField.Id + Definitions.separator);
            propertyValue.Append(controlMode.ToString() + Definitions.separator);
            propertyValue.Append(fieldStatus);

            //SPGuys_ListId_FieldId_FormMode_ReadOnly Hidden
            string propertyFromWeb = propertyValue.ToString();

            //SPGuys_b9a223e1-58da-4608-aaf4-0a0c2f1d6b0e_fa564e0f-0c70-4ab9-b863-0177e6ddd247_New_Hidden   title
            //SPGuys_b9a223e1-58da-4608-aaf4-0a0c2f1d6b0e_6e180dd1-762e-46a0-8ffd-7925b70e699e_New_ReadOnly   people

            if(propertyFromWeb.Contains(SPBuiltInFieldId.LinkTitle.ToString()))
                propertyFromWeb = propertyFromWeb.Replace(SPBuiltInFieldId.LinkTitle.ToString(), SPBuiltInFieldId.Title.ToString());
            
            if(propertyFromWeb.Contains(SPBuiltInFieldId.LinkTitleNoMenu.ToString()))
                propertyFromWeb = propertyFromWeb.Replace(SPBuiltInFieldId.LinkTitleNoMenu.ToString(), SPBuiltInFieldId.Title.ToString());

            //Toto tu je kvoli tomu ze ked volam priamo tuto metodu na zistenie detailov pre ListField.aspx
            if (currentSite == null)
            {
                currentSite = SPContext.Current.Site;
                currentWeb = SPContext.Current.Web;
            }

            if (currentSite.RootWeb.AllProperties[propertyFromWeb] != null)
            {
                //Musim replacovat \\\\ aj ked do property ulozim len \\ ale on tam doplni \\ potrebujem to mat v stave sp2010\\Administrator nie sp2010\\\\Administrator 
                //return currentSite.RootWeb.AllProperties[propertyFromWeb].ToString().Replace("\\\\", "\\");
                
                return currentSite.RootWeb.AllProperties[propertyFromWeb].ToString(); //Tu sa to zapisuje inak preto nam to nie je treba replacovat
            }

            return String.Empty;
        }

        public bool IsUserIncludedInProperties(string propertyValue)
        {
            try
            {
                //usersAndGroups = SplitMethod(propertyValue,';');
                usersAndGroups = SplitMethodSimple(propertyValue, ',');

                if (usersAndGroups != null)
                {
                    sharepointGroups = GetSPGroups(usersAndGroups);
                    activedirectoriesGroups = GetADGroups(usersAndGroups);
                    sharepointUsers = new List<string>(usersAndGroups);

                    if (sharepointUsers.Count != 0)
                    {
                        foreach (string user in sharepointUsers)
                        {
                            string userN;

                            //Toto by sme mohli pouzit v pripade ak sme si na 100% isty za uzivatel nebude v properties 
                            //zapisany bez domeny teda tester1 a nie SP2010\tester1
                            //string userName2 = currentUser.LoginName
                            //if (userName2 == user.ToLower().Trim())
                            //{
                            //    return true;
                            //}

                            //Tu doslo k zmene kvoli zapisom do Property Bag teda pre istotu aj jedno \ ale aj dve \
                            if (user.IndexOf('\\') != -1)
                                userN = user.Split('\\')[1].ToLower().Trim();
                            else if (user.IndexOf("\\") != -1)
                                userN = user.Split(@"\\".ToCharArray())[1].ToLower().Trim();
                            else
                                userN = user.ToLower().Trim();
                            
                            string userName = currentUser.LoginName.Split('\\')[1].ToLower().Trim();

                            if (userName == userN)
                            {
                                return true;
                            }
                        }
                    }

                    if (sharepointGroups.Count != 0)
                    {
                        foreach (string userGroup in sharepointGroups)
                        {
                            SPGroup spGroup = currentWeb.SiteGroups[userGroup];

                            if (spGroup != null)
                            {
                                if (spGroup.ContainsCurrentUser)
                                {
                                    return true;
                                }
                            }
                        }
                    }

                    if (activedirectoriesGroups.Count != 0)
                    {
                        foreach (string userGroup in activedirectoriesGroups)
                        {
                            //string groupName = userGroup.Split('\\')[1];

                            if (IsUserInADGroup(userGroup, currentUser.LoginName))
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Rozsekame zoznam uzivatelov a skupin do pola v pripade ak sa jedna o toto 8;#tester1;#9;#tester10;#4;#Intranet – návštevníci;#10;#tester2;#5;#Intranet – členovia;#9;#sp2010\\Administrator
        public List<string> SplitMethod(string splitString, char delimiter)
        {
            try
            {
                List<string> allGroups = new List<string>();
                string[] userArray;
                userArray = splitString.Split(delimiter);

                for (int i = 1; i <= userArray.Count(); i += 2)
                {
                    if(i < userArray.Length)
                        allGroups.Add(userArray[i].ToString().Substring(1));
                }

                return allGroups;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<string> SplitMethodSimple(string splitString, char delimiter)
        {
            try
            {
                List<string> allGroups = new List<string>();
                string[] userArray;
                userArray = splitString.Split(delimiter);

                foreach (string userOrgroup in userArray)
                {
                    allGroups.Add(userOrgroup);
                }

                return allGroups;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public List<string> GetSPGroups(List<string> groups)
        {
            List<string> _groups = new List<string>(groups);
            List<string> spGroups = new List<string>();
            foreach (string group in _groups)
            {
                //GroupExistsInWebSite(spWeb, group);
                if (GroupExistsInSiteCollection(currentWeb, group))
                {
                    spGroups.Add(group);
                    usersAndGroups.Remove(group);
                }
            }

            return spGroups;
        }


        public List<string> GetADGroups(List<string> groups)
        {
            List<string> _groups = new List<string>(groups);
            List<string> adGroups = new List<string>();
            string domainName = currentUser.LoginName.Split('\\').First() + @"\";

            foreach (string group in _groups)
            {
                if (group.ToLower().IndexOf(domainName.ToLower()) != -1)
                {
                    if (IsADGroup(group))
                    {
                        adGroups.Add(group);
                        usersAndGroups.Remove(group);

                        AddInnerADGroups(group);
                    }
                }
            }

            if (innerADGroups.Count != 0)
            {
                foreach (string _group in innerADGroups)
                {
                    adGroups.Add(_group);
                }
            }

            return adGroups;
        }

        /// <summary>
        /// Check if groupName is an Active directory group
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public bool IsADGroup(string groupName)
        {
            SPPrincipalInfo userInfo = SPUtility.ResolvePrincipal(currentWeb, groupName, SPPrincipalType.SecurityGroup, SPPrincipalSource.All, null, false);

            if (userInfo != null)
                return true;

            return false;
        }

        public void AddInnerADGroups(string groupName)
        {
            bool reachedMax;

            SPPrincipalInfo[] groupUsers =
            SPUtility.GetPrincipalsInGroup(currentWeb, groupName, 10000, out reachedMax);

            if (groupUsers == null || groupUsers.Length == 0)
            {
                return;
            }
            else
            {
                foreach (SPPrincipalInfo _user in groupUsers)
                {
                    if (!_user.IsSharePointGroup && _user.PrincipalType == SPPrincipalType.SecurityGroup)
                    {
                        innerADGroups.Add(_user.LoginName);
                        AddInnerADGroups(_user.LoginName);
                    }
                }
                return;
            }
        }

        public bool IsUserInADGroup(string groupName, string userName)
        {
            bool reachedMax;

            SPPrincipalInfo[] groupUsers = SPUtility.GetPrincipalsInGroup(currentWeb, groupName, 10000, out reachedMax);

            if (groupUsers == null || groupUsers.Length == 0)
            {
                return false;
            }
            else
            {
                foreach (SPPrincipalInfo _user in groupUsers)
                {
                    if (!_user.IsSharePointGroup && _user.PrincipalType != SPPrincipalType.SecurityGroup)
                    {
                        if (_user.LoginName.Trim() == userName.Trim())
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public void ClearAllLists()
        {
            usersAndGroups.Clear();
            sharepointGroups.Clear();
            activedirectoriesGroups.Clear();
            sharepointUsers.Clear();
            innerADGroups.Clear();
        }

        //zistime ci dana groupa existuje pre dany WEB
        public bool GroupExistsInWebSite(SPWeb web, string groupName)
        {
            return web.Groups.OfType<SPGroup>().Count(g => g.Name.Equals(groupName, StringComparison.InvariantCultureIgnoreCase)) > 0;
        }

        //zistime ci dana groupa existuje pre danu SITE
        public bool GroupExistsInSiteCollection(SPWeb web, string groupName)
        {
            return web.SiteGroups.OfType<SPGroup>().Count(g => g.Name.Equals(groupName, StringComparison.InvariantCultureIgnoreCase)) > 0;
        }

        #endregion

    }
}
