
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GIT
{
    public partial class Form1 : Form
    {
        Boolean load;

        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (load)
                {
                    SinhVien sv = new SinhVien(dataGridView1.CurrentRow.Cells["MaSV"].Value.ToString());
                    if (e.ColumnIndex == 1) sv.HoDem = dataGridView1.CurrentRow.Cells["HoDem"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = connectionSQL.GetDataTable("LayDSSinhVien", new object[] { "" });
            dataGridView1.DataSource = dt;
            load = true;
        }
    }
}
