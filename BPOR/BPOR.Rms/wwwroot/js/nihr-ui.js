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
            var options = document.querySelectorAll('.' + optionClass);
            var announcementElement = document.getElementById(search.dataset.announcementId);

            var foundCount = 0;
            var selectedCount = 0;

            options.forEach(function (option) {
                var label = option.dataset.searchValue;
                var checkbox = option.querySelector('input[type="checkbox"]');

                option.style.display = 'none';

                if (checkbox.checked) {
                    option.style.display = 'flex';
                    selectedCount++;
                }

                if (label.includes(searchText)) {
                    option.style.display = 'flex';
                    foundCount++;
                }
            });

            document.querySelectorAll('.govuk-checkboxes__item.skip-link').forEach(function (item) {
                item.style.display = searchText ? 'none' : 'flex';
            });

            var foundLabel = foundCount == 1 ? announcementElement.dataset.single : announcementElement.dataset.multiple;

            announcementElement.innerText = `${foundCount} ${foundLabel}, ${selectedCount} ${announcementElement.dataset.selected}`;
        });
    });
}