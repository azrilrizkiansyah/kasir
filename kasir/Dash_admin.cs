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
        private Form cerrentChild = null;
        private Control[] defaultDashboardControls = null;
        public Dash_admin()
        {
            InitializeComponent();
            try
            {
                defaultDashboardControls = panelUtama.Controls.Cast<Control>().ToArray();
            }
            catch { defaultDashboardControls = new Control[0]; }
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
            // Open Laporan_penjualan inside the main panel as MDI-like child
            BukaFormAnak(new Laporan_penjualan());
            AktifkanMenu(btn_laporaPenjual);
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
                string query = @"SELECT 
                        td.id_detail AS 'Detail ID', 
                        td.id_transaksi AS 'ID Transaksi', 
                        td.id_buku AS 'ID Buku', 
                        b.judul AS 'Nama Buku', 
                        td.jumlah AS 'Jumlah', 
                        td.subtotal AS 'Subtotal' 
                     FROM transaction_details td 
                     JOIN books b ON td.id_buku = b.id_buku";

                MySqlCommand cmd = new MySqlCommand(query, koneksi);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = dt;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dataGridView1.Columns["Subtotal"] != null)
                {
                    dataGridView1.Columns["Subtotal"].DefaultCellStyle.Format = "N0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (koneksi.State == ConnectionState.Open)
                {
                    koneksi.Close();
                }
            }

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
        public void BukaFormAnak(Form formAnak)
        {
            // remove existing child if any
            if (cerrentChild != null)
            {
                cerrentChild.Close();
                cerrentChild.Dispose();
                panelUtama.Controls.Remove(cerrentChild);
                cerrentChild = null;
            }

            // clear panel and show the new child
            panelUtama.Controls.Clear();

            formAnak.TopLevel = false;
            formAnak.FormBorderStyle = FormBorderStyle.None;
            formAnak.Dock = DockStyle.Fill;

            panelUtama.Controls.Add(formAnak);
            formAnak.Show();

            cerrentChild = formAnak;
        }
        private void pindahForm<T>(Button tombol, Form formAnak) where T : Form
        {
            if(typeof(T) == typeof(Dash_admin))
            {
               showdashboard();
                AktifkanMenu(button16);
                return;
            }

           
        }
        public void showdashboard()
        {
            try 
            {
                // close and remove current child form
                if (cerrentChild != null)
                {
                    cerrentChild.Close();
                    cerrentChild.Dispose();
                    panelUtama.Controls.Remove(cerrentChild);
                    cerrentChild = null;
                }

                // restore the original dashboard controls
                panelUtama.Controls.Clear();
                if (defaultDashboardControls != null)
                {
                    foreach (var ctrl in defaultDashboardControls)
                    {
                        // avoid adding disposed controls
                        if (ctrl != null && !ctrl.IsDisposed)
                            panelUtama.Controls.Add(ctrl);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan dashboard: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btn_tambah_buku_Click(object sender, EventArgs e)
        {
            dafta_buku formStok = new dafta_buku();
            BukaFormAnak(formStok);
            AktifkanMenu(btn_tambah_buku);
            
        }

        private void Pindah_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btn_manajemenStok_Click(object sender, EventArgs e)
        {
            // Open Stok_buku inside the main panel
            BukaFormAnak(new Stok_buku());          
            AktifkanMenu(btn_manajemenStok);
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

        private void button16_Click(object sender, EventArgs e) => pindahForm<Dash_admin>(button16, new Dash_admin());
        

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
            // Open Stok_buku inside the main panel (alternate handler)
            BukaFormAnak(new Stok_buku());
            AktifkanMenu(btn_manajemenStok);
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
            // Open dafta_buku inside the main panel
            BukaFormAnak(new dafta_buku());
            AktifkanMenu(btn_tambah_buku);
        }

        private void btn_laporaPenjual_Click_1(object sender, EventArgs e)
        {
            BukaFormAnak(new Laporan_penjualan());
            AktifkanMenu(btn_laporaPenjual);
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

        private void btn_transaksi_Click(object sender, EventArgs e)
        {

        }
    }
    
}
