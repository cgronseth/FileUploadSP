using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.Script.Services;
using System.Web.Services;

namespace FileUploadSP
{
    [ScriptService]
    public partial class WSFileUpload : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static bool GuardarElemento(string titulo, string loginUsuario)
        {
            try
            {
                return DAL.SharePoint2010.GuardarElemento(titulo, loginUsuario);
            }
            catch { }
            return false;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static bool BorrarFicheroTemporal(string nombreFichero, string loginUsuario)
        {
            try
            {
                return DAL.SharePoint2010.BorrarEntradaTemporal(nombreFichero, loginUsuario);
            }
            catch { }
            return false;
        }
    }
}
