namespace PDF
{
    partial class FormSSCP
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
            this.btnBPN = new System.Windows.Forms.Button();
            this.btnSSCP1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBPN
            // 
            this.btnBPN.Location = new System.Drawing.Point(34, 36);
            this.btnBPN.Name = "btnBPN";
            this.btnBPN.Size = new System.Drawing.Size(92, 23);
            this.btnBPN.TabIndex = 0;
            this.btnBPN.Text = "PRINT BPN";
            this.btnBPN.UseVisualStyleBackColor = true;
            this.btnBPN.Click += new System.EventHandler(this.btnBPN_Click);
            // 
            // btnSSCP1
            // 
            this.btnSSCP1.Location = new System.Drawing.Point(158, 36);
            this.btnSSCP1.Name = "btnSSCP1";
            this.btnSSCP1.Size = new System.Drawing.Size(99, 23);
            this.btnSSCP1.TabIndex = 1;
            this.btnSSCP1.Text = "PRINT SSCP1";
            this.btnSSCP1.UseVisualStyleBackColor = true;
            this.btnSSCP1.Click += new System.EventHandler(this.btnSSCP1_Click);
            // 
            // FormSSCP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 85);
            this.Controls.Add(this.btnSSCP1);
            this.Controls.Add(this.btnBPN);
            this.Name = "FormSSCP";
            this.Text = "FormSSCP";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBPN;
        private System.Windows.Forms.Button btnSSCP1;
    }
}