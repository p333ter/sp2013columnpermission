using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Utilities;
using System.Collections.Generic;

namespace SPGuysCustomFieldPermission.Features.SPGuysCustomAlertPermissionFeature
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("553562e7-1fea-48cc-9aeb-094f080c095f")]
    public class SPGuysCustomAlertPermissionFeatureEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SetNewAlertTemplate(properties);

            #region - HttpModule -

            //Zakomentovane pretoze pod sharepoint 2013 nefunguje schovanie view ktore su na stranke nepristupne
            //// Toto funguje ale problem je ten ze pri redirecte SPContext.Current.Site nie je spravny lebo je z hlavnej stranky nie sub site collection
            //// Cize sa toto nepouzije ale povodny system kde sa prepise subor
            //SPWebService myService = SPWebService.ContentService;
            //SPWebConfigModification myModification = new SPWebConfigModification();

            //myModification.Path = "configuration/system.webServer/modules";
            ////myModification.Path = "configuration/system.web/httpModules";
            //myModification.Name = "add[@name='SPGuysHttpModule']";
            //myModification.Sequence = 0;
            //myModification.Owner = "SPGuysHttpModule";
            //myModification.Type = SPWebConfigModification.SPWebConfigModificationType.EnsureChildNode;
            //myModification.Value = string.Format("<add name='SPGuysHttpModule' type='SPGuysCustomFieldPermissionLibrary.RedirectModule,SPGuysCustomFieldPermissionLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a8a8bdc8ce8bd55f' />");

            //myService.WebConfigModifications.Add(myModification);
            //myService.ApplyWebConfigModifications();
            //myService.Update();

            #endregion
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SetBackAlertTemplate(properties);

            #region - HttpModule -

            //Zakomentovane pretoze pod sharepoint 2013 nefunguje schovanie view ktore su na stranke nepristupne
            //// Toto funguje ale problem je ten ze pri redirecte SPContext.Current.Site nie je spravny lebo je z hlavnej stranky nie sub site collection
            //// Cize sa toto nepouzije ale povodny system kde sa prepise subor
            //SPWebService myService = SPWebService.ContentService;

            //List<SPWebConfigModification> mods = new List<SPWebConfigModification>();
            //foreach (SPWebConfigModification mod in myService.WebConfigModifications)
            //{
            //    if (mod.Value.Contains("SPGuysHttpModule")) mods.Add(mod);
            //}

            //foreach (SPWebConfigModification m in mods)
            //{
            //    myService.WebConfigModifications.Remove(m);
            //}

            //myService.Update();
            //myService.ApplyWebConfigModifications();

            #endregion
        }

        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.
        public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        {
            SetBackAlertTemplate(properties);
        }

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}

        private void SetNewAlertTemplate(SPFeatureReceiverProperties properties)
        {
            try
            {
                SPWebApplication webApplication = (SPWebApplication)properties.Feature.Parent;
                SPAlertTemplateCollection alertTemplateCollection = new SPAlertTemplateCollection((SPWebService)(webApplication.Parent));
                alertTemplateCollection.InitAlertTemplatesFromFile(SPUtility.GetVersionedGenericSetupPath(@"TEMPLATE\XML",15) + @"\SPGuys_AlertTemplates2013.xml");
            }
            catch (Exception ex)
            {
                //ToDo 
            }
        }

        private void SetBackAlertTemplate(SPFeatureReceiverProperties properties)
        {
            try
            {
                SPWebApplication webApplication = (SPWebApplication)properties.Feature.Parent;
                SPAlertTemplateCollection alertTemplateCollection = new SPAlertTemplateCollection((SPWebService)(webApplication.Parent));
                alertTemplateCollection.InitAlertTemplatesFromFile(SPUtility.GetVersionedGenericSetupPath(@"TEMPLATE\XML",15) + @"\AlertTemplates.xml");
            }
            catch (Exception ex)
            {
                //ToDo
            }
        }
    }
}
