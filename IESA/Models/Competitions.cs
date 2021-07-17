using IESA.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IESA.Models
{
    public class Competitions
    {
        int competitionid;
        string competitionname;
        int isonline; //0 or 1
        string address1;
        string banner; //nvarchar (1) - Check
        string logo; //nvarchar (1) - Check
        string prize1;
        string prize2;
        string price3;
        string linkforregistration;
        DateTime lastdateforregistration;
        string body; //nvarchar (1) - Check
        DateTime startdate;
        DateTime enddate;
        TimeSpan startime; //Check Definition*
        TimeSpan endtime; //Check Definition*
        int ispro; //0 or 1
        string discord; //nvarchar (1) - Check
        string console;
        int isiesa; //0 or 1
        string linkforstream;
        int numofparticipants;
        string competitionstatus;
        int status1; //0 or 1
        int isPayment; //0 or 1
        TimeSpan startcheckin;
        TimeSpan endcheckin;
        string gamecategory;
        List<Gamers> glist;
        string kind;

        public Competitions(int competitionid, string competitionname, int isonline, string address1, string banner, string logo, string prize1, string prize2, string price3, string linkforregistration, DateTime lastdateforregistration, string body, DateTime startdate, DateTime enddate, TimeSpan startime, TimeSpan endtime, int ispro, string discord, string console, int isiesa, string linkforstream, int numofparticipants, string competitionstatus, int status1, int isPayment, TimeSpan startcheckin, TimeSpan endcheckin, string gamecategory, List<Gamers> glist , string kind)
        {
            Competitionid = competitionid;
            Competitionname = competitionname;
            Isonline = isonline;
            Address1 = address1;
            Banner = banner;
            Logo = logo;
            Prize1 = prize1;
            Prize2 = prize2;
            Price3 = price3;
            Linkforregistration = linkforregistration;
            Lastdateforregistration = lastdateforregistration;
            Body = body;
            Startdate = startdate;
            Enddate = enddate;
            Startime = startime;
            Endtime = endtime;
            Ispro = ispro;
            Discord = discord;
            Console = console;
            Isiesa = isiesa;
            Linkforstream = linkforstream;
            Numofparticipants = numofparticipants;
            Competitionstatus = competitionstatus;
            Status1 = status1;
            IsPayment = isPayment;
            Startcheckin = startcheckin;
            Endcheckin = endcheckin;
            Gamecategory = gamecategory;
            Glist = glist;
            Kind = kind;
        }

        public int Competitionid { get => competitionid; set => competitionid = value; }
        public string Competitionname { get => competitionname; set => competitionname = value; }
        public int Isonline { get => isonline; set => isonline = value; }
        public string Address1 { get => address1; set => address1 = value; }
        public string Banner { get => banner; set => banner = value; }
        public string Logo { get => logo; set => logo = value; }
        public string Prize1 { get => prize1; set => prize1 = value; }
        public string Prize2 { get => prize2; set => prize2 = value; }
        public string Price3 { get => price3; set => price3 = value; }
        public string Linkforregistration { get => linkforregistration; set => linkforregistration = value; }
        public DateTime Lastdateforregistration { get => lastdateforregistration; set => lastdateforregistration = value; }
        public string Body { get => body; set => body = value; }
        public DateTime Startdate { get => startdate; set => startdate = value; }
        public DateTime Enddate { get => enddate; set => enddate = value; }
        public TimeSpan Startime { get => startime; set => startime = value; }
        public TimeSpan Endtime { get => endtime; set => endtime = value; }
        public int Ispro { get => ispro; set => ispro = value; }
        public string Discord { get => discord; set => discord = value; }
        public string Console { get => console; set => console = value; }
        public int Isiesa { get => isiesa; set => isiesa = value; }
        public string Linkforstream { get => linkforstream; set => linkforstream = value; }
        public int Numofparticipants { get => numofparticipants; set => numofparticipants = value; }
        public string Competitionstatus { get => competitionstatus; set => competitionstatus = value; }
        public int Status1 { get => status1; set => status1 = value; }
        public int IsPayment { get => isPayment; set => isPayment = value; }
        public TimeSpan Startcheckin { get => startcheckin; set => startcheckin = value; }
        public TimeSpan Endcheckin { get => endcheckin; set => endcheckin = value; }
        public string Gamecategory { get => gamecategory; set => gamecategory = value; }
        public List<Gamers> Glist { get => glist; set => glist = value; }
        public string Kind { get => kind; set => kind = value; }

        public Competitions() { }


        //#Functions Area


        public int getCompetitionId() //Add_New_Competition.html - method OO1
        {
            DBServices dbs = new DBServices();
            int newid = dbs.getCompetitionId();
            return newid;
        }

        public void InsertCompetition() //Add_New_Competition.html
        {
            DBServices dbs = new DBServices();
            dbs.InsertCompetition(this);
        }

        public List<Competitions> Read(int OID) //Orgenaizer_Main_Page.html - method SC1
        {
            DBServices dbs = new DBServices();
            List<Competitions> lCompetitions = dbs.GetOrgenaizerCompetitions(OID);
            return lCompetitions;
        }


        public List<Competitions> ReadGamer(int GID)// gets all the compation of a specifical gamer. // Gamer.html
        {
            DBServices dbs = new DBServices();
            List<Competitions> lCompetitions = dbs.GetGamersCompetitions(GID);
            return lCompetitions;
        }


        //---Manager_Main_Page.html--- *Open*


        public List<Competitions> ReadCompetitionsM() //Manager_Main_Page.html - method OM8
        {
            DBServices dbs = new DBServices();
            List<Competitions> clist = dbs.ReadCompetitionsMSQL();
            return clist;
        }


        //---Manager_Main_Page.html--- *Close*


        //---Database.html--- *Open*

        public void DeleteCompetition(int todeleteid) //Database.html - method OD3
        {
            DBServices dbs = new DBServices();
            dbs.DeleteCompSQL(todeleteid);
        }

        public List<Competitions> ReadCompetitionsDB() //Database.html - method OD4
        {
            DBServices dbs = new DBServices();
            List<Competitions> clist = dbs.ReadCompetitionsDBSQL();
            return clist;
        }

        public List<Competitions> GetRanks() //Database.html - method OD12
        {
            DBServices dbs = new DBServices();
            List<Competitions> rankslist = dbs.GetRanksSQL();
            return rankslist;
        }

        public List<Competitions> GetCompetitionListToDelete(int CategoryID) //DataBase.html - method SM4
        {
            DBServices dbs = new DBServices();
            List<Competitions> cListToDelete = dbs.GetCompetitionsByCategoryToDelete(CategoryID);
            return cListToDelete;
        }

        //---Database.html--- *Close*


        //---Gamer_Main_Page.html--- *Open*



        public List<Competitions> GetRelevantCompetitionsForGamer(int GID) //Gamer_Main_Page.html - SG08
        {
            DBServices dbs = new DBServices();
            List<Competitions> RelevantCompetitionsList = dbs.GetRelevantCompetitions(GID);
            return RelevantCompetitionsList;
        }

        public List<Competitions> GetCompetitionsForGamer(int GID) //Gamer_Main_Page.html - method SG09
        {
            DBServices dbs = new DBServices();
            List<Competitions> CompetitionsListForGamer = dbs.GetCompetitionsSQL(GID);
            return CompetitionsListForGamer;
        }
        public List<Competitions> GetCompetitionsBystatus(string Cstatus) //Gamer_Main_Page.html - method SG10
        {
            DBServices dbs = new DBServices();
            List<Competitions> CompetitionsListForGamer = dbs.GetCompetitionsByStatus(Cstatus);
            return CompetitionsListForGamer;
        }


        //---Gamer_Main_Page.html--- *Close*

        public Competitions ReadOneCompetition(int CId)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadOneCompetition(CId);
        }

        //---Edit_Competition.html--- *Open*

        public GamesCategories Getcategory(int CId) //Edit_Competitions.html - method SC6
        {
            DBServices dbs = new DBServices();
            return dbs.Getcategory(CId);
        }

        public void updateCompetitonDetails(int CId, Competitions c1) //Manager_Main_Page.html - method OM6
        {
            DBServices dbs = new DBServices();
            dbs.updateCompetitonDetails(CId, c1);
        }


        //---Edit_Competition.html--- *Close*


        //---Profile_View_Manager.html--- *Open*


        public List<Competitions> G_Read_View_Competitions(int idtoserver) //Profile_View_Manager.html - method OC1
        {
            DBServices dbs = new DBServices();
            List<Competitions> clist = dbs.G_Read_View_CompetitionsSQL(idtoserver);
            return clist;
        }

        public List<Competitions> O_Read_View_Competitions(int idtoserver) //Profile_View_Manager.html - method OC2
        {
            DBServices dbs = new DBServices();
            List<Competitions> clist = dbs.O_Read_View_CompetitionsSQL(idtoserver);
            return clist;
        }


        //---Profile_View_Manager.html--- *Close*


        //---Competitions.html--- *Open*


        public List<Competitions> GetSiteCompetitions(string sqlcommand) //Competitions.html - method OCS1
        {
            DBServices dbs = new DBServices();
            List<Competitions> clist = dbs.GetSiteCompetitionsSQL(sqlcommand);
            return clist;
        }


        //---Competitions.html--- *Close*


        //---index.html--- *Open*


        public List<Competitions> ReadCompetitionsindex() //index.html - method OMI2
        {
            DBServices dbs = new DBServices();
            List<Competitions> comlist = dbs.ReadCompetitionsindexSQL();
            return comlist;
        }


        //---index.html--- *Close*


        //---ReactClientSide---*Open*


        public List<Competitions> GetCompetitonL() //Manager_Main_Page.html - method OM8
        {
            DBServices dbs = new DBServices();
            List<Competitions> clist = dbs.GetCompetitonL();
            return clist;
        }


        //---ReactClientSide---*Close*








    }
}