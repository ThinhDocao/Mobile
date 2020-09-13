var Loginform = {
    init: function () {
        Loginform.registerEvent();
    },
    registerEvent: function () {
        $("#msg").hide();
        $('.loginHome').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $("#loginForm").serialize();
            

            $.ajax({
                url: '/Admin/Employee/CheckValidUser',
                data: { id: id},
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.result == false) {
                        $("#msg").show();
                    }
                    else {
                        window.location.href = "/";
                        $("#msg").hide();
                    }
                }

            });

        });
    }
}
Loginform.init();

var Registerform = {
    init: function () {
        Registerform.registerEvent();
    },
    registerEvent: function () {
        $("#gister").hide();
        $('.RegisterHome').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $("#RegisterForm").serialize();


            $.ajax({
                url: '/Admin/Employee/CheckValidRegister',
                data: { id: id },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.test == false) {
                        window.location.href = "/";
                        
                    }
                    else {
                        $("#gister").show();
                        $("#gister").html('<ul style="color:red;">'+res.ss+'</ul>');
                    }
                }

            });

        });
    }
}
Registerform.init();


var Forgotform = {
    init: function () {
        Forgotform.registerEvent();
    },
    registerEvent: function () {
        $("#forgot").hide();
        $('.ForgotHome').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $("#ForgotForm").serialize();


            $.ajax({
                url: '/Admin/Employee/CheckValidForgot',
                data: { id: id },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.result == true) {
                        $("#forgot").show();
                        $(".emailAfter").hide();
                        $("#forgot").html('<ul style="color:red;">Please check your email to reset your password. </ul>');
                    }
                    else {
                        $("#forgot").show();
                    }
                }

            });

        });
    }
}
Forgotform.init();


var LoginGoogle = {
    init: function () {
        LoginGoogle.registerEvent();
    },
    registerEvent: function () {
        
        
    }
}
LoginGoogle.init();