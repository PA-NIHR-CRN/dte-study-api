document.onreadystatechange = function () {
    if (document.readyState == "interactive") {
        initialiseOptionSearch();
    }
}

function initialiseOptionSearch() {
    Array.from(document.getElementsByClassName('nihr-option-search')).forEach(function (search) {
        search.classList.remove("js-only");
        search.style.display = 'unset';

        search.addEventListener('input', function () {
            var searchText = this.value.toLowerCase();
            var optionClass = this.dataset.optionClass;
            var checkboxes = document.querySelectorAll('.' + optionClass);

            checkboxes.forEach(function (checkbox) {
                var label = checkbox.dataset.searchValue;
                if (label.includes(searchText)) {
                    checkbox.style.display = 'flex';
                } else {
                    checkbox.style.display = 'none';
                }
            });
        });
    });
}