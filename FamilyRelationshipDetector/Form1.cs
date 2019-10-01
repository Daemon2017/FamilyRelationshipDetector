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
            int maxHorizontal = horizonatal.Concat(new[] { 0 }).Max();

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
                _nullPersonsX = ((Relative)sender).X;
                _nullPersonsY = ((Relative)sender).Y;
                label2.Text = ((Relative)sender).RelationName;
            }
            else if (e.Button == MouseButtons.Right)
            {
                _firstPersonsX = ((Relative)sender).X;
                _firstPersonsY = ((Relative)sender).Y;
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

            foreach (var possibleRelative in _relatives)
            {
                if (possibleRelative.X >= minX && possibleRelative.X <= maxX
                                               && possibleRelative.Y >= minY && possibleRelative.Y <= maxY)
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

            foreach (var possibleRelative in _relatives)
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
                        _relationshipSelector.DetectRelationship(numberOfGenerationsBetweenMrcaAndFirstRelative,
                            numberOfGenerationsBetweenMrcaAndSecondRelative, _relatives)
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
                                    secondRelative.X, ++j1New);

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
                        ((firstRelative.Y > 0) && (secondRelative.X > 1) ||
                         (secondRelative.Y > 0) && (firstRelative.X > 1)))
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

                foreach (var usefulRelative in usefulRelatives)
                {
                    if (usefulRelative.X.Equals(firstRelative.X) && usefulRelative.Y.Equals(firstRelative.Y))
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

            foreach (var rel in _relatives)
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

            foreach (var rel in _relatives)
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

            if (_nullPersonsX == _firstPersonsX &&
                !((_nullPersonsX == 0 && _nullPersonsY >= 0) || (_firstPersonsX == 0 && _firstPersonsY >= 0)))
            {
                int y0New = _nullPersonsY,
                    y1New = _firstPersonsY;

                while (y0New < _nullPersonsX &&
                    y1New < _firstPersonsX)
                {
                    yMrca = _mrcaSelector.SelectMrca(_nullPersonsX, ++y0New, _firstPersonsX, ++y1New);

                    label10.Text += "\n" + _relationshipSelector.DetectRelationship(
                        yMrca - _nullPersonsY,
                        yMrca - _firstPersonsY,
                        _relatives);
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