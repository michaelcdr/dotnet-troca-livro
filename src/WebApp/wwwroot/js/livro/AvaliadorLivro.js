class AvaliadorLivro {
    constructor() {

        this.iniciarEventos();
        
    }

    iniciarEventos() {
        let _self = this;
        document.querySelector('#nota-livro')
            .addEventListener('click', function (ev) {
                console.log(ev.target);
                console.log(ev.target.dataset)
                _self.avaliar(ev.target)
            });
    }

    avaliar(target) {
        let indice = target.dataset.indice;

        for (var i = 1; i < 6; i++) {
            document.querySelector('#nota-livro .fa-star[data-indice="' + i + '"]')
                .classList.add('deselecionada');
        }

        for (var i = 1; i <= indice; i++) {
            document.querySelector('#nota-livro .fa-star[data-indice="' + i + '"]')
                .classList.remove('deselecionada');
        }

        let nota = document.querySelectorAll('#nota-livro .fa-star:not(.deselecionada)').length;

        document.querySelector("#Nota").setAttribute("value", nota);
    }
}
