function GirisYap() {
    Kullanici = new Object();
    Kullanici.Email = $("#email").val();
    Kullanici.Sifre = $("#sifre").val();

    $.ajax({
        url: "/Account/Login",
        data: Kullanici,
        type: "POST",
        datatype: "json",
        success: function (response) {
            if (response.Success) {
                bootbox.alert(response.Message, function () {
                    window.location = "/Home/Index";
                });
            }
            else {
                bootbox.alert(response.Message, function () {

                });
            }
        }
    })
}


function KategoriEkle() {
    Kategori = new Object();
    Kategori.KategoriAdi = $("#kategoriAdi").val();
    Kategori.AktifMi = $("#kategoriAktifMi").is(":checked");
    Kategori.ParentID = $("#ParentID").val();

    $.ajax({
        url: "/Kategori/Ekle",
        data: Kategori,
        type: "POST",
        datatype: "json",
        success: function (response) {
            if (response.Success) {
                bootbox.alert(response.Message, function () {
                    location.reload();
                });
            }
            else {
                bootbox.alert(response.Message, function () {

                });
            }
        }
    })
}

$(document).on("click", "#btn-KategoriSil", function () {
    var gelenID = $(this).attr("data-id");
    var silTR = $(this).closest("tr");

    $.ajax({
        url: "/Kategori/Sil/" + gelenID,
        type: "POST",
        dataType: "json",
        success: function (response) {
            if (response.Success) {
                $.notify(response.Message, "success");
                silTR.fadeOut(300, function () {
                    silTR.remove();
                })
            }
            else {
                $.notify(response.Message, "error");
            }
        }
    })
})

function KategoriDuzenle() {
    Kategori = new Object();
    Kategori.KategoriAdi = $("#kategoriAdi").val();
    Kategori.AktifMi = $("#kategoriAktif").is(":checked");
    Kategori.ParentID = $("#ParentID").val();
    Kategori.ID = $("#ID").val();


    $.ajax({
        url: "/Kategori/Duzenle",
        data: Kategori,
        type: "POST",
        dataType: "json",
        success: function (response) {
            if (response.Success) {
                bootbox.alert(response.Message, function () {
                    window.location = "/Kategori/Index";
                });
            }
            else {
                bootbox.alert(response.Message, function () {
                    //şimdilik boş geçtik
                });
            }
        }
    })
}

function HaberEkle() { //ajaxta resim upload etmesi sıkıntı olduğundan şimdilik boşa çıktı.
    Haber = new Object();
    Haber.KategoriID = $("#haberKategori").val();
    Haber.Baslik = $("#haberBaslik").val();
    Haber.KisaAciklama = CKEDITOR.instances['KisaAciklama'].getData();
    Haber.Aciklama = CKEDITOR.instances['Aciklama'].getData();
    Haber.Etiket = $("#haberEtiket").val();
    Haber.Resim = $("#vitrinResim").val();
    Haber.AktifMi = $("#haberAktifMi").is(":checked");

    $.ajax({
        url: "/Haber/Ekle",
        data: Haber,
        type: "POST",
        dataType: "json",
        success: function (response) {
            if (response.Success) {
                bootbox.alert(response.Message, function () {
                    location.reload();
                });
            }
            else {
                bootbox.alert(response.Message, function () {

                });
            }
        }
    })
}


$(document).on("click", "#btn-HaberSil", function () {
    var gelenID = $(this).attr("data-id");
    var silTR = $(this).closest("tr");

    $.ajax({
        url: "/Haber/Sil/" + gelenID,
        type: "POST",
        dataType: "json",
        success: function (response) {
            if (response.Success) {
                $.notify(response.Message, "success");
                silTR.fadeOut(300, function () {
                    silTR.remove();
                })
            }
            else {
                $.notify(response.Message, "error");
            }
        }
    })
})


$(document).on("click", "#btn-Onay", function () {
    var gelenID = $(this).attr("data-id");

    $.ajax({
        url: "/Haber/Onay/" + gelenID,
        type: "POST",
        datatype: "json",
        success: function (response) {
            if (response.Success) {
                $.notify(response.Message, "success");
                location.reload();
            }
            else {
                $.notify(response.Message, "error");
            }
        }
    })
})


