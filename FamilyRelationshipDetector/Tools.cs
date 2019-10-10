using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilyRelationshipDetector
{
    public partial class Tools
    {
        private readonly FileSaver _fileSaver = new FileSaver();

        public void SaveMatricesToFiles(List<Relative> usefulRelatives, List<Relative> _relativesList)
        {
            usefulRelatives.Remove(_relativesList.Where(rel => rel.X == -1 && rel.Y == -1).Single());

            /*
             * Построение матрицы допустимых степеней родства.
             * Построение матрицы примерного количества общих сантиморган.
             */
            List<Relative>[,] relationshipsMatrix = new List<Relative>[usefulRelatives.Count, usefulRelatives.Count];
            List<string> centimorgansMatrix = new List<string>();
            List<string> xMatrix = new List<string>();
            List<string> yMatrix = new List<string>();

            int person = 0, relative = 0;

            foreach (var zeroRelative in usefulRelatives)
            {
                foreach (var firstRelative in usefulRelatives)
                {
                    int yOfMrca = GetYOfMRCA(zeroRelative.X, zeroRelative.Y, firstRelative.X, firstRelative.Y);

                    int numberOfGenerationsBetweenMrcaAndZeroRelative = yOfMrca - zeroRelative.Y,
                        numberOfGenerationsBetweenMrcaAndFirstRelative = yOfMrca - firstRelative.Y;

                    relationshipsMatrix[person, relative] = GetPossibleRelationshipsList(
                        yOfMrca,
                        numberOfGenerationsBetweenMrcaAndZeroRelative, numberOfGenerationsBetweenMrcaAndFirstRelative,
                        zeroRelative, firstRelative,
                        _relativesList);

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
    }
}