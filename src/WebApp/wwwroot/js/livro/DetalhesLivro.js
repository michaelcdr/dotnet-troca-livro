function DetalhesLivro() {

    this._aplicarEventos();
    this._livroId = document.querySelector('#detalhes-livro').dataset.livroId;
    this.avaliador = null;
}

DetalhesLivro.prototype.obterTitulo = function () {
    return document.querySelector('#detalhes-livro').dataset.titulo;
};

DetalhesLivro.prototype._aplicarEventos = function () {
    let _self = this;
    let btnDeletar = document.querySelector('#btn-deletar');
    if (btnDeletar !== null) {
        btnDeletar.addEventListener('click', function () {
            return _self.deletarLivro(btnDeletar.dataset.id);
        });
    }

    let btnAvaliar = document.querySelector('#btn-avaliar');
    if (btnAvaliar !== null) {
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

DetalhesLivro.prototype.avaliar = function () {
    console.log('avaliar livro...');
    let _self = this;

    var botoes = [
        {
            estilo: "btn-warning", icone: "fa fa-chevron-left", callback: function () {
                $.sidebar.fnFechar();
            }
        }, {
            estilo: "btn-dark", icone: "fa fa-save", label: "Salvar", callback: function () {
            }
        }
    ];

    $.sidebar(this, {
        url: `/livro/avaliar/${parseInt(_self._livroId)}?tituloLivro=${_self.obterTitulo()}`,
        callbackAbrir: function () {
        },
        botoes: botoes
    });
};