using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace kasir
{
    public partial class PrintPreviewControl : Form
    {

        string konfigurasi = "server=localhost;username=root;password=;database=toko_buku";
        public PrintPreviewControl(long idTrans, decimal totalBayar, decimal uangBayar, decimal kembalian)
        {
            InitializeComponent();

        }
       

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

       

        private void struk_Load(object sender, EventArgs e)
        {


        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

       

        private void btnCetakStruk_Click(object sender, EventArgs e)
        {

        }

        private void btnTutup_Click(object sender, EventArgs e)
        {

        }
    }
}
