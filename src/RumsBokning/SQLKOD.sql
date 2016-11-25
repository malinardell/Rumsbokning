select
    name, so.*
from
    sys.sysobjects as so
where
    --name like 'room%'
    uid = 5 and
    xtype = 'U'

select
    r.Name,
    ' - - - ',
    ru.*,
    ' - - - ',
    u.FirstName, u.LastName
    ' - - - ',
    rt.R_Id, rt.StartTime, rt.EndTime
from
    aaa.Room as r
inner join
    aaa.RoomUsers as ru on
    ru.R_Id = r.Id
inner join
    aaa.Users as u on
    ru.U_Id = u.ID
inner join
    aaa.RoomTime as rt on
    rt.R_Id = r.Id and
    rt.U_Id = u.ID
where
    r.Id = 3

select
    *
from
    aaa.RoomTime

select
    *
from
    aaa.Users

select
    rt.*,
    ' - - - ',
    u.*,
    ' - - - ',
    r.*
from
    aaa.RoomTime as rt
inner join
    aaa.Users as u on
    rt.U_Id = u.ID
inner join
    aaa.Room as r on
    rt.R_Id = r.Id
where
    rt.R_Id = 3

select
    *
from
    aaa.Users

select
    *
from
    aaa.RoomUsers
where
    aaa.RoomUsers.R_Id = 3

select
    *
from
    aaa.RoomTime

insert into
    aaa.RoomUsers(U_Id, R_Id)
values
    (N'31e60277-9665-4a3b-b0c4-867b48dda258', 2),
    (N'31e60277-9665-4a3b-b0c4-867b48dda258', 3)

insert into
    aaa.RoomUsers(U_Id, R_Id)
values
    (N'2ac58fb9-fe36-43a3-a9ca-fbbc5de9b738', 2),
    (N'2ac58fb9-fe36-43a3-a9ca-fbbc5de9b738', 3)
    
insert into
    aaa.RoomTime
values
    ('2016-11-25 10:00', '2016-11-25 11:00', 2, N'2ac58fb9-fe36-43a3-a9ca-fbbc5de9b738'),
    ('2016-11-25 11:00', '2016-11-25 12:00', 2, N'2ac58fb9-fe36-43a3-a9ca-fbbc5de9b738'),
    ('2016-11-25 10:00', '2016-11-25 11:00', 3, N'31e60277-9665-4a3b-b0c4-867b48dda258'),
    ('2016-11-25 11:00', '2016-11-25 12:00', 3, N'31e60277-9665-4a3b-b0c4-867b48dda258'),
    ('2016-11-25 13:00', '2016-11-25 14:00', 3, N'31e60277-9665-4a3b-b0c4-867b48dda258')


insert into
    aaa.RoomTime
values
    ('2016-11-25 16:00', '2016-11-25 18:00', 3, N'2ac58fb9-fe36-43a3-a9ca-fbbc5de9b738')
