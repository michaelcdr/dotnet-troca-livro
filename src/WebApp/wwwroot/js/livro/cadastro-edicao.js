$(function () {
    $('#Imagens').change(function () {
        let input = this;
        if (input.files) {
            for (var i = 0; i < input.files.length; i++) {
                let reader = new FileReader();
                reader.onload = function (e) {
                    let imagemId = createGuid();
                    let cardHtml = obterCardUpload(imagemId, e.target.result);

                    $('#imagens-container').append(cardHtml);

                    $('.btn-remover-img').unbind('click');
                    $('.btn-remover-img').click(function () {
                        let id = $(this).data('imagemId');
                        let index = obterIndiceImagem(id);
                        removerArquivoDoFileList(index, '#Imagens');
                        $('.card[data-imagem-id="' + id + '"]').remove();
                    });
                }

                reader.readAsDataURL(input.files[i]);
            }
        }
    });

    $("#CategoriaId").change(function () {
        if ($(this).val() !== "") {
            let categoriaId = parseInt($(this).val());
            $.getJSON("/Livro/ObterSubCategorias/", { categoriaId: categoriaId }, function (subCategorias) {
                if (subCategorias) {
                    let options = ['<option value="">Selecione</option>'];

                    subCategorias.forEach(subCategoria => {
                        options.push(`<option value="${subCategoria.id}">${subCategoria.nome}</option>`);
                    });

                    $("#SubCategoriaId").html(options.join(''));
                    $("#SubCategoriaId").prop("disabled", false)
                }

            }).fail(function () {

            });
        }
    });

    $('.btn-remover-img-atual').unbind('click');
    $('.btn-remover-img-atual').click(function () {
        let imagemId = $(this).data('imagemId');
        $(".img-atual[data-imagem-id='" + imagemId + "']").remove();
        $("#ImagensAtuaisId").val(obterImagensAtuais());

        setTimeout(function () {
            if ($(".img-atual").length === 0) {
                $("#imgs-atuais-container").html('<div class="col-md-12"><div class="alert alert-info">Nenhuma imagem disponível.</div></div>')
            }
        },100)
    });
});

function obterImagensAtuais() {
    var ids = [];
    $(".img-atual").each(function () { ids.push($(this).data('imagemId')) });
    
    return ids.join(",");
}