namespace kasir
{
    partial class PrintPreviewControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.btnCetakStruk = new System.Windows.Forms.Button();
            this.btnTutup = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // btnCetakStruk
            // 
            this.btnCetakStruk.Location = new System.Drawing.Point(206, 596);
            this.btnCetakStruk.Name = "btnCetakStruk";
            this.btnCetakStruk.Size = new System.Drawing.Size(184, 43);
            this.btnCetakStruk.TabIndex = 0;
            this.btnCetakStruk.Text = "Cetak";
            this.btnCetakStruk.UseVisualStyleBackColor = true;
            this.btnCetakStruk.Click += new System.EventHandler(this.btnCetakStruk_Click);
            // 
            // btnTutup
            // 
            this.btnTutup.Location = new System.Drawing.Point(396, 596);
            this.btnTutup.Name = "btnTutup";
            this.btnTutup.Size = new System.Drawing.Size(184, 43);
            this.btnTutup.TabIndex = 1;
            this.btnTutup.Text = "Tutup";
            this.btnTutup.UseVisualStyleBackColor = true;
            this.btnTutup.Click += new System.EventHandler(this.btnTutup_Click);
            // 
            // PrintPreviewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 680);
            this.Controls.Add(this.btnTutup);
            this.Controls.Add(this.btnCetakStruk);
            this.DoubleBuffered = true;
            this.Name = "PrintPreviewControl";
            this.Text = "struk";
            this.Load += new System.EventHandler(this.struk_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Button btnCetakStruk;
        private System.Windows.Forms.Button btnTutup;
    }
}