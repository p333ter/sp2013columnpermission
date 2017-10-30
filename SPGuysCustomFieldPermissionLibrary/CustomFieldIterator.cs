using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.DirectoryServices.Protocols;
using System.DirectoryServices;
using Microsoft.SharePoint.Utilities;

namespace SPGuysCustomFieldPermissionLibrary
{
    public class CustomFieldIterator : ListFieldIterator
    {
        //Kedze je tato featura aktivovana ako Scope = Site tak pracujem vsade zo Site. Kontrola ci je featura aktivovana, Groupy nacitavam tiez so SiteGroups, 
        //Propety tiez nacitavam so Site nie Web. Ak by som chcel aby Scope = Web tak musim prerobit aj z kade nacitavam tieto veci takze na to pozor!!!

        #region - Definitions -

        SPContext spContext = SPContext.Current;
        SPWeb spWeb = SPContext.Current.Web;
        SPSite spSite = SPContext.Current.Site;
        SPFormContext formContext = SPContext.Current.FormContext;
        SPControlMode formMode = SPContext.Current.FormContext.FormMode;
        SPFeature spFeatureSite;
        SPUser currentUser = SPContext.Current.Web.CurrentUser;

        #endregion

        #region - Dynamic Methods -

        //Dynamicke metody na vytvorenie vlastnosti
        //protected static GenericSetter set_TemplateContainer_ControlMode = ILUtils.CreateSetMethod(typeof(TemplateContainer), "ControlMode");
        //protected static GenericSetter set_TemplateContainer_FieldName = ILUtils.CreateSetMethod(typeof(TemplateContainer), "FieldName");

        #endregion

        #region - Base Methods -


        protected override void CreateChildControls()
        {
            spFeatureSite = spSite.Features[Definitions.featureGuid];
            if (spFeatureSite != null)
            {
                UserPermission userPermission = new UserPermission();

                this.Controls.Clear();
                if (this.ControlTemplate == null)
                {
                    throw new ArgumentException("Could not find ListFieldIterator control template.");
                }

                for (int i = 0; i < base.Fields.Count; i++)
                {
                    SPField field = base.Fields[i];

                    String fieldName = field.InternalName;

                    if ((!this.IsFieldExcluded(field)))
                    {
                        TemplateContainer child = new TemplateContainer();
                        this.Controls.Add(child);

                        //Dynamicky vytvorenie metod na nastavenie vlastnosti ControlMode a FieldName
                        //set_TemplateContainer_ControlMode(child, SPControlMode.Display);
                        //set_TemplateContainer_FieldName(child, fieldName);

                        SPUser currentUser = SPContext.Current.Web.CurrentUser;

                        //Zistim nastavenie pre field ci ma byt v Display mode
                        SPControlMode fieldMode;
                        if (formMode != SPControlMode.Display)
                        {
                            if (userPermission.GetFieldPermissionForCurrentUser(field, "ReadOnly", formMode, false, currentUser, null))
                                fieldMode = SPControlMode.Display;
                            else
                                fieldMode = formMode;
                        }
                        else
                            fieldMode = formMode;

                        //Zadefinovane vlastnosti ControlMode a FieldName priamo cez reflexiu
                        SetFieldName(child, field.InternalName);
                        SetControlMode(child, fieldMode);

                        this.ControlTemplate.InstantiateIn(child);
                    }
                }
            }
            else
            {
                base.CreateChildControls();
            }
        }

        protected override bool IsFieldExcluded(SPField field)
        {
            spFeatureSite = spSite.Features[Definitions.featureGuid];
            if (spFeatureSite != null)
            {
                SPUser currentUser = SPContext.Current.Web.CurrentUser;

                UserPermission userPermission = new UserPermission();

                if (userPermission.GetFieldPermissionForCurrentUser(field, "Hidden", formMode, false, currentUser, null))
                    return true;

                return base.IsFieldExcluded(field);
            }
            else
                return base.IsFieldExcluded(field);
        }

        #endregion

        #region - Recursive Methods -

        private static void SetControlMode(TemplateContainer child, SPControlMode controlMode)
        {
            try
            {
                child.GetType().GetProperty("ControlMode", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(child, controlMode, null);
            }
            catch (Exception)
            {
            }
        }

        private static void SetFieldName(TemplateContainer child, string fieldName)
        {
            try
            {
                child.GetType().GetProperty("FieldName", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(child, fieldName, null);
            }
            catch (Exception)
            {
            }
        }

        #endregion
    }
}
