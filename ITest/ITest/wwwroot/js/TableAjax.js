$(function () {
    
    var deleteTestSubmitEvent = $('#table_id').on('submit', '.delete-test', function (event) {
        event.preventDefault();

        var table = $(this).closest('.table').DataTable();
        console.log(table);
        var testName = $(this).children('#testName').val();
        

        var testRow = $(this).closest('tr');

        var url = this.action;
        console.log(url);
        var data = $(this).serialize();
        console.log(data);
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            success: function (isDeleted) {
                if (isDeleted === true) {
                    table
                        .row(testRow)
                        .remove()
                        .draw();
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr);
                alert(xhr.status);
                alert(thrownError);
            }
        });
    });

    
});