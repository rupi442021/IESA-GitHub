﻿using System;
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

        private string emaila;
        private int userId;

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


        public string CheckEmailSQL(string emailADD) //Sign_Up.html - method OG1 (Check Email: Gamer)
        {

            SqlConnection con = null;

            try
            {

                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Gamers Union Orgenaizers WHERE email='" + emailADD + "'";

                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                emaila = "null";

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

        public int GetnewId()  //Sign_Up.html - method OG2 (Get New Id: Gamer)
        {

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

        public int InsertGamer(Gamers gamer) //Sign_Up.html - method OG3 (Insert: Gamer (1))
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

        private String BuildInsertCommand(Gamers gamer) //Sign_Up.html - method OG3 (Insert: Gamer (2))
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}','{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}')", gamer.Email, gamer.Password1, gamer.Nickname, gamer.Firstname, gamer.Lastname, gamer.Gender, gamer.Id, gamer.Phone, gamer.Dob, gamer.Address1, gamer.Discorduser, gamer.Picture, gamer.Registrationdate, gamer.License, gamer.Status1);
            String prefix = "INSERT INTO Gamers " + "(email, password1, nickname, firstname, lastname, gender, id, phone, dob, address1, discorduser, picture, registrationdate, license, status1) ";

            command = prefix + sb.ToString();

            return command;

        }


        //---Sign_Up.html--- *Close*








    }
}