"use strict";

let xxFecha = ""
let xxCodigoCompania = ""
let UrlListaOrdenes = ""
let UrlListaCompanias = ""
let tipoOrdenServicio =""

$(document).ready(function () {        
    UrlListaOrdenes =  $('#divListaOrdenes').attr('data-controler')
    UrlListaCompanias =  $('#divListaCompanias').attr('data-controler')
    
    $('#Calendar-FecAtc-ini').datepicker({
        weekStart: 1,
        autoclose: true,
        todayHighlight: true
    })
    $('#Calendar-FecAtc-ini').datepicker("setDate", new Date());

    $('#Calendar-FecAtc-fin').datepicker({
        weekStart: 1,
        autoclose: true,
        todayHighlight: true
    })
    $('#Calendar-FecAtc-fin').datepicker("setDate", new Date());


    GetDataControler(UrlListaCompanias,{}).then((response) => {            
        return LoadListaCompania(JSON.parse(response.data))
    }).then(response=>{
        $(".preloader").fadeOut();
    })
    .catch((err)=>{
        console.log(err)
    })

    
    $('#btnSeleccionarCompania').on('click', function () {
        $('#tableCompanias_filter input').trigger('focus')
        $('#divModalCompanias').modal('show')
    })

    $('#form-BuscarAtenciones').submit(function (e) {
        e.preventDefault()
        if(xxCodigoCompania!=""){
            xxFecha = $('#Calendar-FecAtc-ini').val()+"_"+$('#Calendar-FecAtc-fin').val();
            loadingItem('divListaOrdenes', 'text-info', "Por favor espere, estamos cargando las atenciones...");        
            
            let data = { finoscab: xxFecha, numcia: xxCodigoCompania }
            
            GetDataControler(UrlListaOrdenes,data)
            .then((response) => {
                 LoadListaOrdenes(JSON.parse(response.data))
                 loadItem();
            })
            .catch((err)=>{    
                showMessage('ATENCIÓN', "Estamos en matenimiento en estos momentos. Comuniquese con el Área de Sistemas. "+err, 'error')
            })
        }else
            showMessage('ATENCIÓN', "Selecciones una compañia.", 'error')        

        return false;
    })

    $('#form-UpdateOrden').submit(function (e) {
        e.preventDefault()
        let monto = $('#inpMonto').val();
        if(monto!=""){
            showConfirmMessage('ATENCIÓN', "¿Está seguro de agregar S/."+monto+" al total de todos los tickets de la lista?", "Si, Registrar","Cancelar").then(response=>{
                if(response)
                {   
                    $('#divListaOrdenes').empty();

                    loadingItem('divListaOrdenes', 'text-info', "Por favor espere, estamos registrando monto en los tickets...");        
                    
                    let data = { finoscab: xxFecha, numcia: xxCodigoCompania, valventa: monto}

                    GetDataControler($(this).attr('action'),data)
                    .then((response) => {
                        tipoOrdenServicio = "Actualizado";            
                        loadItem();    
                        showMessage('CORRECTO', response.Message, 'success') 
                        
                        loadingItem('divListaOrdenes', 'text-info', "Por favor espere, estamos cargando las atenciones...");        
            
                        let data = { finoscab: xxFecha, numcia: xxCodigoCompania }
                        
                        GetDataControler(UrlListaOrdenes,data)
                        .then((response) => {
                            LoadListaOrdenes(JSON.parse(response.data))
                            loadItem();

                            $('#inpMonto').val("");    
                            $('#divRegateo').addClass("Hide");
                        })     
                    })
                    .catch((err)=>{    
                        loadItem();    
                        showMessage('ATENCIÓN', err, 'error')   
                    })                         
                }
                
            })
        }else
            showMessage('ATENCIÓN', "Debes Ingresar el Monto de regateo.", 'error')        

        return false;
    })
    
})

function GetDataControler(Contoler,_data) {            
    return new Promise((resolve, reject) => {
        $.ajax({
            url: Contoler,
            data: _data,
            type: "POST",
            success: function (respuesta) {     
               // console.log(respuesta)                  
                respuesta.Success==true ? resolve(respuesta) :  reject(respuesta.Message)                
            },
            error: function (err) { reject(err) }
        })
    })
}


