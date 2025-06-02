
$(document).ready(function () {
    forms();
});


function forms() {
    let idCita = cita.IdCita;
    DDLEstatusCita(IdCita);
    if (cita.Piso.IdPiso != 0 || cita.URL != null || cita.Piso.IdPiso != null) {
        if (cita.Piso.IdPiso == 0 || cita.Piso.IdPiso == null) {
            $('#formRemoto').show();
            $('#formPresencial').hide();
        }
        if (cita.URL == null) {
            $('#formRemoto').hide();
            $('#formPresencial').show();
        }
    }
    else {
        $('#formRemoto').hide();
        $('#formPresencial').hide();
    }
    //console.log(cita);
}

$("#datetimepicker").datetimepicker({
    dateFormat: "dd-mm-yy",
    timeFormat: "HH:mm",
    showAnim: "clip"
});
$("#datetimepickerPresencial").datetimepicker({
    dateFormat: "dd-mm-yy",
    timeFormat: "HH:mm",
    showAnim: "clip"
});

function EntrevistaPresencial() {
    $('#formRemoto').hide();
    $('#formPresencial').show();
}

function EntrevistaRemota() {
    $('#formRemoto').show();
    $('#formPresencial').hide();
}

function DDLEstatusCita(idCita) {
    var estatus = $(".ddlEstatusCita").val();
    console.log(estatus);
    if (idCita == 0) {
        console.log("no hay cita");
    }
    else {
        console.log("si hay cita");
    }
}