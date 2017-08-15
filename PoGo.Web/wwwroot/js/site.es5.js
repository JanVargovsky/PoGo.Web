'use strict';

$(function () {
    $('[data-toggle="tooltip"]').tooltip();

    setTimeout(function () {
        if (typeof window.google_jobrunner === 'undefined') $("#adblockAlert").removeAttr("hidden");
    }, 3000);
});

