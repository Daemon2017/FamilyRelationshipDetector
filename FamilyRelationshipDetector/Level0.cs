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
        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 0,
                y = 0;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button2.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button2.Text;
            }
        }

        private void button7_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 1,
                y = 0;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button7.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button7.Text;
            }
        }

        private void button23_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 2,
                y = 0;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button23.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button23.Text;
            }
        }

        private void button24_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 3,
                y = 0;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button24.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button24.Text;
            }
        }

        private void button25_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 4,
                y = 0;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button25.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button25.Text;
            }
        }

        private void button26_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 5,
                y = 0;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button26.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button26.Text;
            }
        }

        private void button47_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 6,
                y = 0;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button47.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button47.Text;
            }
        }
    }
}