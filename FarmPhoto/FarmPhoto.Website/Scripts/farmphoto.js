
var FarmPhoto = {};

FarmPhoto.Effects = {};

FarmPhoto.Effects.Fading = function () {
    return {
        Messaging: function () {
            $(".alertPanel").slideUp("slow", function () {
                $(".alertPanel").remove();
            });
        }
    };
};

FarmPhoto.Dialogs = {};

FarmPhoto.Dialogs.Messaging = function() {
    return {
        showAdded: function() {
            this.show("Added", "message");
        },
        showMessageWithState: function(message, state) {
            this.show(message, state);
        },
        showSuccessMessage: function(message) {
            this.show(message, "message");
        },
        showMessage: function(message) {
            this.show(message, "error");
        },
        showErrorSummary: function() {
            this.show("Sorry we couldn't do that. Please try again. ", "error");
        },
        show: function (message, type) {
            setTimeout(function () { new FarmPhoto.Effects.Fading().Messaging(); }, 5000);
            
            $('#alertArea').append("<div class=\"alertPanel\"><div class=\"farmphotoalert " + type + "\"><div><span>" + message + "</span></div><a class=\"alertClose\">Close</a></div></div>");
        }
    };
};

$(document).ready(function() {
    $('.alertClose').on('click', function () {
        new FarmPhoto.Effects.Fading().Messaging();
    });
});