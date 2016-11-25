function logout() {
    alert("Är du säker på att du vill logga ut?");
    window.location = "/account/index";
}

$("#btnGetJSON").click(function () {
    $.get("/Home/GetRoom", null, function (result) {
        //var html = "<ul>";
        //html += "</ul>";
        $("#calendar").html(html);
    });
});

$.getScript('', function () {

    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();

    $('#calendar').fullCalendar({
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'agendaWeek, agendaDay'
        },
        editable: true,
        firstDay: 1,
        defaultView: 'agendaWeek',
        navLinks: false,
        events: '/Home/GetRoom'
    });
});