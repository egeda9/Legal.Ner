$(function() {
    // This will make every element with the class "date-picker" into a DatePicker element
    $('.date-picker').datepicker();
});

$("#ddlSource").on("change", function () {
    $("#hdnSourceLabel").val($(this).find("option:selected").text());
});

$("#ddlTarget").on("change", function () {
    $("#hdnTargetLabel").val($(this).find("option:selected").text());
});