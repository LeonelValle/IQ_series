using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQProject
{
    class WO : Conexion
    {
        int id_wo;
        static string wo;
        string rev;
        int qty;

        public int Id_wo { get => id_wo; set => id_wo = value; }
        public string Wo { get => wo; set => wo = value; }
        public string Rev { get => rev; set => rev = value; }
        public int Qty { get => qty; set => qty = value; }
    }
}
