using IESA.Models.DAL;
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
        string body1;
        string body2;
        string body3;
        string image1;
        string category;
        DateTime postdate;
        int viewsnum;
        int status1;

        public Posts(int postid, string title, string body1, string body2, string body3, string image1, string category, DateTime postdate, int viewsnum, int status1)
        {
            Postid = postid;
            Title = title;
            Body1 = body1;
            Body2 = body2;
            Body3 = body3;
            Image1 = image1;
            Category = category;
            Postdate = postdate;
            Viewsnum = viewsnum;
            Status1 = status1;
        }

        public Posts() { }

        public int Postid { get => postid; set => postid = value; }
        public string Title { get => title; set => title = value; }
        public string Body1 { get => body1; set => body1 = value; }
        public string Body2 { get => body2; set => body2 = value; }
        public string Body3 { get => body3; set => body3 = value; }
        public string Image1 { get => image1; set => image1 = value; }
        public string Category { get => category; set => category = value; }
        public DateTime Postdate { get => postdate; set => postdate = value; }
        public int Viewsnum { get => viewsnum; set => viewsnum = value; }
        public int Status1 { get => status1; set => status1 = value; }

        //#Functions Area


        //---Add_New_Post.html--- *Open*


        public int ReadID() //Add_New_Post.html - method OP1
        {
            DBServices dbs = new DBServices();
            int newid = dbs.GetnewIdPost();
            return newid;
        }

        public void InsertPost() //Add_New_Post.html - method OP2
        {
            DBServices dbs = new DBServices();
            dbs.InsertPost(this);
        }


        //---Add_New_Post.html--- *Close*


        //---Post.html--- *Open*


        public Posts GetPost(int id_toserver) //Post.html - method OP3
        {
            DBServices dbs = new DBServices();
            return dbs.getPostSQL(id_toserver);
        }

        public List<Posts> ReadAnotherPosts(int postid, string categoryname) //Post.html - method OP4
        {
            DBServices dbs = new DBServices();
            List<Posts> postsList = dbs.ReadAnotherPostsSQL(postid, categoryname);
            return postsList;
        }


        //---Post.html--- *Close*


        //---Post_Archive.html--- *Open*


        public List<Posts> Read() //Post_Archive.html - method MP1
        {
            DBServices dbs = new DBServices();
            List<Posts> posts = dbs.GetPosts();
            return posts;
        }


        //---Post_Archive.html--- *Close*


        //---Manager_Main_Page.html--- *Open*


        public List<Posts> ReadPosts() //Manager_Main_Page.html - method OM7
        {
            DBServices dbs = new DBServices();
            List<Posts> plist = dbs.ReadPostsMSQL();
            return plist;
        }


        //---Manager_Main_Page.html--- *Close*


        //---Database.html--- *Open*


        public List<Posts> ReadDBPosts() //Database.html - method OD1
        {
            DBServices dbs = new DBServices();
            List<Posts> plist = dbs.ReadDBPostsSQL();
            return plist;
        }

        public void DeletePost(int todeleteid) //Database.html - method OD2
        {
            DBServices dbs = new DBServices();
            dbs.DeletePostSQL(todeleteid);
        }


        //---Database.html--- *Close*



        //---Edit_Post.html--- *Open*

        public void UpdatePost() //Edit_Post.html - method OP4
        {
            DBServices dbs = new DBServices();
            dbs.UpdatePostSQL(this);
        }


        //---Edit_Post.html--- *Close*



        //---index.html--- *Open*

        public List<Posts> GetPostsIndex() //index.html - method OMI1
        {
            DBServices dbs = new DBServices();
            List<Posts> postslist = dbs.GetPostsIndexSQL();
            return postslist;
        }

        public Posts GetPostIndex() //index.html - method OMI3
        {
            DBServices dbs = new DBServices();
            return dbs.GetPostIndexSQL();
        }


        //---index.html--- *Close*



    }
}