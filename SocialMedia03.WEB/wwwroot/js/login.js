
Validator({
    form: '#loginForm',
    formGroupSelector: '.form-group',
    errSelector: '.err-validate',
    rules: [
        Validator.isRequired('#email'),
        Validator.isEmail('#email'),
        Validator.minLength('#password', 6, 'Mật khẩu phải có tối thiểu 6 kí tự')
    ],
    onSubmit: (data) => {

        $.ajax({
            type: 'POST',
            url: '/api/user/authenticate',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({
                Email: data.email,
                Password: data.password
            }),
            dataType: 'json',
            success: function (data) {
                swal({
                    title: "Đăng nhập thành công",
                    icon: "success",
                })

                setTimeout(() => {
                    window.location.replace("/");
                }, 1500)
            }
        }).fail((res) => {
            $('.show-err').css("display", "block");
            $('.show-err').text(res.responseJSON.message);
        });
    }
});