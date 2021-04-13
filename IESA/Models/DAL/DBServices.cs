using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
                    g.Dob = (dr["dob"] != DBNull.Value) ? Convert.ToDateTime(dr["dob"]) : default;
                    g.Address1 = (dr["address1"] != DBNull.Value) ? (string)dr["address1"] : default;
                    g.Discorduser = (dr["discorduser"] != DBNull.Value) ? (string)dr["discorduser"] : default;
                    g.Picture = (dr["picture"] != DBNull.Value) ? (string)dr["picture"] : default;
                    g.Registrationdate = (dr["registrationdate"] != DBNull.Value) ? Convert.ToDateTime(dr["registrationdate"]) : default;
                    g.Outofdate = (dr["outofdate"] != DBNull.Value) ? Convert.ToDateTime(dr["outofdate"]) : default;
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
                    o.Dob = (dr["dob"] != DBNull.Value) ? Convert.ToDateTime(dr["dob"]) : default;
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
                    m.Dob = (dr["dob"] != DBNull.Value) ? Convert.ToDateTime(dr["dob"]) : default;
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
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}','{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}','{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}','{22}', '{23}')", Competition.Competitionname, Competition.Address1, Competition.Banner, Competition.Logo, Competition.Prize1, Competition.Prize2, Competition.Price3, Competition.Linkforregistration, Competition.Lastdateforregistration, Competition.Body, Competition.Startdate, Competition.Enddate, Competition.Startime, Competition.Endtime, Competition.Ispro, Competition.Discord, Competition.Console, Competition.Isiesa, Competition.Linkforstream, '1', Competition.IsPayment, Competition.Isonline, Competition.Startcheckin, Competition.Endcheckin);
            String prefix = "INSERT INTO Competitions " + "(competitionname, address1, banner, logo, prize1, prize2, prize3, linkforregistration, lastdateforregistration, body , startdate, enddate, startTime, endTime, ispro, discord, console, isiesa, linkforstream, competitionstatus, ispayment, isonline, startcheckin, endcheckin) ";

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
                    c.Lastdateforregistration = Convert.ToDateTime(dr["lastdateforregistration"]);
                    c.Body = (string)dr["body"];
                    c.Startdate = Convert.ToDateTime(dr["startdate"]);
                    c.Enddate = Convert.ToDateTime(dr["enddate"]);
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
                    g.Dob = (dr["dob"] != DBNull.Value) ? Convert.ToDateTime(dr["dob"]) : default;
                    g.Address1 = (dr["address1"] != DBNull.Value) ? (string)dr["address1"] : default;
                    g.Discorduser = (dr["discorduser"] != DBNull.Value) ? (string)dr["discorduser"] : default;
                    g.Picture = (dr["picture"] != DBNull.Value) ? (string)dr["picture"] : default;
                    g.Registrationdate = (dr["registrationdate"] != DBNull.Value) ? Convert.ToDateTime(dr["registrationdate"]) : default;
                    g.Outofdate = (dr["outofdate"] != DBNull.Value) ? Convert.ToDateTime(dr["outofdate"]) : default;
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
                    p.Postdate = (dr["postdate"] != DBNull.Value) ? Convert.ToDateTime(dr["postdate"]) : default;
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
                    post.Postdate = (dr["postdate"] != DBNull.Value) ? Convert.ToDateTime(dr["postdate"]) : default;
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
                    gamer.Dob = (dr["dob"] != DBNull.Value) ? Convert.ToDateTime(dr["dob"]) : default;
                    gamer.Registrationdate = (dr["registrationdate"] != DBNull.Value) ? Convert.ToDateTime(dr["registrationdate"]) : default;

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
                    orgenaizer.Dob = (dr2["dob"] != DBNull.Value) ? Convert.ToDateTime(dr2["dob"]) : default;
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
                String selectSTR3 = "UPDATE Competitions set Competitionstatus = '4' where Competitionstatus='2' and CONVERT(date,enddate,103) > getdate() SELECT TOP 5 WITH TIES * FROM Posts ORDER BY postid desc";

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
                    post.Postdate = (dr3["postdate"] != DBNull.Value) ? Convert.ToDateTime(dr3["postdate"]) : default;

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

                String selectSTR4 = "SELECT Competitions.competitionid, Competitions.competitionstatus, Competitions.competitionname, Competitions.ispro, Orgenaizers.firstname + ' ' + Orgenaizers.lastname AS 'orgenaizername', Competitions.startdate, Competitions.enddate FROM Competitions inner join Orgenaizer_Competition ON Competitions.competitionid = Orgenaizer_Competition.competitionid and Competitions.competitionstatus = '1' or Competitions.competitionstatus = '5' inner join Orgenaizers ON Orgenaizer_Competition.orgenaizerid = Orgenaizers.userid";

                SqlCommand cmd4 = new SqlCommand(selectSTR4, con4);

                // get a reader
                SqlDataReader dr4 = cmd4.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr4.Read())
                {
                    Competitions competition = new Competitions();

                    competition.Competitionid = (dr4["competitionid"] != DBNull.Value) ? Convert.ToInt32(dr4["competitionid"]) : default;
                    competition.Competitionstatus = (dr4["competitionstatus"] != DBNull.Value) ? (string)dr4["competitionstatus"] : default;
                    competition.Competitionname = (dr4["competitionname"] != DBNull.Value) ? (string)dr4["competitionname"] : default;
                    competition.Ispro = (dr4["ispro"] != DBNull.Value) ? Convert.ToInt32(dr4["ispro"]) : default;
                    competition.Console = (dr4["orgenaizername"] != DBNull.Value) ? (string)dr4["orgenaizername"] : default; //'False' Console -> gets the orgenaizer name instead*
                    competition.Startdate = (dr4["startdate"] != DBNull.Value) ? Convert.ToDateTime(dr4["startdate"]) : default;
                    competition.Enddate = (dr4["enddate"] != DBNull.Value) ? Convert.ToDateTime(dr4["enddate"]) : default;

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


        //---Manager_Main_Page.html--- *Close*


        //---Database.html--- *Open*

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
                    post.Postdate = (dr1["postdate"] != DBNull.Value) ? Convert.ToDateTime(dr1["postdate"]) : default;

                    PostsList.Add(post);
                }

                return PostsList;
            }
            catch (Exception)
            {

                throw new Exception("בעיה בהתקשרות עם השרת, נא נסה שנית מאוחר יותר");
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
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildDeleteCommandComp(todeleteid);      // helper method to build the insert string

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

                String selectSTR4 = "SELECT Competitions.competitionid, Competitions.competitionstatus, Competitions.competitionname, Competitions.ispro, Orgenaizers.firstname + ' ' + Orgenaizers.lastname AS 'orgenaizername', Competitions.startdate, Competitions.enddate FROM Competitions inner join Orgenaizer_Competition ON Competitions.competitionid = Orgenaizer_Competition.competitionid inner join Orgenaizers ON Orgenaizer_Competition.orgenaizerid = Orgenaizers.userid";

                SqlCommand cmd4 = new SqlCommand(selectSTR4, con4);

                // get a reader
                SqlDataReader dr4 = cmd4.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr4.Read())
                {
                    Competitions competition = new Competitions();

                    competition.Competitionid = (dr4["competitionid"] != DBNull.Value) ? Convert.ToInt32(dr4["competitionid"]) : default;
                    competition.Competitionstatus = (dr4["competitionstatus"] != DBNull.Value) ? (string)dr4["competitionstatus"] : default;
                    competition.Competitionname = (dr4["competitionname"] != DBNull.Value) ? (string)dr4["competitionname"] : default;
                    competition.Ispro = (dr4["ispro"] != DBNull.Value) ? Convert.ToInt32(dr4["ispro"]) : default;
                    competition.Console = (dr4["orgenaizername"] != DBNull.Value) ? (string)dr4["orgenaizername"] : default; //'False' Console -> gets the orgenaizer name instead*
                    competition.Startdate = (dr4["startdate"] != DBNull.Value) ? Convert.ToDateTime(dr4["startdate"]) : default;
                    competition.Enddate = (dr4["enddate"] != DBNull.Value) ? Convert.ToDateTime(dr4["enddate"]) : default;

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
                    gamer.Dob = (dr["dob"] != DBNull.Value) ? Convert.ToDateTime(dr["dob"]) : default;
                    gamer.Registrationdate = (dr["registrationdate"] != DBNull.Value) ? Convert.ToDateTime(dr["registrationdate"]) : default;

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
                    orgenaizer.Dob = (dr2["dob"] != DBNull.Value) ? Convert.ToDateTime(dr2["dob"]) : default;
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
                    manager.Dob = (dr5["dob"] != DBNull.Value) ? Convert.ToDateTime(dr5["dob"]) : default;


                    ManagersList.Add(manager);
                }

                return ManagersList;
            }
            catch (Exception)
            {

                throw new Exception("Problem getting the information from the server, please try again later");
            }
            finally
            {
                if (con5 != null)
                {
                    con5.Close();
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
                sa.Append(" INSERT INTO Gamer_Competition (gamerid, competitionid, rank1) ");
                sa.Append(" Values(" + rankarray[i].Gamerid + " , " + rankarray[i].Competitionid + " , " + rankarray[i].Rank1 + " ) ");
            }

            command2 = " UPDATE Competitions SET  competitionstatus = 5 WHERE competitionid = " + rankarray[0].Competitionid + " ";
            command = sa.ToString() + command2;

            return command;
        }


        //---Orgenzier_Main_Page.html--- *Close*


        //---Add_Game_Category.html--- *Open*

        public List<GamesCategories> GetGameCategoriesr() //Add_Game_Category.html - method MC1
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

        public int InsertGameCategory(GamesCategories GameC) //Add_Game_Category.html - method MC2
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

            String cStr = BuildInsertCommand(GameC);      // helper method to build the insert string

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

        private String BuildInsertCommand(GamesCategories GameC)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}')", GameC.Categoryname, GameC.Status1);
            String prefix = "INSERT INTO GamesCategories " + "(categoryName, status1) ";

            command = prefix + sb.ToString();

            return command;

        }

        public int setNotactive(int id, string status) //Add_Game_Category.html - method MC3
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

            String cStr = BuildInsertCommand6(id, status);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected; //return how many row's effected.
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

        private String BuildInsertCommand6(int id, string status)
        {
            if (status == "1")
                return ("UPDATE GamesCategories SET Status1 = 'False' WHERE categoryID= " + id);
            else
                return ("UPDATE GamesCategories SET Status1 = 'True' WHERE categoryID= " + id);
        }

        public int ChangeName(int id, string UpdateCategoryName) //Add_Game_Category.html - method MC4
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

            String cStr = BuildInsertCommand7(id, UpdateCategoryName);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected; //return how many row's effected.
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

        private String BuildInsertCommand7(int id, string UpdateCategoryName)
        {
            string str = "UPDATE GamesCategories SET categoryname ='" + UpdateCategoryName + "' WHERE categoryid= " + id;
            return (str);
        }


        //---Add_Game_Category.html--- *Close*


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
                    p.Postdate = Convert.ToDateTime(dr["postdate"]);
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
                    c.Lastdateforregistration = Convert.ToDateTime(dr["lastdateforregistration"]);
                    c.Body = (string)dr["body"];
                    c.Startdate = Convert.ToDateTime(dr["startdate"]);
                    c.Enddate = Convert.ToDateTime(dr["enddate"]);
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

        public Competitions ReadOneCompetition(int CId)
        {
            SqlConnection con = null;
            Competitions c = new Competitions();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = " select * from competitions where competitionid = " + CId + "";
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
                    c.Prize1 = (string)dr["prize1"];
                    c.Prize2 = (string)dr["prize2"];
                    c.Price3 = (string)dr["prize3"];
                    c.Linkforregistration = (string)dr["linkforregistration"];
                    c.Lastdateforregistration = Convert.ToDateTime(dr["lastdateforregistration"]);
                    c.Body = (string)dr["body"];
                    c.Startdate = Convert.ToDateTime(dr["startdate"]);
                    c.Enddate = Convert.ToDateTime(dr["enddate"]);
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
            command = " UPDATE Orgenaizers set firstname = '" + o1.Firstname + "' , lastname = '" + o1.Lastname + "' , gender = '" + o1.Gender + "' , phone = '" + o1.Phone + "', dob = '" + o1.Dob + "' , address1 = '" + o1.Address1 + "' , picture = '" + o1.Picture + "' , communityname = '" + o1.Comunityname + "', linktocommunity = '" + o1.Linktocommunity + "' where userid = " + OId + " ";

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

        public int Getcategory(int CId)
        {
            var categoryId = 0;
            SqlConnection con = null;
            Competitions c = new Competitions();


            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = " select categoryid from Competition_Game where competitionid = " + CId + "";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read()) //1 row
                {
                    categoryId = Convert.ToInt32(dr["categoryid"]);

                }
                return (categoryId);
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


        //---Edit_Competition.html--- *Close*

        //---ReactClientSide---*Open*

        public List<Competitions> GetCompetitonL()
        {
            SqlConnection con = null;
            List<Competitions> cList = new List<Competitions>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = " select * from Competitions where competitionstatus = '2' ";
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
                    c.Lastdateforregistration = Convert.ToDateTime(dr["lastdateforregistration"]);
                    c.Body = (string)dr["body"];
                    c.Startdate = Convert.ToDateTime(dr["startdate"]);
                    c.Enddate = Convert.ToDateTime(dr["enddate"]);
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

        //---ReactClientSide---*Close*


    }

}