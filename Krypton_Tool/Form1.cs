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

namespace Krypton_Tool
{
    public partial class NotePadPage : KryptonForm
    {
        // For the movement of the form
        bool formMoving = false;
        Point startPoint = new Point(0, 0);


        // Constructor
        public NotePadPage()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Form Buttons
            rjBtnClose.FlatAppearance.MouseOverBackColor = Color.Red;
            if (rjBtnClose.FlatAppearance.MouseDownBackColor == Color.Red)
                rjBtnClose.ForeColor = Color.FromArgb(250, 252, 252);
            else
                rjBtnClose.ForeColor = Color.FromArgb(250, 252, 252);
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
    }
}
