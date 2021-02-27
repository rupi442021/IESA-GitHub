using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IESA.Models
{
    public class Posts
    {
        int postid;
        string title;
        string body; //In SQL nvarchar(1) ? Check*
        string image1;
        string category;
        int status1; //0 or 1

        public Posts(int postid, string title, string body, string image1, string category, int status1)
        {
            Postid = postid;
            Title = title;
            Body = body;
            Image1 = image1;
            Category = category;
            Status1 = status1;
        }

        public Posts() { }

        public int Postid { get => postid; set => postid = value; }
        public string Title { get => title; set => title = value; }
        public string Body { get => body; set => body = value; }
        public string Image1 { get => image1; set => image1 = value; }
        public string Category { get => category; set => category = value; }
        public int Status1 { get => status1; set => status1 = value; }


        //#Functions Area








    }
}