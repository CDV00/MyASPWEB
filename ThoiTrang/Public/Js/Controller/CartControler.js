var cart = {
    init: function () {
        cart.regEvents();
        /*var Tong = 0;
        $('.Thanhtien').each(function () {
            Tong += parseFloat($(this).html());
            //alert(parseFloat($(this).html()))
        });
        $("#Tong").text(Tong + ",000,000 VND");*/
    },
    
    regEvents: function () {
        //Tiếp tục mua săm
        $('#btnContinue').off('click').on('click', function () {
            window.location.href='/';
        });
        //Tiếp tục thanh toán
        $('#btnPayment').off('click').on('click', function () {
            window.location.href='/thanh-toan';
        });

        //cập nhập số lượng
        $('.txtQuantity').off('click').on('click', function () {
            var listProduct = $('.txtQuantity');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    Product: {
                        Id: $(item).data('id'),
                    }
                });
            });

            $.ajax({
                url: '/Cart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = '/gio-hang';
                    }
                    else {
                        
                    }
                }
            })
        });
        //Xóa tất cả
        $('#btnDeleteAll').off('click').on('click', function () {
            $.ajax({
                url: '/Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = '/gio-hang';
                    }
                }
            })
        });
        //Xóa tất cả
        $('#btnDaDatHang').off('click').on('click', function () {
            $.ajax({
                url: '/Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = '/trang-chu';
                    }
                }
            })
        });
        //Xóa từng sản phẩm
        $('.btnDelete').off('click').on('click', function () {
            $.ajax({
                url: '/Cart/Delete',
                data: { id: $(this).data('id') },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = '/gio-hang';
                    }
                }
            })
        });
        //xem chi tiết
        $('.hinh').off('click').on('click', function () {
            var slug = $(this).data('slug');
            window.location.href = '/chi-tiet-san-pham/' + slug;
                    
        });
        //
        $(document).ready(function () {
            
            //tổng
            var Tong = parseFloat(0);
            $('.Tien').each(function () {
                Tong += parseFloat($(this).html());
            });
            Tong = new Number(Tong).toLocaleString("fi-FI");
            $("#Tong").text(Tong);

            //tiền
            $('.Tien').each(function () {
                var Tien = $(this).html();
                //var Tien = new Number(Tien).toLocaleString("fi-FI");
                $(this).text(new Number(Tien).toLocaleString("fi-FI"));
            });
            

        });
        //
    }
}

cart.init();