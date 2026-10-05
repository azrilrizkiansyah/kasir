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
    public partial class Stok_buku : Form
    {
        public Stok_buku()
        {
            InitializeComponent();
        }
        private Button tombolAktif = null;

        private void AktifkanMenu(Button tombolPilihan)
        {

            if (tombolAktif != null)
            {
                tombolAktif.BackColor = Color.FromArgb(15, 23, 42);
            }
            tombolAktif = tombolPilihan;
            tombolAktif.BackColor = Color.FromArgb(37, 99, 235);
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void Kembali_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void Stok_buku_Load(object sender, EventArgs e)
        {
            btn_transaksi.Enabled = false;
            AktifkanMenu(btn_manajemenStok);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Dash_admin kembali = new Dash_admin();
            kembali.FormClosed += Kembali_FormClosed;
            kembali.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_manajemenBuku_Click(object sender, EventArgs e)
        {
            dafta_buku pindah = new dafta_buku();
            pindah.FormClosed += Pindah_FormClosed;
            pindah.Show();
            this.Hide();
        }

        private void Pindah_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btn_laporaPenjual_Click(object sender, EventArgs e)
        {
            Laporan_penjualan pindah = new Laporan_penjualan();
            pindah.FormClosed += Pindah_FormClosed1;
            pindah.Show();
            this.Hide();
        }

        private void Pindah_FormClosed1(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
