using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MySql.Data.MySqlClient;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace kasir
{
    public partial class dash_kasir : Form
    {
        string konfigurasi = "server=localhost;username=root;password=;database=toko_buku;";
        public dash_kasir()
        {
            InitializeComponent();
        }

        private void dash_kasir_Load(object sender, EventArgs e)
        {
            btn_laporaPenjual.Enabled = false;
            btn_manajemenStok.Enabled = true;
            btn_Restore_Data.Enabled = false;
            btn_tambah_buku.Enabled = false;
            btn_transaksi.Enabled = true;
            Btn_BackupData.Enabled = false;
            TampilkanChartStokBuku();


        }
        private void TampilkanChartStokBuku()
        {
           
        }

        private void btn_sig_out_Click(object sender, EventArgs e)
        {
            login kembali = new login();
            kembali.FormClosed += Kembali_FormClosed;
            kembali.Show();
            this.Hide();
        }

        private void Kembali_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btn_transaksi_Click(object sender, EventArgs e)
        {
            tran_penjulan pindah = new tran_penjulan();
            pindah.FormClosed += Pindah_FormClosed;
            pindah.Show();
            this.Hide();
        }

        private void Pindah_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Kembali1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btn_manajemenStok_Click(object sender, EventArgs e)
        {
            kasir_stok pindah = new kasir_stok();
            pindah.FormClosed += Pindah_FormClosed1;
            pindah.Show();
            this.Hide();
        }

        private void Pindah_FormClosed1(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
