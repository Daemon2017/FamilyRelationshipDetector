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

        private readonly Tools _tools = new Tools();
        private readonly List<Relative> _relativesList = new List<Relative>();

        private Relative _zeroRelative, _firstRelative;

        public void GetConfig_OnLoad(object sender, EventArgs e)
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

            _tools.GetMatrices(usefulRelatives, _relativesList);
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

            _tools.GetMatrices(usefulRelatives, _relativesList);
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            if (_zeroRelative != null && _firstRelative != null)
            {
                int yMrca = _tools.SelectMrca(_zeroRelative, _firstRelative);

                int y0Result = yMrca - _zeroRelative.Y;
                int y1Result = yMrca - _firstRelative.Y;
                List<string> possibleRelationshipsList = _tools.GetPossibleRelationshipsList(yMrca, y0Result, y1Result, _zeroRelative, _firstRelative, _relativesList);

                label7.Text = y0Result.ToString();
                label8.Text = y1Result.ToString();
                label10.Text = string.Join(", ", possibleRelationshipsList.ToArray());
            }
        }
    }
}