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
        public List<GamesCategories> Get() //Add_New_Competition.html - method OO1
        {
            GamesCategories gamecategories = new GamesCategories();
            return gamecategories.Read();
        }







    }
}