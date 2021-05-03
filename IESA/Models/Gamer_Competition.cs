using IESA.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IESA.Models
{
    public class Gamer_Competition
    {
        int gamerid;
        int competitionid;
        int managerid;
        DateTime date1;
        TimeSpan time1; //Check Definition*
        int rank1;
        int score;

        public Gamer_Competition(int gamerid, int competitionid, int managerid, DateTime date1, TimeSpan time1, int rank1, int score)
        {
            Gamerid = gamerid;
            Competitionid = competitionid;
            Managerid = managerid;
            Date1 = date1;
            Time1 = time1;
            Rank1 = rank1;
            Score = score;
        }

        public Gamer_Competition() { }

        public int Gamerid { get => gamerid; set => gamerid = value; }
        public int Competitionid { get => competitionid; set => competitionid = value; }
        public int Managerid { get => managerid; set => managerid = value; }
        public DateTime Date1 { get => date1; set => date1 = value; }
        public TimeSpan Time1 { get => time1; set => time1 = value; }
        public int Rank1 { get => rank1; set => rank1 = value; }
        public int Score { get => score; set => score = value; }


        //#Functions Area





    }
}