using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.WebPartPages;

namespace FileUploadSP
{
    public partial class PaginaFileUpload : WebPartPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Elimina entradas antiguas, si hay, del usuario actual.
                DAL.SharePoint2010.InicializarListaTemporal();
            }
        }
    }
}
