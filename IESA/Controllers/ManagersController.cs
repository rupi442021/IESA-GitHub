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
    public class ManagersController : ApiController
    {

        //---Sign_In.html--- *Open*


        [HttpGet] //Sign_In.html - method OL4
        [Route("api/Managers/login1")]
        public Managers GetManager(int idtoserver)
        {
            Managers userin = new Managers();
            return userin.getUserInfo(idtoserver);
        }


        //---Sign_Up.html--- *Close*


        //---Manager_Main_Page.html--- *Open*

        [HttpGet] //Manager_Main_Page.html - method OM9
        [Route("api/Managers/tablestats")]
        public Dictionary<int, int> GetStatistics()
        {
            Managers statisticsdict = new Managers();
            return statisticsdict.GetStatisticsM();
        }

        [HttpGet] //Manager_Main_Page.html - method OM10
        [Route("api/Managers/tablegraph")]
        public Dictionary<int, int> GetGraph()
        {
            Managers statisticsdict = new Managers();
            return statisticsdict.GetGraph();
        }


        //---Manager_Main_Page.html--- *Close*

        //---Database.html--- *Open*


        [HttpGet] //Database.html - method OD7
        [Route("api/Managers/tableMdb")]
        public List<Managers> GetManagersDB()
        {
            Managers managerslist = new Managers();
            return managerslist.ReadManagersDB();
        }

        [HttpGet] //Database.html - method SM4
        [Route("api/Managers/CompetitionsToDelete")]
        public List<Competitions> GetCompetitionsToDelete(int CategoryID)
        {
            Competitions CompetitionsToDelete = new Competitions();
            return CompetitionsToDelete.GetCompetitionListToDelete(CategoryID);
        }


        //---Database.html--- *Close*


        //---RestorePassword.html---- *Open*


        [HttpPut] //RestorePassword.html - method SD1
        [Route("api/Manager/passwordupdate")]
        public void updatePassword(string userEmail, string newPass)
        {
            DBServices dbs = new DBServices();
            dbs.updatePassword(userEmail, newPass);
        }


        //---RestorePassword.html---- *Close*


        //---Edit_Personal_Details.html--- *Open*


        [HttpPut] //Edit_Personal_Details.html - method OME1
        [Route("api/Managers/updatedetailsm")]
        public void updateManagerDetails(int MId, Managers m1)
        {
            Managers M1 = new Managers();
            M1.updateManagerDetails(MId, m1);
        }


        //---Edit_Personal_Details.html--- *Close*




    }
}