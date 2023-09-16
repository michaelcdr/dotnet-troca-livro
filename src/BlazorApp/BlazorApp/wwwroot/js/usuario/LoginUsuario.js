class LoginUsuario {
    constructor() {
        this._formEl = $('form#form-login');
        this._loginEl = $("#login");
        this._passwordEl = $("#password");
        this.initEvents();
    }

    initEvents() {
        let _self = this;
        _self._formEl.submit(() => {
            _self.login();
            return false;
        });

        $('#btn-register').click(function () {
            $('#btn-register').button('loading');
            document.location = '/vendedor-registrar';
        });
    }

    validate() {
        let isValid = true;
        $('input[required]').each((index, el) => {
            if ($(el).val() === '') {
                $(el).addClass('is-invalid');
                isValid = false;
            }
        });
        return isValid;
    }

    getModelData() {
        let _self = this;
        return {
            isValid: _self.validate(),
            model: {
                Usuario: _self._loginEl.val(),
                Senha: _self._passwordEl.val()
            }
        };
    }

    login() {
        let _self = this;
        let objModel = _self.getModelData();
        let btn = _self._formEl.find('button[type=submit]');
        if (objModel.isValid) {
            btn.button('loading');
            $.post('/Usuario/Logar', objModel.model, function (data) {
                if (data.sucesso) {

                    document.location = data.urlDestino;
                } else {
                    alertError({ text: data.mensagem, toast: true, timer: 4000 });
                    btn.button('reset');
                }
            }).fail(() => {
                alertServerError();
                btn.button('reset');
            });
        }
        return false;
    }
}