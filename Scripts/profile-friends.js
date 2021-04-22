$('#updateDiv').on("click", ".deleteFriend", delete_friend);



function UpdateFriendPage() {

    var serviceUrl = "/Profile/MyFriends/";
    var request = $.post(serviceUrl);
    request.done(function (data) {
        $("#updateDiv").html(data);
    }).fail(function () {
        console.log("does not complete");
    })
};


$('#updateDiv').on("click", ".favoriteTag", favorite_friend);

function favorite_friend() {
    var id = this.getAttribute("data-post-id");

    $.ajax({
        type: "PUT",
        url: "/Profile/FavoriteTag/" + id,
        contentType: "application/json;charset=UTF-8",
        success: function (data) {
            UpdateFriendPage();

        }, error: function () {
            alert("error");
        }
    })
}
