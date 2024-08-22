using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Farmacist
    {
        public int ID { get; set; }
        public string Nume { get; set; }
        public int Varsta { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Localitate { get; set; }
        public string Judet { get; set; }
        public Farmacist(int ID, string Nume, int Varsta, string Email, string Telefon, string Localitate, string Judet)
        {
            this.ID = ID;
            this.Nume = Nume;
            this.Varsta = Varsta;
            this.Email = Email;
            this.Telefon = Telefon;
            this.Localitate = Localitate;
            this.Judet = Judet;
        }
    }

}