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
        private void button16_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 0,
                y = -2;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button16.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button16.Text;
            }
        }

        private void button20_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 1,
                y = -2;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button20.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button20.Text;
            }
        }

        private void button21_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 2,
                y = -2;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button21.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button21.Text;
            }
        }

        private void button22_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 3,
                y = -2;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button22.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button22.Text;
            }
        }

        private void button27_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 4,
                y = -2;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button27.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button27.Text;
            }
        }

        private void button34_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 5,
                y = -2;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button34.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button34.Text;
            }
        }

        private void button49_MouseDown(object sender, MouseEventArgs e)
        {
            int x = 6,
                y = -2;

            if (e.Button == MouseButtons.Left)
            {
                x0 = x;
                y0 = y;
                label2.Text = button49.Text;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = x;
                y1 = y;
                label4.Text = button49.Text;
            }
        }
    }
}
