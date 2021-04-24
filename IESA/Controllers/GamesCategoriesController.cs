using IESA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IESA.Controllers
{
    public class GamesCategoriesController : ApiController
    {


        //---Add_New_Competition.html--- *Open*

        public List<GamesCategories> Get() //Add_New_Competition.html - method MC1
        {
            GamesCategories gamecategories = new GamesCategories();
            return gamecategories.Read();
        }


        //---Add_New_Competition.html--- *Close*


        //---Database.html--- *Open*


        [HttpGet] //Database.html - method OD8
        [Route("api/GamesCategories/tablecaDB")]
        public List<GamesCategories> GetCategoriesDB()
        {
            GamesCategories gameslist = new GamesCategories();
            return gameslist.ReadCategoriesDB();
        }

        [HttpPut] //Database.html - method OD9
        [Route("api/GamesCategories/tablecstatus")]
        public void changeStatus(int isactive, int categoryid)
        {
            GamesCategories categorystatus = new GamesCategories();
            categorystatus.changeStatus(isactive, categoryid);
        }

        [HttpPut] //Database.html - method OD10
        [Route("api/GamesCategories/editcname")]
        public void changeStatus(string newcategory, int categoryid)
        {
            GamesCategories editname = new GamesCategories();
            editname.editCategory(newcategory, categoryid);
        }


        public void Post([FromBody] GamesCategories category) //Database.html - method OD11
        {
            category.addgameCategory();
        }


        //---Database.html--- *Close*


    }
}