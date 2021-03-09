using IESA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IESA.Controllers
{
    public class CompetitionsController : ApiController
    {
        static public Competitions Pcompetitions;
        public int Get() //Add_New_Competitions.html - method OO1
        {
            Competitions newid = new Competitions();
            return newid.getCompetitionId();
        }

        public void Post([FromBody] Competitions Competition) //Sign_Up.html - method OG4
        {
            Competition.InsertCompetition();
        }

        [HttpPost]
        [Route("api/Competitions/mark2")]
        public void Postc([FromBody] Competitions p1) //Add_New_Competitions.html - 
        {
        
                      Pcompetitions = new Competitions();
                      Pcompetitions = p1;
             
        }

        [HttpGet]
        [Route("api/Competitions/mark1")]
        public Competitions Getc() //Add_New_Competitions.html - 
        {
            return (Pcompetitions);
        }   


    }
}