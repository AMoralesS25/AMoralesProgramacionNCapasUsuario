$(document).ready(function () {
    DDLVacante();
    $('#divSeleccionaVacante').show();
    $('#tabla').hide();
    $('#divSinRegistros').hide();
});

function DDLVacante() {
    $.ajax({
        url: DDLVacanteURL,
        type: "GET",
        dataType: "json",
        success: function (result) {
            if (result.Correct) {
                var vacantes = result.Objects;
                var optionsVacante = $('#ddlVacante');
                optionsVacante.empty();
                var optionDefault = `<option selected>Selecciona una Vacante</option>`;
                optionsVacante.append(optionDefault);
                var option = ``;

                $.each(vacantes, function (i, vacante) {
                    option += `<option value="${vacante.IdVacante}">${vacante.Nombre}</option>`;
                });
                optionsVacante.append(option);

            }
            else {
                console.log("No hay vacantes en la base de datos");
            }
        },
        error: function (xhr, status, error) {
            console.log("Status: " + status);
            console.log("Error: " + error);

        }
    });
}

function BtnBuscar() {
    var idVacante = $('#ddlVacante').val();
    console.log(idVacante);
    $.ajax({
        url: GetAllURL + idVacante,
        type: "GET",
        dataType: "json",
        success: function (result) {
            if (result.Correct) {

                $('#divSeleccionaVacante').hide();
                $('#divSinRegistros').hide();

                var citas = result.Objects;
                var bodytabla = $('#tbody');
                bodytabla.empty();
                var registro = ``;

                $.each(citas, function (i, cita) {
                    let nombreCompleto = `${cita.Candidato.Nombre} ` + ` ${cita.Candidato.ApellidoPaterno} ` +
                        ` ${cita.Candidato.ApellidoMaterno} `;

                    let imagen = cita.Candidato.ImagenBase64 ? ` <img src="data:image/*;base64, ${cita.Candidato.ImagenBase64}"width="90" , height="90" class="rounded-circle border border-secondary"/>`
                        : `<img class="rounded-circle border border-secondary" id="img" src="https://cdn-icons-png.flaticon.com/512/6522/6522581.png" width="90" , height="90" />`;

                    if (cita.IdCita == 0) {

                        var btnAddCita = `<a href="@Url.Action("Form", "Candidato", new { IdCandidato = candidato.IdCandidato })" class="btn btn-info"> Agendar
                            <i class="bi bi-calendar-plus-fill"></i>
                        </a>`;

                        var btnDeleteCita = `<span class="alert alert-dark">
                            Sin cita
                            <i class="bi bi-calendar-event-fill"></i>
                        </span>`;

                    }
                    else
                    {

                        var btnAddCita = ` <a class="btn btn-info" onclick="UsuarioGetById(${cita.Candidato.IdCandidato})"> Editar
                            <i class="bi bi-calendar-event-fill"></i>
                        </a>`;

                        var btnDeleteCita = ` <a class="btn btn-danger" onclick="UsuarioGetById(${cita.Candidato.IdCandidato})"> Eliminar
                            <i class="bi bi-calendar-x-fill"></i>
                        </a>`;

                    }

                    registro = ` <tr>
                                    <td>${btnAddCita}</td>
                                    <td>${btnDeleteCita}</td>
                                    <td>${imagen}</td>
                                    <td>${nombreCompleto}</td>
                                    <td>${cita.Candidato.Edad}</td>
                                    <td>${cita.Candidato.Correo}</td>
                                    <td>${cita.Candidato.Telefono}</td>
                                    <td>${cita.Candidato.Vacante.Nombre}</td>
                                </tr >`  ;

                    bodytabla.append(registro);
                });


                $('#tabla').show();

            }
            else {
                $('#divSinRegistros').show();
                $('#tabla').hide();
                $('#divSeleccionaVacante').hide();
            }
        },
        error: function (xhr, status, error) {
            console.log("Status: " + status);
            console.log("Error: " + error);
            $('#divSinRegistros').hide();
            $('#tabla').hide();
            $('#divSeleccionaVacante').show();
        }
    });
}



