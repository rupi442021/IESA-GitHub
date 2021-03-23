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

 
        public void Post(int cID, int gcID, int oID) //Add_New_Competitions.html 
        {
            DBServices dbs = new DBServices();
            dbs.InsertGameInC(cID, gcID, oID);
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


        public List<Competitions> Get(int OID) //Orgenaizer_Main_Page.html - 
        {
            Competitions lCompetitions = new Competitions();
            return lCompetitions.Read(OID);
        }

        [HttpGet]
        [Route("api/Competitions/mark5")]
        public Dictionary<int, List<string>> GetG(int CId) //Orgenaizer_Main_Page.html - 
        {
            DBServices dbs = new DBServices();
            return dbs.GamersInC(CId);
        }


    }
}