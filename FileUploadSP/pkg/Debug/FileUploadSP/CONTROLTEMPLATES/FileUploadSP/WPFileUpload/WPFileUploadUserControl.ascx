<%@ Assembly Name="FileUploadSP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4062c6340457b78c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WPFileUploadUserControl.ascx.cs" Inherits="FileUploadSP.WPFileUpload.WPFileUploadUserControl" %>

<link rel="Stylesheet" href="/_layouts/FileUploadSP/jquery-ui-1.10.3/themes/base/jquery-ui.css" />
<script type="text/javascript" src="/_layouts/FileUploadSP/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="/_layouts/FileUploadSP/jquery-ui-1.10.3/ui/minified/jquery-ui.min.js"></script>
<script type="text/javascript" src="/_layouts/FileUploadSP/jsFileUpload.js"></script>

<h3>Demo FileUploadSP</h3>
<hr />

<label style="width:90px">Título:</label>
<input type="text" name="titulo" style="width:200px" />
<br />
<br />
<label style="width:90px">Ficheros:</label>
<div id="panelFicheros"></div>
<a href="#" id="btnAgregarFichero">
    <img src="/_layouts/images/UPLOAD.GIF" alt="Nuevo fichero" style="position:relative;top:4px;border:none" />
    Agregar nuevo fichero
</a>
<br />
<br />
<button id="btnGuardar">Guardar</button>

