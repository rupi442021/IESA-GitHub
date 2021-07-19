using IESA.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace IESA.Controllers
{
    public class StatisticsController : ApiController
    {

        public void Post(string message)
        {
            Statistics stats = new Statistics();
            stats.Write("hello1");
        }




    }
}