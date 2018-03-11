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
        private void button6_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 0,
                y = 1;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button6.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button6.Text;
            }
        }


        private void button8_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 2,
                y = 1;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button8.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button8.Text;
            }
        }

        private void button13_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 3,
                y = 1;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button13.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button13.Text;
            }
        }

        private void button17_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 4,
                y = 1;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button17.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button17.Text;
            }
        }

        private void button19_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 5,
                y = 1;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button19.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button19.Text;
            }
        }

        private void button46_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 6,
                y = 1;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button46.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button46.Text;
            }
        }
    }
}
