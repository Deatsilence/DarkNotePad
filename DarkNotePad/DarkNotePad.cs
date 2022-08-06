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
using System.Text.RegularExpressions;

namespace DarkNotePad
{
    public partial class NotePadPage : KryptonForm
    {
        // For the movement of the form
        private bool formMoving = false;
        private Point startPoint = new Point(0, 0);
        private DialogResult result;
        // Modified
        private bool isChanged = true;
        // FindCounter
        private static bool findCounter = true;
        // isShowingStatusBar
        private static bool isShowingStatusBar = true;
        // WordWrap
        private static bool isCheckecWordWrap = true;
        // Replece State
        private bool repleceState = true;
        // Default font
        private Font defaultFont;
        // is equal ?
        private string strMyOriginalText;
        // About
        private string path;
        // Default zoom
        private float statusbarZoomState = 100;
        // Open With
        private string[] openedPaths = Environment.GetCommandLineArgs();

        // TaskBar Icon Click
        const int WS_MINIMIZEBOX = 0x20000;
        const int CS_DBLCLKS = 0x8;


        // Constructor
        public NotePadPage()
        {
            InitializeComponent();
            strMyOriginalText = txtBoxKryptonText.Text;
            this.menuStripNotePad.Renderer = new MyRenderer();
            txtBoxKryptonFindText.GotFocus += txtBoxKryptonFindText_GotFocus;
            txtBoxKryptonFindText.LostFocus += txtBoxKryptonFindText_LostFocus;
            txtBoxKryptonNewText.GotFocus += txtBoxKryptonNewText_GotFocus;
            txtBoxKryptonNewText.LostFocus += txtBoxKryptonNewText_LostFocus;


            if (openedPaths.Length > 1)
            {
                string[] fileName = Environment.GetCommandLineArgs()[1].Split('\\');
                path = Environment.GetCommandLineArgs()[1];
                lblTittle.Text = fileName[fileName.Length - 1].ToString() + " - DarkNotePad";
                strMyOriginalText = File.ReadAllText(path);

                if (strMyOriginalText[strMyOriginalText.Length - 1] == '\n' && strMyOriginalText[strMyOriginalText.Length - 2] == '\r')
                    strMyOriginalText = strMyOriginalText.Substring(0, strMyOriginalText.Length - 2);
                isChanged = true;
            }
        }


        /// <summary>
        /// Functions I Wrote 
        /// </summary>
        // is it same ?
        bool IsSave()
        {
            if (strMyOriginalText != txtBoxKryptonText.Text)
                return true;
            return false;
        }
        bool IsPressedCtrl()
        {
            return (Control.ModifierKeys & Keys.Control) == Keys.Control;
        }
        private void UpdateStatus()
        {
            int position = txtBoxKryptonText.SelectionStart;
            int line = txtBoxKryptonText.GetLineFromCharIndex(position) + 1;
            int column = position - txtBoxKryptonText.GetFirstCharIndexOfCurrentLine() + 1;

            toolStripStatusLineCol.Text = "Ln " + line + ", Col " + column;
        }

        /// <summary>
        /// Application's Events Functions
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            // this
            this.MinimumSize = this.Size;
            this.BackColor = Color.FromArgb(25, 27, 28);
            this.Icon = Properties.Resources.darknotepadimgico;

