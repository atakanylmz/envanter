// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
   // var kullanici_tablo = $('#kullaniciTablo').DataTable();

    var donanimMarka_tablo = $('#donanimMarkaTablo').DataTable({
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/tr.json',
        }
    });
  //  const donanimMarka_tablo = new DataTable('#donanimMarkaTablo');

    $("#yeniMarka_btn").click(function () {
        var ad = $("#ad").val();
        console.log(ad);
        if (ad == null || ad == "") {
            $("#mesaj").append("Marka adı giriniz");
        }
        else {
            var url = "/DonanimYonetim/MarkaKaydet";
            $.ajax({
                url: url,
                type: "POST",
                data: {
                    id: 0,
                    ad: ad,
                    kullanimda: true
                },
                success: function (data) {
                    console.log(data.ad)
                    if (data != null) {
                        var guncelleBtn = "<a class='btn btn-primary markaGuncelle' data-id='" + data.id + "' data-ad='"+data.ad+"' >Güncelle</a>";
                        donanimMarka_tablo.row.add([guncelleBtn, data.ad]).draw();
                        alert("Kayıt eklendi");
                        $("#mesaj").empty();
                    }
                }
            })
        }
    });

    //dinamik oluşabilen dom elemanlarına .click erişemeyebilir
    //bu yüzden .on ile sabit dom elemanı üzerinden erişilir
    $("#donanimMarkaTablo").on("click", ".markaGuncelle", function () {
        var markaId = $(this).data("id");
        var markaAd = $(this).data("ad");
        var indeks = donanimMarka_tablo.row($(this).parents('tr')).index();
        $("#guncelleSatirIndeks").val(indeks);
        $("#id").val(markaId);
        $("#ad").val(markaAd);
        $("#yeniMarka_btn").hide();
        $("#vazgecMarka_btn").show();
        $("#kaydetMarka_btn").show();
        $("#mesaj").empty();
    });

    $("#vazgecMarka_btn").click(function () {
        MarkaFormResetle();
    });

    $("#kaydetMarka_btn").click(function () {
        var markaId = $("#id").val();
        var markaAd = $("#ad").val();
        if (markaAd == null || markaAd == "") {
            $("#mesaj").append("Marka adı giriniz");
        }
        else {
            var url = "/DonanimYonetim/MarkaKaydet";
            $.ajax({
                url: url,
                type: "POST",
                data: {
                    id: markaId,
                    ad: markaAd,
                    kullanimda: true
                },
                success: function (data) {

                    if (data != null) {
                        var guncelleBtn = "<a class='btn btn-primary markaGuncelle' data-id='" + data.id + "' data-ad='" + data.ad + "' >Güncelle</a>";
                        var yeniData = [guncelleBtn, data.ad]
                        var indeks = $("#guncelleSatirIndeks").val();

                        donanimMarka_tablo.row(indeks).data(yeniData).draw();
                        //donanimMarka_tablo.cell({ row: indeks, column: 1 }).data(data.ad).draw();
                        //donanimMarka_tablo.cell({ row: indeks, column: 0 }).data(guncelleBtn).draw();

                        alert("Kayıt güncellendi");
                        $("#mesaj").empty();
                    }
                }
            })

            MarkaFormResetle();
        }
  
    });
})

function MarkaFormResetle() {
    $("#id").val("");
    $("#ad").val("");
    $("#yeniMarka_btn").show();
    $("#vazgecMarka_btn").hide();
    $("#kaydetMarka_btn").hide();
}