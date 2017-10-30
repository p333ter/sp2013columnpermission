using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Web.UI;

namespace SPGuysCustomFieldPermission.Layouts.SPGuysCustomFieldPermission
{
    public partial class ApplicationPage1 : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private string EncodeString(string p)
        {
            p = p.Replace("\"", "'");
            p = p.Replace("\\", "\\\\");
            p = p.Replace("\r", "");
            p = p.Replace("\n", "");
            p = p.Replace("+", "\\u002b");
            p = p.Replace("'", "\\u0027");
            p = p.Replace(">", "\\u003e");
            p = p.Replace("<", "\\u003c");
            p = p.Replace("/", "\\u002f");
            p = p.Replace("/", "\\u002f");
            return p;
        }

        private Control findControl(Control parent, string id)
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
                    control = this.findControl(control2, id);
                    if (control != null)
                    {
                        return control;
                    }
                }
            }
            return null;
        }

        protected override void OnLoad(EventArgs e)
        {

            SPWeb web = SPContext.Current.Web;
            Guid webGuid = web.ID;


            SPList list = SPContext.Current.Web.Lists["Import"];
            Guid listGuid = list.ID;

            SPView currentView = SPContext.Current.Web.Lists["Import"].Views["All Items"];
            Guid viewGuid = currentView.ID;

            string ListData = EncodeString(list.Items.Xml);
            string ViewSchema = EncodeString(currentView.SchemaXml);
            string ListSchema = EncodeString(list.SchemaXml);
            string UrlLink = EncodeString(web.Url);


            string ListData1 = Server.HtmlEncode(list.Items.Xml);
            string ViewSchema1 = Server.HtmlEncode(currentView.SchemaXml);
            string ListSchema1 = Server.HtmlEncode(list.SchemaXml);

            //String csName = "TestExcel";
            //Type csType = this.GetType();


            Control control2 = this.findControl(this.Page, "MSO_ContentTable");

            LiteralControl RenderActiveX = new LiteralControl();
            
            string csScript = "<script type='text/javascript'>RenderActiveX('\u003cobject id=\u0022STSListControlWPQ2\u0022 name=\u0022STSListControlWPQ2\u0022 classid=CLSID:65BCBEE4-7728-41a0-97BE-14E1CAE36AAE class=\u0022ms-dlgDisable\u0022 width=\u002299\u0025\u0022 tabindex=\u00221\u0022\u003e\u003cparam name=\u0022ListName\u0022 value=\u0022{" + listGuid + "}\u0022\u003e\u003cparam name=\u0022ViewGuid\u0022 value=\u0022{" + viewGuid + "}\u0022\u003e\u003cparam name=\u0022ListWeb\u0022 value=\u0022" + UrlLink + "\u002f_vti_bin\u0022\u003e\u003cparam name=\u0022ListData\u0022 value=\u0022"+ListData+"\u0022\u003e\u003cparam name=\u0022ViewSchema\u0022 value=\u0022" + ViewSchema + "\u0022\u003e\u003cparam name=\u0022ListSchema\u0022 value=\u0022"+ ListSchema +"\u0022\u003e\u003cparam name=\u0022ControlName\u0022 value=\u0022STSListControlWPQ2\u0022\u003e\u003cp class=\u0022ms-descriptiontext\u0022\u003eThe Datasheet view of this list cannot be displayed. Please wait while the page is redirected to Standard view. If your list does not appear in a few moments, \u003ca onclick=\u0022javascript:GCNavigateToNonGridPage\u0028\u0029; javascript:return false;\u0022 href=\u0022?ShowInGrid=False\u0022 target=_self\u003eopen the list in Standard view\u003c\u002fa\u003e\u003c\u002fp\u003e\u003c\u002fOBJECT\u003e');</script>";
            
            //literal1.Text = csScript;
            
            //ClientScriptManager clietnScript = this.Page.ClientScript;
            //clietnScript.RegisterClientScriptBlock(csType, csName, csScript);

            RenderActiveX.Text = csScript;

            control2.Controls.Add(RenderActiveX);

            //http://rajendrashekhawat.blogspot.sk/2008/09/custom-datasheet-view-using-listnet.html

        }
    }
}
