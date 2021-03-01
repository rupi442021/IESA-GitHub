using IESA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IESA.Controllers
{
    public class GamersController : ApiController
    {

        //---Sign_Up.html--- *Open*


        [HttpGet] //Sign_Up.html - method OG1
        [Route("api/gamers/mark")]
        public string GetEmail(string emailAdd)
        {
            Gamers eAddress = new Gamers();
            return eAddress.CheckEmail(emailAdd);
        }

        public int Get() //Sign_Up.html - method OG2
        {
            Gamers newid = new Gamers();
            return newid.ReadID();
        }

        public void Post([FromBody] Gamers gamer) //Sign_Up.html - method OG3
        {
            gamer.InsertGamer();
        }


        //---Sign_Up.html--- *Close*





    }
}