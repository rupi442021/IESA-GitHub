select *
from Orgenaizers

select *
from Competitions

select *
from Orgenaizer_Competition

select *
from Competition_Game

Insert INTO GamesCategories(categoryname, status1)
Values ('FIFA', 1)

INSERT INTO Orgenaizer_Competition (orgenaizerid, competitionid, date1, time1)
VALUES (20000000, 1, getdate(), '15:30:00');