using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI;
using Microsoft.SharePoint;

namespace SPGuysCustomFieldPermissionLibrary
{
    //TOTO SA NEPOUZIVA ZATIAL 

    /// <summary>
    /// RedirectModule by zabezpecilo to ze ak sa niekedy dotaze pre vsmenu.aspx tak sa presmeruje s parametrami na inu stranku ktora by vyriesila co nam treba
    /// Nie je to pouzite preto ze je problem pri aktivacii a pripade deaktivacii tohto modulu...hlavny problem s pravami AccessDenied
    /// Metody v SPGuysCustomFieldPermissionsFeatureEventReceiver.cs su zakomentovane na pouzitie tohto
    /// </summary>
    class RedirectModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += new EventHandler(RegisterPreInitRequestHandler);
        }

        void RegisterPreInitRequestHandler(object sender, EventArgs e)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;
            if (page != null)
            {
                page.PreInit += new EventHandler(page_PreInit);
            }
        }

        void page_PreInit(object sender, EventArgs e)
        {
            SPContext spContext = SPContext.Current;
            SPWeb spWeb = SPContext.Current.Web;
            SPSite spSite = SPContext.Current.Site;
            SPFeature spFeatureSite;

            spFeatureSite = spSite.Features[Definitions.featureGuid];

            if (spFeatureSite != null)
            {
                Page page = sender as Page;
                if (page != null)
                {
                    if (page.Request.Url.AbsolutePath.Contains("vsmenu.aspx"))
                    {
                        //todo
                        //page.Response.Redirect("/_layouts/SPGuys_menu.aspx" + page.Request.Url.Query, true);

                        string redirectUrl = spWeb.Url.ToString() + "/_layouts/SPGuys_menu.aspx" + page.Request.Url.Query;
                        page.Response.Redirect(redirectUrl, true);
                    }
                }
            }
        }
        public void Dispose() { }
    }
}
