namespace pdfScratcher
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
            btnGeneratePDF = new Button();
            button1 = new Button();
            comboMapSize = new ComboBox();
            SuspendLayout();
            // 
            // btnGeneratePDF
            // 
            btnGeneratePDF.Location = new Point(12, 12);
            btnGeneratePDF.Name = "btnGeneratePDF";
            btnGeneratePDF.Size = new Size(190, 51);
            btnGeneratePDF.TabIndex = 0;
            btnGeneratePDF.Text = "Make a PDF";
            btnGeneratePDF.UseVisualStyleBackColor = true;
            btnGeneratePDF.Click += btnGeneratePDF_Click;
            // 
            // button1
            // 
            button1.Location = new Point(208, 12);
            button1.Name = "button1";
            button1.Size = new Size(190, 51);
            button1.TabIndex = 1;
            button1.Text = "Exit";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnExit_Click;
            // 
            // comboMapSize
            // 
            comboMapSize.FormattingEnabled = true;
            comboMapSize.Location = new Point(12, 80);
            comboMapSize.Name = "comboMapSize";
            comboMapSize.Size = new Size(190, 23);
            comboMapSize.TabIndex = 2;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(414, 186);
            Controls.Add(comboMapSize);
            Controls.Add(button1);
            Controls.Add(btnGeneratePDF);
            Name = "frmMain";
            Text = "Map Builder";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnGeneratePDF;
        private Button button1;
        private ComboBox comboMapSize;
    }
}
