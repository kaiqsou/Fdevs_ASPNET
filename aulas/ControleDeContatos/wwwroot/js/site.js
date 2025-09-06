// J-Query

// Criando table com paginação - Traduzindo
// Table padrão: let table = new DataTable('#table-contatos');

$(document).ready(function () { 
    getDataTable('#table-contatos');
    getDataTable('#table-usuarios');
})

function getDataTable(id)           
{
    $(id).DataTable({
        ordering: true,
        paging: true,
        searching: true,
        language: {
            emptyTable: "Nenhum registro encontrado na tabela",
            info: "Mostrar _START_ até _END_ de _TOTAL_ registros",
            infoEmpty: "Mostrar 0 até 0 de 0 registros",
            infoFiltered: "(Filtrar de _MAX_ total registros)",
            thousands: ".",
            lengthMenu: "Mostrar _MENU_ registros por página",
            loadingRecords: "Carregando...",
            processing: "Processando...",
            zeroRecords: "Nenhum registro encontrado",
            search: "Pesquisar",
            paginate: {
                next: "Próximo",
                previous: "Anterior"
            },
            aria: {
                sortAscending: ": Ordenar colunas de forma ascendente",
                sortDescending: ": Ordenar colunas de forma descendente"
            }
        }
    })
}

// Quando chamar close-alert por click, ativa a função
$('.close-alert').click(function ()
{
    // Chama a classe alert e ativa o método hide
    $('.alert').hide('hide');
});