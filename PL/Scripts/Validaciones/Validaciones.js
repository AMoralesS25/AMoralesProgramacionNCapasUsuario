console.log("Validaciones")
// Función para validar todo el formulario
function validarFormulario(event) {
    event.preventDefault();  // Prevenir el envío del formulario por defecto

    var inputs = document.querySelectorAll('input, select, textarea');
    var esValido = true;

    // Recorrer todos los campos del formulario
    inputs.forEach(function (input) {
        // Ignorar los campos con id="idUsuario", name="Imagen" y name="Sexo"
        if (input.id !== "IdUsuario" && input.name !== "Imagen" && input.name !== "Sexo" && input.type !== "file") {
            // Verificar el borde de cada campo
            if (input.style.borderColor !== "green") {
                esValido = false;
            }
        }
    });

    // Si algún campo tiene borde rojo (o no es verde), no se envía el formulario
    if (!esValido) {
        alert("Por favor, llena y corrige los errores en los campos.");
    } else {
        // Si todo es válido, se envía el formulario
        document.getElementById("FormUsuario").submit();  // Enviar el formulario manualmente
    }
}




//validar campos de direccion
function ValidarDireccion(input) {
    var direccion = input.value;
    var regexDireccion = /^[A-Za-z0-9\s]{1,50}$/;

    var errorMessage = input.parentNode.querySelector('.error');

    // Limpiar mensaje de error
    errorMessage.textContent = '';

    // Validar si direccion esta bienn
    if (regexDireccion.test(direccion)) {
        // cambiar el borde a verde
        input.style.borderColor = "green";
        errorMessage.style.display = "none";
    } else {
        // Si no es válido, cambiar el borde a rojo y mostrar el span
        input.style.borderColor = "red";
        errorMessage.style.display = "inline";
        errorMessage.textContent = 'No es valido';
    }
}

//validar que los ddl no esten vacios
function ValidarDDL(input) {
    var ddl = input.value;

    var errorMessage = input.parentNode.querySelector('.error');

    // Limpiar mensaje de error
    errorMessage.textContent = '';

    // Validar si el ddl esta bienn
    if (ddl != null && ddl != " ") {
        // cambiar el borde a verde
        input.style.borderColor = "green";
        errorMessage.style.display = "none";
    } else {
        // Si no es válido, cambiar el borde a rojo y mostrar el span
        input.style.borderColor = "red";
        errorMessage.style.display = "inline";
        errorMessage.textContent = 'La fecha no es valida';
    }

}

//Validar fecha
function validarFecha(input) {
    var fecha = input.value;

    // Expcxresión regular
    var regexFecha = /\d{1,2}\/\d{1,2}\/\d{2,4}/;

    var errorMessage = input.parentNode.querySelector('.error');

    // Limpiar mensaje de error
    errorMessage.textContent = '';

    // Validar si fecha esta bienn
    if (regexFecha.test(fecha)) {
        // cambiar el borde a verde
        input.style.borderColor = "green";
        errorMessage.style.display = "none";
    } else {
        // Si no es válido, cambiar el borde a rojo y mostrar el span
        input.style.borderColor = "red";
        errorMessage.style.display = "inline";
        errorMessage.textContent = 'La fecha no es valida';
    }

}

//validar contraseña
function validarPassword(input) {
    var password = input.value;

    // Expcxresión regular
    var regexPassword = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@@$!%*?&])([A-Za-z\d$@@$!%*?&]|[^ ]){8,50}$/;

    var errorMessage = input.parentNode.querySelector('.error');

    // Limpiar mensaje de error
    errorMessage.textContent = '';

    // Validar si el PASSWORD esta bienn
    if (regexPassword.test(password)) {
        // cambiar el borde a verde
        input.style.borderColor = "green";
        errorMessage.style.display = "none";
    } else {
        // Si no es válido, cambiar el borde a rojo y mostrar el span
        input.style.borderColor = "red";
        errorMessage.style.display = "inline";
        errorMessage.textContent = 'La contraseña no es valida';
    }

}

//confirmar contraseña
function ConfirmarPassword(input) {
    var passwordValor = $('#inptPassword').val();
    var confirmarPasswordValor = $('#inptConfirmarPassword').val();

    var errorMessage = input.parentNode.querySelector('.error');

    // Limpiar mensaje de error
    errorMessage.textContent = '';

    if ((passwordValor == confirmarPasswordValor) && (confirmarPasswordValor != "")) {
        // cambiar el borde a verde
        input.style.borderColor = "green";
        errorMessage.style.display = "none";
    }
    else {
        input.style.borderColor = "red";
        errorMessage.style.display = "inline";
        errorMessage.textContent = 'La contraseña no es igual';
    }

}

//validar correo electronico
function validarEmail(input) {
    var email = input.value;

    // Expcxresión regular
    var regexEmail = /^[\w+._-]{4,254}@[a-zA-Z0-9.-]{2,254}\.[a-zA-Z]{2,10}$/;

    var errorMessage = input.parentNode.querySelector('.error');

    // Limpiar mensaje de error
    errorMessage.textContent = '';

    // Validar si el correo esta bienn
    if (regexEmail.test(email)) {
        // cambiar el borde a verde
        input.style.borderColor = "green";
        errorMessage.style.display = "none";
    } else {
        // Si no es válido, cambiar el borde a rojo y mostrar el span
        input.style.borderColor = "red";
        errorMessage.style.display = "inline";
        errorMessage.textContent = 'El correo electrónico es incorrecto.';
    }
}


