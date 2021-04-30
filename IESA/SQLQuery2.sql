select *
from Gamers 

UPDATE Competitions set Competitionstatus = '4' where Competitionstatus='2' and
CONVERT(date,enddate,103) > getdate() 
UPDATE Competitions set Competitionstatus = '1' where competitionid = '43'