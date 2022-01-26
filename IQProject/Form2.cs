using System;
using System.Windows.Forms;

namespace IQProject
{
    public partial class Form2 : Form
    {
        readonly Assembly ensamble = new Assembly();
        readonly Conexion con = new Conexion();
        readonly WO wo = new WO();

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dg_WO.DataSource = wo.LlenarDG("select wo, qty, rev from tb_WO").Tables[0];

            cb_PN.DataSource = wo.LlenarComboBox("select * from tb_Model");
            cb_PN.DisplayMember = "model";
            cb_PN.ValueMember = "id_model";
            cb_PN.SelectedIndex = -1;
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_WO.Text) && !string.IsNullOrEmpty(txt_Rev.Text) && !string.IsNullOrEmpty(txt_QTY.Text) && cb_PN.SelectedIndex != -1)
            {
                wo.Crud("insert into tb_WO values('" + txt_WO.Text + "','" + txt_QTY.Text + "','" + txt_Rev.Text + "','" + cb_PN.SelectedValue + "')");
                txt_QTY.Text = "";
                txt_Rev.Text = "";
                txt_WO.Text = "";
                MessageBox.Show("DONE!");
                Form2_Load(sender, e);
            }
        }
    }
}
