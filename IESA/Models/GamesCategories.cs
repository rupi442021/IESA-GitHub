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


        //---Add_New_Competition.html--- *Open*

        public List<GamesCategories> Read() //Add_New_Competition.html - method MC1
        {
            DBServices dbs = new DBServices();
            List <GamesCategories> gamecategories = dbs.GetGameCategoriesr();
            return gamecategories;
        }


        //---Add_New_Competition.html--- *Close*


        //---Add_Game_Category.html--- *Close*


        //---Database.html--- *Open*


        public List<GamesCategories> ReadCategoriesDB() //Database.html - method OD8
        {
            DBServices dbs = new DBServices();
            List<GamesCategories> calist = dbs.ReadCategoriesDBSQL();
            return calist;
        }


        public void changeStatus(int isactive, int categoryid) //Database.html - method OD9
        {
            DBServices dbs = new DBServices();
            dbs.changeStatusSQL(isactive, categoryid);
        }

        public void editCategory(string newcategory, int categoryid) //Database.html - method OD10
        {
            DBServices dbs = new DBServices();
            dbs.editCategorySQL(newcategory, categoryid);
        }

        //public int addgameCategory(string newcategoryadd) //Database.html - method OD11
        //{
        //    DBServices dbs = new DBServices();
        //    return dbs.addgameCategorySQL(newcategoryadd);
        //}

        public void addgameCategory() //Database.html - method OD11
        {
            DBServices dbs = new DBServices();
            dbs.addgameCategorySQL(this);
        }


        //---Database.html--- *Close*


    }
}