class MinhaConta {
    constructor() {
        this.iniciarEventos()
    }

    iniciarEventos() {
        this.carregarAba($("#editar-usuario"));
        let _self = this;
        $("#tabs-minha-conta > li > a").click(function () {
            let idContainer = $(this).attr("href");
            let containerEl = $(idContainer);
            
            _self.carregarAba(containerEl);
        });
    }

    carregarAba(el) {
        let action = el.data('action');
        $.get(action, function (data) {
            $(el).html(data);
        });
    }
}

var minhaConta = new MinhaConta();