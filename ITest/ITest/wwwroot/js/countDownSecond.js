var clock;
var time = parseInt($('#secondsYouHave').html());
$(document).ready(function () {
    clock = $('.clock').FlipClock(time,
        {
            clockFace: 'HourlyCounter',
            countdown: true,
            callbacks: {
                stop: function () {
                    $('#submitButton').trigger('click');
                }
            }
        });
})

