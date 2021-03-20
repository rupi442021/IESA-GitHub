select *
from gamers
where gamers.status1=1 and gamers.license=1



UPDATE gamers
SET license = '1' , outofdate= '31/12/2021', status1 = 1
WHERE userid = 30000001;

select *
from Orgenaizers