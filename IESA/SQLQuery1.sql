select *
from Competitions
where competitionstatus = '2'

declare @NotADate nvarchar(255) = '01 Sep 2011'

select CAST(@NotADate as datetime)

declare @NotADate2 nvarchar(255)

select @NotADate2 = CONVERT(datetime, @NotADate, 112)

select @NotADate2, CONVERT(datetime, @NotADate, 112)


update Competitions
set competitionstatus='2'
where competitionid = 3
where competitionstatus='2' and convert(date, enddate, 103) < getdate()

update Competitions
set competitionstatus = '3'
where competitionid = 5

select * from Competitions inner join Orgenaizer_Competition 
ON Competitions.competitionid = Orgenaizer_Competition.competitionid 
where Orgenaizer_Competition.orgenaizerid=20000000 and Competitions.status1 = 1

select *
from Orgenaizer_Competition

insert into Orgenaizer_Competition (orgenaizerid, competitionid, date1, time1)
values (20000000 , 3 , '-2047', '03:56:29.0000000')

select * from Competitions inner join Orgenaizer_Competition 
ON Competitions.competitionid = Orgenaizer_Competition.competitionid 
where Orgenaizer_Competition.orgenaizerid= 20000000 and Competitions.status1 = 1 
order by convert(date, Competitions.enddate, 103)  DESC