$(function () {
    //{ { qh_id } }
    var questionTemplate =
        `
<div class="panel panel-default question" name="Questions{{q_id}}" style="margin: 7% 0% 7% 0%; border: none; shadow: none; background-color:transparent;">
    
        <div>
            <div style="display: inline-block; padding-left: 15px;">
<h3></h3>
</div>
            <button type="button" class="delete-button">Delete Question</button>
        </div>
        <div class="form-group">
        <input id="Questions_{{q_id}}__.Content" name="Questions[{{q_id}}].Content" placeholder="Question content" class="form-control" style="width: 70%; margin-left: 10px; display:inline-block;" />
        <button type="button" class="add-answer">Add Answer</button>
        </div>

        <div class="answer-container">

        <div style="height: 50px;" class="form-group col-lg-offset-1">
            <input type="text"     id="Questions_{{q_id}}__.Answers_0__.Content"  name="Questions[{{q_id}}].Answers[0].Content" placeholder="Answer1" class="form-control" style="width: 70%; margin-right: 15px;" />
            <input type="radio" id="Questions_{{q_id}}__.Answers_0__.Correct"   name="radio_{{q_id}}" class="form-control" value="true" style="box-shadow:none; border:none;"/> 

           
        </div>
       

        </div>
 
    </div>
  `;
    var answerTemplate =
        `
    <div style="height: 50px;" class="answer-container">
        <div class="form-group col-lg-offset-1">
            <input type="text"    id="Questions_{{q_id}}__.Answers_{{a_id}}__.Content" name="Questions[{{q_id}}].Answers[{{a_id}}].Content" placeholder="Answer{{ap_id}}" class="form-control" style="width: 70%; margin-right: 15px;" />
            <input type="radio" id="Questions_{{q_id}}__.Answers_{{a_id}}__.Correct"  name="radio_{{q_id}}" class="form-control" value="true" style="box-shadow:none; border:none;"/>
            
        </div>

        </div>
   `;

    var qwerty = $('input:radio');

    var oldNumber = $(`.panel.panel-default.question`).length;

    var total = $(`.panel.panel-default.question`).length;/**/



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

    $('#questions-container').on('click', '.add-question', function () {

        $("#questions-container-backup").append(questionTemplate.replace(/\{\{\q_id\}\}/g, total++));

    });

    $('#createTest-content').on('click', '.hide-question', function () {

        var x = $(this).closest(`.panel.panel-default.question`)/*.attr(`name`).replace(`Questions`, ``)*/;
        var button = x.find(`.checkbox`);

        var y = this.closest(`.panel.panel-default.question`);
        var ss = x.find(`.checkbox`);
        $(button).attr(`checked`, `checked`);
        //console.log(button);
        $(this).closest('.panel.panel-default.question').hide();

        //var number = parseInt(x);
        //console.log(number);
        //

        //$(document).find(`input`).each(function () {

        //    var x = String(this.id).match(/Questions\_(\d+)\__/);
        //    if (x !== null) {
        //        var xNumber = parseInt(x[1]);
        //        if (xNumber > number) {
        //            var ss = this.id.replace(/Questions\_(\d+)\__/g, `Questions_` + (xNumber - 1) + `__`);
        //            this.id = ss;
        //            this.closest('.panel.panel-default.question').setAttribute("name", "Questions" + (xNumber - 1));

        //            var heading = $(this).closest('.panel.panel-default.question');
        //            //var headd = heading.find(`div h3`);
        //            var y = heading.find(`div h3`);
        //            //find(`div h3`).text();
        //            // console.log(y[0]);
        //            y.text("Questions" + xNumber);
        //        }
        //    }
        //});
        //--total;
    });


    $('#createTest-content').on('click', '.delete-button', function () {

        var x = $(this).closest(`.panel.panel-default.question`).attr(`name`).replace(`Questions`, ``);
        var number = parseInt(x);
        //console.log(number);
        $(this).closest('.panel.panel-default.question').remove();
        $(document).find(`input`).each(function () {

            var x = String(this.id).match(/Questions\_(\d+)\__/);
            if (x !== null) {
                var xNumber = parseInt(x[1]);
                if (xNumber > number) {
                    var ss = this.id.replace(/Questions\_(\d+)\__/g, `Questions_` + (xNumber - 1) + `__`);
                    this.id = ss;
                    this.closest('.panel.panel-default.question').setAttribute("name", "Questions" + (xNumber - 1));

                    var heading = $(this).closest('.panel.panel-default.question');
                    //var headd = heading.find(`div h3`);
                    var y = heading.find(`div h3`);
                    //find(`div h3`).text();
                    // console.log(y[0]);
                    y.text("Questions" + xNumber);
                }
            }
        });
        --total;
    });

    $('#questions-container').off().on('click', '.add-answer', function () {

        var containerr = this.closest('.panel.panel-default.question');
        var containerrr = $(this).closest('.panel.panel-default.question');

        var arr = containerrr.find(`div input`);

        var x = String(arr[0].id).match(/Questions\_(\d+)\__/);
        if (x !== null) {
            // console.log(x[1]);
        } else {
            x = String(arr[0].id).match(/Questions\_(\d+)\__/);
            x = String(arr[1].id).match(/Questions\_(\d+)\__/);

            //console.log(x[1]);
        }
        var index = parseInt(x[1]);
        //console.log(arr[0].id);
        //var indexNumber = parseInt(index);
        var listOfPageElements = containerr.querySelectorAll(".answer-container");
        var count = listOfPageElements.length;
        var countPlaceHolder = count;

        $(this).closest('.panel.panel-default.question').append(answerTemplate
            .replace(/\{\{\a_id\}\}/g, count++)
            .replace(/\{\{\ap_id\}\}/g, ++countPlaceHolder)
            .replace(/\{\{\q_id\}\}/g, index)
        );

    });

    $(`#createTest-content`).on('click', '#submitBtnArea', function () {
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


/**//**/


