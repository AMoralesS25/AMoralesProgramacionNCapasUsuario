$(document).ready(function () {
    GetAllTabla();
});


function GetAllTabla() {
    //    var usuarioModel = {
    //        Nombre: "",
    //        ApellidoPaterno: "",
    //        ApellidoMaterno: "",
    //        Rol: {
    //            IdRol: 0
    //        }
    //    };

    //    var parametros = $.param(usuarioModel);

    $.ajax({
        /*url: "/UsuarioJS/GetAll?" + parametros,*/
        url: GetAllURL,
        type: "GET",
        dataType: "json",
        success: function (result) {
            if (result.Correct) {
                console.log("Entro al ajax");
                var usuarios = result.Objects;
                var bodytabla = $('#tbody');
                bodytabla.empty();
                var registro = ``;

                $.each(usuarios, function (i, usuario) {
                    let direccionCompleta = usuario.Direccion.Calle == null ? " " :
                        `Calle ${usuario.Direccion.Calle}, ` +
                        `Numero interior ${usuario.Direccion.NumeroInterior}, ` +
                        `Numero exterior ${usuario.Direccion.NumeroExterior}, ` +
                        `Colonia ${usuario.Direccion.Colonia.Nombre}, ` +
                        `CP. ${usuario.Direccion.Colonia.CodigoPostal}, ` +
                        `${usuario.Direccion.Colonia.Municipio.Nombre}, ` +
                        `${usuario.Direccion.Colonia.Municipio.Estado.Nombre}`;

                    let imagen = usuario.ImagenBase64 ? ` <img src="data:image/*;base64, ${usuario.ImagenBase64}"width="90" , height="90" />`
                        : `<img id="img" src="https://cdn-icons-png.flaticon.com/512/6522/6522581.png" width="90" , height="90" />`;


                    registro += ` <tr>
                                    <td>
                                        <a class="btn btn-warning" onclick="UsuarioGetById(${usuario.IdUsuario})">
                                            <i class="bi bi-pencil-square"></i>
                                        </a>
                                    </td>
                                    <td>${imagen}</td>
                                    <td>${usuario.UserName}</td>
                                    <td>${usuario.Nombre}</td>
                                    <td>${usuario.ApellidoPaterno}</td>
                                    <td>${usuario.ApellidoMaterno}</td>
                                    <td>${usuario.Email}</td>
                                    <td>${usuario.Password}</td>
                                    <td>${usuario.FechaNacimiento}</td>
                                    <td>${usuario.Sexo}</td>
                                    <td>${usuario.Telefono}</td>
                                    <td>${usuario.Celular}</td>
                                    <td>${usuario.Estatus}</td >
                                    <td>${usuario.CURP}</td>
                                    <td>${usuario.Rol.Nombre}</td>
                                    <td>${direccionCompleta}</td>
                                    <td>
                                        <a 
                                           id="btnDelete", class="btn btn-danger", onclick="Delete(${usuario.IdUsuario})">
                                            <i class="bi bi-trash3-fill"></i>
                                        </a>
                                    </td >
                                </tr >`  ;


                });
                bodytabla.append(registro);

            } else {
                var error = $('#error');

                let mensajeError = `<div class="alert alert-danger" role="alert">
                    Hubo un error <br />
                    ${result.ErrorMessage}
                </div>`

                error.append(mensajeError);

            }
        },
        error: function (xhr, status, error) {
            console.log("Status: " + status);
            console.log("Error: " + error);

        }
    });
}

function Form() {
    CleanModal();
    DDLRol();
    DDLEstado();
    ShowModal();
}

function CleanModal() {
    $('#txtIdUsuario').val("");
    $('#Nombre').val("");
    $('#UserName').val("");
    $('#ApellidoPaterno').val("");
    $('#ApellidoMaterno').val("");
    $('#Email').val("");
    $('#Password').val("");
    $('#datepicker').val("").toString();
    $('input[name="Sexo"]:checked').val("");
    $('#Telefono').val("");
    $('#Celular').val("");
    $('#CURP').val("");
    $('#ddlRol').val("");
    $('#Calle').val("");
    $('#NumeroExterior').val("");
    $('#NumeroInterior').val("");
    $('#ddlColonia').val("");
    $('#ddlMunicipio').val("");
    $('#ddlEstado').val("");
}

