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
using System.Xml;
using System.Collections;
using System.Globalization;
using Microsoft.SharePoint.Administration;
using System.Threading;
using System.Web;

namespace SPGuysCustomFieldPermissionLibrary
{

    public class CustomViewSelectorMenu : ViewSelectorMenu
    {

      SPContext spContext = SPContext.Current;
      //SPWeb spWeb = SPContext.Current.Web;
      //SPSite spSite = nSPContext.Current.Site;
      //SPFeature spFeatureSite;

        #region - Base Methods - 


      //protected override void OnLoad(EventArgs e)
      //{
      //    base.OnLoad(e);
      //}


      //public override void RenderControl(HtmlTextWriter writer)
      //{
      //    base.RenderControl(writer);
      //}

      //protected override void SetMenuItemProperties()
      //{
      //    base.SetMenuItemProperties();
      //}

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

        //    using (SPSite spSite = new SPSite(SPContext.Current.Web.Url))
        //    {
        //        SPFeature spFeatureSite;
        //        //base.AddMenuItems();

        //        spFeatureSite = spSite.Features[Definitions.featureGuid];
        //        if (spFeatureSite != null)
        //        {
        //            try
        //            {
        //                UserPermission userPermission = new UserPermission();

        //                IList<Control> itemsToRemove = new List<Control>();

        //                if (MenuTemplateControl.Controls.Count != 0)
        //                {
        //                    foreach (Control menuItemControl in MenuTemplateControl.Controls)
        //                    {
        //                        if (menuItemControl is MenuItemTemplate)
        //                        {
        //                            MenuItemTemplate menuItem = menuItemControl as MenuItemTemplate;
        //                            string url = menuItem.ClientOnClickNavigateUrl;

        //                            if (url.Contains("_layouts"))
        //                                continue;

        //                            int pos = url.IndexOf("?");
        //                            if (pos != -1)
        //                                url = url.Substring(0, pos);

        //                            SPView currentView = SPContext.Current.Web.GetViewFromUrl(url);

        //                            if (userPermission.GetViewPermissionForCurrentUser(currentView))
        //                            {
        //                                itemsToRemove.Add(menuItem);
        //                            }
        //                        }
        //                    }
        //                    if (itemsToRemove.Count != 0)
        //                    {
        //                        foreach (Control menuItemControl in itemsToRemove)
        //                        {
        //                            MenuTemplateControl.Controls.Remove(menuItemControl);
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                //ToDo
        //            }
        //        }
        //    }
        }

        //Nepouziva sa tato metoda pretoze v niektorych pripadoch sa ani nezavola teda som to vsetko presunul do OnInit!!!
        //protected override void AddMenuItems()
        //{
        //    using (SPSite spSite = new SPSite(SPContext.Current.Web.Url))
        //    {
        //        SPFeature spFeatureSite;
        //        base.AddMenuItems();


        //        spFeatureSite = spSite.Features[Definitions.featureGuid];
        //        if (spFeatureSite != null)
        //        {
        //            try
        //            {
        //                UserPermission userPermission = new UserPermission();

        //                IList<Control> itemsToRemove = new List<Control>();

        //                if (MenuTemplateControl.Controls.Count != 0)
        //                {
        //                    foreach (Control menuItemControl in MenuTemplateControl.Controls)
        //                    {
        //                        if (menuItemControl is MenuItemTemplate)
        //                        {
        //                            MenuItemTemplate menuItem = menuItemControl as MenuItemTemplate;
        //                            string url = menuItem.ClientOnClickNavigateUrl;

        //                            if (url.Contains("_layouts"))
        //                                continue;

        //                            int pos = url.IndexOf("?");
        //                            if (pos != -1)
        //                                url = url.Substring(0, pos);

        //                            SPView currentView = SPContext.Current.Web.GetViewFromUrl(url);

        //                            if (userPermission.GetViewPermissionForCurrentUser(currentView))
        //                            {
        //                                itemsToRemove.Add(menuItem);
        //                            }
        //                        }
        //                    }
        //                    if (itemsToRemove.Count != 0)
        //                    {
        //                        foreach (Control menuItemControl in itemsToRemove)
        //                        {
        //                            MenuTemplateControl.Controls.Remove(menuItemControl);
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                //ToDo
        //            }
        //        }
        //    }
        //}

        protected override void OnPreRender(EventArgs e)
        {
            using (SPSite spSite = new SPSite(SPContext.Current.Web.Url))
            {
                base.OnPreRender(e);

                SPFeature spFeatureSite;

                spFeatureSite = spSite.Features[Definitions.featureGuid];
                if (spFeatureSite != null)
                {
                    try
                    {
                        UserPermission userPermission = new UserPermission();

                        SPList currentList = SPContext.Current.List;
                        SPView currentView = SPContext.Current.ViewContext.View;
                        //SPView currentView = SPContext.Current.Web.GetViewFromUrl(url);

                        if (userPermission.GetViewPermissionForCurrentUser(currentView))
                        {
                            if (currentView.DefaultView)
                            {
                                foreach (SPView spView in currentList.Views)
                                {
                                    if (!spView.DefaultView)
                                    {
                                        if (!userPermission.GetViewPermissionForCurrentUser(spView))
                                        {
                                            SPUtility.Redirect(base.Web.Url + "/" + spView.Url, SPRedirectFlags.Default, this.Context);
                                            return;
                                        }
                                    }
                                }

                                SPUtility.TransferToErrorPage("You Are Not Authorized to View any Views of this List. Please contact Administrator.");
                            }

                            SPUtility.HandleAccessDenied(new UnauthorizedAccessException());
                        }
                    }
                    catch (Exception ex)
                    {
                        //Todo
                    }
                }
            }
        }

        #endregion
    }
}
