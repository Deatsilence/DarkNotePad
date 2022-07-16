using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using ComponentFactory.Krypton.Toolkit;
using RJControls.RJConpanents;
using RJControls;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Drawing2D;

namespace DarkNotePad
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
        // Default font
        Font defaultFont;
        // Default zoom
        float statusbarZoomState = 100;
        // FindCounter
        static bool findCounter = true;
        // isShowingStatusBar
        static bool isShowingStatusBar = true;
        // WordWrap
        static bool isCheckecWordWrap = true;
        // Open With
        string[] openedPaths = Environment.GetCommandLineArgs();
        // Opened File Path
        string filePath;

        // Constructor
        public NotePadPage()
        {
            InitializeComponent();
            strMyOriginalText = txtBoxKryptonText.Text;
            this.menuStripNotePad.Renderer = new MyRenderer();

            if (openedPaths.Length > 1)
            {
                string[] fileName = Environment.GetCommandLineArgs()[1].Split('\\');
                filePath = Environment.GetCommandLineArgs()[1];
                lblTittle.Text = fileName[fileName.Length - 1].ToString() + " - DarkNotePad";
            }
        }

        // is it same ?
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
            rjBtnClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 7, 58);
            rjBtnHide.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 0, 0);
            rjBtnMaximize.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 0, 0);

            // MessageBoxDialog
            MessageBoxManager.Yes = "Save";
            MessageBoxManager.No = "Don't Save";
            MessageBoxManager.Cancel = "Cancel";
            MessageBoxManager.Register();

            // Labels
            lblFind.Visible = false;
            lblIsThere.Visible = false;

            // rich Textbox
            txtBoxKryptonText.StateCommon.Back.Color1 = Color.FromArgb(52, 56, 55);
            txtBoxKryptonText.StateCommon.Border.Color1 = Color.FromArgb(52, 56, 55);
            txtBoxKryptonText.StateCommon.Content.Color1 = Color.FromArgb(250, 252, 252);
            txtBoxKryptonText.Text = string.Empty;
            defaultFont = txtBoxKryptonText.Font;
            txtBoxKryptonText.Focus();

            // Find Textbox
            txtBoxKryptonFindText.StateCommon.Back.Color1 = Color.FromArgb(52, 56, 55);
            txtBoxKryptonFindText.StateCommon.Border.GraphicsHint = PaletteGraphicsHint.AntiAlias;
            txtBoxKryptonFindText.StateCommon.Border.Rounding = 8;
            txtBoxKryptonFindText.StateCommon.Border.Width = 1;
            txtBoxKryptonFindText.StateCommon.Content.Color1 = Color.FromArgb(252, 250, 250);
            txtBoxKryptonFindText.StateCommon.Content.Padding = new Padding(10, 0, 10, 0);
            txtBoxKryptonFindText.Visible = false;

            // Word Wrap
            txtBoxKryptonText.WordWrap = wordToolStripMenuItem.Checked;
            statusBarToolStripMenuItem.Enabled = !wordToolStripMenuItem.Checked;
            if (statusBarToolStripMenuItem.Enabled)
                statusBarToolStripMenuItem.Enabled = true;
            statusBar.Visible = statusBarToolStripMenuItem.Checked;

            // Statusbar
            toolStripStatusZoom.Text = "%" + statusbarZoomState;


            // MenuStripItems
            cutToolStripMenuItem.Enabled = false;
            foreach (ToolStripMenuItem menuItem in menuStripNotePad.Items)
            {
                ((ToolStripDropDownMenu)menuItem.DropDown).ShowImageMargin = false;
                menuItem.DropDown.BackColor = Color.FromArgb(31, 31, 46);
                menuItem.DropDown.ForeColor = Color.FromArgb(225, 225, 225);

                foreach (ToolStripMenuItem item in menuItem.DropDownItems)
                {
                    ((ToolStripDropDownMenu)item.DropDown).ShowImageMargin = false;
                    item.DropDown.BackColor = Color.FromArgb(31, 31, 46);
                    item.DropDown.ForeColor = Color.FromArgb(225, 225, 225);
                }
            }
            ((ToolStripDropDownMenu)FormatToolStripMenuItem.DropDown).ShowImageMargin = true;
            ((ToolStripDropDownMenu)ViewToolStripMenuItem.DropDown).ShowImageMargin = true;

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
                result = MessageBox.Show("Do you want to save changes on " + lblTittle.Text.Substring(0, lblTittle.Text.Length - 13).Remove(0, 1) + " ?", "DarkNotePad",
                   MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(null, null);
                    Application.Exit();
                }
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
                DialogResult newResult = MessageBox.Show("Do you want to save changes on " + lblTittle.Text.Substring(0, lblTittle.Text.Length - 13).Remove(0, 1) + " ?", "DarkNotePad",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (newResult == DialogResult.No)
                {
                    path = string.Empty;
                    txtBoxKryptonText.Clear();
                }
            }
            else
            {
                path = string.Empty;
                txtBoxKryptonText.Clear();
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
                            strMyOriginalText = text.Result.Replace("\r", "");
                            txtBoxKryptonText.Text = text.Result;
                            isChanged = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            txtBoxKryptonText.Focus();
            txtBoxKryptonText.SelectionStart = txtBoxKryptonText.Text.Length;
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
                    strMyOriginalText = txtBoxKryptonText.Text;
                    txtBoxKryptonText.Text = strMyOriginalText;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            txtBoxKryptonText.Focus();
            txtBoxKryptonText.SelectionStart = txtBoxKryptonText.Text.Length;
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
            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
            {
                var tsMenuItem = e.Item as ToolStripMenuItem;
                if (tsMenuItem != null)
                    e.ArrowColor = Color.White;
                base.OnRenderArrow(e);
            }
            protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var r = new Rectangle(e.ImageRectangle.Location, e.ImageRectangle.Size);
                r.Inflate(-4, -6);
                e.Graphics.DrawLines(Pens.White, new Point[]{
                    new Point(r.Left, r.Bottom - r.Height /2),
                    new Point(r.Left + r.Width /3,  r.Bottom),
                    new Point(r.Right, r.Top)});
            }
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
            public override Color ImageMarginGradientBegin
            {
                get { return Color.FromArgb(31, 31, 46); }
            }
            public override Color ImageMarginGradientEnd
            {
                get { return Color.FromArgb(31, 31, 46); }
            }
            public override Color ImageMarginGradientMiddle
            {
                get { return Color.FromArgb(31, 31, 46); }
            }
            public override Color MenuItemBorder
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

            else if (txtBoxKryptonText.Text == strMyOriginalText && lblTittle.Text.Contains('*') == true)
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
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            int position = txtBoxKryptonText.SelectionStart;
            int line = txtBoxKryptonText.GetLineFromCharIndex(position) + 1;
            int column = position - txtBoxKryptonText.GetFirstCharIndexOfCurrentLine() + 1;

            toolStripStatusLineCol.Text = "Ln " + line + ", Col " + column;
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
            txtBoxKryptonText.ZoomFactor = 0.9F;
            txtBoxKryptonText.ZoomFactor = 1F;
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (findCounter)
            {
                txtBoxKryptonFindText.Visible = findCounter;
                lblFind.Visible = findCounter;
                lblIsThere.Visible = findCounter;
                findCounter = false;
            }
            else
            {
                txtBoxKryptonFindText.Visible = findCounter;
                lblFind.Visible = findCounter;
                lblIsThere.Visible = findCounter;
                findCounter = true;
            }
        }

        private void rjToggleBtnColor_CheckedChanged(object sender, EventArgs e)
        {
            if (rjToggleBtnColor.Checked)
            {
                txtBoxKryptonText.StateCommon.Back.Color1 = Color.FromArgb(252, 250, 250);
                txtBoxKryptonText.StateActive.Content.Color1 = Color.Black;
                statusBar.BackColor = Color.FromArgb(252, 250, 250);
                toolStripStatusSpace.BackColor = Color.FromArgb(252, 250, 250);
                txtBoxKryptonText.Focus();
            }
            else
            {
                txtBoxKryptonText.StateCommon.Back.Color1 = Color.FromArgb(52, 56, 55);
                txtBoxKryptonText.StateActive.Content.Color1 = Color.FromArgb(252, 250, 250);
                statusBar.BackColor = Color.FromArgb(52, 56, 55);
                toolStripStatusSpace.BackColor = Color.FromArgb(52, 56, 55);
                txtBoxKryptonText.Focus();
            }
        }

        private void restoreDefaultFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtBoxKryptonText.SelectAll();
            txtBoxKryptonText.SelectionFont = defaultFont;
        }

        private void txtBoxKryptonFindText_TextChanged(object sender, EventArgs e)
        {
            int SummationFind = 0;
            int index = 0;
            string temp = txtBoxKryptonText.Text;
            txtBoxKryptonText.Text = "";
            txtBoxKryptonText.Text = temp;

            if (txtBoxKryptonFindText.Text != string.Empty)
            {
                while (index < txtBoxKryptonText.Text.LastIndexOf(txtBoxKryptonFindText.Text))
                {
                    // Searches the text in a RichTextBox control for a string within a range of text withing the control
                    txtBoxKryptonText.Find(txtBoxKryptonFindText.Text, index, txtBoxKryptonText.TextLength, RichTextBoxFinds.None);
                    // Selection color. this is added automatically when a match is found
                    txtBoxKryptonText.SelectionBackColor = Color.DarkOrange;
                    // After a match is found the index is increased so the search won't stop at the same match again. this
                    index = txtBoxKryptonText.Text.IndexOf(txtBoxKryptonFindText.Text, index, StringComparison.InvariantCultureIgnoreCase) + 1;
                    SummationFind++;
                }
                lblIsThere.Text = string.Format("{0}", SummationFind);
            }
            else
            {
                SummationFind = 0;
                lblIsThere.Text = string.Format("{0}", SummationFind);
            }
        }

        private void wordToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (isCheckecWordWrap)
            {
                txtBoxKryptonText.WordWrap = isCheckecWordWrap;
                isCheckecWordWrap = false;
            }
            else
            {
                txtBoxKryptonText.WordWrap = isCheckecWordWrap;
                isCheckecWordWrap = true;
            }
        }

        private void statusBarToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (isShowingStatusBar)
            {
                statusBar.Visible = isShowingStatusBar;
                isShowingStatusBar = false;
            }
            else
            {
                statusBar.Visible = isShowingStatusBar;
                isShowingStatusBar = true;
            }
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtBoxKryptonText.ZoomFactor += 0.1F;
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtBoxKryptonText.ZoomFactor -= 0.1F;
        }

        private void txtBoxKryptonText_MouseEnter(object sender, EventArgs e)
        {
            txtBoxKryptonText.Focus();
        }
        private void txtBoxKryptonText_MouseWheel(object sender, MouseEventArgs e)
        {
            //int mousedeltaval = e.Delta / 120;

            //if (mousedeltaval == 1) //mousewheel up move
            //{
            //    txtBoxKryptonText.ZoomFactor += 0.1F;
            //    statusbarZoomState += 10;
            //    toolStripStatusZoom.Text = "%" + statusbarZoomState;
            //}
            //else if (mousedeltaval == -1) //mousewheel down move
            //{
            //    txtBoxKryptonText.ZoomFactor -= 0.1F;
            //    statusbarZoomState -= 10;
            //    toolStripStatusZoom.Text = "%" + statusbarZoomState;
            //}
        }
    }
}
