using IESA.Models;
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
        [Route("api/Posts/rendpost")]
        public Posts GetPost(int idtoserver)
        {
            Posts post1 = new Posts();
            return post1.GetPost(idtoserver);
        }

        [HttpGet] //Post.html - method OP4
        [Route("api/Posts/rendpost2")]
        public List<Posts> getAnotherPosts(int postid, string categoryname)
        {
            Posts postsList = new Posts();
            return postsList.ReadAnotherPosts(postid, categoryname);
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


        //---Database.html--- *Open*


        [HttpGet] //Database.html - method OD1
        [Route("api/Posts/tableDBPosts")]
        public List<Posts> GetDBPosts()
        {
            Posts postslist = new Posts();
            return postslist.ReadDBPosts();
        }

        [HttpPut] //Database.html - method OD2
        [Route("api/Posts/delp")]
        public void deletePost(int todeleteid)
        {
            Posts post1 = new Posts();
            post1.DeletePost(todeleteid);
        }


        //---Database.html--- *Close*


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


        //---index.html--- *Open*

        [HttpGet] //index.html - method OMI1
        [Route("api/Posts/Postsindex")]
        public List<Posts> GetPostsIndex()
        {
            Posts postslist = new Posts();
            return postslist.GetPostsIndex();
        }

        [HttpGet] //index.html - method OMI3
        [Route("api/Posts/Announcementindex")]
        public Posts GetPostIndex()
        {
            Posts post1 = new Posts();
            return post1.GetPostIndex();

        }

        //---index.html--- *Close*



    }
}