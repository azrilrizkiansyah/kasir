using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
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
        private void Laporan_penjualan_Load_1(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}