$(document).on("click", "#btn-SliderSil", function () {
    var gelenID = $(this).attr("data-id");
    var silTR = $(this).closest("tr");

    $.ajax({
        url: "/Slider/Sil/" + gelenID,
        type: "POST",
        dataType: "json",
        success: function (response) {
            if (response.Success) {
                $.notify(response.Message, "success");
                silTR.fadeOut(300, function () {
                    silTR.remove();
                })
            }
            else {
                $.notify(response.Message, "error");
            }
        }
    })
})

$(document).on("click", "#btn-YorumSil", function () {
    var gelenID = $(this).attr("data-id");
    var silTR = $(this).closest("tr");

    $.ajax({
        url: "/Yorum/Sil/" + gelenID,
        type: "POST",
        dataType: "json",
        success: function (response) {
            if (response.Success) {
                $.notify(response.Message, "success");
                silTR.fadeOut(300, function () {
                    silTR.remove();
                })
            }
            else {
                $.notify(response.Message, "error");
            }
        }
    })
})
function YorumDuzenle() {
    adres = new String();
    Yorum = new Object();
    Yorum.GelenYorum = $("#gelenYorum").val();
    Yorum.AktifMi = $("#yorumAktifMi").is(":checked");
    Yorum.HaberID = $("#haberID").val();
    Yorum.ID = $("#ID").val();
    adres = $("#urlAdres").val();

    $.ajax({
        url: "/Yorum/Duzenle",
        data: Yorum,
        type: "POST",
        dataType: "json",
        success: function (response) {
            if (response.Success) {
                bootbox.alert(response.Message, function () {
                    window.location = adres; /*"/Yorum/HaberYorumlari/" + Yorum.HaberID;*/
                });
            }
            else {
                bootbox.alert(response.Message, function () {
                    //şimdilik boş geçtik
                });
            }
        }
    })
}


function KullaniciEkle() {
    Kullanici = new Object();
    Kullanici.RolID = $("#rolID").val();
    Kullanici.AdSoyad = $("#adSoyad").val();
    Kullanici.Email = $("#email").val();
    Kullanici.Sifre = $("#sifre").val();
    Kullanici.AktifMi = $("#kullaniciAktifMi").is(":checked");

    $.ajax({
        url: "/Account/Ekle",
        data: Kullanici,
        type: "POST",
        dataType: "json",
        success: function (response) {
            if (response.Success) {
                bootbox.alert(response.Message, function () {
                    window.location = "/Account/Index";
                });
            }
            else {
                bootbox.alert(response.Message, function () {

                });
            }
        }
    })
}

$(document).on("click", "#btn-KullaniciSil", function () {
    var gelenID = $(this).attr("data-id");
    var silTR = $(this).closest("tr");

    $.ajax({
        url: "/Account/Sil/" + gelenID,
        type: "POST",
        dataType: "json",
        success: function (response) {
            if (response.Success) {
                $.notify(response.Message, "success");
                silTR.fadeOut(300, function () {
                    silTR.remove();
                })
            }
            else {
                $.notify(response.Message, "error");
            }
        }
    })
})

function KullaniciDuzenle() {
    Kullanici = new Object();
    Kullanici.ID = $("#ID").val();
    Kullanici.RolID = $("#rolID").val();
    Kullanici.AdSoyad = $("#adSoyad").val();
    Kullanici.Email = $("#email").val();
    Kullanici.Sifre = $("#sifre").val();
    Kullanici.AktifMi = $("#kullaniciAktifMi").is(":checked");

    $.ajax({
        url: "/Account/Duzenle",
        data: Kullanici,
        type: "POST",
        dataType: "json",
        success: function (response) {
            if (response.Success) {
                bootbox.alert(response.Message, function () {
                    window.location = "/Account/Index";
                });
            }
            else {
                bootbox.alert(response.Message, function () {

                });
            }
        }
    })
}