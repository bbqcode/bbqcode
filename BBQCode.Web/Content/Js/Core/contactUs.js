/// <reference path="../libs/jquery-1.5.1-vsdoc.js" />
$(document).ready(function () {
    $("form").submit(function (event) {
        event.preventDefault();
        if (isValid()) {
            document.body.style.cursor = 'wait';
            $.ajax({
                type: "Post",
                url: "/" + lang + "/ContactUs",
                data: $("form").serialize(),
                success: function (data) {
                    if (data == "True") {
                        doSuccess();
                    } else {
                        doFailure();
                    }
                }
            });
            document.body.style.cursor = 'default';
        } else {
            doFailure();
        }
    });

    $("form").keypress(function (event) {
        if (eventwhich == 13) {
            event.preventDefault();
            $('form').submit();
        }
    });
});

function doSuccess() {
    $("form input, form textarea").attr("disabled", "disabled");
    $("#successBox").show();
    $("#errorBox").hide();
    jumpTo("#successBox");
}

function doFailure() {
    $("#errorBox").show();
    $("#successBox").hide();
    jumpTo("#errorBox");
}
function jumpTo(element) {
    var pos = $(element).offset();
    window.scrollTo(pos.left, pos.top - 200);
}


function isValid() {
    var v1 = $("#FullName").val().length == 0;
    var v2 = $("#Email").val().length == 0;
    var v3 = $("#Message").val().length == 0;
    var v4 = $("#Answer").val().length == 0;

    var i1 = parseInt($("#QuestionPartOne").val());
    var i2 = parseInt($("#QuestionPartTwo").val());
    var answer = i1 + i2 == parseInt($("#Answer").val());
    var empty = $("#DoNotFillThis").val().length == 0;

    return !(v1 || v2 || v3 || v4) && answer && empty;
}