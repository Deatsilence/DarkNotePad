
namespace Krypton_Tool
{
    partial class Find
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
            this.rjTxtBoxSearch = new RJconpanents.RJtextBox();
            this.SuspendLayout();
            // 
            // rjTxtBoxSearch
            // 
            this.rjTxtBoxSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(56)))), ((int)(((byte)(55)))));
            this.rjTxtBoxSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(56)))), ((int)(((byte)(55)))));
            this.rjTxtBoxSearch.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(56)))), ((int)(((byte)(55)))));
            this.rjTxtBoxSearch.BorderSize = 0;
            this.rjTxtBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rjTxtBoxSearch.ForeColor = System.Drawing.Color.DimGray;
            this.rjTxtBoxSearch.Location = new System.Drawing.Point(13, 6);
            this.rjTxtBoxSearch.Margin = new System.Windows.Forms.Padding(4);
            this.rjTxtBoxSearch.MaxLength = 32767;
            this.rjTxtBoxSearch.Multiline = false;
            this.rjTxtBoxSearch.Name = "rjTxtBoxSearch";
            this.rjTxtBoxSearch.Padding = new System.Windows.Forms.Padding(7);
            this.rjTxtBoxSearch.PasswordChar = false;
            this.rjTxtBoxSearch.Size = new System.Drawing.Size(156, 31);
            this.rjTxtBoxSearch.TabIndex = 0;
            this.rjTxtBoxSearch.Texts = "";
            this.rjTxtBoxSearch.UnderlinedStyle = false;
            // 
            // Find
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(259, 44);
            this.Controls.Add(this.rjTxtBoxSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Find";
            this.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.StateCommon.Border.Rounding = 10;
            this.Text = "Find";
            this.Load += new System.EventHandler(this.Find_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private RJconpanents.RJtextBox rjTxtBoxSearch;
    }
}