﻿var SearchAndShortlist = SearchAndShortlist || {};
(function (analytics) {

    analytics.pushEvent = function (category, text, action) {
        if (typeof ga === "undefined") {
            return;
        }

        ga("send", "event", {
            "eventCategory": category,
            "eventAction": action || "Click",
            "eventLabel": text
        });
    };

    var trackShortlist = function(action, type) {
        var label = action + " " + type;
        analytics.pushEvent("Shortlist", label);
    };

    analytics.init = function () {

        $("#apprenticeship-results .result a:lt(3)").on("click", function () {
            analytics.pushEvent("Apprenticeship Search Results", "top3");
        });

        $("#apprenticeship-results .result a:gt(2)").on("click", function () {
            analytics.pushEvent("Apprenticeship Search Results", "results");
        });

        $("#start-button").on("click", function () {
            analytics.pushEvent("Start page", "Start button");
        });

        $(".provider-detail .data-list a[href^=mailto], .provider-detail .data-list a.course-link, .provider-detail .data-list a.contact-link").on("click", function () {
            analytics.pushEvent("Provider Details", "Contact link");
        });

        $(".provider-detail .data-list a[href^=mailto]").on("click", function () {
            analytics.pushEvent("Provider Details", "Email");
        });

        $(".provider-detail a.course-link").on("click", function () {
            analytics.pushEvent("Provider Details", "Website");
        });

        $(".provider-detail a.contact-link").on("click", function () {
            analytics.pushEvent("Provider Details", "Contact page");
        });

        // Shortlist

        $(".standard-shortlist-link").on('click', function () {
            trackShortlist($(this).attr('data-action'), "standard");
        });

        $(".provider-shortlist-link").on('click', function() {
            trackShortlist($(this).attr('data-action'), "provider");
        });

        $(".delete-link").on('click', function() {
            trackShortlist("remove", "apprenticeship");
        });

        $(".provider-delete-link").on('click', function () {
            trackShortlist("remove", "provider");
        });
    };

    if (typeof ga !== "undefined"){
        analytics.init();
    }

}(SearchAndShortlist.analytics = {}));