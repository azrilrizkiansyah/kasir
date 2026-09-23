namespace kasir
{
    partial class dash_kasir
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Restore_Data = new System.Windows.Forms.Button();
            this.Btn_BackupData = new System.Windows.Forms.Button();
            this.btn_transaksi = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_manajemenStok = new System.Windows.Forms.Button();
            this.btn_tambah_buku = new System.Windows.Forms.Button();
            this.btn_laporaPenjual = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_sig_out = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chart1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Location = new System.Drawing.Point(3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1442, 717);
            this.panel1.TabIndex = 0;
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(27, 324);
            this.chart1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(1396, 391);
            this.chart1.TabIndex = 9;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_Restore_Data);
            this.panel2.Controls.Add(this.Btn_BackupData);
            this.panel2.Controls.Add(this.btn_transaksi);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.btn_manajemenStok);
            this.panel2.Controls.Add(this.btn_tambah_buku);
            this.panel2.Controls.Add(this.btn_laporaPenjual);
            this.panel2.Location = new System.Drawing.Point(18, 178);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1385, 137);
            this.panel2.TabIndex = 8;
            // 
            // btn_Restore_Data
            // 
            this.btn_Restore_Data.BackColor = System.Drawing.SystemColors.Highlight;
            this.btn_Restore_Data.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Restore_Data.Location = new System.Drawing.Point(1129, 46);
            this.btn_Restore_Data.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_Restore_Data.Name = "btn_Restore_Data";
            this.btn_Restore_Data.Size = new System.Drawing.Size(182, 80);
            this.btn_Restore_Data.TabIndex = 8;
            this.btn_Restore_Data.Text = "Restore Data";
            this.btn_Restore_Data.UseVisualStyleBackColor = false;
            // 
            // Btn_BackupData
            // 
            this.Btn_BackupData.BackColor = System.Drawing.SystemColors.Highlight;
            this.Btn_BackupData.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Btn_BackupData.Location = new System.Drawing.Point(908, 46);
            this.Btn_BackupData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_BackupData.Name = "Btn_BackupData";
            this.Btn_BackupData.Size = new System.Drawing.Size(178, 80);
            this.Btn_BackupData.TabIndex = 9;
            this.Btn_BackupData.Text = "Backup Data";
            this.Btn_BackupData.UseVisualStyleBackColor = false;
            // 
            // btn_transaksi
            // 
            this.btn_transaksi.BackColor = System.Drawing.Color.LawnGreen;
            this.btn_transaksi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_transaksi.Location = new System.Drawing.Point(45, 46);
            this.btn_transaksi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_transaksi.Name = "btn_transaksi";
            this.btn_transaksi.Size = new System.Drawing.Size(174, 80);
            this.btn_transaksi.TabIndex = 3;
            this.btn_transaksi.Text = "Transaksi Penjualan";
            this.btn_transaksi.UseVisualStyleBackColor = false;
            this.btn_transaksi.Click += new System.EventHandler(this.btn_transaksi_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 12);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 29);
            this.label5.TabIndex = 2;
            this.label5.Text = "Fitur Kasir";
            // 
            // btn_manajemenStok
            // 
            this.btn_manajemenStok.BackColor = System.Drawing.SystemColors.Highlight;
            this.btn_manajemenStok.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_manajemenStok.Location = new System.Drawing.Point(697, 46);
            this.btn_manajemenStok.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_manajemenStok.Name = "btn_manajemenStok";
            this.btn_manajemenStok.Size = new System.Drawing.Size(176, 80);
            this.btn_manajemenStok.TabIndex = 7;
            this.btn_manajemenStok.Text = " manajemen Stok";
            this.btn_manajemenStok.UseVisualStyleBackColor = false;
            this.btn_manajemenStok.Click += new System.EventHandler(this.btn_manajemenStok_Click);
            // 
            // btn_tambah_buku
            // 
            this.btn_tambah_buku.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_tambah_buku.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_tambah_buku.Location = new System.Drawing.Point(242, 46);
            this.btn_tambah_buku.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_tambah_buku.Name = "btn_tambah_buku";
            this.btn_tambah_buku.Size = new System.Drawing.Size(180, 80);
            this.btn_tambah_buku.TabIndex = 4;
            this.btn_tambah_buku.Text = "Tambah Buku Data";
            this.btn_tambah_buku.UseVisualStyleBackColor = false;
            // 
            // btn_laporaPenjual
            // 
            this.btn_laporaPenjual.BackColor = System.Drawing.SystemColors.Highlight;
            this.btn_laporaPenjual.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_laporaPenjual.Location = new System.Drawing.Point(461, 46);
            this.btn_laporaPenjual.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_laporaPenjual.Name = "btn_laporaPenjual";
            this.btn_laporaPenjual.Size = new System.Drawing.Size(186, 80);
            this.btn_laporaPenjual.TabIndex = 6;
            this.btn_laporaPenjual.Text = "Laporan Penjualan ";
            this.btn_laporaPenjual.UseVisualStyleBackColor = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.LightCoral;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(18, 141);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1406, 28);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(39, 84);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(383, 29);
            this.label3.TabIndex = 6;
            this.label3.Text = "Dashboard && Ringkasan Transaksi";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightCoral;
            this.panel3.Controls.Add(this.btn_sig_out);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(1, 2);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1440, 55);
            this.panel3.TabIndex = 5;
            // 
            // btn_sig_out
            // 
            this.btn_sig_out.BackColor = System.Drawing.Color.Red;
            this.btn_sig_out.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_sig_out.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_sig_out.Location = new System.Drawing.Point(1304, 6);
            this.btn_sig_out.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_sig_out.Name = "btn_sig_out";
            this.btn_sig_out.Size = new System.Drawing.Size(126, 40);
            this.btn_sig_out.TabIndex = 2;
            this.btn_sig_out.Text = "Sign Out";
            this.btn_sig_out.UseVisualStyleBackColor = false;
            this.btn_sig_out.Click += new System.EventHandler(this.btn_sig_out_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Toko Buku";
            // 
            // dash_kasir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1447, 718);
            this.Controls.Add(this.panel1);
            this.Name = "dash_kasir";
            this.Text = "dash_kasir";
            this.Load += new System.EventHandler(this.dash_kasir_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_Restore_Data;
        private System.Windows.Forms.Button Btn_BackupData;
        private System.Windows.Forms.Button btn_transaksi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_manajemenStok;
        private System.Windows.Forms.Button btn_tambah_buku;
        private System.Windows.Forms.Button btn_laporaPenjual;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_sig_out;
        private System.Windows.Forms.Label label1;
    }
}