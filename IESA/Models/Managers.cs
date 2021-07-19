using IESA.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IESA.Models
{
    public class Managers
    {
        int userid; //Automatic
        string email;
        string password1;
        string nickname;
        string firstname;
        string lastname;
        string gender;
        int id; //Passport id
        string phone;
        DateTime dob;
        string role1;
        string address1;
        string picture;

        public Managers(int userid, string email, string password1, string nickname, string firstname, string lastname, string gender, int id, string phone, DateTime dob, string role1, string address1, string picture)
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
            Role1 = role1;
            Address1 = address1;
            Picture = picture;
        }

        public Managers() { }

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
        public string Role1 { get => role1; set => role1 = value; }
        public string Address1 { get => address1; set => address1 = value; }
        public string Picture { get => picture; set => picture = value; }


        //#Functions Area


        //---Sign_In.html--- *Open*


        public Managers getUserInfo(int id_toserver) //Sign_In.html - method OL4
        {
            DBServices dbs = new DBServices();
            return dbs.getManagerSQL(id_toserver);
        }


        //---Sign_In.html--- *Close*


        //---Manager_Main_Page.html--- *Open*

        public Dictionary<int, int> GetStatisticsM() //Manager_Main_Page.html - method OM9
        {
            DBServices dbs = new DBServices();
            Dictionary<int, int> slist = dbs.GetStatisticsSQL();
            return slist;
        }

        public Dictionary<int, int> GetGraph() //Manager_Main_Page.html - method OM10
        {
            DBServices dbs = new DBServices();
            Dictionary<int, int> glist = dbs.GetGraphSQL();
            return glist;
        }


        //---Manager_Main_Page.html--- *Close*


        //---Database.html--- *Open*

        public List<Managers> ReadManagersDB() //Database.html - method OD7
        {
            DBServices dbs = new DBServices();
            List<Managers> mlist = dbs.ReadManagersDBSQL();
            return mlist;
        }


        //---Database.html--- *Close*


        //---Edit_Personal_Details.html--- *Open*

        public void updateManagerDetails(int MId, Managers m1) //Edit_Personal_Details.html - method OME1
        {
            DBServices dbs = new DBServices();
            dbs.updateManagerDetailsSQL(MId, m1);
        }


        //---Edit_Personal_Details.html--- *Close*

    }
}