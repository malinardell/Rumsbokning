function logout() {
    alert("Är du säker på att du vill logga ut?");
    window.location = "/account/index";
}


function getCalendar(roomId) {
    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();
    locale: 'sv',

    $('#calendar').empty().fullCalendar({
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'agendaWeek, agendaDay'
        },
        editable: true,      
        weekNumbers: true,
        firstDay: 1,
        defaultView: 'agendaWeek',
        navLinks: false,
        events: '/Home/GetCalendar/'+ roomId
    });

}

