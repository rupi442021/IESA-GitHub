using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IESA.Models
{
    public class Manager_Post
    {
        int managerid;
        int postid; //Automatic
        DateTime lastupdate;

        public Manager_Post(int managerid, int postid, DateTime lastupdate)
        {
            Managerid = managerid;
            Postid = postid;
            Lastupdate = lastupdate;
        }

        public Manager_Post() { }

        public int Managerid { get => managerid; set => managerid = value; }
        public int Postid { get => postid; set => postid = value; }
        public DateTime Lastupdate { get => lastupdate; set => lastupdate = value; }


        //#Functions Area







    }
}