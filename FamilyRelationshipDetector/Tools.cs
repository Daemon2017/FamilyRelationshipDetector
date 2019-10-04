using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilyRelationshipDetector
{
    public class Tools
    {
        private readonly FileSaver _fileSaver = new FileSaver();

        public int GetYOfMRCA(int startX, int startY, int endX, int endY)
        {
            int yOfMRCA = 0;

            /*
             * Определение количества поколений до БОП.
             */
            if (startX > endX)
            {
                if (0 == endX)
                {
                    if (0 < startX && startX < endY)
                    {
                        yOfMRCA = endY;
                    }
                    else
                    {
                        yOfMRCA = startX;
                    }
                }
                else
                {
                    yOfMRCA = startX;
                }
            }
            else if (startX == endX)
            {
                yOfMRCA = startY >= endY ? startY : endY;
            }
            else if (startX < endX)
            {
                if (0 == startX)
                {
                    if (0 < endX && endX < startY)
                    {
                        yOfMRCA = startY;
                    }
                    else
                    {
                        yOfMRCA = endX;
                    }
                }
                else
                {
                    yOfMRCA = endX;
                }
            }

            return yOfMRCA;
        }

        public Relative GetRelationship(int distanceBetweenMrcaAndZeroPerson, int distanceBetweenMrcaAndFirstPerson,
            List<Relative> relatives)
        {
            /*
             * Определение степени родства между парой персон по данным о расстоянии от каждого из них до БОП.
             */
            if (0 == distanceBetweenMrcaAndFirstPerson)
            {
                return relatives.Where(rel =>
                rel.X == 0 &&
                rel.Y == distanceBetweenMrcaAndZeroPerson).Single();
            }
            else
            {
                return relatives.Where(rel =>
                rel.X == distanceBetweenMrcaAndZeroPerson &&
                rel.Y == distanceBetweenMrcaAndZeroPerson - distanceBetweenMrcaAndFirstPerson).Single();
            }
        }

        public List<Relative> GetPossibleRelationshipsList(int yOfMrca, 
            int numberOfGenerationsBetweenMrcaAndZeroRelative, int numberOfGenerationsBetweenMrcaAndFirstRelative,
            Relative _zeroRelative, Relative _firstRelative, 
            List<Relative> _relativesList)
        {
            /*
             * Определение основной степени родства.
             */
            List<Relative> possibleRelationshipsList = new List<Relative>
            {
                GetRelationship(
                    numberOfGenerationsBetweenMrcaAndZeroRelative,
                    numberOfGenerationsBetweenMrcaAndFirstRelative,
                    _relativesList)
            };

            /*
             * Определение дополнительных степеней родства, которые могут возникать от того, что 1-я и 2-я личности
             * находятся в одной вертикали.
             */
            if (_zeroRelative.X == _firstRelative.X &&
                !((_zeroRelative.X == 0 && _zeroRelative.Y >= 0) || (_firstRelative.X == 0 && _firstRelative.Y >= 0)))
            {
                int y0New = _zeroRelative.Y;
                int y1New = _firstRelative.Y;

                while (y0New < _zeroRelative.X && y1New < _firstRelative.X)
                {
                    try
                    {
                        yOfMrca = GetYOfMRCA(_zeroRelative.X, ++y0New, _firstRelative.X, ++y1New);
                        possibleRelationshipsList.Add(GetRelationship(
                            yOfMrca - _zeroRelative.Y,
                            yOfMrca - _firstRelative.Y,
                            _relativesList));
                    }
                    catch (InvalidOperationException)
                    {

                    }
                }
            }

            /*
             * Определение возможности отсутствия родства между 1-й и 2-й личностями. 
             */
            if (((_zeroRelative.X > 1) && (_firstRelative.X > 1)) ||
                ((_zeroRelative.Y > 0) && (_firstRelative.Y > 0)) ||
                ((_zeroRelative.Y > 0) && (_firstRelative.X > 1) || (_firstRelative.Y > 0) && (_zeroRelative.X > 1)))
            {
                possibleRelationshipsList.Add(_relativesList.Where(rel => rel.X == -1 && rel.Y == -1).Single());
            }

            return possibleRelationshipsList;
        }

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