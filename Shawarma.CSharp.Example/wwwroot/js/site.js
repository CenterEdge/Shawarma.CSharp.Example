// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function() {
    function sendRequest(active) {
        $.ajax({
            method: "POST",
            url: "/applicationstate",
            dataType: "json",
            data: JSON.stringify({
                status: active ? "active" : "inactive"
            }),
            contentType: "application/json",
            success: function(data, status) {
                $("#response").text(JSON.stringify(data));
            },
            error: function(xhr, status) {
                $("#response").text(status);
            }
        });
    }

    $("#enable").click(function() {
        sendRequest(true);
    });

    $("#disable").click(function() {
        sendRequest(false);
    });
});
