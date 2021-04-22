
function showDivWithRequests() {
    document.getElementById("myDropdown").classList.toggle("show");

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


