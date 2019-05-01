$("document").ready(() => {
    //Toggle between options
    $("#wish-person-button").click(() => {
        $(this).addClass("selected");
        $("#wish-business-button").removeClass("selected");
        $("#wish-person-button").addClass("selected");
        $("#wish-person").removeClass("hidden");
        $("#wish-business").addClass("hidden");
    });

    $("#wish-business-button").click(() => {
        $(this).addClass("selected");
        $("#wish-person-button").removeClass("selected");
        $("#wish-business-button").addClass("selected");
        $("#wish-person").addClass("hidden");
        $("#wish-business").removeClass("hidden");
    });

    //submit business wish
    $("#wish-business-submit").click(() => {
        createBusIntList("#wish-business");
    });
});