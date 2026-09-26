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
        private Button tombolAktif = null;

        private void AktifkanMenu(Button tombolPilihan)
        {
            // Jika ada tombol yang sebelumnya aktif, kembalikan warnanya ke normal
            if (tombolAktif != null)
            {
                tombolAktif.BackColor = Color.FromArgb(15, 23, 42); // Warna normal sidebar
            }

            // Set tombol yang baru dipilih menjadi tombol aktif
            tombolAktif = tombolPilihan;

            // Berikan warna khusus untuk menandakan menu sedang aktif (misalnya warna biru terang / sedikit lebih terang)
            tombolAktif.BackColor = Color.FromArgb(37, 99, 235);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            AktifkanMenu(btn_manajemenBuku);
            btn_transaksi.Enabled = false;
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
            Dash_admin kembali = new Dash_admin();
            kembali.FormClosed += Kembali_FormClosed;
            kembali.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_manajemenBuku_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            Dash_admin kembali = new Dash_admin();
            kembali.FormClosed += Kembali_FormClosed1;
            kembali.Show();
            this.Hide();
        }

        private void Kembali_FormClosed1(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}
