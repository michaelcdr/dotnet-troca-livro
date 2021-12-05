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

    $("#btn-adicionar-autor").click(function () {
        cadastrarAutor($(this));
    });
});

function obterImagensAtuais() {
    var ids = [];
    $(".img-atual").each(function () { ids.push($(this).data('imagemId')) });
    
    return ids.join(",");
}

function atualizarOpcoesCampoSelecaoAutor(autores) {
    var opcoes = [];
    if (autores) {
        autores.forEach(autor => {
            opcoes.push("<option value='" + autor.id + "'>" + autor.nome + "</option>");
        });
    }

    $("#AutorId").html(opcoes.join(""));
}

function atualizarOpcoesCampoSelecaoCategorias(categorias) {
    var opcoes = [];
    if (categorias) {
        categorias.forEach(categoria => {
            opcoes.push("<option value='" + categoria.id + "'>" + categoria.nome + "</option>");
        });
    }

    $("#CategoriaId").html(opcoes.join(""));
}

function atualizarOpcoesCampoSelecaoSubCategorias(subCategorias) {
    var opcoes = [];
    if (subCategorias) {
        subCategorias.forEach(subCategoria => {
            opcoes.push("<option value='" + subCategoria.id + "'>" + subCategoria.nome + "</option>");
        });
    }

    $("#SubCategoriaId").html(opcoes.join(""));
}

function atualizarOpcoesCampoSelecaoEditoras(editoras) {
    var opcoes = [];
    if (editoras) {
        editoras.forEach(editor => {
            opcoes.push("<option value='" + editor.id + "'>" + editor.nome + "</option>");
        });
    }

    $("#EditoraId").html(opcoes.join(""));
}
 
function cadastrarAutor(el) {
    let botoes = [
        {
            estilo: "btn-warning", icone: "fa fa-chevron-left", callback: function () {
                $.sidebar.fnFechar();
            }
        }, {
            estilo: "btn-dark", icone: "fa fa-save", loadingText:"Processando...", label: "Salvar", callback: function () {
                jQuery.validator.unobtrusive.parse($(".sidebar .corpo"));

                const formId = "form-cadastrar-autor";

                if ($("#" + formId).validate().form()) {
                    const myRequest = new Request('/Autor/Cadastrar', {
                        method: 'POST',
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify({
                            Nome: $(`#${formId} #Nome`).val()
                        })
                    });

                    $.sidebar.fnLoader(true);
                    fetch(myRequest)
                        .then(response => { return response.json(); })
                        .then(dados => {
                            if (!dados.sucesso) {
                                var erros = [];
                                $(dados.erros).each(function (index, erroObj) {
                                    erros.push(erroObj.mensagem);
                                });
                                alertError({ title: dados.mensagem, text: erros.join("<br/>") });
                            } else {
                                atualizarOpcoesCampoSelecaoAutor(dados.autores);
                                $.sidebar.fnFechar();
                            }
                            $.sidebar.fnLoader(false);
                        }).catch(error => {
                            alertError({ text: error });
                            $.sidebar.fnLoader(false);
                        });
                }
            }
        }
    ];

    $.sidebar(this, { url: `/Autor/Cadastrar`, botoes: botoes });
};