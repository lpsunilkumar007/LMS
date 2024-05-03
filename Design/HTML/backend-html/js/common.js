$(document).ready(function () {
      $('[data-toggle=search-form]').click(function () {
        $('.search-form-wrapper').toggleClass('open');
        $('.search-form-wrapper .search').focus();
    });
    $('.search-form-wrapper .search').keypress(function (event) {
        if ($(this).val() == "Search") $(this).val("");
    });
    $('.search-form-wrapper').click(function (event) {
        $('.search-form-wrapper').removeClass('open');
    });
});



$(window).load(function () {
    $("#flexiselDemo3").flexisel({
        visibleItems: 4,
        itemsToScroll: 1,
        autoPlay: {
            enable: true,
            interval: 5000,
            pauseOnHover: true
        }
    });
});