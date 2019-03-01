using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FamilyRelationshipDetector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            InitializeComponent();
        }

        private readonly FileSaver _fileSaver = new FileSaver();
        private readonly RelationshipSelector _relationshipSelector = new RelationshipSelector();
        private readonly MrcaSelector _mrcaSelector = new MrcaSelector();

        private int _nullPersonsX,
            _nullPersonsY,
            _firstPersonsX,
            _firstPersonsY;

        private readonly List<Relative> _relatives = new List<Relative>();

        public void LoadFromFile(object sender, EventArgs e)
        {
            int numberOfRows = File.ReadAllLines(@"InputMatrix.cfg").ToArray().Length;
            string input = File.ReadAllText(@"InputMatrix.cfg");

            string[,] relativesMatrix = new string[numberOfRows, 7];
            int numberOfRelative = 0;

            /*
             * Заполнение матрицы из файла.
             */
            foreach (var row in input.Split('\n'))
            {
                var numberOfParameter = 0;

                foreach (var column in row.Trim().Split(','))
                {
                    relativesMatrix[numberOfRelative, numberOfParameter] = column.Trim();
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
                horizonatal[i] = Convert.ToInt16(relativesMatrix[i, 2]);
            }

            /*
             * Поиск наибольшей горизонтали.
             */
            int maxHorizontal = horizonatal.Concat(new[] {0}).Max();

            for (int i = 0;
                i < numberOfRows;
                i++)
            {
                Relative newRelative = new Relative(Convert.ToInt16(relativesMatrix[i, 0]),
                    Convert.ToInt16(relativesMatrix[i, 1]),
                    Convert.ToInt16(relativesMatrix[i, 2]),
                    relativesMatrix[i, 3],
                    Convert.ToInt16(relativesMatrix[i, 4]),
                    Convert.ToInt16(relativesMatrix[i, 5]),
                    Convert.ToDouble(relativesMatrix[i, 6]),
                    maxHorizontal);
                newRelative.MouseDown += RelativeButton_MouseDown;
                _relatives.Add(newRelative);
                panel2.Controls.Add(newRelative);
            }
        }


        private void RelativeButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _nullPersonsX = ((Relative) sender).X;
                _nullPersonsY = ((Relative) sender).Y;
                label2.Text = ((Relative) sender).RelationName;
            }
            else if (e.Button == MouseButtons.Right)
            {
                _firstPersonsX = ((Relative) sender).X;
                _firstPersonsY = ((Relative) sender).Y;
                label4.Text = ((Relative) sender).RelationName;
            }
        }

        private void Generate(object sender, EventArgs e)
        {
            int minX = Convert.ToInt16(textBox1.Text);
            int minY = Convert.ToInt16(textBox2.Text);
            int maxX = Convert.ToInt16(textBox4.Text);
            int maxY = Convert.ToInt16(textBox3.Text);

            if (maxX >= minX && maxY >= minY)
            {
                /*
                 * Количества ячеек по вертикали/горизонтали в генерируемой матрице. 
                 */
                int quantityOfCells = -26 + minX * -4 + minY * -6 + maxX * 7 + maxY * 3;

                /*
                 * Построение матрицы возможных степеней родства.
                 */
                List<string>[,] relationshipsMatrix = new List<string>[quantityOfCells, quantityOfCells];

                /*
                 * Построение матрицы количества общих сантиморган.
                 */
                List<string> centimorgansMatrix = new List<string>();

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
                                        int numberOfGenerationOfMrca = _mrcaSelector.SelectMrca(startX,
                                            startY,
                                            endX,
                                            endY);

                                        int jStartResult = numberOfGenerationOfMrca - startY,
                                            jEndResult = numberOfGenerationOfMrca - endY;

                                        /*
                                         * Определение основной степени родства.
                                         */
                                        relationshipsMatrix[person, relative] = new List<string>
                                        {
                                            _relationshipSelector.DetectRelationship(jStartResult, jEndResult,
                                                _relatives)
                                        };

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
                                                    numberOfGenerationOfMrca = _mrcaSelector.SelectMrca(startX,
                                                        ++j0New,
                                                        endX,
                                                        ++j1New);

                                                    relationshipsMatrix[person, relative].Add(
                                                        _relationshipSelector.DetectRelationship(
                                                            numberOfGenerationOfMrca - startY,
                                                            numberOfGenerationOfMrca - endY,
                                                            _relatives));
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
                                            relationshipsMatrix[person, relative].Add("0.");
                                        }

                                        relative++;
                                    }
                                }
                            }

                            /*
                             * Определение ожидаемого количества общих сантиморган с каждой из степеней родства.
                             */
                            foreach (var rel in _relatives)
                            {
                                if (rel.X.Equals(startX) && rel.Y.Equals(startY))
                                {
                                    centimorgansMatrix.Add(rel.CommonCm.ToString());
                                }
                            }

                            person++;
                            relative = 0;
                        }
                    }
                }

                _fileSaver.SaveToFile("relationships.csv", relationshipsMatrix);
                _fileSaver.SaveToFile("centimorgans.csv", centimorgansMatrix);

                /*
                 * Построение матрицы максимального числа предков каждого вида.
                 */
                List<List<string>> maxCountMatrix = new List<List<string>>();

                foreach (var rel in _relatives)
                {
                    if (rel.X.Equals(0) && rel.Y > 0)
                    {
                        maxCountMatrix.Add(new List<string>
                        {
                            rel.RelationNumber.ToString(),
                            Math.Pow(2, rel.Y).ToString()
                        });
                    }
                }

                _fileSaver.SaveToFile("maxCount.csv", maxCountMatrix);
            }
            else
            {
                MessageBox.Show("Конечные значения X и Y должны превышать их начальные значения!",
                    "Ошибка начальных/конечных значений",
                    MessageBoxButtons.OK);
            }
        }

        private void Calculate(object sender, EventArgs e)
        {
            int yMrca = _mrcaSelector.SelectMrca(_nullPersonsX,
                _nullPersonsY,
                _firstPersonsX,
                _firstPersonsY);

            int y0Result = yMrca - _nullPersonsY,
                y1Result = yMrca - _firstPersonsY;

            label7.Text = y0Result.ToString();
            label8.Text = y1Result.ToString();

            label10.Text = _relationshipSelector.DetectRelationship(y0Result,
                y1Result, _relatives);

            if (_nullPersonsX == _firstPersonsX)
            {
                if (!((_nullPersonsX == 0 && _nullPersonsY >= 0) ||
                      (_firstPersonsX == 0 && _firstPersonsY >= 0)))
                {
                    int y0New = _nullPersonsY,
                        y1New = _firstPersonsY;

                    while (y0New < _nullPersonsX && y1New < _firstPersonsX)
                    {
                        yMrca = _mrcaSelector.SelectMrca(_nullPersonsX,
                            ++y0New,
                            _firstPersonsX,
                            ++y1New);

                        label10.Text += "\n" + _relationshipSelector.DetectRelationship(yMrca - _nullPersonsY,
                                            yMrca - _firstPersonsY,
                                            _relatives);
                    }
                }
            }

            if (((_nullPersonsX > 1) && (_firstPersonsX > 1)) ||
                ((_nullPersonsY > 0) && (_firstPersonsY > 0)) ||
                ((_nullPersonsY > 0) && (_firstPersonsX > 1) || (_firstPersonsY > 0) && (_nullPersonsX > 1)))
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