$("#datepicker").datepicker({
    dateFormat: "dd/mm/yy",
    showAnim: "slideDown"
});

$("#datepicker").change(e => {
    inputFecha = $("#datepicker")
    inputBorderColor = e.target;
    if (inputFecha.val() != "") {
        inputBorderColor.style.borderColor = "green"
    } else {
        inputBorderColor.style.borderColor = "red"
    }
})

//function cambiarColor() {
//    let inputCelular = $('#celular')
//    inputCelular.target.style.borderColor = "green"
//}


let MunicipioGetByIdEstado = () => {
    let ddlEstado = ddlURLMunicipio + $('#ddlEstado').val()
    //console.log(ddlEstado)
    $.ajax({
        url: ddlEstado,
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
let ColoniaGetByIdMunicipio = () => {
    let ddl = ddlURLColonia + $('#ddlMunicipio').val()

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

// Crear arreglos con extensiones permitidas
let allowedImages = ['jpeg', 'jpg', 'gif', 'png'];

// Obtener elemento y que solo admita imágenes
let validarImagen = () => {
    //Saber la extension del archivo que me paso
    //Saber la extencion
    //dividir el nombre en 2, a traves del (.)

    let input = $('#inputFileImagen')[0].files[0].name.split('.').pop().toLowerCase()
    console.log(input)

    //Compararla con extensiones de imagen
    let extensionesValidas = ['png', 'jpg', 'jpeg', 'webp']
    let banderaImg = extensionesValidas.includes(input)
    console.log(banderaImg)
    if (!banderaImg) {
        alert(`Las extensiones permitidas son: ${extensionesValidas}`)
        $('#inputFileImagen').val('');
    }

}

let visualizarImagen = (input) => {
    if (input.files) {
        let reader = new FileReader()
        reader.onload = elemento => {
            $('#img').attr('src', elemento.target.result)
        }
        reader.readAsDataURL(input.files[0])
    }
}

//$('.soloLetras').on('keypress', (e) => {
//    let entrada = String.fromCharCode(e.which)
//    let inputFiled = e.target
//    let ErrorMessage = inputFiled.parentNode.querySelector('.error')
//    ErrorMessage.textContent = '';
//    if (!(/[a-z A-Z]/).test(entrada)) {
//        e.preventDefault()
//        inputFiled.style.borderColor = 'red'
//        ErrorMessage.textContent = 'Solo se aceptan letras'
//    } else {
//        console.log("Si es una letra")
//        inputFiled.style.borderColor = "green"
//    }
//})



//$('.soloNumeros').on('keypress', (e) => {
//    let entrada = String.fromCharCode(e.which)
//    let inputFiled = e.target
//    let ErrorMessage = inputFiled.parentNode.querySelector('.error')
//    ErrorMessage.textContent = '';
//    if (!(/^[0-9]*$/).test(entrada)) {
//        e.preventDefault()
//        inputFiled.style.borderColor = 'red'
//        ErrorMessage.textContent = 'Solo se aceptan numeros'
//    } else {
//        console.log("Si es un numero")
//        inputFiled.style.borderColor = "green"
//    }
//})

//$('.validarEmail').on('blur', (e) => {

//    let inputField = e.target;
//    let email = inputField.value.trim();
//    let ErrorMessage = inputField.parentNode.querySelector('.error');
//    console.log(ErrorMessage)
//    ErrorMessage.textContent = '';

//    var EmailRegex = /^[a-zA-Z0-9._%+-]+@@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

//    if (!EmailRegex.test(email)) {
//        inputField.style.borderColor = 'red';
//        ErrorMessage.textContent = 'Correo electronico es invalido';
//    }
//    else {
//        inputField.style.borderColor = 'green';
//    }
//})

//$('#contraseniaUno').on('blur', (e) => {
//    let inputFiled = e.target
//    let entrada = inputFiled.value.trim();
//    let ErrorMessage = inputFiled.parentNode.querySelector('.error')
//    ErrorMessage.textContent = '';
//    if (!(/^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040])(?=.*[A-Z])(?=.*[a-z])\S{8,16}$/).test(entrada)) {
//        inputFiled.style.borderColor = "red"
//        ErrorMessage.textContent = 'la contraseña es incorrecta'
//    } else {
//        e.preventDefault()
//        console.log("la contraseña es correcta")
//        inputFiled.style.borderColor = 'green'
//    }
//})

//$('#contraseniaDos').blur((e) => {
//    let inputFiled = e.target
//    let entrada = inputFiled.value.trim();
//    let ErrorMessage = inputFiled.parentNode.querySelector('.error')
//    ErrorMessage.textContent = '';
//    if (!(/^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040])(?=.*[A-Z])(?=.*[a-z])\S{8,16}$/).test(entrada)) {

//        //ErrorMessage.textContent = 'la contraseña es incorrecta'
//    } else {
//        e.preventDefault()
//        console.log("la contraseña es correcta")

//    }

//    //validar contraseñas cohincidan
//    let contraseniaUno = $('#contraseniaUno').val()
//    let contraseniaDos = $('#contraseniaDos').val()
//    if (contraseniaUno == contraseniaDos) {
//        //console.log("La contraseña es correcta")
//        //ErrorMessage.textContent = 'la contraseña es correcta'
//        inputFiled.style.borderColor = 'green'
//    } else {
//        ErrorMessage.textContent = 'la contraseña no coincide'
//        console.log("La contraseña es incorrecta")
//        inputFiled.style.borderColor = "red"
//    }
//})

//Validar CURP

//function validarInput(input) {
//    var curp = input.value.toUpperCase(),
//        //resultado = $("#resultado"),
//        valido = "no es válida";
//    let inputFiled = input
//    let ErrorMessage = inputFiled.parentNode.querySelector('.error')
//    ErrorMessage.textContent = '';
//    if (curpValida(curp)) {
//        valido = "es válida";
//        inputFiled.style.borderColor = 'green'
//    } else {
//        inputFiled.style.borderColor = "red"
//    }
//}
//function curpValida(curp) {
//     var re = /^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0\d|1[0-2])(?:[0-2]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$/,
//       validado = curp.match(re);

//     if (!validado)  //Coincide con el formato general?
//            return false;

//        //Validar que coincida el dígito verificador
//     function digitoVerificador(curp17) {
//         //Fuente https://consultas.curp.gob.mx/CurpSP/
//         var diccionario = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ",
//             lngSuma = 0.0,
//             lngDigito = 0.0;
//         for (var i = 0; i < 17; i++)
//             lngSuma = lngSuma + diccionario.indexOf(curp17.charAt(i)) * (18 - i);
//         lngDigito = 10 - lngSuma % 10;
//         if (lngDigito == 10)
//            return 0;
//         return lngDigito;
//     }
//     if (validado[2] != digitoVerificador(validado[1]))
//         return false;

//     return true; //Validado
//}

$('.borrarSpam').blur((e) => {

    let inputFiled = e.target
    let ErrorMessage = inputFiled.parentNode.querySelector('.error')
    if (inputFiled.value != "") {
        ErrorMessage.textContent = '';
        inputFiled.style.borderColor = 'green';
    } else {
        inputFiled.style.borderColor = 'red';
    }

})

$('.colorInput').blur(e => {
    let inputFiled = e.target
    if (inputFiled.value != "") {
        inputFiled.style.borderColor = 'green';
    } else {
        inputFiled.style.borderColor = 'red';
        ErrorMessage.textContent = '';
    }
})

//$('#form').submit((e) => {
//    let inputs = $('input').toArray()
//    console.log(inputs)
//    let valido = true;
//    console.log(inputs)

//    let nuevoInput = inputs.slice(3);
//    console.log(nuevoInput)
//    for (let input of nuevoInput) {
//        let estilo = window.getComputedStyle(input)
//        let borderColor = estilo.borderColor
//        if (borderColor != "rgb(0, 128, 0)") {
//            console.log(borderColor)
//            valido = false;
//            console.log("informacion invalida")
//            break;
//        }

//    }

//    if (!valido) {
//        e.preventDefault()
//    }
//})