$("document").ready(() => {
    $(".meeting-pause").click(() => {
        $(event.target).parent(".meeting-pause").toggleClass("collapsed");
        if ($(event.target).hasClass("meeting-pause")) {
            $(event.target).toggleClass("collapsed");
        }
    });
});