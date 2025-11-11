    const cookieDomain = globalThis.cookieDomain;
    
    document.addEventListener("DOMContentLoaded", function () {
        document.querySelectorAll(".btn-dark.cookie-btn").forEach(element => {
            element.addEventListener("keydown", event => {
                if (event.which == 13) {
                    if (event.target.getAttribute("value").includes("accepted")) {
                        acceptCookie()
                    } else {
                        rejectCookie()
                    }
                }
            });
        });
        if (Cookies.get("cookiesAccepted")) {
            closeCookieBanner();
        }
    });
    
    function acceptCookie() {
        Cookies.set("cookiesAccepted", "true-" + "20221221", { expires: 180, secure: true, domain: cookieDomain })
        Cookies.set("BPoRcookie", "BePartOfResearch")
        confirmation("accepted")
    }

    function rejectCookie() {
        Cookies.set("cookiesAccepted", "false-" + "20221221", { expires: 180, secure: true, domain: cookieDomain })
        Cookies.set("BPoRcookie", "BePartOfResearch")
        // remove existing cookies
        for (const cookie in Cookies.get()) {
            if (cookie.includes("_g") || cookie.includes("_hj") || cookie.includes("_fbp")) {
                Cookies.remove(cookie, { path: '/', domain: '.nihr.ac.uk' })
            }
        }
        confirmation("rejected")
    }
    
    function confirmation(userDecision) {
        const template = document.getElementById("confirmationMessageTemplate");
        const clone = template.content.cloneNode(true);
        clone.querySelector(".user-decision").innerText = userDecision;
        const target = document.querySelector(".govuk-cookie-banner__message");
        target.replaceChildren(clone);
        clone.querySelectorAll(".btn-dark.cookie-btn").forEach(element => {
            element.addEventListener("keydown", event => {
                if (event.which == 13) {
                    closeCookieBanner()
                }
            });
        });
    }
    
    function closeCookieBanner() {
        document.querySelector("#cookies").classList.add("d-none")
    }