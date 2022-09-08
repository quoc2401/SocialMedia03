Validator({
    form: '#form-register',
    formGroupSelector: '.form-group',
    errSelector: '.err-validate',
    rules: [
        Validator.isRequired('#lastname'),
        Validator.isRequired('#firstname'),
        Validator.isRequired('#email'),
        Validator.isEmail('#email'),
        Validator.isDate('#dateofbirth'),
        Validator.isRequired('#phone'),
        Validator.isPhone('#phone'),
        Validator.minLength('#password', 6, 'Mật khẩu phải có tối thiểu 6 kí tự'),
        Validator.isRequired('#password-confirm'),
        Validator.isConfirmed('#password-confirm', function () {
            return document.querySelector('#form-register #password').value;
        }, 'Mật khẩu nhập lại không chính xác')
    ],
    onSubmit: (data) => {
        var formData = new FormData();
        var fs = document.getElementById('avatar');
        if (fs.files[0] === undefined) {
            createUser(data);
        }
        else {
            var fileType = fs.files[0]['type'];
            var validImageTypes = ['image/jpeg', 'image/png'];
            if (!validImageTypes.includes(fileType)) {
                alert("Không thể nhận loại file này!");
            }
            else {
                $('.register-load').css("display", "flex");

                for (const file of fs.files) {
                    formData.append("file", file);
                }
                $.ajax({
                    type: 'POST',
                    url: `/api/post/image-upload`,
                    data: formData,
                    dataType: "json",
                    processData: false,
                    cache: false,
                    contentType: false
                })
                    .done(function (res) {
                        createUser(data, res);
                    });
            }


        }
    }
})

function createUser(data, res) {
    $.ajax({
        type: 'POST',
        url: '/api/user/register',
        contentType: 'application/json',
        data: JSON.stringify({
            Email: data.email,
            Password: data.password,
            Firstname: data.firstname,
            Lastname: data.lastname,
            Birthday: data.birthday,
            Address: data.address,
            Hometown: data.hometown,
            Phone: data.phone,
            Avatar: res ? res.data : "",
        }),
        success: function () {
            $('.register-load').css("display", "none");
            swal({
                title: "Đăng kí thành công",
                icon: "success",
            })

            setTimeout(() => {
                window.location.replace("/account/login");
            }, 1500)
        }
    }).fail((res) => {
        console.log(res);
        $('.register-load').css("display", "none");
        $('.show-err').css("display", "block");
        $('.show-err').text(res.responseJSON.message);
    });
}