using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace kasir
{
    
    public partial class tran_penjulan : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);
        string konfigurasi = "server=localhost;username=root;password=;database=toko_buku";
        private DataTable dtKeranjang = new DataTable();
        private string selectedKodeBuku = "";
        private string selectedJudulBuku = "";
        private decimal selectedHargaBuku = 0;
        private int selectedStokBuku = 0;
        private int selectedIdBuku = 0;
        private DataTable dtBuku;

        private void MuatDaftarBuku2()
        {
            using (MySqlConnection koneksi = new MySqlConnection(konfigurasi))
            {
                koneksi.Open();
                string query = "SELECT id_buku, judul, harga, stok FROM books WHERE stok > 0";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, koneksi);

                dtBuku = new DataTable();
                adapter.Fill(dtBuku); // Seluruh data buku dari database disalin ke dtBuku

                // Tampilkan ke ListBox
                Listbuku.DataSource = dtBuku;
                Listbuku.DisplayMember = "judul";
                Listbuku.ValueMember = "id_buku";
            }
        }
        public tran_penjulan()
        {
            InitializeComponent();
            InitKeranjang();
        }
        private void InitKeranjang()
        {
            dtKeranjang.Columns.Clear();

           
            dtKeranjang.Columns.Add("Judul", typeof(string));     
            dtKeranjang.Columns.Add("Harga", typeof(decimal));   
            dtKeranjang.Columns.Add("Jumlah", typeof(int));       
            dtKeranjang.Columns.Add("Subtotal", typeof(decimal)); 
            dtKeranjang.Columns.Add("KodeBuku", typeof(string));  
            dtKeranjang.Columns.Add("IdBuku", typeof(int));      

            dataGridView1.DataSource = dtKeranjang;

            
            if (dataGridView1.Columns["KodeBuku"] != null) dataGridView1.Columns["KodeBuku"].Visible = false;
            if (dataGridView1.Columns["IdBuku"] != null) dataGridView1.Columns["IdBuku"].Visible = false;
        }

        private void Listbuku_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            
        }
        

        private void tran_penjulan_Load(object sender, EventArgs e)
        {
            btn_tambah_buku.Enabled = false;
            btn_laporaPenjual.Enabled = false;
            Btn_BackupData.Enabled = false;
            btn_Restore_Data.Enabled = false;
            AktifkanMenu(btn_transaksi);
            SendMessage(textBox1.Handle, 0x1501, (IntPtr)1, "Cari buku disini....");
            MySqlConnection koneksi = new MySqlConnection(konfigurasi);
            MySqlDataReader hasil = null;
            string sql = "SELECT kode_buku, judul FROM books ORDER BY judul ASC";

            try
            {
                koneksi.Open();
                MySqlCommand komando = new MySqlCommand(sql, koneksi);
                hasil = komando.ExecuteReader();

                Listbuku.Items.Clear();

                while (hasil.Read())
                {
                    string itemTeks = $"{hasil["kode_buku"]} - {hasil["judul"]}";
                    Listbuku.Items.Add(itemTeks);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat daftar buku: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (hasil != null && !hasil.IsClosed) hasil.Close();
                if (koneksi != null && koneksi.State == ConnectionState.Open) koneksi.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Kembali_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void cyberButton1_Click(object sender, EventArgs e)
        {
           
        }
        private void MuatDaftarBuku()
        {
            MySqlConnection koneksi = new MySqlConnection(konfigurasi);
            MySqlDataReader hasil = null;
            string sql = "SELECT kode_buku, judul FROM books ORDER BY judul ASC";

            try
            {
                koneksi.Open();
                MySqlCommand komando = new MySqlCommand(sql, koneksi);
                hasil = komando.ExecuteReader();

                Listbuku.Items.Clear(); 

                while (hasil.Read())
                {
                    string itemTeks = $"{hasil["kode_buku"]} - {hasil["judul"]}";
                    Listbuku.Items.Add(itemTeks);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat daftar buku: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (hasil != null && !hasil.IsClosed) hasil.Close();
                if (koneksi != null && koneksi.State == ConnectionState.Open) koneksi.Close();
            }
        }

        private void cyberButton2_Click(object sender, EventArgs e)
        {
           
        }
        private decimal HitungTotalBayar()
        {
            decimal grandTotal = 0;
            foreach (DataRow row in dtKeranjang.Rows)
            {
                if (row["Subtotal"] != DBNull.Value)
                {
                    grandTotal += Convert.ToDecimal(row["Subtotal"]);
                }
            }
            return grandTotal;
        }

        private string BuatTeksStruk(long idTransaksi, decimal total, decimal bayar, decimal kembali)
        {
            var sb = new StringBuilder();
            int width = 40;
            Func<string, string> center = s => s.PadLeft((width + s.Length) / 2).PadRight(width);

            sb.AppendLine("========================================");
            sb.AppendLine(center("TOKO BUKU"));
            sb.AppendLine("========================================");
            sb.AppendLine($"No: {idTransaksi}\nTanggal: {DateTime.Now:yyyy-MM-dd HH:mm}");
            sb.AppendLine("----------------------------------------");
            sb.AppendLine(string.Format("{0,-6}{1,-18}{2,3}{3,9}", "Kode", "Judul", "Jml", "Subtotal"));

            foreach (DataRow row in dataGridView1.Rows)
            {
                string judul = row["judul"].ToString();
                judul = judul.Length > 15 ? judul.Substring(0, 15) + ".." : judul;
                sb.AppendLine(string.Format("{0,-6}{1,-18}{2,3} {3,9:N0}", row["kode_buku"], judul, row["jumlah"], row["subtotal"]));
            }

            sb.AppendLine("----------------------------------------");
            sb.AppendLine(string.Format("{0,-30}{1,10:N0}", "TOTAL:", total));
            sb.AppendLine(string.Format("{0,-30}{1,10:N0}", "BAYAR:", bayar));
            sb.AppendLine(string.Format("{0,-30}{1,10:N0}", "KEMBALIAN:", kembali));
            sb.AppendLine("========================================");
            sb.AppendLine(center("TERIMA KASIH"));

            return sb.ToString();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
          
        }
        // Variabel untuk menyimpan tombol yang sedang aktif saat ini
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

        private void btn_transaksi_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_dashboard_Click(object sender, EventArgs e)
        {

            dash_kasir kembali = new dash_kasir();
            kembali.FormClosed += Kembali_FormClosed;
            kembali.Show();
            this.Hide();
        }

        private void Listbuku_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (Listbuku.SelectedItem == null) return;

            string itemSelected = Listbuku.SelectedItem.ToString();
            string kodeBuku = itemSelected.Split('-')[0].Trim();

            MySqlConnection koneksi = new MySqlConnection(konfigurasi);
            MySqlDataReader hasil = null;
            string sql = "SELECT id_buku, kode_buku, judul, harga, stok FROM books WHERE kode_buku = @kode";

            try
            {
                koneksi.Open();
                MySqlCommand komando = new MySqlCommand(sql, koneksi);
                komando.Parameters.AddWithValue("@kode", kodeBuku);

                hasil = komando.ExecuteReader();

                if (hasil.Read())
                {
                    selectedIdBuku = Convert.ToInt32(hasil["id_buku"]);
                    selectedKodeBuku = hasil["kode_buku"].ToString();
                    selectedJudulBuku = hasil["judul"].ToString();
                    selectedHargaBuku = Convert.ToDecimal(hasil["harga"]);
                    selectedStokBuku = Convert.ToInt32(hasil["stok"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengambil detail buku: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (hasil != null && !hasil.IsClosed) hasil.Close();
                if (koneksi != null && koneksi.State == ConnectionState.Open) koneksi.Close();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedKodeBuku))
            {
                MessageBox.Show("Pilih buku dari ListBox terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int jumlahBeli = (int)numericUpDown1.Value;

            if (jumlahBeli <= 0)
            {
                MessageBox.Show("Masukkan jumlah beli yang valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedStokBuku < jumlahBeli)
            {
                MessageBox.Show($"Stok tidak cukup! Stok tersedia: {selectedStokBuku}", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal subtotal = selectedHargaBuku * jumlahBeli;
            dtKeranjang.Rows.Add(selectedJudulBuku, selectedHargaBuku, jumlahBeli, subtotal, selectedKodeBuku, selectedIdBuku);

            numericUpDown1.Value = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Pastikan ada baris di DataGridView Keranjang yang dipilih oleh kasir
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Ambil indeks baris yang sedang dipilih
                int indexBaris = dataGridView1.SelectedRows[0].Index;

                // Hapus baris tersebut dari DataTable keranjang (dtKeranjang)
                dtKeranjang.Rows.RemoveAt(indexBaris);

                // Hitung ulang total bayar keseluruhan setelah item dihapus
                HitungTotalBayar();

                MessageBox.Show("Item berhasil dihapus dari keranjang.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Pilih baris barang di tabel keranjang yang ingin dihapus terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            

        }

        private void btn_manajemenStok_Click(object sender, EventArgs e)
        {
            kasir_stok kembali = new kasir_stok();
            kembali.FormClosed += Kembali_FormClosed1;
            kembali.Show();
            this.Hide();
        }

        private void Kembali_FormClosed1(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btn_cari_Click(object sender, EventArgs e)
        {
            // test
        }

        private void btn_log_out_Click(object sender, EventArgs e)
        {
            login kembali = new login();
            kembali.FormClosed += Kembali_FormClosed2;
            kembali.Show();
            this.Hide();
        }

        private void Kembali_FormClosed2(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

       

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_bayar_Click(object sender, EventArgs e)
        {
            if (dtKeranjang.Rows.Count == 0)
            {
                MessageBox.Show("Keranjang belanja masih kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal totalBayar = HitungTotalBayar();

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Uang pembayaran belum diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }

            decimal uangBayar;
            if (!decimal.TryParse(textBox2.Text, out uangBayar) || uangBayar < totalBayar)
            {
                MessageBox.Show($"Uang pembayaran kurang/tidak valid!\n\nTotal: Rp {totalBayar:N0}", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }

            decimal kembalian = uangBayar - totalBayar;

            MySqlConnection koneksi = new MySqlConnection(konfigurasi);
            MySqlTransaction transaksi = null;

            try
            {
                koneksi.Open();
                transaksi = koneksi.BeginTransaction();

                // A. Insert Header Transaksi (Tabel: transactions)
                string sqlPenjualan = "INSERT INTO transactions (tanggal, total_harga, id_user) VALUES (NOW(), @total, @idUser); SELECT LAST_INSERT_ID();";
                MySqlCommand cmdPenjualan = new MySqlCommand(sqlPenjualan, koneksi, transaksi);
                cmdPenjualan.Parameters.AddWithValue("@total", totalBayar);
                cmdPenjualan.Parameters.AddWithValue("@idUser", 2);

                long idTransaksi = Convert.ToInt64(cmdPenjualan.ExecuteScalar());

                // B. Insert Detail Transaksi (Tabel: transaction_details) & Update Stok (Tabel: books)
                foreach (DataRow row in dtKeranjang.Rows)
                {
                    int idBuku = Convert.ToInt32(row["IdBuku"]);
                    int jumlah = Convert.ToInt32(row["Jumlah"]);
                    decimal subtotal = Convert.ToDecimal(row["Subtotal"]);

                    // Detail
                    string sqlDetail = "INSERT INTO transaction_details (id_transaksi, id_buku, jumlah, subtotal) VALUES (@idTrx, @idBuku, @jumlah, @subtotal)";
                    MySqlCommand cmdDetail = new MySqlCommand(sqlDetail, koneksi, transaksi);
                    cmdDetail.Parameters.AddWithValue("@idTrx", idTransaksi);
                    cmdDetail.Parameters.AddWithValue("@idBuku", idBuku);
                    cmdDetail.Parameters.AddWithValue("@jumlah", jumlah);
                    cmdDetail.Parameters.AddWithValue("@subtotal", subtotal);
                    cmdDetail.ExecuteNonQuery();

                    // Update Stok
                    string sqlUpdateStok = "UPDATE books SET stok = stok - @jumlah WHERE id_buku = @idBuku";
                    MySqlCommand cmdStok = new MySqlCommand(sqlUpdateStok, koneksi, transaksi);
                    cmdStok.Parameters.AddWithValue("@jumlah", jumlah);
                    cmdStok.Parameters.AddWithValue("@idBuku", idBuku);
                    cmdStok.ExecuteNonQuery();
                }

                transaksi.Commit();

                // C. Buat Teks Struk
                string teksStruk = BuatTeksStruk(idTransaksi, totalBayar, uangBayar, kembalian);

                // D. Buka Form Struk Terpisah
                struk formStruk = new struk(teksStruk);
                formStruk.ShowDialog();

                // E. Reset Form Transaksi
                dtKeranjang.Clear();
                textBox2.Clear();
                selectedKodeBuku = "";
                HitungTotalBayar();
                MuatDaftarBuku();
            }
            catch (Exception ex)
            {
                if (transaksi != null) transaksi.Rollback();
                MessageBox.Show("Transaksi gagal disimpan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (koneksi != null && koneksi.State == ConnectionState.Open) koneksi.Close();
            }
        }
    }
}
