$(document).ready(() => {
    GetAll();
    console.log("x")
})

var GetAll = () => {

    $.ajax({
        url: urlGetAllJs,
        type: "GET",
        dataType: "JSON",
        success: result => {
            if (result.Correct) {

                let listaUsuarios = result.Objects;
                let fila = $('#tbody')
                for (var usuario of listaUsuarios) {



                    let fila = `<tr class="body--row">
                                <td class="editar">
                                    <a href="#" id="${usuario.IdUsuario}" onclick="update(this.id)" class="btnUsuario btn btn-warning"><i class="bi bi-pencil"></i></a>
                                </td>
                                <td>
                                    ${cambiarStatus(usuario.Estatus)}
                                </td>
                                <td>
                                    ${cambiarImagen(usuario.ImagenBase64)}
                                </td>
                                <td>${usuario.UserName}</td>
                                <td>${usuario.Nombre} ${usuario.ApellidoPaterno} ${usuario.ApellidoMaterno}</td>
                                <td>${usuario.Direccion.Calle}, ${usuario.Direccion.Colonia.CodigoPostal}, ${usuario.Direccion.NumeroExterior}, ${usuario.Direccion.NumeroInterior}</td>
                                <td>${usuario.Email}</td>
                                <td>${usuario.Password}</td>
                                <td>${usuario.FechaNacimiento}</td>
                                <td>${usuario.Sexo}</td>
                                <td>${usuario.Telefono}</td>
                                <td>${usuario.Celular}</td>
                                <td>${usuario.CURP}</td>
                                <td>${usuario.Rol.Nombre}</td>
                                <td class="eliminar">
                                    <a href="${urlDelete}=${usuario.IdUsuario}" class="btn btn-danger" onclick="return confirm('¿Seguro que quieres eliminarlo?')"><i class="bi bi-trash3-fill"></i></a>
                                </td>
                            </tr>`


                    document.getElementById('tbody').innerHTML += fila
                }



            }
        },
        error: xhr => {
            console.log(xhr)
        }
    })

}

var GetAllRol = () => {

    $.ajax({
        url: urlDDLRol,
        type: "GET",
        dataType: "JSON",
        success: result => {
            if (result.Correct) {
                //Buscar el ddl donde pinta el valor
                let ddlRol = $('#ddlRol');
                ddlRol.empty()

                let defaultSelect = `<option value="">Seleccione un Rol</option>`
                ddlMunicipio.append(defaultSelect)

                $.each(result.Objects, (i, valor) => {
                    let option = `<option value="${valor.IdRol}">${valor.Nombre}</option>`;
                    ddlRol.append(option)

                })

            }
        },
        error: xhr => {
            console.log(xhr)
        }
    })
}


var GetAllEstado = () => {

    $.ajax({
        url: urlDDLEstado,
        type: "GET",
        dataType: "JSON",
        success: result => {
            if (result.Correct) {
                //Buscar el ddl donde pinta el valor
                let ddlEstado = $('#ddlEstado');
                ddlEstado.empty();

                let defaultSelect = `<option value="">Seleccione un Estado</option>`
                ddlMunicipio.append(defaultSelect)

                $.each(result.Objects, (i, valor) => {
                    let option = `<option value="${valor.IdEstado}">${valor.Nombre}</option>`;
                    ddlEstado.append(option)

                })

            }
        },
        error: xhr => {
            console.log(xhr)
        }
    })
}

var MunicipioGetByIdEstado = () => {
    //console.log(e)
    let ddl = ddlURLMunicipio + $('#ddlEstado').val()
    console.log(ddl)
    $.ajax({
        url: ddl,
        type: "GET",
        dataType: "JSON",
        success: result => {
            if (result.Correct) {
                //Buscar el ddl donde pinta el valor
                let ddlMunicipio = $('#ddlMunicipio');
                ddlMunicipio.empty();

                let defaultSelect = `<option value="">Seleccione un Municipio</option>`
                ddlMunicipio.append(defaultSelect)

                $.each(result.Objects, (i, valor) => {
                    let option = `<option value="${valor.IdMunicipio}">${valor.Nombre}</option>`;
                    ddlMunicipio.append(option)

                })

            }
        },
        error: xhr => {
            console.log(xhr)
        }
    })
}

var ColoniaGetByIdMunicipio = () => {
    let ddl = ddlURLColonia + $('#ddlMunicipio').va()

    console.log(ddl)
    $.ajax({
        url: ddl,
        type: "GET",
        dataType: "JSON",
        success: result => {
            if (result.Correct) {
                //Buscar el ddl donde pinta el valor
                let ddlColonia = $('#ddlColonia');
                ddlColonia.empty();

                let defaultSelect = `<option value="">Seleccione una Colonia</option>`
                ddlColonia.append(defaultSelect)

                $.each(result.Objects, (i, valor) => {
                    let option = `<option value="${valor.IdColonia}">${valor.Nombre}</option>`;
                    ddlColonia.append(option)
                })

            }
        },
        error: xhr => {
            console.log(xhr)
        }
    })
}


//$('#Formulario').on('submit', function (event) {
//    event.preventDefault();
//    console.log(urlUsuarioAdd)
//    var formData = $(this).serialize();

