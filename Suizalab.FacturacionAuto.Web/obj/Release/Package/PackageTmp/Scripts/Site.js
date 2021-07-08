function loadingItem(section, color, text) {
    if (text === undefined) text = "";
    $(`#${section}`).append(`<div id="loading-item" class="w-100 text-center"><div  class="spinner-grow ${color }" role="status"></div><div class="text-loading text-sky-blue">${text}</div></div>`);
}

function loadItem(section) {
    $("#loading-item").remove();
}