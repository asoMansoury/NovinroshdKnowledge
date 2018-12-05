function showError(msg, element, pElement) {
    $("#mainError").show();
    $(element).show();
    $(pElement).text("");
    $(pElement).text(msg);
}