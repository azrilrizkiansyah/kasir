using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace kasir
{
    public partial class Laporan_penjualan : Form
    {

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

        private void button16_Click(object sender, EventArgs e)
        {
            TampilkanLaporan();
        }

        private void TampilkanLaporan()
        {

            DateTime tanggalMulai = dateTimePicker1.Value.Date;
            DateTime tanggalSelesai = dateTimePicker2.Value.Date;

            if (tanggalMulai > tanggalSelesai)
            {
                MessageBox.Show(
                    "Tanggal mulai tidak boleh lebih besar dari tanggal selesai.",
                    "Peringatan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            var hasil = dataPenjualan
                .Where(x =>
                    x.Tanggal.Date >= tanggalMulai &&
                    x.Tanggal.Date <= tanggalSelesai)
                .ToList();

            decimal totalPendapatan = hasil.Sum(
                x => x.Jumlah * x.Harga
            );

            int jumlahTransaksi = hasil.Count;
            int totalBukuTerjual = hasil.Sum(
                x => x.Jumlah
            );

            panel4.Text =
                "Rp " + totalPendapatan.ToString("N0");

            panel5.Text =
                jumlahTransaksi.ToString();

            panel6.Text =
                totalBukuTerjual.ToString();

            dataGridView1.Rows.Clear();


            foreach (var item in hasil)
            {
                decimal subtotal =
                    item.Jumlah * item.Harga;

                dataGridView1.Rows.Add(
                    item.Tanggal.ToString("dd/MM/yyyy"),
                    item.NoTransaksi,
                    item.Buku,
                    item.Jumlah,
                    "Rp " + item.Harga.ToString("N0"),
                    "Rp " + subtotal.ToString("N0")
                );
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_transaksi_Click(object sender, EventArgs e)
        {
            Transaksi_penjualan form = new Transaksi_penjualan();
            form.Show();
            this.Hide();
        }

        private void btn_laporaPenjual_Click(object sender, EventArgs e)
        {
            Laporan_penjualan form = new Laporan_penjualan();
            form.Show();
            this.Hide();
        }

        private void btn_manajemenStok_Click(object sender, EventArgs e)
        {
            Stok_buku form = new Stok_buku();
            form.Show();
            this.Hide();
        }

        private void Btn_BackupData_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_Restore_Data_Click(object sender, EventArgs e)
        {
            

        }

        private void btn_log_out_Click(object sender, EventArgs e)
        {
            DialogResult hasil = MessageBox.Show(
        "Apakah Anda yakin ingin keluar?",
        "Konfirmasi",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
     );

            if (hasil == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Laporan_penjualan_Load(object sender, EventArgs e)
        {

        }
    }


    public class DataPenjualan
    {
        public DateTime Tanggal { get; set; }

        public string NoTransaksi { get; set; }

        public string Buku { get; set; }

        public int Jumlah { get; set; }

        public decimal Harga { get; set; }
    }
}