function DetalhesLivro() {

    this._aplicarEventos();
    this._livroId = document.querySelector('#detalhes-livro').dataset.livroId;
    this._formAvaliacaoId = "form-avaliar";
    this.avaliador = null;
}

DetalhesLivro.prototype.obterTitulo = function () {
    return document.querySelector('#detalhes-livro').dataset.titulo;
};

DetalhesLivro.prototype._aplicarEventos = function () {
    let _self = this;
    let btnDeletar = document.querySelector('#btn-deletar');
    if (btnDeletar !== null) {
        btnDeletar.removeEventListener('click', function () { });
        btnDeletar.addEventListener('click', function () {
            return _self.deletarLivro(btnDeletar.dataset.id);
        });
    }

    let btnAvaliar = document.querySelector('#btn-avaliar');
    if (btnAvaliar !== null) {
        btnAvaliar.removeEventListener('click', function () { });
        btnAvaliar.addEventListener('click', function () {
            return _self.avaliar(btnAvaliar.dataset.livroId);
        });
    }
};

DetalhesLivro.prototype.deletarLivro = function (id) {
    let callback = function () {
        let parametros = new URLSearchParams({ id: id });

        const myRequest = new Request('/Livro/Deletar?' + parametros, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        });

        fetch(myRequest)
            .then(response => { return response.json(); })
            .then(dados => {
                if (dados.sucesso) {
                    document.location = '/Home/Index';
                }

            }).catch(error => {
                console.error('There has been a problem with your fetch operation:', error);
            });
    };

    let options = {
        text: 'Essa ação não pode ser desfeita.',
        cancelButtonText: 'Cancelar',
        title: 'Atenção, você esta prestes a remover o livro, deseja continuar?'
    };

    alertConfirm(options, callback);
};

DetalhesLivro.prototype.obterDadosFormAvaliacao = function () {
    let formId = this._formAvaliacaoId;
    //return JSON.stringify({
    //    LivroId: $(`#${formId} #LivroId`).val(),
    //    Titulo: $(`#${formId} #Titulo`).val(),
    //    Descricao: $(`#${formId} #Descricao`).val(),
    //    Nota: parseInt($(`#${formId} #Nota`).val())
    //});

    let formData = new FormData();
    formData.append('LivroId', parseInt($(`#${formId} #LivroId`).val()));
    formData.append('Titulo', $(`#${formId} #Titulo`).val());
    formData.append('Descricao', $(`#${formId} #Descricao`).val());

    if ($.isNullOrEmpty($(`#${formId} #Nota`).val()))
        formData.append('Nota', 0);
    else
        formData.append('Nota', parseInt($(`#${formId} #Nota`).val()));

    return formData;
};

DetalhesLivro.prototype.formAvaliacaoTaValido = function () {
    let formId = this._formAvaliacaoId;
    return $("#" + formId).validate().form();
};

DetalhesLivro.prototype.avaliar = function () {
    console.log('avaliar livro...');
    let _self = this;

    let botoes = [
        {
            estilo: "btn-warning", icone: "fa fa-chevron-left", callback: function () {
                $.sidebar.fnFechar();
            }
        }, {
            estilo: "btn-dark", icone: "fa fa-save", label: "Salvar", callback: function () {
                jQuery.validator.unobtrusive.parse($(".sidebar .corpo"));

                if (_self.formAvaliacaoTaValido()) {
                    const myRequest = new Request('/Livro/Avaliar', {
                        method: 'POST',
                        body: _self.obterDadosFormAvaliacao()
                    });

                    fetch(myRequest)
                        .then(response => { return response.json(); })
                        .then(dados => {
                            if (!dados.sucesso) {
                                var erros = [];
                                $(dados.erros).each(function (index,erroObj) {
                                    if (erroObj.mensagem === "The value '0' is invalid.")
                                        erros.push("Informe a nota.");
                                    else
                                        erros.push(erroObj.mensagem);
                                });
                                alertError({ title: dados.mensagem, text: erros.join("<br/>") });
                            } else {
                                _self.listarAvaliacoes();
                                $.sidebar.fnFechar();
                            }

                        }).catch(error => {
                            alertError({ text : error})
                        });
                }
            }
        }
    ];

    $.sidebar(this, {
        url: `/livro/avaliar/${parseInt(_self._livroId)}?tituloLivro=${_self.obterTitulo()}`,
        botoes: botoes
    });
};

DetalhesLivro.prototype.listarAvaliacoes = function () {
    let livroId = this._livroId;
    let _self = this;
    const myRequest = new Request('/Livro/_Avaliacoes/' + livroId, {
        method: 'GET' 
    });

    fetch(myRequest)
        .then(response => {
            response.text().then(resultado => {
                $("#avaliacoes-container").html(resultado)
                _self._aplicarEventos();
            });
        });
};