function validarFoto() {
    var input = $('#inptFileFoto')[0].files[0].name.split('.').pop().toLowerCase()
    console.log(input)

    var extensionesValidas = ['png', 'jpg', 'jpeg', 'webp']
    var banderaImg = false;

    for (var i = 0; i <= extensionesValidas.length; i++) {
        if (input == extensionesValidas[i]) {
            banderaImg = true
        }
    }
    if (!banderaImg) {
        alert(`Las extensiones permitidas son: ${extensionesValidas}`)
        $('#inptFileImagen').val("");
    }
}


function visualizarFoto(input) {
    if (input.files) {
        var reader = new FileReader();
        reader.onload = function (elemento) {
            $('#foto').attr('src', elemento.target.result)
        }
        reader.readAsDataURL(input.files[0])
    }
}

function validarCurriculum() {
    var input = $('#inptFileCurriculum')[0].files[0].name.split('.').pop().toLowerCase()
    console.log(input)

    var extensionValida = ['pdf']
    var banderaImg = false;


    if (input == extensionValida) {
        banderaImg = true
    }

    if (!banderaImg) {
        alert(`Solo se permite PDF`)
        $('#inptFileCurriculum').val("");
    }
}