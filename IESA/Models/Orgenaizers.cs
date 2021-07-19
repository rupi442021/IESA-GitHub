using IESA.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IESA.Models
{
    public class Orgenaizers
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
        string address1;
        string picture;
        string comunityname;
        string linktocommunity;
        int status1; //0 or 1

        public Orgenaizers(int userid, string email, string password1, string nickname, string firstname, string lastname, string gender, int id, string phone, DateTime dob, string address1, string picture, string comunityname, string linktocommunity, int status1)
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
            Picture = picture;
            Comunityname = comunityname;
            Linktocommunity = linktocommunity;
            Status1 = status1;
        }

        public Orgenaizers() { }

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
        public string Picture { get => picture; set => picture = value; }
        public string Comunityname { get => comunityname; set => comunityname = value; }
        public string Linktocommunity { get => linktocommunity; set => linktocommunity = value; }
        public int Status1 { get => status1; set => status1 = value; }


        //#Functions Area


        //---Sign_Up.html--- *Open*


        public int ReadID() //Sign_Up.html - method OO1
        {
            DBServices dbs = new DBServices();
            int newid = dbs.GetnewIdOrgenaizer();
            return newid;
        }

        public void InsertOrgenaizer() //Sign_Up.html - method OO2
        {
            DBServices dbs = new DBServices();
            dbs.InsertOrgenaizer(this);
        }


        //---Sign_Up.html--- *Close*


        //---Sign_In.html--- *Open*


        public Orgenaizers getUserInfo(int id_toserver) //Sign_In.html - method OL3
        {
            DBServices dbs = new DBServices();
            return dbs.getOrgenaizerSQL(id_toserver);
        }


        //---Sign_In.html--- *Close*


        //---Manager_Main_Page.html--- *Open*


        public List<Orgenaizers> ReadOrgenaizers() //Manager_Main_Page.html - method OM4
        {
            DBServices dbs = new DBServices();
            List<Orgenaizers> olist = dbs.ReadOrgenaizersMSQL();
            return olist;
        }

        public void DeleteOrgenaizer(int todeleteid) //Manager_Main_Page.html - method OM5
        {
            DBServices dbs = new DBServices();
            dbs.DeleteOrgenaizerSQL(todeleteid);
        }


        public void UpdateOrgenaizerStatus(int toupdateid) //Manager_Main_Page.html - method OM6 
        {
            DBServices dbs = new DBServices();
            dbs.UpdateOrgenaizerStatusSQL(toupdateid);
        }

        //---Manager_Main_Page.html--- *Close*


        //---Database.html--- *Open*

        public List<Orgenaizers> ReadOrgenaizersDB() //Database.html - method OD6
        {
            DBServices dbs = new DBServices();
            List<Orgenaizers> olist = dbs.ReadOrgenaizersDBSQL();
            return olist;
        }

        //---Database.html--- *Close*


        //---Edit_Personal_Details.html--- *Open*

        public void UpdateOrgenaizerDetails(int OId, Orgenaizers o1) //Manager_Main_Page.html - method OM6 + Edit_Personal_Details.html - method SO1
        {
            DBServices dbs = new DBServices();
            dbs.UpdateOrgenaizerDetails(OId, o1);
        }

        //---Edit_Personal_Details.html--- *Close*




        //Edit_Personal_By_Manager.html - method MU1---*Open*
        public List<Orgenaizers> ReadOrgDetails(int IdUser) 
        {
            DBServices dbs = new DBServices();
            List<Orgenaizers> glist = dbs.ReadOrgDetails(IdUser);
            return glist;
        }

        //Edit_Personal_By_Manager.html - method MU1---*Close*







    }
}