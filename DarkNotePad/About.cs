using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DarkNotePad
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            lblProductName.Text = string.Format("Product Name: {0}", Application.ProductName);
            lblProductVersion.Text = string.Format("Version: {0}", Application.ProductVersion);
            lblCopyright.Text = string.Format("Product Copyright: {0}", "Copyright ©  2022 by MMDream");
        }

        private void rjBtnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
