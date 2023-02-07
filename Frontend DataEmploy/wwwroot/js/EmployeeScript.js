let URLBackend = "https://localhost:7116/api/Employees"

$(document).ready(function () {
    const days = ["Minggu", "Senin", "Selasa", "Rabu", "Kamis", "Jumat", "Sabtu"];
    const months = ["Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "November", "Desember"];
    $('#tbEmployee').DataTable({
        "ajax": {
            url: URLBackend,
            type: "GET",
            "dataType": "json",
            "dataSrc": "data",
            "order": [[4, 'desc']],
        },
        "columns": [
            {
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { "data": "nik" },
            { "data": "firstName" },
            { "data": "phone" },
            {
                "data": "birthDate",
                "render": function (data) {
                    localDay = new Date(data)
                    return `${days[localDay.getDay()]} ${localDay.getDate()} ${months[localDay.getMonth()]} ${localDay.getFullYear()}`;
                }
            },
            { "data": "salary" },
            { "data": "email" },
            {
                "data": "gender",
                "render": function (data) {
                    if (data == 1) {
                        return "Female";
                    }
                    return "Male";
                }
            },

            //{ "data": "departmentName" },
            //{ "data": "roleName" },
            
            {
                "className": "text-center",
                "render": function (data, type, row) {
                    return '<button class="btn btn-light " data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick=" select(); GetById(\'' + row.nik + '\')"><i class="fas fa-search"></i></button >' + '&nbsp;' +
                        '<button class="btn btn-light" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="ConfirmDelete(\'' + row.nik + '\')"><i class="fa fa-trash"></i></button >'
                }
            }
        ]
    });
})