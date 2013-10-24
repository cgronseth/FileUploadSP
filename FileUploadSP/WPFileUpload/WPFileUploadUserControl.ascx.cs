using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

namespace FileUploadSP.WPFileUpload
{
    public partial class WPFileUploadUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.Page.ClientScript.RegisterHiddenField("loginUsuario",
                    SPContext.Current.Web.CurrentUser.LoginName.Replace('\\', '/').Replace('/', '_'));
            }
        }
    }
}
