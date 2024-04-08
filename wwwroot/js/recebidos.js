function validar(id) {
    var cghoraria = $('#cargahoraria' + id).val();
    console.log(cghoraria, id)
    if (cghoraria != "" || cghoraria != null) {
        $.ajax({
            url: '/Adm/Validar',
            method: 'POST',
            data: { idcomprovante: id, novaCargaHoraria: cghoraria },
            dataType: 'json',
            success: function (data) {
                $('#form' + id).html("<span style='color: green'><strong>Validado com sucesso! <br />Carga Horária: " + cghoraria + "m</strong></span>");
            },
            error: function (xhr, status, error) {
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