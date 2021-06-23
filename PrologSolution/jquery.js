var _confirmCallback = null;
var _cancelCallback = null;
var _submitForm = null;
$(function () {
    var placeholderElement = $("#modal-placeholder");

    $('*[data-toggle="ajax-modal"]').click(ajaxModalOnClick);

    placeholderElement.on("click", '[data-save="modal"]', function (event) {
        event.preventDefault();

        if (_submitForm) {
            var form = $(this).parents(".modal").find("form");
            var actionUrl = form.attr("action");
            var dataToSend = form.serialize();
            $.post(actionUrl, dataToSend).done(function (data) {
                var newBody = $(".modal-body", data);
                placeholderElement.find(".modal-body").replaceWith(newBody);

                var isValid = newBody.find('[name="IsValid"]').val() == "True";
                if (!isValid) return
                
                evaluateCallback(_confirmCallback);
                removeCallbacks();
                hideModal();
            });
        } else {
            evaluateCallback(_confirmCallback);
            hideModal();
            removeCallbacks();
        }
    });

    placeholderElement.on("click", '[data-cancel="modal"]', function (event) {
        event.preventDefault();
        evaluateCallback(_cancelCallback);
        removeCallbacks();
        hideModal();
    });
    
    placeholderElement.on("click", '[data-navigate="modal"]', function (event) {
        event.preventDefault();
        var callback = $(this).data("success-callback");
        var url = $(this).attr("href");
        $.get(url).done(function (data) {
            var newBody = $(".modal-body", data);
            placeholderElement.find(".modal-body").replaceWith(newBody);
        });
        evaluateCallback(callback);
    });

    function evaluateCallback(callback) {
        if (callback == null) return;
        if (typeof callback === "string") {
            if (callback.includes('(')) eval(callback);
            else {
                var obj = {};
                $('[id*="-form-input"]').each(function () {
                    let formItem = $(this);
                    obj[formItem[0].id.split('-')[0]] = formItem.val()
                })
                var func = window[callback];
                func(obj);
            }
        }
        if (typeof callback === "function") {
            callback();
        }
    }

    function removeCallbacks() {
        _confirmCallback = null;
        _cancelCallback = null;
    }

    function hideModal() {
        placeholderElement.find(".modal").modal("hide");
    }
});

function ajaxModalOnClick(event) {
    event.preventDefault();
    var url = this.href || $(this).data("url");
    var successCallback = $(this).data("success-callback");
    var cancelCallback = $(this).data("cancel-callback");
    showAjaxModal(url, "GET", null, true, successCallback, cancelCallback);
}

function showAjaxModal(url, httpMethod, data, submitForm, confirmCallback, cancelCallback) {
    if (url == undefined) return;
    _confirmCallback = confirmCallback;
    _cancelCallback = cancelCallback;
    _submitForm = submitForm;
    $.ajax({
        url: url,
        method: httpMethod,
        data: data,
        success: function (data) {
            data = data.replaceAll("\\u003c", "<");
            data = data.replaceAll("\\u003e", ">");
            data = data.replaceAll("\\u0022", "\"");
            hideModal();
            $("#modal-placeholder").html(data);
            $("#modal-placeholder > .modal").modal("show");
        },
    })
}

function hideModal() {
    $("#modal-placeholder > .modal").modal("hide");
}
