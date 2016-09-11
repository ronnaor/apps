$(document).ready(function () {
    if (!$(".video1 .form-control").val()) { $(".video1 .form-control").hide(); }
    if (!$(".image1 .form-control").val()) { $(".image1 .form-control").hide(); }
});


$(':checkbox#videoCheck').change(function () {
    if (this.checked) {
        $(".video1 .form-control").show();
    }
    else {
        $(".video1 .form-control").hide();
        $(".video1 .form-control").val('');
    }
});

$(':checkbox#imageCheck').change(function () {
    if (this.checked) {
        $(".image1 .form-control").show();
    }
    else {
        $(".image1 .form-control").hide();
        $(".image1 .form-control").val('');
    }
});


function showValue(newValue) {
    document.getElementById("range").innerHTML = newValue;
}