//validar curp
function validarCurp(input) {
    var curp = input.value.toUpperCase();

    // Expresión regular para validar CURP
    var regexCurp = /^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$/;

    // Buscar el span de error dentro del contenedor del input
    var errorMessage = input.parentNode.querySelector('.error');

    // Limpiar mensaje de error previo
    errorMessage.textContent = '';

    // Validar si el CURP cumple con el formato
    if (regexCurp.test(curp)) {
        // Si es válid se cambia el borde a verde
        input.style.borderColor = "green";
        errorMessage.style.display = "none"; // Ocultar
    } else {
        // Si no es válido se cambia el borde a rojo
        input.style.borderColor = "red";
        errorMessage.style.display = "inline"; // Mostrar mensaje de error
        errorMessage.textContent = 'El CURP es erróneo.'; // Mostrar mensaje de error
    }
}

//VALIDACION PARA NOMBRE, APELLIDOS
function SoloLetras(evento) {
    var entrada = String.fromCharCode(evento.which)
    var inputField = evento.target;
    var ErrorMessage = inputField.parentNode.querySelector('.error')
    ErrorMessage.textContent = '';
    if (!(/[a-z A-Z]/.test(entrada))) {
        evento.preventDefault();
        inputField.style.borderColor = 'red';
        ErrorMessage.textContent = 'Solo se permiten letras';
    }
    else {
        console.log("si es una letra")
        inputField.style.borderColor = 'green';
    }
}

//VALIDACION PARA TELEFONO SOLO NUMEROS
function SoloNumeros(evento) {
    var entrada = String.fromCharCode(evento.which)
    var inputField = evento.target;
    var ErrorMessage = inputField.parentNode.querySelector('.error')
    ErrorMessage.textContent = '';
    if (!(/^[0-9]*$/.test(entrada))) {
        evento.preventDefault();
        inputField.style.borderColor = 'red';
        ErrorMessage.textContent = 'Solo se permiten numeros';
    }
    else {
        console.log("si es un numero")
        inputField.style.borderColor = 'green';
    }
}

//validar campo de user name
function ValidarUserName(input) {
    var userName = input.value;
    var regexUserName = /^[A-Za-z0-9]{5,50}$/;

    var errorMessage = input.parentNode.querySelector('.error');

    // Limpiar mensaje de error
    errorMessage.textContent = '';

    // Validar si el nombre de usuario esta bienn
    if (regexUserName.test(userName)) {
        // cambiar el borde a verde
        input.style.borderColor = "green";
        errorMessage.style.display = "none";
    } else {
        // Si no es válido, cambiar el borde a rojo y mostrar el span
        input.style.borderColor = "red";
        errorMessage.style.display = "inline";
        errorMessage.textContent = 'No es valido el nombre de usuario';
    }
}


$("#datepicker").datepicker();
dateFormat = "dd-mm-yy";
showAnim = "Clip (UI Effect)";

function MunicipioGetByIdEstado() {
    let ddl = $('#ddlEstado').val();

    $.ajax({
        url: ddlMunicipioURL + ddl,
        type: "GET",
        datatype: "JSON",
        //data es solo para modelos
        success: function (result) {
            let ddlMunicipio = $('#ddlMunicipio');
            $(ddlMunicipio).empty();
            let ddlColonia = $('#ddlColonia');
            $(ddlColonia).empty();
            let optionDefault = `<option value>Selecciona un Municipio</option>`
            ddlMunicipio.append(optionDefault)

            $.each(result.Objects, function (i, valor) {
                let option = `<option value=${valor.IdMunicipio}>${valor.Nombre}</option>`
                //let option = "<option value=" + valor.IdMunicipio + ">" + valor.Nombre + "</option>";
                ddlMunicipio.append(option)
            })
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}

function ColoniaGetByIdMunicipio() {
    let ddl = $('#ddlMunicipio').val();

    $.ajax({
        url: ddlColoniaURL + ddl,
        type: "GET",
        datatype: "JSON",
        //data es solo para modelos
        success: function (result) {
            let ddlColonia = $('#ddlColonia');
            $(ddlColonia).empty();
            let optionDefault = `<option value>Selecciona una Colonia</option>`
            ddlColonia.append(optionDefault)
            $.each(result.Objects, function (i, valor) {
                let option = `<option value=${valor.IdColonia}>${valor.Nombre}</option>`
                //let option = "<option value=" + valor.IdColonia + ">" + valor.Nombre + "</option>";
                ddlColonia.append(option)
            })
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}

function validarImagen() {
    var input = $('#inptFileImagen')[0].files[0].name.split('.').pop().toLowerCase()
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

function visualizarImagen(input) {
    if (input.files) {
        var reader = new FileReader();
        reader.onload = function (elemento) {
            $('#img').attr('src', elemento.target.result)
        }
        reader.readAsDataURL(input.files[0])
    }
}
