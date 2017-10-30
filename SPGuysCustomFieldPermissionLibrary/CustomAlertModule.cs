using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System.Web;
using Microsoft.SharePoint.WebControls;
using System.Xml;
using HtmlAgilityPack;

namespace SPGuysCustomFieldPermissionLibrary
{
    //Pouziva sa ale je potrebne manualne aktivovat

    /// <summary>
    /// 
    /// </summary>
    public class CustomAlertModule : IAlertNotifyHandler
    {
        #region  - IAlertNotifyHandler -

        public bool OnNotification(SPAlertHandlerParams ahp)
        {
            SPSite spSite = new SPSite(ahp.siteUrl + ahp.webUrl);
            SPWeb spWeb = spSite.OpenWeb();
            SPFeature spFeatureSite;

            //Musime to takto pouzit ked chcem aby to vyzeralo presne ako orginal mail
            string siteName = spSite.RootWeb.Title;
            siteName = "=?utf-8?B?" + EncodeTo64(siteName) + "?= ";

            ahp.headers["From"] = siteName + "<" + ahp.headers["from"].ToString() + ">";

            spFeatureSite = spSite.Features[Definitions.featureGuid];
            if (spFeatureSite != null)
            {

                UserPermission userPermission = new UserPermission();
                List<SPField> removeFields = new List<SPField>();

                #region - Custom Email 1 -

                //    public bool OnNotification(SPAlertHandlerParams alertHandler)
                //    {
                //        string siteUrl = alertHandler.siteUrl;
                //        using (SPSite site = new SPSite(siteUrl + alertHandler.webUrl))
                //        {
                //            using (SPWeb web = site.OpenWeb())
                //            {
                //                try
                //                {
                //                    // to apply only to specific site within site collection
                //                    //if (web.Url == "Sitename")
                //                        return CustomAlertNotification(web, alertHandler, siteUrl);
                //                    //else
                //                    //{
                //                    //    //for other SharePoint sites use the OOTB Alert Email
                //                    //    SPUtility.SendEmail(web, alertHandler.headers, alertHandler.body);
                //                    //    return false;
                //                    //}
                //                }
                //                catch
                //                {
                //                    //if it fails due to configuration still the default alert should work
                //                    SPUtility.SendEmail(web, alertHandler.headers, alertHandler.body);
                //                    return false;
                //                }
                //            }
                //        }
                //    }

                //    private bool CustomAlertNotification(SPWeb web, SPAlertHandlerParams alertHandler, string siteUrl)
                //    {
                //        SPList list = web.Lists[alertHandler.a.ListID];
                //        StringBuilder body = new StringBuilder();
                //        string style = ".style1 {font-size: 10pt;font-family: Arial;border:0px;}";
                //        int eventCount = alertHandler.eventData.Count();
                //        string listPath = HttpUtility.UrlPathEncode(siteUrl + "/" + alertHandler.webUrl + "/lists/" + list.Title);
                //        string webPath = HttpUtility.UrlPathEncode(siteUrl + "/" + alertHandler.webUrl);
                //        for (int i = 0; i < eventCount; i++)
                //        {
                //            string eventType = null;
                //            if (alertHandler.eventData[i].eventType == 1) eventType = "Added";
                //            else if (alertHandler.eventData[i].eventType == 2) eventType = "Changed";
                //            else if (alertHandler.eventData[i].eventType == 3 || alertHandler.eventData[0].eventType == 4) eventType = "Deleted";
                //            if (eventType != null)
                //            {
                //                try
                //                {
                //                    XmlDocument doc = new XmlDocument();
                //                    XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
                //                    doc.InnerXml = alertHandler.eventData[i].eventXml;
                //                    XmlNode titleNode = doc.SelectSingleNode(".//Field[@Name = 'Title']");
                //                    XmlNode bodyNode = doc.SelectSingleNode(".//Field[@Name = 'Body']");
                //                    string titleValue = null;
                //                    string bodyValue = null;
                //                    string header = null;
                //                    if (eventType == "Deleted")
                //                    {
                //                        titleValue = titleNode.Attributes["Old"].Value;
                //                        bodyValue = bodyNode.Attributes["Old"].Value;
                //                    }
                //                    else
                //                    {
                //                        //alternative way of getting item id
                //                        int itemId = int.Parse(alertHandler.eventData[i].itemFullUrl.Substring(alertHandler.
                //                        eventData[i].itemFullUrl.LastIndexOf('/') + 1).Replace("_.000", ""));
                //                        SPListItem item = list.GetItemById(itemId);
                //                        if (eventType != "Changed" || eventCount > 1)
                //                        {
                //                            titleValue = item["Title"].ToString();
                //                            bodyValue = item["Body"].ToString();
                //                        }
                //                        else
                //                        {
                //                            if (titleNode.Attributes["New"] != null)
                //                                titleValue = titleNode.Attributes["New"].Value;
                //                            else
                //                                titleValue = titleNode.Attributes["Old"].Value;
                //                            if (bodyNode.Attributes["New"] != null)
                //                                bodyValue = bodyNode.Attributes["New"].Value;
                //                            else
                //                                bodyValue = bodyNode.Attributes["Old"].Value;
                //                        }
                //                    }
                //                    int index = titleValue.IndexOf('.');
                //                    if (index == -1)
                //                        header = titleValue;
                //                    else
                //                        header = titleValue.Substring(0, index);
                //                    body.AppendFormat("{0} <table cellpadding='5' cellspacing='5' > ", style);
                //                    body.AppendFormat(" <tr> <td>link to SharePoint site</td></tr>", webPath);
                //                    body.AppendFormat(" <tr> <td> {0} </td></tr>", titleValue);
                //                    body.AppendFormat(" <tr> <td> {0} </td></tr></table>", bodyValue);
                //                }
                //                catch
                //                {//ignored excepetion
                //                }
                //                //send email only if all alerts are processed 
                //                if ((alertHandler.eventData.Count() - 1) == i)
                //                {
                //                    SPUtility.SendEmail(web, alertHandler.headers, body.ToString());
                //                }
                //            }
                //        }
                //        if (eventCount > 1)
                //            return true;
                //        else
                //            return false;
                //    }

                #endregion

                #region - Custom Email 2 -

                //SPSite site = new SPSite(ahp.siteUrl + ahp.webUrl);
                //SPWeb web = site.OpenWeb();
                //SPList list = web.Lists[ahp.a.ListID];
                //SPListItem item = list.GetItemById(ahp.eventData[0].itemId);

                //string FullPath = HttpUtility.UrlPathEncode(ahp.siteUrl + "/" + ahp.webUrl + "/" + list.Title + "/" + item.Name);
                //string ListPath = HttpUtility.UrlPathEncode(ahp.siteUrl + "/" + ahp.webUrl + "/" + list.Title);
                //string webPath = HttpUtility.UrlPathEncode(ahp.siteUrl + "/" + ahp.webUrl);
                //string eventType = "";

                //string build = "";
                //if (ahp.eventData[0].eventType == 1)
                //    eventType = "Added";
                //else if (ahp.eventData[0].eventType == 2)
                //    eventType = "Changed";
                //else if (ahp.eventData[0].eventType == 3)
                //    eventType = "Deleted";


                //build = "<style type=\"text/css\">.style1 {              font-size: small; border: 1px solid #000000;" +
                //    "background-color: #DEE7FE;}.style2 {               border: 1px solid #000000;}</style></head>" +
                //    "<p><strong>" + item.Name.ToString() + "</strong> has been " + eventType + "</p>" +
                //    "<table style=\"width: 100%\" class=\"style2\"><tr><td style=\"width: 25%\" class=\"style1\">" +
                //    "<a href=" + webPath + "/_layouts/mysubs.aspx>Modify my Settings</a></td>" +
                //    "<td style=\"width: 25%\" class=\"style1\"> <a href=" + FullPath + ">View " + item.Name + "</a></td>" +
                //    "<td style=\"width: 25%\" class=\"style1\"><a href=" + ListPath + ">View " + list.Title + "</a></td>" +
                //    "        </tr></table>";


                //string subject = list.Title.ToString();
                //SPUtility.SendEmail(web, true, false, ahp.headers["to"].ToString(), subject, build);



                //SPUtility.SendEmail(currentWeb, ahp.headers, ahp.body);
                //return false;

                #endregion

                try
                {
                    SPList currentList = spWeb.Lists[ahp.a.ListID] == null ? null : spWeb.Lists[ahp.a.ListID];
                    //SPListItem currentItem = currentList.GetItemById(ahp.eventData[0].itemId) == null ? null : currentList.GetItemById(ahp.eventData[0].itemId);
                    SPUser alertUser = spWeb.AllUsers.GetByID(ahp.a.UserId) == null ? null : spWeb.AllUsers.GetByID(ahp.a.UserId);

                    if (currentList != null && alertUser != null)
                    {
                        foreach (SPField field in currentList.Fields)
                        {
                            if (currentList.Fields[field.Id] != null)
                            {
                                //Zatial nastavene iba pre DisplayMode
                                if (userPermission.GetFieldPermissionForCurrentUser(field, "Hidden", SPControlMode.Display, true, alertUser, spSite))
                                {
                                    removeFields.Add(field);
                                }
                            }
                        }


                        if (removeFields.Count != 0)
                        {
                            HtmlDocument html = new HtmlDocument();
                            html.LoadHtml(ahp.body);

                            foreach (SPField field in removeFields)
                            {
                                XmlDocument xmlSchema = new XmlDocument();
                                xmlSchema.LoadXml(field.SchemaXml);

                                string currentFieldName = xmlSchema.SelectSingleNode("/Field/@DisplayName") == null ? "" : xmlSchema.SelectSingleNode("/Field/@DisplayName").Value;

                                HtmlNode selectedNode = html.DocumentNode.SelectSingleNode("//td[@class='formlabel'][.='" + currentFieldName + ":']");
                                if (selectedNode != null)
                                    selectedNode.ParentNode.Remove();
                            }
                            
                            ahp.body = html.DocumentNode.InnerHtml;
                        }
                    }

                    SPUtility.SendEmail(spWeb, ahp.headers, ahp.body);
                    return false;

                }
                catch (System.Exception ex)
                {
                    SPUtility.SendEmail(spWeb, ahp.headers, ahp.body);
                    return false;
                }
            }
            else
            {
                SPUtility.SendEmail(spWeb, ahp.headers, ahp.body);
                return false;
            }
        }


        static public string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes= System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        #endregion

    }
}
