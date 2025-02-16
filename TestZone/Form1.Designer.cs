namespace TestZone
{
    partial class Form1
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
            this.selectPackageDialog = new System.Windows.Forms.OpenFileDialog();
            this.buttonTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // selectPackageDialog
            // 
            this.selectPackageDialog.FileName = "selectPackageDialog";
            this.selectPackageDialog.Filter = "Sims 2 Package Files (*.package)|*.package";
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(228, 145);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(349, 134);
            this.buttonTest.TabIndex = 0;
            this.buttonTest.Text = "Test!";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonTest);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog selectPackageDialog;
        private System.Windows.Forms.Button buttonTest;
    }
}

