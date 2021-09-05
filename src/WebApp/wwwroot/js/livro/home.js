$(function () {
    $.get("/Livro/_ListAdicionadosRecentemente", function (data) {
        $("#livros-adicionados-recentemente").html(data);
    })
})