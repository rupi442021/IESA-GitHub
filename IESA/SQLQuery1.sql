update competitions
set competitionstatus = 4
where competitionid=1
update competitions
set competitionstatus = 5
where competitionid=2
update competitions
set competitionstatus = 6
where competitionid = 3
update competitions
set competitionstatus = 8
where competitionid = 7
update competitions
set competitionstatus = 5
where competitionid = 12

select competitionstatus
from competitions

select competitionname, competitionstatus, status1
from Competitions inner join Orgenaizer_Competition ON Competitions.competitionid = Orgenaizer_Competition.competitionid where Orgenaizer_Competition.orgenaizerid=20000000 and Competitions.status1=1 

ALTER TABLE competitions
ADD CONSTRAINT df_competitionstatis
DEFAULT 1 FOR competitionstatus;

ALTER TABLE Competitions
DROP COLUMN competitionstatus 