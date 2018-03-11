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
        private void button5_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 0,
                y = 2;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button5.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button5.Text;
            }
        }

        private void button9_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 3,
                y = 2;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button9.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button9.Text;
            }
        }

        private void button14_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 4,
                y = 2;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button14.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button14.Text;
            }
        }

        private void button18_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 5,
                y = 2;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button18.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button18.Text;
            }
        }

        private void button45_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 6,
                y = 2;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button45.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button45.Text;
            }
        }
    }
}
