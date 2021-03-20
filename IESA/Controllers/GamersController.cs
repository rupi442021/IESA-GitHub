﻿using IESA.Models;
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
        public Gamers GetGamer(int idtoserver)
        {
            Gamers userin = new Gamers();
            return userin.getUserInfo(idtoserver);
        }


        //---Sign_Up.html--- *Close*



        //---Orgenaizer_Main_Page.html-------- *Open*


        [HttpGet] 
        [Route("api/gamers/mark3")]
        public List<Gamers> GetG()
        {
            Gamers lGamers = new Gamers();
            return lGamers.Read();
        }




        //---Orgenaizer_Main_Page.html-------- *Close*








    }
}