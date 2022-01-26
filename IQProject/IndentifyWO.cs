using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IQProject
{
    public partial class IndentifyWO : Form
    {
        readonly Conexion con = new Conexion();
        readonly WO wo = new WO();
        public IndentifyWO()
        {
            InitializeComponent();
        }

        public static Form IsFormAlreadyOpen(Type formType)
        {
            return Application.OpenForms.Cast<Form>().FirstOrDefault(openForm => openForm.GetType() == formType);
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                wo.Wo = txt_WO.Text;
                if (txt_WO.Text == "")
                    throw new Exception();

                SqlCommand cmd = new SqlCommand("select wo from tb_WO where wo = '" + wo.Wo + "'", con.Con1);
                con.Abrir();
                cmd.ExecuteNonQuery();
                string regreso = cmd.ExecuteScalar().ToString();
                if (regreso != "0")
                {
                    this.Close();
                    //Ensamble_Etiquetado ee = new Ensamble_Etiquetado();
                    Employee setup = new Employee();
                    //setup.Show();

                    Form sp;

                    if ((sp = IsFormAlreadyOpen(typeof(Employee))) == null)
                    {
                        setup.ShowDialog(this);
                        this.Close();
                    }

                    else
                    {
                        sp.WindowState = FormWindowState.Normal;
                        sp.BringToFront();
                    }
                }
                else
                {
                    MessageBox.Show("No se encontro");
                    txt_WO.Text = "";
                }

                con.Cerrar();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No existe ese registro", "ERROR!");
                con.Cerrar();
            }
            catch (Exception)
            {
                MessageBox.Show("Inserte una orden");
            }
        }

        private void txt_WO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Submit_Click(this, new EventArgs());
            }
        }

        private void IndentifyWO_Load(object sender, EventArgs e)
        {

        }
    }
}
