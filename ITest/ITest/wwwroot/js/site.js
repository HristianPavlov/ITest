
﻿$(document).ready(function () {
    const activeClass = 'active';
    const firstItem = document.getElementById('categoryButton-0');

    if (firstItem) {
        firstItem.classList.add(activeClass);
        $('#startButton').text(firstItem.textContent);

        let activeCategory = firstItem;

        $('.categoryButton').on('click', function () {
            activeCategory.classList.remove(activeClass);
            activeCategory = document.getElementById(this.id);
            activeCategory.classList.add(activeClass);
            $('#startButton').text(this.textContent);
        });
    }
});

var questionTemplate =
    `<div class="panel panel-default question" name="Questions{{q_id}}">
    
        <div>
            <h3>Question{{q_id}}</h3>
            <button type="button" class="delete-button">Delete</button>
        </div>
        <div class="form-group">
        <input id="Questions[{{q_id}}].Content" placeholder="Title" class="form-control" />
            
        </div>

        <div class="answer-container">
        <div class="form-group col-lg-offset-1">
            <input type="text"     id="Questions[{{q_id}}].Answers[0].Content" placeholder="Answer1" class="form-control" />
            <input type="checkbox" id="Questions[{{q_id}}].Answers[0].Correct" class="form-control" />
        </div>
        <button type="button" class="add-answer">Add Answer</button>

        </div>
        
    </div>`;

var answerTemplate =

    `<div class="answer-container">
        <div class="form-group col-lg-offset-1">
            <input type="text"     id="Questions[{{q_id}}].Answers[{{a_id}}].Content" placeholder="Answer{{ap_id}}" class="form-control" />
            <input type="checkbox" id="Questions[{{q_id}}].Answers[{{a_id}}].Correct" class="form-control" />
        </div>

        </div>`;

$(function () {
    var total = 0;

    $('.add-question').click(function () {
        $("#questions-container").append(questionTemplate.replace(/\{\{\q_id\}\}/g, ++total));
    });

    $('#questions-container').on('click', '.add-answer', function () {

        var containerr = this.closest('.panel.panel-default.question');
        var containerrr = $(this).closest('.panel.panel-default.question');

        var arr = containerrr.find(`div input`);

        var x = String(arr[0].id).match(/Questions\[(\d+)\]/);
        if (x !== null) {
           // console.log(x[1]);
        } else {
            x = String(arr[0].id).match(/Questions\_(\d+)\__/);
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

        var x = $(this).closest(`.panel.panel-default.question`).attr(`name`).replace(`Questions`,``);
               
        var number = parseInt(x);
        
        console.log(number);    
                 
               
        $(this).closest('.panel.panel-default.question').remove();


        

        //var listOfPageElements = document.querySelectorAll(".panel.panel-default.question div h3");


        $(document).find(`input`).each(function () {

            var x = String(this.id).match(/Questions\[(\d+)\]/);
            if (x !== null) {
                var xNumber = parseInt(x[1]);
                if (xNumber > number) {
                    var ss = this.id.replace(/Questions\[(\d+)\]/g, `Questions[` + (xNumber - 1) + `]`);
                    this.id = ss;
                    this.closest('.panel.panel-default.question').setAttribute("name", "Questions" + (xNumber - 1));

                    var heading = $(this).closest('.panel.panel-default.question');
                    //var headd = heading.find(`div h3`);
                    var y = heading.find(`div h3`);
                        //find(`div h3`).text();
                   // console.log(y[0]);
                    y.text("Questions" + (xNumber - 1));
                    


                }
            }



            //console.log(this.id);
            
            
        });

        --total;

     




    });

    $('#question-form').on('submit', function (event) {
        event.preventDefault();

        var data = $(this).serializeArray();
        console.log(data);
    });
});