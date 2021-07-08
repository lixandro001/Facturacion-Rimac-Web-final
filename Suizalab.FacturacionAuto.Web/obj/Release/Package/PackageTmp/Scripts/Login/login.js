"use strinct"

$(document).ready(function () {
    $('#txtUsuario').focus();
    $('#login form').submit(function (e) {
        e.preventDefault();
        
        let Usuario = $('#txtUsuario').val();
        let Contrasenia = $('#txtPassword').val();
        $('#login button span').removeClass('invisible');
        let _data = {
            NombreUsuario: Usuario,
            Contrasenia: Contrasenia
        }
        console.log(_data)
        $.ajax({
            type: "POST",
            url: $(this).attr('action'),
            content: "application/json; charset=utf-8",
            dataType: "json",
            data: _data,
            beforeSend: function () { showMessageWithImage("","Estamos validado su sesión. Por favor espere.","../Images/load.GIF") },
            success: function (response) {
                $('#login button span').addClass('invisible');
                if (response.Success) {
                    location.href=response.ruta
                }
                else {
                    showMessage('Incorrecto', response.Message, 'warning')                            
                    $('#txtPassword').focus()
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                loadItem()
                $('#login button span').addClass('invisible');
                showMessage('ERROR', 'Error!', 'error')  
            }
        })
        
    })
})
