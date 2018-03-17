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
        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 0,
                y = 3;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button3.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button3.Text;
            }
        }

        private void button10_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 4,
                y = 3;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button10.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button10.Text;
            }
        }

        private void button15_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 5,
                y = 3;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button15.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button15.Text;
            }
        }

        private void button44_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 6,
                y = 3;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button44.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button44.Text;
            }
        }
    }
}
