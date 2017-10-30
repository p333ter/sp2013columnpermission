using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Utilities;
using System.IO;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;

namespace SPGuysCustomFieldPermission.Features.SPGuysCustomFieldPermissionsFeature
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("1afc4a3c-6d75-49ca-9617-12b126f6dcea")]
    public class SPGuysCustomFieldPermissionsFeatureEventReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            #region - Copy vsmenu.aspx -

            //string fileName = "vsmenu.aspx";
            //string filePath = SPUtility.GetGenericSetupPath(@"TEMPLATE\LAYOUTS\SPGuysCustomFieldPermission\Override") + @"\" + fileName;
            //string fileInHive = SPUtility.GetGenericSetupPath(@"TEMPLATE\LAYOUTS") + @"\" + fileName;

            //if (File.Exists(fileInHive))
            //{
            //    try
            //    {
            //        if (!File.Exists(Path.GetDirectoryName(fileInHive) + @"\" + Path.GetFileNameWithoutExtension(fileInHive) + "_backup" + Path.GetExtension(fileInHive)))
            //            File.Move(fileInHive, Path.GetDirectoryName(fileInHive) + @"\" + Path.GetFileNameWithoutExtension(fileInHive) + "_backup" + Path.GetExtension(fileInHive));
            //        else
            //            File.Move(fileInHive, Path.GetDirectoryName(fileInHive) + @"\" + Path.GetFileNameWithoutExtension(fileInHive) + "_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + "_backup" + Path.GetExtension(fileInHive));

            //        File.Copy(filePath, fileInHive, true);
            //    }
            //    catch (Exception ex)
            //    {
            //        //ToDo
            //    }
            //}

            #endregion

            //POZOR ANI JEDNO NEFUNGUJE SPRAVNE AJ KED SUBOR REDIRECTMODULE.CS JE SPRAVNY PROBLEM S PRAVAMI
            //BOLO BY VHODNE TOTO POUZIT PRE WEBAPPLICATION FEATURU NIE PRE SITE ALEBO WEB
            #region - HttpModule -

                #region - HttpModule -

            ////var oWebApp = properties.Feature.Parent as SPWebApplication;
            //SPWeb spWeb = SPContext.Current.Web;
            //SPWebApplication oWebApp = spWeb.Site.WebApplication;

            //try
            //{
            //    if (!oWebApp.IsAdministrationWebApplication)
            //    {
            //        ModifyWebConfigEntries(oWebApp);
            //    }
            //}
            //catch (Exception ex)
            //{

            //}

            #endregion

                #region - HttpModule 2 -

            //SPWebService myService = SPWebService.ContentService;

            //List<SPWebConfigModification> mods = new List<SPWebConfigModification>();
            //foreach (SPWebConfigModification mod in myService.WebConfigModifications)
            //{
            //    if (mod.Value.Contains("CustomHttpModule")) mods.Add(mod);
            //}

            //foreach (SPWebConfigModification m in mods)
            //{
            //    myService.WebConfigModifications.Remove(m);
            //}

            //myService.Update();

            //SPWebConfigModification webConfigModifications = new SPWebConfigModification();
            //webConfigModifications.Path = "configuration/system.web/httpModules";
            //webConfigModifications.Name = "add[@name='CustomHttpModule']";
            //webConfigModifications.Sequence = 0;
            //webConfigModifications.Owner = "addCustomModule";


            //webConfigModifications.Type = SPWebConfigModification.SPWebConfigModificationType.EnsureChildNode;
            ////webConfigModifications.Value = @"<add name='CustomHttpModule' type='ClassName,AssemblyName, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cfba5cfbc4661d2d' />";
            //webConfigModifications.Value = string.Format("<add name='CustomHttpModule' type='SPGuysCustomFieldPermissionLibrary.RedirectModule,SPGuysCustomFieldPermissionLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a8a8bdc8ce8bd55f' />");

            //myService.WebConfigModifications.Add(webConfigModifications);
            //myService.Update();
            //myService.ApplyWebConfigModifications();

            #endregion

                #region - HttpModule 3 -

            //SPSecurity.RunWithElevatedPrivileges(delegate()
            //{
            //    // Toto funguje
            //    SPWebService myService = SPWebService.ContentService;
            //    SPWebConfigModification myModification = new SPWebConfigModification();

            //    myModification.Path = "configuration/system.webServer/modules";
            //    myModification.Name = "add[@name='SPGuysHttpModule'][@type='SPGuysCustomFieldPermissionLibrary.RedirectModule,SPGuysCustomFieldPermissionLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a8a8bdc8ce8bd55f']";
            //    myModification.Sequence = 333;
            //    myModification.Owner = "SPGuysHttpModule";
            //    myModification.Type = SPWebConfigModification.SPWebConfigModificationType.EnsureChildNode;
            //    myModification.Value = string.Format("<add name='CustomHttpModule' type='SPGuysCustomFieldPermissionLibrary.RedirectModule,SPGuysCustomFieldPermissionLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a8a8bdc8ce8bd55f' />");

            //    myService.WebConfigModifications.Add(myModification);
            //    myService.ApplyWebConfigModifications();
            //    myService.Update();
            //});

            #endregion

            #endregion

        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            #region - Copy vsmenu.aspx -

            //string fileName = "vsmenu.aspx";
            //string fileNameBackUp = "vsmenu_backup.aspx";
            //string fileInHive = SPUtility.GetGenericSetupPath(@"TEMPLATE\LAYOUTS") + @"\" + fileName;
            //string fileInHiveBackUp = SPUtility.GetGenericSetupPath(@"TEMPLATE\LAYOUTS") + @"\" + fileNameBackUp;

            //if (File.Exists(fileInHive) && File.Exists(fileInHiveBackUp))
            //{
            //    try
            //    {
            //        File.Delete(fileInHive);
            //        File.Move(fileInHiveBackUp, fileInHive);
            //    }
            //    catch (Exception ex)
            //    {
            //        //ToDo
            //    }
            //}

            #endregion

            //POZOR ANI JEDNO NEFUNGUJE SPRAVNE AJ KED SUBOR REDIRECTMODULE.CS JE SPRAVNY PROBLEM S PRAVAMI
            //BOLO BY VHODNE TOTO POUZIT PRE WEBAPPLICATION FEATURU NIE PRE SITE ALEBO WEB
            #region - HttpModule -

                #region - HttpModule -

            ////var oWebApp = properties.Feature.Parent as SPWebApplication;
            //SPWeb spWeb = SPContext.Current.Web;
            //SPWebApplication oWebApp = spWeb.Site.WebApplication;

            //try
            //{
            //    if (!oWebApp.IsAdministrationWebApplication)
            //    {
            //        // Web application is not central administration.  Configuration changes are different.
            //        DeleteWebConfigEntries(oWebApp);
            //    }
            //}
            //catch (Exception ex)
            //{
            //}

            #endregion

                #region - HttpModule 2 -

            //SPWebService myService = SPWebService.ContentService;

            //List<SPWebConfigModification> mods = new List<SPWebConfigModification>();
            //foreach (SPWebConfigModification mod in myService.WebConfigModifications)
            //{
            //    if (mod.Value.Contains("CustomHttpModule")) mods.Add(mod);
            //}

            //foreach (SPWebConfigModification m in mods)
            //{
            //    Console.WriteLine(m.Value);
            //    myService.WebConfigModifications.Remove(m);

            //}

            //myService.Update();

            //myService.ApplyWebConfigModifications();

            #endregion

                #region - HttpModule 3 -

            //SPSecurity.RunWithElevatedPrivileges(delegate()
            //{
            //    //Toto funguje
            //    SPWebService myService = SPWebService.ContentService;

            //    List<SPWebConfigModification> mods = new List<SPWebConfigModification>();
            //    foreach (SPWebConfigModification mod in myService.WebConfigModifications)
            //    {
            //        if (mod.Value.Contains("SPGuysHttpModule")) mods.Add(mod);
            //    }

            //    foreach (SPWebConfigModification m in mods)
            //    {
            //        //Console.WriteLine(m.Value);
            //        myService.WebConfigModifications.Remove(m);

            //    }

            //    myService.Update();
            //    myService.ApplyWebConfigModifications();
            //});

            #endregion

            #endregion

        }

        #region - Metody pre HttpModule -

        public void ModifyWebConfigEntries(SPWebApplication oWebApp)
        {
            //SPWebConfigModification webConfigModifications = new SPWebConfigModification();

            //webConfigModifications.Path = "configuration/system.web/httpModules";
            //webConfigModifications.Name = "add[@name='CustomHttpModule']";
            //webConfigModifications.Sequence = 0;
            //webConfigModifications.Owner = "addCustomModule";


            //webConfigModifications.Type = SPWebConfigModification.SPWebConfigModificationType.EnsureChildNode;
            ////webConfigModifications.Value = @"<add name='CustomHttpModule' type='ClassName,AssemblyName, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cfba5cfbc4661d2d' />";
            //webConfigModifications.Value = string.Format("<add name='CustomHttpModule' type='SPGuysCustomFieldPermissionLibrary.RedirectModule,SPGuysCustomFieldPermissionLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a8a8bdc8ce8bd55f' />");
            //oWebApp.WebConfigModifications.Add(webConfigModifications);



            ////SPFarm.Local.Services.GetValue<SPWebService>().ApplyWebConfigModifications();
            //SPWebService.ContentService.WebApplications[oWebApp.Id].Update();
            ////Applies the web config settings in all the web application in the farm
            //SPWebService.ContentService.WebApplications[oWebApp.Id].WebService.ApplyWebConfigModifications();
        }

        public void DeleteWebConfigEntries(SPWebApplication oWebApp)
        {
            //Collection<SPWebConfigModification> oCollection = oWebApp.WebConfigModifications;
            //int iStartCount = oCollection.Count;
            //for (int c = iStartCount - 1; c >= 0; c--)
            //{
            //    SPWebConfigModification oModification = oCollection[c];

            //    if (oModification.Owner == "addCustomModule")
            //        oCollection.Remove(oModification);
            //}


            //if (iStartCount > oCollection.Count)
            //{
            //    //oWebApp.Update();
            //    //Applies the web config settings in all the web application in the farm
            //    SPFarm.Local.Services.GetValue<SPWebService>().ApplyWebConfigModifications();
            //}
        }

        #endregion

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        
        //}

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}

        #region - Uninstalling Feature Methods -

        //private void UninstallingFeatureMethod()
        //{
            //    if (!EventLog.SourceExists("TESTzapisu"))
            //        EventLog.CreateEventSource("TESTzapisu", "Application");

            //    EventLog.WriteEntry("TESTzapisu", "Uninstalling feature", EventLogEntryType.Error);



            //    try
            //    {
            //SPWeb currentWeb = (properties.Feature.Parent as SPSite).OpenWeb();
            //SPSite currentSite1 = properties.Feature.Parent as SPSite;

            //SPWebApplication currentWebApp = currentWeb.Site.WebApplication;

            //if (!EventLog.SourceExists("TESTzapisu"))
            //    EventLog.CreateEventSource("TESTzapisu", "Application");

            //EventLog.WriteEntry("TESTzapisu", "WebApplication - DisplayName:" + currentWebApp.DisplayName + "    Name:" + currentWebApp.Name, EventLogEntryType.Error);

            //EventLog.WriteEntry("TESTzapisu", "currentSite - HostName:" + currentSite1.HostName + "    Url:" + currentSite1.Url, EventLogEntryType.Error);


            //SPSiteCollection currentSiteColl = currentWebApp.Sites;

            //foreach (SPSite currentSite in currentSiteColl)
            //{
            //    EventLog.WriteEntry("TESTzapisu", "Site - Url:" + currentSite.Url + "    HostName:" + currentSite.HostName + "      PortalName:" + currentSite.PortalName + "     RootWeb.Name:" + currentSite.RootWeb.Name, EventLogEntryType.Error);
            //    currentSite.Features.Remove(Definitions.featureGuid, true);
            //}
            //}
            //catch (Exception ex)
            //{
            //    if (!EventLog.SourceExists("TESTzapisu"))
            //        EventLog.CreateEventSource("TESTzapisu", "Application");

            //    EventLog.WriteEntry("TESTzapisu", "Chyba pri odinstalovani", EventLogEntryType.Error);


            //}
        //}

        #endregion

        #region - Info how to get current SPWeb or SPSite if SPContext is null -

        //Ak je featura typu scope SITE
        //SPWeb objSPWeb = (properties.Feature.Parent as SPSite).OpenWeb();
        //SPWeb objSPWeb = (properties.Feature.Parent as SPSite).RootWeb;

        ///Ak je featura typu scope WEB
        //SPWeb objSPWeb = properties.Feature.Parent as SPWeb;
        //SPWeb objSPWeb = properties.Feature.Parent as SPWeb;

        #endregion

        #region - Custom Methods -

        //http://msdn.microsoft.com/en-us/library/ee231545.aspx
        //http://ranaictiu-technicalblog.blogspot.com/2010/10/sharepoint-activedeactivate-feature.html#%7B%22color%22%3A%22%23000000%22%2C%22backgroundColor%22%3A%22%23f6f6f6%22%2C%22unvisitedLinkColor%22%3A%22%23de7008%22%2C%22fontFamily%22%3A%22%5C%22Trebuchet%20MS%5C%22%2C%20Trebuchet%2C%20Verdana%2C%20Sans-Serif%22%7D
        //http://www.sharepointer.com.br/sharepoint-development/sharepoint-object-model/how-to-activate-a-feature-in-a-sharepoint-farm-programmatically/s

        public static void DeactivateFeatureInFarm(string strFeatureTitle)
        {
            SPFarm farm = SPFarm.Local;
            SPWebService webService = farm.Services.GetValue<SPWebService>("");

            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo(1033);
            SPFeatureDefinitionCollection featureDefinitionColl = SPFarm.Local.FeatureDefinitions;

            foreach (SPFeatureDefinition featureDefinition in featureDefinitionColl)
            {
                if (featureDefinition.GetTitle(cultureInfo) == strFeatureTitle)
                {
                    Guid guidFeatureDefinitionID = featureDefinition.Id;

                    if (featureDefinition.Scope == SPFeatureScope.Farm)
                    {
                        SPFeatureCollection featureColl = webService.Features;
                        featureColl.Remove(guidFeatureDefinitionID, true);
                    }
                }
            }
        }

        public static void ActivateFeatureInFarm(string strFeatureTitle)
        {
            SPFarm farm = SPFarm.Local;
            SPWebService webService = farm.Services.GetValue<SPWebService>("");

            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo(1033);
            SPFeatureDefinitionCollection featureDefinitionColl = SPFarm.Local.FeatureDefinitions;

            foreach (SPFeatureDefinition featureDefinition in featureDefinitionColl)
            {
                if (featureDefinition.GetTitle(cultureInfo) == strFeatureTitle)
                {
                    Guid guidFeatureDefinitionID = featureDefinition.Id;

                    if (featureDefinition.Scope == SPFeatureScope.Farm)
                    {
                        SPFeatureCollection featureColl = webService.Features;
                        featureColl.Add(guidFeatureDefinitionID, true);
                    }
                }
            }
        }

        public static void ActivateFeatureInWeb(string strSiteUrl, string strFeatureTitle)
        {
            using (SPSite site = new SPSite(strSiteUrl))
            {
                System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo(1033);
                SPFeatureDefinitionCollection featureDefinitionColl = SPFarm.Local.FeatureDefinitions;
                SPWebCollection webColl = site.AllWebs;
                foreach (SPFeatureDefinition featureDefinition in featureDefinitionColl)
                {
                    if (featureDefinition.GetTitle(cultureInfo) == strFeatureTitle)
                    {
                        Guid guidFeatureDefinitionID = featureDefinition.Id;

                        foreach (SPWeb web in webColl)
                        {
                            if (featureDefinition.Scope == SPFeatureScope.Web)
                            {
                                SPFeatureCollection featureColl = web.Features;
                                featureColl.Add(guidFeatureDefinitionID, true);
                            }
                            web.Dispose();
                        }
                    }
                }
            }
        }

        public static void DeactivateFeatureInWeb(string strSiteUrl, string strFeatureTitle)
        {
            using (SPSite site = new SPSite(strSiteUrl))
            {

                System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo(1033);
                SPFeatureDefinitionCollection featureDefinitionColl = SPFarm.Local.FeatureDefinitions;
                SPWebCollection webColl = site.AllWebs;
                foreach (SPFeatureDefinition featureDefinition in featureDefinitionColl)
                {
                    if (featureDefinition.GetTitle(cultureInfo) == strFeatureTitle)
                    {
                        Guid guidFeatureDefinitionID = featureDefinition.Id;



                        foreach (SPWeb web in webColl)
                        {
                            if (featureDefinition.Scope == SPFeatureScope.Web)
                            {
                                SPFeatureCollection featureColl = web.Features;
                                featureColl.Remove(guidFeatureDefinitionID, true);
                            }
                            web.Dispose();
                        }
                    }
                }
            }
        }

        #endregion
    }
}
