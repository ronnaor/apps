(function () {
    $(document).ready(function () {
        $.getJSON("http://api.openweathermap.org/data/2.5/weather?id=293397&units=metric&APPID=e40e7a67ef72c2f7260ec5aa3c14bb0d", function (data) {
            $(document).ready(function () {
                $('#city').text(data.name);
                $('#country').text(data.sys.country);
                $('#temp').text(data.main.temp);
                $('#humidity').text(data.main.humidity);
            });
        });
    });
}());