using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQProject
{
    class Model : Conexion
    {
        int id_model;
        string model;

        public int Id_model { get => id_model; set => id_model = value; }
        public string Models { get => model; set => model = value; }
    }
}
