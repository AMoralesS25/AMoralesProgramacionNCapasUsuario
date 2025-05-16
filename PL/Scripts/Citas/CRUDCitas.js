$(document).ready(function () {
    DDLVacante();
    EligeVacante();
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
            else
            {
                console.log("No hay vacantes en la base de datos");
            }
        },
        error: function (xhr, status, error) {
            console.log("Status: " + status);
            console.log("Error: " + error);

        }
    });
}

function BtnBuscar()
{
    var idVacante = $('#ddlVacante').val();
    console.log(idVacante);
    $.ajax({
        url: GetAllURL + idVacante,
        type: "GET",
        dataType: "json",
        success: function (result) {
            if (result.Correct) {
                var citas = result.Objects;
                var bodytabla = $('#tbody');
                bodytabla.empty();
                var registro = ``;

                $.each(citas, function (i, cita) {
                    let nombreCompleto = `${cita.Candidato.Nombre} ` + ` ${cita.Candidato.ApellidoPaterno} ` +
                        ` ${cita.Candidato.ApellidoMaterno} `;

                    let imagen = cita.Candidato.ImagenBase64 ? ` <img src="data:image/*;base64, ${cita.Candidato.ImagenBase64}"width="90" , height="90" class="rounded-circle border border-secondary"/>`
                        : `<img class="rounded-circle border border-secondary" id="img" src="https://cdn-icons-png.flaticon.com/512/6522/6522581.png" width="90" , height="90" />`;


                    registro += ` <tr>
                                    <td>
                                        <a class="btn btn-warning" onclick="UsuarioGetById(${cita.Candidato.IdCandidato})">
                                            <i class="bi bi-pencil-square"></i>
                                        </a>
                                    </td>
                                    <td>
                                        <a class="btn btn-warning" onclick="UsuarioGetById(${cita.Candidato.IdCandidato})">
                                            <i class="bi bi-pencil-square"></i>
                                        </a>
                                    </td>
                                    <td>${imagen}</td>
                                    <td>${nombreCompleto}</td>
                                    <td>${cita.Candidato.Edad}</td>
                                    <td>${cita.Candidato.Correo}</td>
                                    <td>${cita.Candidato.Telefono}</td>
                                    <td>${cita.Candidato.Vacante.Nombre}</td>
                                </tr >`  ;
                    console.log(registro);
                });

                bodytabla.append(registro);
            }
            else {
                NoHayRegistros();
            }
        },
        error: function (xhr, status, error) {
            console.log("Status: " + status);
            console.log("Error: " + error);
        }
    });
}