select *
from Competitions

ALTER TABLE Competitions
ADD gamecategoryid int;

ALTER TABLE Competitions
DROP COLUMN gamecategoryid;

select categoryid from Competition_Game where competitionid = 17

select *
from Competitions where competitionid =16

UPDATE Competition_Game set categoryid = 5 where competitionid = 16;

