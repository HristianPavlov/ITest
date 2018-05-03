$(document).ready(function () {
    var qwerty = $('input:radio');

    for (var i = 0; i < qwerty.length; i++) {
        var theName = qwerty[i].getAttribute(`id`);
        var parts = theName.split("__Answers_");

        var NumberOfTheAnswerInThisQuestionArr = parts[1].split("__");
        var NumberOfTheAnswerInThisQuestion = NumberOfTheAnswerInThisQuestionArr[0];

        var IdOfTheQuestionArr = parts[0].split("_");
        var IdOfTheQuestion = IdOfTheQuestionArr[1];
        //"Questions[{{q_id}}].Answers[{{a_id}}].Correct"
        //.replace(/{{a_id}}/, NumberOfTheAnswerInThisQuestion)
        var newNameForTheInput = "radio_A_{{q_id}}"
            .replace(/{{q_id}}/, IdOfTheQuestion);

        qwerty[i].setAttribute("name", newNameForTheInput);

    }
    $(document).on('click', '#SaveButton', function () {
        //event.preventDefault();
        var qwerty = $('input:radio');
        for (var i = 0; i < qwerty.length; i++) {
            var theName = qwerty[i].getAttribute(`id`);
            var parts = theName.split("__Answers_");

            var NumberOfTheAnswerInThisQuestionArr = parts[1].split("__");
            var NumberOfTheAnswerInThisQuestion = NumberOfTheAnswerInThisQuestionArr[0];

            var IdOfTheQuestionArr = parts[0].split("_");
            var IdOfTheQuestion = IdOfTheQuestionArr[1];
            var newNameForTheInput = "Questions[{{q_id}}].Answers[{{a_id}}].Correct"
                .replace(/{{q_id}}/, IdOfTheQuestion)
                .replace(/{{a_id}}/, NumberOfTheAnswerInThisQuestion);

            qwerty[i].setAttribute("name", newNameForTheInput);
        }
    });




   
});

