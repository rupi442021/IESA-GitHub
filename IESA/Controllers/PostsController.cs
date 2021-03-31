﻿using IESA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IESA.Controllers
{
    public class PostsController : ApiController
    {

        //---Add_New_Post.html--- *Open*

        public int Get() //Add_New_Post.html - method OP1
        {
            Posts newid = new Posts();
            return newid.ReadID();
        }

        public void Post([FromBody] Posts post) //Add_New_Post.html - method OP2
        {
            post.InsertPost();
        }

        //---Add_New_Post.html--- *Close*


        //---Post.html--- *Open*

        [HttpGet] //Post.html - method OP3
        [Route("api/posts/rendpost")]
        public Posts GetPost(int idtoserver)
        {
            Posts post1 = new Posts();
            return post1.GetPost(idtoserver);
        }

        //---Post.html--- *Close*


        //---Post_Archive.html--- *Open*


        [HttpGet] //Post_Archive.html - method MP1
        [Route("api/Posts/mark9")] 
        public List<Posts> Gete() 
        {
            Posts posts = new Posts();
            return posts.Read();
        }

        public List<Posts> GET(string cat2)
        {
            Posts posts2 = new Posts();
            return posts2.ReadFillter(cat2);
        }

        //---Post_Archive.html--- *Close*


        //---Manager_Main_Page.html--- *Open*


        [HttpGet] //Manager_Main_Page.html - method OM7
        [Route("api/Posts/tableP")]
        public List<Posts> GetPosts()
        {
            Posts postslist = new Posts();
            return postslist.ReadPosts();
        }


        //---Manager_Main_Page.html--- *Close*


        //---Edit_Post.html--- *Open*


        [HttpGet] //Edit_Post.html - #method OP3
        [Route("api/Posts/getepost")]
        public Posts GetePost(int idtoserver)
        {
            Posts postobj = new Posts();
            return postobj.GetPost(idtoserver);
        }

        [HttpPut] //Edit_Post.html - method OP4
        [Route("api/Posts/updatepost")]
        public void Put([FromBody] Posts updatedpost)
        {
            updatedpost.UpdatePost();
        }


        //---Edit_Post.html--- *Close*

    }
}