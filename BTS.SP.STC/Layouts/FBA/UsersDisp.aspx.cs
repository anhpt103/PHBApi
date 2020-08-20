using System;
using Microsoft.SharePoint.WebControls;

namespace BTS.SP.STC.Layouts.FBA
{
    public partial class UsersDisp : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //lblMessage.Text = "Count=" + (new UserManager()).SelectAll().Count.ToString();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "ex " + ex.Message;
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {
        }
    }
}