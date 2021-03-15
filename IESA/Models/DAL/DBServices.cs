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

        public int GetnewIdGamer()  //Sign_Up.html - method OG3 (Get New Id: Gamer)
        {
            userId = 0;

            SqlConnection con = null;

            try
            {

                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT TOP 1 * FROM Gamers ORDER BY userID DESC";


                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read()) //1 row
                {
                    userId = Convert.ToInt32(dr["userID"]); //future id of the user
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

        public int GetnewIdOrgenaizer()  //Sign_Up.html - method OO1 (Get New Id: Orgenaizer)
        {
            userId = 0;

            SqlConnection con = null;

            try
            {

                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT TOP 1 * FROM Orgenaizers ORDER BY userID DESC";


                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read()) //1 row
                {
                    userId = Convert.ToInt32(dr["userID"]); //future id of the user
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
            String prefix = "INSERT INTO Orgenaizers " + "(email, password1, nickname, firstname, lastname, gender, id, phone, dob, address1, picture, communityName, linkToCommunity, status1) ";

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


                String selectSTR = "SELECT Gamers.userID, Gamers.email, Gamers.password1 FROM Gamers WHERE Gamers.email = '" + email + "' UNION SELECT Orgenaizers.userID, Orgenaizers.email, Orgenaizers.password1 FROM Orgenaizers WHERE Orgenaizers.email = '" + email + "' UNION SELECT Managers.userID, Managers.email, Managers.password1 FROM Managers WHERE Managers.email = '" + email + "' ";


                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end


                while (dr.Read()) //1 row
                {
                    idInfo = Convert.ToInt32(dr["userID"]); //We want to get the id for localstorage

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

                String selectSTR = "SELECT * FROM Gamers WHERE userID = " + id_toserver;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end


                while (dr.Read()) //1 row
                {

                    g.Userid = (dr["userID"] != DBNull.Value) ? Convert.ToInt32(dr["userID"]) : default;
                    g.Email = (dr["email"] != DBNull.Value) ? (string)dr["email"] : default;
                    g.Nickname = (dr["nickname"] != DBNull.Value) ? (string)dr["nickname"] : default;
                    g.Firstname = (dr["firstname"] != DBNull.Value) ? (string)dr["firstname"] : default;
                    g.Lastname = (dr["lastname"] != DBNull.Value) ? (string)dr["lastname"] : default;
                    g.Gender = (dr["gender"] != DBNull.Value) ? (string)dr["gender"] : default;
                    g.Id = (dr["id"] != DBNull.Value) ? Convert.ToInt32(dr["id"]) : default; //Needs to be int in SQL*
                    g.Phone = (dr["phone"] != DBNull.Value) ? (string)dr["phone"] : default;
                    g.Dob = (dr["dob"] != DBNull.Value) ? Convert.ToDateTime(dr["dob"]) : default;
                    g.Address1 = (dr["address1"] != DBNull.Value) ? (string)dr["address1"] : default;
                    g.Discorduser = (dr["discordUser"] != DBNull.Value) ? (string)dr["discordUser"] : default;
                    g.Picture = (dr["picture"] != DBNull.Value) ? (string)dr["picture"] : default;
                    g.Registrationdate = (dr["registrationDate"] != DBNull.Value) ? Convert.ToDateTime(dr["registrationDate"]) : default;
                    g.Outofdate = (dr["outOfDate"] != DBNull.Value) ? Convert.ToDateTime(dr["outOfDate"]) : default;
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

                String selectSTR = "SELECT * FROM Orgenaizers WHERE userID = " + id_toserver;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end


                while (dr.Read()) //1 row
                {

                    o.Userid = (dr["userID"] != DBNull.Value) ? Convert.ToInt32(dr["userID"]) : default;
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
                    o.Comunityname = (dr["communityName"] != DBNull.Value) ? (string)dr["communityName"] : default;
                    o.Linktocommunity = (dr["linkToCommunity"] != DBNull.Value) ? (string)dr["linkToCommunity"] : default;
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

                String selectSTR = "SELECT * FROM Managers WHERE userID = " + id_toserver;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end


                while (dr.Read()) //1 row
                {

                    m.Userid = (dr["userID"] != DBNull.Value) ? Convert.ToInt32(dr["userID"]) : default;
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


        public List<GamesCategories> GetGameCategoriesr()
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
                    g.Categoryid = Convert.ToInt32(dr["categoryID"]);
                    g.Categoryname = (string)dr["categoryName"];
                    g.Status1 = Convert.ToInt32(dr["status1"]);

                    if(g.Status1 == 1)
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

                String selectSTR = "SELECT TOP 1 * FROM Competitions ORDER BY CompetitionID DESC";


                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read()) //1 row
                {
                    competitionId = Convert.ToInt32(dr["CompetitionID"]); //future id of the user
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

        public int InsertCompetition(Competitions Competition) ////Add_New_Competition.html 
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
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}','{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}','{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}','{22}', '{23}')", Competition.Competitionname, Competition.Address1, Competition.Banner, Competition.Logo, Competition.Prize1, Competition.Prize2, Competition.Price3, Competition.Linkforregistration, Competition.Lastdateforregistration, Competition.Body, Competition.Startdate, Competition.Enddate, Competition.Startime, Competition.Endtime, Competition.Ispro, Competition.Discord, Competition.Console, Competition.Isiesa, Competition.Linkforstream, Competition.Competitionstatus, Competition.IsPayment, Competition.Isonline, Competition.Startcheckin, Competition.Endcheckin);
            String prefix = "INSERT INTO Competitions " + "(competitionName, address1, banner, logo, prize1, prize2, prize3, linkForRegistration, lastDateForRegistration, body , startDate, endDate, startTime, endTime, isPro, discord, console, isIESA, linkForStream, competitionStatus, isPayment, isOnline, startCheckIn, endCheckIn) ";
            
            command = prefix + sb.ToString();
            
            return command;
        }

        

        public int InsertGameInC(int cID, int gcID) //Add_New_Competition.html - Add row to game_competition 
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

            String cStr = BuildInsertCommand(cID, gcID);      // helper method to build the insert string

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
        private String BuildInsertCommand(int cID, int gcID) //Add_New_Competition.html - Add row to game_competition
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
           String prefix1 = " INSERT INTO Competition_Game VALUES (" + cID + ", " + gcID + ")";

            command = prefix1;

            return command;


        }



        //---Add_New_Competition.html--- *Close*




        //---Orgenaizer_Main_Page.html--- *Open*
        
        public List<Competitions> GetOrgenaizerCompetitions(int OID)
        {
            SqlConnection con = null;
            List<Competitions> cList = new List<Competitions>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from Competitions inner join Orgenaizer_Competition ON Competitions.competitionID = Orgenaizer_Competition.competitionID where Orgenaizer_Competition.orgenaizerID="+OID;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Competitions c    = new Competitions();
                    c.Competitionid   = Convert.ToInt32(dr["competitionID"]);
                    c.Competitionname = (string)dr["competitionName"];
                    c.Isonline        = Convert.ToInt32(dr["isOnline"]);
                    c.Address1        = (string)dr["address1"];
                    c.Banner          = (string)dr["banner"];
                    c.Logo            = (string)dr["logo"];
                    c.Prize1          = (string)dr["prize1"];
                    c.Prize2          = (string)dr["prize2"];
                    c.Price3          = (string)dr["prize3"];
                    c.Linkforregistration = (string)dr["linkForRegistration"];
                    c.Lastdateforregistration = Convert.ToDateTime(dr["lastDateForRegistration"]);
                    c.Body = (string)dr["body"];
                    c.Startdate = Convert.ToDateTime(dr["startDate"]);
                    c.Enddate = Convert.ToDateTime(dr["endDate"]);
                    c.Startime = ((TimeSpan)dr["startTime"]);
                    c.Endtime = ((TimeSpan)dr["endTime"]);
                    c.Ispro = Convert.ToInt32(dr["isPro"]);
                    c.Discord = (string)dr["discord"];
                    c.Console = (string)dr["console"];
                    c.Isiesa = Convert.ToInt32(dr["isIESA"]);
                    c.Linkforstream = (string)dr["linkForStream"];
                    c.Numofparticipants = Convert.ToInt32(dr["numOfParticipants2"]);
                    c.Competitionstatus = (string)dr["competitionStatus"];
                    c.Status1 = Convert.ToInt32(dr["status1"]);
                    c.IsPayment = Convert.ToInt32(dr["isPayment"]);
                    c.Startcheckin = ((TimeSpan)dr["startCheckIn"]);
                    c.Endcheckin = ((TimeSpan)dr["endCheckIn"]);

                    //c.Gamecategory = (string)dr["gameCategory"]; - Missed Column in DB.




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



        //---Orgenaizer_Main_Page.html--- *Close*



    }
}