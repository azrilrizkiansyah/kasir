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
            
        }

        private void Kembali_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btn_log_out_Click(object sender, EventArgs e)
        {
            Dash_admin kembali2 = new Dash_admin();
            kembali2.FormClosed += Kembali2_FormClosed;
            kembali2.Show();
            this.Hide();
        }

        private void Kembali2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btn_laporaPenjual_Click(object sender, EventArgs e)
        {
            Laporan_penjualan pindah = new Laporan_penjualan();
            pindah.FormClosed += Pindah_FormClosed;
            pindah.Show();
            this.Hide();
        }

        private void Pindah_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btn_manajemenStok_Click(object sender, EventArgs e)
        {
            Stok_buku pindah = new Stok_buku();
            pindah.FormClosed += Pindah_FormClosed1;
            pindah.Show();
            this.Hide();
        }

        private void Pindah_FormClosed1(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
