class MinhaConta {
    constructor() {
        this.iniciarEventos()
    }

    iniciarEventos() {
        this.carregarAba($("#editar-usuario"));
        let _self = this;
        $("#tabs-minha-conta > a").click(function (ev) {
            ev.preventDefault();
            let idContainer = $(this).attr("href");
            let containerEl = $(idContainer);
            
            _self.carregarAba(containerEl);
        });
    }

    iniciarEventosFormEdicaoUsuario() {
        let formEl = $("#editar-usuario-form");
        jQuery.validator.unobtrusive.parse(formEl.parent());
        let _self = this;
        if ($(".img-livro-container").length > 0) 
            _self.iniciarEventosRemocaoImg();

        formEl.unbind('submit');
        formEl.submit(function () {
            let btn = $("#btn-editar-usuario");
            let formData = new FormData();
            formData.append('UsuarioId', $("#UsuarioId").val());
            formData.append('Nome', $("#Nome").val());
            formData.append('Sobrenome', $("#Sobrenome").val());
            formData.append('Email', $("#Email").val());

            if ($('#Avatar')[0].files != null && $('#Avatar')[0].files.length > 0)
                formData.append('Avatar', $('#Avatar')[0].files[0]);

            btn.button('loading');

            let taValido = formEl.validate().form();
            if (taValido) {
                $.ajax({
                    url: formEl.attr("action"),
                    type: 'POST',
                    data: formData,
                    contentType: false,
                    processData: false,
                    error: function () {
                        btn.button('reset');
                        alertError({ text: "Ocorreu um erro interno em nossos servidores, tente novamente mais tarde." });
                    },
                    success: function (retorno) {
                        if (retorno.sucesso) {
                            alertSuccess({ title: "Sucesso", text: retorno.mensagem });
                        } else {
                            if (retorno.erros != null) {
                                let text = "Verifique os erros a seguir: <br/>" + retorno.erros.map(e => e.mensagem).join(",");
                                alertError({ html : text, title: retorno.mensagem });

                            } else
                                alertError({ text: retorno.mensagem });
                        }

                        btn.button('reset');
                    }
                });
            }
            return false;
        });

        $('#Avatar').unbind('change');
        $('#Avatar').change(function () {
            let input = this;
            if (input.files) {
                $('.img-livro-container').parent().remove();

                for (var i = 0; i < input.files.length; i++) {
                    let reader = new FileReader();
                    reader.onload = function (e) {
                        let imagemId = createGuid();
                        let cardHtml = obterCardUpload(imagemId, e.target.result);

                        $('#imagens-container').append(cardHtml);
                        _self.iniciarEventosRemocaoImg();
                    }

                    reader.readAsDataURL(input.files[i]);
                }
            }
        });
    }

    iniciarEventosRemocaoImg() {
        $('.btn-remover-img').unbind('click');
        $('.btn-remover-img').click(function () {
            let id = $(this).data('imagemId');
            let index = obterIndiceImagem(id);
            removerArquivoDoFileList(index, '#Avatar');
            $('.card[data-imagem-id="' + id + '"]').remove();
        });
    }

    carregarAba(el) {
        let _self = this;
        let action = el.data('action');
        let idForm = el.attr("id");

        $(el).html("Processando, aguarde...");

        $.get(action, function (data) {
            $(el).html(data);

            if (idForm == "editar-usuario") {
                _self.iniciarEventosFormEdicaoUsuario()
            } else {
                

                $(".btn-detalhes").unbind('click');
                $('.btn-detalhes').click(function (ev) {
                    ev.preventDefault();
                    $('.tab-pane.active').html('Processando, aguarde...');

                    let trocaId = parseInt($(this).data('id'));
                    $.get('/Troca/_Detalhes', { id: trocaId }, function (htmlViewDetalhes) {
                        $('.tab-pane.active').html(htmlViewDetalhes);

                        $('.btn-voltar').data("target", "#" + idForm);
                        $(".btn-voltar").unbind('click');
                        $(".btn-voltar").click(function (ev) {
                            ev.preventDefault();
                            let target = $(this).data('target');
                            _self.carregarAba($(target));
                        });

                        $("#btn-marcar-como-enviada").unbind('click');
                        $("#btn-marcar-como-enviada").click(function () {
                            _self.marcarComoEnviada($(this));
                        });

                        $("#btn-marcar-como-recebido").unbind('click');
                        $("#btn-marcar-como-recebido").click(function () {
                            _self.marcarComoRecebido($(this));
                        });

                        $('.btn-aprovar').unbind('click');
                        $('.btn-aprovar').click(function () {
                            _self.aprovar($(this));
                        });

                        $('.img-livro').unbind('click');
                        $('.img-livro').click(function (ev) {
                            ev.preventDefault();

                            let w = window.open('');
                            let image = new Image();
                            image.src = $(this).attr('src');

                            w.document.write(image.outerHTML);
                            w.stop();
                        });
                    });
                });
            }
        });
    }

    aprovar(el) {
        let funcaoAprovar = function () {
            return new Promise(function (resolve, reject) {
                let trocaId = parseInt(el.data('id'));
                el.button('loading');
                $.post("/Troca/Aprovar", { trocaId: trocaId }, function (data) {
                    if (data.sucesso) {
                        alertSuccess({ timer: 3000, title: "Sucesso", text: data.mensagem });
                        $("#trocas-solicitadas-tab").trigger('click');
                        el.button('reset');
                    } else {
                        alertError({ text: data.mensagem });
                    }
                    resolve();
                }).fail(function () {
                    alertServerError();
                    el.button('reset');
                });
            });
        };
        alertConfirmComPromise("Deseja aprovar a troca?", funcaoAprovar);
    }

    marcarComoEnviada(el) {
        let marcarComoEnviadaFuncao = function () {
            return new Promise(function (resolve, reject) {
                $.post('/Troca/MarcarComoEnviada', { id: el.data('id') }, function (data) {
                    if (!data.sucesso) {
                        alertError({ text: data.mensagem });
                    } else {
                        $("#status-atual").html("<strong>Status:</strong> Enviado");
                        el.remove();
                    }
                    resolve();
                }).fail(function () {
                    alertServerError();
                });
            });
        };
        alertConfirmComPromise("Deseja marcar esse livro com enviado?", marcarComoEnviadaFuncao); 
    }

    marcarComoRecebido(el) {
        let marcarComoRecebidoFuncao = function () {
            return new Promise(function (resolve, reject) {
                $.post('/Troca/MarcarComoRecebido', { id: el.data('id') }, function (data) {
                    if (!data.sucesso) {
                        alertError({ text: data.mensagem });
                    } else {
                        $("#status-atual").html("<strong>Status:</strong> Recebido");
                        el.remove();
                    }
                    resolve();
                }).fail(function () {
                    alertServerError();
                });
            });
        };
        alertConfirmComPromise("Deseja marcar esse livro com recebido?", marcarComoRecebidoFuncao);
    }

}

var minhaConta = new MinhaConta();