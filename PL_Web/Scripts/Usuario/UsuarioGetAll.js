function CambiarStatus(IdUsuario, input) {
    let status = input.checked;
    $.ajax({
        url: cambiarStatus,
        dataType: "json",
        type: "POST",
        data: {
            IdUsuario,
            status
        },
        success: function (result) {
            if (result.Correct) {
                //algo
            } else {
                alert("Hubo un error")
            }
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}

$("#btnOcultar").click(function () {
    let mostrarOcultar = $(".form_ocultar").get(0)
    let estilo = window.getComputedStyle(mostrarOcultar)
    if (estilo.display == "none") {
        $(".form_ocultar").stop(true, true).slideDown("slow");
        $("#btnOcultar").text("Ocultar Formulario")

    } else {
        $(".form_ocultar").stop(true, true).slideUp("slow");
        $("#btnOcultar").text("Mostrar Formulario")
    }
});

$("#btn-excel").click(function () {
    let mostrarOcultar = $("#formExcel").get(0)
    let estilo = window.getComputedStyle(mostrarOcultar)
    if (estilo.display == "none") {
        $("#formExcel").stop(true, true).slideDown("slow");
        $("#btn-excel").text("Ocultar Excel")

    } else {
        $("#formExcel").stop(true, true).slideUp("slow");
        $("#btn-excel").text("Agregar Excel")
    }
});