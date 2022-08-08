using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DarkNotePad
{
    internal class MyColors : ProfessionalColorTable
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
