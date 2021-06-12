select 1 *
from Gamers,Orgenaizers, Managers
where Gamers.email = 'Shaydavid2@gmail.com' or Orgenaizers.email = 'Shaydavid2@gmail.com' or Managers.email = 'Shaydavid2@gmail.com'


select *
from Gamers
where email = 'Shaydavid2@gmail.com'

select *
from Orgenaizers
where email = 'Shaydavid2@gmail.com'


if exists (Select 1 from Gamers where Gamers.email = 'Shaydavid2@gmail.com')
    Select * from Gamers where Gamers.email = 'Shaydavid2@gmail.com'
else if exists (Select 1 from Orgenaizers where Orgenaizers.email = 'Shaydavid2@gmail.com')
    Select * from Orgenaizers where Orgenaizers.email = 'Shaydavid2@gmail.com'
else if exists (Select 1 from Managers where Managers.email = 'Shaydavid2@gmail.com')
    Select * from Managers where Managers.email = 'Shaydavid2@gmail.com'
else
    print 'no records returned from tables'


update if exists (Select 1 from Gamers where Gamers.email = 'Shaydavid2@gmail.com')
    Select * from Gamers where Gamers.email = 'Shaydavid2@gmail.com'
else if exists (Select 1 from Orgenaizers where Orgenaizers.email = 'Shaydavid2@gmail.com')
    Select * from Orgenaizers where Orgenaizers.email = 'Shaydavid2@gmail.com'
else if exists (Select 1 from Managers where Managers.email = 'Shaydavid2@gmail.com')
    Select * from Managers where Managers.email = 'Shaydavid2@gmail.com'
else
    print 'no records returned from tables'
set password1 = 'aa123456'



select *
from manager