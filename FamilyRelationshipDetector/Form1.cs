using System;
using System.Windows.Forms;

namespace FamilyRelationshipDetector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int x0 = 0,
            y0 = 0,
            x1 = 0,
            y1 = 0;

        private void button12_Click(object sender, EventArgs e)
        {
            int yMRCA = 0;

            if (x0 > x1)
            {
                if (0 == x1)
                {
                    if ((0 < x0) & (x0 < y1))
                    {
                        yMRCA = y1;
                    }
                    else
                    {
                        yMRCA = x0;
                    }
                }
                else
                {
                    yMRCA = x0;
                }
            }
            else if (x0 == x1)
            {
                if (y0 >= y1)
                {
                    yMRCA = y0;
                }
                else
                {
                    yMRCA = y1;
                }
            }
            else if (x0 < x1)
            {
                if (0 == x0)
                {
                    if ((0 < x1) & (x1 < y0))
                    {
                        yMRCA = y0;
                    }
                    else
                    {
                        yMRCA = x1;
                    }
                }
                else
                {
                    yMRCA = x1;
                }
            }

            int y0Result = yMRCA - y0,
                y1Result = yMRCA - y1;

            label7.Text = y0Result.ToString();
            label8.Text = y1Result.ToString();

            string mainRelationship = RelationshipSelector(y0Result, y1Result);
            label10.Text = mainRelationship;

            if (x0 == x1)
            {
                if (y0 != y1)
                {
                    if (!((x0 == 0 & y0 >= 0) |
                        (x1 == 0 & y1 >= 0)))
                    {
                        label10.Text += "\n\nДругая степень родства:";
                    }
                }
            }
                
            if (((x0 > 1) & (x1 > 1)) |
                ((y0 > 0) & (y1 > 0)) |
                ((y0 > 0) & (x1 > 1) | (y1 > 0) & (x0 > 1)))
            {
                label10.Text += "\n\nРодства нет.";
            }

            if ((0 > y0Result) |
                (0 > y1Result))
            {
                label10.Text = "Ошибка!";
            }
        }
    }
}
