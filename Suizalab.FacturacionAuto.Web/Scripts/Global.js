$(function (e) {

    $(".soloEnteros").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
});


function Post(url, params, async) {
    return ajaxMethod(url, "POST", params, async);
}

function PostUpload(url, params, async) {
    return ajaxMethodFile(url, "POST", params, async);
}

function Get(url, params, async) {
    return ajaxMethod(url, "GET", params, async);
}

function ajaxMethod(url, method, params, async) {
    if (async == undefined || async == null) async = true;

    //Pace.restart()
    return $.ajax({
        url: window.appURL + url,
        method: method,
        cache: false,
        data: params,
        async: async

    }).fail(function (jqXHR, textStatus, errorThrown) {
        console.debug(jqXHR);
        console.debug(textStatus);
        console.debug(errorThrown);
    }).always(function () {
        $('#loading-indicator').hide();
    });
}

function ajaxMethodFile(url, method, params, async) {
    if (async == undefined || async == null) async = true;


    return $.ajax({
        url: window.appURL + url,
        method: method,
        cache: false,
        data: params,
        async: async,
        processData: false,
        contentType: false,
    }).fail(function (jqXHR, textStatus, errorThrown) {
        console.debug(jqXHR);
        console.debug(textStatus);
        console.debug(errorThrown);
    }).always(function () {
        $('#loading-indicator').hide();
    });
}

function fnLimpiarTabla(tabla) {
    tabla.fnClearTable();
}

function fnBaseUrlWeb(url) {
    return window.appURL + url;
}

function fnConfirmar(titulo, mensaje, funcion) {
    $.confirm({
        title: titulo,
        content: mensaje,
        buttons: {
            confirm: {
                text: "Sí",
                btnClass: 'btn-blue',
                action: funcion
            },
            cancel: {
                text: "No"
            }
        }
    });
}

function fnAlertar(titulo, mensaje, funcion) {
    $.alert({
        title: titulo,
        content: mensaje,
        buttons: {
            confirm: {
                text: "Ok",
                action: funcion
            }
        }
    });
}

function fnBuildURL(url) {
    return window.appURL + url;
}

function fnLimpiarTabla(tabla) {
    tabla.fnClearTable();
}