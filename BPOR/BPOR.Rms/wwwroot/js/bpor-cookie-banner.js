(function (base) {
    const cookieDomain = globalThis.cookieDomain;
    const originalReject = base.rejectCookie;
    const originalAccept = base.acceptCookie;
    const originalConfirmation = base.confirmation;

    const extended = {
        acceptCookie: function () {
            originalAccept();
            Cookies.set("cookiesAccepted", "true-" + "20221221", { expires: 180, secure: true, domain: cookieDomain });
            Cookies.set("BPoRcookie", "BePartOfResearch");
        },

        rejectCookie: function () {
            originalReject();
            Cookies.set("cookiesAccepted", "false-" + "20221221", { expires: 180, secure: true, domain: cookieDomain });
            Cookies.set("BPoRcookie", "BePartOfResearch");

            for (const cookie in Cookies.get()) {
                if (cookie.includes("_g") || cookie.includes("_hj") || cookie.includes("_fbp")) {
                    Cookies.remove(cookie, { path: '/', domain: '.nihr.ac.uk' });
                }
            }
        },

        confirmation: function (decision) {
            originalConfirmation(decision);
            const template = document.getElementById("confirmationMessageTemplate");
            const clone = template.content.cloneNode(true);
            clone.querySelector(".user-decision").innerText = decision;
            const target = document.querySelector(".govuk-cookie-banner__message");
            target.replaceChildren(clone);

            clone.querySelectorAll(".govuk-button.cookie-btn").forEach(element => {
                element.addEventListener("keydown", event => {
                    if (event.which === 13) {
                        base.closeCookieBanner();
                    }
                });
            });
        }
    };

    Object.assign(base, extended);
})(window.CookieBanner);