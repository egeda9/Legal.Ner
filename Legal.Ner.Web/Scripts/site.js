$(function() {
    // This will make every element with the class "date-picker" into a DatePicker element
    $(".date-picker").datepicker();
});

$(document).ready(function () {
    $('#table_result').DataTable({
        paging: false,
        searching: false,
        info: false,
        lengthChange: false,
        processing: false,
        deferRender: false,
        "language": {
            "emptyTable": "Datos disponibles de la búsqueda"
        }
    });
});