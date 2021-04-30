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
        [Route("api/gamers/mark1")]
        public string GetEmail(string emailAdd)
        {
            Gamers emailc = new Gamers();
            return emailc.CheckEmail(emailAdd);
        }

        [HttpGet] //Sign_Up.html - method OG2
        [Route("api/gamers/mark2")]
        public string GetNickname(string nicknameAdd)
        {
            Gamers nicknamec = new Gamers();
            return nicknamec.CheckNickname(nicknameAdd);
        }

        public int Get() //Sign_Up.html - method OG3
        {
            Gamers newid = new Gamers();
            return newid.ReadID();
        }

        public void Post([FromBody] Gamers gamer) //Sign_Up.html - method OG4
        {
            gamer.InsertGamer();
        }


        //---Sign_Up.html--- *Close*


        //---Sign_In.html--- *Open*


        [HttpGet] //Sign_In.html - method OL1
        [Route("api/Gamers/{email}/login/{pass}")]
        public int CheckInformation(string email, string pass)
        {
            Gamers uinfo = new Gamers();
            return uinfo.CheckInfo(email, pass);
        }


        [HttpGet] //Sign_In.html - method OL2
        [Route("api/Gamers/login3")]
        public Gamers GetGamer(int idtoserver) //Edit_Personal_Details.html - method SG2
        {
            Gamers userin = new Gamers();
            return userin.GetUserInfo(idtoserver);
        }


        //---Sign_Up.html--- *Close*


        //---Orgenaizer_Main_Page.html-------- *Open*


        //bring all valid gamers in database.
        [HttpGet] 
        [Route("api/gamers/mark3")]
        public List<Gamers> GetG() //Orgenaizer_Main_Page.html - method SG1
        {
            Gamers lGamers = new Gamers();
            return lGamers.Read();
        }


        //---Orgenaizer_Main_Page.html-------- *Close*


        //---Manager_Main_Page.html--- *Open*


        [HttpGet] //Manager_Main_Page.html - method OM1
        [Route("api/Gamers/tableG")]
        public List<Gamers> GetGamers()
        {
            Gamers gamerslist = new Gamers();
            return gamerslist.ReadGamers();
        }


        [HttpPut] //Manager_Main_Page.html - method OM2
        [Route("api/Gamers/delg")]
        public void deleteGamer(int todeleteid)
        {
            Gamers gam1 = new Gamers();
            gam1.DeleteGamer(todeleteid);
        }


        [HttpPut] //Manager_Main_Page.html - method OM3
        [Route("api/Gamers/Tablegstatus")]
        public void updateGamerStatus(int toupdateid)
        {
            Gamers gamer1 = new Gamers();
            gamer1.UpdateGamerStatus(toupdateid);
        }

        //---Manager_Main_Page.html--- *Close*



        //---Database.html--- *Open*


        [HttpGet] //Database.html - method OD5
        [Route("api/Gamers/tableGdb")]
        public List<Gamers> GetGamersDB()
        {
            Gamers gamerslist = new Gamers();
            return gamerslist.ReadGamersDB();
        }



        //---Database.html--- *Close*


        //---Edit_Personal_Details.html--- *Open*

        [HttpPut] //Manager_Main_Page.html 
        [Route("api/Gamers/updatedetails")]
        public void updateGamerDetails(int GId, Gamers g1) //Edit_Personal_Details.html - method SG1 
        {
            Gamers G1 = new Gamers();
            G1.UpdateGamerDetails(GId, g1);
        }


        //---Edit_Personal_Details.html--- *Close*


        [HttpGet] //Edit_Personal_By_Manager.html - method MU2---*Open*
        [Route("api/Gamers/{IdUser}")]
        public List<Gamers> GetGamerDetails(int IdUser)
        {
            Gamers gamerDetails = new Gamers();
            return gamerDetails.ReadGamerDetails(IdUser);
        }


        //Edit_Personal_By_Manager.html - method MU2---*Close*






    }
}