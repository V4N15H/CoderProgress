$(document).ready(function() {
    console.log("document is ready");

    $(".bd-img").hover(
        function() {
            $(this).addClass('shadow-lg').css('cursor', 'pointer');
        },
        function() {
            $(this).removeClass('shadow-lg');
        }
    );

    $(".card").hover(
        function() {
            $(this).addClass('shadow-lg').css('cursor', 'pointer');
            $(this).find(".card-img-overlay").removeClass('invisible').css('cursor', 'pointer');

        },
        function() {
            $(this).removeClass('shadow-lg');
            $(this).find(".card-img-overlay").addClass('invisible');
        }
    );
});


