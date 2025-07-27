// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $('#reservationdate').datetimepicker({
        format: 'DD MMMM YYYY', // e.g., 01 January 2025
    });

    $('#reservationdate').on('change.datetimepicker', function (e) {
        // Optional: log or do something with the formatted date
        console.log(e.date.format('DD MMMM YYYY')); // like "01 January 2025"
    });
});
$(document).ready(function () {
    $('.preventSign').on('keydown', function (e) {
        // Block "-", "+", "e", and "E"
        if ($.inArray(e.key, ['-', '+', 'e', 'E']) !== -1) {
            e.preventDefault();
        }
    });
});
$(function () {
    $('#testing').select2();
});

//$(function () {
//    const forms = $("#quickForm");
//    // Loop over them and prevent submission
//    Array.from(forms).forEach(form => {
//        form.addEventListener('submit', event => {
//            if (!form.checkValidity()) {
//                event.preventDefault()
//                event.stopPropagation()
//            }

//            form.classList.add('was-validated')
//        }, false)
//    })
//})
