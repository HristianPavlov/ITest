
//name="Questions[{{q_id}}].Answers[{{a_id}}].Correct"
//name="Questions[{{q_id}}].Answers[{{a_id}}].Correct"
$(document).ready(function () {
    var questionTemplate =
        `
<div class="panel panel-default question" name="Questions{{q_id}}">
    
        <div>
            <h3>Question{{qh_id}}</h3>
            <button type="button" class="delete-button">Delete</button>
        </div>
        <div class="form-group">
        <input id="Questions_{{q_id}}__.Content" name="Questions[{{q_id}}].Content" placeholder="Title" class="form-control" />
            
        </div>

        <div class="answer-container">
        <button type="button" class="add-answer">Add Answer</button>
        <div class="form-group col-lg-offset-1">
            <input type="text"     id="Questions_{{q_id}}__.Answers_0__.Content"  name="Questions[{{q_id}}].Answers[0].Content" placeholder="Answer1" class="form-control" />
            <input type="radio" id="Questions_{{q_id}}__.Answers_0__.Correct"   name="radio_{{q_id}}" class="form-control" value="true" /> 

           
        </div>
       

        </div>
 
    </div>
  `;

    var answerTemplate =
        `
    <div style="width: 70%; height: 50px;" class="answer-container">
        <div class="form-group col-lg-offset-1">
            <input type="text"    id="Questions_{{q_id}}__.Answers_{{a_id}}__.Content" name="Questions[{{q_id}}].Answers[{{a_id}}].Content" placeholder="Answer{{ap_id}}" class="form-control" />
            <input type="radio" id="Questions_{{q_id}}__.Answers_{{a_id}}__.Correct"  name="radio_{{q_id}}" class="form-control" value="true"/>
            
        </div>

        </div>;
   `;
    var total = 0;

    $('.add-question').click(function () {

        $("#questions-container").append(questionTemplate.replace(/\{\{\q_id\}\}/g, ++total).replace(/\{\{qh_id\}\}/, total + 1));
    });

    //$('.radio\_(\d+)\__').change(function () {
    //    $('.radio').not(this).prop('checked', false);
    //});

    $('#questions-container').on('click', '.add-answer', function () {

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

    $('#questions-container').on('click', '.delete-button', function () {
        var x = $(this).closest(`.panel.panel-default.question`).attr(`name`).replace(`Questions`, ``);
        var number = parseInt(x);
        console.log(number);
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




    $('#question-form').on('click', '#submitNewTestBtn', function () {
        //event.preventDefault();
        var qwerty = $('input:radio');
        for (var i = 0; i < qwerty.length; i++) {
            var theName = qwerty[i].getAttribute(`id`);
            var parts = theName.split("__.Answers_");

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