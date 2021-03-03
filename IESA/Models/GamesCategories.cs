using IESA.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IESA.Models
{
    public class GamesCategories
    {
        int categoryid;
        string categoryname;
        int status1; //0 or 1

        public GamesCategories(int categoryid, string categoryname, int status1)
        {
            Categoryid = categoryid;
            Categoryname = categoryname;
            Status1 = status1;
        }

        public GamesCategories() { }

        public int Categoryid { get => categoryid; set => categoryid = value; }
        public string Categoryname { get => categoryname; set => categoryname = value; }
        public int Status1 { get => status1; set => status1 = value; }


        //#Functions Area

        public List<GamesCategories> Read() //Sign_Up.html - method OO1
        {
            DBServices dbs = new DBServices();
            List <GamesCategories> gamecategories = dbs.GetGameCategoriesr();
            return gamecategories;
        }
        


    }
}