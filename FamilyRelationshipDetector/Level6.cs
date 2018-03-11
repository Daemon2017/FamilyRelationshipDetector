using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealtionshipDetector
{
    public partial class Form1 : Form
    {
        private void button41_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 0,
                y = 6;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button41.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button41.Text;
            }
        }
    }
}
