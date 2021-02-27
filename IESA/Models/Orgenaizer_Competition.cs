using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IESA.Models
{
    public class Orgenaizer_Competition
    {
        int orgenaizerid;
        int competitionid;
        DateTime date1;
        TimeSpan time1; //Check Definition*
        int rank1;
        int score;

        public Orgenaizer_Competition(int orgenaizerid, int competitionid, DateTime date1, TimeSpan time1, int rank1, int score)
        {
            Orgenaizerid = orgenaizerid;
            Competitionid = competitionid;
            Date1 = date1;
            Time1 = time1;
            Rank1 = rank1;
            Score = score;
        }

        public Orgenaizer_Competition() { }

        public int Orgenaizerid { get => orgenaizerid; set => orgenaizerid = value; }
        public int Competitionid { get => competitionid; set => competitionid = value; }
        public DateTime Date1 { get => date1; set => date1 = value; }
        public TimeSpan Time1 { get => time1; set => time1 = value; }
        public int Rank1 { get => rank1; set => rank1 = value; }
        public int Score { get => score; set => score = value; }


        //#Functions Area








    }
}