function ShowModal() {
    $('#formulario').modal("show");
}

function CloseModal() {
    $('#formulario').modal("hide");
}

function BtnGuardar() {
    var inpIdUsuario = $('#txtIdUsuario').val();
    var inptNombre = $('#Nombre').val();
    var inptUserName = $('#UserName').val();
    var inptApellidoPaterno = $('#ApellidoPaterno').val();
    var inptApellidoMaterno = $('#ApellidoMaterno').val();
    var inptEmail = $('#Email').val();
    var inptPassword = $('#Password').val();
    var inptFecha = $('#datepicker').val().toString();
    var sexoSeleccionado = $('input[name="Sexo"]:checked').val();
    var inptTelefono = $('#Telefono').val();
    var inptCelular = $('#Celular').val();
    var inptCURP = $('#CURP').val();
    var inptRol = $('#ddlRol').val();
    var inptCalle = $('#Calle').val();
    var inptNumeroExterior = $('#NumeroExterior').val();
    var inptNumeroInterior = $('#NumeroInterior').val();
    var inptColonia = $('#ddlColonia').val();
    var inptMunicpio = $('#ddlMunicipio').val();
    var inptEstado = $('#ddlEstado').val();


    if (inpIdUsuario == "") {
        inpIdUsuario = 0;
        var json = {
            UserName: inptUserName,
            Nombre: inptNombre,
            ApellidoPaterno: inptApellidoPaterno,
            ApellidoMaterno: inptApellidoMaterno,
            Email: inptEmail,
            Password: inptPassword,
            FechaNacimiento: inptFecha,
            Sexo: sexoSeleccionado,
            Telefono: inptTelefono,
            Celular: inptCelular,
            CURP: inptCURP,
            Rol: {
                IdRol: inptRol,
            },
            Direccion: {
                Calle: inptCalle,
                NumeroExterior: inptNumeroExterior,
                NumeroInterior: inptNumeroInterior,
                Colonia: {
                    IdColonia: inptColonia,
                    Municipio: {
                        IdMunicpio: inptMunicpio,
                        Estado: {
                            IdEstado: inptEstado
                        }
                    }
                }
            }

        };
        /* console.log(json)*/
        Add(json);
    }
    else {
        var json = {
            IdUsuario: inpIdUsuario,
            UserName: inptUserName,
            Nombre: inptNombre,
            ApellidoPaterno: inptApellidoPaterno,
            ApellidoMaterno: inptApellidoMaterno,
            Email: inptEmail,
            Password: inptPassword,
            FechaNacimiento: inptFecha,
            Sexo: sexoSeleccionado,
            Telefono: inptTelefono,
            Celular: inptCelular,
            CURP: inptCURP,
            Rol: {
                IdRol: inptRol,
            },
            Direccion: {
                Calle: inptCalle,
                NumeroExterior: inptNumeroExterior,
                NumeroInterior: inptNumeroInterior,
                Colonia: {
                    IdColonia: inptColonia,
                    Municipio: {
                        IdMunicpio: inptMunicpio,
                        Estado: {
                            IdEstado: inptEstado
                        }
                    }
                }
            }

        };
        Update(json);
    }

}

function Add(json) {
    console.log(json)
    $.ajax({
        url: AddURL,
        type: "POST",
        data: JSON.stringify(json),
        contentType: "application/json",
        dataType: "json",
        success: function (result) {
            if (result.Correct) {
                console.log("Entro al ajax add");
                CloseModal();
                GetAllTabla();

            } else {
                console.log("Hubo un error al insertar");
            }
        },
        error: function (xhr, status, error) {
            console.log("Status: " + status);
            console.log("Error: " + error);

        }
    });
}

