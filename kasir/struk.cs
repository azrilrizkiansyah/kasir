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
    public partial class struk : Form
    {
        string konfigurasi = "server=localhost;username=root;password=;database=toko_buku";
        private long idTransaksi;
        
        public struk(long idTrans)
        {
            InitializeComponent();
            this.idTransaksi = idTrans;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void struk_Load(object sender, EventArgs e)
        {
            MySqlConnection koneksi = new MySqlConnection(konfigurasi);

            try
            {
                koneksi.Open();
                string queryHeader = @"SELECT id_transaksi, tanggal, total_harga, id_user 
                               FROM transactions 
                               WHERE id_transaksi = @idTrans";

                MySqlCommand cmdHeader = new MySqlCommand(queryHeader, koneksi);
                cmdHeader.Parameters.AddWithValue("@idTrans", idTransaksi);

                using (MySqlDataReader reader = cmdHeader.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lblNoTrans.Text = "TRX-" + reader["id_transaksi"].ToString().PadLeft(3, '0');
                        lblTanggal.Text = Convert.ToDateTime(reader["tanggal"]).ToString("dd/MM/yyyy HH:mm");
                        lblKasir.Text = reader["id_user"].ToString();
                        decimal total = Convert.ToDecimal(reader["total_harga"]);
                        lblTotal.Text = "Rp " + total.ToString("N0");
                    }
                }
                string queryDetail = @"SELECT b.judul, b.harga, td.jumlah, td.subtotal 
                               FROM transaction_details td 
                               JOIN books b ON td.id_buku = b.id_buku 
                               WHERE td.id_transaksi = @idTrans";

                MySqlCommand cmdDetail = new MySqlCommand(queryDetail, koneksi);
                cmdDetail.Parameters.AddWithValue("@idTrans", idTransaksi);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmdDetail);
                DataTable dtDetail = new DataTable();
                adapter.Fill(dtDetail);
                string textRincian = "";
                int maxKarakterPerBaris = 40; 

                foreach (DataRow row in dtDetail.Rows)
                {
                    string judul = row["judul"].ToString();
                    int jumlah = Convert.ToInt32(row["jumlah"]);
                    decimal harga = Convert.ToDecimal(row["harga"]);
                    decimal subtotal = Convert.ToDecimal(row["subtotal"]);
                    string teksKiri = $"{jumlah} X  {harga:N0}";  
                    string teksKanan = $"{subtotal:N0}";           
                    int jumlahSpasi = maxKarakterPerBaris - (teksKiri.Length + teksKanan.Length);
                    if (jumlahSpasi < 1) jumlahSpasi = 1; 
                    string spasi = new string(' ', jumlahSpasi);
                    textRincian += $"{judul}\n";
                    textRincian += $"{teksKiri}{spasi}{teksKanan}\n\n";
                }

                lblDaftarItem.Text = textRincian;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan struk: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (koneksi.State == ConnectionState.Open) koneksi.Close();
            }
        }
    }
}
