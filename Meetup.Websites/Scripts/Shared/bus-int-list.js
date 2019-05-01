const createBusIntList = function(appendTo)  {
    //Get list of business names
    var businessList = [];
    var businessElements = $("#business-list-selected").children("option");
    for (var i = 0; i < businessElements.length; i++) {
        businessList.push(businessElements[i].value);
    }

    //Get list of interest names
    var interestList = [];
    var interestElements = $("#interest-list-selected").children("option");
    for (var i = 0; i < interestElements.length; i++) {
        interestList.push(interestElements[i].value);
    }

    $("#ChosenBusinesses").remove();
    $("#ChosenInterests").remove();
    $(appendTo).append($("<input type=\"hidden\" value=\"" + businessList.join(",") + "\" name=\"ChosenBusinesses\" id=\"ChosenBusinesses\">"));
    $(appendTo).append($("<input type=\"hidden\" value=\"" + interestList.join(",") + "\" name=\"ChosenInterests\" id=\"ChosenInterests\">"));
};

$("document").ready(() => {
    //Business list
    $("#business-list-unselected").change(() => {
        $("#business-list-selected").prepend($("#business-list-unselected").children("option:selected")).children("option:selected").prop("selected", false);
        $("#business-list-unselected").children("option:selected").remove();
    });

    $("#business-list-selected").change(() => {
        $("#business-list-unselected").prepend($("#business-list-selected").children("option:selected")).children("option:selected").prop("selected", false);
        $("#business-list-selected").children("option:selected").remove();
    });

    //interest list
    $("#interest-list-unselected").change(() => {
        $("#interest-list-selected").prepend($("#interest-list-unselected").children("option:selected")).children("option:selected").prop("selected", false);
        $("#interest-list-unselected").children("option:selected").remove();
    });

    $("#interest-list-selected").change(() => {
        $("#interest-list-unselected").prepend($("#interest-list-selected").children("option:selected")).children("option:selected").prop("selected", false);
        $("#interest-list-selected").children("option:selected").remove();
    });
});