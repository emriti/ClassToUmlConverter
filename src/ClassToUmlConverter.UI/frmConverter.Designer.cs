
namespace ClassToUmlConverterUI
{
    partial class frmConverter
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
            this.label1 = new System.Windows.Forms.Label();
            this.txbInputFolder = new System.Windows.Forms.TextBox();
            this.txbOutputFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnInBrowse = new System.Windows.Forms.Button();
            this.btnOutBrowse = new System.Windows.Forms.Button();
            this.txbProgress = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input Folder";
            // 
            // txbInputFolder
            // 
            this.txbInputFolder.Location = new System.Drawing.Point(114, 12);
            this.txbInputFolder.Name = "txbInputFolder";
            this.txbInputFolder.ReadOnly = true;
            this.txbInputFolder.Size = new System.Drawing.Size(610, 23);
            this.txbInputFolder.TabIndex = 1;
            this.txbInputFolder.Text = "C:\\Users\\emriti\\Desktop\\wow\\in";
            // 
            // txbOutputFolder
            // 
            this.txbOutputFolder.Location = new System.Drawing.Point(114, 41);
            this.txbOutputFolder.Name = "txbOutputFolder";
            this.txbOutputFolder.ReadOnly = true;
            this.txbOutputFolder.Size = new System.Drawing.Size(610, 23);
            this.txbOutputFolder.TabIndex = 5;
            this.txbOutputFolder.Text = "C:\\Users\\emriti\\Desktop\\wow\\test";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Output Folder";
            // 
            // btnInBrowse
            // 
            this.btnInBrowse.Location = new System.Drawing.Point(730, 11);
            this.btnInBrowse.Name = "btnInBrowse";
            this.btnInBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnInBrowse.TabIndex = 6;
            this.btnInBrowse.Text = "Browse";
            this.btnInBrowse.UseVisualStyleBackColor = true;
            this.btnInBrowse.Click += new System.EventHandler(this.btnInBrowse_Click);
            // 
            // btnOutBrowse
            // 
            this.btnOutBrowse.Location = new System.Drawing.Point(730, 40);
            this.btnOutBrowse.Name = "btnOutBrowse";
            this.btnOutBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnOutBrowse.TabIndex = 7;
            this.btnOutBrowse.Text = "Browse";
            this.btnOutBrowse.UseVisualStyleBackColor = true;
            this.btnOutBrowse.Click += new System.EventHandler(this.btnOutBrowse_Click);
            // 
            // txbProgress
            // 
            this.txbProgress.Location = new System.Drawing.Point(12, 98);
            this.txbProgress.Multiline = true;
            this.txbProgress.Name = "txbProgress";
            this.txbProgress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbProgress.Size = new System.Drawing.Size(793, 372);
            this.txbProgress.TabIndex = 8;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(730, 69);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // frmConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 482);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txbProgress);
            this.Controls.Add(this.btnOutBrowse);
            this.Controls.Add(this.btnInBrowse);
            this.Controls.Add(this.txbOutputFolder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txbInputFolder);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConverter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C# Class to UML Class Diagram Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbInputFolder;
        private System.Windows.Forms.TextBox txbOutputFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnInBrowse;
        private System.Windows.Forms.Button btnOutBrowse;
        private System.Windows.Forms.TextBox txbProgress;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

