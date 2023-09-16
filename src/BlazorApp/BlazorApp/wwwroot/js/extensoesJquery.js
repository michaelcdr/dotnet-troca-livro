//extendendo funções do jquery...
$.extend({
    isNullOrEmpty: function (value) {
        try {
            return value === null || value === "";
        } catch (ex) {
            return ex.message;
        }
    },
    isNullOrEmptyOrUndefined: function (value) {
        try {
            return value === null || value === undefined || value === "";
        } catch (ex) {
            return ex.message;
        }
    }
});