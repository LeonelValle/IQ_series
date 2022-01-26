using System;
using System.Windows.Forms;

namespace IQProject
{
    public partial class Reports : Form
    {
        readonly WO wo = new WO();
        public Reports()
        {
            InitializeComponent();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            string salida_datos = "";
            _ = this.txt_Search.Text.Split(' ');

            if (cb_Report.Text == "Serial Number")
            {
                if (wo.Existe("select COUNT(*) from tb_Assembly where idNumber = '" + txt_Search.Text + "'"))
                {
                    string id_modelorden = wo.ReturnID("select idNumber from tb_Assembly where idNumber = '" + txt_Search.Text + "'");
                    salida_datos = "select idNumber AS IDNumber, model AS Model, regDate, wo.wo, wo.qty, wo.rev from tb_Assembly am left join tb_WO wo on am.id_wo = wo.id_wo where am.idNumber = '" + id_modelorden + "' order by id_assembly desc";
                }

            }

            if (cb_Report.Text == "WO")
            {
                int id_orden = int.Parse(wo.ReturnValue("select id_wo from tb_WO where wo = " + txt_Search.Text));
                salida_datos = "select idNumber AS IDNumber, model AS Model, regDate , wo.wo, wo.qty, wo.rev from tb_Assembly am left join tb_WO wo on am.id_wo = wo.id_wo where wo.id_wo = '" + id_orden + "' order by id_assembly desc";
            }


            if (cb_Report.Text == "Model")
            {

                salida_datos = "select idNumber AS IDNumber, model AS Model, regDate , wo.wo, wo.qty, wo.rev from tb_Assembly am left join tb_WO wo on am.id_wo = wo.id_wo where am.model = '" + txt_Search.Text + "' order by id_assembly desc";
            }



            dg_Reports.DataSource = wo.LlenarDG(salida_datos).Tables[0];
        }

        private void btn_Report_Click(object sender, EventArgs e)
        {
            SaveToCSV(dg_Reports);
        }

        private void SaveToCSV(DataGridView DGV)
        {
            SaveFileDialog dlGuardar = new SaveFileDialog
            {
                Filter = "Fichero CSV (*.csv)|*.csv",
                FileName = "",
                Title = "Exportar a CSV"
            };
            if (dlGuardar.ShowDialog() == DialogResult.OK)
            {
                System.Text.StringBuilder csvMemoria = new System.Text.StringBuilder();

                //para los títulos de las columnas, encabezado
                for (int i = 0; i < DGV.Columns.Count; i++)
                {
                    if (i == DGV.Columns.Count - 1)
                    {
                        csvMemoria.Append(String.Format("\"{0}\"",
                            DGV.Columns[i].HeaderText));
                    }
                    else
                    {
                        csvMemoria.Append(String.Format("\"{0}\",",
                            DGV.Columns[i].HeaderText));
                    }
                }

                csvMemoria.AppendLine();


                for (int m = 0; m < DGV.Rows.Count; m++)
                {
                    for (int n = 0; n < DGV.Columns.Count; n++)
                    {
                        //si es la última columna no poner el ;
                        if (n == DGV.Columns.Count - 1)
                        {
                            csvMemoria.Append(String.Format("\"{0}\"", DGV.Rows[m].Cells[n].Value, @"\d+"));
                        }
                        else
                        {
                            csvMemoria.Append(String.Format("\"{0}\",", DGV.Rows[m].Cells[n].Value, @"\d+"));
                        }

                    }
                    csvMemoria.AppendLine();
                }
                System.IO.StreamWriter sw = new System.IO.StreamWriter(dlGuardar.FileName, false, System.Text.Encoding.Default);
                sw.Write(csvMemoria.ToString());
                sw.Close();
            }
        }

    }
}
