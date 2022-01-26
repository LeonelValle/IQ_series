using System;
using System.Net.Mail;
using System.Windows.Forms;
using Exception = System.Exception;
using System.Data;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Linq;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace IQProject
{
    public partial class Form1 : Form
    {
        private readonly Assembly ensamble = new Assembly();
        private readonly Repit repetido = new Repit();
        readonly Employ emp = new Employ();
        readonly WO wo = new WO();

        readonly Image pass = IQProject.Properties.Resources.great;
        readonly Image fail = IQProject.Properties.Resources.bad;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //pictureBox1.Image = pass;
            try
            {


                wo.Id_wo = int.Parse(wo.ReturnID("select id_wo from tb_WO where wo = '" + wo.Wo + "'"));
                ensamble.Id_essambly = int.Parse(wo.ReturnID("select id_model from tb_WO where id_wo = '" + wo.Id_wo + "'"));
                emp.Id_emp = int.Parse(emp.ReturnID("select id_emp from tb_Employee where emp = '" + emp.Emp + "'"));
                ensamble.Model = ensamble.ReturnValue("select model from tb_Model where id_model = '" + ensamble.Id_essambly + "'");

                LoadGV();


            }
            catch (Exception)
            {
                MessageBox.Show("No se encontro la base de datos!", "ERROR!");
            }
        }

        private void Btn_submit_Click(object sender, EventArgs e)
        {
            //IF PARA SABER SI LOS TEXTBOX ESTAN VACIOS
            if (ValidarTextboxVacios() == true)
            {
                if (!(ensamble.Existe("select COUNT(*) from tb_Assembly where idNumber = '" + txt_idNumber.Text + "' and idNumber != ''")))
                {
                    ensamble.Crud(comando: "insert into tb_Assembly(idNumber, model, regDate, id_wo, id_emp) values( '" + txt_idNumber.Text.ToUpper() + "' , '" + ensamble.Model + "' , '" + DateTime.Now + "' , '" + wo.Id_wo + "' , '" + emp.Id_emp + "' )");
                    pictureBox1.Image = IQProject.Properties.Resources.great;
                    //PrintLabel();
                    VaciarTextbox();
                }
                else
                {
                    MessageBox.Show("El ID Number ya existe!");
                    //SendEmail(txt_idNumber.Text.ToUpper(), cb_model.SelectedValue.ToString());
                    pictureBox1.Image = IQProject.Properties.Resources.bad;
                    repetido.Crud(comando: "insert into tb_RepitedAssembly(idNumber, model, regDate, id_wo, id_emp) values( '" + txt_idNumber.Text.ToUpper() + "' , '" + ensamble.Model + "' , '" + DateTime.Now + "' , '" + wo.Id_wo + "' , '" + emp.Id_emp + "' )");
                    VaciarTextbox();
                }
            }
            else
            {
                MessageBox.Show("Ne deje ningun campo vacio!");
            }
            //Form1_Load(sender, e);
            LoadGV();
        }

        private void LoadGV()
        {
            try
            {
                //dataGridView1.DataSource = ensamble.LlenarDG("select TOP 1000  idNumber AS IDNumber, model AS Model, regDate AS Date from tb_Assembly order by id_assembly desc ").Tables[0];
                dataGridView1.DataSource = ensamble.LlenarDG(" select TOP 1000  idNumber AS IDNumber, model AS Model, regDate AS Date from tb_Assembly am join tb_WO wo on am.id_wo = wo.id_wo where wo.id_wo = '" + wo.Id_wo + "' order by id_assembly desc").Tables[0];
                dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
                dataGridView1.RowHeadersVisible = false;
                //dataGridView1.Columns[0].Visible = false;
                dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Ascending);


            }
            catch (Exception)
            {
                MessageBox.Show("No se encontro la base de datos!", "ERROR!");
            }
        }

        private bool ValidarTextboxVacios()
        {


            if (txt_idNumber.Enabled == true && (string.IsNullOrEmpty(txt_idNumber.Text)))
            {
                txt_idNumber.Focus();
                return false;
            }

            return true;
        }

        private void VaciarTextbox()
        {
            txt_idNumber.Text = "";
        }


        private void Btn_paste_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text) == true)
            {

                txt_idNumber.Paste();
                Clipboard.Clear();

            }
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
                StringBuilder csvMemoria = new StringBuilder();

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
                            csvMemoria.Append(String.Format("\"{0}\"", DGV.Rows[m].Cells[n].Value));
                        }
                        else
                        {
                            csvMemoria.Append(String.Format("\"{0}\",", DGV.Rows[m].Cells[n].Value));
                        }

                    }
                    csvMemoria.AppendLine();
                }
                System.IO.StreamWriter sw =
                    new System.IO.StreamWriter(dlGuardar.FileName, false,
                       System.Text.Encoding.Default);
                sw.Write(csvMemoria.ToString());
                sw.Close();
            }
        }

        public void ExportarExcel(DataGridView dataCWS)
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = false;
            //worksheet = workbook.Sheets["Hoja1"];
            worksheet = workbook.ActiveSheet;
            //worksheet.Name = "Usuarios";
            int valorFila = 0;



            for (int i = 1; i <= dataCWS.Columns.Count; i++)
            {
                worksheet.Cells[1, i] = dataCWS.Columns[i - 1].HeaderText;

            }
            valorFila += 1;
            for (int i = 0; i < dataCWS.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataCWS.Columns.Count; j++)
                {
                    if ((dataCWS.Rows[i].Cells[j].Value == null) == false)
                    {
                        if (j <= 2 && dataCWS.Rows[i].Cells[j].Value.ToString() != "")
                        {
                            worksheet.Cells[valorFila + 1, j + 1] = dataCWS.Rows[i].Cells[j].Value.ToString();
                            worksheet.Cells.NumberFormat = "@";
                        }
                        else
                            worksheet.Cells[valorFila + 1, j + 1] = dataCWS.Rows[i].Cells[j].Value.ToString();

                    }
                }
                valorFila++;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Archivos de Excel|*.xlsx",
                Title = "Guardar archivo",
                FileName = ""
            };
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                Console.WriteLine("Ruta en: " + saveFileDialog.FileName);
                workbook.SaveAs(saveFileDialog.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                app.Quit();
            }

        }

        private void Btn_report_Click(object sender, EventArgs e)
        {
            //DataGridView dataCWS = new DataGridView();

            dataGridView2.DataSource = ensamble.LlenarDG("select idNumber AS IDNumber, model AS Model, regDate AS Date from tb_RepitedAssembly").Tables[0];
            //ExportarExcel(dataGridView2);
            if (dataGridView2.Rows.Count != 0)
                SaveToCSV(dataGridView2);

            else
                MessageBox.Show("Realize una busqueda", "ERROR!");
        }

        private void SendEmail(string idnumber, string model)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient client = new SmtpClient("192.168.1.5");
                //SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

                mail.From = new MailAddress("notifications@masterworkelectronics.com");
                mail.To.Add("leonel.7648@gmail.com");
                mail.To.Add("leonel.valle@masterworkelectronics.com");
                mail.Subject = "ID NUMBER REPETIDO";
                mail.Body = "Se encontró un repetido con el siguiente ID number: " + idnumber + " del modelo: " + model + " con fecha: " + DateTime.Now;


                client.Credentials = new System.Net.NetworkCredential("notifications@masterworkelectronics.com", "Mexicali1");
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                //client.UseDefaultCredentials = true;
                client.Port = 587;
                client.EnableSsl = true;

                ServicePointManager.ServerCertificateValidationCallback =
            delegate (
                object s,
                X509Certificate certificate,
                X509Chain chain,
                SslPolicyErrors sslPolicyErrors
            )
            {
                return true;
            };

                client.Send(mail);
                MessageBox.Show("mail Send");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            // Find last visible row
            DataGridViewRow row = dataGridView1.Rows.Cast<DataGridViewRow>().Where(r => r.Visible).Last();
            // scroll to last row if necessary
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.IndexOf(row);
            // select row
            row.Selected = true;
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Mantenimiento manto = new Mantenimiento();
            manto.Show();
        }

        private void PrintLabel()
        {
            BarTender.Application btapp;
            BarTender.Format btformat;
            btapp = new BarTender.Application();
            btformat = btapp.Formats.Open(@"C:\Users\leonel.valle\Documents\BarTender\BarTender Documents\IQ20-TEST.btw", true, "");
            btformat.PrintSetup.NumberSerializedLabels = 1;
            btformat.SetNamedSubStringValue("MAC", txt_idNumber.Text);
            //btformat.SetNamedSubStringValue("SN", cb_model.Text);
            btformat.PrintOut(true, true);
        }
    }
}

