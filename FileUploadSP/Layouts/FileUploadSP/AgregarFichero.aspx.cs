using System;
using Microsoft.SharePoint.WebControls;
using System.IO;

namespace FileUploadSP
{
    public partial class AgregarFichero : LayoutsPageBase
    {
        private const int MAX_FILESIZE = 204800; //sobre 200KB

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (fichero.HasFile)
                {
                    if (fichero.PostedFile.ContentLength > MAX_FILESIZE)
                        throw new Exception("Se ha superado el tamaño máximo de fichero");

                    string strFileName = fichero.PostedFile.FileName;
                    strFileName = strFileName.Substring(strFileName.LastIndexOf('\\') + 1);
   
                    Stream fStream = fichero.PostedFile.InputStream;

                    byte[] binFile = new byte[fichero.PostedFile.ContentLength];

                    fStream.Read(binFile, 0, binFile.Length);

                    bool bOK = DAL.SharePoint2010.CrearEntradaTemporal(strFileName, binFile);

                    if (bOK)
                    {
                        string js = "";
                        js += "var data={'fichero':'" + strFileName + "'};";
                        js += "SP.UI.ModalDialog.commonModalDialogClose(1, data);";
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Resultado", js, true);
                    }
                    else Status.Text = "No se pudo cargar el fichero.";
                }
            }
            catch (Exception ex)
            {
                Status.Text = "Error: " + ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            string js =  "SP.UI.ModalDialog.commonModalDialogClose(0, 0);";
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Cancelar", js, true);
        }
    }
}
