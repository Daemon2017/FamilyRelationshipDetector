﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FamilyRelationshipDetector
{
    public partial class Form1
    {
        private void LoadFromFile(object sender, EventArgs e)
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

        private void SaveToFile(string outputFileName, List<List<string>> dataArray)
        {
            using (StreamWriter outfile = new StreamWriter(outputFileName))
            {
                foreach (var line in dataArray)
                {
                    outfile.WriteLine(line[0] + "," + line[1]);
                }
            }
        }

        private void SaveToFile(string outputFileName, List<string> dataArray)
        {
            using (StreamWriter outfile = new StreamWriter(outputFileName))
            {
                foreach (var line in dataArray)
                {
                    outfile.WriteLine(line);
                }
            }
        }

        private void SaveToFile(string outputFileName, List<string>[,] dataArray)
        {
            using (StreamWriter outfile = new StreamWriter(outputFileName))
            {
                for (int line = 0;
                    line < dataArray.GetLength(0);
                    line++)
                {
                    string content = "";

                    for (int column = 0;
                        column < dataArray.GetLength(1);
                        column++)
                    {
                        foreach (var cell in dataArray[line, column])
                        {
                            if (!string.IsNullOrEmpty(cell))
                            {
                                content += cell.Substring(0, cell.IndexOf(".", StringComparison.Ordinal)) + ";";
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
        }
    }
}