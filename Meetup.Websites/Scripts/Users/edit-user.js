var Search;
var GetCompanyList = function (event) {
    //stop last search if its still going
    if (Search !== undefined) {
        Search.abort();
    }

    var value = $(event.target).val();
    if (value !== "") {
        //Call API and get suggested companies
        Search = $.get({
            url: "https://autocomplete.clearbit.com/v1/companies/suggest?query=" + value
        }).done((data) => {
            var suggestionList = [];
            for (var i = 0; i < data.length; i++) {
                suggestionList.push($("<option value=\"" + data[i]["name"] + "\">"));
            }
            $(event.target).next().html(suggestionList);
        });
    } else {
        $(event.target).next().html(null);
    }
}

var DeleteCompanyItem = function (event) {
    //Deletes/hiddes the organization clicked on
    $(event.target).parent().addClass("hidden");
    $(event.target).parent().find(".organization-state[value=old]").val("removed");
    $(event.target).parent().find(".organization-state[value=new]").val("new-removed");

    var readdButton = $("<button type=\"button\" class=\"readd-organization\">Fortyd sletning</button>");
    readdButton.click(UndoDeletion);
    $(event.target).parent().parent().append(readdButton);
}

var UndoDeletion = function (event) {
    //Undo the the deletion of an organization
    $(event.target).siblings(".form-group").removeClass("hidden");
    $(event.target).siblings(".form-group").find(".organization-state[value=removed]").val("old");
    $(event.target).siblings(".form-group").find(".organization-state[value=new-removed]").val("new");
    $(event.target).remove();
}

var LastWorkPlaceId;

$("document").ready(() => {
    LastWorkPlaceId = $(".organization-edit").length;
    //organization events
    $(".organization-edit").keyup(GetCompanyList);
    $(".organization-remove").click(DeleteCompanyItem);

    //add organization
    $("#organization-add").click(() => {
        var id = LastWorkPlaceId++;
        var editPlace = $("<input list=\"organization-list" + id + "\" name=\"Organizations[" + id + "].Name\" id=\"Organizations[" + id + "].Name\" class=\"organization-edit\" />");
        editPlace.keyup(GetCompanyList);
        var removeButton = $("<button type=\"button\" class=\"organization-remove\">Slet</button>");
        removeButton.click(DeleteCompanyItem);
        //Create organization form
        $("#organization-list").append($("<li class=\"item\"></li>")
            .append($("<div class=\"form-group\"></div>")
            .append($("<label for=\"Organizations[" + id + "].Name\">Organisation navn</label>"))
            .append(editPlace)
            .append($("<datalist id=\"organization-list" + id + "\" class=\"organization-holder\"></datalist>"))
            .append($("<label for=\"Organizations[" + id + "].StartDate\">Ansættelsesdato</label>"))
            .append($("<input type=\"date\" name=\"Organizations[" + id + "].StartDate\" id=\"Organizations[" + id + "].StartDate\" />"))
            .append($("<label for=\"Organizations[" + id + "].EndDate\">Slut dato</label>"))
            .append($("<input type=\"date\" name=\"Organizations[" + id + "].EndDate\" id=\"Organizations[" + id + "].EndDate\" />"))
            .append($("<input type=\"hidden\" name=\"Organizations[" + id + "].State\" value=\"new\"/ class=\"organization-state\">"))
            .append(removeButton)));
    });

    //make submit
    $("#submit").click(() => {
        createBusIntList("#user-edit-form");
    });
})