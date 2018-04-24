$(document).ready(function () {
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