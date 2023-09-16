//funcionalidade de loading em botoes
(function($) {
    $.fn.button = function(action) {
        //console.log('entrou na função')
        if (this.length > 0) {
            this.each(function(){
                if (action === 'loading' && $(this).data('loading-text')) 
                {
                    $(this).data('original-text', $(this).html())
                           .html($(this).data('loading-text'))
                           .prop('disabled', true);
                } 
                else if (action === 'reset' && $(this).data('original-text')) 
                {
                    $(this).html($(this).data('original-text'))
                           .prop('disabled', false);
                }
            });
        }
    };
}(jQuery));