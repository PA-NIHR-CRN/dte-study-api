
window.CookieBanner = {
    init: function () {
        document.addEventListener("DOMContentLoaded", function () {
            const buttons = document.querySelectorAll(".govuk-button.cookie-btn");

            buttons.forEach(element => {
                element.addEventListener("click", function () {
                    if (element.value.includes("accept")) {
                        CookieBanner.acceptCookie();
                    } else {
                        CookieBanner.rejectCookie();
                    }
                });
            });

            if (Cookies.get("cookiesAccepted")) {
                CookieBanner.closeCookieBanner();
            }
        });


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
        document.querySelector("#cookies")?.classList.add("d-none");
    }
};

// Initialize
CookieBanner.init();