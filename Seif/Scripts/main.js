$(document).ready(function () {

    $('.funShow').fancybox({
        padding: 0,
        openEffect: "elastic",
        openSpeed: 150,
        closeEffect: "elastic",
        closeSpeed: 150,
        closeClick: true,
        helpers: {
            overlay: {
                css: {
                    "background": "rgba(238,238,238,0.85)"
                }
            }
        }
    });
});