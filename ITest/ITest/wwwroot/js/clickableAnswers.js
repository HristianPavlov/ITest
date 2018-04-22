$(document).ready(function () {
    const activeClass = 'active';
    //const firstItem = document.getElementById('categoryButton-0');
    //firstItem.classList.add(activeClass);
    //$('#startButton').text(firstItem.textContent);
    //let currentQuestion;
    let currentAnswer;
    let currentClass;

    let testId = 11;
    $('.label-text').on('click', function () {

        currentAnswer = document.getElementById(this.id);
        currentClass = currentAnswer.className.split(' ')[1];
         $(`.${currentClass}`).removeClass(activeClass);
        currentAnswer.classList.add(activeClass);
    });
});