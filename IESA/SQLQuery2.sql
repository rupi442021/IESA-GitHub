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
from gamers

select *
from gamer_competition
where competitionid = 52

update gamer_competition
set score = 0, status1=0
where competitionid = 52

update competitions set competitionstatus='2' where competitionid=52


select *
from competitions
where status1='1'

select *
from GamesCategories

select *
from Gamer_Competition

select *
from Competition_Game

select *
from competitions inner join competition_game on competitions.competitionid =  competition_game.competitionid inner join gamer_competition on competitions.competitionid = Gamer_Competition.competitionid
where gamerid = 30000043 and competitionstatus = '2'


select *
from competitions inner join gamer_competition on competitions.competitionid = Gamer_Competition.competitionid
where competitionstatus='7' and gamerid= 30000043 and competitions.status1='1'