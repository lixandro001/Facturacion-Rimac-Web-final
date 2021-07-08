let listaOrdenes = "",
app = {
    load:()=>{
        Contoller = $('#gwdListOrdenes').attr('data-controller');        
        loadingItem('loadTable', 'text-sky-blue', "Por favor espere, estamos cargando las Ordenes de Biopsia...");
        app.loadDatos(Contoller)
    },

    loadDatos:(Contoller)=>{
        app.GetDataControler(Contoller)
            .then((response) => {
                listaOrdenes=JSON.parse(response.data)
                return app.LoadTabla(listaOrdenes)
            })
            .then((response) => {
                loadItem();
                $('#gwdListOrdenes').removeClass('hide')
            })
            .catch((err) => {
                loadItem();
                console.log("Estamos en mantenimiento en estos momentos. Comuniquese con el Área de Sistemas. <br>" + err)
            })
    },

    GetDataControler:(Contoler)=>{        
        return new Promise((resolve, reject) => {
           $.ajax({
                url: Contoler,
                data: {},
                type: "POST",
                success: (respuesta) =>{    
                    respuesta.Success == true ? resolve(respuesta) : reject(respuesta.Message)
                },
                error: (err)=> reject(err)
            })

        })
    },
    LoadTabla:(_data)=>{
        return new Promise((resolve) => {
            let html = ''
            let listaJson = _data  
            let mes_unico= ""  
            var options = { year: 'numeric', month: 'numeric', day: 'numeric' };            
            $('.boxResult').empty()
            html += '<table id="tableResult" class="table table-hover" style="width:100%"><thead class="thead-dark"><tr>';            
            html += '<th>' + 'TICKET' + '</th>';
            html += '<th>' + 'FECHA' + '</th>';
            html += '<th>' + 'PACIENTE' + '</th>';
            html += '<th>' + 'SEDE' + '</th>';
            html += '<th>' + 'USUARIO' + '</th>';
            html += '<th></th>';
            html += '</tr></thead>';
            html += '<tbody>';
            $.each(listaJson,(index, orden)=>{                
                let f = new Date(orden.Finoscab)
                if(index==0){  
                    mes_unico =  f.getMonth()
                    app.AgregarfiltroMes(mes_unico)
                }
                else{
                    if(mes_unico!=f.getMonth()){
                        mes_unico =f.getMonth()
                        app.AgregarfiltroMes(mes_unico)
                    }
                }
                
                html += `<tr class="OrdenesServicio os-${f.getMonth()}">`
                html += `<td>${orden.Ticket}</td>`
                html += `<td>${f.toLocaleDateString("es-ES", options)}</td>`
                html += `<td>${orden.Paciente}</td>`
                html += `<td>${orden.Numsuc}</td>`
                html += `<td>${orden.usumod}</td>`
                html += `<td></td>`
                html += '</tr>'
            })
            html += '</tbody></table>';
            $('.boxResult ').append(html)    
            $('.totalOrdenes').html($('.OrdenesServicio').length)        
            resolve("OK => DataTable")
        })        
    },
    AgregarfiltroMes:(mes)=>{
        let meses = new Array ("Enero","Febrero","Marzo","Abril","Mayo","Junio","Julio","Agosto","Septiembre","Octubre","Noviembre","Diciembre");
        let mesDesc = meses[mes]
        $('#cmbFecha').append(`<option value="os-${mes}">${mesDesc}</option>`)
    },
    filtroOrdenes:(mes)=>{
        if(mes=='0'){
            $('.OrdenesServicio').removeClass('hide')            
            $('.totalOrdenes').html($('.OrdenesServicio').length)
        }else{
            $('.OrdenesServicio').addClass('hide')
            $(`.${mes}`).removeClass('hide')
            $('.totalOrdenes').html($(`.${mes}`).length)
        }        
    }
}


$(document).ready(()=>{ 
    app.load()

    $('#cmbFecha').change(function() {
        let mes = "";
        $('#cmbFecha option:selected').each(()=>{
            mes = $(this).val()
        })
        app.filtroOrdenes(mes)
    }).trigger( "change" )    
})

function fitro(obj){app.AgregarOrden(obj)}