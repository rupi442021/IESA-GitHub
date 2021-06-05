update 

if exists (Select 1 from Gamers where Gamers.email = 'Shaydavid2@gmail.com')
    Select * from Gamers where Gamers.email = 'Shaydavid2@gmail.com'
else if exists (Select 1 from Orgenaizers where Orgenaizers.email = 'Shaydavid2@gmail.com')
    Select * from Orgenaizers where Orgenaizers.email = 'Shaydavid2@gmail.com'
else if exists (Select 1 from Managers where Managers.email = 'Shaydavid2@gmail.com')
    Select * from Managers where Managers.email = 'Shaydavid2@gmail.com'
else
    print 'no records returned from tables'


update Orgenaizers set password1= 'Aa12345' where email = 'Shaydavid2@gmail.com' update Managers set password1= 'Aa12345' where email = 'Shaydavid2@gmail.com' update Gamers set password1= 'Aa12345' where email = 'Shaydavid2@gmail.com'
