var user = {
    init: function () {
        user.registerEvent();
    },
    registerEvent: function () {
        $('.btn-blockCustomer').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = $(this).data('id');
            $.ajax({
                url: "/Admin/Customer/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.text('Kích hoạt');
                    }
                    if (response.status == false) {
                        btn.text('Khóa');
                    }
                }
            })

        });
    }
}
user.init();

var userEmployee = {
    init: function () {
        userEmployee.registerEvent();
    },
    registerEvent: function () {
        $('.btn-activeEmployee').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = $(this).data('id');
            $.ajax({
                url: "/Admin/Employee/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.text('Kích hoạt');
                    }
                    if (response.status == false) {
                        btn.text('Khóa');
                    }
                }
            })

        });
    }
}
userEmployee.init();

var product = {
    init: function () {
        product.registerEvent();
    },
    registerEvent: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = $(this).data('id');
            $.ajax({
                url: "/Admin/Product/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);

                    if (response.status == true) {

                        var element = document.getElementById("row_" + id);
                        element.style.backgroundColor = 'white';

                        var title = document.getElementById("title_" + id)
                        title.title = "open";

                        btn.html('<i class="zmdi zmdi-globe"></i>');
                    }
                    if (response.status == false) {

                        var element = document.getElementById("row_" + id);
                        element.style.backgroundColor = '#e4e8e4';

                        var title = document.getElementById("title_" + id)
                        title.title = "lock";

                        btn.html('<i class="zmdi zmdi-block-alt"></i>');


                    }
                }
            })

        });
    }
}
product.init();


var productCategory = {
    init: function () {
        productCategory.registerEvent();
    },
    registerEvent: function () {
        $('.btn-proactive').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = $(this).data('id');
            $.ajax({
                url: "/Admin/ProductCategory/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.text('open');
                    }
                    if (response.status == false) {
                        btn.text('lock');
                    }
                }
            })

        });
    }
}
productCategory.init();

var brand = {
    init: function () {
        brand.registerEvent();
    },
    registerEvent: function () {
        $('.btn-tiveac').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = $(this).data('id');
            $.ajax({
                url: "/Admin/Brand/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.text('open');
                    }
                    if (response.status == false) {
                        btn.text('lock');
                    }
                }
            })

        });
    }
}
brand.init();


var question = {
    init: function () {
        question.registerEvent();
    },
    registerEvent: function () {
        $('.btn-question').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = $(this).data('id');
            $.ajax({
                url: "/Admin/AnswerQuestion/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.text('open');
                    }
                    if (response.status == false) {
                        btn.text('lock');
                    }
                }
            })

        });
    }
}
question.init();