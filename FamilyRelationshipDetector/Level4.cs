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
        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 0,
                y = 4;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button1.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button1.Text;
            }
        }

        private void button11_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 5,
                y = 4;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button11.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button11.Text;
            }
        }

        private void button43_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 6,
                y = 4;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button43.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button43.Text;
            }
        }
    }
}
