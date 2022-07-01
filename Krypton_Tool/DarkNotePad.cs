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
using System.IO;

namespace Krypton_Tool
{
    public partial class NotePadPage : KryptonForm
    {
        // For the movement of the form
        bool formMoving = false;
        Point startPoint = new Point(0, 0);
        // is equal ?
        string strMyOriginalText;
        DialogResult result;
        // About
        string path;
        // Modified
        bool isChanged = true;
        // Default zoom
        float defaultZoom;


        // Constructor
        public NotePadPage()
        {
            InitializeComponent();
            strMyOriginalText = txtBoxKryptonText.Text;
            menuStripNotePad.Renderer = new MyRenderer();

        }

        bool IsSave()
        {
            if (strMyOriginalText != txtBoxKryptonText.Text)
                return true;
            return false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // this
            this.MinimumSize = this.Size;
            this.BackColor = Color.FromArgb(31, 59, 77);

            // Form Buttons
            rjBtnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 7, 58);

            // rich Textbox
            txtBoxKryptonText.StateCommon.Back.Color1 = Color.FromArgb(52, 56, 55);
            txtBoxKryptonText.StateCommon.Border.Color1 = Color.FromArgb(52, 56, 55);
            txtBoxKryptonText.StateCommon.Content.Color1 = Color.FromArgb(250, 252, 252);
            txtBoxKryptonText.Text = string.Empty;
            defaultZoom = txtBoxKryptonText.ZoomFactor;

            // MenuStripItems
            cutToolStripMenuItem.Enabled = false;
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
            if (IsSave())
            {
                result = MessageBox.Show("Değişiklikleri Kaydetmek İstiyormusunuz ?", "DarkNotePad",
                   MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                    Application.Exit();
                else if (result == DialogResult.No)
                    Application.Exit();
            }
            else
                Application.Exit();
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
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSave())
            {
                DialogResult newResult = MessageBox.Show("Değişiklikleri Kaydetmek İstiyormusunuz ?", "DarkNotePad",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (newResult == DialogResult.No)
                {
                    path = string.Empty;
                    txtBoxKryptonText.Clear();
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Text Documents|*.txt", ValidateNames = true, Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(ofd.FileName))
                        {
                            path = ofd.FileName;
                            lblTittle.Text = ofd.SafeFileName + " - DarkNotePad";
                            Task<string> text = sr.ReadToEndAsync();
                            txtBoxKryptonText.Text = text.Result;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(path))
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Documents|*.txt", ValidateNames = true })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            path = sfd.FileName;
                            FileInfo fi = new FileInfo(sfd.FileName);
                            lblTittle.Text = fi.Name + " - DarkNotePad";
                            using (StreamWriter sw = new StreamWriter(sfd.FileName))
                            {
                                await sw.WriteAsync(txtBoxKryptonText.Text);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        await sw.WriteLineAsync(txtBoxKryptonText.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Documents|*.txt", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(sfd.FileName))
                        {
                            await sw.WriteAsync(txtBoxKryptonText.Text);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (About frm = new About())
            {
                frm.ShowDialog();
            }
        }
        private class MyRenderer : ToolStripProfessionalRenderer
        {
            public MyRenderer() : base(new MyColors()) { }
        }
        private class MyColors : ProfessionalColorTable
        {
            public MyColors()
            {
                base.UseSystemColors = false;
            }
            public override Color MenuItemSelected
            {
                // when the menu is selected
                get { return Color.FromArgb(64, 64, 64); }
            }
            public override Color MenuItemSelectedGradientBegin
            {
                get { return Color.FromArgb(31, 31, 46); }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return Color.FromArgb(31, 31, 46); }
            }
            public override Color MenuItemPressedGradientBegin
            {
                get { return Color.FromArgb(31, 31, 46); }
            }
            public override Color MenuItemPressedGradientEnd
            {
                get { return Color.FromArgb(31, 31, 46); }
            }
            public override Color MenuBorder
            {
                get { return Color.FromArgb(31, 31, 46); }
            }

        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtBoxKryptonText.Undo();
        }

        private void txtBoxKryptonText_TextChanged(object sender, EventArgs e)
        {

            if (txtBoxKryptonText.Text != strMyOriginalText && isChanged)
            {
                lblTittle.Text = "*" + lblTittle.Text;
                isChanged = false;
            }

            else if (txtBoxKryptonText.Text == strMyOriginalText)
            {
                lblTittle.Text = lblTittle.Text.Remove(0, 1);
                isChanged = true;
            }
        }

        private void reUndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtBoxKryptonText.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBoxKryptonText.SelectedText))
                txtBoxKryptonText.Cut();
        }

        private void txtBoxKryptonText_SelectionChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBoxKryptonText.SelectedText))
                cutToolStripMenuItem.Enabled = true;
            else
                cutToolStripMenuItem.Enabled = false;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtBoxKryptonText.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtBoxKryptonText.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtBoxKryptonText.SelectionStart < txtBoxKryptonText.Text.Length) // after cursor 
            {
                int startCursorLocation = txtBoxKryptonText.SelectionStart;
                txtBoxKryptonText.Text = txtBoxKryptonText.Text.Remove(txtBoxKryptonText.SelectionStart, 1);
                txtBoxKryptonText.SelectionStart = startCursorLocation;
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtBoxKryptonText.SelectAll();
        }

        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtBoxKryptonText.Text = txtBoxKryptonText.Text.Insert(txtBoxKryptonText.SelectionStart, DateTime.Now.ToString("h:mm tt d/M/yyyy "));
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FontDialog font = new FontDialog())
            {
                if (font.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(txtBoxKryptonText.Text))
                {
                    txtBoxKryptonText.SelectAll();
                    txtBoxKryptonText.SelectionFont = font.Font;
                }
            }
        }

        private void restoreDefaultZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtBoxKryptonText.ZoomFactor = 1F; // don't working
        }
    }
}
