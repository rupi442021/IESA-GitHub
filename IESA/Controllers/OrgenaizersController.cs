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

        public int Get() //Sign_Up.html - method OO1
        {
            Orgenaizers newid = new Orgenaizers();
            return newid.ReadID();
        }

        public void Post([FromBody] Orgenaizers orgenaizer) //Sign_Up.html - method OO2
        {
            orgenaizer.InsertOrgenaizer();
        }


    }
}