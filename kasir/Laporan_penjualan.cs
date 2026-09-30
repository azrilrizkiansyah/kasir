using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace kasir
{
    public partial class Laporan_penjualan : Form
    {
        public Laporan_penjualan()
        {
            InitializeComponent();
        }

        // halo
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


        private List<DataPenjualan> dataPenjualan = new List<DataPenjualan>()
        {
            new DataPenjualan
            {
                Tanggal = new DateTime(2026, 9, 1),
                NoTransaksi = "TRX001",
                Buku = "Laskar Pelangi",
                Jumlah = 3,
                Harga = 75000
            },

            new DataPenjualan
            {
                Tanggal = new DateTime(2026, 9, 5),
                NoTransaksi = "TRX002",
                Buku = "Bumi",
                Jumlah = 2,
                Harga = 85000
            },

            new DataPenjualan
            {
                Tanggal = new DateTime(2026, 9, 10),
                NoTransaksi = "TRX003",
                Buku = "Negeri 5 Menara",
                Jumlah = 4,
                Harga = 90000
            },

            new DataPenjualan
            {
                Tanggal = new DateTime(2026, 9, 15),
                NoTransaksi = "TRX004",
                Buku = "Dilan 1990",
                Jumlah = 5,
                Harga = 80000
            },

            new DataPenjualan
            {
                Tanggal = new DateTime(2026, 9, 20),
                NoTransaksi = "TRX005",
                Buku = "Atomic Habits",
                Jumlah = 3,
                Harga = 120000
            },

            new DataPenjualan
            {
                Tanggal = new DateTime(2026, 9, 25),
                NoTransaksi = "TRX006",
                Buku = "Filosofi Teras",
                Jumlah = 2,
                Harga = 95000
            },

            new DataPenjualan
            {
                Tanggal = new DateTime(2026, 9, 28),
                NoTransaksi = "TRX007",
                Buku = "Bumi Manusia",
                Jumlah = 4,
                Harga = 100000
            }
        };


        public Laporan_penjualan()
        {
            InitializeComponent();
            TampilkanLaporan();
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

        private void Laporan_penjualan_Load(object sender, EventArgs e)
        {
            btn_transaksi.Enabled = false;
            AktifkanMenu(btn_laporaPenjual);
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

        private void btn_manajemenStok_Click(object sender, EventArgs e)
        {
            Stok_buku pindah3 = new Stok_buku();
            pindah3.FormClosed += Pindah3_FormClosed;
            pindah3.Show();
            this.Hide();
        }

        private void Pindah3_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}