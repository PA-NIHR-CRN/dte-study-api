function toggleNavigation() {
  const iconContainer = document.querySelector('.icon-container');
  const sideNav = document.querySelector('#nihr-side-nav');
  iconContainer.classList.toggle('clicked');
  sideNav.classList.toggle('show');
}
