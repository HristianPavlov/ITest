$(function () {
    const activeClass = 'active';
    let currentAnswer;
    let currentClass;
    $('.label-text').on('click', function () {

        currentAnswer = document.getElementById(this.id);
        currentClass = currentAnswer.className.split(' ')[1];
        $(`.${currentClass}`).removeClass(activeClass);
        $(`#Answer-${currentClass}`)[0].textContent = currentAnswer.textContent;
        currentAnswer.classList.add(activeClass);
    });
}); 