function showBasicMessage(text) {
    swal(text);
}

function showWithTitleMessage(message,text) {
    swal(message, text);
}

//allowOutsideClick = true;
function showMessage(title, message, typeBtn) {
    // typeBtn = 'success', 'error', 'warning', 'info' 
    swal({
        title: title,
        text: message,
        type: typeBtn,
        showCancelButton: false,
        confirmButtonColor: "#F44336",
        confirmButtonText: 'Ok',
        closeOnConfirm: true
    });        
}

function showConfirmMessage(question, msg, btnConfirm,btnCancel) {

    if(btnConfirm===undefined) btnConfirm="Si, eliminar!";    
    if(btnCancel===undefined) btnCancel="Cancelar";    
    
    return new Promise(resolve=>{
        swal({
            title: question,
            text: msg,
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#F44336",
            confirmButtonText: btnConfirm,
            cancelButtonText: btnCancel
        }, function (isConfirm) {        
            resolve(isConfirm)
        }); 
    })

}


function showCancelMessage(question, msg,btnConfirm,btnCancel,colorConfirm) {
    let confirm = "";  
    if(btnConfirm===undefined) btnConfirm="Si, eliminar!";
    if(btnCancel===undefined) btnCancel="Cancelar";    
    if(colorConfirm===undefined) colorConfirm="#F44336";    
    swal({
        title: question,
        text: msg,
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: colorConfirm,
        confirmButtonText: btnConfirm,
        cancelButtonText: btnCancel,
        closeOnConfirm: false,
        closeOnCancel: false
    }, function (isConfirm) {
        if (isConfirm) {
           // swal("Deleted!", msgConfirm, "success");
            confirm = true;
        } else {
            //swal("Cancelled", msgCancel, "error");
            confirm = false;
        }
    })

    return confirm;
}

//Alerta con Imagen
function showWithCustomIconMessage(title,msg,imgURL) {
    let confirm = false;
    swal({
        title: title,
        text: msg,
        imageUrl: imgURL
    },function(){
        confirm = true;
    });

    return confirm;
}

//Alerta con Imagen
function showMessageWithImage(title, msg, imgURL) {
    swal({
        title: title,
        text: msg,
        imageUrl: imgURL,
        showConfirmButton: false
    });
}

//Alerta dinámica con HTML
function showHtmlMessage() {
    swal({
        title: "HTML <small>Title</small>!",
        text: "A custom <span style=\"color: #CC0000\">html<span> message.",
        html: true
    });
}

//Alerta con input para ingresar
function showPromptMessage(title,text,placeholder,msgError) {
    let data = "";
    swal({
        title: title,
        text: text,
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        animation: "slide-from-top",
        inputPlaceholder: placeholder
    }, function (inputValue) {
        if (inputValue === false) return false;
        if (inputValue === "") {
            swal.showInputError(msgError); 
            return false
        }else{
            data = inputValue;
        }        
    });
    return data;
}

function showAutoCloseTimerMessage() {
    swal({
        title: "Auto close alert!",
        text: "I will close in 2 seconds.",
        timer: 2000,
        showConfirmButton: false
    });
}

function showAjaxLoaderMessage() {
    swal({
        title: "Ajax request example",
        text: "Submit to run ajax request",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
    }, function () {
        setTimeout(function () {
            swal("Ajax request finished!");
        }, 2000);
    });
}