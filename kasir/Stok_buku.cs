using MySql.Data.MySqlClient;
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

namespace kasir
{
    public partial class Stok_buku : Form
    {
        string konfigurasi = "server=localhost;database=toko_buku;username=root;password=;";
        DataTable dtStok = new DataTable();
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
        private void BuatStrukturTabel()
        {
            dtStok.Columns.Clear();
            dtStok.Columns.Add("id_buku", typeof(int));
            dtStok.Columns.Add("Kode Buku", typeof(string));
            dtStok.Columns.Add("Judul Buku", typeof(string));
            dtStok.Columns.Add("Stok Saat Ini", typeof(int));
            dtStok.Columns.Add("Jumlah Buku Baru", typeof(int));

            dataGridView1.DataSource = dtStok;

            // Sembunyikan Primary Key id_buku
            if (dataGridView1.Columns["id_buku"] != null)
            {
                dataGridView1.Columns["id_buku"].Visible = false;
            }

            // Kunci kolom info buku agar tidak bisa diedit acak
            if (dataGridView1.Columns["Kode Buku"] != null) dataGridView1.Columns["Kode Buku"].ReadOnly = true;
            if (dataGridView1.Columns["Judul Buku"] != null) dataGridView1.Columns["Judul Buku"].ReadOnly = true;
            if (dataGridView1.Columns["Stok Saat Ini"] != null) dataGridView1.Columns["Stok Saat Ini"].ReadOnly = true;

            // Kolom Jumlah Buku Baru dapat diedit di tabel atau via panel kanan
            if (dataGridView1.Columns["Jumlah Buku Baru"] != null) dataGridView1.Columns["Jumlah Buku Baru"].ReadOnly = false;

            // Atur lebar kolom penuh tanpa space kosong di kanan
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
            AktifkanMenu(btn_manajemenStok);
            BuatStrukturTabel();
            btn_transaksi.Enabled = false;
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
            if (dataGridView1.CurrentRow == null || dtStok.Rows.Count == 0)
            {
                MessageBox.Show("Pilih baris buku di dalam tabel terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int inputJumlah;
            bool isAngka = int.TryParse(txt_buku_baru.Text.Trim(), out inputJumlah);

            if (!isAngka || inputJumlah <= 0)
            {
                MessageBox.Show("Jumlah buku baru harus valid untuk semua buku.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Set nilai ke kolom Jumlah Buku Baru di DataGridView pada baris yang dipilih
            dataGridView1.CurrentRow.Cells["Jumlah Buku Baru"].Value = inputJumlah;
            txt_buku_baru.Clear();
            SimpanPerubahanStok();
        }

        // 4. Proses Simpan & Perubahan Stok ke Database (Poin 3, 4, 5, 6)
        private void SimpanPerubahanStok()
        {
            if (dtStok.Rows.Count == 0)
            {
                MessageBox.Show("Tabel masih kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Poin 3: Validasi Input untuk seluruh baris tabel
            foreach (DataRow row in dtStok.Rows)
            {
                if (row["Jumlah Buku Baru"] == DBNull.Value)
                {
                    MessageBox.Show("Jumlah buku baru harus valid untuk semua buku.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int val;
                bool valid = int.TryParse(row["Jumlah Buku Baru"].ToString(), out val);

                if (!valid || val <= 0)
                {
                    MessageBox.Show("Jumlah buku baru harus valid untuk semua buku.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Poin 4 & 5: Proses Perubahan Stok dan Simpan ke Database MySQL
            MySqlConnection koneksi = new MySqlConnection(konfigurasi);

            try
            {
                koneksi.Open();

                foreach (DataRow row in dtStok.Rows)
                {
                    int idBuku = Convert.ToInt32(row["id_buku"]);
                    int jumlahBaru = Convert.ToInt32(row["Jumlah Buku Baru"]);

                    string queryUpdate = "UPDATE books SET stok = stok + @jumlahBaru WHERE id_buku = @idBuku";

                    MySqlCommand cmdUpdate = new MySqlCommand(queryUpdate, koneksi);
                    cmdUpdate.Parameters.AddWithValue("@jumlahBaru", jumlahBaru);
                    cmdUpdate.Parameters.AddWithValue("@idBuku", idBuku);

                    cmdUpdate.ExecuteNonQuery();
                }

                // Poin 6: Pesan Konfirmasi Sukses
                MessageBox.Show("Stok berhasil diperbarui untuk semua buku.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Mengosongkan tabel kembali
                dtStok.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memperbarui stok: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (koneksi.State == ConnectionState.Open)
                {
                    koneksi.Close();
                }
            }
        }

        private void btn_cari_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_cari.Text))
            {
                MessageBox.Show("Masukkan Kode Buku atau Judul Buku terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MySqlConnection koneksi = new MySqlConnection(konfigurasi);

            try
            {
                koneksi.Open();

                string query = "SELECT id_buku, kode_buku, judul, stok FROM books WHERE kode_buku = @keyword OR judul LIKE @keywordLike LIMIT 1";

                MySqlCommand cmd = new MySqlCommand(query, koneksi);
                cmd.Parameters.AddWithValue("@keyword", txt_cari.Text.Trim());
                cmd.Parameters.AddWithValue("@keywordLike", "%" + txt_cari.Text.Trim() + "%");

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int idBuku = Convert.ToInt32(reader["id_buku"]);
                    string kode = reader["kode_buku"].ToString();
                    string judul = reader["judul"].ToString();
                    int stokSaatIni = Convert.ToInt32(reader["stok"]);

                    // Cek apakah buku sudah ada di dalam tabel agar tidak terduplikasi
                    bool sudahAda = false;
                    foreach (DataRow row in dtStok.Rows)
                    {
                        if (Convert.ToInt32(row["id_buku"]) == idBuku)
                        {
                            sudahAda = true;
                            break;
                        }
                    }

                    if (sudahAda)
                    {
                        MessageBox.Show("Buku ini sudah ada di dalam tabel!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Tambahkan ke tabel dengan nilai awal Jumlah Buku Baru = 0
                        dtStok.Rows.Add(idBuku, kode, judul, stokSaatIni, 0);
                        txt_cari.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Buku tidak ditemukan!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (koneksi.State == ConnectionState.Open)
                {
                    koneksi.Close();
                }
            }
        }

        private void btn_batal_Click(object sender, EventArgs e)
        {
            txt_cari.Clear();
            txt_buku_baru.Clear();
            dtStok.Rows.Clear();
        }

        private void btn_log_out_Click(object sender, EventArgs e)
        {

        }
    }
}
