using System;
using System.IO;
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

        private void button60_Click(object sender, EventArgs e)
        {
            int i0 = 0,
                j0 = -3,
                i1 = 3,
                j1 = 3;

            string[,,] relationships = new string[(Math.Abs(i1 - i0) + 1) * (Math.Abs(j1 - j0) + 1), 
                                                  (Math.Abs(j1 - j0) + 1) * (Math.Abs(i1 - i0) + 1), 
                                                  10];
            
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
                    for (int iEnd = i0; 
                        iEnd <= i1; 
                        iEnd++)
                    {
                        for (int jEnd = j0; 
                            jEnd <= j1; 
                            jEnd++)
                        {
                            int jMRCA = MrcaSelector(iStart, jStart, iEnd, jEnd);

                            int jStartResult = jMRCA - jStart,
                                jEndResult = jMRCA - jEnd;

                            int k = 0;

                            relationships[i, j, k] = RelationshipSelector(jStartResult, jEndResult);

                            k++;

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

                            if (((iStart > 1) && (iEnd > 1)) ||
                                ((jStart > 0) && (jEnd > 0)) ||
                                ((jStart > 0) && (iEnd > 1) || (jEnd > 0) && (iStart > 1)))
                            {
                                relationships[i, j, k] = "0.";
                            }

                            j++;
                        }
                    }

                    i++;
                    j = 0;
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
