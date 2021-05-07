select *
from Orgenaizers

select *
from Competitions
where competitionid=19

update gamers
set license='1'
where userid=30000043

select *
from Orgenaizer_Competition

select *
c gamers

select *
from Competitions
where competitionstatus='2'

update competitions
set competitions.competitionstatus='2'
where competitionid = 48

update competitions set competitionstatus='2' where competitionid=52

SELECT * 
FROM competitions inner join gamer_competition on competitions.competitionid = Gamer_Competition.competitionid 
WHERE gamerid = 30000039 and competitionstatus = '2'

select *
from gamer_competition
where competitionid=48



SELECT DISTINCT Competitions.competitionid, Competitions.competitionname, Competitions.competitionstatus, Competitions.ispro,
Orgenaizers.firstname + ' ' + Orgenaizers.lastname AS 'orgenaizername', 
Competitions.startdate, Competitions.enddate,Competitions.logo, Competitions.address1, Competitions.isonline 
FROM Competitions inner join Orgenaizer_Competition ON Competitions.competitionid = Orgenaizer_Competition.competitionid 
and Competitions.competitionstatus = '1' or Competitions.competitionstatus = '5' inner join Orgenaizers 
ON Orgenaizer_Competition.orgenaizerid = Orgenaizers.userid

SELECT * FROM Competitions WHERE status1=1 and competitionstatus = '2'

select *
from GamesCategories

select *
from Gamer_Competition

select *
from Competition_Game

select *
from Competitions inner join competition_game 
on competitions.competitionid =  competition_game.competitionid 
inner join gamer_competition 
on competitions.competitionid = Gamer_Competition.competitionid
where gamerid = 30000043 and competitionstatus = '2'


select *
from competitions inner join gamer_competition on competitions.competitionid = Gamer_Competition.competitionid
where competitionstatus='7' and gamerid= 30000043 and competitions.status1='1'

SELECT GamesCategories.categoryid, GamesCategories.categoryname, Gamers.firstname + ' '
+ Gamers.lastname AS 'fullname', Gamers.nickname, Gamers.userid, Gamers.picture, 
SUM(Gamer_Competition.score) as 'score' 
FROM Gamer_Competition inner join Gamers ON Gamer_Competition.gamerid = Gamers.userid inner join Competitions 
ON Gamer_Competition.competitionid = Competitions.competitionid inner join Competition_Game 
ON Competitions.competitionid = Competition_Game.competitionid inner join GamesCategories 
ON Competition_Game.categoryid = GamesCategories.categoryid 
where Gamer_Competition.status1 = 1 and GamesCategories.status1 = 1 and Competitions.ispro=1
GROUP BY Gamers.userid, GamesCategories.categoryid , GamesCategories.categoryname , Gamers.firstname , Gamers.lastname ,  Gamers.nickname , Gamers.picture 
order by categoryid, score desc

select *
from gamers

select Competitions.competitionid 'Competition ID',
competitions.competitionname 'Competition Name',
competitions.startdate 'Start Date', 
competitions.console 'Console',
Gamer_Competition.rank1 'Rank', 
Gamer_Competition.score 'Score' 
from competitions inner join gamer_competition on competitions.competitionid=gamer_competition.competitionid inner join gamers on Gamer_Competition.gamerid=gamers.userid where competitions.status1=1 and gamer_competition.status1=1 and gamerid=30000043

select *
from competitions
where competitions.competitionstatus='2'
SELECT distinct Competitions.competitionid, Competitions.competitionname, Competitions.competitionstatus, Competitions.ispro, Orgenaizers.firstname + ' ' + Orgenaizers.lastname AS 'orgenaizername', Competitions.startdate, Competitions.enddate FROM Competitions inner join Orgenaizer_Competition ON Competitions.competitionid = Orgenaizer_Competition.competitionid and Competitions.competitionstatus = '1' or Competitions.competitionstatus = '5' inner join Orgenaizers ON Orgenaizer_Competition.orgenaizerid = Orgenaizers.userid

select Competitions.competitionid 'Competition ID', competitions.competitionname 'Competition Name', competitions.startdate 'Start Date', competitions.console 'Console', Gamer_Competition.rank1 'Rank', Gamer_Competition.score 'Score', gamesCategories.categoryname 'Category Name' from competitions inner join gamer_competition on competitions.competitionid=gamer_competition.competitionid inner join gamers on Gamer_Competition.gamerid=gamers.userid inner join competition_game on competitions.competitionid = competition_game.competitionid inner join GamesCategories on gamesCategories.categoryid = Competition_Game.categoryid where competitions.status1=1 and gamer_competition.status1=1 and gamerid=30000043