using IESA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IESA.Controllers
{
    public class OrgenaizersController : ApiController
    {

        //---Sign_Up.html--- *Open*


        public int Get() //Sign_Up.html - method OO1
        {
            Orgenaizers newid = new Orgenaizers();
            return newid.ReadID();
        }

        public void Post([FromBody] Orgenaizers orgenaizer) //Sign_Up.html - method OO2
        {
            orgenaizer.InsertOrgenaizer();
        }


        //---Sign_Up.html--- *Close*


        //---Sign_In.html--- *Open*

        [HttpGet] //Sign_In.html - method OL3
        [Route("api/Orgenaizers/login2")]
        public Orgenaizers GetOrgenaizer(int idtoserver) //Sign_In.html - method OL3
        {
            Orgenaizers userin = new Orgenaizers();
            return userin.getUserInfo(idtoserver);
        }




        //---Sign_In.html--- *Close*


        //---Manager_Main_Page.html--- *Open*


        [HttpGet] //Manager_Main_Page.html - method OM4
        [Route("api/Orgenaizers/tableO")]
        public List<Orgenaizers> GetOrgenaizers()
        {
            Orgenaizers orgenaizerslist = new Orgenaizers();
            return orgenaizerslist.ReadOrgenaizers();
        }


        [HttpPut] //Manager_Main_Page.html - method OM5
        [Route("api/Orgenaizers/delo")]
        public void deleteOrgenaizer(int todeleteid)
        {
            Orgenaizers org1 = new Orgenaizers();
            org1.DeleteOrgenaizer(todeleteid);
        }


        [HttpPut] //Manager_Main_Page.html - method OM6
        [Route("api/Orgenaizers/Tableostatus")]
        public void updateOrgenaizerStatus(int toupdateid)
        {
            Orgenaizers orgenaizer1 = new Orgenaizers();
            orgenaizer1.UpdateOrgenaizerStatus(toupdateid);
        }




        //---Manager_Main_Page.html--- *Close*


        //---Database.html--- *Open*


        [HttpGet] //Database.html - method OD6
        [Route("api/Orgenaizers/tableOdb")]
        public List<Orgenaizers> GetOrgenaizersDB()
        {
            Orgenaizers orgelist = new Orgenaizers();
            return orgelist.ReadOrgenaizersDB();
        }



        //---Database.html--- *Close*


        //---Edit_Personal_Details.html--- *Open*

        [HttpPut] //Manager_Main_Page.html 
        [Route("api/Orgenaizers/updatedetails")]
        public void updateOrgenaizerDetails(int OId, Orgenaizers o1) //Edit_Personal_Details.html - method SO1
        {
            Orgenaizers O1 = new Orgenaizers();
            O1.UpdateOrgenaizerDetails(OId, o1);
        }


        //---Edit_Personal_Details.html--- *Close*



        [HttpGet] //Edit_Personal_By_Manager.html - method MU1---*Open*
        [Route("api/Orgenaizers/{IdUser}")]
        public List<Orgenaizers> GetOrgDetails(int IdUser)
        {
            Orgenaizers OrgDetails = new Orgenaizers();
            return OrgDetails.ReadOrgDetails(IdUser);
        }


        //Edit_Personal_By_Manager.html - method MU1---*Close*

    }
}