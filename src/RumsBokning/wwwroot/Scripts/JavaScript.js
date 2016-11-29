function logout() {
    alert("Är du säker på att du vill logga ut?");
    window.location = "/account/index";
}


function getCalendar(roomId) {
    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();

    $('#calendar').empty().fullCalendar({

        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'agendaWeek, agendaDay'
        },
        axisFormat: 'H:mm',
        timeFormat: {
            '': 'H(:mm)',
            agenda: 'H:mm{ - H:mm}'
        },
        monthNames: ['Januari', 'Februari', 'Mars', 'April', 'Maj', 'Juni', 'Juli', 'Augusti', 'September', 'Oktober', 'November', 'December'],
        monthNamesShort: ['Jan', 'Feb', 'Mar', 'Apr', 'Maj', 'Jun', 'Jul', 'Aug', 'Sep', 'Okt', 'Nov', 'Dec'],
        dayNames: ['Söndag', 'Måndag', 'Tisdag', 'Onsdag', 'Torsdag', 'Fredag', 'Lördag'],
        dayNamesShort: ['Sön', 'Mån', 'Tis', 'Ons', 'Tor', 'Fre', 'Lör'],
        buttonText: {
            today: 'idag',
            week: 'vecka',
            day: 'dag'
        },
        editable: true,
        weekNumbers: true,
        weekNumberTitle: 'V',
        allDayText: 'heldag',
        firstDay: 1,
        defaultView: 'agendaWeek',
        navLinks: false,
        selectable: true,
        selectHelper: true,
        select: function (start, end) {
            var title = prompt('Beskrivning av mötet:');
            var eventData;
            if (title) {
                eventData = {
                    title: title,
                    description: title,
                    start: start.toLocaleString(),
                    end: end.toLocaleString(),
                    roomId: roomId
                };
                eventRender = function (event, element) {
                    element.qtip({
                        content: event.description
                    });
                };
                //$('#calendar').fullCalendar('renderEvent', eventData, false); // stick? = true
                //console.log(title + " " + start + " " + end);
                $.post('/home/CreateEvent', eventData, function () {
                    getCalendar(roomId);
                });
            }
            $('#calendar').fullCalendar('unselect');
        },
        events: '/Home/GetCalendar/' + roomId

    });

}

