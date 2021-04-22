$(document).ready(function () {

    function CreatePost() {
        jQuery.support.cors = true;
        var post = {
            Text = $('.postText').val(),

        }
    }


    $.ajax({
        url: "http://localhost:49493/api/Values",
        type: "Post",
        data: JSON.stringify([name, address, dob]), //{ Name: name, 
        // Address: address, DOB: dob },
        contentType: 'application/json; charset=utf-8',
        success: function (data) { },
        error: function () { alert('error'); }
    });
});
});     