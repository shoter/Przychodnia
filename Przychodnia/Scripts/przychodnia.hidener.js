$(function () {
    //How to use :
    $("[data-checkbox_hidener]").each(function () {
        var $this = $(this);

        var $$el1 = $this.attr("data-hide_element1");
        var $$el2 = $this.attr("data-hide_element2");

        if ($$el2 != undefined && $$el2 != "") {
                var $el1 = $($$el1);
                var $el2 = $($$el2);

                if ($el1.is(":visible") && $el2.is(":visible")) {
                    $el2.hide();
            }
        }

    })

    $(document).on("change", "[data-checkbox_hidener]", function () {
        var $this = $(this);

        var $el1 = $($this.attr("data-hide_element1"));
        $el1.toggle();

        var $el2 = $($this.attr("data-hide_element2"));
        $el2.toggle();
    });

})