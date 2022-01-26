using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQProject
{
    class Repit : Conexion
    {
        int id_essambly;
        string idNumber;
        string model;
        DateTime regDate;

        public int Id_essambly { get => id_essambly; set => id_essambly = value; }
        public string IdNumber { get => idNumber; set => idNumber = value; }
        public string Model { get => model; set => model = value; }
        public DateTime RegDate { get => regDate; set => regDate = value; }
    }
}
