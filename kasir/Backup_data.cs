using System;
using System.Windows.Forms;

namespace kasir
{
    public partial class Backup_data : Form
    {
        public Backup_data()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Backup Data";
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new System.Drawing.Size(400, 300);
        }
    }
}
