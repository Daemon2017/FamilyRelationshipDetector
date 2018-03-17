using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FamilyRelationshipDetector
{
    public partial class Form1 : Form
    {
        private void button28_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 0,
                y = -1;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button28.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button28.Text;
            }
        }

        private void button29_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 1,
                y = -1;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button29.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button29.Text;
            }
        }

        private void button30_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 2,
                y = -1;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button30.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button30.Text;
            }
        }

        private void button31_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 3,
                y = -1;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button31.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button31.Text;
            }
        }

        private void button32_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 4,
                y = -1;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button32.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button32.Text;
            }
        }

        private void button33_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 5,
                y = -1;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button33.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button33.Text;
            }
        }

        private void button48_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 6,
                y = -1;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button48.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button48.Text;
            }
        }
    }
}
