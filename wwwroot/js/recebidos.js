function validar(id) {
    let cghoraria = $('#cargahoraria' + id).val();
    console.log(cghoraria, id)
    if (cghoraria !== "" || cghoraria !== undefined) {
        $.ajax({
            url: '/Adm/Validar',
            method: 'POST',
            data: { idcomprovante: id, novaCargaHoraria: cghoraria },
            dataType: 'json',
            success: function (data) {
                $('#form' + id).html("<span style='color: green'><strong>Validado com sucesso! <br />Carga Horária: " + data.newCgHoraria + " horas</strong></span>");
            },
            error: function (xhr, error) {
                console.error(xhr.responseText); // Exibe detalhes do erro no console
                $('#form' + id).html("<span style='color: red'><strong>Erro ao validar! Tente novamente mais tarde</strong></span>");
            }
        });
    } else {
        alert("Por favor insira uma carga horária");
    }
}

function revogar(id) {
    $.ajax({
        url: '/Adm/Revogar',
        method: 'POST',
        data: { idcomprovante: id },
        dataType: 'json',
        success: function () {
            $('#form' + id).html("<span style='color: green'><strong>Revogado com sucesso! Por favor atualize a página.</strong></span>");
        },
        error: function () {
            $('#form' + id).html("<span style='color: #DC143C'><strong>Falha ao revogar. Tente novamente mais tarde</strong></span>");
        }
    });
}