//    $.ajax({
//        url: urlUsuarioAdd,
//        type: 'POST',
//        data: formData,
//        success: function (response) {
//            console.log('Respuesta del servidor:', response);
//            alert(response.message);
//        },
//        error: function (xhr, status, error) {
//            console.error('Error al enviar los datos:', error);
//            alert('Hubo un error al enviar los datos');
//        }
//    });
//});


var cambiarStatus = (status) => {
    if (status) {
        return `<div class="form-check form-switch">
            <input class="form-check-input" type="checkbox" id="flexSwitchCheckDefault" checked onchange="CambiarStatus(@usuario.IdUsuario, this)">
        </div>`
    }
    else {
        return `<div class="form-check form-switch">
            <input class="form-check-input" type="checkbox" id="flexSwitchCheckDefault" onchange="CambiarStatus(@usuario.IdUsuario, this)">
        </div>`
    }
}

var cambiarImagen = (imagen) => {
    if (imagen != "") {
        return `<div class="contenedor--img">
            <img class="img" src="data:image/*;base64, ${imagen}">
        </div>`;
    }
    else {
        return `<div class="contenedor--img">
            <img id="img" class="img" src="https://static.vecteezy.com/system/resources/previews/024/983/914/non_2x/simple-user-default-icon-free-png.png" alt="Img Usuario">
        </div>`
    }
}



$('#btnModal').click(() => {
    limpiarModal()
    GetAllRol()
    GetAllEstado()
    $('#staticBackdrop').modal('show')
})


var cambioMunicipio = (e) => {
    MunicipioGetByIdEstado(e);
}

var cambiarColonia = (e) => {
    ColoniaGetByIdMunicipio(e)
}

//$('#ddlMunicipio').change(() => {
//    let valor = $('#ddlMunicipio').val()
//    console.log(valor)
//})

var limpiarModal = () => {
    let inputs = $('input').toArray()

    for (let input of inputs) {
        input.value = ''
    }
}

//let update = () => {

//    let usuarios = $('.btnUsuario').toArray()
//    console.log(usuarios)
//    for (let usuario of usuarios) {
//        usuario.click(() => {

//            $('#staticBackdrop').modal('show')
//        });
//    }
//}

var update = (id) => {
    let urlGetById = urlGetByIdUsuario + id
    limpiarModal()
    GetAllRol()
    GetAllEstado()
    $.ajax({
        url: urlGetById,
        type: "GET",
        dataType: "JSON",
        success: result => {
            if (result.Correct) {
                //Buscar el ddl donde pinta el valor
                //let ddlColonia = $('#ddlColonia');
                //ddlColonia.empty();

                //let defaultSelect = `<option value="">Seleccione una Colonia</option>`
                //ddlColonia.append(defaultSelect)
                var usuario = result.Object

                console.log(usuario)
                //cambiarImagen()
                $('#mostrarImagen').html(cambiarImagen(usuario.ImagenBase64))
                $('#inpUserName').val(usuario.UserName)
                $('#Nombre').val(usuario.Nombre)
                $('#ApellidoPaterno').val(usuario.ApellidoPaterno)
                $('#ApellidoMaterno').val(usuario.ApellidoMaterno)
                $('#Email').val(usuario.Email)
                $('#Password').val(usuario.Password)
                $('#datepicker').val(usuario.datepicker)
                $('#sexo').val(usuario.sexo)
                $('#Telefono').val(usuario.Telefono)
                $('#Celular').val(usuario.Celular)
                $('#CURP').val(usuario.CURP)
                $('#ddlRol').val(usuario.Rol.IdRol)
                $('#Calle').val(usuario.Direccion.Calle)
                $('#NumeroInterior').val(usuario.Direccion.NumeroInterior)
                $('#NumeroExterior').val(usuario.Direccion.NumeroExterior)
                $('#ddlEstado').val(usuario.Direccion.Colonia.Municipio.Estado.IdEstado)
                MunicipioGetByIdEstado()
                $('#ddlMunicipio').val(usuario.Direccion.Colonia.Municipio.IdMunicipio)
                ColoniaGetByIdMunicipio()
                $('#ddlColonia').val(usuario.Direccion.Colonia.IdColonia)


            }
        },
        error: xhr => {
            console.log(xhr)
        }
    })
    $('#staticBackdrop').modal('show')

}

//document.getElementById('inpImagen').addEventListener('change', function (e) {
//    const file = e.target.files[0]; // Obtiene el archivo seleccionado

//    if (!file) return; // Si no hay archivo, termina

//    const reader = new FileReader(); // Crea un lector de archivos

//    // Evento que se dispara cuando el archivo se carga
//    reader.onload = function (event) {
//        const base64String = event.target.result; // Obtiene Base64
//        console.log(base64String); // Muestra en consola (opcional)

//        // Muestra una previsualización (opcional)
//        document.getElementById('preview').src = base64String;
//        document.getElementById('preview').style.display = 'block';

//        // Aquí puedes enviar `base64String` a tu backend con AJAX
//    };

//    reader.readAsDataURL(file); // Convierte el archivo a Base64
//});
