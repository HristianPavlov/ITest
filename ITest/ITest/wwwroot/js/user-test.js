$(function () {
    $('#start-Button').on('click', function (event) {
        event.preventDefault();
        var category = $('#startButton').text().replace(/\s/g, '');
        var url = '/Solve/GenerateTest/' + category;
        console.log(url);

        $.ajax({
            type: "GET",
            url: url,
            success: function (data) {
                window.location.href = data;
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr);
                alert(xhr.status);
                alert(thrownError);
            }
        });
    });
});