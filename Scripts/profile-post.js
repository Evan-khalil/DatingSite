//$("#submitPost").click(add_post);

$('#updateDiv').on("click", "#submitPost", add_post)

function add_post() {

    post =
    {
        Text: $("#postText").val(),
        RecieverId: userId
    };
    //För att ta bort Overflow-hidden på modalen!
    if ($("body").hasClass("modal-open")) {
        $("body").toggleClass("modal-open");
    }

    $.ajax({
        type: "POST",
        url: "/api/AjaxApi/",
        data: JSON.stringify(post),
        contentType: "application/json;charset=UTF-8",
        success: function (data) {
            UpdateWallPage();

            $.notify("You have shared a post!", {
                className: "success"
            });

        }, error: function () {
            alert("error");
        }
    });
};

function UpdateWallPage() {

    var id = userId

    var serviceUrl = "/Profile/WallPage/" + id;
    var request = $.post(serviceUrl);
    request.done(function (data) {
        $("#updateDiv").html(data);
    }).fail(function () {
        console.log("does not complete");
    })
};

function UpdateWallPage_With_Parameter(id) {

    var serviceUrl = "/Profile/WallPage/" + id;
    var request = $.post(serviceUrl);
    request.done(function (data) {
        $("#updateDiv").html(data);
    }).fail(function () {
        console.log("does not complete");
    })
};

//$(".deleteBtn").click(delete_post);

$('#updateDiv').on("click", ".deleteBtn", delete_post)

function delete_post() {

    var postID = this.getAttribute("data-post-id");

    $('.modal-backdrop').remove();
    //För att ta bort Overflow-hidden på modalen!
    if ($("body").hasClass("modal-open")) {
        $("body").toggleClass("modal-open");
    }


    $.ajax({
        type: "DELETE",
        url: "/api/AjaxApi/" + postID,
        contentType: "application/json;charset=UTF-8",
        success: function (data) {
            UpdateWallPage();

            $.notify("You have removed a post!", {
                className: "success"
            });

        }, error: function () {
            alert("error");
        }
    });
};