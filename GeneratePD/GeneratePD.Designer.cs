namespace GeneratePD
{
    partial class GeneratePD
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
            this.Genarate = new System.Windows.Forms.Button();
            this.OldP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.NEWP = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Genarate
            // 
            this.Genarate.Location = new System.Drawing.Point(205, 26);
            this.Genarate.Name = "Genarate";
            this.Genarate.Size = new System.Drawing.Size(75, 23);
            this.Genarate.TabIndex = 0;
            this.Genarate.Text = "Genarate";
            this.Genarate.UseVisualStyleBackColor = true;
            this.Genarate.Click += new System.EventHandler(this.Genarate_Click);
            // 
            // OldP
            // 
            this.OldP.Location = new System.Drawing.Point(98, 28);
            this.OldP.Name = "OldP";
            this.OldP.Size = new System.Drawing.Size(100, 20);
            this.OldP.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Input Password:";
            // 
            // NEWP
            // 
            this.NEWP.Location = new System.Drawing.Point(21, 61);
            this.NEWP.Name = "NEWP";
            this.NEWP.ReadOnly = true;
            this.NEWP.Size = new System.Drawing.Size(247, 20);
            this.NEWP.TabIndex = 3;
            this.NEWP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GeneratePD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 108);
            this.Controls.Add(this.NEWP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OldP);
            this.Controls.Add(this.Genarate);
            this.Name = "GeneratePD";
            this.Text = "GeneratePD";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Genarate;
        private System.Windows.Forms.TextBox OldP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox NEWP;
    }
}