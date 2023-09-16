class DisponibilizarLivroParaTroca {
    constructor() {
        this._formEl = $("#form-disponibilizacao");
        this._btn = $("#btn-disponibilizar-troca");
        this.iniciarEventos();
    }

    obterDadosTroca() {
        return {
            livroId: parseInt($("#LivroId").val()),
            descritivo: $("#Descritivo").val(),
            pontos: parseInt($("#Pontos").val())
        };
    }

    obterAction() { return this._formEl.attr("action"); }

    taValido() { return this._formEl.validate().form(); }

    iniciarEventos() {
        let _self = this;

        console.log("iniciarEventos: " + _self.obterAction() );

        _self._btn.click(function () {
            if (_self.taValido()) {
                const request = _self.obterDadosTroca();
                _self._btn.button('loading');

                let formData = new FormData();

                if ($('#Imagens')[0].files != null && $('#Imagens')[0].files.length > 0) 
                    for (var indiceFiles = 0; indiceFiles < $('#Imagens')[0].files.length; indiceFiles++) 
                        formData.append('Imagens', $('#Imagens')[0].files[indiceFiles]);
                
                formData.append('LivroId', parseInt($("#LivroId").val()));
                formData.append('Descritivo', $("#Descritivo").val());
                formData.append('Pontos', parseInt($("#Pontos").val()));

                $.ajax({
                    url: _self.obterAction(),
                    type: 'POST',
                    data: formData,
                    contentType: false,
                    processData: false,
                    error: function () {
                        _self._btn.button('reset');
                        alertError({ text: "Ocorreu um erro interno em nossos servidores, tente novamente mais tarde." });
                    },
                    success: function (data) {
                        if (data.sucesso) {
                            alertSuccess({ title: "Sucesso", text: "O livro foi disponibilizado para troca com sucesso." });
                            setTimeout(function () {
                                document.location = `/Livro/Detalhes/${request.livroId}`;
                            }, 1000);
                        } else
                            alertError({ text: data.Mensagem });

                        _self._btn.button('reset');
                    }
                });
            }
        });

        $('#Imagens').change(function () {
            let input = this;
            if (input.files) {
                console.log(input)
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
    }
}