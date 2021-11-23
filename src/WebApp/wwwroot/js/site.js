// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    // executes when HTML-Document is loaded and DOM is ready

    // breakpoint and up  
    //$(window).resize(function () {
    //    if ($(window).width() >= 980) {

    //        // when you hover a toggle show its dropdown menu
    //        $(".navbar .dropdown-toggle").hover(function () {
    //            $(this).parent().toggleClass("show");
    //            $(this).parent().find(".dropdown-menu").toggleClass("show");
    //        });

    //        // hide the menu when the mouse leaves the dropdown
    //        $(".navbar .dropdown-menu").mouseleave(function () {
    //            $(this).removeClass("show");
    //        });

    //    }
    //});

    (function ($) {
        $('.dropdown-menu a.dropdown-toggle').on('click', function (e) {
            if (!$(this).next().hasClass('show')) {
                $(this).parents('.dropdown-menu').first().find('.show').removeClass("show");
            }
            var $subMenu = $(this).next(".dropdown-menu");
            $subMenu.toggleClass('show');

            $(this).parents('li.nav-item.dropdown.show').on('hidden.bs.dropdown', function (e) {
                $('.dropdown-submenu .show').removeClass("show");
            });

            return false;
        });
    })(jQuery)
});

function createGuid() {
    let S4 = () => Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1);
    let guid = `${S4()}${S4()}-${S4()}-${S4()}-${S4()}-${S4()}${S4()}${S4()}`;

    return guid.toLowerCase();
}

function removerArquivoDoFileList(indiceDoArquivo, selector) {
    const dt = new DataTransfer();
    const input = document.querySelector(selector);
    const { files } = input

    for (let i = 0; i < files.length; i++) {
        const file = files[i]
        if (indiceDoArquivo !== i)
            dt.items.add(file);
    }

    input.files = dt.files;
}

function obterIndiceImagem(imagemId) {
    let indice = -1;
    $(".card").each(function (index, elemento) {
        if ($(elemento).data('imagemId') === imagemId) {
            indice = index;
            return;
        }
    });
    return indice;
}

function obterCardUpload(imagemId, src) {
    return `<div class="col-md-2 d-flex m-3 align-items-stretch  card" data-imagem-id="${imagemId}" >
        <div class="img-livro-container text-center pt-2 pb-2">
            <img src="${src}" class='img-livro' style='max-width:100px; max-height: 100px' />
        </div>
        <div class="text-center pb-2">
            <button class="btn btn-danger btn-sm btn-remover-img" data-imagem-id="${imagemId}" type="button">Remover</button>
        </div>
    </div>`;
}