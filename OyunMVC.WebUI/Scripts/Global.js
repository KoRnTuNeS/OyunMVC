function Giris() {
    adres = new String();
    Kullanici = new Object();
    Kullanici.Email = $("#email").val();
    Kullanici.Sifre = $("#sifre").val();
    adres = $("#urlAdres").val();

    $.ajax({
        url: "/Account/Login",
        data: Kullanici,
        type: "POST",
        datatype: "json",
        success: function (response) {
            if (response.Success) {
                bootbox.alert(response.Message, function () {
                    window.location = adres;
                });
            }
            else {
                bootbox.alert(response.Message, function () {

                });
            }
        }
    })
}

function YorumEkle() {
    Yorum = new Object();
    Yorum.HaberID = $("#haberID").val();
    Yorum.KullaniciID = $("#kullaniciID").val();
    Yorum.GelenYorum = $("#gelenYorum").val();

    $.ajax({
        url: "/Yorum/Ekle",
        data: Yorum,
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

function MesajGonder() {
    Mail = new Object();
    Mail.Ad = $("#ad").val();
    Mail.MailAdresi = $("#mail").val();
    Mail.Konu = $("#konu").val();
    Mail.Mesaj = $("#mesaj").val();

    $.ajax({
        url: "/Home/Contact",
        data: Mail,
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

function Kayit() {
    Kullanici = new Object();
    Kullanici.AdSoyad = $("#adSoyad").val();
    Kullanici.Email = $("#email").val();
    Kullanici.Sifre = $("#sifre").val();
    Kullanici.SifreTekrar = $("#sifreTekrar").val();

    $.ajax({
        url: "/Account/Register",
        data: Kullanici,
        type: "POST",
        datatype: "json",
        success: function (response) {
            if (response.Success) {
                bootbox.alert(response.Message, function () {
                    window.location = "/Account/Login";
                });
            }
            else {
                bootbox.alert(response.Message, function () {

                });
            }
        }
    })
}