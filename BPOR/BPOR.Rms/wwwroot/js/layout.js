document.querySelector('.settings-cog').addEventListener('click', function () {
  var dropdown = document.querySelector('.settings-dropdown');
  dropdown.style.display = dropdown.style.display === 'none' ? 'block' : 'none';
});

window.onclick = function (event) {
  if (!event.target.matches('.fas.fa-cog')) {
    var dropdowns = document.getElementsByClassName("settings-dropdown");
    for (var i = 0; i < dropdowns.length; i++) {
      var openDropdown = dropdowns[i];
      if (openDropdown.style.display === "block") {
        openDropdown.style.display = "none";
      }
    }
  }
};

function showContainer() {
  const container = document.querySelector('.progress-bar-container');
  container.style.display = 'block';
}

// Function to add 10% to the width of the progress bar
function increaseProgressBarWidth() {
  const progressBar = document.getElementById('progress-bar');
  const currentWidth = parseFloat(progressBar.style.width);
  const newWidth = currentWidth + 10;
  progressBar.style.width = newWidth > 100 ? "100%" : `${newWidth}%`;
}

document.addEventListener("DOMContentLoaded", function () {
  const addStudyBtn = document.getElementById('addStudyBtn');

  if (addStudyBtn) {
    addStudyBtn.addEventListener('click', function (event) {
      event.preventDefault();
      showContainer();
      window.location.href = this.href;
    });
  }
});

function debounce(func, wait, immediate) {
  let timeout;
  return function() {
    let context = this, args = arguments;
    let later = function() {
      timeout = null;
      if (!immediate) func.apply(context, args);
    };
    let callNow = immediate && !timeout;
    clearTimeout(timeout);
    timeout = setTimeout(later, wait);
    if (callNow) func.apply(context, args);
  };
};

function searchStudies(query) {
  // Here you would typically make an AJAX call to your server with the query,
  // e.g., using fetch or jQuery.ajax to a specific endpoint that returns the filtered studies.
  console.log("Searching for:", query);
  // For demonstration, this just logs to the console.
}

// Attach the debounced event listener to the search input
document.getElementById('search-bar').addEventListener('input', debounce(function(event) {
  searchStudies(event.target.value);
}, 250)); // Adjust the delay (250ms) as needed
