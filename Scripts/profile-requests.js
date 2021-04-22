$('#updateDiv').on("click", ".acceptRequest", accept_request)
$('#updateDiv').on("click", ".declineRequest", decline_request)

$('.requestDiv').on("click", ".acceptRequest", accept_request)
$('.requestDiv').on("click", ".declineRequest", decline_request)

function accept_request() {

    var requestId = this.getAttribute("data-post-id");

    $.ajax({
        type: "POST",
        url: "/Profile/AcceptRequest/" + requestId,
        contentType: "application/json;charset=UTF-8",
        success: function (data) {
            UpdateRequestPage();
            UpdateRequestDiv();
            see_if_requests_exists();
            $.notify("A friend has been added!", {
                className: "success"
            });

        }, error: function () {
            alert("error");
        }
    });
};

function decline_request() {
    var requestId = this.getAttribute("data-post-id");

    $.ajax({
        type: "DELETE",
        url: "/Profile/DeclineRequest/" + requestId,
        contentType: "application/json;charset=UTF-8",
        success: function (data) {
            UpdateRequestPage();
            UpdateRequestDiv();
            $.notify("You have declined a request", {
                className: "info"
            });

        }, error: function () {
            alert("error");
        }
    });
}

function UpdateRequestPage() {

    var serviceUrl = "/Profile/MyFriendRequests/";
    var request = $.post(serviceUrl);
    request.done(function (data) {
        $("#updateDiv").html(data);
    }).fail(function () {
        console.log("does not complete");
    })
};

function UpdateRequestDiv() {
    $.ajax({
        type: "GET",
        url: "/Profile/MyFriendRequests/",
        contentType: "application/json;charset=UTF-8",
        dataType: "html",
        success: function (data) {

            $(".requestDiv").html(data);

        },
        error: function (data) {
            alert("error");
        }
    });
}

