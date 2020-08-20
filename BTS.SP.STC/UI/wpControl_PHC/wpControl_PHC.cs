using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace BTS.SP.STC.UI.wpControl_PHC
{
    [ToolboxItemAttribute(false)]
    public class wpControl_PHC : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/BTS.SP.STC.UI/wpControl_PHC/wpControl_PHCUserControl.ascx";

        protected override void CreateChildControls()
        {
            ChromeType = PartChromeType.None;
            Control control = Page.LoadControl(_ascxPath);
            Controls.Add(control);
        }
    }
}
