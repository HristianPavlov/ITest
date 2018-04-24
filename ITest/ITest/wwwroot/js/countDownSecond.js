var clock;

//var minutes = 5;
//var time = minutes * 60;
//var currentTime;

//delete this shit
//var currentTime = $('currentTime');
//var endTime = $('endTime');


var time = parseInt($('#secondsYouHave').html());
//to prevent user from reloading - maybe not gonna be used
//window.onbeforeunload = function (event) {
//    alert("EHOOOOOOOoo")
//    return confirm("Confirm refresh");
//};
$(document).ready(function () {
    clock = $('.clock').FlipClock(time,
        {
           
            clockFace: 'MinuteCounter',
            countdown: true,
            callbacks: {
                stop: function () {
                    $('#submitButton').trigger('click');
                }
            }
        });
})

