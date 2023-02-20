let URLBackend = "https://localhost:7116/api/Departmens"

$(document).ready(function () {    
    $('#tbEmployee').DataTable({
        "pagingType": 'simple_numbers',
        "ajax": {
            url: URLBackend +"/All",
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
            { "data": "name" },                                                      
            { "data": "manager_Name" },  
            {
                "className": "text-center",
                "render": function (data, type, row) {
                    return '<button class="btn btn-light btn-sm " data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="  GetById(\'' + row.id + '\')"><i class="bi bi-pencil-fill"></i></button >' + '&nbsp;' +
                        '<button class="btn btn-light btn-sm" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="ConfirmDelete(\'' + row.id + '\')"><i class="bi bi-trash3"></i></button >'
                }
            }
        ]
    });
})

function select() {

}

function Save() {
    let validateForm = true
    if (
        $("#departmentNameInput").val() == "" 
    ) {
        swal({
            icon: 'error',
            title: 'Failed',
            text: "Please fill out all your data",
        })
        validateForm = false
    }

    if (validateForm) {
        var dept = new Object();
        dept.name = $('#departmentNameInput').val();
        dept.manager_Id = $('#managerInput').val();

        $.ajax({
            "type": "POST",
            "url": URLBackend,
            "data": JSON.stringify(dept),
            "contentType": "application/json;charset=utf-8",
            "success": (result) => {
                if (result.status == 200 || result.status == 201) {
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
                if (result.status == 500) {
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

function GetById(id) {
    
    $.ajax({
        type: "GET",
        url: URLBackend + "/" + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var obj = result.data;
            debugger;
            $("#departmentIdInput").val(obj.id);
            $("#departmentNameInput").val(obj.name);
            $('#managerInput').append('<option value="' + obj.manager_Id + '">' + obj.manager_Id + '</option>');
            $("#buttonSubmit").attr("onclick", "Update()");
            $("#buttonSubmit").attr("class", "btn btn-warning btn-sm");
            $("#buttonSubmit").html("Update");
            $("#addModalLabel").html(obj.name);
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
        $("#departmentIdInput").val() == "" ||
        $("#departmentNameInput").val() == ""
    ) {
        swal({
            icon: 'error',
            title: 'Failed',
            text: "Please fill out all your data",
        })
        validateForm = false
    }
    if (validateForm) {
       //debugger;
        var dept = new Object();
        dept.id = $('#departmentIdInput').val();
        dept.name = $('#departmentNameInput').val();
        dept.manager_id = $('#managerInput').val();

        $.ajax({
            type: 'PUT',
            url: URLBackend,
            data: JSON.stringify(dept),
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

function Delete(id) {
    //debugger;
    $.ajax({
        url: URLBackend + "/" + id,
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

function ConfirmDelete(id) {
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
            Delete(id);
            //swal(
            //    'Dihapus!',
            //    'Data Berhasil Dihapus.',
            //    'Berhasil'
            //)
        }
    })
}