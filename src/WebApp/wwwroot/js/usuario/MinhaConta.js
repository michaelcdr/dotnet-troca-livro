class MinhaConta {
    constructor() {
        this.iniciarEventos()
    }

    iniciarEventos() {
        this.carregarAba($("#editar-usuario"));
        let _self = this;
        $("#tabs-minha-conta > li > a").click(function (ev) {
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
        });
    }

    aprovar(el) {
        let trocaId = parseInt(el.data('trocaId'));
        el.button('loading');
        $.post("/Troca/Aprovar", { trocaId: trocaId }, function (data) {
            $("#trocas-solicitadas-tab").trigger('click');
            el.button('reset');
        });
    }
}

var minhaConta = new MinhaConta();