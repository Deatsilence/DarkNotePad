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
    public partial class Find : KryptonForm
    {
        public Find()
        {
            InitializeComponent();
        }

        private void Find_Load(object sender, EventArgs e)
        {
            // this
            this.BackColor = Color.FromArgb(52, 56, 55);

            // rjTxtBoxSearch
            rjTxtBoxSearch.BackColor = Color.FromArgb(52, 56, 55);
        }
    }
}
