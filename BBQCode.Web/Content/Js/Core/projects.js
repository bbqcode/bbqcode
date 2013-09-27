$(document).ready(function () {
    $('.texting').scrollFollow(
    {
        container: $(this).parents('.projectContainer').attr('id'),
        offset: 20
    });
});