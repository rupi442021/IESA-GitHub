update Competitions set Competitionstatus = '4' where Competitionstatus='2' and CONVERT(date,enddate,103) > getdate()



select * from Competitions inner join Orgenaizer_Competition
ON Competitions.competitionid = Orgenaizer_Competition.competitionid
where Orgenaizer_Competition.orgenaizerid=20000000 and Competitions.status1 = 1
order by CONVERT(date,enddate,103)