            // Form Buttons
            rjBtnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(196, 43, 28);
            rjBtnClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(196, 43, 28);
            rjBtnHide.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 0, 0);
            rjBtnMaximize.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 0, 0);
            rjBtnReplece.FlatAppearance.MouseOverBackColor = Color.FromArgb(39, 39, 39);
            rjBtnReplece.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 39, 39);

            // MessageBoxDialog
            MessageBoxManager.Yes = "Save";
            MessageBoxManager.No = "Don't Save";
            MessageBoxManager.Cancel = "Cancel";
            MessageBoxManager.Register();

            // Labels
            lblIsThere.Visible = false;

            // PictureBox Find
            pictureBoxFind.Visible = false;

            // PictureBox Change
            rjBtnReplece.Visible = false;

            // rich Textbox
            txtBoxKryptonText.BackColor = Color.FromArgb(39, 39, 39);
            txtBoxKryptonText.ForeColor = Color.FromArgb(250, 252, 252);
            txtBoxKryptonText.Text = string.Empty;
            defaultFont = txtBoxKryptonText.Font;
            txtBoxKryptonText.Text = strMyOriginalText;
            txtBoxKryptonText.Focus();
            txtBoxKryptonText.SelectionStart = txtBoxKryptonText.Text.Length;


            // Find Textbox
            txtBoxKryptonFindText.StateCommon.Back.Color1 = Color.FromArgb(52, 56, 55);
            txtBoxKryptonFindText.StateCommon.Border.GraphicsHint = PaletteGraphicsHint.AntiAlias;
            txtBoxKryptonFindText.StateCommon.Border.Rounding = 8;
            txtBoxKryptonFindText.StateCommon.Border.Width = 2;
            txtBoxKryptonFindText.StateCommon.Content.Color1 = Color.FromArgb(252, 250, 250);
            txtBoxKryptonFindText.StateCommon.Content.Padding = new Padding(10, 0, 10, 0);
            txtBoxKryptonFindText.Visible = false;

            // New Textbox
            txtBoxKryptonFindText.StateCommon.Back.Color1 = Color.FromArgb(52, 56, 55);
            txtBoxKryptonFindText.StateCommon.Border.GraphicsHint = PaletteGraphicsHint.AntiAlias;
            txtBoxKryptonFindText.StateCommon.Border.Rounding = 8;
            txtBoxKryptonFindText.StateCommon.Border.Width = 2;
            txtBoxKryptonFindText.StateCommon.Content.Color1 = Color.FromArgb(252, 250, 250);
            txtBoxKryptonFindText.StateCommon.Content.Padding = new Padding(10, 0, 10, 0);
            txtBoxKryptonNewText.Visible = false;


            // Word Wrap
            txtBoxKryptonText.WordWrap = wordToolStripMenuItem.Checked;
            statusBarToolStripMenuItem.Enabled = !wordToolStripMenuItem.Checked;
            if (statusBarToolStripMenuItem.Enabled)
                statusBarToolStripMenuItem.Enabled = true;
            statusBar.Visible = statusBarToolStripMenuItem.Checked;

            // Statusbar
            toolStripStatusLineCol.ForeColor = Color.WhiteSmoke;
            toolStripStatusSpace.ForeColor = Color.WhiteSmoke;
            toolStripStatusSpace.BackColor = ColorTranslator.FromHtml("#191B1C");
            toolStripStatusZoom.ForeColor = Color.WhiteSmoke;
            toolStripStatusZoom.Text = "%" + statusbarZoomState;
            toolStripStatusCRLF.ForeColor = Color.WhiteSmoke;
            toolStripStatusUTF.ForeColor = Color.WhiteSmoke;
            statusBar.BackColor = ColorTranslator.FromHtml("#191B1C");
            statusBar.Visible = true;



            // MenuStripItems
            foreach (ToolStripMenuItem menuItem in menuStripNotePad.Items)
            {
                ((ToolStripDropDownMenu)menuItem.DropDown).ShowImageMargin = false;
                menuItem.DropDown.BackColor = Color.FromArgb(25, 27, 28);
                menuItem.DropDown.ForeColor = Color.FromArgb(225, 225, 225);
                menuItem.BackColor = Color.FromArgb(25, 27, 28);

                foreach (ToolStripMenuItem item in menuItem.DropDownItems)
                {
                    ((ToolStripDropDownMenu)item.DropDown).ShowImageMargin = false;
                    item.DropDown.BackColor = Color.FromArgb(25, 27, 28);
                    item.DropDown.ForeColor = Color.FromArgb(225, 225, 225);
                }
            }
            ((ToolStripDropDownMenu)FormatToolStripMenuItem.DropDown).ShowImageMargin = true;
            ((ToolStripDropDownMenu)ViewToolStripMenuItem.DropDown).ShowImageMargin = true;
            statusBarToolStripMenuItem.Checked = true;
            cutToolStripMenuItem.Enabled = false;

        }

        private void NotePadPage_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
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
        private void rjBtnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 0, 0));
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 0, 0));
            }
        }
        private void rjBtnHide_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void rjToggleBtnColor_CheckedChanged(object sender, EventArgs e)
        {
            if (rjToggleBtnColor.Checked)
            {
                txtBoxKryptonText.BackColor = Color.WhiteSmoke;
                txtBoxKryptonText.ForeColor = Color.Black;
                txtBoxKryptonText.Focus();
            }
            else
            {
                txtBoxKryptonText.BackColor = Color.FromArgb(39, 39, 39);
                txtBoxKryptonText.ForeColor = Color.WhiteSmoke;
                txtBoxKryptonText.Focus();
            }
        }
        private void rjBtnReplece_Click(object sender, EventArgs e)
        {
            if (txtBoxKryptonText.Text != string.Empty && Convert.ToInt32(lblIsThere.Text) > 0)
                txtBoxKryptonText.Text = txtBoxKryptonText.Text.Replace(txtBoxKryptonFindText.Text, txtBoxKryptonNewText.Text);
            else
                MessageBox.Show("Not found in text", "DarkNotePad", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
                else if (newResult == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(null, null);
                    txtBoxKryptonText.Clear();
                    lblTittle.Text = "Untitled - DarkNotePad";
                    path = string.Empty;
                    isChanged = true;
                    strMyOriginalText = txtBoxKryptonText.Text;
                }
            }
            else
            {
                txtBoxKryptonText.Clear();
                lblTittle.Text = "Untitled - DarkNotePad";
                path = string.Empty;
                isChanged = true;
                strMyOriginalText = txtBoxKryptonText.Text;
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
                            if (text.Result[text.Result.Length - 1] == '\n' && text.Result[text.Result.Length - 2] == '\r')
                                strMyOriginalText = text.Result.Substring(0, text.Result.Length - 2);
                            else
                                strMyOriginalText = text.Result;
                            txtBoxKryptonText.Text = strMyOriginalText;
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
            txtBoxKryptonText.SelectionStart = 0;
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
                        FileInfo fi = new FileInfo(sfd.FileName);
                        lblTittle.Text = fi.Name + " - DarkNotePad";
                        strMyOriginalText = txtBoxKryptonText.Text;
                        txtBoxKryptonText.Text = strMyOriginalText;
                        isChanged = true;

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

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtBoxKryptonText.Undo();
        }

        private void txtBoxKryptonText_MouseEnter(object sender, EventArgs e)
        {
            txtBoxKryptonText.Focus();
        }
        private void txtBoxKryptonText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                txtBoxKryptonText.Text += (string)System.Windows.Clipboard.GetData("Text");
                e.Handled = true;
            }
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

        private void txtBoxKryptonText_SelectionChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBoxKryptonText.SelectedText))
                cutToolStripMenuItem.Enabled = true;
            else
                cutToolStripMenuItem.Enabled = false;
            UpdateStatus();
        }


        private void txtBoxKryptonText_MouseWheel(object sender, MouseEventArgs e)
        {
            if (IsPressedCtrl())
            {
                int mousedeltaval = e.Delta / 120;

                if (mousedeltaval == 1 && statusbarZoomState < 500) //mousewheel up move
                {
                    txtBoxKryptonText.ZoomFactor += 0.1F;
                    statusbarZoomState += 10;
                    txtBoxKryptonText.ZoomFactor -= 0.1F;
                    toolStripStatusZoom.Text = "%" + statusbarZoomState;
                }
                else if (mousedeltaval == -1 && statusbarZoomState > 10) //mousewheel down move
                {
                    txtBoxKryptonText.ZoomFactor -= 0.1F;
                    statusbarZoomState -= 10;
                    txtBoxKryptonText.ZoomFactor += 0.1F;
                    toolStripStatusZoom.Text = "%" + statusbarZoomState;
                }
            }
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
                while (index <= txtBoxKryptonText.Text.LastIndexOf(txtBoxKryptonFindText.Text))
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
        private void txtBoxKryptonNewText_GotFocus(object sender, EventArgs e)
        {
            txtBoxKryptonNewText.StateActive.Border.Color1 = Color.FromArgb(5, 112, 147);
        }
        private void txtBoxKryptonNewText_LostFocus(object sender, EventArgs e)
        {
            txtBoxKryptonNewText.StateActive.Border.Color1 = Color.FromArgb(196, 43, 28);
        }

        private void txtBoxKryptonFindText_GotFocus(object sender, EventArgs e)
        {
            txtBoxKryptonFindText.StateActive.Border.Color1 = Color.FromArgb(5, 112, 147);
        }
        private void txtBoxKryptonFindText_LostFocus(object sender, EventArgs e)
        {
            txtBoxKryptonFindText.StateActive.Border.Color1 = Color.FromArgb(196, 43, 28);
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

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtBoxKryptonText.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtBoxKryptonText.Text += (string)System.Windows.Clipboard.GetData("Text");
            txtBoxKryptonText.SelectionStart = txtBoxKryptonText.Text.Length;
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
        private void repleceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (repleceState || pictureBoxFind.Visible == true)
            {
                repleceState = true;
                rjBtnReplece.Visible = repleceState;
                pictureBoxFind.Visible = !repleceState;
                txtBoxKryptonNewText.Visible = repleceState;
                txtBoxKryptonFindText.Visible = repleceState;
                lblIsThere.Visible = repleceState;
                repleceState = false;
                //txtBoxKryptonText.Text = txtBoxKryptonText.Text.Replace(txtBoxKryptonFindText.Text, txtBoxKryptonNewText.Text);
            }
            else
            {
                rjBtnReplece.Visible = repleceState;
                pictureBoxFind.Visible = repleceState;
                txtBoxKryptonNewText.Visible = repleceState;
                txtBoxKryptonFindText.Visible = repleceState;
                lblIsThere.Visible = repleceState;
                repleceState = true;
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
                if (font.ShowDialog() == DialogResult.OK/*string.IsNullOrEmpty(txtBoxKryptonText.Text)*/)
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
            statusbarZoomState = 100;
            toolStripStatusZoom.Text = "%" + statusbarZoomState;
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (findCounter || txtBoxKryptonNewText.Visible == true)
            {
                findCounter = true;
                txtBoxKryptonFindText.Visible = findCounter;
                txtBoxKryptonNewText.Visible = !findCounter;
                rjBtnReplece.Visible = !findCounter;
                pictureBoxFind.Visible = findCounter;
                lblIsThere.Visible = findCounter;
                findCounter = false;
            }
            else
            {
                txtBoxKryptonFindText.Visible = findCounter;
                txtBoxKryptonNewText.Visible = findCounter;
                rjBtnReplece.Visible = findCounter;
                pictureBoxFind.Visible = findCounter;
                lblIsThere.Visible = findCounter;
                findCounter = true;
            }
        }

        private void restoreDefaultFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtBoxKryptonText.SelectAll();
            txtBoxKryptonText.SelectionFont = defaultFont;
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
                txtBoxKryptonText.Size = new Size(834, 404);
                isShowingStatusBar = false;
            }
            else
            {
                statusBar.Visible = isShowingStatusBar;
                isShowingStatusBar = true;
                txtBoxKryptonText.Location = new Point(0, 73);
                txtBoxKryptonText.Size = new Size(834, 429);
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

        // For MenuStrip Properties
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
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= WS_MINIMIZEBOX;
                cp.ClassStyle |= CS_DBLCLKS;
                return cp;
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
                get { return Color.FromArgb(52, 56, 55); }
            }
            public override Color MenuItemSelectedGradientBegin
            {
                get { return Color.FromArgb(52, 56, 55); }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return Color.FromArgb(52, 56, 55); }
            }
            public override Color MenuItemPressedGradientBegin
            {
                get { return Color.FromArgb(52, 56, 55); }
            }
            public override Color MenuItemPressedGradientEnd
            {
                get { return Color.FromArgb(52, 56, 55); }
            }
            public override Color MenuBorder
            {
                get { return Color.FromArgb(25, 27, 28); }
            }
            public override Color ImageMarginGradientBegin
            {
                get { return Color.FromArgb(25, 27, 28); }
            }
            public override Color ImageMarginGradientEnd
            {
                get { return Color.FromArgb(25, 27, 28); }
            }
            public override Color ImageMarginGradientMiddle
            {
                get { return Color.FromArgb(25, 27, 28); }
            }
            public override Color MenuItemBorder
            {
                get { return Color.FromArgb(25, 27, 28); }
            }
        }
    }
}
