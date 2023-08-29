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
        var url = "/DonanimYonetim/MarkaKaydet";
        $.ajax({
            url: url,
            type: "POST",
            data: {
                id: 0,
                ad: ad,
                kullanimda:true
            },
            success: function (data) {
                console.log(data.id)
                if (data!=null) {
                    console.log(data);
                    var guncelleBtn = "<a class='btn btn-primary markaGuncelle' data-id='" + data.id + "' >Güncelle</a>";
                    donanimMarka_tablo.row.add([guncelleBtn, data.ad]).draw();
                }
            }
        })
    });

    $("#donanimMarkaTablo").on("click", ".markaGuncelle", function () {
        var markaId = $(this).data("id");
        var markaAd = $(this).data("ad");
        console.log(markaAd);
        $("#id").val(markaId);
        $("#ad").val(markaAd);
        $("#yeniMarka_btn").hide();
        $("#vazgecMarka_btn").show();
        $("#kaydetMarka_btn").show();

    });

})