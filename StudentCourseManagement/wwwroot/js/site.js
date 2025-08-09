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

$(document).ready(function () {
    $('#courseTable').DataTable({
        dom: 'Bfrtip', // B = buttons, f = filter, r = processing, t = table, i = info, p = pagination
        buttons: [
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: ':not(.no-export)'
                },
                text: '<i class="fas fa-file-excel"></i> Export Excel',
                className: 'btn btn-success btn-sm',
                init: function (api, node, config) {
                    $(node).removeClass('dt-button');
                },
            },
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: ':not(.no-export)'
                },
                text: '<i class="fas fa-file-pdf"></i> Export PDF',
                className: 'btn btn-danger btn-sm',
                init: function (api, node, config) {
                    $(node).removeClass('dt-button');
                },
            }
        ],
        "processing": true,
        "serverSide": true,
        "ajax": {
            url: "/Course/GetList",
            type: "POST",
            dataSrc: function (json) {
                return json.data; // <- Make sure this matches the shape of your JSON
            }
        },
        "columns": [
            { "data": "courseCode" },
            { "data": "courseTitle" },
            {
                data: 'id', // assuming each row has a unique ID
                width: '20%',
                render: function (data, type, row) {
                    return `
                        <div class="d-flex justify-content-center">
                            <a href="/Course/Edit/${data}" class="btn btn-sm btn-primary mr-2" data-toggle="tooltip" title="Edit">
                                <i class="fa-solid fa-pen-to-square"></i>
                            </a>
                            <a onclick="deleteCourse(${data})" class="btn btn-sm btn-danger" data-toggle="tooltip" title="Delete">
                                <i class="fa-solid fa-trash"></i>
                            </a>
                        </div>
                    `;
                },
                orderable: false,
                searchable: false
            }
        ]
    });
});
function deleteCourse(id) {
    if (confirm("Are you sure you want to delete this course?")) {
        $.ajax({
            url: `/Course/Delete/${id}`,
            type: 'GET',
            success: function (result) {
                alert('Success deleting course.');
                $('#courseTable').DataTable().ajax.reload(); // Refresh table
            },
            error: function () {
                alert('Error deleting course.');
            }
        });
    }
}

