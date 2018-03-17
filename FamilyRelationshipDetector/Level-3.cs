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
        private void button37_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 2,
                y = -3;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button37.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button37.Text;
            }
        }

        private void button38_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 3,
                y = -3;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button38.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button38.Text;
            }
        }

        private void button39_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 4,
                y = -3;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button39.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button39.Text;
            }
        }

        private void button40_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 5,
                y = -3;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button40.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button40.Text;
            }
        }

        private void button36_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 1,
                y = -3;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button36.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button36.Text;
            }
        }

        private void button35_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 0,
                y = -3;
                        
            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button35.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button35.Text;
            }
        }

        private void button50_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 6,
                y = -3;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button50.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button50.Text;
            }
        }
    }
}
