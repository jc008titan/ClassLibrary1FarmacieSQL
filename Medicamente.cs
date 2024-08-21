using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClassLibrary1
{
    public class Medicament
    {
        public string Nume { get; set; }
        public string Data_Expirare { get; set; }
        public string Pret { get; set; }
        public string Cantitate { get; set; }
        public int Categorie { get; set; }
        public int id { get; set; }
    }
}
