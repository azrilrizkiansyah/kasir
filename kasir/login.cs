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
namespace kasir
{
    public partial class login : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);
        string konfigurasi = "server=localhost;username=root;password=;database=toko_buku";
        public login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Ha2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void Hal_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void login_Load(object sender, EventArgs e)
        {
            SendMessage(textBox1.Handle, 0x1501, (IntPtr)1, "Masukan username Anda");
            
            SendMessage(textBox2.Handle, 0x1501, (IntPtr)1, "Masukan password Anda");
            textBox1.Focus();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.UseSystemPasswordChar == false)
            {
                textBox2.UseSystemPasswordChar = true; 
                button3.Text = "👁"; 
            }
            else
            {
                textBox2.UseSystemPasswordChar = false; 
                button3.Text = "🙈"; 
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (username.Length < 1)
            {
                MessageBox.Show("Username tidak boleh kosong", "peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (password.Length < 1)
            {
                MessageBox.Show("Password tidak boleh kosong", "peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MySqlConnection koneksi = new MySqlConnection(konfigurasi);
            MySqlDataReader hasil = null;
            try
            {
                koneksi.Open();
                string query = "SELECT * FROM users WHERE username=@username AND password=@password";
                MySqlCommand komando = new MySqlCommand(query, koneksi);
                komando.Parameters.AddWithValue("@username", username);
                komando.Parameters.AddWithValue("@password", password);
                hasil = komando.ExecuteReader();
                if (hasil.Read())
                {
                    string role = hasil["role"].ToString();
                    if (role == "admin")
                    {
                        Dash_admin hal = new Dash_admin();
                        hal.FormClosed += Hal_FormClosed;
                        hal.Show();
                        this.Hide();
                    }
                    else if (role == "kasir")
                    {
                        dash_kasir ha2 = new dash_kasir();
                        ha2.FormClosed += Ha2_FormClosed;
                        ha2.Show();
                        this.Hide();

                    }
                }
                else
                {
                    MessageBox.Show("Username atau password salah", "peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
