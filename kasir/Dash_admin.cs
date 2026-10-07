using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Util;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace kasir
{
    public partial class Dash_admin : Form
    {
        string konfigurasi = "server=localhost;username=root;password=;database=toko_buku;";
        public Dash_admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {
            Laporan_penjualan pindah2 = new Laporan_penjualan();
            pindah2.FormClosed += Pindah2_FormClosed;
            pindah2.Show();
            this.Hide();
        }

        private void Pindah2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void Dash_admin_Load(object sender, EventArgs e)
        {
            btn_transaksi.Enabled = false;
            AktifkanMenu(button16);

            MySqlConnection koneksi = new MySqlConnection(konfigurasi);
            try 
            {
                koneksi.Open();
                string query = "select * from transaction_details;";
                MySqlCommand eksekusi = new MySqlCommand(query, koneksi);
                MySqlDataReader cmd = eksekusi.ExecuteReader();
                while (cmd.Read())
                {
                    dataGridView1.Rows.Add(cmd.GetInt32("id_detail").ToString(),cmd.GetInt32("id_transaksi").ToString(),cmd.GetInt32("id_buku").ToString(),cmd.GetInt32("jumlah").ToString(),cmd.GetDecimal("subtotal").ToString());
                }
                  


            } catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            koneksi.Close();
            
            UpdateDashboardPenjualan();
            TampilkanTotalSemuaTransaksi();
            tampilkanunitbuku();
            tampilkanStok();


        }

        private void tampilkanStok()
        {

            MySqlConnection koneksi = new MySqlConnection(konfigurasi);
            try
            {
                koneksi.Open();
                string query = "SELECT SUM(stok) FROM books";
                MySqlCommand cmd = new MySqlCommand(query, koneksi);
                int totalStok = System.Convert.ToInt32(cmd.ExecuteScalar());
                label1.Text = totalStok > 0 ? totalStok + " " : "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void tampilkanunitbuku()
        {
            MySqlConnection koneksi = new MySqlConnection(konfigurasi);
            try 
            { 
                koneksi .Open();
                string query = "SELECT SUM(jumlah) FROM transaction_details";
                MySqlCommand cmd = new MySqlCommand(query, koneksi);
                int totalUnit = System.Convert.ToInt32(cmd.ExecuteScalar());
                label14.Text = totalUnit > 0 ? totalUnit + " " : "";

                if(totalUnit > 0)
                {
                    label13.Text = totalUnit.ToString() + " unit buku terjual";
                    label13.ForeColor = Color.ForestGreen;
                }
                else
                {
                    label13.Text = "Belum ada buku terjual";
                    label13.ForeColor = Color.Gray;
                }
            } 
            catch ( Exception ex) 
            {
                MessageBox.Show("Gagal menampilkan data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void TampilkanTotalSemuaTransaksi()
        {
            MySqlConnection koneksi = new MySqlConnection(konfigurasi);
            try
            {
                koneksi.Open();
                string query = "SELECT COUNT(id_transaksi) FROM transactions";
                MySqlCommand cmd = new MySqlCommand(query, koneksi);

                int total = System.Convert.ToInt32(cmd.ExecuteScalar());
                label11.Text = total > 0 ? total + " " : "";

                if(total > 0)
                {
                    label10.Text =total.ToString() + " transaksi tercatat";
                    label10.ForeColor = Color.ForestGreen;
                }
                else
                {
                    label10.Text = "Belum ada transaksi";
                    label10.ForeColor = Color.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                koneksi.Close();
            }
        }
        private void UpdateDashboardPenjualan()
        {
            // Deklarasi variabel di luar try agar bisa diakses di blok finally
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;
            MySqlConnection koneksi = new MySqlConnection(konfigurasi);
            try
            {
               
                koneksi.Open();
                // Query untuk mengambil total nominal dan total jumlah transaksi
                string query = @"SELECT COALESCE(SUM(total_harga), 0) AS total_nominal,COUNT(id_transaksi) AS total_jumlah FROM transactions WHERE DATE(tanggal) = CURDATE()";
                MySqlCommand kemando = new MySqlCommand(query, koneksi);
                MySqlDataReader pembaca = kemando.ExecuteReader();

                if (pembaca.Read())
                {
                    decimal totalNominal = pembaca.GetDecimal("total_nominal");
                    int totalJumlah = pembaca.GetInt32("total_jumlah");

                   
                    label8.Text = totalNominal.ToString("C0", new System.Globalization.CultureInfo("id-ID"));

                   
                    if (totalJumlah > 0)
                    {
                        label9.Text = totalJumlah.ToString() + " Transaksi hari ini";
                        label9.ForeColor = System.Drawing.Color.ForestGreen;
                    }
                    else
                    {
                        label9.Text = "Belum ada transaksi hari ini";
                        label9.ForeColor = System.Drawing.Color.Gray;
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Gagal memuat data: " + ex.Message);
            }
            finally
            {
               
                if (reader != null) reader.Close();
                if (cmd != null) cmd.Dispose();
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        private void btn_tambah_buku_Click(object sender, EventArgs e)
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

        private void Btn_BackupData_Click(object sender, EventArgs e)
        {
           
        }

        private void btn_Restore_Data_Click(object sender, EventArgs e)
        {
           
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

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

        private void button16_Click(object sender, EventArgs e)
        {

        }

        private void btn_tambah_buku_Click_1(object sender, EventArgs e)
        {
            
        }

        private void Pindah_FormClosed1(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btn_laporaPenjual_Click(object sender, EventArgs e)
        {
          
        }

        private void Pindah2_FormClosed1(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btn_manajemenStok_Click_1(object sender, EventArgs e)
        {
            Stok_buku pindah3 = new Stok_buku();
            pindah3.FormClosed += Pindah3_FormClosed1;
            pindah3.Show();
            this.Hide();
        }

        private void Pindah3_FormClosed1(object sender, FormClosedEventArgs e)
        {
           
            this.Close();
        
        }

        private void Btn_BackupData_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "SQL Files (*.sql)|*.sql";
            saveFileDialog.FileName = $"Backup_Database_{DateTime.Now:yyyyMMdd_HHmmss}.sql";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Sesuaikan dengan connection string kamu
                string connString = "server=localhost;user=root;password=;database=toko_buku;";

                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            try
                            {
                                cmd.Connection = conn;
                                conn.Open();

                                // Ekspor data ke file SQL
                                mb.ExportToFile(saveFileDialog.FileName);

                                MessageBox.Show("Backup data berhasil disimpan!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Gagal melakukan backup data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void btn_log_out_Click(object sender, EventArgs e)
        {
            login kembali = new login();
            kembali.FormClosed += Kembali_FormClosed1;
            kembali.Show();
            this.Hide();
        }

        private void Kembali_FormClosed1(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btn_tambah_buku_Click_2(object sender, EventArgs e)
        {
            dafta_buku pindah = new dafta_buku();
            pindah.FormClosed += Pindah_FormClosed1;
            pindah.Show();
            this.Hide();
        }

        private void btn_laporaPenjual_Click_1(object sender, EventArgs e)
        {
            Laporan_penjualan pindah2 = new Laporan_penjualan();
            pindah2.FormClosed += Pindah2_FormClosed1;
            pindah2.Show();
            this.Hide();
        }

        private void btn_Restore_Data_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "SQL Files (*.sql)|*.sql";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Path ke mysql.exe XAMPP
                    string mysqlPath = @"C:\xampp\mysql\bin\mysql.exe";

                    string cmdText = $"-u root nama_database_kamu -e \"source {openFileDialog.FileName.Replace(@"\", "/")}\"";

                    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                    psi.FileName = mysqlPath;
                    psi.Arguments = cmdText;
                    psi.RedirectStandardOutput = true;
                    psi.UseShellExecute = false;
                    psi.CreateNoWindow = true;

                    using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(psi))
                    {
                        process.WaitForExit();
                    }

                    MessageBox.Show("Data berhasil dipulihkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memulihkan data. Error: " + ex.Message, "Error Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
           
        }

        private void Kembalii_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fitur ini belum tersedia. Silakan login atau hubungi admin.", "Informasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (MessageBox.Show("Apakah Anda ingin login kembali?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                login kembali = new login();
                kembali.FormClosed += Kembali_FormClosed1;
                kembali.Show();
                this.Hide();
            }
        }
    }
    
}
