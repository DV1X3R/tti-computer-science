$(function () {
    $('.jq-form').each(function () {
        let form = $(this);
        let btn = form.find('.button-submit');

        btn.click(function () {
            btn.removeClass('submit-disabled'); // Force mark submit button as enabled
            form.find('.field-required').each(function () {
                if ($(this).val() != '') { // Unmark fields as empty
                    $(this).removeClass('field-empty');
                } else { // Mark fields as empty and disable button
                    $(this).addClass('field-empty');
                    btn.addClass('submit-disabled');
                }
            });

            if ($(this).hasClass('submit-disabled')) {
                form.find('.field-empty').css({
                    'border-color': 'red' // Set the red border color to empty fields
                });

                setTimeout(function () { // Remove style from empty fields after 500ms
                    form.find('.field-empty').removeAttr('style');
                }, 500);

                return false; // Do not submit anything
            } else {
                form.submit();
                return true; // Submit form
            }
        });
    });
});
