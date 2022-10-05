/// <reference path="owl.carousel.min.js" />
/// <reference path="owl.carousel.min.js" />
a= $(document).ready(function ($) {
    //repon
    
    load();
    
});

function load() {
    $('.product-carousel').owlCarousel({
        loop: true,
        nav: true,
        margin: 20,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
            },
            600: {
                items: 3,
            },
            1000: {
                items: 5,
            }
        }
    });
    // Bootstrap ScrollPSY repon
    $('body').scrollspy({
        target: '.navbar-collapse',
        offset: 95
    })

    var dd = $(location).attr('href');
    if (dd == 'https://localhost:44304/trang-chu') {
        start();
    }
}

//ham thoiwf gian sale
var n = 0; // Ngay
var h = 0; // Giờ
var m = 0; // Phút
var s = 30;// Giây
function start() {
    /*BƯỚC 1: LẤY GIÁ TRỊ BAN ĐẦU
    if (h === null)
    {
        h = parseInt(document.getElementById('h_val').value);
        m = parseInt(document.getElementById('m_val').value);
        s = parseInt(document.getElementById('s_val').value);
    }*/
    
    /*BƯỚC 1: CHUYỂN ĐỔI DỮ LIỆU*/
    // Nếu số giây = -1 tức là đã chạy ngược hết số giây, lúc này:
    //  - giảm số phút xuống 1 đơn vị
    if (n == -1) {
        clearTimeout(timeout);
        document.getElementById('n').innerText = "00";
        document.getElementById('h').innerText = "00";
        document.getElementById('m').innerText = "00";
        document.getElementById('s').innerText = "00";
        document.getElementById('Sale').innerText = "Đã hết thời gian sale";
        return false;
    }
    //  - thiết lập số giây lại 59
    if (s === -1) {
        m -= 1;
        s = 59;
    }

    // Nếu số phút = -1 tức là đã chạy ngược hết số phút, lúc này:
    //  - giảm số giờ xuống 1 đơn vị
    //  - thiết lập số phút lại 59
    if (m === -1) {
        h -= 1;
        m = 59;
    }

    // Nếu số giờ = -1 tức là đã hết giờ, lúc này:
    //  - Dừng chương trình
    if (h == -1) {
        n -= 1;
        h = 23;
        m = 59;
        s = 59;
    }
    //Kiểm nếu số(ngày,Giờ...) chỉ có 1 kí tự thì thêm số 0 vào trước
    function KT(a) {
        if (a.length == 1) {
            return "0" + a;
        }
        return a;
    }

    /*BƯỚC 1: HIỂN THỊ ĐỒNG HỒ*/
    document.getElementById('n').innerText = KT(n.toString());
    document.getElementById('h').innerText = KT(h.toString());
    document.getElementById('m').innerText = KT(m.toString());
    document.getElementById('s').innerText = KT(s.toString());

    /*BƯỚC 1: GIẢM PHÚT XUỐNG 1 GIÂY VÀ GỌI LẠI SAU 1 GIÂY */
    timeout = setTimeout(function () {
        s--;
        start();
    }, 1000);
}


//add to cart
/*$('body').on('click', '.catHome', function (e) {
    e.preventDefault();
    $.ajax({
        url: '/Module/Update',
        data: { id: $(this).data('id') },
        dataType: 'json',
        type: 'POST',
        success: function (res) {
            if (res.status == true) {
                
                window.location.href = '/trang-chu';
                e.preventDefault();
            }
        }
   })

});*/
$('.catHome').off('click').on('click', function () {
    var dd = $(location).attr('href');
    $.ajax({
        url: '/Module/Update',
        data: { id: $(this).data('id') },
        dataType: 'json',
        type: 'POST',
        success: function (res) {


            if (res.status == true) {
                //$("#productHome").load('/Module/_productHome');
                //$("*").load('/Module/_productHome');

                //
                //load();
                window.location.href = dd;
            }
        }
    })
});

//mua sản phẩm trang chi tiết
$('#MuaSP').off('click').on('click', function () {
    var dd = $("#MuaSP").attr('href');
    var soLuong = $('.quantity').val();
    //e.preventDefault();
    //alert(dd + soLuong)
    window.location.href = dd + soLuong;
})
//Đăng xuất
$('#btnLogout').off('click').on('click', function () {
    $.ajax({
        url: '/AuthSite/Logout',
        dataType: 'json',
        type: 'POST',
        success: function (res) {


            if (res.status == true) {
                
                window.location.href = '/dang-nhap';
            }
        }
    })
});