function Update(json) {
    console.log(json)
    $.ajax({
        url: UpdateURL,
        type: "POST",
        data: JSON.stringify(json),
        contentType: "application/json",
        dataType: "json",
        success: function (result) {
            if (result.Correct) {
                console.log("Actualizo de manera correcta");

                GetAllTabla();
                CloseModal();
            } else {
                console.log("Hubo un error al actualizar");
            }
        },
        error: function (xhr, status, error) {
            console.log("Status: " + status);
            console.log("Error: " + error);

        }
    });
}
function UsuarioGetById(IdUsuario) {
    $.ajax({
        url: UsuarioGetByIdURL + IdUsuario,
        type: "GET",
        dataType: "json",
        success: function (result) {
            if (result.Correct) {
                console.log("Entro al ajax get by id");
                var usuario = result.Object;
                console.log(usuario)
                CleanModal();
                DDLRol();
                DDLEstado();
                $('#txtIdUsuario').val(usuario.IdUsuario);
                $('#Nombre').val(usuario.Nombre);
                $('#UserName').val(usuario.UserName);
                $('#ApellidoPaterno').val(usuario.ApellidoPaterno);
                $('#ApellidoMaterno').val(usuario.ApellidoMaterno);
                $('#Email').val(usuario.Email);
                $('#Password').val(usuario.Password);
                $('#datepicker').val(usuario.FechaNacimiento).toString();
                $('input[name="Sexo"]:checked').val(usuario.Sexo);
                $('#Telefono').val(usuario.Telefono);
                $('#Celular').val(usuario.Celular);
                $('#CURP').val(usuario.CURP);
                $('#ddlRol').val(usuario.Rol.IdRol).trigger('change');

                // Si quieres ver el texto del elemento seleccionado:
                console.log($("#ddlRol :selected").text()); 
                $('#Calle').val(usuario.Direccion.Calle);
                $('#NumeroExterior').val(usuario.Direccion.NumeroExterior);
                $('#NumeroInterior').val(usuario.Direccion.NumeroInterior);
                $('#ddlColonia').val(usuario.Direccion.Colonia.IdColonia);
                $('#ddlMunicipio').val(usuario.Direccion.Colonia.Municipio.IdMunicpio);
                $('#ddlEstado').val(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                var test = $('#ddlEstado');
                console.log(test);
                ShowModal();
            } else {
                alert("No hay datos de usuario");
            }
        },
        error: function (xhr, status, error) {
            console.log("Status: " + status);
            console.log("Error: " + error);

        }
    });
}

function UsuarioDelete(IdUsuario) {
    $.ajax({
        url: DeleteURL + IdUsuario,
        type: "POST",
        dataType: "json",
        success: function (result) {
            if (result.Correct) {
                console.log("Entro al ajax y elimino");
                GetAllTabla();

            } else {
                alert("No se pudo eliminar el usuario");
            }
        },
        error: function (xhr, status, error) {
            console.log("Status: " + status);
            console.log("Error: " + error);

        }
    });
}

function DDLRol() {
    $.ajax({
        url: GetAllRolURL,
        type: "GET",
        dataType: "json",
        success: function (result) {
            if (result.Correct) {
                console.log("Entro al ajax");
                var roles = result.Objects;
                var optionsRol = $('#ddlRol');
                optionsRol.empty();
                var optionDefault = `<option selected>Selecciona un Rol</option>`;
                optionsRol.append(optionDefault);
                var option = ``;

                $.each(roles, function (i, rol) {
                    option += `<option value="${rol.IdRol}">${rol.Nombre}</option>`;


                });
                optionsRol.append(option);

            } else {
                console.log("No hay Roles en la base de datos");
            }
        },
        error: function (xhr, status, error) {
            console.log("Status: " + status);
            console.log("Error: " + error);

        }
    });
}

function DDLEstado() {
    $.ajax({
        url: GetAllEstadoURL,
        type: "GET",
        dataType: "json",
        success: function (result) {
            if (result.Correct) {
                console.log("Entro al ajax");
                var estados = result.Objects;
                var optionsEstado = $('#ddlEstado');
                optionsEstado.empty();
                var optionDefault = `<option selected>Selecciona un Estado</option>`;
                optionsEstado.append(optionDefault);
                var option = ``;

                $.each(estados, function (i, estado) {
                    option += `<option value="${estado.IdEstado}">${estado.Nombre}</option>`;

                });
                optionsEstado.append(option);

            } else {
                console.log("No hay Estados en la base de datos");
            }
        },
        error: function (xhr, status, error) {
            console.log("Status: " + status);
            console.log("Error: " + error);

        }
    });
}

function Delete(IdUsuario) {
    var confirmacion = confirm('¿Estás seguro de eliminar el usuario?');

    if (confirmacion) {
        UsuarioDelete(IdUsuario);
    }
}