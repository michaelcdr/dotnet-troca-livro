window.alertSuccess = function (dataObj, callback) {
    let opts = {
        type: "success",
        toast: true,
        position: 'top-start',
        timer: 3000,
        showConfirmButton: false
    }

    $.extend(opts, dataObj);

    Swal.fire(opts)
        .then((result) => {
            if (result.value) 
                callback();
        });
}

window.alertError = function (dataObj) {
    let opts = {
        title: 'Ops, algo deu errado',
        type: 'error',
        showConfirmButton: false
    };
    $.extend(opts, dataObj);

    Swal.fire(opts);
}

window.alertServerError = function () {
    Swal.fire({
        toast: false,
        title: 'Ops, algo deu errado',
        text: 'Ocorreu um erro interno em nossos servidores, tente novamente mais tarde.',
        type: 'error'
    });
}

window.alertConfirm = function (dataObj, callback) {
    let opts = {
        title: "Atenção",
        text: "Erro deseja remover?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#DC3545',
        cancelButtonColor: '#343A40',
        confirmButtonText: 'Sim, desejo remover'
    };
    $.extend(opts, dataObj);

    Swal.fire(opts)
        .then((result) => {
            if (result.value) {
                callback();
            }
        });
}