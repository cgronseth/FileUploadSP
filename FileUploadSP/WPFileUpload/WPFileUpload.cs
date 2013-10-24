using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace FileUploadSP.WPFileUpload
{
    [ToolboxItemAttribute(false)]
    public class WPFileUpload : WebPart
    {
        // Visual Studio puede actualizar automáticamente esta ruta de acceso cuando cambie el elemento de proyecto Elemento web visual.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/FileUploadSP/WPFileUpload/WPFileUploadUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            Controls.Add(control);
        }
    }
}
