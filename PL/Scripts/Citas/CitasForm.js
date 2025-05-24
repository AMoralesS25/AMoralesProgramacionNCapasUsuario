
$(document).ready(function () {
    $('#lblURL').hide();
    $('#lblPiso').hide();

    
});

$("#datepicker").datepicker();
dateFormat = "dd-mm-yy";
showAnim = "Clip (UI Effect)";


function EntrevistaPresencial() {
    $('#lblURL').hide();
    $('#lblPiso').show();
}

function EntrevistaRemota() {
    $('#lblURL').show();
    $('#lblPiso').hide();
}