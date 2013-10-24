using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace FileUploadSP.DAL
{
    class SharePoint2010
    {
        private const string URL_WEB = "http://vdi7shrpt005:88/fileupload";

        private const string BIBLIOTECA_TEMPORAL = "Temporal";
        private const string LISTA_DOCUMENTOS = "Documentos";
        private const string SUBWEB = "fileupload";
        private const string CAMPO_TITLE = "Title";
        private const string CAMPO_MODIFICADO_POR = "Editor";

        public static bool CrearEntradaTemporal(string nombreFichero, byte[] binFile)
        {
            using (SPSite oSite = new SPSite(URL_WEB))
            {
                using (SPWeb oWeb = oSite.OpenWeb())
                {
                    SPList listaTemporal = oWeb.Lists.TryGetList(BIBLIOTECA_TEMPORAL);
                    if (listaTemporal != null)
                    {
                        string loginUsuario = oWeb.CurrentUser.LoginName;
                        loginUsuario = loginUsuario.Replace('\\', '/').Replace('/', '_');

                        SPFolder biblioteca = oWeb.Folders[BIBLIOTECA_TEMPORAL];
                        SPFolderCollection carpetas = biblioteca.SubFolders;

                        SPFolder carpetaUsuario = null;
                        foreach (SPFolder c in carpetas)
                        {
                            if (c.Url.Contains(loginUsuario))
                            {
                                carpetaUsuario = c;
                                break;
                            }
                        }
                        if (carpetaUsuario == null)
                        {
                            carpetaUsuario = carpetas.Add(loginUsuario);
                        }

                        SPFile fichero = carpetaUsuario.Files.Add(nombreFichero, binFile);
                        carpetaUsuario.Update();

                        return true;
                    }
                }
            }
            return false;
        }

        public static bool BorrarEntradaTemporal(string nombreFichero, string loginUsuario)
        {
            using (SPSite oSite = new SPSite(URL_WEB))
            {
                using (SPWeb oWeb = oSite.OpenWeb())
                {
                    try
                    {
                        oWeb.AllowUnsafeUpdates = true;

                        SPList listaTemporal = oWeb.Lists.TryGetList(BIBLIOTECA_TEMPORAL);
                        if (listaTemporal != null)
                        {
                            SPFolder biblioteca = oWeb.Folders[BIBLIOTECA_TEMPORAL];
                            SPFolderCollection carpetas = biblioteca.SubFolders;

                            foreach (SPFolder c in carpetas)
                            {
                                if (c.Url.Contains(loginUsuario))
                                {
                                    foreach (SPFile fichero in c.Files)
                                    {
                                        if (fichero.Name.Equals(nombreFichero))
                                        {
                                            fichero.Delete();
                                            c.Update();
                                            return true;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    catch { }
                    finally
                    {
                        oWeb.AllowUnsafeUpdates = false;
                    }
                }
            }
            return false;
        }

        public static bool InicializarListaTemporal()
        {
            using (SPSite oSite = new SPSite(URL_WEB))
            {
                using (SPWeb oWeb = oSite.OpenWeb())
                {
                    try
                    {
                        oWeb.AllowUnsafeUpdates = true;

                        SPList listaTemporal = oWeb.Lists.TryGetList(BIBLIOTECA_TEMPORAL);
                        if (listaTemporal == null)
                            return false;

                        string loginUsuario = oWeb.CurrentUser.LoginName;
                        loginUsuario = loginUsuario.Replace('\\', '/').Replace('/', '_');

                        SPFolder biblioteca = oWeb.Folders[BIBLIOTECA_TEMPORAL];

                        foreach (SPFolder c in biblioteca.SubFolders)
                        {
                            if (c.Url.Contains(loginUsuario))
                            {
                                for (int i = c.Files.Count - 1; i >= 0; i--)
                                {
                                    c.Files[i].Delete();
                                    c.Update();
                                }
                                c.Delete();
                                biblioteca.Update();

                                break;
                            }
                        }
                    }
                    catch { }
                    finally
                    {
                        oWeb.AllowUnsafeUpdates = false;
                    }
                }
            }
            return true;
        }

        public static bool GuardarElemento(string titulo, string loginUsuario)
        {
            bool bReturn = false;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite oSite = new SPSite(URL_WEB))
                {
                    using (SPWeb oWeb = oSite.OpenWeb())
                    {
                        try
                        {
                            oWeb.AllowUnsafeUpdates = true;
                            SPList listaTemporal = oWeb.Lists.TryGetList(BIBLIOTECA_TEMPORAL);
                            SPList listaDocumentos = oWeb.Lists.TryGetList(LISTA_DOCUMENTOS);
                            if (listaTemporal != null && listaDocumentos != null)
                            {
                                SPListItem li = listaDocumentos.Items.Add();
                                li[CAMPO_TITLE] = titulo;
                                li.Update();

                                SPFolder biblioteca = oWeb.Folders[BIBLIOTECA_TEMPORAL];

                                foreach (SPFolder c in biblioteca.SubFolders)
                                {
                                    if (c.Url.Contains(loginUsuario))
                                    {
                                        for (int i = c.Files.Count - 1; i >= 0; i--)
                                        {
                                            li.Attachments.AddNow(c.Files[i].Name, c.Files[i].OpenBinary(SPOpenBinaryOptions.SkipVirusScan));
                                        }
                                        break;
                                    }
                                }

                                //ahora se pueden borrar
                                foreach (SPFolder c in biblioteca.SubFolders)
                                {
                                    if (c.Url.Contains(loginUsuario))
                                    {
                                        for (int i = c.Files.Count - 1; i >= 0; i--)
                                        {
                                            c.Files[i].Delete();
                                            c.Update();
                                        }
                                        c.Delete();
                                        biblioteca.Update();

                                        break;
                                    }
                                }
                            }

                            bReturn = true;
                        }
                        catch { }
                        finally
                        {
                            oWeb.AllowUnsafeUpdates = false;
                        }
                    }
                }
            });
            return bReturn;
        }

        public static SPUser GetSPUser(SPListItem item, string key)
        {
            SPUser user = null;
            SPFieldUserValue userValue = new SPFieldUserValue(item.Web, item[key].ToString());
            if (userValue != null)
            {
                user = userValue.User;
            }
            return user;
        }
    }
}
