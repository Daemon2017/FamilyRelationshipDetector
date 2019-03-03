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

        private void GenerateSquare(object sender, EventArgs e)
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

                for (int firstRelativeX = minX;
                    firstRelativeX <= maxX;
                    firstRelativeX++)
                {
                    for (int firstRelativeY = minY;
                        firstRelativeY <= maxY;
                        firstRelativeY++)
                    {
                        /*
                         * Исключение повторов степеней родства, 
                         * занимающих более одной вертикали.
                         */
                        if (!(firstRelativeY > 0 && (firstRelativeX > 0 && firstRelativeX <= firstRelativeY)))
                        {
                            for (int secondRelativeX = minX;
                                secondRelativeX <= maxX;
                                secondRelativeX++)
                            {
                                for (int secondRelativeY = minY;
                                    secondRelativeY <= maxY;
                                    secondRelativeY++)
                                {
                                    /*
                                     * Исключение повторов степеней родства, 
                                     * занимающих более одной вертикали.
                                     */
                                    if (!(secondRelativeY > 0 && (secondRelativeX > 0 && secondRelativeX <= secondRelativeY)))
                                    {
                                        int numberOfGenerationOfMrca = _mrcaSelector.SelectMrca(firstRelativeX,
                                            firstRelativeY,
                                            secondRelativeX,
                                            secondRelativeY);

                                        int jStartResult = numberOfGenerationOfMrca - firstRelativeY,
                                            jEndResult = numberOfGenerationOfMrca - secondRelativeY;

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
                                        if (firstRelativeX == secondRelativeX)
                                        {
                                            if (!((firstRelativeX == 0 && firstRelativeY >= 0) ||
                                                  (secondRelativeX == 0 && secondRelativeY >= 0)))
                                            {
                                                int j0New = firstRelativeY,
                                                    j1New = secondRelativeY;

                                                while (j0New < firstRelativeX && j1New < secondRelativeX)
                                                {
                                                    numberOfGenerationOfMrca = _mrcaSelector.SelectMrca(firstRelativeX,
                                                        ++j0New,
                                                        secondRelativeX,
                                                        ++j1New);

                                                    relationshipsMatrix[person, relative].Add(
                                                        _relationshipSelector.DetectRelationship(
                                                            numberOfGenerationOfMrca - firstRelativeY,
                                                            numberOfGenerationOfMrca - secondRelativeY,
                                                            _relatives));
                                                }
                                            }
                                        }

                                        /*
                                         * Обработка расклада, когда между первичной и вторичной личностями может не быть родства.
                                         */
                                        if (((firstRelativeX > 1) && (secondRelativeX > 1)) ||
                                            ((firstRelativeY > 0) && (secondRelativeY > 0)) ||
                                            ((firstRelativeY > 0) && (secondRelativeX > 1) || (secondRelativeY > 0) && (firstRelativeX > 1)))
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
                                if (rel.X.Equals(firstRelativeX) && rel.Y.Equals(firstRelativeY))
                                {
                                    centimorgansMatrix.Add(rel.CommonCm.ToString());
                                }
                            }

                            person++;
                            relative = 0;
                        }
                    }
                }

                _fileSaver.SaveToFile("relationshipsSquare.csv", relationshipsMatrix);
                _fileSaver.SaveToFile("centimorgansSquare.csv", centimorgansMatrix);

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

                _fileSaver.SaveToFile("maxCountSquare.csv", maxCountMatrix);
            }
            else
            {
                MessageBox.Show("Конечные значения X и Y должны превышать их начальные значения!",
                    "Ошибка начальных/конечных значений",
                    MessageBoxButtons.OK);
            }
        }

        private void GenerateDiagonal(object sender, EventArgs e)
        {
            int clusterNumber = Convert.ToInt16(textBox5.Text);

            /*
             * Составление списка X;Y, входящих в кластер.
             */
            List<Relative> usefulRelatives = new List<Relative>();

            foreach (var possibleRelative in _relatives)
            {
                if (possibleRelative.ClusterNumber <= clusterNumber)
                {
                    usefulRelatives.Add(possibleRelative);
                }
            }

            /*
             * Построение матрицы допустимых степеней родства.
             * Построение матрицы примерного количества общих сантиморган.
             */
            List<string>[,] relationshipsMatrix = new List<string>[usefulRelatives.Count, usefulRelatives.Count];
            List<string> centimorgansMatrix = new List<string>();

            int person = 0,
                relative = 0;

            foreach (var firstRelative in usefulRelatives)
            {
                foreach (var secondRelative in usefulRelatives)
                {
                    int numberOfGenerationOfMrca = _mrcaSelector.SelectMrca(firstRelative.X, firstRelative.Y,
                        secondRelative.X, secondRelative.Y);

                    int numberOfGenerationsBetweenMrcaAndFirstRelative = numberOfGenerationOfMrca - firstRelative.Y,
                        numberOfGenerationsBetweenMrcaAndSecondRelative = numberOfGenerationOfMrca - secondRelative.Y;

                    /*
                     * Определение основной степени родства.
                     */
                    relationshipsMatrix[person, relative] = new List<string>
                    {
                        _relationshipSelector.DetectRelationship(numberOfGenerationsBetweenMrcaAndFirstRelative, numberOfGenerationsBetweenMrcaAndSecondRelative, _relatives)
                    };

                    /*
                     * Определение дополнительных степеней родства, которые могут возникать от того, что 1-я и 2-я личности
                     * находятся в одной вертикали.
                     */
                    if (firstRelative.X == secondRelative.X)
                    {
                        if (!((firstRelative.X == 0 && firstRelative.Y >= 0) ||
                              (secondRelative.X == 0 && secondRelative.Y >= 0)))
                        {
                            int j0New = firstRelative.Y,
                                j1New = secondRelative.Y;

                            while (j0New < firstRelative.X && j1New < secondRelative.X)
                            {
                                numberOfGenerationOfMrca = _mrcaSelector.SelectMrca(firstRelative.X, ++j0New,
                                    secondRelative.X,++j1New);

                                relationshipsMatrix[person, relative].Add(
                                    _relationshipSelector.DetectRelationship(numberOfGenerationOfMrca - firstRelative.Y, 
                                        numberOfGenerationOfMrca - secondRelative.Y, 
                                        _relatives));
                            }
                        }
                    }

                    /*
                     * Определение возможности отсутствия родства между 1-й и 2-й личностями. 
                     */
                    if (((firstRelative.X > 1) && (secondRelative.X > 1)) ||
                        ((firstRelative.Y > 0) && (secondRelative.Y > 0)) ||
                        ((firstRelative.Y > 0) && (secondRelative.X > 1) || (secondRelative.Y > 0) && (firstRelative.X > 1)))
                    {
                        relationshipsMatrix[person, relative].Add("0.");
                    }

                    relative++;
                }

                /*
                 * Определение ожидаемого количества общих сантиморган с каждой из степеней родства.
                 */
                foreach (var usefulRelative in usefulRelatives)
                {
                    if (usefulRelative.X.Equals(firstRelative.X) && usefulRelative.Y.Equals(firstRelative.Y))
                    {
                        centimorgansMatrix.Add(usefulRelative.CommonCm.ToString());
                    }
                }

                person++;
                relative = 0;
            }

            _fileSaver.SaveToFile("relationshipsDiagonal.csv", relationshipsMatrix);
            _fileSaver.SaveToFile("centimorgansDiagonal.csv", centimorgansMatrix);
            
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

            _fileSaver.SaveToFile("maxCountDiagonal.csv", maxCountMatrix);
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