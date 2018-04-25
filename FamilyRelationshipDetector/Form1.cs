using System;
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

        int nullPersonsX = 0,
            nullPersonsY = 0,
            firstPersonsX = 0,
            firstPersonsY = 0;

        List<Relative> relatives = new List<Relative>();

        private void Form1_Load(object sender, EventArgs e)
        {
            int numberOfRows = File.ReadAllLines(@"InputMatrix.cfg").ToArray().Length;
            string input = File.ReadAllText(@"InputMatrix.cfg");

            string[,] relationshipMatrix = new string[numberOfRows, 6];
            int numberOfRelative = 0,
                numberOfParameter = 0;

            /*
             * Заполнение матрицы из файла.
             */
            foreach (var row in input.Split('\n'))
            {
                numberOfParameter = 0;

                foreach (var column in row.Trim().Split(';'))
                {
                    relationshipMatrix[numberOfRelative, numberOfParameter] = column.Trim();
                    numberOfParameter++;
                }

                numberOfRelative++;
            }

            int[] horizonatal = new int[numberOfRows];

            /*
             * Выявление всех возможных горизонталей.
             */
            for (int i = 0;
                i < numberOfRows;
                i++)
            {
                horizonatal[i] = Convert.ToInt16(relationshipMatrix[i, 2]);
            }

            int maxHorizontal = 0;

            /*
             * Поиск наибольшей горизонтали.
             */
            for (int i = 0;
                i < horizonatal.Length;
                i++)
            {
                if (horizonatal[i] > maxHorizontal)
                {
                    maxHorizontal = horizonatal[i];
                }
            }

            for (int i = 0;
                i < numberOfRows;
                i++)
            {
                Relative newRelative = new Relative(Convert.ToInt16(relationshipMatrix[i, 0]),
                                                    Convert.ToInt16(relationshipMatrix[i, 1]),
                                                    Convert.ToInt16(relationshipMatrix[i, 2]),
                                                    relationshipMatrix[i, 3],
                                                    Convert.ToInt16(relationshipMatrix[i, 4]),
                                                    Convert.ToInt16(relationshipMatrix[i, 5]),
                                                    maxHorizontal);
                newRelative.MouseDown += new MouseEventHandler(RelativeButton_MouseDown);
                relatives.Add(newRelative);
                panel2.Controls.Add(newRelative);
            }
        }

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
                string[,,] relationships = new string[quantityOfCells,
                                                      quantityOfCells,
                                                      100];

                /*
                 * Построение матрицы предковых степеней родства.
                 */
                string[,] ancestors = new string[quantityOfCells,
                                                 quantityOfCells];

                /*
                 * Построение матрицы потомковых степеней родства.
                 */
                string[,] descendants = new string[quantityOfCells,
                                                   quantityOfCells];

                /*
                 * Построение матрицы количества общих сантиморган.
                 */
                string[] centimorgans = new string[quantityOfCells];

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
                         * Исключение повторов степеней родства, занимающих более одной вертикали.
                         */
                        if (!(startY > 0 && (startX > 0 && startX <= startY)))
                        {
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
                                                 * Определение степеней родства, приходящихся личности предковыми.
                                                 */
                                                if (startY < endY)
                                                {
                                                    if (Relative.X.Equals(endX) && Relative.Y.Equals(endY))
                                                    {
                                                        ancestors[person, relative] = numTemp;
                                                    }
                                                }
                                                /*
                                                 * Определение степеней родства, приходящихся личности потомковыми.
                                                 */
                                                else if (startY > endY)
                                                {
                                                    if (Relative.X.Equals(endX) && Relative.Y.Equals(endY))
                                                    {
                                                        descendants[person, relative] = numTemp;
                                                    }
                                                }
                                            }
                                            /*
                                             * Определение степеней родства, приходящихся личности предковыми.
                                             */
                                            else if (0 == endX && endY >= startX)
                                            {
                                                if (startY < endY)
                                                {
                                                    if (Relative.X.Equals(endX) && Relative.Y.Equals(endY))
                                                    {
                                                        ancestors[person, relative] = numTemp;
                                                    }
                                                }
                                            }
                                            /*
                                             * Определение степеней родства, приходящихся личности потомковыми.
                                             */
                                            else if (0 == startX && endX <= startY)
                                            {
                                                if (startY > endY)
                                                {
                                                    if (Relative.X.Equals(endX) && Relative.Y.Equals(endY))
                                                    {
                                                        descendants[person, relative] = numTemp;
                                                    }
                                                }
                                            }
                                        }

                                        int numberOfGenerationOfMrca = MrcaSelector(startX, startY, endX, endY);

                                        int jStartResult = numberOfGenerationOfMrca - startY,
                                            jEndResult = numberOfGenerationOfMrca - endY;

                                        int relationship = 0;

                                        /*
                                         * Определение основной степени родства.
                                         */
                                        relationships[person, relative, relationship] = DetectRelationship(jStartResult, jEndResult);

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
                                                    numberOfGenerationOfMrca = MrcaSelector(startX, ++j0New, endX, ++j1New);

                                                    relationships[person, relative, relationship] = DetectRelationship(numberOfGenerationOfMrca - startY, numberOfGenerationOfMrca - endY);

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
                                            relationships[person, relative, relationship] = "0.";
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
                                        centimorgans[person] = "3400";
                                    }
                                    else if (1 == Relative.ClusterNumber)
                                    {
                                        centimorgans[person] = "2550";
                                    }
                                    else
                                    {
                                        centimorgans[person] = (3400 / Math.Pow(2, Relative.ClusterNumber - 1)).ToString();
                                    }
                                }
                            }

                            person++;
                            relative = 0;
                        }
                    }
                }

                using (StreamWriter outfile = new StreamWriter(@"relationships.csv"))
                {
                    for (int nullPerson = 0;
                        nullPerson < relationships.GetLength(0);
                        nullPerson++)
                    {
                        string content = "";

                        for (int firstPerson = 0;
                            firstPerson < relationships.GetLength(1);
                            firstPerson++)
                        {
                            for (int relationship = 0;
                                relationship < relationships.GetLength(2);
                                relationship++)
                            {
                                string temp = relationships[nullPerson, firstPerson, relationship];

                                if (temp != null && temp != "")
                                {
                                    content += temp.Substring(0, temp.IndexOf(".")) + ";";
                                }
                            }

                            if (content != "")
                            {
                                content = content.Remove(content.Length - 1);
                            }

                            content += ",";
                        }

                        if (content != "")
                        {
                            content = content.Remove(content.Length - 1);
                        }

                        outfile.WriteLine(content);
                    }
                }

                using (StreamWriter outfile = new StreamWriter(@"ancestors.csv"))
                {
                    for (int relationship = 0;
                        relationship < ancestors.GetLength(0);
                        relationship++)
                    {
                        string content = "";

                        for (int ancestor = 0;
                            ancestor < ancestors.GetLength(1);
                            ancestor++)
                        {
                            string temp = ancestors[relationship, ancestor];

                            if (temp != null)
                            {
                                content += temp + ",";
                            }
                        }

                        if (content != "")
                        {
                            content = content.Remove(content.Length - 1);
                        }

                        outfile.WriteLine(content);
                    }
                }

                using (StreamWriter outfile = new StreamWriter(@"descendants.csv"))
                {
                    for (int relationship = 0;
                        relationship < descendants.GetLength(0);
                        relationship++)
                    {
                        string content = "";

                        for (int descendant = 0;
                            descendant < descendants.GetLength(1);
                            descendant++)
                        {
                            string temp = descendants[relationship, descendant];

                            if (temp != null)
                            {
                                content += temp + ",";
                            }
                        }

                        if (content != "")
                        {
                            content = content.Remove(content.Length - 1);
                        }

                        outfile.WriteLine(content);
                    }
                }

                using (StreamWriter outfile = new StreamWriter(@"centimorgans.csv"))
                {
                    for (int relationship = 0;
                        relationship < centimorgans.GetLength(0);
                        relationship++)
                    {
                        outfile.WriteLine(centimorgans[relationship].Replace(",", "."));
                    }
                }
            }
            else
            {
                string caption = "Ошибка начальных/конечных значений";
                string message = "Конечные значения X и Y должны превышать их начальные значения!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
            }
        }

        private void Calculate(object sender, EventArgs e)
        {
            int yMRCA = MrcaSelector(nullPersonsX, nullPersonsY, firstPersonsX, firstPersonsY);

            int y0Result = yMRCA - nullPersonsY,
                y1Result = yMRCA - firstPersonsY;

            label7.Text = y0Result.ToString();
            label8.Text = y1Result.ToString();

            label10.Text = DetectRelationship(y0Result, y1Result);

            if (nullPersonsX == firstPersonsX)
            {
                if (!((nullPersonsX == 0 && nullPersonsY >= 0) ||
                    (firstPersonsX == 0 && firstPersonsY >= 0)))
                {
                    int y0New = nullPersonsY,
                        y1New = firstPersonsY;

                    while (y0New < nullPersonsX && y1New < firstPersonsX)
                    {
                        yMRCA = MrcaSelector(nullPersonsX, ++y0New, firstPersonsX, ++y1New);

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
