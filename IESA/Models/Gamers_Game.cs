using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IESA.Models
{
    public class Gamers_Game
    {
        int gamerid;
        int categoryid;
        int rank1;
        int score;

        public Gamers_Game(int gamerid, int categoryid, int rank1, int score)
        {
            Gamerid = gamerid;
            Categoryid = categoryid;
            Rank1 = rank1;
            Score = score;
        }

        public Gamers_Game() { }

        public int Gamerid { get => gamerid; set => gamerid = value; }
        public int Categoryid { get => categoryid; set => categoryid = value; }
        public int Rank1 { get => rank1; set => rank1 = value; }
        public int Score { get => score; set => score = value; }


        //#Functions Area








    }
}