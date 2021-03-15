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

        public int Get() //Add_New_Post.html - method OP1
        {
            Posts newid = new Posts();
            return newid.ReadID();
        }

        public void Post([FromBody] Posts post) //Add_New_Post.html - method OP2
        {
            post.InsertPost();
        }

    }
}