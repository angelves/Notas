// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function JsonWorker(json, key)
{
    json = json.replace("{", "");
    json = json.replace("}", "");
    json = json.replace(/["']/g, "");

    var js = json.split(',');

    var r = "";

    js.forEach(function (i) {

        var pair = i.split(':');

        pair[0] = pair[0].replace(/\s/g, '')

        if (pair[0] == key) { r = pair[1]; }

    });

    return r;
}