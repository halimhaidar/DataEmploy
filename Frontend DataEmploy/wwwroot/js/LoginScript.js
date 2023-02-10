function Login() {
    let validateForm = true
    if (
        $("#emailInput").val() == "" ||
        $("#passwordInput").val() == ""

    ) {
        swal({
            icon: 'error',
            title: 'Failed',
            text: "Please fill out all your data",
        })
        validateForm = false
    }


    if (validateForm) {
        var User = new Object();
        User.email = $('#emailInput').val();
        User.password = $('#passwordInput').val();
        debugger;
        $.ajax({
            "type": "POST",
            "url": "https://localhost:7116/api/accounts/login/",
            "data": JSON.stringify(User),
            "contentType": "application/json;charset=utf-8",
            "success": (result) => {
                if (result.status == 200 || result.status == 201) {                    
                    localStorage.setItem('token', result.data.token);
                    localStorage.setItem('nik', result.data.nik);
                    localStorage.setItem('roleId', result.data.roleId);
                    localStorage.setItem('roleName', result.data.roleName);                    
                    sessionStorage.setItem("login", "Anda Login");
                    window.location.assign("https://localhost:7104/");
                } else {
                    alert("Login failed")
                }
               
            },
            "error": (result) => {
                if (result.status == 400 || result.status == 500) {
                    swal({
                        icon: 'error',
                        title: 'Failed',
                        text: result.responseJSON.message,
                    })
                }
            },
        })
    }
}