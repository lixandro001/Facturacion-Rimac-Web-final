"use strict";
let UrlListaCompanias = ""

$(document).ready(function () {        
    UrlListaCompanias =  $('#divListaCompanias').attr('data-controler')

    GetDataControler(UrlListaCompanias,{}).then((response) => {            
        return LoadListaCompania(JSON.parse(response.data))
    }).then(response=>{
        $(".preloader").fadeOut();
    })
    .catch((err)=>{
        console.log(err)
    })
    
})

function GetDataControler(Contoler,_data) {            
    return new Promise((resolve, reject) => {
        $.ajax({
            url: Contoler,
            data: _data,
            type: "POST",
            success: function (respuesta) {                                  
                respuesta.Success==true ? resolve(respuesta) :  reject(respuesta.Message)                
            },
            error: function (err) { reject(err) }
        })
    })
}

function LoadListaCompania(_data){
    return new Promise((resolve) => {
        var html = '';
        let estado = '';
        let btnActionText="";
        let btnActionClass ="";

        $('#divListaCompanias').empty();
        html += '<table id="tableCompanias" class="display" style="width:100%"><thead><tr>';
        html += '<th>' + 'CODIGO' + '</th>';
        html += '<th>' + 'COMPAÑIA' + '</th>';
        html += '<th>' + 'ESTADO' + '</th>';
        html += '<th></th>';
        html += '</tr></thead>';
        html += '<tbody>'; 
        $.each(_data, function (index, compania) {
            if(compania.Estado==0){
                estado = "DESACTIVADO"
                btnActionText = "Activar"
                btnActionClass = "secondary";
            }else{
                estado = "ACTIVADO"
                btnActionText = "Desactivar"
                btnActionClass = "success"
            }
            html += `<tr id ="${compania.Numcia}">`
            html += `<td>${compania.Codigocia}</td>`                       
            html += `<td>${compania.Nomcia}</td>`                     
            html += `<td>${estado}</td>`               
            html += `<td><button type="button" class="btn btn-${btnActionClass}" onclick="SelectCompania(this)">${btnActionText}</button></td>`
            html += '</tr>'
        })
        html += '</tbody></table>';
        $('#divListaCompanias').append(html);
        $('#tableCompanias').dataTable({ "aaSorting": [[1, "asc"]] });
        resolve("OK => ListaCompania");
    })
}

function SelectCompania(item){
    let accion = $(item).html()
    let xxCodigoCompania = $(item).parent().parent().attr('id')
    showConfirmMessage('ATENCIÓN', "¿Está segur@ de "+accion+" ésta compania para su facturación?", "Si, "+accion,"Cancelar").then(response=>{
        if(response)    
        {   
            let data = { numcia: xxCodigoCompania, estado: accion }
            
            GetDataControler($('.updateCompania').attr('data-controler'),data)
            .then((response) => {
                showMessage('CORRECTO', response.Message, 'success')   
                return GetDataControler(UrlListaCompanias,{})
            }).then(response=>{
                return LoadListaCompania(JSON.parse(response.data))
            }).then((response) => {                                          
            })
            .catch((err)=>{    
                showMessage('ATENCIÓN', "Estamos en matenimiento en estos momentos. Comuniquese con el Área de Sistemas. "+err, 'error')
            })
        }
    })
    
}
