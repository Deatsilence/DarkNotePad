
namespace DarkNotePad
{
    partial class About
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
            this.lblProductName = new System.Windows.Forms.Label();
            this.rjBtnOk = new RJControls.RJConpanents.RJButton();
            this.lblProductVersion = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblForCommunication = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductName.Location = new System.Drawing.Point(25, 33);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(106, 23);
            this.lblProductName.TabIndex = 0;
            this.lblProductName.Text = "Product Name:";
            // 
            // rjBtnOk
            // 
            this.rjBtnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(59)))), ((int)(((byte)(77)))));
            this.rjBtnOk.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(59)))), ((int)(((byte)(77)))));
            this.rjBtnOk.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rjBtnOk.BorderRadius = 10;
            this.rjBtnOk.BorderSize = 0;
            this.rjBtnOk.FlatAppearance.BorderSize = 0;
            this.rjBtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjBtnOk.ForeColor = System.Drawing.Color.White;
            this.rjBtnOk.Location = new System.Drawing.Point(149, 154);
            this.rjBtnOk.Name = "rjBtnOk";
            this.rjBtnOk.Size = new System.Drawing.Size(96, 33);
            this.rjBtnOk.TabIndex = 1;
            this.rjBtnOk.Text = "&OK";
            this.rjBtnOk.TextColor = System.Drawing.Color.White;
            this.rjBtnOk.UseVisualStyleBackColor = false;
            this.rjBtnOk.Click += new System.EventHandler(this.rjBtnOk_Click);
            // 
            // lblProductVersion
            // 
            this.lblProductVersion.AutoSize = true;
            this.lblProductVersion.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductVersion.Location = new System.Drawing.Point(25, 58);
            this.lblProductVersion.Name = "lblProductVersion";
            this.lblProductVersion.Size = new System.Drawing.Size(61, 23);
            this.lblProductVersion.TabIndex = 0;
            this.lblProductVersion.Text = "Version:";
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyright.Location = new System.Drawing.Point(25, 83);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(128, 23);
            this.lblCopyright.TabIndex = 0;
            this.lblCopyright.Text = "Product Copyright";
            // 
            // lblForCommunication
            // 
            this.lblForCommunication.AutoSize = true;
            this.lblForCommunication.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForCommunication.Location = new System.Drawing.Point(25, 108);
            this.lblForCommunication.Name = "lblForCommunication";
            this.lblForCommunication.Size = new System.Drawing.Size(140, 23);
            this.lblForCommunication.TabIndex = 0;
            this.lblForCommunication.Text = "For Communication:";
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(397, 199);
            this.Controls.Add(this.rjBtnOk);
            this.Controls.Add(this.lblForCommunication);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblProductVersion);
            this.Controls.Add(this.lblProductName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.Load += new System.EventHandler(this.About_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProductName;
        private RJControls.RJConpanents.RJButton rjBtnOk;
        private System.Windows.Forms.Label lblProductVersion;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblForCommunication;
    }
}