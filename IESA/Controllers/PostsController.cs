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
        [Route("api/posts/rendpost")]
        public Posts GetPost(int idtoserver)
        {
            Posts post1 = new Posts();
            return post1.getPost(idtoserver);
        }

        //---Post.html--- *Close*

    }
}