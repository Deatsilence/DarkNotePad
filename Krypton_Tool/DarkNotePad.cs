using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using RJControls.RJConpanents;
using RJControls;
using System.Runtime.InteropServices;

namespace Krypton_Tool
{
    public partial class NotePadPage : KryptonForm
    {
        // For the movement of the form
        bool formMoving = false;
        Point startPoint = new Point(0, 0);
        string strMyOriginalText;


        // Constructor
        public NotePadPage()
        {
            InitializeComponent();
            strMyOriginalText = txtBoxKryptonText.Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // this
            this.MinimumSize = this.Size;

            // Form Buttons
            rjBtnClose.FlatAppearance.MouseOverBackColor = Color.Red;
        }

        private void NotePadPage_MouseDown(object sender, MouseEventArgs e)
        {
            formMoving = true;
            startPoint = new Point(e.X, e.Y);
        }
        private void NotePadPage_MouseUp(object sender, MouseEventArgs e)
        {
            formMoving = false;
        }
        private void NotePadPage_MouseMove(object sender, MouseEventArgs e)
        {
            if (formMoving)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.startPoint.X, p.Y - this.startPoint.Y);
            }
        }
        private void rjBtnClose_MouseEnter(object sender, EventArgs e)
        {
            rjBtnClose.Image = Properties.Resources.closewhite;
        }
        private void rjBtnClose_MouseLeave(object sender, EventArgs e)
        {
            rjBtnClose.Image = Properties.Resources.closedark;
        }
        private void rjBtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void NotePadPage_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Determine if text has changed in the textbox by comparing to original text.
            if (txtBoxKryptonText.Text != strMyOriginalText)
            {
                // Display a MsgBox asking the user to save changes or abort.
                if (MessageBox.Show("Kaydetmeden Çıkmak İstediğinize Emin misiniz?", "Uyarı",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    // Cancel the Closing event from closing the form.
                    e.Cancel = true;
                    // Call method to save file...
                }
            }
        }
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        private void rjBtnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 0, 0));
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
            }
        }

        private void rjBtnHide_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
