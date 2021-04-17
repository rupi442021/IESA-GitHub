﻿using IESA.Models;
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


        //---Add_Game_Category.html--- *Open*

        public List<GamesCategories> Get() //Add_Game_Category.html - method MC1
        {
            GamesCategories gamecategories = new GamesCategories();
            return gamecategories.Read();
        }

        public int Post([FromBody] GamesCategories GameC) //Add_Game_Category.html - method MC2
        {
            return GameC.InsertGameCategory();
        }

        [HttpPut]
        [Route("api/GamesCategories/mark6")]
        public void Put(int id, string status) //Add_Game_Category.html - method MC3
        {
            GamesCategories c = new GamesCategories();
            c.setNotactive(id, status);

        }

        [HttpPut]
        [Route("api/GamesCategories/mark7")]
        public void PUTe(int id, string UpdateCategoryName) //Add_Game_Category.html - method MC4
        {
            GamesCategories c = new GamesCategories();
            c.ChangeName(id, UpdateCategoryName);

        }


        //---Add_Game_Category.html--- *Close*


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

        //---Database.html--- *Close*


    }
}