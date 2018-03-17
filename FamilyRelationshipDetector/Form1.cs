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
            int yMRCA = MrcaSelector(x0, x1, y0, y1);            

            int y0Result = yMRCA - y0,
                y1Result = yMRCA - y1;

            label7.Text = y0Result.ToString();
            label8.Text = y1Result.ToString();

            label10.Text = RelationshipSelector(y0Result, y1Result);

            if (x0 == x1)
            {
                if (!((x0 == 0 && y0 >= 0) ||
                    (x1 == 0 && y1 >= 0)))
                {
                    int y0New = y0,
                        y1New = y1;

                    while (y0New < x0 && y1New < x1)
                    {
                        yMRCA = MrcaSelector(x0, x1, ++y0New, ++y1New);

                        label10.Text += "\n" + RelationshipSelector(yMRCA - y0, yMRCA - y1);
                    }
                }
            }

            if (((x0 > 1) && (x1 > 1)) ||
                ((y0 > 0) && (y1 > 0)) ||
                ((y0 > 0) && (x1 > 1) || (y1 > 0) && (x0 > 1)))
            {
                label10.Text += "\nРодства нет.";
            }

            if ((0 > y0Result) ||
                (0 > y1Result))
            {
                label10.Text = "Ошибка!";
            }
        }
    }
}
