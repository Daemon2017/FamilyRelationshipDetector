﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        List<Relative> relatives = new List<Relative>();

        string[,] relationshipMatrix;

        private void Form1_Load(object sender, EventArgs e)
        {
            int numberOfRows = File.ReadAllLines(@"InputMatrix.cfg").ToArray().Length;
            string input = File.ReadAllText(@"InputMatrix.cfg");

            relationshipMatrix = new string[numberOfRows, 6];
            int i = 0,
                j = 0;

            foreach (var row in input.Split('\n'))
            {
                j = 0;

                foreach (var col in row.Trim().Split(';'))
                {
                    relationshipMatrix[i, j] = col.Trim();
                    j++;
                }

                i++;
            }

            int[] horizonatal = new int[numberOfRows];

            for (int h = 0; h < numberOfRows; h++)
            {
                horizonatal[h] = Convert.ToInt16(relationshipMatrix[h, 2]);
            }

            int maxHorizontal = 0;
            for (int x = 0; x < horizonatal.Length; x++)
            {
                if (horizonatal[x] > maxHorizontal)
                {
                    maxHorizontal = horizonatal[x];
                }
            }

            for (int a = 0; a < numberOfRows; a++)
            {
                Relative newRelative = new Relative
                {
                    RelationNumber = Convert.ToInt16(relationshipMatrix[a, 0]),
                    Vertical = Convert.ToInt16(relationshipMatrix[a, 1]),
                    Horizontal = Convert.ToInt16(relationshipMatrix[a, 2]),
                    RelationName = relationshipMatrix[a, 3],
                    WidthMultiplier = Convert.ToInt16(relationshipMatrix[a, 4]),
                    ClusterColor = relationshipMatrix[a, 5]
                };
                newRelative.Left = 100 * newRelative.Vertical;
                newRelative.Top = (50 * 7) + (50 * -newRelative.Horizontal);
                newRelative.Width = 100 * newRelative.WidthMultiplier;
                newRelative.Height = 50;
                newRelative.Text = newRelative.RelationNumber + ". [" + newRelative.Vertical + ";" + newRelative.Horizontal + "] " + newRelative.RelationName;
                newRelative.MouseDown += new MouseEventHandler(RelativeButton_MouseDown);
                relatives.Add(newRelative);
                panel2.Controls.Add(newRelative);
            }
        }

        private void RelativeButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                x0 = ((Relative)sender).Vertical;
                y0 = ((Relative)sender).Horizontal;
                label2.Text = ((Relative)sender).RelationName;
            }
            else if (e.Button == MouseButtons.Right)
            {
                x1 = ((Relative)sender).Vertical;
                y1 = ((Relative)sender).Horizontal;
                label4.Text = ((Relative)sender).RelationName;
            }
        }

        private void button60_Click(object sender, EventArgs e)
        {
            int i0 = 0,
                j0 = -3,
                i1 = 5,
                j1 = 3;

            /*
             * Построение матрицы возможных степеней родства
             */
            string[,,] relationships = new string[(Math.Abs(i1 - i0) + 1) * (Math.Abs(j1 - j0) + 1),
                                                  (Math.Abs(j1 - j0) + 1) * (Math.Abs(i1 - i0) + 1),
                                                  10];

            /*
             * Построение матрицы предковых степеней родства
             */
            string[,] ancestors = new string[(Math.Abs(i1 - i0) + 1) * (Math.Abs(j1 - j0) + 1),
                                             (Math.Abs(i1 - i0) + 1) * (Math.Abs(j1 - j0) + 1)];

            /*
             * Построение матрицы потомковых степеней родства
             */
            string[,] descendants = new string[(Math.Abs(i1 - i0) + 1) * (Math.Abs(j1 - j0) + 1),
                                               (Math.Abs(i1 - i0) + 1) * (Math.Abs(j1 - j0) + 1)];

            int i = 0,
                j = 0;

            for (int iStart = i0;
                iStart <= i1;
                iStart++)
            {
                for (int jStart = j0;
                    jStart <= j1;
                    jStart++)
                {
                    /*
                     * Исключение повторов степеней родства, занимающих более одной вертикали
                     */
                    if (!(jStart > 0 && (iStart > 0 && iStart <= jStart)))
                    {
                        for (int iEnd = i0;
                        iEnd <= i1;
                        iEnd++)
                        {
                            for (int jEnd = j0;
                                jEnd <= j1;
                                jEnd++)
                            {
                                /*
                                 * Исключение повторов степеней родства, занимающих более одной вертикали
                                 */
                                if (!(jEnd > 0 && (iEnd > 0 && iEnd <= jEnd)))
                                {
                                    foreach (var Relative in relatives)
                                    {
                                        int iTemp = Relative.Vertical;
                                        int jTemp = Relative.Horizontal;
                                        string numTemp = Relative.RelationNumber.ToString();

                                        if (iStart == iEnd)
                                        {
                                            /*
                                             * Определение степеней родства, приходящихся личности предковыми
                                             */
                                            if (jStart < jEnd)
                                            {
                                                if (iTemp.Equals(iEnd) && jTemp.Equals(jEnd))
                                                {
                                                    ancestors[i, j] = numTemp;
                                                }
                                            }
                                            /*
                                             * Определение степеней родства, приходящихся личности потомковыми
                                             */
                                            else if (jStart > jEnd)
                                            {
                                                if (iTemp.Equals(iEnd) && jTemp.Equals(jEnd))
                                                {
                                                    descendants[i, j] = numTemp;
                                                }
                                            }
                                        }
                                        else if (0 == iEnd && jEnd >= iStart)
                                        {
                                            /*
                                             * Определение степеней родства, приходящихся личности предковыми
                                             */
                                            if (jStart < jEnd)
                                            {
                                                if (iTemp.Equals(iEnd) && jTemp.Equals(jEnd))
                                                {
                                                    ancestors[i, j] = numTemp;
                                                }
                                            }
                                        }
                                    }


                                    int jMRCA = MrcaSelector(iStart, jStart, iEnd, jEnd);

                                    int jStartResult = jMRCA - jStart,
                                        jEndResult = jMRCA - jEnd;

                                    int k = 0;

                                    /*
                                     * Определение основной степени родства
                                     */
                                    relationships[i, j, k] = RelationshipSelector(jStartResult, jEndResult);

                                    k++;

                                    /*
                                     * Обработка расклада, когда первичная и вторичная личность находятся в одной вертикали
                                     * и между ними возможны различные степени родства
                                     */
                                    if (iStart == iEnd)
                                    {
                                        if (!((iStart == 0 && jStart >= 0) ||
                                            (iEnd == 0 && jEnd >= 0)))
                                        {
                                            int j0New = jStart,
                                                j1New = jEnd;

                                            while (j0New < iStart && j1New < iEnd)
                                            {
                                                jMRCA = MrcaSelector(iStart, ++j0New, iEnd, ++j1New);

                                                relationships[i, j, k] = RelationshipSelector(jMRCA - jStart, jMRCA - jEnd);

                                                k++;
                                            }
                                        }
                                    }

                                    /*
                                     * Обработка расклада, когда между первичной и вторичной личностями может не быть родства
                                     */
                                    if (((iStart > 1) && (iEnd > 1)) ||
                                        ((jStart > 0) && (jEnd > 0)) ||
                                        ((jStart > 0) && (iEnd > 1) || (jEnd > 0) && (iStart > 1)))
                                    {
                                        relationships[i, j, k] = "0.";
                                    }

                                    j++;
                                }
                            }
                        }

                        i++;
                        j = 0;
                    }
                }
            }

            using (StreamWriter outfile = new StreamWriter(@"relationships.csv"))
            {
                for (int x = 0; x < relationships.GetLength(0); x++)
                {
                    string content = "";

                    for (int y = 0; y < relationships.GetLength(1); y++)
                    {
                        for (int z = 0; z < relationships.GetLength(2); z++)
                        {
                            string temp = relationships[x, y, z];

                            if (temp != null)
                            {
                                content += temp.Substring(0, temp.IndexOf(".")) + ";";
                            }
                        }

                        content += ",";
                    }

                    outfile.WriteLine(content);
                }
            }

            using (StreamWriter outfile = new StreamWriter(@"ancestors.csv"))
            {
                for (int x = 0; x < ancestors.GetLength(0); x++)
                {
                    string content = "";

                    for (int y = 0; y < ancestors.GetLength(1); y++)
                    {
                        string temp = ancestors[x, y];

                        if (temp != null)
                        {
                            content += temp + ",";
                        }
                    }

                    outfile.WriteLine(content);
                }
            }

            using (StreamWriter outfile = new StreamWriter(@"descendants.csv"))
            {
                for (int x = 0; x < descendants.GetLength(0); x++)
                {
                    string content = "";

                    for (int y = 0; y < descendants.GetLength(1); y++)
                    {
                        string temp = descendants[x, y];

                        if (temp != null)
                        {
                            content += temp + ",";
                        }
                    }

                    outfile.WriteLine(content);
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int yMRCA = MrcaSelector(x0, y0, x1, y1);

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
                        yMRCA = MrcaSelector(x0, ++y0New, x1, ++y1New);

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
