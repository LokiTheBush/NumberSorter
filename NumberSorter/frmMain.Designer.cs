namespace NumberSorter
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            numSize = new NumericUpDown();
            progressBar1 = new ProgressBar();
            btnSort = new Button();
            lblTesting = new Label();
            ((System.ComponentModel.ISupportInitialize)numSize).BeginInit();
            SuspendLayout();
            // 
            // numSize
            // 
            numSize.Location = new Point(12, 47);
            numSize.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numSize.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numSize.Name = "numSize";
            numSize.Size = new Size(120, 23);
            numSize.TabIndex = 0;
            numSize.Value = new decimal(new int[] { 100000, 0, 0, 0 });
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(12, 110);
            progressBar1.Maximum = 20;
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(247, 23);
            progressBar1.TabIndex = 1;
            // 
            // btnSort
            // 
            btnSort.Location = new Point(138, 47);
            btnSort.Name = "btnSort";
            btnSort.Size = new Size(121, 23);
            btnSort.TabIndex = 2;
            btnSort.Text = "Sort Arrays";
            btnSort.UseVisualStyleBackColor = true;
            btnSort.Click += btnSort_Click;
            // 
            // lblTesting
            // 
            lblTesting.AutoSize = true;
            lblTesting.Location = new Point(96, 83);
            lblTesting.Name = "lblTesting";
            lblTesting.Size = new Size(71, 15);
            lblTesting.TabIndex = 3;
            lblTesting.Text = "Start Testing";
            // 
            // frmMain
            // 
            AcceptButton = btnSort;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(283, 156);
            Controls.Add(lblTesting);
            Controls.Add(btnSort);
            Controls.Add(progressBar1);
            Controls.Add(numSize);
            Name = "frmMain";
            Text = "Number Sorter";
            FormClosing += frmMain_FormClosing;
            Load += frmMain_Load;
            ((System.ComponentModel.ISupportInitialize)numSize).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown numSize;
        private ProgressBar progressBar1;
        private Button btnSort;
        private Label lblTesting;
    }
}