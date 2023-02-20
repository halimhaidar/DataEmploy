let URLBackend = "https://localhost:7116/api/Employees"

$(document).ready(function () {
    const days = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
    const months = ["January", "February", "March", "April", "Mey", "Juny", "July", "Agustus", "September", "October", "November", "December"];
    $('#tbEmployee').DataTable({
        "pagingType": 'simple_numbers',
        "ajax": {
            url: URLBackend + "/All",
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
            {
                "data": "firstName",
                render: function (data, type, row) {
                    return row.firstName +' '+ row.lastName;
                }
            },
            { "data": "phone" },
            {
                "data": "birthDate",
                "render": function (data) {
                    localDay = new Date(data)
                    return `${days[localDay.getDay()]}, ${localDay.getDate()} ${months[localDay.getMonth()]} ${localDay.getFullYear()}`;
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
            { "data": "roleName"},
            { "data": "departmentName" },
            { "data": "manager_Name" },

            {
                "className": "text-center",
                "render": function (data, type, row) {
                    return '<button class="btn btn-light btn-sm " data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick=" GetById(\'' + row.nik + '\')"><i class="bi bi-pencil-fill"></i></button >' + '&nbsp;' +
                        '<button class="btn btn-light btn-sm" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="ConfirmDelete(\'' + row.nik + '\')"><i class="bi bi-trash3"></i></button >'
                }
            }
        ]
    });
})

function select() {
    debugger;
    $('#departmentInput').empty("");
    $('#departmentInput').append('<option value="">Choose Department</option>');
    $('#passwordInput').prop('disabled', false);
    $.ajax({
        type: 'GET',
        url: "https://localhost:7116/api/Departmens",
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (result) {
            var obj = result.data;
            obj.length;
            for (var i = 0; i < obj.length; i++) {
                $('#departmentInput').append('<option value="' + obj[i].id + '">' + obj[i].name + '</option>');
            }
        }
    });
};

function Save() {
    let validateForm = true
    if (
        $("#firstName").val() == "" ||
        $("#lastName").val() == "" ||
        $("#phoneInput").val() == "" ||
        $("#birthDateInput").val() == "" ||
        $("#salaryInput").val() == "" ||
        $("#genderInput").val() == "" ||
        $("#emailInput").val() == "" ||
        $("#passwordInput").val() == "" ||        
        $("#departmentInput").val() == "" ||        
        $("#managerInput").val() == ""            
    ) {
        swal({
            icon: 'error',
            title: 'Failed',
            text: "Please fill out all your data",
        })
        validateForm = false
    } else {
        if (!$("#emailInput").val().match(/^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)) {
            swal({
                icon: 'error',
                title: 'Failed',
                text: "Sorry, your email is not valid",
            })
            validateForm = false
        }
        if (!$("#phoneInput").val().match(/^\d*\d$/)) {
            swal({
                icon: 'error',
                title: 'Failed',
                text: "Sorry, your phone number is not valid",
            })
            validateForm = false
        }
    }

    if (validateForm) {
        var User = new Object();
        User.firstName = $('#firstNameInput').val();
        User.lastName = $('#lastNameInput').val();
        User.phone = $('#phoneInput').val();
        User.email = $('#emailInput').val();
        User.salary = parseInt($('#salaryInput').val());
        User.birthDate = $('#birthDateInput').val();                
        User.password = $('#passwordInput').val();
        User.gender = parseInt($('#genderInput').val());
        User.department_Id = $('#departmentInput').val();
        User.manager_Id= $('#managerInput').val();

        $.ajax({
            "type": "POST",
            "url": URLBackend +"/Register",
            "data": JSON.stringify(User),            
            "contentType": "application/json;charset=utf-8",
            "success": (result) => {
                if (result.status == 200) {
                    swal({
                        icon: 'success',
                        title: 'Success',
                        text: 'Data successfully created',
                    })
                    $('#tbEmployee').DataTable().ajax.reload();
                    $('#addModal').modal("hide");
                } else {
                    alert("Data failed to create")
                }
                $('#tbEmployee').DataTable().ajax.reload();
                $('#addModal').modal("hide");
            },
            "error": (result) => {
                if (result.status == 500 ) {
                    swal({
                        icon: 'error',
                        title: 'Failed',
                        text: result.message,
                    })
                }
            },
        })
    }
}

function GetById(nik) {
    select();
    $.ajax({
        type: "GET",
        url: URLBackend + "/" + nik,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var obj = result.data;                       
            debugger;
            $("#nikInput").val(obj.nik);
            $("#firstNameInput").val(obj.firstName);
            $("#lastNameInput").val(obj.lastName);
            $("#phoneInput").val(obj.phone);
            $("#emailInput").val(obj.email);
            $("#genderInput").val(obj.gender);
            $("#passwordInput").prop('disabled', true);
            $("#salaryInput").val(obj.salary);             

            var birthDt = new Date(obj.birthDate).toISOString().split('T')[0];
            $("#birthDateInput").val(birthDt);
                                                           
            $("#buttonSubmit").attr("onclick", "Update()");
            $("#buttonSubmit").attr("class", "btn btn-warning btn-sm");
            $("#buttonSubmit").html("Update");
            $("#addModalLabel").html(obj.firstName + ' ' + obj.lastName );
            $('#addModal').modal('show');

        },
        error: function (errormesage) {
            swal("Data Gagal Dimasukkan!", "You clicked the button!", "error");
        }
    })
}

function Update() {
    let validateForm = true
    if (
        $("#firstName").val() == "" ||
        $("#lastName").val() == "" ||
        $("#phoneInput").val() == "" ||
        $("#birthDateInput").val() == "" ||
        $("#salaryInput").val() == "" ||
        $("#genderInput").val() == "" ||
        $("#emailInput").val() == ""         
    ) {
        swal({
            icon: 'error',
            title: 'Failed',
            text: "Please fill out all your data",
        })
        validateForm = false
    } else {
        if (!$("#emailInput").val().match(/^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)) {
            swal({
                icon: 'error',
                title: 'Failed',
                text: "Sorry, your email is not valid",
            })
            validateForm = false
        }
        if (!$("#phoneInput").val().match(/^\d*\d$/)) {
            swal({
                icon: 'error',
                title: 'Failed',
                text: "Sorry, your phone number is not valid",
            })
            validateForm = false
        }
    }
    if (validateForm) {

        //debugger;
        var User = new Object();
        User.nik = $('#nikInput').val();
        User.firstName = $('#firstNameInput').val();
        User.lastName = $('#lastNameInput').val();
        User.phone = $('#phoneInput').val();
        User.email = $('#emailInput').val();
        User.salary = parseInt($('#salaryInput').val());
        User.birthDate = $('#birthDateInput').val();        
        User.gender = parseInt($('#genderInput').val());
        User.department_Id = $('#departmentInput').val();
        User.manager_Id = $('#managerInput').val();       
        $.ajax({
            type: 'PUT',
            url: URLBackend,
            data: JSON.stringify(User),
            contentType: "application/json; charset=utf-8",
        }).then((result) => {
            //debugger;
            if (result.status == 200) {
                $('#tbEmployee').DataTable().ajax.reload();
                swal({
                    icon: 'success',
                    title: 'Success',
                    text: 'Data successfully updated!',
                });
                $('#addModal').modal("hide");
            }
            else {
                swal({
                    icon: 'error',
                    title: 'Failed',
                    text: 'Data update failed!',
                });
            }
        });
    }
}

function Delete(nik) {
    //debugger;
    $.ajax({
        url: URLBackend + "/" + nik,
        type: "DELETE",
        dataType: "json",
    }).then((result) => {
        if (result.status == 200) {
            $('#addModal').modal('hide');
            $('#tbEmployee').DataTable().ajax.reload();

            swal({
                icon: 'success',
                title: 'Deleted',
                text: ''
            });
        }
        else {
            swal({
                icon: 'error',
                title: 'Failed',
                text: ''
            });
        }
    });
}

function ConfirmDelete(nik) {
    //debugger;
    //console.log(nik);
    swal({
        title: 'Apakah kamu yakin?',
        text: "Kamu Tidak Bisa  Mengulang Takdir yang Telah Terjadi!",
        icon: 'warning',
        buttons: true,
        dangerMode: true,
    }).then((isConfirmed) => {
        if (isConfirmed) {
            Delete(nik);
            //swal(
            //    'Dihapus!',
            //    'Data Berhasil Dihapus.',
            //    'Berhasil'
            //)
        }
    })
}