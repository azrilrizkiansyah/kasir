using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kasir
{
    public partial class struk : Form
    {
        private string isiStruk = "";
        public struk()
        {
            InitializeComponent();
            printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
        }
        public struk(string strukText) : this()
        {
           this.isiStruk = strukText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                MessageBox.Show("Teks struk kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            printDialog1.Document = printDocument1;

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    printDocument1.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal mencetak struk: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void struk_Load(object sender, EventArgs e)
        {
            richTextBox1.Font = new Font("Courier New", 9, FontStyle.Regular);
            richTextBox1.Text = isiStruk;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font fontCetak = richTextBox1.Font;
            Brush kuas = Brushes.Black;

            float x = 10;
            float y = 10;

            e.Graphics.DrawString(richTextBox1.Text, fontCetak, kuas, x, y);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
