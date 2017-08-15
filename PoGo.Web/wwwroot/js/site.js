$(function () {
    $('[data-toggle="tooltip"]').tooltip()

    setTimeout(() => {
        if (typeof window.google_jobrunner === 'undefined')
            $("#adblockAlert").removeAttr("hidden")
    }, 2000)
})