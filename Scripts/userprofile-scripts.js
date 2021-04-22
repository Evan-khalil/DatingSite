$(".userProfileRelationStatusContent").on("click", ".acceptRequestUserProfile", accept_request_userprofile)
$(".userProfileRelationStatusContent").on("click", ".declineRequestUserProfile", decline_request_userprofile)
$(".userProfileRelationStatusContent").on("click", ".sendRequestUserProfile", send_request_userprofile)


function accept_request_userprofile() {

    var requestId = this.getAttribute("data-post-id");
    var id = this.getAttribute("data-user-id");

    $.ajax({
        type: "POST",
        url: "/Profile/AcceptRequest/" + requestId,
        contentType: "application/json;charset=UTF-8",
        success: function (data) {
            UpdateUserProfileRelationStatusContent(id)
            UpdateRequestDiv();
            UpdateWallPage();
            $.notify("A friend has been added!", {
                className: "success"
            });

        }, error: function () {
            alert("error");
        }
    });
};

function decline_request_userprofile() {

    var requestId = this.getAttribute("data-post-id");
    var id = this.getAttribute("data-user-id");

    $.ajax({
        type: "DELETE",
        url: "/Profile/DeclineRequest/" + requestId,
        contentType: "application/json;charset=UTF-8",
        success: function (data) {
            UpdateUserProfileRelationStatusContent(id)
            UpdateRequestDiv();
            $.notify("You have declined a request", {
                className: "info"
            });

        }, error: function () {
            alert("error");
        }
    });
}



function send_request_userprofile() {

    var id = this.getAttribute("data-user-id");

    $.ajax({
        type: "POST",
        url: "/Profile/SendRequest/" + id,
        contentType: "application/json;charset=UTF-8",
        success: function (data) {
            UpdateUserProfileRelationStatusContent(id)
            $.notify("A request has been sent!", {
                className: "success"
            });

        }, error: function () {
            alert("error");
        }
    });
}



function UpdateUserProfileRelationStatusContent(id) {

    var serviceUrl = "/Profile/UserProfileRelationStatusToIdentity/" + id;
    var request = $.post(serviceUrl);
    request.done(function (data) {
        $(".userProfileRelationStatusContent").html(data);
    }).fail(function () {
        console.log("does not complete");
    })
}