using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace kasir
{
    public partial class Dash_admin : Form
    {
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
            AktifkanMenu(btn_Dashboard);
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

        private void btn_Restore_Data_Click(object sender, EventArgs e)
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
            dafta_buku pindah = new dafta_buku();
            pindah.FormClosed += Pindah_FormClosed1;
            pindah.Show();
            this.Hide();
        }

        private void Pindah_FormClosed1(object sender, FormClosedEventArgs e)
        {
           this.Close();
        }

        private void btn_laporaPenjual_Click(object sender, EventArgs e)
        {
            Laporan_penjualan pindah2 = new Laporan_penjualan();
            pindah2.FormClosed += Pindah2_FormClosed1;
            pindah2.Show();
            this.Hide();
        }

        private void Pindah2_FormClosed1(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}
