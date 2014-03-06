$(document).ready(function () {
    $(".vote").click(onVoteButtonClick);
});

function onVoteButtonClick() {
    var id = $(this).data("id");
    var pollId = $(this).data("pollid");
    var page = $(this).data("page");
    var url = $(this).data("url");
    $.ajax({
        url: url + "/Poll/CreateVote",
        type: 'POST',
        data: { 'id': id, 'pollid': pollId, 'page': page },
        success: function (result) {
            if (result.showDialog) {
                showDialog();
            } else {
                window.location = result.url;
            }
        },
        error: function () {
            alert('error');
        }
    });
}

function showDialog() {
    $("#dialog").dialog({
        resizable: false,
        height: 240,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog("close");
            }
        }
    });
}