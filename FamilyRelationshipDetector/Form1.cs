using System;
using System.Windows.Forms;

namespace RealtionshipDetector
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

            if (0 == y0Result)
            {
                if (0 == y1Result)
                {
                    label10.Text = "?";
                }
                else if (1 == y1Result)
                {
                    label10.Text = button28.Text;
                }
                else if (2 == y1Result)
                {
                    label10.Text = button16.Text;
                }
                else if (3 == y1Result)
                {
                    label10.Text = button35.Text;
                }
                else if (4 == y1Result)
                {
                    label10.Text = button51.Text;
                }
                else if (5 == y1Result)
                {
                    label10.Text = button54.Text;
                }
                else if (6 == y1Result)
                {
                    label10.Text = button57.Text;
                }
                else if (7 == y1Result)
                {
                    label10.Text = "[0;-6] 5*пра(внук)";
                }
                else if (8 == y1Result)
                {
                    label10.Text = "[0;-7] 6*пра(внук)";
                }
                else if (9 == y1Result)
                {
                    label10.Text = "[0;-8] 7*пра(внук)";
                }
            }
            else if (1 == y0Result)
            {
                if (0 == y1Result)
                {
                    label10.Text = button6.Text;
                }
                else if (1 == y1Result)
                {
                    label10.Text = button7.Text;
                }
                else if (2 == y1Result)
                {
                    label10.Text = button29.Text;
                }
                else if (3 == y1Result)
                {
                    label10.Text = button20.Text;
                }
                else if (4 == y1Result)
                {
                    label10.Text = button36.Text;
                }
                else if (5 == y1Result)
                {
                    label10.Text = button53.Text;
                }
                else if (6 == y1Result)
                {
                    label10.Text = "[1;-5] 3*пра(внучатый племянник)";
                }
                else if (7 == y1Result)
                {
                    label10.Text = "[1;-6] 4*пра(внучатый племянник)";
                }
                else if (8 == y1Result)
                {
                    label10.Text = "[1;-7] 5*пра(внучатый племянник)";
                }
                else if (9 == y1Result)
                {
                    label10.Text = "[1;-8] 6*пра(внучатый племянник)";
                }
            }
            else if (2 == y0Result)
            {
                if (0 == y1Result)
                {
                    label10.Text = button5.Text;
                }
                else if (1 == y1Result)
                {
                    label10.Text = button8.Text;
                }
                else if (2 == y1Result)
                {
                    label10.Text = button23.Text;
                }
                else if (3 == y1Result)
                {
                    label10.Text = button30.Text;
                }
                else if (4 == y1Result)
                {
                    label10.Text = button21.Text;
                }
                else if (5 == y1Result)
                {
                    label10.Text = button37.Text;
                }
                else if (6 == y1Result)
                {
                    label10.Text = "[2;-4] 2-ный 2*пра(внучатый племянник)";
                }
                else if (7 == y1Result)
                {
                    label10.Text = "[2;-5] 2-ный 3*пра(внучатый племянник)";
                }
                else if (8 == y1Result)
                {
                    label10.Text = "[2;-6] 2-ный 4*пра(внучатый племянник)";
                }
                else if (9 == y1Result)
                {
                    label10.Text = "[2;-7] 2-ный 5*пра(внучатый племянник)";
                }
            }
            else if (3 == y0Result)
            {
                if (0 == y1Result)
                {
                    label10.Text = button3.Text;
                }
                else if (1 == y1Result)
                {
                    label10.Text = button9.Text;
                }
                else if (2 == y1Result)
                {
                    label10.Text = button13.Text;
                }
                else if (3 == y1Result)
                {
                    label10.Text = button24.Text;
                }
                else if (4 == y1Result)
                {
                    label10.Text = button31.Text;
                }
                else if (5 == y1Result)
                {
                    label10.Text = button22.Text;
                }
                else if (6 == y1Result)
                {
                    label10.Text = button38.Text;
                }
                else if (7 == y1Result)
                {
                    label10.Text = "[3;-4] 3-ный 2*пра(внучатый племянник)";
                }
                else if (8 == y1Result)
                {
                    label10.Text = "[3;-5] 3-ный 3*пра(внучатый племянник)";
                }
                else if (9 == y1Result)
                {
                    label10.Text = "[3;-6] 3-ный 4*пра(внучатый племянник)";
                }
            }
            else if (4 == y0Result)
            {
                if (0 == y1Result)
                {
                    label10.Text = button1.Text;
                }
                else if (1 == y1Result)
                {
                    label10.Text = button10.Text;
                }
                else if (2 == y1Result)
                {
                    label10.Text = button14.Text;
                }
                else if (3 == y1Result)
                {
                    label10.Text = button17.Text;
                }
                else if (4 == y1Result)
                {
                    label10.Text = button25.Text;
                }
                else if (5 == y1Result)
                {
                    label10.Text = button32.Text;
                }
                else if (6 == y1Result)
                {
                    label10.Text = button27.Text;
                }
                else if (7 == y1Result)
                {
                    label10.Text = button39.Text;
                }
                else if (8 == y1Result)
                {
                    label10.Text = "[4;-4] 4-ный 2*пра(внучатый племянник)";
                }
                else if (9 == y1Result)
                {
                    label10.Text = "[4;-5] 4-ный 3*пра(внучатый племянник)";
                }
            }
            else if (5 == y0Result)
            {
                if (0 == y1Result)
                {
                    label10.Text = button4.Text;
                }
                else if (1 == y1Result)
                {
                    label10.Text = button11.Text;
                }
                else if (2 == y1Result)
                {
                    label10.Text = button15.Text;
                }
                else if (3 == y1Result)
                {
                    label10.Text = button18.Text;
                }
                else if (4 == y1Result)
                {
                    label10.Text = button19.Text;
                }
                else if (5 == y1Result)
                {
                    label10.Text = button26.Text;
                }
                else if (6 == y1Result)
                {
                    label10.Text = button33.Text;
                }
                else if (7 == y1Result)
                {
                    label10.Text = button34.Text;
                }
                else if (8 == y1Result)
                {
                    label10.Text = button40.Text;
                }
                else if (9 == y1Result)
                {
                    label10.Text = "[5;-4] 5-ный 2*пра(внучатый племянник)";
                }
            }
            else if (6 == y0Result)
            {
                if (0 == y1Result)
                {
                    label10.Text = button41.Text;
                }
                else if (1 == y1Result)
                {
                    label10.Text = button42.Text;
                }
                else if (2 == y1Result)
                {
                    label10.Text = button43.Text;
                }
                else if (3 == y1Result)
                {
                    label10.Text = button44.Text;
                }
                else if (4 == y1Result)
                {
                    label10.Text = button45.Text;
                }
                else if (5 == y1Result)
                {
                    label10.Text = button46.Text;
                }
                else if (6 == y1Result)
                {
                    label10.Text = button47.Text;
                }
                else if (7 == y1Result)
                {
                    label10.Text = button48.Text;
                }
                else if (8 == y1Result)
                {
                    label10.Text = button49.Text;
                }
                else if (9 == y1Result)
                {
                    label10.Text = button50.Text;
                }
            }
            else if (7 == y0Result)
            {
                if (0 == y1Result)
                {
                    label10.Text = "[0;7] 5*пра(дед)";
                }
                else if (1 == y1Result)
                {
                    label10.Text = "[7;6] 2-ный 4*пра(дед)";
                }
                else if (2 == y1Result)
                {
                    label10.Text = "[7;5] 3-ный 3*пра(дед)";
                }
                else if (3 == y1Result)
                {
                    label10.Text = "[7;4] 4-ный 2*пра(дед)";
                }
                else if (4 == y1Result)
                {
                    label10.Text = "[7;3] 5-ный пра(дед)";
                }
                else if (5 == y1Result)
                {
                    label10.Text = "[7;2] 6-ный дед";
                }
                else if (6 == y1Result)
                {
                    label10.Text = "[7;1] 7-ный родитель";
                }
                else if (7 == y1Result)
                {
                    label10.Text = "[7;0] 7-ный брат";
                }
                else if (8 == y1Result)
                {
                    label10.Text = "[7;-1] 7-ный племянник";
                }
                else if (9 == y1Result)
                {
                    label10.Text = "[7;-2] 7-ный внучатый племянник";
                }
            }
            else if (8 == y0Result)
            {
                if (0 == y1Result)
                {
                    label10.Text = "[0;8] 6*пра(дед)";
                }
                else if (1 == y1Result)
                {
                    label10.Text = "[8;7] 2-ный 5*пра(дед)";
                }
                else if (2 == y1Result)
                {
                    label10.Text = "[8;6] 3-ный 4*пра(дед)";
                }
                else if (3 == y1Result)
                {
                    label10.Text = "[8;5] 4-ный 3*пра(дед)";
                }
                else if (4 == y1Result)
                {
                    label10.Text = "[8;4] 5-ный 2*пра(дед)";
                }
                else if (5 == y1Result)
                {
                    label10.Text = "[8;3] 6-ный прадед";
                }
                else if (6 == y1Result)
                {
                    label10.Text = "[8;2] 7-ный дед";
                }
                else if (7 == y1Result)
                {
                    label10.Text = "[8;1] 8-ный родитель";
                }
                else if (8 == y1Result)
                {
                    label10.Text = "[8;0] 8-ный брат";
                }
                else if (9 == y1Result)
                {
                    label10.Text = "[8;-1] 8-ный племянник";
                }
            }
            else if (9 == y0Result)
            {
                if (0 == y1Result)
                {
                    label10.Text = "[0;9] 7*пра(дед)";
                }
                else if (1 == y1Result)
                {
                    label10.Text = "[9;8] 2-ный 6*пра(дед)";
                }
                else if (2 == y1Result)
                {
                    label10.Text = "[9;7] 3-ный 5*пра(дед)";
                }
                else if (3 == y1Result)
                {
                    label10.Text = "[9;6] 4-ный 4*пра(дед)";
                }
                else if (4 == y1Result)
                {
                    label10.Text = "[9;5] 5-ный 3*пра(дед)";
                }
                else if (5 == y1Result)
                {
                    label10.Text = "[9;4] 6-ный 2*пра(дед)";
                }
                else if (6 == y1Result)
                {
                    label10.Text = "[9;3] 7-ный прадед";
                }
                else if (7 == y1Result)
                {
                    label10.Text = "[9;2] 8-ный дед";
                }
                else if (8 == y1Result)
                {
                    label10.Text = "[9;1] 9-ный родитель";
                }
                else if (9 == y1Result)
                {
                    label10.Text = "[9;0] 9-ный брат";
                }
            }

            if (x0 == x1)
            {
                if (y0 != y1)
                {
                    if (!((x0 == 0 & y0 >= 0) |
                        (x1 == 0 & y1 >= 0)))
                    {
                        label10.Text += "\nДругая степень родства.";
                    }
                }
            }
                
            if (((x0 > 1) & (x1 > 1)) |
                ((y0 > 0) & (y1 > 0)) |
                ((y0 > 0) & (x1 > 1) | (y1 > 0) & (x0 > 1)))
            {
                label10.Text += "\nРодства нет.";
            }

            if ((0 > y0Result) |
                (0 > y1Result))
            {
                label10.Text = "Ошибка!";
            }
        }
    }
}
