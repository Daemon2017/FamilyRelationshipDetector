using System;
using System.Collections.Generic;
using System.IO;

namespace FamilyRelationshipDetector
{
    public class FileSaver
    {
        public void SaveToFile(string outputFileName, List<List<string>> dataArray)
        {
            using (StreamWriter outfile = new StreamWriter(outputFileName))
            {
                foreach (var line in dataArray)
                {
                    outfile.WriteLine(line[0] + "," + line[1]);
                }
            }
        }

        public void SaveToFile(string outputFileName, List<string> dataArray)
        {
            using (StreamWriter outfile = new StreamWriter(outputFileName))
            {
                foreach (var line in dataArray)
                {
                    outfile.WriteLine(line);
                }
            }
        }

        public void SaveToFile(string outputFileName, List<string>[,] dataArray)
        {
            using (StreamWriter outfile = new StreamWriter(outputFileName))
            {
                for (int line = 0; line < dataArray.GetLength(0); line++)
                {
                    string content = "";

                    for (int column = 0; column < dataArray.GetLength(1); column++)
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