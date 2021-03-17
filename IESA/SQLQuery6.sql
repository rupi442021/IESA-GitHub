INSERT INTO Orgenaizer_Competition VALUES (20000000, 12, getdate(), '11:10:10')

select *
from Orgenaizer_Competition

select * from Competitions inner join Orgenaizer_Competition 
ON Competitions.competitionid = Orgenaizer_Competition.competitionid 
where Orgenaizer_Competition.orgenaizerid=20000000

select *
from Competitions

ALTER TABLE Competitions
ADD CONSTRAINT df_Competitions
DEFAULT -1 FOR numofparticipants;

drop table Gamer_Competition
drop table Orgenaizer_Competition
drop table Competition_Game
drop table Competitions

CREATE TABLE Competitions(
	competitionid int IDENTITY(1 , 1) PRIMARY KEY,
	competitionname nvarchar (100) NOT NULL,
	address1 nvarchar (150),
	banner nvarchar (150),
	logo nvarchar (150),
	prize1 nvarchar (20),
	prize2 nvarchar (20),
	prize3 nvarchar (20),
	linkforregistration nvarchar(1000) NOT NULL,
	lastdateforregistration nvarchar (50) NOT NULL,
	body nvarchar (1000) NOT NULL,
	startdate nvarchar (50) NOT NULL,
	enddate nvarchar (50) NOT NULL,
	starttime time,
	endtime time,
	ispro bit DEFAULT '0' NOT NULL,
	discord nvarchar(150),
	console nvarchar (20) NOT NULL,
	isiesa bit DEFAULT '0' NOT NULL,
	linkforstream nvarchar (1000),
	numofparticipants smallint,
	competitionstatus char DEFAULT 'S', 
	status1 bit DEFAULT 1,
	ispayment bit DEFAULT 0,
	isonline bit DEFAULT 0,
	startcheckIn time,
	endcheckIn time
	)

	create table Gamer_Competition(
	gamerid int foreign key references Gamers(userID),
	competitionid int foreign key references Competitions (competitionID),
	managerid int foreign key references Managers(userID),
	date1 nvarchar (50) NOT NULL,
	time1 time NOT NULL,
	rank1 smallint NOT NULL,
	score smallint NOT NULL,
	constraint pk1 PRIMARY KEY(gamerID,competitionID)
)

create table Orgenaizer_Competition(
	orgenaizerid int foreign key references Orgenaizers(userID),
	competitionid int foreign key references Competitions(competitionID),
	date1 nvarchar (50) NOT NULL,
	time1 time NOT NULL,
	constraint pk2 PRIMARY KEY(orgenaizerID,competitionID)
)
create table Competition_Game(
	competitionid int foreign key references Competitions(competitionID),
	categoryid int foreign key references GamesCategories(categoryID),
	constraint pk4 PRIMARY KEY(competitionID,categoryID)
)

update competitions
set competitionstatus = 4
where competitionid=1
update competitions
set competitionstatus = 5
where competitionid=2
update competitions
set competitionstatus = 6
where competitionid=3
update competitions
set competitionstatus = 7
where competitionid=4
update competitions
set competitionstatus = 8
where competitionid=5