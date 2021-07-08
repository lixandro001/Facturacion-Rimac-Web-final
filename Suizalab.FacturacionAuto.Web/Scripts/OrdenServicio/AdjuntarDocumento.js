"use strict";

//const { data } = require("jquery");

let Orden_FechaInicio = "";
let Orden_FechaFin = "";
let Orden_Ticket = "";
let Contoller = "";

//ejecutar funciones una vez cargada
$(document).ready(function () {
    
    Contoller = $('.BoxSearch form').attr('data-controler');
    loadingItem('loadTable', 'text-sky-blue', "Por favor espere, estamos cargando las Ordenes de Biopsia...");

    $('#Calendar-FecAtc-ini').datepicker({
        weekStart: 1,
        autoclose: true,
        todayHighlight: true
    })
    var date = new Date();
    date.setDate(date.getDate() - 2); //menos un mes para la fecha

    $('#Calendar-FecAtc-ini').datepicker("setDate", date);

    $('#Calendar-FecAtc-fin').datepicker("setDate", new Date());

    $('#Calendar-FecAtc-fin').datepicker({
        language: "es"
    })

    let data = { fechaInicio: $('#Calendar-FecAtc-ini').val(), fechaFin: $('#Calendar-FecAtc-fin').val() }
    loadDatos(Contoller, data)

    $('.BoxSearch form').submit(function (e) {
      

        $('.gwdListClientes').addClass('hide')
        $('.boxResult').empty();
        loadingItem('gwdListClientes', 'text-sky-blue', "Por favor espere, estamos cargando los datos...");
        let data = { fechaInicio: $('#Calendar-FecAtc-ini').val(), fechaFin: $('#Calendar-FecAtc-fin').val() }
        loadDatos(Contoller, data)
         
        return false;
    })
 
    // PARA EL PDF
    $(".browse-btn").on("click", function (a) {
        a.preventDefault();
        $("#real-input").click();
    });


    // PARA EL PDF
    $("#real-input").on("change", function () {
        const name = $("#real-input").val().split(/\\|\//).pop();
          
        var htmlName = name.length > 40 ? name.substr(name.length - 20) : name;
        $(".file-info").text(htmlName);
        console.log(htmlName);
        var archivoInput = document.getElementById('real-input');

        //PRevio del PDF
        if (archivoInput.files && archivoInput.files[0]) {
            var visor = new FileReader();
            visor.onload = function (e) {
                document.getElementById('visorArchivo').innerHTML =
                    '<embed src="' + e.target.result + '" width="600" height="375" />';
            };
            visor.readAsDataURL(archivoInput.files[0]);
        }
    });


    $("#btnCerrarModal").on("click", function (e) {
        document.getElementById('visorArchivo').innerHTML = "";
        document.getElementById('filepdf').innerHTML = "";
       /* document.getElementById('real-input').value = "";*/
        $("#real-input").val(null);
        $("#modalAdjuntarArchivo").modal('hide');//ocultamos el modal         
        console.log("entraaa");
    });


    $("#btnCerrarModalv2").on("click", function (e) {

        $("#modalMostrarPdf").modal('hide');//ocultamos el modal  
    });



    $("#EnviarGuardar").on("click", function (e) {
        return new Promise((resolve, reject) => {
            e.preventDefault();
            var codeticket = $("#codeTicket").val();
            var numosc = $("#numosc").val();
            var perosc = $("#perosc").val();
            var anoosc = $("#anoosc").val();
            var numsuc = $("#numsuc").val();
            var numemp = $("#numemp").val();
            var file = $("#real-input")[0].files;
            console.log(file);
            console.log(codeticket);
            var fdata = new FormData();
            fdata.append("FormFile", file[0]);
            fdata.append("Ticket", codeticket);
            fdata.append("numosc", numosc);
            fdata.append("perosc", perosc);
            fdata.append("anoosc", anoosc);
            fdata.append("numsuc", numsuc);
            fdata.append("emposc", numemp);
            if (file.length > 0) {
                // Validacion de extension PDF
                var extension = "";
                var ext = file[0].type;
                var ext2 = ext.split("/");
                if (ext2.length > 1) {
                    extension = ext2[1];
                    console.log(extension);
                    console.log(fdata);
                    if (extension == "pdf" || extension == "PDF") {
                        $.ajax({
                            url: $("#listImagenes").attr('data-controler'),
                            type: "POST",
                            data: fdata,
                            contentType: false,
                            processData: false,
                            async: false,
                            beforeSend: function () { },
                            success: function (response) {
                                console.log(response);
                                if (response.Success == true) {
                                    showConfirmMessageOK('CORRECTO', response.Msj, 'success').then(response => {
                                        if (response) {
                                            location.href = "AdjuntarArchivo"
                                        }
                                    })
                                } else {
                                    reject(response.Message)
                                }
                            },
                            error: function (err) { reject(err) }
                        })
                    } else {
                        showMessage("No Ingreso Un Formato pdf")
                    }
                }
            } else {
                showMessage("Debe Seleccionar Un Archivo Pdf")
            }
        })
    });
})




function GetDataControler(Contoler, _data) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: Contoler,
            data: _data,
            type: "POST",
            success: function (respuesta) {
                respuesta.Success == true ? resolve(respuesta) : reject(respuesta.Message)
                console.log(respuesta);
                console.log("trae datos");
            },
            error: function (err) { reject(err) }
        })
    })
}



