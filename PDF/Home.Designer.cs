namespace PDF
{
    partial class Home
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
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RB_ICR = new System.Windows.Forms.RadioButton();
            this.RB_EDII = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RB_SSCP = new System.Windows.Forms.RadioButton();
            this.RB_SSP = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateFrom
            // 
            this.dateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateFrom.Location = new System.Drawing.Point(67, 25);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(99, 20);
            this.dateFrom.TabIndex = 0;
            // 
            // dateTo
            // 
            this.dateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTo.Location = new System.Drawing.Point(203, 25);
            this.dateTo.Name = "dateTo";
            this.dateTo.Size = new System.Drawing.Size(99, 20);
            this.dateTo.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(93, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Date From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(232, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Date To";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RB_ICR);
            this.groupBox1.Controls.Add(this.RB_EDII);
            this.groupBox1.Location = new System.Drawing.Point(12, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(145, 52);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // RB_ICR
            // 
            this.RB_ICR.AutoSize = true;
            this.RB_ICR.Location = new System.Drawing.Point(87, 19);
            this.RB_ICR.Name = "RB_ICR";
            this.RB_ICR.Size = new System.Drawing.Size(43, 17);
            this.RB_ICR.TabIndex = 1;
            this.RB_ICR.TabStop = true;
            this.RB_ICR.Text = "ICR";
            this.RB_ICR.UseVisualStyleBackColor = true;
            // 
            // RB_EDII
            // 
            this.RB_EDII.AutoSize = true;
            this.RB_EDII.Location = new System.Drawing.Point(14, 19);
            this.RB_EDII.Name = "RB_EDII";
            this.RB_EDII.Size = new System.Drawing.Size(46, 17);
            this.RB_EDII.TabIndex = 0;
            this.RB_EDII.TabStop = true;
            this.RB_EDII.Text = "EDII";
            this.RB_EDII.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RB_SSCP);
            this.groupBox2.Controls.Add(this.RB_SSP);
            this.groupBox2.Location = new System.Drawing.Point(209, 62);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(145, 52);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // RB_SSCP
            // 
            this.RB_SSCP.AutoSize = true;
            this.RB_SSCP.Location = new System.Drawing.Point(87, 19);
            this.RB_SSCP.Name = "RB_SSCP";
            this.RB_SSCP.Size = new System.Drawing.Size(53, 17);
            this.RB_SSCP.TabIndex = 1;
            this.RB_SSCP.TabStop = true;
            this.RB_SSCP.Text = "SSCP";
            this.RB_SSCP.UseVisualStyleBackColor = true;
            // 
            // RB_SSP
            // 
            this.RB_SSP.AutoSize = true;
            this.RB_SSP.Location = new System.Drawing.Point(14, 19);
            this.RB_SSP.Name = "RB_SSP";
            this.RB_SSP.Size = new System.Drawing.Size(46, 17);
            this.RB_SSP.TabIndex = 0;
            this.RB_SSP.TabStop = true;
            this.RB_SSP.Text = "SSP";
            this.RB_SSP.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(274, 137);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Download";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 177);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTo);
            this.Controls.Add(this.dateFrom);
            this.Name = "Home";
            this.Text = "Home";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateFrom;
        private System.Windows.Forms.DateTimePicker dateTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RB_ICR;
        private System.Windows.Forms.RadioButton RB_EDII;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton RB_SSCP;
        private System.Windows.Forms.RadioButton RB_SSP;
        private System.Windows.Forms.Button button1;
    }
}