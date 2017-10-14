namespace mrgReader
{
    partial class mrgReader
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
            this.DecompressedResultBox = new System.Windows.Forms.PictureBox();
            this.OpenMrgFile_btn = new System.Windows.Forms.Button();
            this.Quality_found = new System.Windows.Forms.TextBox();
            this.width_found = new System.Windows.Forms.TextBox();
            this.height_found = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Matrixviewer = new System.Windows.Forms.DataGridView();
            this.Column0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DecompressedResultBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Matrixviewer)).BeginInit();
            this.SuspendLayout();
            // 
            // DecompressedResultBox
            // 
            this.DecompressedResultBox.Location = new System.Drawing.Point(167, 60);
            this.DecompressedResultBox.Name = "DecompressedResultBox";
            this.DecompressedResultBox.Size = new System.Drawing.Size(369, 311);
            this.DecompressedResultBox.TabIndex = 0;
            this.DecompressedResultBox.TabStop = false;
            // 
            // OpenMrgFile_btn
            // 
            this.OpenMrgFile_btn.Location = new System.Drawing.Point(254, 12);
            this.OpenMrgFile_btn.Name = "OpenMrgFile_btn";
            this.OpenMrgFile_btn.Size = new System.Drawing.Size(176, 42);
            this.OpenMrgFile_btn.TabIndex = 1;
            this.OpenMrgFile_btn.Text = "Open mrg file";
            this.OpenMrgFile_btn.UseVisualStyleBackColor = true;
            this.OpenMrgFile_btn.Click += new System.EventHandler(this.OpenMrgFile_btn_Click);
            // 
            // Quality_found
            // 
            this.Quality_found.Location = new System.Drawing.Point(149, 409);
            this.Quality_found.Name = "Quality_found";
            this.Quality_found.Size = new System.Drawing.Size(100, 22);
            this.Quality_found.TabIndex = 2;
            // 
            // width_found
            // 
            this.width_found.Location = new System.Drawing.Point(149, 437);
            this.width_found.Name = "width_found";
            this.width_found.Size = new System.Drawing.Size(100, 22);
            this.width_found.TabIndex = 3;
            // 
            // height_found
            // 
            this.height_found.Location = new System.Drawing.Point(149, 465);
            this.height_found.Name = "height_found";
            this.height_found.Size = new System.Drawing.Size(100, 22);
            this.height_found.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 414);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Quality Detected";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 442);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "width";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 470);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "height";
            // 
            // Matrixviewer
            // 
            this.Matrixviewer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Matrixviewer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column0,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.Matrixviewer.Location = new System.Drawing.Point(731, 36);
            this.Matrixviewer.Name = "Matrixviewer";
            this.Matrixviewer.RowTemplate.Height = 24;
            this.Matrixviewer.Size = new System.Drawing.Size(688, 395);
            this.Matrixviewer.TabIndex = 75;
            // 
            // Column0
            // 
            this.Column0.HeaderText = "Col0";
            this.Column0.Name = "Column0";
            this.Column0.Width = 50;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Col1";
            this.Column1.Name = "Column1";
            this.Column1.Width = 50;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Col2";
            this.Column2.Name = "Column2";
            this.Column2.Width = 50;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Col3";
            this.Column3.Name = "Column3";
            this.Column3.Width = 50;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Col4";
            this.Column4.Name = "Column4";
            this.Column4.Width = 50;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Col5";
            this.Column5.Name = "Column5";
            this.Column5.Width = 50;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Col6";
            this.Column6.Name = "Column6";
            this.Column6.Width = 50;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Col7";
            this.Column7.Name = "Column7";
            this.Column7.Width = 50;
            // 
            // mrgReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1444, 527);
            this.Controls.Add(this.Matrixviewer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.height_found);
            this.Controls.Add(this.width_found);
            this.Controls.Add(this.Quality_found);
            this.Controls.Add(this.OpenMrgFile_btn);
            this.Controls.Add(this.DecompressedResultBox);
            this.Name = "mrgReader";
            this.Text = "mrgReader";
            ((System.ComponentModel.ISupportInitialize)(this.DecompressedResultBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Matrixviewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox DecompressedResultBox;
        private System.Windows.Forms.Button OpenMrgFile_btn;
        private System.Windows.Forms.TextBox Quality_found;
        private System.Windows.Forms.TextBox width_found;
        private System.Windows.Forms.TextBox height_found;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView Matrixviewer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column0;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    }
}

