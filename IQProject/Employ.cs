using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQProject
{
    class Employ : Conexion
    {
        int id_emp;
        string name;
        static int emp;

        public int Id_emp { get => id_emp; set => id_emp = value; }
        public string Name { get => name; set => name = value; }
        public int Emp { get => emp; set => emp = value; }
    }
}
