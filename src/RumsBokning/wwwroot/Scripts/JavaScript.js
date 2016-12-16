function logout() {
    var c = confirm("Är du säker på att du vill logga ut?");
    if (c === true)
    {
        window.location = "/account/logout";
    }
 
}


function getCalendar(roomId) {
    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();

    $.post('/Home/GetRoomTitle/' + roomId, function (data) {
        var room = $.parseJSON(data);
        $("#title").text(room.Name + ' (' + room.Capacity + ' platser)');
        if (room.HasTvScreen === true) {
            $('#Tvimage').fadeIn();
        }
        else {
            $('#Tvimage').fadeOut();
        }

        if (room.HasProjector === true) {
            $('#Projectorimage').fadeIn();
        }
        else {
            $('#Projectorimage').fadeOut();
        }

        if (room.HasWhiteBoard === true) {
            $('#Whiteboardimage').fadeIn();
        }
        else {
            $('#Whiteboardimage').fadeOut();
        }
    });

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
        columnFormat: {
            week: 'ddd d/M',
            day: 'dddd d/M'
        },       
        weekNumbers: true,
        weekNumberTitle: 'V',
        allDaySlot: false,
        allDayText: 'heldag',
        firstDay: 1,
        defaultView: 'agendaWeek',
        eventColor: 'green',
        businessHours: {
            start: '06:00',
            end: '21:00'
        },        
        editable: true,
        eventDrop: function(event, revertFunc) {

            alert(event.description + " flyttades till " + event.start.toLocaleString());

            if (!confirm("Vill du spara ändringarna?")) {
                revertFunc();
            }
            else {
                $.post('/home/ChangeBooking', event, function () {
                    getCalendar(roomId);
                });
            }
        },
        navLinks: false,
        eventOverlap: false,
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
                    roomId: roomId,
                };
                eventRender = function (event, element) {
                    element.qtip({
                        content: event.description
                    });
                };
                $.post('/home/CreateEvent', eventData, function () {
                    getCalendar(roomId);
                });
            }
            $('#calendar').fullCalendar('unselect');
        },

        events: '/Home/GetCalendar/' + roomId
    });
}
function cancelBooking(RoomTimeId) {
    var c = confirm("Vill du avboka?");
    if (c === true) {
        $.post('/Home/ShowBooking/' + RoomTimeId, function (data) {
            if (data === '1')
                $('tr#' + RoomTimeId).fadeOut();
        });
    }
}

function hideAdmin() {
    $.post('/Home/HideAdmin/', function (data) {
        if (data === 'Admin') {
            $('#menuadmin').fadeIn();
        }
        else 
        {      
            $('#menuadmin').fadeOut();
        }
    });
    
}

function deleteRoom(RoomId) {
    var c = confirm("Vill du ta bort rummet?");
    if (c === true) {
        $.post('/Admin/DeleteRoom/' + RoomId, function (data) {
            if (data === '1')
                $('tr#' + RoomId).fadeOut();
        });
    }
}


function hideSettings() {
    $.post('/Home/HideSettings/', function (data) {
        if (data === 'Admin') {
            $('#menusetting').fadeOut();
        }
        else {
            $('#menusetting').fadeIn();
        }
    });

}
function deleteUser(UserId) {
    var c = confirm("Vill du ta bort användaren?");
    if (c === true) {
        $.post('/Admin/DeleteUser/' + UserId, function (data) {
            if (data === '1')
                $('tr#' + UserId).fadeOut();
        });
    }
}