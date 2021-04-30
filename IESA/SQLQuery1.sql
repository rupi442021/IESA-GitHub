select *
from Gamer_Competition


SELECT GamesCategories.categoryid, GamesCategories.categoryname, Gamers.firstname + ' ' + Gamers.lastname AS 'fullname', Gamers.nickname, Gamers.userid, SUM (Gamer_Competition.score) as 'score' , Gamer_Competition.status1
FROM Gamer_Competition
inner join Gamers ON Gamer_Competition.gamerid = Gamers.userid
inner join Competitions ON Gamer_Competition.competitionid = Competitions.competitionid
inner join Competition_Game ON Competitions.competitionid = Competition_Game.competitionid
inner join GamesCategories ON Competition_Game.categoryid = GamesCategories.categoryid
where  Gamer_Competition.status1 = 1 
GROUP BY Gamers.userid , GamesCategories.categoryid , GamesCategories.categoryname , Gamers.firstname , Gamers.lastname ,  Gamers.nickname , Gamer_Competition.status1
order by categoryid, score desc



update Gamer_Competition
set managerid=10000001, date1=getdate(), time1=getdate(), score=30, status1=1
where gamerid=30000039 and competitionid=50

update Gamer_Competition
set managerid=10000001, date1=getdate(), time1=getdate(), score=10, status1=1
where gamerid=30000036 and competitionid=50


select *
from Gamer_Competition
order by competitionid, rank1 desc


select *
from competitions

insert into Gamer_Competition (gamerid, competitionid, managerid, date1, time1, rank1, score, status1)
values (30000041, 20, 10000001, getdate(), '10:00:00', 1, 20, 0)

update Competitions
set competitionstatus=0
where competitionid=48

DELETE FROM Competitions
WHERE competitionid=48

select *
from Competitions inner join Orgenaizer_Competition on Competitions.competitionid = Orgenaizer_Competition.competitionid
where orgenaizerid = 20000000



select *
from Gamers

select *
from Competitions