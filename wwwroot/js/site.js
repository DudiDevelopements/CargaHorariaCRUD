// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(() => {
    $('#recebidos').DataTable(
        {
            order: [[5, 'asc']],
            columnDefs: [
                { targets: 'no-sort', orderable: false } // Desativa a ordenação para colunas com a classe 'no-sort'
            ],
            "language": {
                "lengthMenu": "Mostrando _MENU_ consultas por página",
                "zeroRecords": "Nada foi encontrado",
                "info": "Mostrando página _PAGE_ de _PAGES_",
                "infoEmpty": "Nada foi encontrado",
                "infoFiltered": "(total de _MAX_ consultas)"
            }
        });
})