function LoadListaCompania(_data){
    return new Promise((resolve) => {
        var html = '';
        $('#divListaCompanias').empty();
        html += '<table id="tableCompanias" class="display" style="width:100%"><thead><tr>';
        html += '<th>' + 'CODIGO' + '</th>';
        html += '<th>' + 'COMPAÑIA' + '</th>';
        html += '</tr></thead>';
        html += '<tbody>'; 
        $.each(_data, function (index, compania) {
            html += `<tr id ="${compania.Numcia}" ondblclick="SelectCompania(this)">`
            html += `<td>${compania.Codigocia}</td>`                       
            html += `<td>${compania.Nomcia}</td>`
            html += '</tr>'
        })
        html += '</tbody></table>';
        $('#divListaCompanias').append(html);
        $('#tableCompanias').dataTable({ "aaSorting": [[1, "asc"]] });
        resolve("OK => ListaCompania");
    })
}

function SelectCompania(item){
    $('#tableCompanias_filter input').val("");    
    xxCodigoCompania = item.id;
    $('#divDatosCompania #spnCompaniaId').text($(item).find('td').eq(0).html());
    $('#divDatosCompania #spnCompaniaName').val($(item).find('td').eq(1).html());        
    $('#divModalCompanias').modal('hide')
}

function LoadListaOrdenes(_data){
        //console.log(_data)
        var html = '';
        $('#divListaOrdenes').empty();
        html += '<table id="tableOSC" class="display" style="width:100%"><thead><tr>';
        html += '<th>' + 'TICKET' + '</th>';
        html += '<th>' + 'FECHA ATC' + '</th>';
        html += '<th>' + 'SEXO' + '</th>';
        html += '<th>' + 'PACIENTE' + '</th>';
        html += '</tr></thead>';
        html += '<tbody>'; 
        $.each(_data, function (index, orden) {
            html += `<tr ondblclick="SelectOrder(this)">`
            html += `<td>${orden.Ticket}</td>`                       
            html += `<td>${orden.Finoscab}</td>`                    
            html += `<td>${orden.Sexo}</td>`                 
            html += `<td>${orden.Paciente}</td>`
            html += '</tr>'
            if(index==_data.length-1){            
                $('#divRegateo').removeClass('Hide') 
            }
        })        
        html += '</tbody></table>';
        $('#divListaOrdenes').append(html);
        $('#divListaOrdenes').addClass(tipoOrdenServicio);
        $('#tableOSC').dataTable({ "aaSorting": [[1, "asc"]] });
}


function SelectOrder(item){
    let urlControler = $('#divListaServiciosDet').attr('data-controler')
    let data = {ticket:$(item).find('td').eq(0).html()}

    GetDataControler(urlControler,data)
            .then((response) => {
                return LoadListaOrdenesDetalle(JSON.parse(response.data))                           
            })
            .then(response=>{
                $('#divModalServicios').modal('show')
            })
            .catch((err)=>{    
                showMessage('ATENCIÓN', "Estamos en matenimiento en estos momentos. Comuniquese con el Área de Sistemas. "+err, 'error')
            })
}

function LoadListaOrdenesDetalle(_data){
    return new Promise(resolve=>{    
        var html = '';
        $('#divListaServiciosDet').empty();
        html += '<table id="tableOSD" class="display" style="width:100%"><thead><tr>';
        html += '<th>' + 'N° SERVICIO' + '</th>';
        html += '<th>' + 'DESCRIPCIÓN' + '</th>';
        html += '<th>' + 'CANTIDAD' + '</th>';
        html += '<th>' + 'PRECIO UNIT' + '</th>';
        html += '<th>' + 'VALOR UNIT' + '</th>';
        html += '</tr></thead>';
        html += '<tbody>'; 
        $.each(_data, function (index, orden) {
            html += `<tr>`
            html += `<td>${orden.numfox}</td>`                       
            html += `<td>${orden.desser}</td>`                    
            html += `<td>${orden.cantdet}</td>`                 
            html += `<td>${orden.presol}</td>`              
            html += `<td>${orden.valventa}</td>`
            html += '</tr>'
            if(index==_data.length-1){            
                resolve("ok=>Lista de detalles") 
            }
        })        
        html += '</tbody></table>';
        $('#divListaServiciosDet').append(html);
        $('#divListaServiciosDet').addClass(tipoOrdenServicio);
    })
}