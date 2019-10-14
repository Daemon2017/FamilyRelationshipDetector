using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilyRelationshipDetector
{
    public partial class Tools
    {
        private readonly FileSaver _fileSaver = new FileSaver();

        public void SaveMatricesToFiles(List<RelationshipDegree> usefulRelationshipDegreesList, List<RelationshipDegree> _relationshipDegreesList)
        {
            usefulRelationshipDegreesList.Remove(_relationshipDegreesList.Where(rel => rel.X == -1 && rel.Y == -1).Single());

            /*
             * Построение матрицы допустимых степеней родства.
             * Построение матрицы примерного количества общих сантиморган.
             */
            List<RelationshipDegree>[,] relationshipDegreesMatrix = new List<RelationshipDegree>[usefulRelationshipDegreesList.Count, usefulRelationshipDegreesList.Count];
            List<string> centimorgansMatrix = new List<string>();
            List<string> xMatrix = new List<string>();
            List<string> yMatrix = new List<string>();

            int person = 0, relative = 0;

            foreach (var zeroRelative in usefulRelationshipDegreesList)
            {
                foreach (var firstRelative in usefulRelationshipDegreesList)
                {
                    int yOfMrca = GetYOfMRCA(zeroRelative.X, zeroRelative.Y, firstRelative.X, firstRelative.Y);

                    int numberOfGenerationsBetweenMrcaAndZeroRelative = yOfMrca - zeroRelative.Y,
                        numberOfGenerationsBetweenMrcaAndFirstRelative = yOfMrca - firstRelative.Y;

                    relationshipDegreesMatrix[person, relative] = GetPossibleRelationshipsList(
                        yOfMrca,
                        numberOfGenerationsBetweenMrcaAndZeroRelative, numberOfGenerationsBetweenMrcaAndFirstRelative,
                        zeroRelative, firstRelative,
                        _relationshipDegreesList);

                    relative++;
                }

                /*
                 * Определение ожидаемого количества общих сантиморган с каждой из степеней родства.
                 */
                foreach (var usefulRelative in usefulRelationshipDegreesList)
                {
                    if (usefulRelative.X.Equals(zeroRelative.X) && usefulRelative.Y.Equals(zeroRelative.Y))
                    {
                        centimorgansMatrix.Add(usefulRelative.CommonCm.ToString());
                    }
                }

                foreach (var usefulRelative in usefulRelationshipDegreesList)
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

            _fileSaver.SaveToFile("relationships.csv", relationshipDegreesMatrix);
            _fileSaver.SaveToFile("centimorgans.csv", centimorgansMatrix);
            _fileSaver.SaveToFile("ys.csv", yMatrix);
            _fileSaver.SaveToFile("xs.csv", xMatrix);

            /*
             * Построение матрицы максимального числа предков каждого вида.
             */
            List<List<string>> ancestorsMatrix = new List<List<string>>();

            foreach (var rel in _relationshipDegreesList)
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
            List<string> descendantsMatrix = new List<string>();

            foreach (var rel in _relationshipDegreesList)
            {
                if (rel.X.Equals(0) && rel.Y < 0)
                {
                    descendantsMatrix.Add(rel.RelationNumber.ToString());
                }
            }

            _fileSaver.SaveToFile("descendantsMatrix.csv", descendantsMatrix);
        }
    }
}