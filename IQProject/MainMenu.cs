using System;
using System.Linq;
using System.Windows.Forms;

namespace IQProject
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        public static Form IsFormAlreadyOpen(Type formType)
        {
            return Application.OpenForms.Cast<Form>().FirstOrDefault(openForm => openForm.GetType() == formType);
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void btn_Scan_Click(object sender, EventArgs e)
        {
            IndentifyWO setup = new IndentifyWO();
            //setup.Show();

            Form sp;

            if ((sp = IsFormAlreadyOpen(typeof(IndentifyWO))) == null)
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

        private void btn_WO_Click(object sender, EventArgs e)
        {
            Form2 setup = new Form2();
            //setup.Show();

            Form sp;

            if ((sp = IsFormAlreadyOpen(typeof(Form2))) == null)
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

        private void btn_Reports_Click(object sender, EventArgs e)
        {
            Reports setup = new Reports();
            //setup.Show();

            Form sp;

            if ((sp = IsFormAlreadyOpen(typeof(Reports))) == null)
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
    }
}
