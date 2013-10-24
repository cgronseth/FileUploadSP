/// <reference path="jquery-1.10.2.min.js" />

jQuery(document).ready(function () {
    jQuery("#btnAgregarFichero").click(function (e) {
        e.preventDefault();
        var options = {
            title: "Agregar fichero",
            width: 500,
            height: 130,
            showClose: true,
            dialogReturnValueCallback: function (dialogResult, returnValue) {
                if (dialogResult == "1") {
                    var nuevo = "<div>";
                    nuevo += "<a href=\"#\" class=\"borrarEntrada\">";
                    nuevo += "<input type=\"hidden\" value=\"" + returnValue.fichero + "\">";
                    nuevo += "<img src=\"/_layouts/images/DELETE.GIF\" alt=\"Borrar\" style=\"position:relative;top:4px;border:none\" />";
                    nuevo += "</a>&nbsp;";
                    nuevo += returnValue.fichero;
                    nuevo += "</div>";
                    jQuery("#panelFicheros").append(nuevo);

                    jQuery("a.borrarEntrada").click(function (e2) {
                        e2.preventDefault();
                        if (confirm("Se va a eliminar el fichero de la lista, ¿continuar?")) {
                            var fichero = $(this).children("input[type=hidden]").val();
                            eliminarFicheroTemporal(fichero, $(this).parent("div"));
                        }
                    });
                }
            },
            url: "/_layouts/FileUploadSP/AgregarFichero.aspx"
        };
        SP.UI.ModalDialog.showModalDialog(options);
    });

    jQuery("#btnGuardar").click(function (e) {
        e.preventDefault();

        //Validar entradas
        if (jQuery("input[name=titulo]").val() == "") {
            alert("El título es necesario para guardar");
            return;
        }
        if (jQuery("#panelFicheros").html() == "") {
            alert("Es necesario añadir archivos antes de guardar");
            return;
        }

        guardarElemento();
    });
});

function guardarElemento() {
    var titulo = jQuery("input[name=titulo]").val();
    var loginUsuario = jQuery("input[name=loginUsuario]").val();

    jQuery.ajax({
        url: "_layouts/FileUploadSP/WSFileUpload.aspx/GuardarElemento",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        dataType: "json",
        data: "{'titulo':'" + titulo + "', 'loginUsuario':'" + loginUsuario + "'}",
        success: function (data) {
            if (data.d == true) {
                alert("Guardado OK");
                jQuery("input[name=titulo]").val("");
                jQuery("#panelFicheros").html("");
            } else {
                alert("No se ha podido guardar");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
}

function eliminarFicheroTemporal(nombreFichero, divEntrada) {
    var loginUsuario = jQuery("input[name=loginUsuario]").val();

    jQuery.ajax({
        url: "_layouts/FileUploadSP/WSFileUpload.aspx/BorrarFicheroTemporal",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        dataType: "json",
        data: "{'nombreFichero':'" + nombreFichero + "', 'loginUsuario':'" + loginUsuario + "'}",
        success: function (data) {
            if (data.d == true) {
                divEntrada.remove()
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
        }
    });
}



