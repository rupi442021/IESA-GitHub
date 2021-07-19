using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace IESA.Models
{
    public class Statistics
    {

        public void Write(string message)
        {

            string path = HttpContext.Current.Server.MapPath("~") + "Statistics\\statlog.txt";

            byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);

            FileStream fs = null;

            try
            {
                using (fs = File.Open(path, FileMode.Open, FileAccess.Write, FileShare.None))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(message);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                    fs.Write(newline, 0, newline.Length);
                }
            }
            catch (Exception ex)
            {
                // write to a log file
            }
            finally
            {
                if (fs != null) fs.Close();
            }

        }



    }
}