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
        public Orgenaizers GetOrgenaizer(int idtoserver)
        {
            Orgenaizers userin = new Orgenaizers();
            return userin.getUserInfo(idtoserver);
        }




        //---Sign_In.html--- *Close*

    }
}