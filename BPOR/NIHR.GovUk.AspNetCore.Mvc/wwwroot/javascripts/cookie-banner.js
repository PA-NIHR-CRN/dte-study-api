
window.CookieBanner = {
    init: function () {
        document.addEventListener("DOMContentLoaded", () => {
            const form = document.querySelector("form[action='/form-handler']");

            if (form) {
                form.addEventListener("submit", (event) => {
                    event.preventDefault();
                    const clickedButton = event.submitter?.value || "unknown";

                    if (clickedButton === "yes") {
                        CookieBanner.acceptCookie();
                    } else if (clickedButton === "no") {
                        CookieBanner.rejectCookie();
                    } else if (clickedButton === "hide") {
                        CookieBanner.closeCookieBanner();
                    }

                });
            }
        });

        if (Cookies.get("cookiesAccepted")) {
                CookieBanner.closeCookieBanner();
            }

        document.addEventListener("click", (e) => {
            if (e.target.classList.contains("hide-banner-btn")) {
                CookieBanner.closeCookieBanner();
            }
        });
    },

    acceptCookie: function () {
        CookieBanner.confirmation("accepted");
    },

    rejectCookie: function () {
        CookieBanner.confirmation("rejected");
    },

    confirmation: function (decision) {
        console.log(`Base confirmation: ${decision}`);
    },

    closeCookieBanner: function () {
        const banner = document.querySelector("#cookies");
        if (banner) {
            banner.setAttribute("hidden", "true");
        }
    }
};

CookieBanner.init();