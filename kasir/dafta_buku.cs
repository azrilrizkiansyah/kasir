using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kasir
{
    public partial class dafta_buku : Form
    {
        public dafta_buku()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dash_admin kembali = new Dash_admin();
            kembali.FormClosed += Kembali_FormClosed;
            kembali.Show();
            this.Hide();
        }

        private void Kembali_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}