function loadDatos(Contoller, data) {
    GetDataControler(Contoller, data)
        .then(async (response) => {
            return LoadTabla(JSON.parse(response.data))
        })
        .then((response) => {           
            loadItem();
            $('#gwdListClientes').removeClass('hide')
        })
        .catch((err) => {
            loadItem();
            showMessage('ATENCIÓN', "Estamos en matenimiento en estos momentos. Comuniquese con el Área de Sistemas. <br>" + err, 'error')
        })
}

   

function LoadTabla(_data) {
    return new Promise((resolve) => {
        var html = '';
        let estado = '';
        let btnActionText = "";
        let btnActionClass = "";
        let btnActionClassv2 = "";
        let btnActionClassv4 = "";
        var listaJson = _data;
        $('.boxResult').empty();
        html += '<table id="tableResult" class="display" style="width:100%"><thead><tr>';
        html += '<th>' + 'Ticket' + '</th>';
        html += '<th>' + 'Paciente' + '</th>';
        html += '<th>' + 'Compañia' + '</th>';
        html += '<th>' + 'EstadoSited' + '</th>';             
        html += '</tr></thead>';
        html += '<tbody>';
        $.each(listaJson, function (index, orden) {
            console.log(orden);
            if (orden.estado == 0) {               
                btnActionClass = "success";
                html += `<tr id="${orden.ticket}" class="OB-${orden.estado}">`
                html += `<td>${orden.ticket}</td>`
                html += `<td>${orden.paciente}</td>`
                html += `<td>${orden.compania.substring(0, 10)}</td>`
                html += `<td><button onClick ="eventShowPopup( '${orden.ticket}','${orden.numosc}','${orden.perosc}','${orden.anoosc}','${orden.numsuc}','${orden.numemp}' )" 
                            class='btn btn-${btnActionClass} subirArchivo'  
                            href='javascript:void(0)'
                            data-numosc='"${orden.numosc}"'
                            data-perosc='"${orden.perosc}"'     
                            data-anoosc='"${orden.anoosc}"' 
                            data-numsuc='"${orden.numsuc}"'  
                            data-code=${orden.ticket}  title='subir archivo' >  <i class='fa fa-upload'></i> </button></td>`                
                html += '</tr>'
            }
            else if (orden.estado == 1) {
                btnActionClassv2 = "primary";
                btnActionClassv4 = "danger";
                html += `<tr id="${orden.ticket}" class="OB-${orden.estado}">`
                html += `<td>${orden.ticket}</td>`
                html += `<td>${orden.paciente}</td>`
                html += `<td>${orden.compania.substring(0, 10)}</td>`                    
                html += `<td> 
                            <button onClick ="eventShowVerPdf( '${orden.ticket}','${orden.numosc}','${orden.perosc}','${orden.anoosc}','${orden.numsuc}','${orden.numemp}' )"
                            class='btn btn-${btnActionClassv2} verpdf'  
                            href='javascript:void(0)'                            
                            data-codeTicket=${orden.ticket}  title='Ver Pdf' >  <i class="fa fa-search" aria-hidden="true"></i> </button>
                            &nbsp;&nbsp;&nbsp;

                            <button onClick ="eventShowEliminarPdf( '${orden.ticket}','${orden.numosc}','${orden.perosc}','${orden.anoosc}','${orden.numsuc}','${orden.numemp}' )"
                            class='btn btn-${btnActionClassv4} EliminarPdf'
                            href='javascript:void(0)'                            
                            data-codeTicket=${orden.ticket}  title='Eliminar Pdf' >  <i class="fa fa-trash" aria-hidden="true"></i> </button>
                         </td>`
                html += '</tr>'
            }            
        })   
        html += '</tbody></table>';
        $('.boxResult ').append(html);
        $('#tableResult').dataTable({ "aaSorting": [[0, "desc"]] });
        resolve("OK => DataTable");
    })
}



function eventShowVerPdf(codeTicket, numosc2, perosc2, anoosc2, numsuc2, numemp2) {    
    console.log(codeTicket);
    console.log(numosc2);
    console.log(perosc2);
    console.log(anoosc2);
    console.log(numsuc2);
    console.log(numemp2);
    let _data = {
        codeTicket: codeTicket,
        numosc: numosc2,
        perosc: perosc2,
        anoosc: anoosc2,
        sucosc: numsuc2,
        emposc: numemp2
    }

    return new Promise((resolve, reject) => {
        $.ajax({
            url: $("#VerPdf").attr('data-controler'),
            xhrFields: { responseType: "blob" },
            data: _data,
            type: "POST",
            success: function (respuesta) {               
                console.log(respuesta);                                  
                var file = new Blob([respuesta], { type: "application/pdf" });
                var fileURL = URL.createObjectURL(file);
                $('#displayPDF').attr('src', fileURL);
                $("#modalMostrarPdf").modal("show");
            },
            error: function (err) { reject(err) }
        })
    })
}





  


function eventShowPopup(code, numosc, perosc, anoosc, numsuc, numemp) {
  
    console.log(code);
    console.log(numosc);
    console.log(perosc);
    console.log(anoosc);
    console.log(numsuc);
    console.log(numemp);
    $("#codeTicket").val(code);
    $("#numosc").val(numosc);
    $("#perosc").val(perosc);
    $("#anoosc").val(anoosc);
    $("#numsuc").val(numsuc);
    $("#numemp").val(numemp);
    $("#modalAdjuntarArchivo").modal("show");

}