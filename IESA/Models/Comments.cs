using IESA.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IESA.Models
{
    public class Comments
    {
        int postid;
        int commentid;
        string name;
        string body;
        DateTime date;

        public Comments(int postid, int commentid, string name, string body, DateTime date)
        {
            Postid = postid;
            Commentid = commentid;
            Name = name;
            Body = body;
            Date = date;
        }
        public Comments() { }

        public int Postid { get => postid; set => postid = value; }
        public int Commentid { get => commentid; set => commentid = value; }
        public string Name { get => name; set => name = value; }
        public string Body { get => body; set => body = value; }
        public DateTime Date { get => date; set => date = value; }

        //#Functions Area

        public List<Comments> ReadCommentsForPosts(int Pid) //Orgenaizer_Main_Page.html - method SC1
        {
            DBServices dbs = new DBServices();
            List<Comments> readPosts = dbs.ReadCommentsForPosts(Pid);
            return readPosts;
        }

        public void InsertComments() //Add_New_Competition.html
        {
            DBServices dbs = new DBServices();
            dbs.InsertComments(this);
        }



    }
}