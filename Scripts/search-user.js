//onkeyup function
function searchFunction(elem) {
    var id = $(elem).val();
    UpdateSearchResult(id);
}

//Updating the .searchResult div in Partial View Requests
function UpdateSearchResult(id) {

    var serviceUrl = "/Search/Result/" + id;
    var request = $.get(serviceUrl);
    request.done(function (data) {
        $(".searchResult").html(data);
    }).fail(function () {
        console.log("does not complete");
    })
};


