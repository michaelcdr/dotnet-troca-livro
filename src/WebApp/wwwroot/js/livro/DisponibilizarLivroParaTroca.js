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
                $.post(_self.obterAction(), request, function (data) {
                    if (data.sucesso) {
                        alertSuccess({ title: "Sucesso", text: "O livro foi disponibilizado para troca com sucesso." });
                        setTimeout(function () {
                            document.location = `/Livro/Detalhes/${request.livroId}`;
                        },1000)
                    } else
                        alertError({ text: data.Mensagem });

                    _self._btn.button('reset');
                });
            }
        });
    }
}