using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FamilyRelationshipDetector
{
    public partial class Form1 : Form
    {
        private void LoadFromFile(object sender, EventArgs e)
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

        private void SaveToFile(string outputFileName, string[] dataArray)
        {
            using (StreamWriter outfile = new StreamWriter(@"centimorgans.csv"))
            {
                for (int line = 0;
                    line < dataArray.GetLength(0);
                    line++)
                {
                    outfile.WriteLine(dataArray[line].Replace(",", "."));
                }
            }
        }

        private void SaveToFile(string outputFileName, List<string>[] dataArray)
        {
            using (StreamWriter outfile = new StreamWriter(outputFileName))
            {
                for (int line = 0;
                    line < dataArray.GetLength(0);
                    line++)
                {
                    string content = "";

                    foreach(var column in dataArray[line])
                    {
                        if (column != null)
                        {
                            content += column + ",";
                        }
                    }

                    if (content != "")
                    {
                        content = content.Remove(content.Length - 1);
                    }

                    outfile.WriteLine(content);
                }
            }
        }

        private void SaveToFile(string outputFileName, List<string>[,] dataArray)
        {
            using (StreamWriter outfile = new StreamWriter(@"relationships.csv"))
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
                        foreach(var cell in dataArray[line, column])
                        {
                            if (cell != null && cell != "")
                            {
                                content += cell.Substring(0, cell.IndexOf(".")) + ";";
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
