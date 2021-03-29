select *
from Orgenaizers

update Orgenaizers
set status1=1
where userid=20000003 

SELECT Gamers.userid, Gamers.email, Gamers.password1 FROM Gamers 
WHERE Gamers.email = 'shaydavid2@gmail.com'
UNION 
SELECT Orgenaizers.userid, Orgenaizers.email, Orgenaizers.password1 FROM Orgenaizers 
WHERE Orgenaizers.email = 'shaydavid2@gmail.com'
UNION 
SELECT Managers.userid, Managers.email, Managers.password1 FROM Managers 
WHERE Managers.email = 'shaydavid2@gmail.com'
