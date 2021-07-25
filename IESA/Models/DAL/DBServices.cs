using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace IESA.Models.DAL
{
    public class DBServices
    {
        public SqlDataAdapter da;
        public DataTable dt;

        private int competitionId;
        private string emaila;
        private string nicknamec;
        private int userId;
        private string emailInfo;
        private string passInfo;
        private int idInfo;
        private int postId;
        private string renderdate1;
        private string renderdate2;
        private string renderdate3;
        private string renderdate4;
        private string month1;
        private string day1;

        public DBServices() { }

        //--------------------------------------------------------------------------------------------------
        // This method creates a connection to the database according to the connectionString name in the web.config 
        //--------------------------------------------------------------------------------------------------
        public SqlConnection connect(String conString)
        {

            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

            return cmd;
        }


        //#Functions Area


        //---Sign_Up.html--- *Open*

        public string CheckEmailSQL(string emailADD) //Sign_Up.html - method OG1 (Check Email: Gamer/Orgenaizer)
        {

            SqlConnection con = null;

            try
            {

                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT Gamers.email FROM Gamers WHERE email='" + emailADD + "' UNION SELECT Orgenaizers.email FROM Orgenaizers WHERE email='" + emailADD + "' UNION SELECT Managers.email FROM Managers WHERE email='" + emailADD + "'";

                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                emaila = "NULL";

                while (dr.Read()) //1 row
                {

                    emaila = (string)dr["email"];
                }

                if (emaila == emailADD)
                {
                    return "exist";
                }
                else
                {
                    return "not exist";
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public string CheckNicknameSQL(string nicknameAdd) //Sign_Up.html - method OG2 (Check Nickname: Gamer/Orgenaizer)
        {

            SqlConnection con = null;

            try
            {

                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT Gamers.nickname FROM Gamers WHERE nickname='" + nicknameAdd + "' UNION SELECT Orgenaizers.nickname FROM Orgenaizers WHERE nickname='" + nicknameAdd + "' UNION SELECT Managers.nickname FROM Managers WHERE nickname='" + nicknameAdd + "'";

                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                nicknamec = "NULL";

                while (dr.Read()) //1 row
                {

                    nicknamec = (string)dr["nickname"];
                }

                if (nicknamec == nicknameAdd)
                {
                    return "exist";
                }
                else
                {
                    return "not exist";
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public int GetnewIdGamer() //Sign_Up.html - method OG3 (Get New Id: Gamer)
        {
            userId = 0;

            SqlConnection con = null;

            try
            {

                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT TOP 1 * FROM Gamers ORDER BY userid DESC";


                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read()) //1 row
                {
                    userId = Convert.ToInt32(dr["userid"]); //future id of the user
                }

                userId += 1;

                return userId; //Future id of the user
            }
            catch (Exception)
            {
                throw new Exception("Problem getting the information from the server, please try again later");
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public int GetnewIdOrgenaizer() //Sign_Up.html - method OO1 (Get New Id: Orgenaizer)
        {
            userId = 0;

            SqlConnection con = null;

            try
            {

                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT TOP 1 * FROM Orgenaizers ORDER BY userid DESC";


                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read()) //1 row
                {
                    userId = Convert.ToInt32(dr["userid"]); //future id of the user
                }

                userId += 1;

                return userId; //Future id of the user
            }
            catch (Exception)
            {
                throw new Exception("Problem getting the information from the server, please try again later");
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public int InsertGamer(Gamers gamer) //Sign_Up.html - method OG4 (Insert: Gamer (1))
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception)
            {
                // write to log

                throw new Exception("Problem inserting to the server, please try again later");
            }

            String cStr = BuildInsertCommand(gamer);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildInsertCommand(Gamers gamer) //Sign_Up.html - method OG4 (Insert: Gamer (2))
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}','{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}')", gamer.Email, gamer.Password1, gamer.Nickname, gamer.Firstname, gamer.Lastname, gamer.Gender, gamer.Id, gamer.Phone, gamer.Dob, gamer.Address1, gamer.Discorduser, gamer.Picture, gamer.Registrationdate, gamer.License, gamer.Status1);
            String prefix = "INSERT INTO Gamers " + "(email, password1, nickname, firstname, lastname, gender, id, phone, dob, address1, discorduser, picture, registrationdate, license, status1) ";

            command = prefix + sb.ToString();

            return command;

        }

        public int InsertOrgenaizer(Orgenaizers orgenaizer) //Sign_Up.html - method OO2 (Insert: Orgenaizer (1))
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception)
            {
                // write to log

                throw new Exception("Problem inserting to the server, please try again later");
            }

            String cStr = BuildInsertCommand(orgenaizer);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildInsertCommand(Orgenaizers orgenaizer) //Sign_Up.html - method OO2 (Insert: Orgenaizer (2))
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}','{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}')", orgenaizer.Email, orgenaizer.Password1, orgenaizer.Nickname, orgenaizer.Firstname, orgenaizer.Lastname, orgenaizer.Gender, orgenaizer.Id, orgenaizer.Phone, orgenaizer.Dob, orgenaizer.Address1, orgenaizer.Picture, orgenaizer.Comunityname, orgenaizer.Linktocommunity, orgenaizer.Status1);
            String prefix = "INSERT INTO Orgenaizers " + "(email, password1, nickname, firstname, lastname, gender, id, phone, dob, address1, picture, communityname, linktocommunity, status1) ";

            command = prefix + sb.ToString();

            return command;

        }


        //---Sign_Up.html--- *Close*


        //---Sign_In.html--- *Open*

        public int CheckInfoSQL(string email, string pass) //Sign_In.html - method OL1
        {

            SqlConnection con = null;

            try
            {

                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file


                String selectSTR = "SELECT Gamers.userid, Gamers.email, Gamers.password1 FROM Gamers WHERE Gamers.email = '" + email + "' UNION SELECT Orgenaizers.userid, Orgenaizers.email, Orgenaizers.password1 FROM Orgenaizers WHERE Orgenaizers.email = '" + email + "' UNION SELECT Managers.userid, Managers.email, Managers.password1 FROM Managers WHERE Managers.email = '" + email + "' ";

                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end


                while (dr.Read()) //1 row
                {
                    idInfo = Convert.ToInt32(dr["userid"]); //We want to get the id for localstorage

                    emailInfo = (string)dr["email"];
                    passInfo = (string)dr["password1"];

                }


                if (emailInfo == email && passInfo == pass)
                {
                    return idInfo; //true info
                }
                else
                {
                    return 0; //false info
                }

            }
            catch (Exception ex) //change it to a message
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public Gamers getGamerSQL(int id_toserver) //Sign_In.html - method OL2
        {

            SqlConnection con = null;
            Gamers g = new Gamers();

            try
            {

                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Gamers WHERE userid = " + id_toserver;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end


                while (dr.Read()) //1 row
                {

                    g.Userid = (dr["userid"] != DBNull.Value) ? Convert.ToInt32(dr["userid"]) : default;
                    g.Email = (dr["email"] != DBNull.Value) ? (string)dr["email"] : default;
                    g.Nickname = (dr["nickname"] != DBNull.Value) ? (string)dr["nickname"] : default;
                    g.Firstname = (dr["firstname"] != DBNull.Value) ? (string)dr["firstname"] : default;
                    g.Lastname = (dr["lastname"] != DBNull.Value) ? (string)dr["lastname"] : default;
                    g.Gender = (dr["gender"] != DBNull.Value) ? (string)dr["gender"] : default;
                    g.Id = (dr["id"] != DBNull.Value) ? Convert.ToInt32(dr["id"]) : default; //Needs to be int in SQL*
                    g.Phone = (dr["phone"] != DBNull.Value) ? (string)dr["phone"] : default;
                    //g.Dob = (dr["dob"] != DBNull.Value) ? Convert.ToDateTime(dr["dob"]) : default;


                    renderdate1 = (dr["dob"] != DBNull.Value) ? (string)dr["dob"] : default;

                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            g.Dob = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            g.Dob = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        g.Dob = (dr["dob"] != DBNull.Value) ? Convert.ToDateTime(dr["dob"]) : default;
                    }


                    g.Address1 = (dr["address1"] != DBNull.Value) ? (string)dr["address1"] : default;
                    g.Discorduser = (dr["discorduser"] != DBNull.Value) ? (string)dr["discorduser"] : default;
                    g.Picture = (dr["picture"] != DBNull.Value) ? (string)dr["picture"] : default;
                    //g.Registrationdate = (dr["registrationdate"] != DBNull.Value) ? Convert.ToDateTime(dr["registrationdate"]) : default;

                    renderdate2 = (dr["registrationdate"] != DBNull.Value) ? (string)dr["registrationdate"] : default;

                    if (renderdate2 != null)
                    {

                        if (renderdate2.Contains("AM") || renderdate2.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            g.Registrationdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            g.Registrationdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        g.Registrationdate = (dr["registrationdate"] != DBNull.Value) ? Convert.ToDateTime(dr["registrationdate"]) : default;
                    }


                    //g.Outofdate = (dr["outofdate"] != DBNull.Value) ? Convert.ToDateTime(dr["outofdate"]) : default;

                    renderdate3 = (dr["outofdate"] != DBNull.Value) ? (string)dr["outofdate"] : default;

                    if (renderdate3 != null)
                    {

                        if (renderdate3.Contains("AM") || renderdate3.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate3.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            g.Outofdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate3.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            g.Outofdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        g.Outofdate = (dr["outofdate"] != DBNull.Value) ? Convert.ToDateTime(dr["outofdate"]) : default;
                    }

                    g.License = (dr["license"] != DBNull.Value) ? Convert.ToInt32(dr["license"]) : default;
                    g.Status1 = (dr["status1"] != DBNull.Value) ? Convert.ToInt32(dr["status1"]) : default;

                }

                return g;

            }
            catch (Exception ex) //change it to a message
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public Orgenaizers getOrgenaizerSQL(int id_toserver) //Sign_In.html - method OL3
        {

            SqlConnection con = null;
            Orgenaizers o = new Orgenaizers();

            try
            {

                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Orgenaizers WHERE userid = " + id_toserver;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end


                while (dr.Read()) //1 row
                {

                    o.Userid = (dr["userid"] != DBNull.Value) ? Convert.ToInt32(dr["userid"]) : default;
                    o.Email = (dr["email"] != DBNull.Value) ? (string)dr["email"] : default;
                    o.Nickname = (dr["nickname"] != DBNull.Value) ? (string)dr["nickname"] : default;
                    o.Firstname = (dr["firstname"] != DBNull.Value) ? (string)dr["firstname"] : default;
                    o.Lastname = (dr["lastname"] != DBNull.Value) ? (string)dr["lastname"] : default;
                    o.Gender = (dr["gender"] != DBNull.Value) ? (string)dr["gender"] : default;
                    o.Id = (dr["id"] != DBNull.Value) ? Convert.ToInt32(dr["id"]) : default; //Needs to be int in SQL*
                    o.Phone = (dr["phone"] != DBNull.Value) ? (string)dr["phone"] : default;
                    //o.Dob = (dr["dob"] != DBNull.Value) ? Convert.ToDateTime(dr["dob"]) : default;

                    renderdate1 = (dr["dob"] != DBNull.Value) ? (string)dr["dob"] : default;

                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            o.Dob = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            o.Dob = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        o.Dob = (dr["dob"] != DBNull.Value) ? Convert.ToDateTime(dr["dob"]) : default;
                    }



                    o.Address1 = (dr["address1"] != DBNull.Value) ? (string)dr["address1"] : default;
                    o.Picture = (dr["picture"] != DBNull.Value) ? (string)dr["picture"] : default;
                    o.Comunityname = (dr["communityname"] != DBNull.Value) ? (string)dr["communityname"] : default;
                    o.Linktocommunity = (dr["linktocommunity"] != DBNull.Value) ? (string)dr["linktocommunity"] : default;
                    o.Status1 = (dr["status1"] != DBNull.Value) ? Convert.ToInt32(dr["status1"]) : default;

                }

                return o;

            }
            catch (Exception ex) //change it to a message
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public Managers getManagerSQL(int id_toserver) //Sign_In.html - method OL4
        {

            SqlConnection con = null;
            Managers m = new Managers();

            try
            {

                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Managers WHERE userid = " + id_toserver;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end


                while (dr.Read()) //1 row
                {


                    m.Userid = (dr["userid"] != DBNull.Value) ? Convert.ToInt32(dr["userid"]) : default;
                    m.Email = (dr["email"] != DBNull.Value) ? (string)dr["email"] : default;
                    m.Nickname = (dr["nickname"] != DBNull.Value) ? (string)dr["nickname"] : default;
                    m.Firstname = (dr["firstname"] != DBNull.Value) ? (string)dr["firstname"] : default;
                    m.Lastname = (dr["lastname"] != DBNull.Value) ? (string)dr["lastname"] : default;
                    m.Gender = (dr["gender"] != DBNull.Value) ? (string)dr["gender"] : default;
                    m.Id = (dr["id"] != DBNull.Value) ? Convert.ToInt32(dr["id"]) : default; //Needs to be int in SQL*
                    m.Phone = (dr["phone"] != DBNull.Value) ? (string)dr["phone"] : default;
                    //m.Dob = (dr["dob"] != DBNull.Value) ? Convert.ToDateTime(dr["dob"]) : default;

                    renderdate1 = (dr["dob"] != DBNull.Value) ? (string)dr["dob"] : default;

                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            m.Dob = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            m.Dob = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        m.Dob = (dr["dob"] != DBNull.Value) ? Convert.ToDateTime(dr["dob"]) : default;
                    }

                    m.Role1 = (dr["role1"] != DBNull.Value) ? (string)dr["role1"] : default;
                    m.Address1 = (dr["address1"] != DBNull.Value) ? (string)dr["address1"] : default;
                    m.Picture = (dr["picture"] != DBNull.Value) ? (string)dr["picture"] : default;

                }

                return m;

            }
            catch (Exception ex) //change it to a message
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }


        //---Sign_In.html--- *Close*


        //---Add_New_Competition.html--- *Open*

        public List<GamesCategories> GetGameCategoriesr() //Add_New_Competition.html - method MC1
        {
            SqlConnection con = null;
            List<GamesCategories> gList = new List<GamesCategories>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from GamesCategories";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    GamesCategories g = new GamesCategories();
                    g.Categoryid = Convert.ToInt32(dr["categoryid"]);
                    g.Categoryname = (string)dr["categoryname"];
                    g.Status1 = Convert.ToInt32(dr["status1"]);


                    gList.Add(g);
                }

                return gList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        public int getCompetitionId()  //Add_New_Competition.html - method OO1 (Get New Id: Competition)
        {
            competitionId = 0;

            SqlConnection con = null;

            try
            {

                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file


                String selectSTR = "SELECT TOP 1 * FROM Competitions ORDER BY competitionid DESC";


                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read()) //1 row
                {

                    competitionId = Convert.ToInt32(dr["competitionid"]); //future id of the user

                }

                competitionId += 1;

                return competitionId; //Future id of the user
            }
            catch (Exception)
            {
                throw new Exception("Problem getting the information from the server, please try again later");
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public int InsertCompetition(Competitions Competition) //Add_New_Competition.html 
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception)
            {
                // write to log

                throw new Exception("Problem inserting to the server, please try again later");
            }

            String cStr = BuildInsertCommand(Competition);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildInsertCommand(Competitions Competition) //Add_New_Competition.html
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}','{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}','{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}','{22}', '{23}', '{24}')", Competition.Competitionname, Competition.Address1, Competition.Banner, Competition.Logo, Competition.Prize1, Competition.Prize2, Competition.Price3, Competition.Linkforregistration, Competition.Lastdateforregistration, Competition.Body, Competition.Startdate, Competition.Enddate, Competition.Startime, Competition.Endtime, Competition.Ispro, Competition.Discord, Competition.Console, Competition.Isiesa, Competition.Linkforstream, '1', Competition.IsPayment, Competition.Isonline, Competition.Startcheckin, Competition.Endcheckin, Competition.Kind);
            String prefix = "INSERT INTO Competitions " + "(competitionname, address1, banner, logo, prize1, prize2, prize3, linkforregistration, lastdateforregistration, body , startdate, enddate, startTime, endTime, ispro, discord, console, isiesa, linkforstream, competitionstatus, ispayment, isonline, startcheckin, endcheckin, competitiontype) ";

            command = prefix + sb.ToString();

            return command;
        }

        public int InsertGameInC(int cID, int gcID, int oID) //Add_New_Competition.html - Add row to game_competition 
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception)
            {
                // write to log

                throw new Exception("Problem inserting to the server, please try again later");
            }

            String cStr = BuildInsertCommand(cID, gcID, oID);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildInsertCommand(int cID, int gcID, int oID) //Add_New_Competition.html - Add row to game_competition
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            string date = DateTime.Now.ToString("dd-mm-yyyy");
            string time = DateTime.Now.ToString("hh:mm:ss");

            String prefix1 = "INSERT INTO Competition_Game VALUES (" + cID + ", " + gcID + ")";
            String prefix2 = " INSERT INTO Orgenaizer_Competition VALUES (" + oID + ", " + cID + " , " + date + " , '" + time + "')";
            command = prefix1 + prefix2;

            return command;


        }


        //---Add_New_Competition.html--- *Close*


        //---Add_New_Post.html--- *Open*

        public int GetnewIdPost() //Add_New_Post.html - method OP1
        {
            postId = 0;

            SqlConnection con = null;

            try
            {

                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT TOP 1 * FROM Posts ORDER BY postid DESC";


                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read()) //1 row
                {
                    postId = Convert.ToInt32(dr["postid"]); //future id of the user
                }

                postId += 1;

                return postId; //Future id of the user
            }
            catch (Exception)
            {
                throw new Exception("Problem getting the information from the server, please try again later");
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public int InsertPost(Posts post) //Add_New_Post.html - method OP2 (Insert: Post (1))
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception)
            {
                // write to log

                throw new Exception("Problem inserting to the server, please try again later");
            }

            String cStr = BuildInsertCommand(post);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildInsertCommand(Posts post) //Add_New_Post.html - method OP2 (Insert: Post (2))
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')", post.Title, post.Body1, post.Body2, post.Body3, post.Image1, post.Category, post.Postdate, post.Status1);
            String prefix = "INSERT INTO Posts " + "(title, body1, body2, body3, image1, category, postdate, status1) ";

            command = prefix + sb.ToString();

            return command;

        }


        //---Add_New_Post.html--- *Close*


        //---Orgenaizer_Main_Page.html--- *Open*

        public List<Competitions> GetOrgenaizerCompetitions(int OID)
        {
            SqlConnection con = null;
            List<Competitions> cList = new List<Competitions>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = " update Competitions set Competitionstatus = '4' where Competitionstatus='2' and CONVERT(date,enddate,103) > getdate() select * from Competitions inner join Orgenaizer_Competition ON Competitions.competitionid = Orgenaizer_Competition.competitionid where Orgenaizer_Competition.orgenaizerid=" + OID + " and Competitions.status1 = 1 order by CONVERT(date,enddate,103) ";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Competitions c = new Competitions();
                    c.Competitionid = Convert.ToInt32(dr["competitionid"]);
                    c.Competitionname = (string)dr["competitionname"];
                    c.Isonline = Convert.ToInt32(dr["isonline"]);
                    c.Address1 = (string)dr["address1"];
                    c.Banner = (string)dr["banner"];
                    c.Logo = (string)dr["logo"];
                    c.Prize1 = (string)dr["prize1"];
                    c.Prize2 = (string)dr["prize2"];
                    c.Price3 = (string)dr["prize3"];
                    c.Linkforregistration = (string)dr["linkforregistration"];
                    //c.Lastdateforregistration = Convert.ToDateTime(dr["lastdateforregistration"]);

                    renderdate1 = (dr["Lastdateforregistration"] != DBNull.Value) ? (string)dr["Lastdateforregistration"] : default;

                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            c.Lastdateforregistration = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            c.Lastdateforregistration = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        c.Lastdateforregistration = (dr["Lastdateforregistration"] != DBNull.Value) ? Convert.ToDateTime(dr["Lastdateforregistration"]) : default;
                    }

                    c.Body = (string)dr["body"];

                    //c.Startdate = Convert.ToDateTime(dr["startdate"]);

                    renderdate2 = (dr["startdate"] != DBNull.Value) ? (string)dr["startdate"] : default;

                    if (renderdate2 != null)
                    {

                        if (renderdate2.Contains("AM") || renderdate2.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            c.Startdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            c.Startdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                       c.Startdate = (dr["startdate"] != DBNull.Value) ? Convert.ToDateTime(dr["startdate"]) : default;
                    }


                    //c.Enddate = Convert.ToDateTime(dr["enddate"]);

                    renderdate3 = (dr["enddate"] != DBNull.Value) ? (string)dr["enddate"] : default;

                    if (renderdate3 != null)
                    {

                        if (renderdate3.Contains("AM") || renderdate3.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate3.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            c.Enddate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate3.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            c.Enddate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        c.Enddate = (dr["enddate"] != DBNull.Value) ? Convert.ToDateTime(dr["enddate"]) : default;
                    }


                    c.Startime = ((TimeSpan)dr["starttime"]);
                    c.Endtime = ((TimeSpan)dr["endtime"]);
                    c.Ispro = Convert.ToInt32(dr["ispro"]);
                    c.Discord = (string)dr["discord"];
                    c.Console = (string)dr["console"];
                    c.Isiesa = Convert.ToInt32(dr["isiesa"]);
                    c.Linkforstream = (string)dr["linkforstream"];
                    c.Numofparticipants = (dr["numofparticipants"] != DBNull.Value) ? Convert.ToInt32(dr["numofparticipants"]) : default;
                    c.Competitionstatus = (string)dr["competitionstatus"];
                    c.Status1 = Convert.ToInt32(dr["status1"]);
                    c.IsPayment = Convert.ToInt32(dr["ispayment"]);
                    c.Startcheckin = ((TimeSpan)dr["startcheckin"]);
                    c.Endcheckin = ((TimeSpan)dr["endcheckin"]);


                    if (c.Status1 == 1)
                        cList.Add(c);
                }

                return cList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        public List<Gamers> GetGamers()
        {
            SqlConnection con = null;
            List<Gamers> gList = new List<Gamers>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from Gamers where Gamers.status1 = 1 and Gamers.license = 1";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Gamers g = new Gamers();
                    g.Userid = (dr["userid"] != DBNull.Value) ? Convert.ToInt32(dr["userid"]) : default;
                    g.Email = (dr["email"] != DBNull.Value) ? (string)dr["email"] : default;
                    g.Nickname = (dr["nickname"] != DBNull.Value) ? (string)dr["nickname"] : default;
                    g.Firstname = (dr["firstname"] != DBNull.Value) ? (string)dr["firstname"] : default;
                    g.Lastname = (dr["lastname"] != DBNull.Value) ? (string)dr["lastname"] : default;
                    g.Gender = (dr["gender"] != DBNull.Value) ? (string)dr["gender"] : default;
                    g.Id = (dr["id"] != DBNull.Value) ? Convert.ToInt32(dr["id"]) : default; //Needs to be int in SQL*
                    g.Phone = (dr["phone"] != DBNull.Value) ? (string)dr["phone"] : default;
                    //g.Dob = (dr["dob"] != DBNull.Value) ? Convert.ToDateTime(dr["dob"]) : default;

                    renderdate1 = (dr["dob"] != DBNull.Value) ? (string)dr["dob"] : default;

                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            g.Dob = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            g.Dob = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        g.Dob = (dr["dob"] != DBNull.Value) ? Convert.ToDateTime(dr["dob"]) : default;
                    }





                    g.Address1 = (dr["address1"] != DBNull.Value) ? (string)dr["address1"] : default;
                    g.Discorduser = (dr["discorduser"] != DBNull.Value) ? (string)dr["discorduser"] : default;
                    g.Picture = (dr["picture"] != DBNull.Value) ? (string)dr["picture"] : default;
                    //g.Registrationdate = (dr["registrationdate"] != DBNull.Value) ? Convert.ToDateTime(dr["registrationdate"]) : default;

                    renderdate2 = (dr["registrationdate"] != DBNull.Value) ? (string)dr["registrationdate"] : default;

                    if (renderdate2 != null)
                    {

                        if (renderdate2.Contains("AM") || renderdate2.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            g.Registrationdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            g.Registrationdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        g.Registrationdate = (dr["registrationdate"] != DBNull.Value) ? Convert.ToDateTime(dr["registrationdate"]) : default;
                    }


                    //g.Outofdate = (dr["outofdate"] != DBNull.Value) ? Convert.ToDateTime(dr["outofdate"]) : default;

                    renderdate3 = (dr["outofdate"] != DBNull.Value) ? (string)dr["outofdate"] : default;

                    if (renderdate3 != null)
                    {

                        if (renderdate3.Contains("AM") || renderdate3.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate3.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            g.Outofdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate3.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            g.Outofdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        g.Outofdate = (dr["outofdate"] != DBNull.Value) ? Convert.ToDateTime(dr["outofdate"]) : default;
                    }


                    g.License = (dr["license"] != DBNull.Value) ? Convert.ToInt32(dr["license"]) : default;
                    g.Status1 = (dr["status1"] != DBNull.Value) ? Convert.ToInt32(dr["status1"]) : default;


                    gList.Add(g);
                }

                return gList;
            }
            catch (Exception ex)
            {
                throw new Exception("Problem getting the information from the server, please try again later");
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }


        //---Orgenaizer_Main_Page.html--- *Close*


        //---Post.html--- *Open*

        public Posts getPostSQL(int id_toserver) //Post.html - method OP3
        {

            SqlConnection con = null;
            Posts p = new Posts();

            try
            {

                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "UPDATE Posts SET viewsnum = viewsnum+1 WHERE postid =" + id_toserver + " SELECT * FROM Posts WHERE postid = " + id_toserver;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end


                while (dr.Read()) //1 row
                {

                    p.Postid = (dr["postid"] != DBNull.Value) ? Convert.ToInt32(dr["postid"]) : default;
                    p.Title = (dr["title"] != DBNull.Value) ? (string)dr["title"] : default;
                    p.Body1 = (dr["body1"] != DBNull.Value) ? (string)dr["body1"] : default;
                    p.Body2 = (dr["body2"] != DBNull.Value) ? (string)dr["body2"] : default;
                    p.Body3 = (dr["body3"] != DBNull.Value) ? (string)dr["body3"] : default;
                    p.Image1 = (dr["image1"] != DBNull.Value) ? (string)dr["image1"] : default;
                    p.Category = (dr["category"] != DBNull.Value) ? (string)dr["category"] : default;
                    //p.Postdate = (dr["postdate"] != DBNull.Value) ? Convert.ToDateTime(dr["postdate"]) : default;

                    renderdate1 = (dr["postdate"] != DBNull.Value) ? (string)dr["postdate"] : default;

                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            p.Postdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            p.Postdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        p.Postdate = (dr["postdate"] != DBNull.Value) ? Convert.ToDateTime(dr["postdate"]) : default;
                    }



                    p.Status1 = (dr["status1"] != DBNull.Value) ? Convert.ToInt32(dr["status1"]) : default;

                }

                return p;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public List<Posts> ReadAnotherPostsSQL(int postid, string categoryname) //Post.html - method OP4
        {

            SqlConnection con = null;
            List<Posts> PostsList = new List<Posts>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file


                String selectSTR = "SELECT TOP 3 WITH TIES * FROM Posts WHERE category = '" + categoryname + "' and postid != '" + postid + "' and status1 = 1 ORDER BY postid desc";

                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    Posts post = new Posts();

                    post.Postid = (dr["postid"] != DBNull.Value) ? Convert.ToInt32(dr["postid"]) : default;
                    post.Title = (dr["title"] != DBNull.Value) ? (string)dr["title"] : default;
                    post.Body1 = (dr["body1"] != DBNull.Value) ? (string)dr["body1"] : default;
                    post.Body2 = (dr["body2"] != DBNull.Value) ? (string)dr["body2"] : default;
                    post.Body3 = (dr["body3"] != DBNull.Value) ? (string)dr["body3"] : default;
                    post.Image1 = (dr["image1"] != DBNull.Value) ? (string)dr["image1"] : default;
                    post.Category = (dr["category"] != DBNull.Value) ? (string)dr["category"] : default;
                    //post.Postdate = (dr["postdate"] != DBNull.Value) ? Convert.ToDateTime(dr["postdate"]) : default;

                    renderdate1 = (dr["postdate"] != DBNull.Value) ? (string)dr["postdate"] : default;
                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            post.Postdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            post.Postdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        post.Postdate = (dr["postdate"] != DBNull.Value) ? Convert.ToDateTime(dr["postdate"]) : default;
                    }



                    post.Status1 = (dr["status1"] != DBNull.Value) ? Convert.ToInt32(dr["status1"]) : default;

                    PostsList.Add(post);
                }

                return PostsList;
            }
            catch (Exception)
            {

                throw new Exception("Problem getting the information from the server, please try again later");
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }


        //---Post.html--- *Close*


        //---Manager_Main_Page.html--- *Open*


        public List<Gamers> ReadGamersMSQL() //Manager_Main_Page.html - method OM1
        {

            SqlConnection con = null;
            List<Gamers> GamersList = new List<Gamers>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Gamers WHERE status1 = 'False' ";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    Gamers gamer = new Gamers();

                    gamer.Userid = (dr["userid"] != DBNull.Value) ? Convert.ToInt32(dr["userid"]) : default;
                    gamer.Firstname = (dr["firstname"] != DBNull.Value) ? (string)dr["firstname"] : default;
                    gamer.Lastname = (dr["lastname"] != DBNull.Value) ? (string)dr["lastname"] : default;
                    gamer.Nickname = (dr["nickname"] != DBNull.Value) ? (string)dr["nickname"] : default;
                    gamer.Phone = (dr["phone"] != DBNull.Value) ? (string)dr["phone"] : default;
                    gamer.Email = (dr["email"] != DBNull.Value) ? (string)dr["email"] : default;
                    //gamer.Dob = (dr["dob"] != DBNull.Value) ? Convert.ToDateTime(dr["dob"]) : default;

                    renderdate1 = (dr["dob"] != DBNull.Value) ? (string)dr["dob"] : default;

                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            gamer.Dob = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            gamer.Dob = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        gamer.Dob = (dr["dob"] != DBNull.Value) ? Convert.ToDateTime(dr["dob"]) : default;
                    }




                    //gamer.Registrationdate = (dr["registrationdate"] != DBNull.Value) ? Convert.ToDateTime(dr["registrationdate"]) : default;

                    renderdate2 = (dr["registrationdate"] != DBNull.Value) ? (string)dr["registrationdate"] : default;

                    if (renderdate2 != null)
                    {

                        if (renderdate2.Contains("AM") || renderdate2.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            gamer.Registrationdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            gamer.Registrationdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        gamer.Registrationdate = (dr["registrationdate"] != DBNull.Value) ? Convert.ToDateTime(dr["registrationdate"]) : default;
                    }



                    GamersList.Add(gamer);
                }

                return GamersList;
            }
            catch (Exception)
            {

                throw new Exception("Problem getting the information from the server, please try again later");
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public int DeleteGamerSQL(int todeleteid) //Manager_Main_Page.html - method OM2
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildDeleteCommandGamer(todeleteid);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildDeleteCommandGamer(int todeleteid)
        {
            String command;
            command = "DELETE From Gamers WHERE userid = " + todeleteid;
            return command;
        }

        public int UpdateGamerStatusSQL(int toupdateid) //Manager_Main_Page.html - method OM3
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildUpdateCommandSGamer(toupdateid);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildUpdateCommandSGamer(int toupdateid)
        {
            String command;
            command = "UPDATE Gamers SET status1 = 1 WHERE userid = " + toupdateid;
            return command;
        }

        public List<Orgenaizers> ReadOrgenaizersMSQL() //Manager_Main_Page.html - method OM4
        {

            SqlConnection con2 = null;
            List<Orgenaizers> OrgenaizersList = new List<Orgenaizers>();

            try
            {
                con2 = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR2 = "SELECT * FROM Orgenaizers WHERE status1 = 'False' ";
                SqlCommand cmd2 = new SqlCommand(selectSTR2, con2);

                // get a reader
                SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr2.Read())
                {
                    Orgenaizers orgenaizer = new Orgenaizers();

                    orgenaizer.Userid = (dr2["userid"] != DBNull.Value) ? Convert.ToInt32(dr2["userid"]) : default;
                    orgenaizer.Firstname = (dr2["firstname"] != DBNull.Value) ? (string)dr2["firstname"] : default;
                    orgenaizer.Lastname = (dr2["lastname"] != DBNull.Value) ? (string)dr2["lastname"] : default;
                    orgenaizer.Phone = (dr2["phone"] != DBNull.Value) ? (string)dr2["phone"] : default;
                    orgenaizer.Email = (dr2["email"] != DBNull.Value) ? (string)dr2["email"] : default;
                    //orgenaizer.Dob = (dr2["dob"] != DBNull.Value) ? Convert.ToDateTime(dr2["dob"]) : default;

                    renderdate1 = (dr2["dob"] != DBNull.Value) ? (string)dr2["dob"] : default;

                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            orgenaizer.Dob = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            orgenaizer.Dob = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        orgenaizer.Dob = (dr2["dob"] != DBNull.Value) ? Convert.ToDateTime(dr2["dob"]) : default;
                    }



                    orgenaizer.Comunityname = (dr2["communityname"] != DBNull.Value) ? (string)dr2["communityname"] : default;

                    OrgenaizersList.Add(orgenaizer);
                }

                return OrgenaizersList;
            }
            catch (Exception)
            {

                throw new Exception("Problem getting the information from the server, please try again later");
            }
            finally
            {
                if (con2 != null)
                {
                    con2.Close();
                }

            }

        }

        public int DeleteOrgenaizerSQL(int todeleteid) //Manager_Main_Page.html - method OM5
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildDeleteCommandOrgenaizer(todeleteid);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildDeleteCommandOrgenaizer(int todeleteid)
        {
            String command;
            command = "DELETE From Orgenaizers WHERE userid = " + todeleteid;
            return command;
        }

        public int UpdateOrgenaizerStatusSQL(int toupdateid) //Manager_Main_Page.html - method OM5
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildUpdateCommandSOrgenaizer(toupdateid);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildUpdateCommandSOrgenaizer(int toupdateid)
        {
            String command;
            command = "UPDATE Orgenaizers SET status1 = 1 WHERE userid = " + toupdateid;
            return command;
        }

        public List<Posts> ReadPostsMSQL() //Manager_Main_Page.html - method OM7
        {

            SqlConnection con3 = null;
            List<Posts> PostsList = new List<Posts>();

            try
            {
                con3 = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                //Update competitions + Get First 5 Posts
                String selectSTR3 = "UPDATE Competitions set Competitionstatus = '4' where Competitionstatus='2' and CONVERT(date,enddate,103) > getdate() SELECT TOP 5 WITH TIES * FROM Posts where status1='1' ORDER BY postid desc";

                SqlCommand cmd3 = new SqlCommand(selectSTR3, con3);

                // get a reader
                SqlDataReader dr3 = cmd3.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr3.Read())
                {
                    Posts post = new Posts();

                    post.Postid = (dr3["postid"] != DBNull.Value) ? Convert.ToInt32(dr3["postid"]) : default;
                    post.Title = (dr3["title"] != DBNull.Value) ? (string)dr3["title"] : default;
                    post.Category = (dr3["category"] != DBNull.Value) ? (string)dr3["category"] : default;
                    post.Viewsnum = (dr3["viewsnum"] != DBNull.Value) ? Convert.ToInt32(dr3["viewsnum"]) : default;
                    //post.Postdate = (dr3["postdate"] != DBNull.Value) ? Convert.ToDateTime(dr3["postdate"]) : default;

                    renderdate1 = (dr3["postdate"] != DBNull.Value) ? (string)dr3["postdate"] : default;

                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            post.Postdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            post.Postdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        post.Postdate = (dr3["postdate"] != DBNull.Value) ? Convert.ToDateTime(dr3["postdate"]) : default;
                    }


                    PostsList.Add(post);
                }

                return PostsList;
            }
            catch (Exception)
            {

                throw new Exception("Problem getting the information from the server, please try again later");
            }
            finally
            {
                if (con3 != null)
                {
                    con3.Close();
                }

            }

        }

        public List<Competitions> ReadCompetitionsMSQL() //Manager_Main_Page.html - method OM8
        {

            SqlConnection con4 = null;
            List<Competitions> CompetitionsList = new List<Competitions>();

            try
            {
                con4 = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR4 = "SELECT DISTINCT Competitions.competitionid, Competitions.competitionname, Competitions.competitionstatus, Competitions.ispro, Orgenaizers.firstname + ' ' + Orgenaizers.lastname AS 'orgenaizername', Competitions.startdate, Competitions.enddate,Competitions.logo, Competitions.address1, Competitions.isonline FROM Competitions inner join Orgenaizer_Competition ON Competitions.competitionid = Orgenaizer_Competition.competitionid inner join Orgenaizers ON Orgenaizer_Competition.orgenaizerid = Orgenaizers.userid where Competitions.competitionstatus = '1' or Competitions.competitionstatus = '5'";

                SqlCommand cmd4 = new SqlCommand(selectSTR4, con4);

                // get a reader
                SqlDataReader dr4 = cmd4.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr4.Read())
                {
                    Competitions competition = new Competitions();

                    competition.Competitionid = (dr4["competitionid"] != DBNull.Value) ? Convert.ToInt32(dr4["competitionid"]) : default;
                    competition.Competitionname = (dr4["competitionname"] != DBNull.Value) ? (string)dr4["competitionname"] : default;
                    competition.Competitionstatus = (dr4["competitionstatus"] != DBNull.Value) ? (string)dr4["competitionstatus"] : default;
                    competition.Logo = (dr4["logo"] != DBNull.Value) ? (string)dr4["logo"] : default;
                    competition.Address1 = (dr4["address1"] != DBNull.Value) ? (string)dr4["address1"] : default;
                    competition.Ispro = (dr4["ispro"] != DBNull.Value) ? Convert.ToInt32(dr4["ispro"]) : default;
                    competition.Isonline = (dr4["isonline"] != DBNull.Value) ? Convert.ToInt32(dr4["isonline"]) : default;
                    competition.Console = (dr4["orgenaizername"] != DBNull.Value) ? (string)dr4["orgenaizername"] : default; //'False' Console -> gets the orgenaizer name instead*
                    //competition.Startdate = (dr4["startdate"] != DBNull.Value) ? Convert.ToDateTime(dr4["startdate"]) : default;

                    renderdate1 = (dr4["startdate"] != DBNull.Value) ? (string)dr4["startdate"] : default;

                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            competition.Startdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            competition.Startdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        competition.Startdate = (dr4["startdate"] != DBNull.Value) ? Convert.ToDateTime(dr4["startdate"]) : default;
                    }


                    //competition.Enddate = (dr4["enddate"] != DBNull.Value) ? Convert.ToDateTime(dr4["enddate"]) : default;


                    renderdate2 = (dr4["enddate"] != DBNull.Value) ? (string)dr4["enddate"] : default;

                    if (renderdate2 != null)
                    {

                        if (renderdate2.Contains("AM") || renderdate2.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            competition.Enddate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            competition.Enddate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        competition.Enddate = (dr4["enddate"] != DBNull.Value) ? Convert.ToDateTime(dr4["enddate"]) : default;
                    }

                    CompetitionsList.Add(competition);
                }

                return CompetitionsList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con4 != null)
                {
                    con4.Close();
                }

            }

        }

        public Dictionary<int, int> GetStatisticsSQL() //Manager_Main_Page.html - method OM9
        {

            SqlConnection con5 = null;
            Dictionary<int, int> statisticsdict = new Dictionary<int, int>();

            try
            {
                con5 = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR5 = "SELECT(SELECT COUNT(*) FROM Gamers WHERE status1 = 1) AS 'Num_Gamers', (SELECT COUNT(*) FROM Orgenaizers WHERE status1 = 1) AS Num_Orgenaizers, (SELECT COUNT(*) FROM Competitions WHERE status1 = 1 AND competitionstatus != 3 AND CONVERT(datetime, enddate, 103) > getdate()) AS Num_Future_Comp, (SELECT COUNT(*) FROM Competitions WHERE status1 = 1 AND competitionstatus = '7' OR competitionstatus = '8' AND CONVERT(datetime, enddate, 103) < getdate()) AS Num_Past_Comp, (SELECT COUNT(*) FROM Gamers WHERE status1 = 0) AS 'Num_Gamers_Waithing', (SELECT COUNT(*) FROM Orgenaizers WHERE status1 = 0) AS 'Num_Orgenaizers_Waithing', (SELECT COUNT(*) FROM Competitions WHERE competitionstatus = '1' or competitionstatus = '5' AND CONVERT(datetime, enddate, 103) < getdate()) AS Num_Competition_Waithing";
                SqlCommand cmd5 = new SqlCommand(selectSTR5, con5);

                SqlDataReader dr5 = cmd5.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr5.Read())
                {

                    statisticsdict.Add(1, (dr5["Num_Gamers"] != DBNull.Value) ? Convert.ToInt32(dr5["Num_Gamers"]) : default);
                    statisticsdict.Add(2, (dr5["Num_Orgenaizers"] != DBNull.Value) ? Convert.ToInt32(dr5["Num_Orgenaizers"]) : default);
                    statisticsdict.Add(3, (dr5["Num_Future_Comp"] != DBNull.Value) ? Convert.ToInt32(dr5["Num_Future_Comp"]) : default);
                    statisticsdict.Add(4, (dr5["Num_Past_Comp"] != DBNull.Value) ? Convert.ToInt32(dr5["Num_Past_Comp"]) : default);
                    statisticsdict.Add(5, (dr5["Num_Gamers_Waithing"] != DBNull.Value) ? Convert.ToInt32(dr5["Num_Gamers_Waithing"]) : default);
                    statisticsdict.Add(6, (dr5["Num_Orgenaizers_Waithing"] != DBNull.Value) ? Convert.ToInt32(dr5["Num_Orgenaizers_Waithing"]) : default);
                    statisticsdict.Add(7, (dr5["Num_Competition_Waithing"] != DBNull.Value) ? Convert.ToInt32(dr5["Num_Competition_Waithing"]) : default);

                }

                return statisticsdict;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con5 != null)
                {
                    con5.Close();
                }

            }

        }

        public Dictionary<int, int> GetGraphSQL() //Manager_Main_Page.html - method OM10
        {

            SqlConnection con6 = null;
            Dictionary<int, int> graphdict = new Dictionary<int, int>();

            try
            {
                con6 = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR6 = "SELECT(SELECT COUNT(userid) FROM Gamers WHERE userid IN(SELECT userid FROM Gamers) AND DATEDIFF(MONTH, convert(date, registrationdate, 103), convert(date, GETDATE(), 103)) = 6) AS 'Month-6', (SELECT COUNT(userid) FROM Gamers WHERE userid IN(SELECT userid FROM Gamers) AND DATEDIFF(MONTH, convert(date, registrationdate, 103), convert(date, GETDATE(), 103)) = 5) AS 'Month-5', (SELECT COUNT(userid) FROM Gamers WHERE userid IN(SELECT userid FROM Gamers) AND DATEDIFF(MONTH, convert(date, registrationdate, 103), convert(date, GETDATE(), 103)) = 4) AS 'Month-4', (SELECT COUNT(userid) FROM Gamers WHERE userid IN(SELECT userid FROM Gamers) AND DATEDIFF(MONTH, convert(date, registrationdate, 103), convert(date, GETDATE(), 103)) = 3) AS 'Month-3', (SELECT COUNT(userid) FROM Gamers WHERE userid IN(SELECT userid FROM Gamers) AND DATEDIFF(MONTH, convert(date, registrationdate, 103), convert(date, GETDATE(), 103)) = 2) AS 'Month-2', (SELECT COUNT(userid) FROM Gamers WHERE userid IN(SELECT userid FROM Gamers) AND DATEDIFF(MONTH, convert(date, registrationdate, 103), convert(date, GETDATE(), 103)) = 1) AS 'Month-1', (SELECT COUNT(userid) FROM Gamers WHERE userid IN(SELECT userid FROM Gamers) AND DATEDIFF(MONTH, convert(date, registrationdate, 103), convert(date, GETDATE(), 103)) = 0) AS 'Month0';";

                SqlCommand cmd6 = new SqlCommand(selectSTR6, con6);

                SqlDataReader dr6 = cmd6.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr6.Read())
                {

                    graphdict.Add(1, (dr6["Month-6"] != DBNull.Value) ? Convert.ToInt32(dr6["Month-6"]) : default);
                    graphdict.Add(2, (dr6["Month-5"] != DBNull.Value) ? Convert.ToInt32(dr6["Month-5"]) : default);
                    graphdict.Add(3, (dr6["Month-4"] != DBNull.Value) ? Convert.ToInt32(dr6["Month-4"]) : default);
                    graphdict.Add(4, (dr6["Month-3"] != DBNull.Value) ? Convert.ToInt32(dr6["Month-3"]) : default);
                    graphdict.Add(5, (dr6["Month-2"] != DBNull.Value) ? Convert.ToInt32(dr6["Month-2"]) : default);
                    graphdict.Add(6, (dr6["Month-1"] != DBNull.Value) ? Convert.ToInt32(dr6["Month-1"]) : default);
                    graphdict.Add(7, (dr6["Month0"] != DBNull.Value) ? Convert.ToInt32(dr6["Month0"]) : default);

                }

                return graphdict;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con6 != null)
                {
                    con6.Close();
                }

            }

        }


        //---Manager_Main_Page.html--- *Close*


        //---Database.html--- *Open*

        public List<Competitions> GetCompetitionsByCategoryToDelete(int CategoryID) //DataBase.html - method SM4
        {

            SqlConnection con = null;
            List<Competitions> CompetitionsListToDelete = new List<Competitions>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT DISTINCT Competitions.competitionid AS 'Competition ID' FROM Competitions INNER JOIN Gamer_Competition ON Competitions.competitionid=Gamer_Competition.competitionid INNER JOIN Competition_Game ON Competition_Game.competitionid=Competitions.competitionid WHERE Competition_Game.categoryid= " + CategoryID + " AND Gamer_Competition.status1=1";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    Competitions c = new Competitions();
                    c.Competitionid = (dr["Competition ID"] != DBNull.Value) ? Convert.ToInt32(dr["Competition ID"]) : default;
                    CompetitionsListToDelete.Add(c);
                }

                return CompetitionsListToDelete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }


        public List<Posts> ReadDBPostsSQL() //Database.html - method OD1
        {

            SqlConnection con1 = null;
            List<Posts> PostsList = new List<Posts>();

            try
            {
                con1 = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR1 = "SELECT * FROM Posts ORDER BY postid desc";

                SqlCommand cmd1 = new SqlCommand(selectSTR1, con1);

                // get a reader
                SqlDataReader dr1 = cmd1.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr1.Read())
                {
                    Posts post = new Posts();

                    post.Postid = (dr1["postid"] != DBNull.Value) ? Convert.ToInt32(dr1["postid"]) : default;
                    post.Title = (dr1["title"] != DBNull.Value) ? (string)dr1["title"] : default;
                    post.Category = (dr1["category"] != DBNull.Value) ? (string)dr1["category"] : default;
                    post.Viewsnum = (dr1["viewsnum"] != DBNull.Value) ? Convert.ToInt32(dr1["viewsnum"]) : default;
                    //post.Postdate = (dr1["postdate"] != DBNull.Value) ? Convert.ToDateTime(dr1["postdate"]) : default;

                    renderdate1 = (dr1["postdate"] != DBNull.Value) ? (string)dr1["postdate"] : default;

                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            post.Postdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            post.Postdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        post.Postdate = (dr1["postdate"] != DBNull.Value) ? Convert.ToDateTime(dr1["postdate"]) : default;
                    }

                    PostsList.Add(post);
                }

                return PostsList;
            }
            catch (Exception)
            {
                throw new Exception("בעיה בהתקשורת עם השרת, נא נסה שנית מאוחר יותר");
            }
            finally
            {
                if (con1 != null)
                {
                    con1.Close();
                }

            }

        }


        public int DeletePostSQL(int todeleteid) //Database.html - method OD2
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildDeleteCommandPost(todeleteid);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception)
            {
                throw new Exception("בעיה בהתקשורת עם השרת, נא נסה שנית מאוחר יותר");
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildDeleteCommandPost(int todeleteid)
        {
            String command;
            command = "DELETE From Posts WHERE postid = " + todeleteid;
            return command;
        }

        public int DeleteCompSQL(int todeleteid) //Database.html - method OD3
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception)
            {
                throw new Exception("בעיה בהתקשורת עם השרת, נא נסה שנית מאוחר יותר");
            }

            String cStr = BuildDeleteCommandComp(todeleteid);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception)
            {
                throw new Exception("בעיה בהתקשורת עם השרת, נא נסה שנית מאוחר יותר");
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildDeleteCommandComp(int todeleteid)
        {
            String command;
            command = "DELETE From Competitions WHERE competitionid = " + todeleteid;
            return command;
        }

        public List<Competitions> ReadCompetitionsDBSQL() //Database.html - method OD4
        {

            SqlConnection con4 = null;
            List<Competitions> CompetitionsList = new List<Competitions>();

            try
            {
                con4 = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR4 = "SELECT Competitions.competitionid, Competitions.competitionname, Competitions.competitionstatus, Competitions.isiesa, Competitions.ispro, Orgenaizers.firstname + ' ' + Orgenaizers.lastname AS 'orgenaizername', Competitions.startdate, Competitions.enddate FROM Competitions inner join Orgenaizer_Competition ON Competitions.competitionid = Orgenaizer_Competition.competitionid inner join Orgenaizers ON Orgenaizer_Competition.orgenaizerid = Orgenaizers.userid";

                SqlCommand cmd4 = new SqlCommand(selectSTR4, con4);

                // get a reader
                SqlDataReader dr4 = cmd4.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr4.Read())
                {
                    Competitions competition = new Competitions();

                    competition.Competitionid = (dr4["competitionid"] != DBNull.Value) ? Convert.ToInt32(dr4["competitionid"]) : default;
                    competition.Competitionname = (dr4["competitionname"] != DBNull.Value) ? (string)dr4["competitionname"] : default;
                    competition.Competitionstatus = (dr4["competitionstatus"] != DBNull.Value) ? (string)dr4["competitionstatus"] : default;
                    competition.Isiesa = (dr4["isiesa"] != DBNull.Value) ? Convert.ToInt32(dr4["isiesa"]) : default;
                    competition.Ispro = (dr4["ispro"] != DBNull.Value) ? Convert.ToInt32(dr4["ispro"]) : default;
                    competition.Console = (dr4["orgenaizername"] != DBNull.Value) ? (string)dr4["orgenaizername"] : default; //'False' Console -> gets the orgenaizer name instead*
                    //competition.Startdate = (dr4["startdate"] != DBNull.Value) ? Convert.ToDateTime(dr4["startdate"]) : default;


                    renderdate1 = (dr4["startdate"] != DBNull.Value) ? (string)dr4["startdate"] : default;

                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            competition.Startdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            competition.Startdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        competition.Startdate = (dr4["startdate"] != DBNull.Value) ? Convert.ToDateTime(dr4["startdate"]) : default;
                    }


                    //competition.Enddate = (dr4["enddate"] != DBNull.Value) ? Convert.ToDateTime(dr4["enddate"]) : default;

                    renderdate2 = (dr4["enddate"] != DBNull.Value) ? (string)dr4["enddate"] : default;

                    if (renderdate2 != null)
                    {

                        if (renderdate2.Contains("AM") || renderdate2.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            competition.Enddate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            competition.Enddate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        competition.Enddate = (dr4["enddate"] != DBNull.Value) ? Convert.ToDateTime(dr4["enddate"]) : default;
                    }


                    CompetitionsList.Add(competition);
                }

                return CompetitionsList;
            }
            catch (Exception)
            {
                throw new Exception("בעיה בהתקשורת עם השרת, נא נסה שנית מאוחר יותר");
            }
            finally
            {
                if (con4 != null)
                {
                    con4.Close();
                }

            }

        }

        public List<Gamers> ReadGamersDBSQL() //Database.html - method OD5
        {

            SqlConnection con = null;
            List<Gamers> GamersList = new List<Gamers>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Gamers";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    Gamers gamer = new Gamers();

                    gamer.Userid = (dr["userid"] != DBNull.Value) ? Convert.ToInt32(dr["userid"]) : default;
                    gamer.Firstname = (dr["firstname"] != DBNull.Value) ? (string)dr["firstname"] : default;
                    gamer.Lastname = (dr["lastname"] != DBNull.Value) ? (string)dr["lastname"] : default;
                    gamer.Nickname = (dr["nickname"] != DBNull.Value) ? (string)dr["nickname"] : default;
                    gamer.Phone = (dr["phone"] != DBNull.Value) ? (string)dr["phone"] : default;
                    gamer.Email = (dr["email"] != DBNull.Value) ? (string)dr["email"] : default;
                    //gamer.Dob = (dr["dob"] != DBNull.Value) ? Convert.ToDateTime(dr["dob"]) : default;

                    renderdate1 = (dr["dob"] != DBNull.Value) ? (string)dr["dob"] : default;

                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            gamer.Dob = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            gamer.Dob = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        gamer.Dob = (dr["dob"] != DBNull.Value) ? Convert.ToDateTime(dr["dob"]) : default;
                    }


                    //gamer.Registrationdate = (dr["registrationdate"] != DBNull.Value) ? Convert.ToDateTime(dr["registrationdate"]) : default;

                    renderdate2 = (dr["registrationdate"] != DBNull.Value) ? (string)dr["registrationdate"] : default;

                    if (renderdate2 != null)
                    {

                        if (renderdate2.Contains("AM") || renderdate2.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            gamer.Registrationdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            gamer.Registrationdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        gamer.Registrationdate = (dr["registrationdate"] != DBNull.Value) ? Convert.ToDateTime(dr["registrationdate"]) : default;
                    }


                    GamersList.Add(gamer);
                }

                return GamersList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public List<Orgenaizers> ReadOrgenaizersDBSQL() //Database.html - method OD6
        {

            SqlConnection con2 = null;
            List<Orgenaizers> OrgenaizersList = new List<Orgenaizers>();

            try
            {
                con2 = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR2 = "SELECT * FROM Orgenaizers";
                SqlCommand cmd2 = new SqlCommand(selectSTR2, con2);

                // get a reader
                SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr2.Read())
                {
                    Orgenaizers orgenaizer = new Orgenaizers();

                    orgenaizer.Userid = (dr2["userid"] != DBNull.Value) ? Convert.ToInt32(dr2["userid"]) : default;
                    orgenaizer.Firstname = (dr2["firstname"] != DBNull.Value) ? (string)dr2["firstname"] : default;
                    orgenaizer.Lastname = (dr2["lastname"] != DBNull.Value) ? (string)dr2["lastname"] : default;
                    orgenaizer.Phone = (dr2["phone"] != DBNull.Value) ? (string)dr2["phone"] : default;
                    orgenaizer.Email = (dr2["email"] != DBNull.Value) ? (string)dr2["email"] : default;
                    //orgenaizer.Dob = (dr2["dob"] != DBNull.Value) ? Convert.ToDateTime(dr2["dob"]) : default;

                    renderdate1 = (dr2["dob"] != DBNull.Value) ? (string)dr2["dob"] : default;

                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            orgenaizer.Dob = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            orgenaizer.Dob = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        orgenaizer.Dob = (dr2["dob"] != DBNull.Value) ? Convert.ToDateTime(dr2["dob"]) : default;
                    }



                    orgenaizer.Comunityname = (dr2["communityname"] != DBNull.Value) ? (string)dr2["communityname"] : default;

                    OrgenaizersList.Add(orgenaizer);
                }

                return OrgenaizersList;
            }
            catch (Exception)
            {
                throw new Exception("בעיה בהתקשורת עם השרת, נא נסה שנית מאוחר יותר");
            }
            finally
            {
                if (con2 != null)
                {
                    con2.Close();
                }

            }

        }

        public List<Managers> ReadManagersDBSQL() //Database.html - method OD7
        {

            SqlConnection con5 = null;
            List<Managers> ManagersList = new List<Managers>();

            try
            {
                con5 = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR5 = "SELECT * FROM Managers";
                SqlCommand cmd5 = new SqlCommand(selectSTR5, con5);

                // get a reader
                SqlDataReader dr5 = cmd5.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr5.Read())
                {
                    Managers manager = new Managers();

                    manager.Userid = (dr5["userid"] != DBNull.Value) ? Convert.ToInt32(dr5["userid"]) : default;
                    manager.Firstname = (dr5["firstname"] != DBNull.Value) ? (string)dr5["firstname"] : default;
                    manager.Lastname = (dr5["lastname"] != DBNull.Value) ? (string)dr5["lastname"] : default;
                    manager.Role1 = (dr5["role1"] != DBNull.Value) ? (string)dr5["role1"] : default;
                    manager.Phone = (dr5["phone"] != DBNull.Value) ? (string)dr5["phone"] : default;
                    manager.Email = (dr5["email"] != DBNull.Value) ? (string)dr5["email"] : default;
                    //manager.Dob = (dr5["dob"] != DBNull.Value) ? Convert.ToDateTime(dr5["dob"]) : default;

                    renderdate1 = (dr5["dob"] != DBNull.Value) ? (string)dr5["dob"] : default;

                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            manager.Dob = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            manager.Dob = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        manager.Dob = (dr5["dob"] != DBNull.Value) ? Convert.ToDateTime(dr5["dob"]) : default;
                    }



                    ManagersList.Add(manager);
                }

                return ManagersList;
            }
            catch (Exception)
            {
                throw new Exception("בעיה בהתקשורת עם השרת, נא נסה שנית מאוחר יותר");
            }
            finally
            {
                if (con5 != null)
                {
                    con5.Close();
                }

            }

        }

        public List<GamesCategories> ReadCategoriesDBSQL() //Database.html - method OD8
        {
            SqlConnection con6 = null;
            List<GamesCategories> caList = new List<GamesCategories>();

            try
            {
                con6 = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR6 = "select * from GamesCategories";
                SqlCommand cmd = new SqlCommand(selectSTR6, con6);

                // get a reader
                SqlDataReader dr6 = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr6.Read())
                {
                    GamesCategories ca = new GamesCategories();

                    ca.Categoryid = (dr6["categoryid"] != DBNull.Value) ? Convert.ToInt32(dr6["categoryid"]) : default;
                    ca.Categoryname = (dr6["categoryname"] != DBNull.Value) ? (string)dr6["categoryname"] : default;
                    ca.Status1 = (dr6["status1"] != DBNull.Value) ? Convert.ToInt32(dr6["status1"]) : default;

                    caList.Add(ca);
                }

                return caList;
            }
            catch (Exception)
            {
                throw new Exception("בעיה בהתקשורת עם השרת, נא נסה שנית מאוחר יותר");
            }
            finally
            {
                if (con6 != null)
                {
                    con6.Close();
                }

            }
        }

        public int changeStatusSQL(int isactive, int categoryid) //Database.html - method OD9
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildUpdateCommandCStatus(isactive, categoryid);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception)
            {
                throw new Exception("בעיה בהתקשורת עם השרת, נא נסה שנית מאוחר יותר");
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildUpdateCommandCStatus(int isactive, int categoryid)
        {
            String command;
            command = "UPDATE GamesCategories SET status1 = " + isactive + " WHERE categoryid = " + categoryid + " ";
            return command;
        }

        public int editCategorySQL(string newcategory, int categoryid) //Database.html - method OD10
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildUpdateCommandCName(newcategory, categoryid);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception)
            {
                throw new Exception("קטגוריה זו כבר קיימת ברשומות");
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildUpdateCommandCName(string newcategory, int categoryid)
        {
            String command;
            command = "UPDATE GamesCategories SET categoryname = '" + newcategory + "' WHERE categoryid = " + categoryid + " ";
            return command;
        }

        public int addgameCategorySQL(GamesCategories category) //Database.html - method OD11
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception)
            {
                throw new Exception("בעיה בהתקשרות עם השרת, נא נסה שנית מאוחר יותר");
            }

            String cStr = BuildInsertcategoryCommand(category);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception)
            {
                throw new Exception("בעיה בהתקשרות עם השרת, נא נסה שנית מאוחר יותר");
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildInsertcategoryCommand(GamesCategories category)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}')", category.Categoryname);
            String prefix = "INSERT INTO GamesCategories " + "(categoryname) ";

            command = prefix + sb.ToString();

            return command;

        }

        public List<Competitions> GetRanksSQL() //Database.html - method OD12
        {

            SqlConnection con = null;
            List<Competitions> RanksList = new List<Competitions>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT 1 AS 'rank', GamesCategories.categoryid, GamesCategories.categoryname, Gamers.firstname + ' ' + Gamers.lastname AS 'fullname', Gamers.nickname, Gamers.userid, Gamers.picture, SUM(Gamer_Competition.score) as 'score' FROM Gamer_Competition inner join Gamers ON Gamer_Competition.gamerid = Gamers.userid inner join Competitions ON Gamer_Competition.competitionid = Competitions.competitionid inner join Competition_Game ON Competitions.competitionid = Competition_Game.competitionid inner join GamesCategories ON Competition_Game.categoryid = GamesCategories.categoryid where Gamer_Competition.status1 = 1 and GamesCategories.status1 = 1 and Competitions.ispro=1 GROUP BY Gamers.userid, GamesCategories.categoryid , GamesCategories.categoryname , Gamers.firstname , Gamers.lastname ,  Gamers.nickname , Gamers.picture order by categoryname, score desc";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    Competitions rank = new Competitions();

                    rank.Ispro = (dr["rank"] != DBNull.Value) ? Convert.ToInt32(dr["rank"]) : default; //rank
                    rank.Competitionid = (dr["categoryid"] != DBNull.Value) ? Convert.ToInt32(dr["categoryid"]) : default; //categoryid
                    rank.Competitionname = (dr["categoryname"] != DBNull.Value) ? (string)dr["categoryname"] : default; //categoryname
                    rank.Body = (dr["fullname"] != DBNull.Value) ? (string)dr["fullname"] : default; //fullname
                    rank.Address1 = (dr["nickname"] != DBNull.Value) ? (string)dr["nickname"] : default; //nickname
                    rank.Linkforregistration = (dr["picture"] != DBNull.Value) ? (string)dr["picture"] : default; //picture
                    rank.Numofparticipants = (dr["userid"] != DBNull.Value) ? Convert.ToInt32(dr["userid"]) : default; //userid
                    rank.Isiesa = (dr["score"] != DBNull.Value) ? Convert.ToInt32(dr["score"]) : default; //score

                    RanksList.Add(rank);
                }

                return RanksList;
            }
            catch (Exception)
            {
                throw new Exception("בעיה בהתקשורת עם השרת, נא נסה שנית מאוחר יותר");
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        //---Database.html--- *Close*


        //---Orgenzier_Main_Page.html--- *Open*

        public Dictionary<int, List<string>> GamersInC(int CId, int val)
        {

            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = " SELECT * FROM Gamer_Competition inner join Gamers on Gamer_Competition.gamerid=Gamers.userid WHERE competitionid=" + CId + " and  Gamer_Competition.status1 = " + val;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                Dictionary<int, List<string>> dict = new Dictionary<int, List<string>>();
                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                int rank;
                int score;
                while (dr.Read())
                {
                    List<string> details = new List<string>();
                    {
                        details.Add((dr["firstname"] != DBNull.Value) ? (string)dr["firstname"] : default);
                        details.Add((dr["lastname"] != DBNull.Value) ? (string)dr["lastname"] : default);
                        details.Add((dr["nickname"] != DBNull.Value) ? (string)dr["nickname"] : default);
                        rank = ((dr["rank1"] != DBNull.Value) ? Convert.ToInt32(dr["rank1"]) : default);
                        details.Add(rank.ToString());
                        score = ((dr["score"] != DBNull.Value) ? Convert.ToInt32(dr["score"]) : default);
                        details.Add(score.ToString());
                    }
                    dict.Add(((dr["userid"] != DBNull.Value) ? Convert.ToInt32(dr["userid"]) : default), details);
                }

                return dict;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public void Insertranks(List<Gamer_Competition> rankarray) //Add_New_Post.html - method OP2 (Insert: Post (1))
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception)
            {
                // write to log

                throw new Exception("Problem inserting to the server, please try again later");
            }

            String cStr = BuildInsertCommand(rankarray);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command

            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildInsertCommand(List<Gamer_Competition> rankarray) //Add_New_Post.html - method OP2 (Insert: Post (2))
        {
            String command;
            String command2;
            // use a string builder to create the dynamic string
            StringBuilder sa = new StringBuilder();

            for (int i = 0; i < rankarray.Count; i++)
            {
                sa.Append(" INSERT INTO Gamer_Competition (gamerid, competitionid, date1, time1, rank1, score) ");
                sa.Append(" Values(" + rankarray[i].Gamerid + " , " + rankarray[i].Competitionid + " , getdate() , '10:00:00' , " + rankarray[i].Rank1 + " , 0) ");
            }

            command2 = " UPDATE Competitions SET competitionstatus = 5 WHERE competitionid = " + rankarray[0].Competitionid + " ";
            command = sa.ToString() + command2;

            return command;
        }


        //---Orgenzier_Main_Page.html--- *Close*


        //---Post_Archive.html--- *Open*

        public List<Posts> GetPosts() //Post_Archive.html - method MP1
        {
            SqlConnection con = null;
            List<Posts> pList = new List<Posts>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Posts ORDER BY postid desc";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    Posts p = new Posts();

                    p.Postid = Convert.ToInt32(dr["postid"]);
                    p.Title = (string)dr["title"];
                    p.Body1 = (string)dr["body1"];
                    p.Image1 = (string)dr["image1"];
                    p.Category = (string)dr["category"];
                    //p.Postdate = Convert.ToDateTime(dr["postdate"]);

                    renderdate1 = (dr["postdate"] != DBNull.Value) ? (string)dr["postdate"] : default;

                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            p.Postdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            p.Postdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        p.Postdate = (dr["postdate"] != DBNull.Value) ? Convert.ToDateTime(dr["postdate"]) : default;
                    }



                    p.Status1 = Convert.ToInt32(dr["status1"]);

                    pList.Add(p);
                }

                return pList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }


        //---Post_Archive.html.html--- *Close*


        //---Competition_view.html--- *Open*

        public List<Competitions> GetGamersCompetitions(int GID)
        {
            SqlConnection con = null;
            List<Competitions> cList = new List<Competitions>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from Competitions inner join Gamer_Competition ON Competitions.competitionid = Gamer_Competition.competitionid where Gamer_Competition.gamerid=" + GID + "and Competitions.status1 = 1";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Competitions c = new Competitions();
                    c.Competitionid = Convert.ToInt32(dr["competitionid"]);
                    c.Competitionname = (string)dr["competitionname"];
                    c.Isonline = Convert.ToInt32(dr["isonline"]);
                    c.Address1 = (string)dr["address1"];
                    c.Banner = (string)dr["banner"];
                    c.Logo = (string)dr["logo"];
                    c.Prize1 = (string)dr["prize1"];
                    c.Prize2 = (string)dr["prize2"];
                    c.Price3 = (string)dr["prize3"];
                    c.Linkforregistration = (string)dr["linkforregistration"];
                    //c.Lastdateforregistration = Convert.ToDateTime(dr["lastdateforregistration"]);

                    renderdate1 = (dr["lastdateforregistration"] != DBNull.Value) ? (string)dr["lastdateforregistration"] : default;

                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            c.Lastdateforregistration = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            c.Lastdateforregistration = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        c.Lastdateforregistration = (dr["lastdateforregistration"] != DBNull.Value) ? Convert.ToDateTime(dr["lastdateforregistration"]) : default;
                    }


                    c.Body = (string)dr["body"];
                    //c.Startdate = Convert.ToDateTime(dr["startdate"]);

                    renderdate2 = (dr["startdate"] != DBNull.Value) ? (string)dr["startdate"] : default;

                    if (renderdate2 != null)
                    {

                        if (renderdate2.Contains("AM") || renderdate2.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            c.Startdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            c.Startdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        c.Startdate = (dr["startdate"] != DBNull.Value) ? Convert.ToDateTime(dr["startdate"]) : default;
                    }


                    //c.Enddate = Convert.ToDateTime(dr["enddate"]);

                    renderdate3 = (dr["enddate"] != DBNull.Value) ? (string)dr["enddate"] : default;

                    if (renderdate3 != null)
                    {

                        if (renderdate3.Contains("AM") || renderdate3.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate3.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            c.Enddate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate3.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            c.Enddate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        c.Enddate = (dr["enddate"] != DBNull.Value) ? Convert.ToDateTime(dr["enddate"]) : default;
                    }


                    c.Startime = ((TimeSpan)dr["starttime"]);
                    c.Endtime = ((TimeSpan)dr["endtime"]);
                    c.Ispro = Convert.ToInt32(dr["ispro"]);
                    c.Discord = (string)dr["discord"];
                    c.Console = (string)dr["console"];
                    c.Isiesa = Convert.ToInt32(dr["isiesa"]);
                    c.Linkforstream = (string)dr["linkforstream"];
                    c.Numofparticipants = (dr["numofparticipants"] != DBNull.Value) ? Convert.ToInt32(dr["numofparticipants"]) : default;
                    c.Competitionstatus = (string)dr["competitionstatus"];
                    c.Status1 = Convert.ToInt32(dr["status1"]);
                    c.IsPayment = Convert.ToInt32(dr["ispayment"]);
                    c.Startcheckin = ((TimeSpan)dr["startcheckin"]);
                    c.Endcheckin = ((TimeSpan)dr["endcheckin"]);


                    if (c.Status1 == 1)
                        cList.Add(c);
                }

                return cList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        public Competitions ReadOneCompetition(int CId) //Edit_Competitions.html - method SC5
        {
            SqlConnection con = null;
            Competitions c = new Competitions();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT *, GamesCategories.categoryname FROM Competitions inner join Competition_Game ON Competitions.competitionid = Competition_Game.competitionid inner join GamesCategories ON Competition_Game.categoryid = GamesCategories.categoryid where Competitions.competitionid = " + CId + "";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    c.Competitionid = Convert.ToInt32(dr["competitionid"]);
                    c.Competitionname = (string)dr["competitionname"];
                    c.Isonline = Convert.ToInt32(dr["isonline"]);
                    c.Address1 = (string)dr["address1"];
                    c.Banner = (string)dr["banner"];
                    c.Logo = (string)dr["logo"];
                    c.Gamecategory = (string)dr["categoryname"];
                    c.Prize1 = (string)dr["prize1"];
                    c.Prize2 = (string)dr["prize2"];
                    c.Price3 = (string)dr["prize3"];
                    c.Linkforregistration = (string)dr["linkforregistration"];
                    //c.Lastdateforregistration = Convert.ToDateTime(dr["lastdateforregistration"]);

                    renderdate1 = (dr["lastdateforregistration"] != DBNull.Value) ? (string)dr["lastdateforregistration"] : default;

                    if (renderdate1 != null)
                        if (renderdate1 != null)
                        {

                            if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                            {
                                string[] arrdate1 = renderdate1.Split('/');
                                string[] year1 = arrdate1[2].Split(' ');

                                string fix_year1 = year1[0];

                                if (arrdate1[0].Length == 1)
                                {
                                    month1 = "0" + arrdate1[0];
                                }
                                else
                                {
                                    month1 = arrdate1[0];
                                }

                                if (arrdate1[1].Length == 1)
                                {
                                    day1 = "0" + arrdate1[1];
                                }
                                else
                                {
                                    day1 = arrdate1[1];
                                }


                                c.Lastdateforregistration = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                            }
                            else
                            {
                                string[] arrdate1 = renderdate1.Split('/');
                                string[] year1 = arrdate1[2].Split(' ');

                                string fix_year1 = year1[0];
                                string month1 = arrdate1[1];
                                string day1 = arrdate1[0];

                                c.Lastdateforregistration = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                            }
                        }
                        else
                        {
                            c.Lastdateforregistration = (dr["lastdateforregistration"] != DBNull.Value) ? Convert.ToDateTime(dr["lastdateforregistration"]) : default;
                        }


                    c.Body = (string)dr["body"];
                    //c.Startdate = Convert.ToDateTime(dr["startdate"]);

                    renderdate2 = (dr["startdate"] != DBNull.Value) ? (string)dr["startdate"] : default;

                    if (renderdate2 != null)
                    {

                        if (renderdate2.Contains("AM") || renderdate2.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            c.Startdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            c.Startdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        c.Startdate = (dr["startdate"] != DBNull.Value) ? Convert.ToDateTime(dr["startdate"]) : default;
                    }


                    //c.Enddate = Convert.ToDateTime(dr["enddate"]);

                    renderdate3 = (dr["enddate"] != DBNull.Value) ? (string)dr["enddate"] : default;

                    if (renderdate3 != null)
                    {

                        if (renderdate3.Contains("AM") || renderdate3.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate3.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }

                            c.Enddate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate3.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            c.Enddate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        c.Enddate = (dr["enddate"] != DBNull.Value) ? Convert.ToDateTime(dr["enddate"]) : default;
                    }


                    c.Startime = ((TimeSpan)dr["starttime"]);
                    c.Endtime = ((TimeSpan)dr["endtime"]);
                    c.Ispro = Convert.ToInt32(dr["ispro"]);
                    c.Discord = (string)dr["discord"];
                    c.Console = (string)dr["console"];
                    c.Isiesa = Convert.ToInt32(dr["isiesa"]);
                    c.Linkforstream = (string)dr["linkforstream"];
                    c.Numofparticipants = (dr["numofparticipants"] != DBNull.Value) ? Convert.ToInt32(dr["numofparticipants"]) : default;
                    c.Competitionstatus = (string)dr["competitionstatus"];
                    c.Status1 = Convert.ToInt32(dr["status1"]);
                    c.IsPayment = Convert.ToInt32(dr["ispayment"]);
                    c.Startcheckin = ((TimeSpan)dr["startcheckin"]);
                    c.Endcheckin = ((TimeSpan)dr["endcheckin"]);
                    c.Kind = (string)dr["competitiontype"];

                    return c;

                }
                return c;

            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }


        //---Competition_view--- *Close*


        //---Edit_Post.html--- *Open*

        public int UpdatePostSQL(Posts post) //Edit_Post.html - method OP4
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildPutCommandPost(post);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildPutCommandPost(Posts post)
        {
            String command;
            command = "UPDATE Posts SET title= '" + post.Title + "', body1 = '" + post.Body1 + "', body2 = '" + post.Body2 + "', body3 = '" + post.Body3 + "', image1 ='" + post.Image1 + "' , category= '" + post.Category + "', status1= " + post.Status1 + " WHERE postid=" + post.Postid;
            return command;
        }


        //---Edit_Post.html--- *Close*


        //---Edit_Personal_Details.html--- *Open*

        public int updateManagerDetailsSQL(int MId, Managers m1)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildUpdateCommandEditManager(MId, m1);      // helper method to build the update string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildUpdateCommandEditManager(int MId, Managers m1)
        {
            String command;
            command = " UPDATE Managers set firstname = '" + m1.Firstname + "' , lastname = '" + m1.Lastname + "' , gender = '" + m1.Gender + "' , phone = '" + m1.Phone + "', dob = '" + m1.Dob + "' , address1 = '" + m1.Address1 + "' , picture = '" + m1.Picture + "' , role1 = '" + m1.Role1 + "' where userid = " + MId + " ";

            return command;
        }


        public int UpdateOrgenaizerDetails(int OId, Orgenaizers o1)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildUpdateCommandEditOrgenzier(OId, o1);      // helper method to build the update string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildUpdateCommandEditOrgenzier(int OId, Orgenaizers o1)
        {
            String command;
            command = " UPDATE Orgenaizers set firstname = '" + o1.Firstname + "' , lastname = '" + o1.Lastname + "' , gender = '" + o1.Gender + "' , phone = '" + o1.Phone + "', dob = '" + o1.Dob + "' , address1 = '" + o1.Address1 + "' , picture = '" + o1.Picture + "' , communityname = '" + o1.Comunityname + "', linktocommunity = '" + o1.Linktocommunity + "', status1 ='" + o1.Status1 + "' where userid = " + OId + " ";

            return command;
        }

        public int UpdateGamerDetails(int GId, Gamers g1)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildUpdateCommandEditGamer(GId, g1);      // helper method to build the update string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildUpdateCommandEditGamer(int GId, Gamers g1)
        {
            String command;
            command = " UPDATE Gamers set firstname = '" + g1.Firstname + "' , lastname = '" + g1.Lastname + "' , gender = '" + g1.Gender + "' , phone = '" + g1.Phone + "', dob = '" + g1.Dob + "' , address1 = '" + g1.Address1 + "' , picture = '" + g1.Picture + "' , discorduser = '" + g1.Discorduser + "' where userid = " + GId + " ";

            return command;
        }


        //---Edit_Personal_Details.html--- *Close*


        //---Edit_Competition.html--- *Open*

        public GamesCategories Getcategory(int CId) //Edit_Competitions.html - method SC6
        {

            SqlConnection con = null;
            GamesCategories moreinfo = new GamesCategories();


            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT Competitions.competitionstatus, Orgenaizers.firstname + ' ' + Orgenaizers.lastname AS 'orgenaizername', Competition_Game.categoryid FROM Competitions inner join Orgenaizer_Competition ON Competitions.competitionid = Orgenaizer_Competition.competitionid inner join Orgenaizers ON Orgenaizer_Competition.orgenaizerid = Orgenaizers.userid inner join Competition_Game ON Competitions.competitionid = Competition_Game.competitionid where Competitions.competitionid = " + CId + "";

                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read()) //1 row
                {
                    moreinfo.Status1 = Convert.ToInt32(dr["competitionstatus"]);
                    moreinfo.Categoryid = Convert.ToInt32(dr["categoryid"]);
                    moreinfo.Categoryname = (string)dr["orgenaizername"];
                }

                return (moreinfo);
            }



            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        public int updateCompetitonDetails(int CId, Competitions c1)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildUpdateCommandEditGamer(CId, c1);      // helper method to build the update string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildUpdateCommandEditGamer(int CId, Competitions c1)
        {
            String command;
            command = " UPDATE Competitions set competitionname = '" + c1.Competitionname + "' , address1 = '" + c1.Address1 + "' , banner = '" + c1.Banner + "' , logo = '" + c1.Logo + "', prize1 = '" + c1.Prize1 + "' , prize2 = '" + c1.Prize2 + "' , prize3 = '" + c1.Price3 + "' , linkforregistration = '" + c1.Linkforregistration + "' , lastdateforregistration = '" + c1.Lastdateforregistration + "' , body = '" + c1.Body + "' , Startdate = '" + c1.Startdate + "' , enddate= '" + c1.Enddate + "' , startTime= '" + c1.Startime + "' , endTime= '" + c1.Endtime + "' , ispro = '" + c1.Ispro + "' , discord = '" + c1.Discord + "' , console = '" + c1.Console + "' , isiesa = '" + c1.Isiesa + "' , linkforstream = '" + c1.Linkforstream + "' , competitionstatus= '" + '1' + "' , ispayment = '" + c1.IsPayment + "' , isonline= '" + c1.Isonline + "' , startcheckin = '" + c1.Startcheckin + "' , endcheckin = '" + c1.Endcheckin + "' where competitionid = " + CId + " ";

            return command;
        }

        public int updateGameInC(int cID, int gcID)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildUpdateCommandEditGamer(cID, gcID);      // helper method to build the update string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildUpdateCommandEditGamer(int cID, int gcID)
        {
            String command;
            command = "UPDATE Competition_Game set categoryid =" + gcID + "  where competitionid = " + cID + " ";

            return command;
        }


        public int decideNewC(int cID, string val)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildUpdateCommanddecideNewC(cID, val);      // helper method to build the update string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildUpdateCommanddecideNewC(int cID, string val)
        {
            String command;
            command = "UPDATE Competitions set competitionstatus =" + val + "  where competitionid = " + cID + " ";

            return command;
        }

        public void addscore(List<Gamer_Competition> scorearr) //Add_New_Post.html - method OP2 (Insert: Post (1))
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception)
            {
                // write to log

                throw new Exception("Problem inserting to the server, please try again later");
            }

            String cStr = BuildInsertCommandscore(scorearr);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command

            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildInsertCommandscore(List<Gamer_Competition> scorearr) //Add_New_Post.html - method OP2 (Insert: Post (2))
        {
            String command;

            // use a string builder to create the dynamic string
            StringBuilder sa = new StringBuilder();

            for (int i = 0; i < scorearr.Count; i++)
            {
                sa.Append(" UPDATE Gamer_Competition set managerid = 10000001, date1 = getdate(), time1 = getdate(), score = " + scorearr[i].Score + ", status1 = 1 where gamerid = " + scorearr[i].Gamerid + " and competitionid = " + scorearr[i].Competitionid + " update competitions set competitionstatus='7' where competitionid= " + scorearr[i].Competitionid);
            }


            command = sa.ToString();

            return command;
        }



        //---Edit_Competition.html--- *Close*


        //---ReactClientSide---*Open*

        public List<Competitions> GetCompetitonL()
        {
            SqlConnection con = null;
            List<Competitions> cList = new List<Competitions>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = " select * from Competitions;";// where competitionstatus = '2' ";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Competitions c = new Competitions();
                    c.Competitionid = Convert.ToInt32(dr["competitionid"]);
                    c.Competitionname = (string)dr["competitionname"];
                    c.Isonline = Convert.ToInt32(dr["isonline"]);
                    c.Address1 = (string)dr["address1"];
                    c.Banner = (string)dr["banner"];
                    c.Logo = (string)dr["logo"];
                    c.Prize1 = (string)dr["prize1"];
                    c.Prize2 = (string)dr["prize2"];
                    c.Price3 = (string)dr["prize3"];
                    c.Linkforregistration = (string)dr["linkforregistration"];
                    //c.Lastdateforregistration = Convert.ToDateTime(dr["lastdateforregistration"]);

                    renderdate1 = (dr["lastdateforregistration"] != DBNull.Value) ? (string)dr["lastdateforregistration"] : default;

                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            c.Lastdateforregistration = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            c.Lastdateforregistration = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        c.Lastdateforregistration = (dr["lastdateforregistration"] != DBNull.Value) ? Convert.ToDateTime(dr["lastdateforregistration"]) : default;
                    }


                    c.Body = (string)dr["body"];
                    //c.Startdate = Convert.ToDateTime(dr["startdate"]);

                    renderdate2 = (dr["startdate"] != DBNull.Value) ? (string)dr["startdate"] : default;

                    if (renderdate2 != null)
                    {

                        if (renderdate2.Contains("AM") || renderdate2.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            c.Startdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            c.Startdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        c.Startdate = (dr["startdate"] != DBNull.Value) ? Convert.ToDateTime(dr["startdate"]) : default;
                    }

                    //c.Enddate = Convert.ToDateTime(dr["enddate"]);

                    renderdate3 = (dr["enddate"] != DBNull.Value) ? (string)dr["enddate"] : default;

                    if (renderdate3 != null)
                    {

                        if (renderdate3.Contains("AM") || renderdate3.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate3.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            c.Enddate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate3.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            c.Enddate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        c.Enddate = (dr["enddate"] != DBNull.Value) ? Convert.ToDateTime(dr["enddate"]) : default;
                    }

                    c.Startime = ((TimeSpan)dr["starttime"]);
                    c.Endtime = ((TimeSpan)dr["endtime"]);
                    c.Ispro = Convert.ToInt32(dr["ispro"]);
                    c.Discord = (string)dr["discord"];
                    c.Console = (string)dr["console"];
                    c.Isiesa = Convert.ToInt32(dr["isiesa"]);
                    c.Linkforstream = (string)dr["linkforstream"];
                    c.Numofparticipants = (dr["numofparticipants"] != DBNull.Value) ? Convert.ToInt32(dr["numofparticipants"]) : default;
                    c.Competitionstatus = (string)dr["competitionstatus"];
                    c.Status1 = Convert.ToInt32(dr["status1"]);
                    c.IsPayment = Convert.ToInt32(dr["ispayment"]);
                    c.Startcheckin = ((TimeSpan)dr["startcheckin"]);
                    c.Endcheckin = ((TimeSpan)dr["endcheckin"]);

                    cList.Add(c);


                }

                return cList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }


        public List<Comments> ReadCommentsForPosts(int Pid)
        {
            SqlConnection con = null;
            List<Comments> cList = new List<Comments>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from PostsComments_React where idpost=" + Pid;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Comments c = new Comments();
                    c.Postid = Convert.ToInt32(dr["idpost"]);
                    c.Commentid = Convert.ToInt32(dr["idcomment"]);
                    c.Name = (string)dr["commentname"];
                    c.Body = (string)dr["commentbody"];
                    //c.Date = Convert.ToDateTime(dr["date1"]);

                    renderdate1 = (dr["date1"] != DBNull.Value) ? (string)dr["date1"] : default;

                    if (renderdate1 != null)
                    {
                        string[] arrdate1 = renderdate1.Split('/');
                        string[] year1 = arrdate1[2].Split(' ');

                        string fix_year1 = year1[0];
                        string month1 = arrdate1[1];
                        string day1 = arrdate1[0];

                        c.Date = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));
                    }
                    else
                    {
                        c.Date = (dr["date1"] != DBNull.Value) ? Convert.ToDateTime(dr["date1"]) : default;
                    }

                    cList.Add(c);
                }

                return cList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        public int InsertComments(Comments comments)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception)
            {
                // write to log

                throw new Exception("Problem inserting to the server, please try again later");
            }

            String cStr = BuildInsertCommandforComment(comments);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildInsertCommandforComment(Comments comments)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat(" Values( " + comments.Postid + " , '" + comments.Name + "' , '" + comments.Body + "', getdate() )");
            String prefix = " INSERT INTO PostsComments_React ";

            command = prefix + sb.ToString();

            return command;

        }
        //---ReactClientSide---*Close*




        //Edit_Personal_By_Manager.html - method MU1---*Open*

        public List<Orgenaizers> ReadOrgDetails(int IdUser)
        {

            SqlConnection con2 = null;
            List<Orgenaizers> OrgenaizersList = new List<Orgenaizers>();

            try
            {
                con2 = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR2 = "SELECT * FROM Orgenaizers Where userid=" + IdUser;
                SqlCommand cmd2 = new SqlCommand(selectSTR2, con2);

                // get a reader
                SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr2.Read())
                {
                    Orgenaizers orgenaizer = new Orgenaizers();

                    orgenaizer.Userid = (dr2["userid"] != DBNull.Value) ? Convert.ToInt32(dr2["userid"]) : default;
                    orgenaizer.Firstname = (dr2["firstname"] != DBNull.Value) ? (string)dr2["firstname"] : default;
                    orgenaizer.Lastname = (dr2["lastname"] != DBNull.Value) ? (string)dr2["lastname"] : default;
                    orgenaizer.Id = (dr2["id"] != DBNull.Value) ? Convert.ToInt32(dr2["id"]) : default;
                    orgenaizer.Phone = (dr2["phone"] != DBNull.Value) ? (string)dr2["phone"] : default;
                    orgenaizer.Email = (dr2["email"] != DBNull.Value) ? (string)dr2["email"] : default;
                    //orgenaizer.Dob = (dr2["dob"] != DBNull.Value) ? Convert.ToDateTime(dr2["dob"]) : default;

                    renderdate1 = (dr2["dob"] != DBNull.Value) ? (string)dr2["dob"] : default;

                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            orgenaizer.Dob = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            orgenaizer.Dob = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        orgenaizer.Dob = (dr2["dob"] != DBNull.Value) ? Convert.ToDateTime(dr2["dob"]) : default;
                    }


                    orgenaizer.Comunityname = (dr2["communityname"] != DBNull.Value) ? (string)dr2["communityname"] : default;

                    orgenaizer.Gender = (dr2["gender"] != DBNull.Value) ? (string)dr2["gender"] : default;
                    orgenaizer.Nickname = (dr2["nickname"] != DBNull.Value) ? (string)dr2["nickname"] : default;
                    orgenaizer.Picture = (dr2["picture"] != DBNull.Value) ? (string)dr2["picture"] : default;
                    orgenaizer.Address1 = (dr2["address1"] != DBNull.Value) ? (string)dr2["address1"] : default;
                    orgenaizer.Linktocommunity = (dr2["linktocommunity"] != DBNull.Value) ? (string)dr2["linktocommunity"] : default;
                    orgenaizer.Status1 = (dr2["status1"] != DBNull.Value) ? Convert.ToInt32(dr2["status1"]) : default;
                    OrgenaizersList.Add(orgenaizer);
                }

                return OrgenaizersList;
            }
            catch (Exception)
            {
                throw new Exception("בעיה בהתקשורת עם השרת, נא נסה שנית מאוחר יותר");
            }
            finally
            {
                if (con2 != null)
                {
                    con2.Close();
                }

            }

        }



        //Edit_Personal_By_Manager-MU1




        //Edit_Personal_By_Manager.html - method MU2---*Open*


        public List<Gamers> ReadGamerDetails(int IdUser)
        {

            SqlConnection con = null;
            List<Gamers> GamersList = new List<Gamers>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Gamers WHERE userid =" + IdUser;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    Gamers gamer = new Gamers();

                    gamer.Userid = (dr["userid"] != DBNull.Value) ? Convert.ToInt32(dr["userid"]) : default;
                    gamer.Id = (dr["id"] != DBNull.Value) ? Convert.ToInt32(dr["id"]) : default;
                    gamer.Firstname = (dr["firstname"] != DBNull.Value) ? (string)dr["firstname"] : default;
                    gamer.Discorduser = (dr["discorduser"] != DBNull.Value) ? (string)dr["discorduser"] : default;
                    gamer.Gender = (dr["gender"] != DBNull.Value) ? (string)dr["gender"] : default;
                    gamer.Lastname = (dr["lastname"] != DBNull.Value) ? (string)dr["lastname"] : default;
                    gamer.Nickname = (dr["nickname"] != DBNull.Value) ? (string)dr["nickname"] : default;
                    gamer.Phone = (dr["phone"] != DBNull.Value) ? (string)dr["phone"] : default;
                    gamer.Address1 = (dr["address1"] != DBNull.Value) ? (string)dr["address1"] : default;
                    gamer.Picture = (dr["picture"] != DBNull.Value) ? (string)dr["picture"] : default;
                    gamer.Email = (dr["email"] != DBNull.Value) ? (string)dr["email"] : default;
                    //gamer.Dob = (dr["dob"] != DBNull.Value) ? Convert.ToDateTime(dr["dob"]) : default;

                    renderdate1 = (dr["dob"] != DBNull.Value) ? (string)dr["dob"] : default;

                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            gamer.Dob = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            gamer.Dob = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        gamer.Dob = (dr["dob"] != DBNull.Value) ? Convert.ToDateTime(dr["dob"]) : default;
                    }


                    //gamer.Registrationdate = (dr["registrationdate"] != DBNull.Value) ? Convert.ToDateTime(dr["registrationdate"]) : default;

                    renderdate2 = (dr["registrationdate"] != DBNull.Value) ? (string)dr["registrationdate"] : default;

                    if (renderdate2 != null)
                    {

                        if (renderdate2.Contains("AM") || renderdate2.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            gamer.Registrationdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            gamer.Registrationdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        gamer.Registrationdate = (dr["registrationdate"] != DBNull.Value) ? Convert.ToDateTime(dr["registrationdate"]) : default;
                    }


                    gamer.Status1 = (dr["status1"] != DBNull.Value) ? Convert.ToInt32(dr["status1"]) : default;
                    GamersList.Add(gamer);
                }

                return GamersList;
            }
            catch (Exception)
            {

                throw new Exception("Problem getting the information from the server, please try again later");
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }


        //Edit_Personal_By_Manager.html - method MU2---*Close*




        //Gamer_Main_Page.html --*Open*




        public List<Competitions> GetRelevantCompetitions(int GID) //Gamer_Main_Page.html - SG08
        {
            SqlConnection con = null;
            List<Competitions> cList = new List<Competitions>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = " SELECT GamesCategories.categoryid, GamesCategories.categoryname FROM competitions inner join Competition_Game on competitions.competitionid = Competition_Game .competitionid inner join Gamer_Competition on Competitions.competitionid=Gamer_Competition.competitionid inner join GamesCategories on Competition_Game.categoryid=GamesCategories.categoryid WHERE gamerid = " + GID;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Competitions c = new Competitions();
                    c.Competitionid = Convert.ToInt32(dr["categoryid"]);
                    c.Competitionname = (string)dr["categoryname"];

                    cList.Add(c);
                }

                return cList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        public List<Competitions> GetCompetitionsSQL(int GID) //Gamer_Main_Page.html - method SG09
        {

            SqlConnection con = null;
            List<Competitions> RanksList = new List<Competitions>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = " SELECT Competitions.competitionid 'Competition ID', competitions.competitionname 'Competition Name', competitions.startdate 'Start Date', competitions.console 'Console', Gamer_Competition.rank1 'Rank', Gamer_Competition.score 'Score', gamesCategories.categoryname 'Category Name' from competitions inner join gamer_competition on competitions.competitionid=gamer_competition.competitionid inner join gamers on Gamer_Competition.gamerid=gamers.userid inner join competition_game on competitions.competitionid = competition_game.competitionid inner join GamesCategories on gamesCategories.categoryid = Competition_Game.categoryid where competitions.status1=1 and gamer_competition.status1=1 and gamerid=" + GID;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    Competitions rank = new Competitions();

                    rank.Competitionid = (dr["Competition ID"] != DBNull.Value) ? Convert.ToInt32(dr["Competition ID"]) : default; //categoryid
                    rank.Competitionname = (dr["Competition Name"] != DBNull.Value) ? (string)dr["Competition Name"] : default; //categoryname
                    //rank.Startdate = Convert.ToDateTime(dr["Start Date"]); //start date

                    renderdate1 = (dr["Start Date"] != DBNull.Value) ? (string)dr["Start Date"] : default;

                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            rank.Startdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            rank.Startdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        rank.Startdate = (dr["Start Date"] != DBNull.Value) ? Convert.ToDateTime(dr["Start Date"]) : default;
                    }


                    rank.Console = (dr["Console"] != DBNull.Value) ? (string)dr["Console"] : default; //console
                    rank.Isonline = (dr["Rank"] != DBNull.Value) ? Convert.ToInt32(dr["Rank"]) : default; //Rank
                    rank.Numofparticipants = (dr["Score"] != DBNull.Value) ? Convert.ToInt32(dr["Score"]) : default; //score
                    rank.Body = (dr["Category Name"] != DBNull.Value) ? (string)dr["Category Name"] : default; //category name

                    RanksList.Add(rank);
                }

                return RanksList;
            }
            catch (Exception ex)
            {
                throw (ex);
                //throw new Exception("בעיה בהתקשורת עם השרת, נא נסה שנית מאוחר יותר");
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public List<Competitions> GetCompetitionsByStatus(string Cstatus) //Gamer_Main_Page.html - method SG10
        {

            SqlConnection con = null;
            List<Competitions> RanksList = new List<Competitions>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Competitions inner join Competition_Game on Competitions.competitionid = Competition_Game.competitionid WHERE status1='1' and competitionstatus = '2' or competitionstatus ='4'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    Competitions c = new Competitions();

                    c.Competitionid = (dr["competitionid"] != DBNull.Value) ? Convert.ToInt32(dr["competitionid"]) : default;
                    c.Competitionname = (dr["competitionname"] != DBNull.Value) ? (string)dr["competitionname"] : default;
                    c.Isonline = (dr["isonline"] != DBNull.Value) ? Convert.ToInt32(dr["isonline"]) : default;
                    c.Address1 = (dr["address1"] != DBNull.Value) ? (string)dr["address1"] : default;
                    c.Banner = (dr["banner"] != DBNull.Value) ? (string)dr["banner"] : default;
                    c.Logo = (dr["logo"] != DBNull.Value) ? (string)dr["logo"] : default;
                    c.Prize1 = (dr["prize1"] != DBNull.Value) ? (string)dr["prize1"] : default;
                    c.Prize2 = (dr["prize2"] != DBNull.Value) ? (string)dr["prize2"] : default;
                    c.Price3 = (dr["prize3"] != DBNull.Value) ? (string)dr["prize3"] : default;
                    c.Linkforregistration = (dr["linkforregistration"] != DBNull.Value) ? (string)dr["linkforregistration"] : default;
                    //c.Lastdateforregistration = Convert.ToDateTime(dr["lastdateforregistration"]);

                    renderdate1 = (dr["lastdateforregistration"] != DBNull.Value) ? (string)dr["lastdateforregistration"] : default;

                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            c.Lastdateforregistration = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            c.Lastdateforregistration = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        c.Lastdateforregistration = (dr["lastdateforregistration"] != DBNull.Value) ? Convert.ToDateTime(dr["lastdateforregistration"]) : default;
                    }


                    c.Body = (dr["body"] != DBNull.Value) ? (string)dr["body"] : default;
                    //c.Startdate = Convert.ToDateTime(dr["startdate"]);

                    renderdate2 = (dr["startdate"] != DBNull.Value) ? (string)dr["startdate"] : default;

                    if (renderdate2 != null)
                    {

                        if (renderdate2.Contains("AM") || renderdate2.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            c.Startdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            c.Startdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        c.Startdate = (dr["startdate"] != DBNull.Value) ? Convert.ToDateTime(dr["startdate"]) : default;
                    }

                    //c.Enddate = Convert.ToDateTime(dr["enddate"]);

                    renderdate3 = (dr["enddate"] != DBNull.Value) ? (string)dr["enddate"] : default;

                    if (renderdate3 != null)
                    {

                        if (renderdate3.Contains("AM") || renderdate3.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate3.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            c.Enddate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate3.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            c.Enddate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        c.Enddate = (dr["enddate"] != DBNull.Value) ? Convert.ToDateTime(dr["enddate"]) : default;
                    }

                    c.Startime = (dr["starttime"] != DBNull.Value) ? ((TimeSpan)dr["starttime"]) : default;
                    c.Endtime = (dr["endtime"] != DBNull.Value) ? ((TimeSpan)dr["endtime"]) : default;
                    c.Ispro = (dr["ispro"] != DBNull.Value) ? Convert.ToInt32(dr["ispro"]) : default;
                    c.Discord = (dr["discord"] != DBNull.Value) ? (string)dr["discord"] : default;
                    c.Console = (dr["console"] != DBNull.Value) ? (string)dr["console"] : default;
                    c.Isiesa = (dr["isiesa"] != DBNull.Value) ? Convert.ToInt32(dr["isiesa"]) : default;
                    c.Linkforstream = (dr["linkforstream"] != DBNull.Value) ? (string)dr["linkforstream"] : default;
                    c.Numofparticipants = (dr["numofparticipants"] != DBNull.Value) ? Convert.ToInt32(dr["numofparticipants"]) : default;
                    c.Competitionstatus = (dr["competitionstatus"] != DBNull.Value) ? (string)dr["competitionstatus"] : default;
                    c.Status1 = (dr["status1"] != DBNull.Value) ? Convert.ToInt32(dr["status1"]) : default;
                    c.IsPayment = (dr["categoryid"] != DBNull.Value) ? Convert.ToInt32(dr["categoryid"]) : default;
                    c.Startcheckin = (dr["startcheckin"] != DBNull.Value) ? ((TimeSpan)dr["startcheckin"]) : default;
                    c.Endcheckin = (dr["endcheckin"] != DBNull.Value) ? ((TimeSpan)dr["endcheckin"]) : default;

                    RanksList.Add(c);
                }

                return RanksList;
            }
            catch (Exception ex)
            {
                throw (ex);
                //throw new Exception("בעיה בהתקשורת עם השרת, נא נסה שנית מאוחר יותר");
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }



        //Gamer_Main_Page.html --*Close*


        //---Profile_View_Manager.html--- *Open*

        public List<Competitions> G_Read_View_CompetitionsSQL(int idtoserver) //Profile_View_Manager.html - method OC1
        {

            SqlConnection con2 = null;
            List<Competitions> CompetitionsList = new List<Competitions>();

            try
            {
                con2 = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR2 = "SELECT Competitions.competitionid, GamesCategories.categoryname, Competitions.isiesa, Competitions.ispro, Gamer_Competition.score FROM Gamer_Competition inner join Gamers ON Gamer_Competition.gamerid = Gamers.userid inner join Competitions ON Gamer_Competition.competitionid = Competitions.competitionid inner join Competition_Game ON Competitions.competitionid = Competition_Game.competitionid inner join GamesCategories ON Competition_Game.categoryid = GamesCategories.categoryid where Gamer_Competition.status1 = 1 and GamesCategories.status1 = 1 and Gamer_Competition.gamerid = " + idtoserver;

                SqlCommand cmd2 = new SqlCommand(selectSTR2, con2);

                // get a reader
                SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr2.Read())
                {
                    Competitions competition = new Competitions();

                    competition.Competitionid = (dr2["competitionid"] != DBNull.Value) ? Convert.ToInt32(dr2["competitionid"]) : default;
                    competition.Gamecategory = (dr2["categoryname"] != DBNull.Value) ? (string)dr2["categoryname"] : default;
                    competition.Isiesa = (dr2["isiesa"] != DBNull.Value) ? Convert.ToInt32(dr2["isiesa"]) : default;
                    competition.Ispro = (dr2["ispro"] != DBNull.Value) ? Convert.ToInt32(dr2["ispro"]) : default;
                    competition.Isonline = (dr2["score"] != DBNull.Value) ? Convert.ToInt32(dr2["score"]) : default;

                    CompetitionsList.Add(competition);
                }

                return CompetitionsList;
            }
            catch (Exception)
            {
                throw new Exception("בעיה בהתקשורת עם השרת, נא נסה שנית מאוחר יותר");
            }
            finally
            {
                if (con2 != null)
                {
                    con2.Close();
                }

            }

        }

        public List<Competitions> O_Read_View_CompetitionsSQL(int idtoserver) //Profile_View_Manager.html - method OC2
        {

            SqlConnection con3 = null;
            List<Competitions> CompetitionsList = new List<Competitions>();

            try
            {
                con3 = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR3 = "SELECT Competitions.competitionid, GamesCategories.categoryname, Competitions.competitionstatus, Competitions.ispro FROM Competitions inner join Orgenaizer_Competition ON Competitions.competitionid = Orgenaizer_Competition.competitionid inner join Competition_Game ON Orgenaizer_Competition.competitionid = Competition_Game.competitionid inner join GamesCategories ON Competition_Game.categoryid = GamesCategories.categoryid WHERE Orgenaizer_Competition.orgenaizerid = " + idtoserver;

                SqlCommand cmd3 = new SqlCommand(selectSTR3, con3);

                // get a reader
                SqlDataReader dr3 = cmd3.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr3.Read())
                {
                    Competitions competition = new Competitions();

                    competition.Competitionid = (dr3["competitionid"] != DBNull.Value) ? Convert.ToInt32(dr3["competitionid"]) : default;
                    competition.Gamecategory = (dr3["categoryname"] != DBNull.Value) ? (string)dr3["categoryname"] : default;
                    competition.Competitionstatus = (dr3["competitionstatus"] != DBNull.Value) ? (string)dr3["competitionstatus"] : default;
                    competition.Ispro = (dr3["ispro"] != DBNull.Value) ? Convert.ToInt32(dr3["ispro"]) : default;

                    CompetitionsList.Add(competition);
                }

                return CompetitionsList;
            }
            catch (Exception)
            {
                throw new Exception("בעיה בהתקשורת עם השרת, נא נסה שנית מאוחר יותר");
            }
            finally
            {
                if (con3 != null)
                {
                    con3.Close();
                }

            }

        }


        //---Profile_View_Manager.html--- *Close*


        //---Competitions.html--- *Open*

        public List<Competitions> GetSiteCompetitionsSQL(string sqlcommand) //Competitions.html - method OCS1
        {

            SqlConnection con = null;
            List<Competitions> CompetitionsList = new List<Competitions>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = sqlcommand;

                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    Competitions competition = new Competitions();

                    competition.Competitionid = (dr["competitionid"] != DBNull.Value) ? Convert.ToInt32(dr["competitionid"]) : default;
                    competition.Competitionname = (dr["competitionname"] != DBNull.Value) ? (string)dr["competitionname"] : default;
                    competition.Body = (dr["body"] != DBNull.Value) ? (string)dr["body"] : default;
                    competition.Competitionstatus = (dr["competitionstatus"] != DBNull.Value) ? (string)dr["competitionstatus"] : default;
                    competition.Ispro = (dr["ispro"] != DBNull.Value) ? Convert.ToInt32(dr["ispro"]) : default;
                    competition.Console = (dr["console"] != DBNull.Value) ? (string)dr["console"] : default;
                    //competition.Startdate = (dr["startdate"] != DBNull.Value) ? Convert.ToDateTime(dr["startdate"]) : default;

                    renderdate1 = (dr["startdate"] != DBNull.Value) ? (string)dr["startdate"] : default;

                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            competition.Startdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            competition.Startdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        competition.Startdate = (dr["startdate"] != DBNull.Value) ? Convert.ToDateTime(dr["startdate"]) : default;
                    }



                    //competition.Enddate = (dr["enddate"] != DBNull.Value) ? Convert.ToDateTime(dr["enddate"]) : default;

                    renderdate2 = (dr["enddate"] != DBNull.Value) ? (string)dr["enddate"] : default;

                    if (renderdate2 != null)
                    {

                        if (renderdate2.Contains("AM") || renderdate2.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            competition.Enddate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            competition.Enddate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        competition.Enddate = (dr["enddate"] != DBNull.Value) ? Convert.ToDateTime(dr["enddate"]) : default;
                    }


                    competition.Logo = (dr["logo"] != DBNull.Value) ? (string)dr["logo"] : default;
                    competition.Banner = (dr["banner"] != DBNull.Value) ? (string)dr["banner"] : default;
                    competition.Linkforregistration = (dr["categoryname"] != DBNull.Value) ? (string)dr["categoryname"] : default; //categoryname - inner join

                    CompetitionsList.Add(competition);
                }

                return CompetitionsList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }


        //---Competitions.html--- *Close*




        //---PasswordRestore.html--- *Open*


        public int updatePassword(string userEmail, string newPassword) //PasswordRestore.html - method SD1
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildUpdatenewPassword(userEmail, newPassword);      // helper method to build the update string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildUpdatenewPassword(string userEmail, string newPassword)
        {
            String command;
            command = " update Orgenaizers set password1= '" + newPassword + "' where email = '" + userEmail + "' update Managers set password1= '" + newPassword + "' where email = '" + userEmail + "' update Gamers set password1= '" + newPassword + "' where email = '" + userEmail + "' ";

            return command;
        }



        //---PasswordRestore.html--- *Close*


        //---index.html--- *Open*

        public List<Posts> GetPostsIndexSQL() //index.html - method OMI1
        {

            SqlConnection con1 = null;
            List<Posts> PostsList = new List<Posts>();

            try
            {
                con1 = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file


                String selectSTR1 = "SELECT TOP 5 WITH TIES * FROM Posts where status1='1' ORDER BY postid desc";

                SqlCommand cmd1 = new SqlCommand(selectSTR1, con1);

                // get a reader
                SqlDataReader dr1 = cmd1.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr1.Read())
                {
                    Posts post = new Posts();

                    post.Postid = (dr1["postid"] != DBNull.Value) ? Convert.ToInt32(dr1["postid"]) : default;
                    post.Title = (dr1["title"] != DBNull.Value) ? (string)dr1["title"] : default;
                    post.Category = (dr1["category"] != DBNull.Value) ? (string)dr1["category"] : default;
                    post.Body1 = (dr1["body1"] != DBNull.Value) ? (string)dr1["body1"] : default;
                    post.Image1 = (dr1["image1"] != DBNull.Value) ? (string)dr1["image1"] : default;
                    //post.Postdate = (dr1["postdate"] != DBNull.Value) ? Convert.ToDateTime(dr1["postdate"]) : default;

                    renderdate2 = (dr1["postdate"] != DBNull.Value) ? (string)dr1["postdate"] : default;

                    if (renderdate2 != null)
                    {

                        if (renderdate2.Contains("AM") || renderdate2.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            post.Postdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate2.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            post.Postdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        post.Postdate = (dr1["postdate"] != DBNull.Value) ? Convert.ToDateTime(dr1["postdate"]) : default;
                    }

                    PostsList.Add(post);
                }

                return PostsList;
            }
            catch (Exception)
            {
                throw new Exception("נתונים אינם זמינים מהשרת, אנא נסה שנית מאוחר יותר");
            }
            finally
            {
                if (con1 != null)
                {
                    con1.Close();
                }

            }

        }


        public List<Competitions> ReadCompetitionsindexSQL() //index.html - method OMI2
        {

            SqlConnection con2 = null;
            List<Competitions> CompetitionsList = new List<Competitions>();

            try
            {
                con2 = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR2 = "SELECT TOP 2 WITH TIES Competitions.competitionid, Competitions.competitionname, GamesCategories.categoryname, Competitions.body, Competitions.banner, Competitions.logo, Competitions.startdate, Competitions.enddate FROM Competitions inner join Competition_Game ON Competitions.competitionid = Competition_Game.competitionid inner join GamesCategories ON Competition_Game.categoryid = GamesCategories.categoryid where Competitions.status1='1' and Competitions.competitionstatus !='1' and Competitions.competitionstatus !='3' ORDER BY Competitions.competitionid desc";

                SqlCommand cmd2 = new SqlCommand(selectSTR2, con2);

                // get a reader
                SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr2.Read())
                {
                    Competitions competition = new Competitions();

                    competition.Competitionid = (dr2["competitionid"] != DBNull.Value) ? Convert.ToInt32(dr2["competitionid"]) : default;
                    competition.Competitionname = (dr2["competitionname"] != DBNull.Value) ? (string)dr2["competitionname"] : default;
                    competition.Gamecategory = (dr2["categoryname"] != DBNull.Value) ? (string)dr2["categoryname"] : default;
                    competition.Body = (dr2["body"] != DBNull.Value) ? (string)dr2["body"] : default;
                    competition.Banner = (dr2["banner"] != DBNull.Value) ? (string)dr2["banner"] : default;
                    competition.Logo = (dr2["logo"] != DBNull.Value) ? (string)dr2["logo"] : default;
                    //competition.Startdate = (dr2["startdate"] != DBNull.Value) ? Convert.ToDateTime(dr2["startdate"]) : default;

                    renderdate3 = (dr2["startdate"] != DBNull.Value) ? (string)dr2["startdate"] : default;

                    if (renderdate3 != null)
                    {

                        if (renderdate3.Contains("AM") || renderdate3.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate3.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            competition.Startdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate3.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            competition.Startdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        competition.Startdate = (dr2["startdate"] != DBNull.Value) ? Convert.ToDateTime(dr2["startdate"]) : default;
                    }


                    //competition.Enddate = (dr2["enddate"] != DBNull.Value) ? Convert.ToDateTime(dr2["enddate"]) : default;

                    renderdate4 = (dr2["enddate"] != DBNull.Value) ? (string)dr2["enddate"] : default;

                    if (renderdate4 != null)
                    {

                        if (renderdate4.Contains("AM") || renderdate4.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate4.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }


                            competition.Enddate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate4.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            competition.Enddate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        competition.Enddate = (dr2["enddate"] != DBNull.Value) ? Convert.ToDateTime(dr2["enddate"]) : default;
                    }



                    CompetitionsList.Add(competition);
                }

                return CompetitionsList;
            }
            catch (Exception)
            {
                throw new Exception("בעיה בהתקשורת עם השרת, נא נסה שנית מאוחר יותר");
            }
            finally
            {
                if (con2 != null)
                {
                    con2.Close();
                }

            }

        }


        public Posts GetPostIndexSQL() //index.html - method OMI3
        {

            SqlConnection con3 = null;
            Posts p = new Posts();

            try
            {

                con3 = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file


                String selectSTR3 = "SELECT * FROM Posts where postid='1'";
                SqlCommand cmd3 = new SqlCommand(selectSTR3, con3);

                // get a reader
                SqlDataReader dr3 = cmd3.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                //throw new Exception("Omer1");

                while (dr3.Read()) //1 row
                {

                    p.Postid = (dr3["postid"] != DBNull.Value) ? Convert.ToInt32(dr3["postid"]) : default;
                    p.Title = (dr3["title"] != DBNull.Value) ? (string)dr3["title"] : default;
                    p.Body1 = (dr3["body1"] != DBNull.Value) ? (string)dr3["body1"] : default;
                    p.Image1 = (dr3["image1"] != DBNull.Value) ? (string)dr3["image1"] : default;
                    p.Category = (dr3["category"] != DBNull.Value) ? (string)dr3["category"] : default;
                    //p.Postdate = (dr3["postdate"] != DBNull.Value) ? Convert.ToDateTime(dr3["postdate"]) : default;

                    renderdate1 = (dr3["postdate"] != DBNull.Value) ? (string)dr3["postdate"] : default;

                    if (renderdate1 != null)
                    {

                        if (renderdate1.Contains("AM") || renderdate1.Contains("PM"))
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];

                            if (arrdate1[0].Length == 1)
                            {
                                month1 = "0" + arrdate1[0];
                            }
                            else
                            {
                                month1 = arrdate1[0];
                            }

                            if (arrdate1[1].Length == 1)
                            {
                                day1 = "0" + arrdate1[1];
                            }
                            else
                            {
                                day1 = arrdate1[1];
                            }
                            

                            p.Postdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                        else
                        {
                            string[] arrdate1 = renderdate1.Split('/');
                            string[] year1 = arrdate1[2].Split(' ');

                            string fix_year1 = year1[0];
                            string month1 = arrdate1[1];
                            string day1 = arrdate1[0];

                            p.Postdate = new DateTime(Int32.Parse(fix_year1), Int32.Parse(month1), Int32.Parse(day1));

                        }
                    }
                    else
                    {
                        p.Postdate = (dr3["postdate"] != DBNull.Value) ? Convert.ToDateTime(dr3["postdate"]) : default;
                    }


                    p.Status1 = (dr3["status1"] != DBNull.Value) ? Convert.ToInt32(dr3["status1"]) : default;

                }

                return p;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con3 != null)
                {
                    con3.Close();
                }

            }

        }

        //---index.html--- *Close*





    }

}