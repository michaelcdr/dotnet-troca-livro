class Troca {
    constructor() {

        this.iniciarEventos();
        
    }

    iniciarEventos() {
        let _self = this;
        //console.log(document.querySelector("#btn-solicitar"));
        let btn = document.querySelector("#btn-solicitar");
        btn.addEventListener('click', function () {
            $(btn).button('loading');
            $.post('/Livro/Trocar')
        });
    } 
}
