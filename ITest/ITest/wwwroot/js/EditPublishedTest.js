$(document).ready(function () {
    //{ { qh_id } }
    var questionTemplate =
        `
<div class="panel panel-default question" name="Questions{{q_id}}" style="margin: 7% 0% 7% 0%; border: none; shadow: none; background-color:transparent;">
    
        <div>
            <div style="display: inline-block; padding-left: 15px;">

</div>
            <button type="button" class="delete-button">Delete Question</button>
        </div>
        <div class="form-group">
        <input id="Questions_{{q_id}}__.Content" name="Questions[{{q_id}}].Content" placeholder="Question content" class="form-control" style="width: 70%; margin-left: 10px; display:inline-block;" />
        <button type="button" class="add-answer">Add Answer</button>
        </div>

        <div class="answer-container">

        <div style="height: 50px;" class="form-group col-lg-offset-1">
            <input type="text"     id="Questions_{{q_id}}__Answers_0__Content"  name="Questions[{{q_id}}].Answers[0].Content" placeholder="Answer1" class="form-control" style="width: 70%; margin-right: 15px;" />
            <input type="radio" id="Questions_{{q_id}}__Answers_0__Correct"   name="radio_A_{{q_id}}" class="form-control" value="true" style="box-shadow:none; border:none;"/> 

           
        </div>
       

        </div>
 
    </div>
  `;
    var answerTemplate =
        `
    <div style="height: 50px;" class="answer-container">
        <div class="form-group col-lg-offset-1">
            <input type="text"    id="Questions_{{q_id}}__Answers_{{a_id}}__Content" name="Questions[{{q_id}}].Answers[{{a_id}}].Content" placeholder="Answer{{ap_id}}" class="form-control" style="width: 70%; margin-right: 15px;" />
            <input type="radio" id="Questions_{{q_id}}__Answers_{{a_id}}__Correct"  name="radio_A_{{q_id}}" class="form-control" value="true" style="box-shadow:none; border:none;"/>
            
        </div>

        </div>
   `;

    var qwerty = $('input:radio');

    var oldNumber = $(`.panel.panel-default.question`).length;

    var total = $(`.panel.panel-default.question`).length;/**/
    console.log(total);
    console.log(oldNumber);


    //$('.radio\_(\d+)\__').change(function () {
    //    $('.radio').not(this).prop('checked', false);
    //});

    //when the page loads
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

        //console.log(total);
    }


  

    $(`#submitBtnArea`).on('click', '.btn.btn-default.submit', function () {
        //event.preventDefault();
        var qwerty = $('input:radio');
        for (var j = 0; i < qwerty.length; i++) {
            console.log(qwerty[j].name);
            console.log(qwerty[j].id);

        }

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
        console.log(qwerty.attr.name);
    });

});


/**//**/


