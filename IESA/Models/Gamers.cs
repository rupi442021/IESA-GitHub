using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IESA.Models
{
    public class Gamers
    {
        int userid; //Automatic
        string mail;
        string password1;
        int gamercardnum;
        string nickname;
        string firstname;
        string lastname;
        int id; //Passport id
        DateTime dob;
        string address1;
        string discorduser;
        string picture;
        DateTime registrationdate;
        DateTime outofdate;
        int license; //0 or 1
        int status1; //0 or 1

        public Gamers(int userid, string mail, string password1, int gamercardnum, string nickname, string firstname, string lastname, int id, DateTime dob, string address1, string discorduser, string picture, DateTime registrationdate, DateTime outofdate, int license, int status1)
        {
            Userid = userid;
            Mail = mail;
            Password1 = password1;
            Gamercardnum = gamercardnum;
            Nickname = nickname;
            Firstname = firstname;
            Lastname = lastname;
            Id = id;
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
        public string Mail { get => mail; set => mail = value; }
        public string Password1 { get => password1; set => password1 = value; }
        public int Gamercardnum { get => gamercardnum; set => gamercardnum = value; }
        public string Nickname { get => nickname; set => nickname = value; }
        public string Firstname { get => firstname; set => firstname = value; }
        public string Lastname { get => lastname; set => lastname = value; }
        public int Id { get => id; set => id = value; }
        public DateTime Dob { get => dob; set => dob = value; }
        public string Address1 { get => address1; set => address1 = value; }
        public string Discorduser { get => discorduser; set => discorduser = value; }
        public string Picture { get => picture; set => picture = value; }
        public DateTime Registrationdate { get => registrationdate; set => registrationdate = value; }
        public DateTime Outofdate { get => outofdate; set => outofdate = value; }
        public int License { get => license; set => license = value; }
        public int Status1 { get => status1; set => status1 = value; }


        //#Functions Area








    }
}