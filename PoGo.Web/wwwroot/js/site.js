$(function () {
    $('[data-toggle="tooltip"]').tooltip()

    if (typeof window.google_jobrunner === 'undefined')
        $("#adblockAlert").removeAttr("hidden")
})