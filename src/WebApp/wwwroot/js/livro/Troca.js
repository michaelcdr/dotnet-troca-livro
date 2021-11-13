class Troca {
    constructor() {
        this.formEl = $("#form-solicitacao");
        this.btnEl = $("#btn-solicitar");
        this.iniciarEventos();
        
    }

    iniciarEventos() {
        let _self = this;
        _self.btnEl.on('click', function () {
            _self.btnEl.button('loading');

            $.post('/Troca/Solicitar', _self.obterDadosFormulario(), function (data) {
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
                _self.btnEl.button('reset');

            }).fail(function () {
                alertServerError();
                _self.btnEl.button('reset');
            })
        });

        $("#CEP").on('change', function () {
            let cep = $(this).val();
            if (cep !== "") {
                $.getJSON(`https://viacep.com.br/ws/${cep}/json/`, function (data) {
                    if (data) {
                        $("#Bairro").val(data.bairro);
                        $("#Cidade").val(data.localidade);
                        $("#UF option").prop("selected", false);
                        $("#UF option[value='" + data.uf + "']").prop("selected", true);
                        $("#Logradouro").val(data.logradouro);
                    }
                });
            }
        });
    }

    obterDadosFormulario() { return this.formEl.serialize(); }

    validar() { return this.formEl.validate().form(); }
}
