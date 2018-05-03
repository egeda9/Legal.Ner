$("#ddlSource").on("change",
    function () {
        $("#hdnSourceLabel").val($(this).find("option:selected").text());
    });

$("#ddlTarget").on("change",
    function() {
        $("#hdnTargetLabel").val($(this).find("option:selected").text());
    });