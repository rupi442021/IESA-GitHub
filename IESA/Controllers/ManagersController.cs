using IESA.Models;
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


        //---Manager_Main_Page.html--- *Close*


    }
}