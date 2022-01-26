using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IQProject
{
    public partial class Mantenimiento : Form
    {
        Assembly assembly = new Assembly();
        Model model = new Model();
        public Mantenimiento()
        {
            InitializeComponent();
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                assembly.Crud("insert into tb_Model values('" + txt_model.Text + "')");
                txt_model.Text = "";
            }
            catch (Exception)
            {

                MessageBox.Show("Campos vacion!", "ERROR!");
            }
            Mantenimiento_Load(sender,e);
        }

        private void Mantenimiento_Load(object sender, EventArgs e)
        {
            try
            {
                cb_eliminar.DataSource = assembly.LlenarComboBox("select * from tb_Model");
                cb_eliminar.DisplayMember = "model";
                cb_eliminar.ValueMember = "id_model";
                cb_eliminar.SelectedItem = null;

                cb_modificar.DataSource = assembly.LlenarComboBox("select * from tb_Model");
                cb_modificar.DisplayMember = "model";
                cb_modificar.ValueMember = "id_model";
                cb_modificar.SelectedItem = null;

                dataGridView1.DataSource = assembly.LlenarDG("select * from tb_Model").Tables[0];
            }
            catch (Exception)
            {

                MessageBox.Show("No database found!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cb_modificar_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_modificar.Text = this.cb_modificar.GetItemText(this.cb_modificar.SelectedItem);
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                model.Crud("delete tb_Model where id_model = " + cb_eliminar.SelectedValue);

            }
            catch (Exception)
            {

                MessageBox.Show("No deje campos vacios!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Mantenimiento_Load(sender, e);
        }

        private void btn_modificar_Click(object sender, EventArgs e)
        {
            try
            {
                model.Crud("update tb_Model set model = '" + txt_modificar.Text + "' where id_model = " + cb_modificar.SelectedValue);
            }
            catch (Exception)
            {

                MessageBox.Show("No deje campos vacios!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Mantenimiento_Load(sender, e);
        }
    }
}
