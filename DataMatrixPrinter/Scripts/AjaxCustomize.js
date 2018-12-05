function AjaxCustomize(data, urlStr, typeStr) {
    $("#ErrorAlert").hide();
    $("#InfoAlert").hide();
    $("#WarningAlert").hide();
    $("#SuccessAlert").hide();
    $("#loadingGid").show();
    $.ajax({
        type: typeStr,
        url:urlStr,
        data: JSON.stringify(data),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data.isError == true) {
                showError(data.Errors.Message, data.Errors.EnumElements, data.Errors.pEnumElements);
            } else {
                showError(data.Errors.Message, data.Errors.EnumElements, data.Errors.pEnumElements);
            }
            $("#loadingGid").hide();
        },
        error: function (data) {
            console.log(data);
            var object = $.parseJSON(data.responseText);
            var errorMsg = object.ExceptionMessage.toString();
            showError(errorMsg,ErrorAlert, pErrorAlert);
            $("#loadingGid").hide();
        }
    });
}