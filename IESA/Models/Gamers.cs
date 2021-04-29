using IESA.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IESA.Models
{
    public class Gamers
    {
        int userid; //GamerCardNum
        string email;
        string password1;
        string nickname;
        string firstname;
        string lastname;
        string gender;
        int id; //Passport id
        string phone;
        DateTime dob;
        string address1;
        string discorduser;
        string picture;
        DateTime registrationdate;
        DateTime outofdate;
        int license; //0 or 1
        int status1; //0 or 1
     

        public Gamers(int userid, string email, string password1, string nickname, string firstname, string lastname, string gender, int id, String phone, DateTime dob, string address1, string discorduser, string picture, DateTime registrationdate, DateTime outofdate, int license, int status1)
        {
            Userid = userid;
            Email = email;
            Password1 = password1;
            Nickname = nickname;
            Firstname = firstname;
            Lastname = lastname;
            Gender = gender;
            Id = id;
            Phone = phone;
            Dob = dob;
            Address1 = address1;
            Discorduser = discorduser;
            Picture = picture;
            Registrationdate = registrationdate;
            Outofdate = outofdate;
            License = license;
            Status1 = status1;
        }

        public Gamers() { }

        public int Userid { get => userid; set => userid = value; }
        public string Email { get => email; set => email = value; }
        public string Password1 { get => password1; set => password1 = value; }
        public string Nickname { get => nickname; set => nickname = value; }
        public string Firstname { get => firstname; set => firstname = value; }
        public string Lastname { get => lastname; set => lastname = value; }
        public string Gender { get => gender; set => gender = value; }
        public int Id { get => id; set => id = value; }
        public string Phone { get => phone; set => phone = value; }
        public DateTime Dob { get => dob; set => dob = value; }
        public string Address1 { get => address1; set => address1 = value; }
        public string Discorduser { get => discorduser; set => discorduser = value; }
        public string Picture { get => picture; set => picture = value; }
        public DateTime Registrationdate { get => registrationdate; set => registrationdate = value; }
        public DateTime Outofdate { get => outofdate; set => outofdate = value; }
        public int License { get => license; set => license = value; }
        public int Status1 { get => status1; set => status1 = value; }


        //#Functions Area


        //---Sign_Up.html--- *Open*


        public string CheckEmail(string emailADD) //Sign_Up.html - method OG1
        {
            DBServices dbs = new DBServices();
            return dbs.CheckEmailSQL(emailADD);
        }

        public string CheckNickname(string nicknameAdd) //Sign_Up.html - method OG2
        {
            DBServices dbs = new DBServices();
            return dbs.CheckNicknameSQL(nicknameAdd);
        }

        public int ReadID() //Sign_Up.html - method OG3
        {
            DBServices dbs = new DBServices();
            int newid = dbs.GetnewIdGamer();
            return newid;
        }

        public void InsertGamer() //Sign_Up.html - method OG4
        {
            DBServices dbs = new DBServices();
            dbs.InsertGamer(this);
        }


        //---Sign_Up.html--- *Close*


        //---Sign_In.html--- *Open*


        public int CheckInfo(string email, string pass) //Sign_Up.html - method OL1
        {
            //This is General check for all users at ones*

            DBServices dbs = new DBServices();
            return dbs.CheckInfoSQL(email, pass);
        }

        public Gamers GetUserInfo(int id_toserver) //Sign_In.html - method OL2 + Edit_Personal_Details.html - method SG2
        {
            DBServices dbs = new DBServices();
            return dbs.getGamerSQL(id_toserver);
        }


        //---Sign_In.html--- *Close*



        //---Manager_Main_Page.html--- *Open*


        public List<Gamers> ReadGamers() //Manager_Main_Page.html - method OM1
        {
            DBServices dbs = new DBServices();
            List<Gamers> glist = dbs.ReadGamersMSQL();
            return glist;
        }

        public void DeleteGamer(int todeleteid) //Manager_Main_Page.html - method OM2
        {
            DBServices dbs = new DBServices();
            dbs.DeleteGamerSQL(todeleteid);
        }

        public void UpdateGamerStatus(int toupdateid) //Manager_Main_Page.html - method OM3
        {
            DBServices dbs = new DBServices();
            dbs.UpdateGamerStatusSQL(toupdateid);
        }





        //---Manager_Main_Page.html--- *Close*


        //---Database.html--- *Open*

        public List<Gamers> ReadGamersDB() //Database.html - method OD5
        {
            DBServices dbs = new DBServices();
            List<Gamers> glist = dbs.ReadGamersDBSQL();
            return glist;
        }

        //---Database.html--- *Close*


        //---Orgenaizer_Main_Page.html--- *Open*


        public List<Gamers> Read() //Orgenaizer_Main_Page.html - method SG1
        {
            DBServices dbs = new DBServices();
            List<Gamers> lGamers = dbs.GetGamers();
            return lGamers;
        }


        //---Orgenaizer_Main_Page.html--- *Close*


        //---Edit_Personal_Details.html--- *Open*
        public void UpdateGamerDetails(int GId, Gamers g1) //Edit_Personal_Details.html - method SE1 
        {
            DBServices dbs = new DBServices();
            dbs.UpdateGamerDetails(GId, g1);
        }

        //---Edit_Personal_Details.html--- *Close*





        //Edit_Personal_By_Manager.html - method MU2---*Open*
        public List<Gamers> ReadGamerDetails(int IdUser) //Manager_Main_Page.html - method OM1
        {
            DBServices dbs = new DBServices();
            List<Gamers> glist = dbs.ReadGamerDetails(IdUser);
            return glist;
        }

        //Edit_Personal_By_Manager.html - method MU2---*Close*







    }
}