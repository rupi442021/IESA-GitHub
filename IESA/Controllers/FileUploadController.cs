using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace IESA.Controllers
{
    public class FileUploadController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage Post()
        {
            List<string> imageLinks = new List<string>();
            var httpContext = HttpContext.Current;

            // Check for any uploaded file  
            if (httpContext.Request.Files.Count > 0)
            {
                //Loop through uploaded files  
                for (int i = 0; i < httpContext.Request.Files.Count; i++)
                {
                    HttpPostedFile httpPostedFile = httpContext.Request.Files[i];

                    // this is an example of how you can extract addional values from the Ajax call
                    string userid = httpContext.Request.Form["userid"];

                    if (httpPostedFile != null)
                    {
                        // Construct file save path  
                        //var fileSavePath = Path.Combine(HostingEnvironment.MapPath(ConfigurationManager.AppSettings["fileUploadFolder"]), httpPostedFile.FileName);

                        string filetype = ".";
                        filetype += httpPostedFile.FileName.Split('.').Last(); //jpg or png or else

                        userid += filetype; //10052.jpg unique name for a user id

                        var fileSavePath = Path.Combine(HostingEnvironment.MapPath("~/uploadedFiles"), userid);

                        // Save the uploaded file 
                        httpPostedFile.SaveAs(fileSavePath);
                        imageLinks.Add("uploadedFiles/" + userid);
                    }
                }
            }

            // Return status code  
            return Request.CreateResponse(HttpStatusCode.Created, imageLinks);
        }


        //Add_New_Competitions.html
        [HttpPost] 
        [Route("api/FileUpload/mark1")]
        public HttpResponseMessage PostF()
        {
            List<string> imageLinks = new List<string>();
            var httpContext = HttpContext.Current;

            // Check for any uploaded file  
            if (httpContext.Request.Files.Count > 0)
            {
                //Loop through uploaded files  
                for (int i = 0; i < httpContext.Request.Files.Count; i++)
                {
                    HttpPostedFile httpPostedFile = httpContext.Request.Files[i];

                    // this is an example of how you can extract addional values from the Ajax call
                    string competitionid = httpContext.Request.Form["CompetitionID"];

                    if (httpPostedFile != null)
                    {
                        // Construct file save path  
                        //var fileSavePath = Path.Combine(HostingEnvironment.MapPath(ConfigurationManager.AppSettings["fileUploadFolder"]), httpPostedFile.FileName);

                        string filetype = ".";
                        filetype += httpPostedFile.FileName.Split('.').Last(); //jpg or png or else

                        competitionid += filetype; //10052.jpg unique name for a user id

                        var fileSavePath = Path.Combine(HostingEnvironment.MapPath("~/uploadedFiles"), competitionid);

                        // Save the uploaded file 
                        httpPostedFile.SaveAs(fileSavePath);
                        imageLinks.Add("uploadedFiles/" + competitionid);
                    }
                }
            }

            // Return status code  
            return Request.CreateResponse(HttpStatusCode.Created, imageLinks);
        }
    }
}