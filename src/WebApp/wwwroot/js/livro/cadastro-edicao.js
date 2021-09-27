$(function () {
    $('#Imagens').change(function () {
        let input = this;
        if (input.files) {
            for (var i = 0; i < input.files.length; i++) {
                let reader = new FileReader();
                reader.onload = function (e) {
                    let imagemId = createGuid();
                    let cardHtml = `<div class="col-md-2 d-flex m-3 align-items-stretch  card" data-imagem-id="${imagemId}" >
                            <div class="img-livro-container text-center pt-2 pb-2">
                                <img src="${e.target.result}" class='img-livro' style='max-width:100px; max-height: 100px' />
                            </div>
                            <div class="text-center pb-2">
                                <button class="btn btn-danger btn-sm btn-remover-img" data-imagem-id="${imagemId}" type="button">Remover</button>
                            </div>
                        </div>`;

                    $('#imagens-container').append(cardHtml);

                    $('.btn-remover-img').unbind('click');
                    $('.btn-remover-img').click(function () {
                        let id = $(this).data('imagemId');
                        let index = obterIndiceImagem(id);
                        removerArquivoDoFileList(index);
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

function obterIndiceImagem(imagemId) {
    let indice = -1;
    $(".card").each(function (index, elemento) {
        if ($(elemento).data('imagemId') === imagemId) {
            indice = index;
            return;
        }
    });
    return indice;
}

function removerArquivoDoFileList(indiceDoArquivo) {
    const dt = new DataTransfer();
    const input = document.querySelector('#Imagens');
    const { files } = input

    for (let i = 0; i < files.length; i++) {
        const file = files[i]
        if (indiceDoArquivo !== i)
            dt.items.add(file);
    }

    input.files = dt.files;
}

function createGuid() {
    let S4 = () => Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1);
    let guid = `${S4()}${S4()}-${S4()}-${S4()}-${S4()}-${S4()}${S4()}${S4()}`;

    return guid.toLowerCase();
}