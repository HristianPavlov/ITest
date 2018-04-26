$(document).ready(function () {
    let currentAnswer;
    let currentClass;
    $('.label-text').on('click', function () {
        currentAnswer = document.getElementById(this.id);
        $(`#Answer-${currentClass}`)[0].textContent = currentAnswer.textContent;
        currentAnswer.classList.add(activeClass);
    });
});