﻿using IESA.Models;
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


        public List<Competitions> Get(int OID) //Orgenaizer_Main_Page.html - method AA1
        {
            Competitions lCompetitions = new Competitions();
            return lCompetitions.Read(OID);
        }

        [HttpGet]
        [Route("api/Competitions/mark5")]
        public Dictionary<int, List<string>> GetG(int CId ,int statusval) //Orgenaizer_Main_Page.html - 
        {
            DBServices dbs = new DBServices();
            return dbs.GamersInC(CId, statusval);
        }

        [HttpPost]
        [Route("api/Competitions/mark6")]
        public void Postg(List<Gamer_Competition> rankarr) //Add_New_Competitions.html - Preview
        {
            DBServices dbs = new DBServices();
            dbs.Insertranks(rankarr);

        }


        //Gamer's page

        [HttpGet]
        [Route("api/Competitions/mark11")]
        public List<Competitions> Getg(int GID)  
        {
            Competitions lCompetitions = new Competitions();
            return lCompetitions.ReadGamer(GID);
        }

        //END OF--- Gamer's page


        //---Manager_Main_Page.html--- *Open*


        [HttpGet] //Manager_Main_Page.html - method OM8
        [Route("api/Competitions/tableC")]
        public List<Competitions> GetCompetitions()
        {
            Competitions competitionslist = new Competitions();
            return competitionslist.ReadCompetitionsM();
        }


        //---Manager_Main_Page.html--- *Close*


        //---Competition_View.html--- *Open*

        [HttpGet] //Competition_View.html
        [Route("api/Competitions/getone")]
        public Competitions GetOne(int CompetitionId)
        {
            Competitions competitionslist = new Competitions();
            return competitionslist.ReadOneCompetition(CompetitionId);
        }

        //---Competition_View.html--- *Close*

        //---Edit_Competition.html--- *Open*

        [HttpGet] //Edit_Competition.html
        [Route("api/Competitions/getcategory")]
        public int Getcategory(int CompetitionId)
        {
            Competitions competitionslist = new Competitions();
            return competitionslist.Getcategory(CompetitionId);
        }

        [HttpPut] //Edit_Competition.html 
        [Route("api/Competitions/updatedetails")]
        public void updateCompetitonDetails(int CId,Competitions c1)
        {
            Competitions C1 = new Competitions();
            C1.updateCompetitonDetails(CId, c1);
        }

        public void Put(int cID, int gcID) //Add_New_Competitions.html 
        {
            DBServices dbs = new DBServices();
            dbs.updateGameInC(cID, gcID);
        }

        [HttpPut] //Edit_Competition.html 
        [Route("api/Competitions/decideC")]
        public void decideNewC(int CId, string statusvalue)
        {
            DBServices dbs = new DBServices();
            dbs.decideNewC(CId, statusvalue);
        }


        //---Edit_Competition.html--- *Close*






    }
}