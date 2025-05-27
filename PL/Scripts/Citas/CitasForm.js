
$(document).ready(function () {
    $('#lblURL').hide();
    $('#lblPiso').hide();
});

$("#datetimepicker").datetimepicker({
    dateFormat: "dd-mm-yy",
    timeFormat: "HH:mm",
    showAnim: "clip"
});

function EntrevistaPresencial() {
    $('#lblURL').hide();
    $('#lblPiso').show();
}

function EntrevistaRemota() {
    $('#lblURL').show();
    $('#lblPiso').hide();
}
