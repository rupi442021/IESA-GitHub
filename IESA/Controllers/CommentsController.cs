using IESA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IESA.Controllers
{
    public class CommentsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public List<Comments> Get(int Pid)
        {
            Comments readComments = new Comments();
            return readComments.ReadCommentsForPosts(Pid);
        }

        // POST api/<controller>
        public void Post(Comments comments) //Add_New_Competitions.html 
        {
            comments.InsertComments();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}