select * from Gamer_Competition

UPDATE Competitions
SET competitiontype = 2
WHERE isonline = 0;

UPDATE Gamer_Competition
SET status1 = 0
WHERE competitionid = 19;