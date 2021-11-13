class Troca {
    constructor() {

        this.iniciarEventos();
    }

    iniciarEventos() {
        let _self = this; 
        let btn = $("#btn-solicitar");
        btn.on('click', function () {
            $(btn).button('loading');
            let params = {
                disponibilizacaoTrocaId: parseInt(btn.data('disponibilizacaoTrocaId'))
            };
            $.post('/Troca/Solicitar', params,function (data) {
                if (data.sucesso) {
                    alertSuccess({ title: "Troca solicitada com sucesso", text: "" }, function () {
                        document.location = "/";
                    });
                } else {
                    if (data.erros != null) {
                        $("#form-error")
                            .html("<strong>Verifique os erros a seguir:</strong><br />" + data.erros.map(e => e.mensagem).join("<br/>"))
                            .removeClass("hidden");

                    }
                    alertError({ text: data.mensagem })
                }

                $(btn).button('reset');
            }).fail(function () {
                alertServerError();
                $(btn).button('reset');
            })
        });
    } 
}
