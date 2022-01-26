using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace IQProject
{
    public partial class Employee : Form
    {
        Conexion con = new Conexion();
        Employ emp = new Employ();

        public Employee()
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
                emp.Emp = Convert.ToInt32(txt_emp.Text);
                if (txt_emp.Text == "")
                    throw new Exception();

                int regreso = int.Parse(emp.ReturnValue("select id_emp from tb_Employee where emp = " + txt_emp.Text));

                if (regreso > 0)
                {
                    //orden.Crud("update tb_Orden set id_operador = " + regreso + " where orden = '" + orden.Ordenes + "'");
                    Form1 ee = new Form1();
                    //ee.Show();

                    Form enet;

                    if ((enet = IsFormAlreadyOpen(typeof(Form1))) == null)
                    {
                        ee.ShowDialog(this);
                        this.Close();
                    }

                    else
                    {
                        enet.WindowState = FormWindowState.Normal;
                        enet.BringToFront();
                    }
                }
                else
                {
                    MessageBox.Show("No se encontro");
                    txt_emp.Text = "";
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
                MessageBox.Show("Inserte un Operador!");
            }
        }

        private void txt_emp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Submit_Click(this, new EventArgs());
            }
        }

        private void Employee_Load(object sender, EventArgs e)
        {

        }
    }
}
