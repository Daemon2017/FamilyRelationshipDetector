using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FamilyRelationshipDetector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int nullPersonsX = 0,
            nullPersonsY = 0,
            firstPersonsX = 0,
            firstPersonsY = 0;

        List<Relative> relatives = new List<Relative>();

        private void RelativeButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                nullPersonsX = ((Relative)sender).X;
                nullPersonsY = ((Relative)sender).Y;
                label2.Text = ((Relative)sender).RelationName;
            }
            else if (e.Button == MouseButtons.Right)
            {
                firstPersonsX = ((Relative)sender).X;
                firstPersonsY = ((Relative)sender).Y;
                label4.Text = ((Relative)sender).RelationName;
            }
        }

        private void Generate(object sender, EventArgs e)
        {
            int minX = 0,
                minY = 0,
                maxX = 0,
                maxY = 0;

            minX = Convert.ToInt16(textBox1.Text);
            minY = Convert.ToInt16(textBox2.Text);
            maxX = Convert.ToInt16(textBox4.Text);
            maxY = Convert.ToInt16(textBox3.Text);

            if (maxX >= minX && maxY >= minY)
            {
                /*
                 * Количества ячеек по вертикали/горизонтали в генерируемой матрице. 
                 */
                int quantityOfCells = -26 + minX * -4 + minY * -6 + maxX * 7 + maxY * 3;

                /*
                 * Построение матрицы возможных степеней родства.
                 */
                string[,][] relationshipsMatrix = new string[quantityOfCells, quantityOfCells][];

                /*
                 * Построение матрицы предковых степеней родства.
                 */
                string[][] ancestorsMatrix = new string[quantityOfCells][];

                /*
                 * Построение матрицы потомковых степеней родства.
                 */
                string[][] descendantsMatrix = new string[quantityOfCells][];

                /*
                 * Построение матрицы количества общих сантиморган.
                 */
                string[] centimorgansMatrix = new string[quantityOfCells];

                int person = 0,
                    relative = 0;

                for (int startX = minX;
                    startX <= maxX;
                    startX++)
                {
                    for (int startY = minY;
                        startY <= maxY;
                        startY++)
                    {
                        /*
                         * Исключение повторов степеней родства, 
                         * занимающих более одной вертикали.
                         */
                        if (!(startY > 0 && (startX > 0 && startX <= startY)))
                        {
                            ancestorsMatrix[person] = new string[quantityOfCells];
                            descendantsMatrix[person] = new string[quantityOfCells];

                            for (int endX = minX;
                            endX <= maxX;
                            endX++)
                            {
                                for (int endY = minY;
                                    endY <= maxY;
                                    endY++)
                                {
                                    /*
                                     * Исключение повторов степеней родства, 
                                     * занимающих более одной вертикали.
                                     */
                                    if (!(endY > 0 && (endX > 0 && endX <= endY)))
                                    {
                                        foreach (var Relative in relatives)
                                        {
                                            string numTemp = Relative.RelationNumber.ToString();

                                            if (startX == endX)
                                            {
                                                /*
                                                 * Определение степеней родства, 
                                                 * приходящихся личности предковыми.
                                                 */
                                                if (startY < endY)
                                                {
                                                    if (Relative.X.Equals(endX) && Relative.Y.Equals(endY))
                                                    {
                                                        ancestorsMatrix[person][relative] = numTemp;
                                                    }
                                                }
                                                /*
                                                 * Определение степеней родства, 
                                                 * приходящихся личности потомковыми.
                                                 */
                                                else if (startY > endY)
                                                {
                                                    if (Relative.X.Equals(endX) && Relative.Y.Equals(endY))
                                                    {
                                                        descendantsMatrix[person][relative] = numTemp;
                                                    }
                                                }
                                            }
                                            /*
                                             * Определение степеней родства, 
                                             * приходящихся личности предковыми.
                                             */
                                            else if (0 == endX && endY >= startX)
                                            {
                                                if (startY < endY)
                                                {
                                                    if (Relative.X.Equals(endX) && Relative.Y.Equals(endY))
                                                    {
                                                        ancestorsMatrix[person][relative] = numTemp;
                                                    }
                                                }
                                            }
                                            /*
                                             * Определение степеней родства, 
                                             * приходящихся личности потомковыми.
                                             */
                                            else if (0 == startX && endX <= startY)
                                            {
                                                if (startY > endY)
                                                {
                                                    if (Relative.X.Equals(endX) && Relative.Y.Equals(endY))
                                                    {
                                                        descendantsMatrix[person][relative] = numTemp;
                                                    }
                                                }
                                            }
                                        }

                                        int numberOfGenerationOfMrca = MrcaSelector(startX,
                                                                                    startY,
                                                                                    endX,
                                                                                    endY);

                                        int jStartResult = numberOfGenerationOfMrca - startY,
                                            jEndResult = numberOfGenerationOfMrca - endY;

                                        int relationship = 0;
                                        relationshipsMatrix[person, relative] = new string[100];

                                        /*
                                         * Определение основной степени родства.
                                         */
                                        relationshipsMatrix[person, relative][relationship] = DetectRelationship(jStartResult,
                                                                                                           jEndResult);

                                        relationship++;

                                        /*
                                         * Обработка расклада, когда первичная и вторичная личность находятся в одной вертикали.
                                         * и между ними возможны различные степени родства.
                                         */
                                        if (startX == endX)
                                        {
                                            if (!((startX == 0 && startY >= 0) ||
                                                (endX == 0 && endY >= 0)))
                                            {
                                                int j0New = startY,
                                                    j1New = endY;

                                                while (j0New < startX && j1New < endX)
                                                {
                                                    numberOfGenerationOfMrca = MrcaSelector(startX,
                                                                                            ++j0New,
                                                                                            endX,
                                                                                            ++j1New);

                                                    relationshipsMatrix[person, relative][relationship] = DetectRelationship(numberOfGenerationOfMrca - startY,
                                                                                                                       numberOfGenerationOfMrca - endY);

                                                    relationship++;
                                                }
                                            }
                                        }

                                        /*
                                         * Обработка расклада, когда между первичной и вторичной личностями может не быть родства.
                                         */
                                        if (((startX > 1) && (endX > 1)) ||
                                            ((startY > 0) && (endY > 0)) ||
                                            ((startY > 0) && (endX > 1) || (endY > 0) && (startX > 1)))
                                        {
                                            relationshipsMatrix[person, relative][relationship] = "0.";
                                        }

                                        relative++;
                                    }
                                }
                            }

                            /*
                             * Определение ожидаемого количества общих сантиморган с каждой из степеней родства.
                             */
                            foreach (var Relative in relatives)
                            {
                                if (Relative.X.Equals(startX) && Relative.Y.Equals(startY))
                                {
                                    if (0 == Relative.ClusterNumber)
                                    {
                                        centimorgansMatrix[person] = "3400";
                                    }
                                    else if (1 == Relative.ClusterNumber)
                                    {
                                        centimorgansMatrix[person] = "2550";
                                    }
                                    else
                                    {
                                        centimorgansMatrix[person] = (3400 / Math.Pow(2, Relative.ClusterNumber - 1)).ToString();
                                    }
                                }
                            }

                            person++;
                            relative = 0;
                        }
                    }
                }

                SaveToFile("relationships.csv", relationshipsMatrix);
                SaveToFile("centimorgans.csv", centimorgansMatrix);

                SaveToFile("descendants.csv", descendantsMatrix);
                SaveToFile("ancestors.csv", ancestorsMatrix);
            }
            else
            {
                DialogResult result = MessageBox.Show("Конечные значения X и Y должны превышать их начальные значения!",
                                                      "Ошибка начальных/конечных значений",
                                                      MessageBoxButtons.OK);
            }
        }

        private void Calculate(object sender, EventArgs e)
        {
            int yMRCA = MrcaSelector(nullPersonsX,
                                     nullPersonsY,
                                     firstPersonsX,
                                     firstPersonsY);

            int y0Result = yMRCA - nullPersonsY,
                y1Result = yMRCA - firstPersonsY;

            label7.Text = y0Result.ToString();
            label8.Text = y1Result.ToString();

            label10.Text = DetectRelationship(y0Result,
                                              y1Result);

            if (nullPersonsX == firstPersonsX)
            {
                if (!((nullPersonsX == 0 && nullPersonsY >= 0) ||
                    (firstPersonsX == 0 && firstPersonsY >= 0)))
                {
                    int y0New = nullPersonsY,
                        y1New = firstPersonsY;

                    while (y0New < nullPersonsX && y1New < firstPersonsX)
                    {
                        yMRCA = MrcaSelector(nullPersonsX,
                                             ++y0New,
                                             firstPersonsX,
                                             ++y1New);

                        label10.Text += "\n" + DetectRelationship(yMRCA - nullPersonsY, yMRCA - firstPersonsY);
                    }
                }
            }

            if (((nullPersonsX > 1) && (firstPersonsX > 1)) ||
                ((nullPersonsY > 0) && (firstPersonsY > 0)) ||
                ((nullPersonsY > 0) && (firstPersonsX > 1) || (firstPersonsY > 0) && (nullPersonsX > 1)))
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
