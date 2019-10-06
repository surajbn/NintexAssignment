// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {
    var LoadOperationsInstance = new LoadOperations();

    ko.applyBindings(LoadOperationsInstance);
});

function LoadOperations() {
    self = this;
    self.OriginalUrl = ko.observable();
    self.EncodedUrl = ko.observable();
    self.Id = ko.observable();
    self.UrlEncoded = ko.observable(false);    

    $('#convert-url').click(function () {
        let originalUrl = $('#original-url').val();
        $(this).attr("disabled", "disabled");
        $.ajax({
            url: Nintex.TinyUrl.Create,
            type: 'POST',
            data: { 'OriginalUrl': originalUrl },
            success: function (data, status, xhr) {
                console.log(data.originalUrl);

                self.OriginalUrl(data.originalUrl);
                self.EncodedUrl(data.encodedUrl);
                self.Id(data.id);
                self.UrlEncoded(true);
                //alert(location.protocol + "//" + location.host + '/'+data);
            },
            error: function (jqXHR) {
                alert('Failed to Create URL: ' + jqXHR.status + ': ' + jqXHR.statusText);
            },
            complete: function () {
                $('#convert-url').removeAttr("disabled", "disabled");
            }
        })
    });
}

