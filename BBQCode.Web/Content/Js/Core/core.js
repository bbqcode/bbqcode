$(document).ready(function () {
    $("#slider_contain").jCarouselLite({
        btnNext: "#next",
        btnPrev: "#prev",
        visible: 1,
        auto: 5000,
        speed: 1250,
        easing: 'easeInOutExpo',
        beforeStart: h2Hide,
        afterEnd: h2Show
    });

    h2Hide();
    h2Show();


    if ($("#rightCol").height() < $("#mainContent").height()) {
        $("#rightCol").height($("#mainContent").height());
    }

    $('.teamMember').hover(function () {
        $(this).find(".picTeam").hide();
        $(this).find(".altTeam").show();
    }, function () {
        $(this).find(".picTeam").show();
        $(this).find(".altTeam").hide();
    });
});

function h2Show(element) {
    $(element).add('#slider h2').animate({ top: 8, opacity: '1' }, 200);
}

function h2Hide() {
    $('#slider h2').animate({ top: 25, opacity: '0' }, 75);
}

var lang = $('head meta[lang]').attr('lang');