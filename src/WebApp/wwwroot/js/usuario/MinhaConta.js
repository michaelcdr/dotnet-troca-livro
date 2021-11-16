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

    carregarAba(el) {
        let _self = this;
        let action = el.data('action');
        $(el).html("Processando, aguarde...");

        $.get(action, function (data) {
            $(el).html(data);

            $('.btn-aprovar').unbind('click');
            $('.btn-aprovar').click(function () {
                _self.aprovar($(this));
            });

           

            $(".btn-detalhes").unbind('click');
            $('.btn-detalhes').click(function (ev) {
                ev.preventDefault();
                $('.tab-pane.active').html('Processando, aguarde...');

                let trocaId = parseInt($(this).data('id'));
                $.get('/Troca/_Detalhes', { id: trocaId }, function (htmlViewDetalhes) {
                    $('.tab-pane.active').html(htmlViewDetalhes);

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
        });
    }

    aprovar(el) {
        let trocaId = parseInt(el.data('id'));
        el.button('loading');
        $.post("/Troca/Aprovar", { trocaId: trocaId }, function (data) {
            if (data.sucesso) {
                $("#trocas-solicitadas-tab").trigger('click');
                el.button('reset');
            } else {
                alertError({ text: data.mensagem });
            }
        }).fail(function () {
            alertServerError();
            el.button('reset');
        });
    }

    marcarComoEnviada(el) {
        Swal.fire({
            text: "Deseja marcar esse livro com enviado",
            confirmButtonText: "OK",
            confirmButtonColor: '#28a745',
            showLoaderOnConfirm: true,
            title: "Atenção",
            type: 'warning',
            showCancelButton: true,
            cancelButtonColor: '#343A40',
            cancelButtonText: "Cancelar",
            preConfirm: function () {
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
            }
        })
    }
}

var minhaConta = new MinhaConta();