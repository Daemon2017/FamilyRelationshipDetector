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
        private readonly List<Relative> _relativesList = new List<Relative>();

        private Relative _zeroRelative, _firstRelative;

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
            for (int i = 0; i < numberOfRows; i++)
            {
                horizonatal[i] = Convert.ToInt16(relativesMatrix[i, 2]);
            }

            /*
             * Поиск наибольшей горизонтали.
             */
            int maxHorizontal = horizonatal.Concat(new[] { 0 }).Max();

            for (int i = 0; i < numberOfRows; i++)
            {
                Relative newRelative = new Relative(
                    Convert.ToInt16(relativesMatrix[i, 0]),
                    Convert.ToInt16(relativesMatrix[i, 1]),
                    Convert.ToInt16(relativesMatrix[i, 2]),
                    relativesMatrix[i, 3],
                    Convert.ToInt16(relativesMatrix[i, 4]),
                    Convert.ToInt16(relativesMatrix[i, 5]),
                    Convert.ToDouble(relativesMatrix[i, 6]),
                    maxHorizontal);
                newRelative.MouseDown += RelativeButton_MouseDown;
                _relativesList.Add(newRelative);
                panel2.Controls.Add(newRelative);
            }
        }

        private void RelativeButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _zeroRelative = (Relative)sender;
                label2.Text = ((Relative)sender).RelationName;
            }
            else if (e.Button == MouseButtons.Right)
            {
                _firstRelative = (Relative)sender;
                label4.Text = ((Relative)sender).RelationName;
            }
        }

        private void GenerateSquareButton_Click(object sender, EventArgs e)
        {
            int minX = Convert.ToInt16(textBox1.Text);
            int minY = Convert.ToInt16(textBox2.Text);
            int maxX = Convert.ToInt16(textBox4.Text);
            int maxY = Convert.ToInt16(textBox3.Text);

            List<Relative> usefulRelatives = new List<Relative>();

            foreach (var possibleRelative in _relativesList)
            {
                if (possibleRelative.X >= minX && possibleRelative.X <= maxX &&
                    possibleRelative.Y >= minY && possibleRelative.Y <= maxY)
                {
                    usefulRelatives.Add(possibleRelative);
                }
            }

            GetMatrices(usefulRelatives);
        }

        private void GenerateDiagonalButton_Click(object sender, EventArgs e)
        {
            int clusterNumber = Convert.ToInt16(textBox5.Text);

            /*
             * Составление списка X;Y, входящих в кластер.
             */
            List<Relative> usefulRelatives = new List<Relative>();

            foreach (var possibleRelative in _relativesList)
            {
                if (possibleRelative.ClusterNumber <= clusterNumber)
                {
                    usefulRelatives.Add(possibleRelative);
                }
            }

            GetMatrices(usefulRelatives);
        }

        private void GetMatrices(List<Relative> usefulRelatives)
        {
            /*
             * Построение матрицы допустимых степеней родства.
             * Построение матрицы примерного количества общих сантиморган.
             */
            List<string>[,] relationshipsMatrix = new List<string>[usefulRelatives.Count, usefulRelatives.Count];
            List<string> centimorgansMatrix = new List<string>();
            List<string> xMatrix = new List<string>();
            List<string> yMatrix = new List<string>();

            int person = 0, relative = 0;

            foreach (var zeroRelative in usefulRelatives)
            {
                foreach (var firstRelative in usefulRelatives)
                {
                    int numberOfGenerationOfMrca = _mrcaSelector.SelectMrca(zeroRelative, firstRelative);

                    int numberOfGenerationsBetweenMrcaAndFirstRelative = numberOfGenerationOfMrca - zeroRelative.Y,
                        numberOfGenerationsBetweenMrcaAndSecondRelative = numberOfGenerationOfMrca - firstRelative.Y;

                    /*
                     * Определение основной степени родства.
                     */
                    relationshipsMatrix[person, relative] = new List<string>
                    {
                        _relationshipSelector.DetectRelationship(
                            numberOfGenerationsBetweenMrcaAndFirstRelative,
                            numberOfGenerationsBetweenMrcaAndSecondRelative,
                            _relativesList)
                    };

                    /*
                     * Определение дополнительных степеней родства, которые могут возникать от того, что 1-я и 2-я личности
                     * находятся в одной вертикали.
                     */
                    if (zeroRelative.X == firstRelative.X &&
                        !((zeroRelative.X == 0 && zeroRelative.Y >= 0) || (firstRelative.X == 0 && firstRelative.Y >= 0)))
                    {
                        int j0New = zeroRelative.Y,
                            j1New = firstRelative.Y;

                        while (j0New < zeroRelative.X && j1New < firstRelative.X)
                        {
                            int zeroY = ++j0New;
                            int firstY = ++j1New;

                            try
                            {
                                numberOfGenerationOfMrca = _mrcaSelector.SelectMrca(
                                    _relativesList.Where(rel => rel.X == zeroRelative.X && rel.Y == zeroY).Single(),
                                    _relativesList.Where(rel => rel.X == firstRelative.X && rel.Y == firstY).Single());
                            }
                            catch (InvalidOperationException)
                            {

                            }

                            relationshipsMatrix[person, relative].Add(_relationshipSelector.DetectRelationship(
                                    numberOfGenerationOfMrca - zeroRelative.Y,
                                    numberOfGenerationOfMrca - firstRelative.Y,
                                    _relativesList));
                        }
                    }

                    /*
                     * Определение возможности отсутствия родства между 1-й и 2-й личностями. 
                     */
                    if (((zeroRelative.X > 1) && (firstRelative.X > 1)) ||
                        ((zeroRelative.Y > 0) && (firstRelative.Y > 0)) ||
                        ((zeroRelative.Y > 0) && (firstRelative.X > 1) || (firstRelative.Y > 0) && (zeroRelative.X > 1)))
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
                    if (usefulRelative.X.Equals(zeroRelative.X) && usefulRelative.Y.Equals(zeroRelative.Y))
                    {
                        centimorgansMatrix.Add(usefulRelative.CommonCm.ToString());
                    }
                }

                foreach (var usefulRelative in usefulRelatives)
                {
                    if (usefulRelative.X.Equals(zeroRelative.X) && usefulRelative.Y.Equals(zeroRelative.Y))
                    {
                        yMatrix.Add(usefulRelative.Y.ToString());
                        xMatrix.Add(usefulRelative.X.ToString());
                    }
                }

                person++;
                relative = 0;
            }

            _fileSaver.SaveToFile("relationships.csv", relationshipsMatrix);
            _fileSaver.SaveToFile("centimorgans.csv", centimorgansMatrix);
            _fileSaver.SaveToFile("ys.csv", yMatrix);
            _fileSaver.SaveToFile("xs.csv", xMatrix);

            /*
             * Построение матрицы максимального числа предков каждого вида.
             */
            List<List<string>> ancestorsMatrix = new List<List<string>>();

            foreach (var rel in _relativesList)
            {
                if (rel.X.Equals(0) && rel.Y > 0)
                {
                    ancestorsMatrix.Add(new List<string>
                    {
                        rel.RelationNumber.ToString(),
                        Math.Pow(2, rel.Y).ToString()
                    });
                }
            }

            _fileSaver.SaveToFile("ancestorsMatrix.csv", ancestorsMatrix);

            /*
             * Построение списка потомков пробанда, его сиблингов и их потомков
             */
            List<string> siblindantsMatrix = new List<string>();

            foreach (var rel in _relativesList)
            {
                if ((rel.X.Equals(0) && rel.Y < 0) || (rel.X.Equals(1) && rel.Y <= 0))
                {
                    siblindantsMatrix.Add(rel.RelationNumber.ToString());
                }
            }

            _fileSaver.SaveToFile("siblindantsMatrix.csv", siblindantsMatrix);
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            if (_zeroRelative != null && _firstRelative != null)
            {
                int yMrca = _mrcaSelector.SelectMrca(_zeroRelative, _firstRelative);

                int y0Result = yMrca - _zeroRelative.Y,
                    y1Result = yMrca - _firstRelative.Y;

                label7.Text = y0Result.ToString();
                label8.Text = y1Result.ToString();

                label10.Text = _relationshipSelector.DetectRelationship(y0Result,
                    y1Result, _relativesList);

                if (_zeroRelative.X == _firstRelative.X && !((_zeroRelative.X == 0 && _zeroRelative.Y >= 0) || (_firstRelative.X == 0 && _firstRelative.Y >= 0)))
                {
                    int y0New = _zeroRelative.Y,
                        y1New = _firstRelative.Y;

                    while (y0New < _zeroRelative.X && y1New < _firstRelative.X)
                    {
                        int zeroY = ++y0New;
                        int firstY = ++y1New;

                        try
                        {
                            yMrca = _mrcaSelector.SelectMrca(
                                _relativesList.Where(rel => rel.X == _zeroRelative.X && rel.Y == zeroY).Single(),
                                _relativesList.Where(rel => rel.X == _firstRelative.X && rel.Y == firstY).Single());
                        }
                        catch (InvalidOperationException)
                        {

                        }

                        label10.Text += "\n" + _relationshipSelector.DetectRelationship(
                            yMrca - _zeroRelative.Y,
                            yMrca - _firstRelative.Y,
                            _relativesList);
                    }
                }

                if (((_zeroRelative.X > 1) && (_firstRelative.X > 1)) ||
                    ((_zeroRelative.Y > 0) && (_firstRelative.Y > 0)) ||
                    ((_zeroRelative.Y > 0) && (_firstRelative.X > 1) || (_firstRelative.Y > 0) && (_zeroRelative.X > 1)))
                {
                    label10.Text += "\nРодства нет.";
                }

                if ((0 > y0Result) || (0 > y1Result))
                {
                    label10.Text = "Ошибка!";
                }
            }
        }
    }
}