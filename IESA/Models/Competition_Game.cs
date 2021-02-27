using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IESA.Models
{
    public class Competition_Game
    {
        int competitionid;
        int categoryid;

        public Competition_Game(int competitionid, int categoryid)
        {
            Competitionid = competitionid;
            Categoryid = categoryid;
        }

        public Competition_Game() { }

        public int Competitionid { get => competitionid; set => competitionid = value; }
        public int Categoryid { get => categoryid; set => categoryid = value; }


        //#Functions Area








    }
}