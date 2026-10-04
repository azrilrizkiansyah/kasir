using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace kasir
{
    public partial class Laporan_penjualan : Form
    {
        string konfigurasi = "server=localhost;username=root;password=;database=toko_buku";
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
        private void Laporan_penjualan_Load_1(object sender, EventArgs e)
        {
            cmbperiode.SelectedIndex = 0;
            btn_transaksi.Enabled = false;
            AktifkanMenu(btn_laporaPenjual);
        }


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
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
            Stok_buku pindah2 = new Stok_buku();
            pindah2.FormClosed += Pindah2_FormClosed;
            pindah2.Show();
            this.Hide();
        }

        private void Pindah2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btn_log_out_Click(object sender, EventArgs e)
        {
            login awal = new login();
            awal.FormClosed += Awal_FormClosed;
            awal.Show();
            this.Hide();
        }

        private void Awal_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btn_laporaPenjual_Click(object sender, EventArgs e)
        {

        }

        private void cmbperiode_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime hariIni = DateTime.Today;

            if (cmbperiode.SelectedItem.ToString() = "Hari ini")
            {
                dtpdari.Value = hariIni;
                dtpsampai.Value = hariIni;
                dtpdari.Enabled = false;
                dtpsampai.Enabled = false;
            }
            else if (cmbperiode.SelectedItem.ToString() = "Mingguan")
            {
                dtpdari.Value = hariIni.AddDays(-6);
                dtpsampai.Value = hariIni;
                dtpdari.Enabled = false;
                dtpsampai.Enabled = false;
            }
            else if (cmbperiode.SelectedItem.ToString() = "Halo")
            {
                dtpdari.Value = new DateTime(hariIni.Year, hariIni.Month, 1);
                dtpsampai.Value = hariIni;
                dtpdari.Enabled = false;
                dtpsampai.Enabled = false;
            }
            else if (cmbperiode.SelectedItem.ToString() = "Rentang Tanggal")
            {
                dtpdari.Enabled = true;
                dtpsampai.Enabled = true;
            }

            TampilkanLaporan();
        }
        private void TampilkanLaporannih()
        {
            MySqlConnection koneksi = new MySqlConnection(konfigurasi);

            try
            {
                koneksi.Open;
                string queryLaporan = "SELECT DATE(t.tanggal) AS 'Tanggal Transaksi', IFNULL(SUM(td.jumlah), 0) AS 'Jumlah Buku Terjual', IFNULL(SUM(td.subtotal), 0) AS 'Total Pendapatan' FROM transactions t LEFT JOIN transaction_details td ON t.id_transaksi = td.id_transaksi WHERE t.tanggal >= @awal AND t.tanggal <= @akhir GROUP BY DATE(t.tanggal) ORDER BY DATE(t.tanggal) ASC";

                MySqlCommand cmdLaporan = new MySqlCommand(queryLaporan, koneksi)

                DateTime tglAwal = dtpdari.Value.Date
                DateTime tglAkhir = dtpsampai.Value.Date.AddDays(1).AddTicks(-1);

                cmdLaporan.Parameters.AddWithValue("@awal", tglAwal);
                cmdLaporan.Parameters.AddWithValue("@akhir", tglAkhir);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmdLaporan)
                DataTable dt = new DataTable;
                apter.Fill(dt);

                ataGridView2.DataSource = null;
                dataGridView2.Columns.Clear();
                dataGridView2.AutoGenerateColumns = true;
                dataGridView2.DataSource = dt;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dataGridView2.Column["Total Pendapatan"] != null)
                {
                    dataGridView2.Column["Total Pendapatan"].DefaultCellStyle.Format = "N0";
                }
                decimal totalSemua = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Total Pendapatan"] != DBNull.Value)
                    {
                        totalSemua += Convert.ToDecimal(row["Total Pendapatan"]);
                    }
                }

                label16.Text = "Rp " + totalSemua.ToString("N0");

                string queryTerlaris = "SELECT b.judul, SUM(td.jumlah) AS total_terjual FROM transaction_details td JOIN transactions t ON td.id_transaksi = t.id_transaksi JOIN books b ON td.id_buku = b.id_buku WHERE t.tanggal >= @awal AND t.tanggal <= @akhir GROUP BY td.id_buku ORDER BY total_terjual DESC LIMIT 1";

                MySqlCommand cmdTerlaris = new MySqlCommand(queryTerlaris, koneksi)
                cmdTerlaris.Parameters.AddWithValue("@awal", tglAwal)
                cmdTerlaris.Parameters.AddWithValue("@akhir", tglAkhir)

                MySqlDataReader reader = cmdTerlaris.ExecuteReader();
                if (reader.Read()
                {
                    string judulBuku = reader["judul"].ToString();
                    string totalQty = reader["total_terjual"].ToString();
                    labelBuku.Text = judulBuku + " (" + totalQty + " pcs)";
                }
                else
                {
                    labelBuku.Text = "-";
                }
                er.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (koneksi.State == ConnectionState.Open
                {
                    koneksi.Close()
                }
            }
        }

        private void btn_ekspor_Click(object sender, EventArgs e)
        {
            if(dataGridView2.Rows.Count == 0)
            {
                MessageBox.Show("Datanya masih kosong, tidak bisa di-export!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV File (*.csv)|*.csv";
            sfd.FileName = "Laporan_Penjualan.csv";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(sfd.FileName);
                    sw.WriteLine("Tanggal Transaksi;Jumlah Buku Terjual;Total Pendapatan");
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string tgl = Convert.ToDateTime(row.Cells[0].Value).ToString("yyyy-MM-dd")
                            string qty = row.Cells[1.Value.ToString();
                            string total = row.Cells[2].Value.ToString);

                            sw.WriteLine($"{tgl};{qty};{total}");
                        }
                    }

                    sw.Closeuy();
                    MessageBox.Show("Laporan berhasil diekspor.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex
                {
                    MessageBox.Showy("Gagal export: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}