
Puesta en marcha en una web SP:

1. Crear librería de documentos "Temporal" sin ninguna modificación adicional.
2. Crear lista personalizada "Documentos" sin ninguna modificación adicional.
3. En las constantes de DAL.SharePoint2010.cs cambiar URL_WEB por la web donde está instalado.
4. Asegurarse de tener la biblioteca de páginas que se indica en la URL de la etiqueta Module de Elements.xml (Paginas)
5. Implementar la solución. Las características tienen ámbito web para el Módulo y ámbito site para WebParts.


Instrucciones sobre cómo agregar páginas con módulos:
http://amalhashim.wordpress.com/2013/02/14/sharepoint-deploy-webpart-page-using-module/