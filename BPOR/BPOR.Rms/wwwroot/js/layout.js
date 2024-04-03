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
