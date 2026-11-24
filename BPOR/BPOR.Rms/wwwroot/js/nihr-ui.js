document.onreadystatechange = function () {
    if (document.readyState == "interactive") {
        javascriptIsEnabled();
        initialiseOptionSearch();
    }
}

function javascriptIsEnabled() {
    // Enable javascript only elements
    Array.from(document.getElementsByClassName('js-only')).forEach(function (item) {
        item.classList.remove('js-only');
    });
}

function initialiseOptionSearch() {
    Array.from(document.getElementsByClassName('nihr-option-search')).forEach(function (search) {
        search.addEventListener('input', function () {
            var searchText = this.value.toLowerCase();
            var optionClass = this.dataset.optionClass;
            var options = document.querySelectorAll('.' + optionClass);
            var announcementElement = document.getElementById(search.dataset.announcementId);

            var foundCount = 0;
            var selectedCount = 0;
            var visibleOptions = [];

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

                if (option.style.display !== 'none') {
                    visibleOptions.push(checkbox);
                }
            });

            document.querySelectorAll('.govuk-checkboxes__item.skip-link').forEach(function (item) {
                item.style.display = searchText ? 'none' : 'flex';
            });

            var fieldset = search.closest('fieldset');
            var endLink = fieldset.querySelector('.govuk-skip-link.end-of-options');
            var endFragment = visibleOptions.length ? `#${visibleOptions[visibleOptions.length - 1].id}` : `#${endLink.id}`;
            fieldset.querySelectorAll('.govuk-skip-link.start-of-options, .govuk-skip-link.options-list').forEach(function (link) {
                link.href = endFragment;
            });
            endLink.href = visibleOptions.length ? `#${visibleOptions[0].id}` : `#${fieldset.querySelector('.govuk-skip-link.start-of-options').id}`;

            var foundLabel = foundCount == 1 ? announcementElement.dataset.single : announcementElement.dataset.multiple;

            announcementElement.innerText = `${foundCount} ${foundLabel}, ${selectedCount} ${announcementElement.dataset.selected}`;
        });
    });
}