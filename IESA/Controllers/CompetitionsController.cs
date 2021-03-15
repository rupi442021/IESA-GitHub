using IESA.Models;
using IESA.Models.DAL;
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

        public void Post([FromBody] Competitions Competition) //Add_New_Competitions.html 
        {
            Competition.InsertCompetition();
        }

 
        public void Post(int cID, int gcID) //Add_New_Competitions.html 
        {
            DBServices dbs = new DBServices();
            dbs.InsertGameInC(cID, gcID);
        }
        [HttpPost]
        [Route("api/Competitions/mark2")]

        public void Postc([FromBody] Competitions p1) //Add_New_Competitions.html - Preview
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

        [HttpGet]
        [Route("api/Competitions/mark3")]
        public List<Competitions> GetOC(int OID) //Orgenaizer_Main_Page.html - 
        {
            Competitions lCompetitions = new Competitions();
            return lCompetitions.Read(OID);
        }


    }
}