
jQuery("document").ready(function () {

    /*Menu-toggle*/

    jQuery("#menu-toggle").click(function (e) {

        if (screen.width >= 1025) {
            e.preventDefault();
            jQuery("#wrapper").toggleClass("MenuActive");
        }
    });

    /* On click dropdown open left side menu */

    jQuery("li.dropdown-Nav").on("click", function (e) {
        if (jQuery("body").hasClass("MenuActive")) {
            jQuery(this).children('ul.dropdownMenu').toggle();
            //jQuery(this).siblings('li.dropdown-Nav').find('ul.dropdownMenu').show();
            e.stopPropagation();
        }
    });

    /* on hover dropdown open left side menu */

    jQuery("li.dropdown-Nav").hover(function (e) {
        if ($("body").hasClass("MenuActive") == false) {
            jQuery(this).children('ul.dropdownMenu').toggle();
            e.stopPropagation();
        }
    });

    jQuery(".page-content").on("click", function () {
        jQuery(".dropdownMenu").hide();
    });


});

$(document).ready(function () {
    $("#search").on("keyup", function () {
        if (this.value.length > 0) {
            $("#myUL li").hide().filter(function () {
                return $(this).text().toLowerCase().indexOf($("#search").val().toLowerCase()) != -1;
            }).show();
        }
        else {
            $("#myUL li").show();
        }
    });

});



$(document).ready(function () {
    var textarea = document.querySelector('textarea');

    textarea.addEventListener('keydown', autosize);

    function autosize() {
        var el = this;
        setTimeout(function () {
            el.style.cssText = 'height:auto; padding:0';
            // for box-sizing other than "content-box" use:
            // el.style.cssText = '-moz-box-sizing:content-box';
            el.style.cssText = 'height:' + el.scrollHeight + 'px';
        }, 0);
    }

});


$(document).ready(function () {
    $(".data-grid").doubleScroll({ resetOnWindowResize: true });
});


$(document).ready(function () {

    $('.spinner .btn:first-of-type').on('click', function () {
        var btn = $(this);
        var input = btn.closest('.spinner').find('input');
        if (input.attr('max') == undefined || parseInt(input.val()) < parseInt(input.attr('max'))) {
            input.val(parseInt(input.val(), 10) + 1);
        } else {
            btn.next("disabled", true);
        }
    });
    $('.spinner .btn:last-of-type').on('click', function () {
        var btn = $(this);
        var input = btn.closest('.spinner').find('input');
        if (input.attr('min') == undefined || parseInt(input.val()) > parseInt(input.attr('min'))) {
            input.val(parseInt(input.val(), 10) - 1);
        } else {
            btn.prev("disabled", true);
        }
    });

})

