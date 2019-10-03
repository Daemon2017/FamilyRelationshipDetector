using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilyRelationshipDetector
{
    public class Tools
    {
        private readonly FileSaver _fileSaver = new FileSaver();

        public int GetNumberOfGenerationOfMRCA(Relative start, Relative end)
        {
            int numberOfGenerationOfMRCA = 0;

            /*
             * Определение количества поколений до БОП.
             */
            if (start.X > end.X)
            {
                if (0 == end.X)
                {
                    if (0 < start.X && start.X < end.Y)
                    {
                        numberOfGenerationOfMRCA = end.Y;
                    }
                    else
                    {
                        numberOfGenerationOfMRCA = start.X;
                    }
                }
                else
                {
                    numberOfGenerationOfMRCA = start.X;
                }
            }
            else if (start.X == end.X)
            {
                numberOfGenerationOfMRCA = start.Y >= end.Y ? start.Y : end.Y;
            }
            else if (start.X < end.X)
            {
                if (0 == start.X)
                {
                    if (0 < end.X && end.X < start.Y)
                    {
                        numberOfGenerationOfMRCA = start.Y;
                    }
                    else
                    {
                        numberOfGenerationOfMRCA = end.X;
                    }
                }
                else
                {
                    numberOfGenerationOfMRCA = end.X;
                }
            }

            return numberOfGenerationOfMRCA;
        }

        public string GetTypeOfRelationship(int x, int y, List<Relative> relatives)
        {
            string foundRelationship = "";

            /*
             * Поиск степени родства, имеющей соответствующие координаты по X и Y.
             */
            foreach (var relative in relatives)
            {
                if (relative.X == x &&
                    relative.Y == y)
                {
                    foundRelationship = relative.RelationNumber + ". [" + relative.X + ";" + relative.Y + "] " +
                                        relative.RelationName;
                }
            }

            return foundRelationship;
        }

        public string GetRelationship(int distanceBetweenMrcaAndNullPerson, int distanceBetweenMrcaAndFirstPerson,
            List<Relative> relatives)
        {
            string typeOfRelationship;

            /*
             * Определение степени родства между парой персон по данным о расстоянии от каждого из них до БОП.
             */
            if (0 == distanceBetweenMrcaAndFirstPerson)
            {
                typeOfRelationship = GetTypeOfRelationship(0, distanceBetweenMrcaAndNullPerson, relatives);
            }
            else
            {
                typeOfRelationship = GetTypeOfRelationship(distanceBetweenMrcaAndNullPerson,
                    distanceBetweenMrcaAndNullPerson - distanceBetweenMrcaAndFirstPerson,
                    relatives);
            }

            return typeOfRelationship;
        }

        public List<string> GetPossibleRelationshipsList(int yMrca, int y0Result, int y1Result,
            Relative _zeroRelative, Relative _firstRelative, List<Relative> _relativesList)
        {
            List<string> possibleRelationshipsList = new List<string>
            {
                GetRelationship(y0Result, y1Result, _relativesList)
            };

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
                        yMrca = GetNumberOfGenerationOfMRCA(
                            _relativesList.Where(rel => rel.X == _zeroRelative.X && rel.Y == zeroY).Single(),
                            _relativesList.Where(rel => rel.X == _firstRelative.X && rel.Y == firstY).Single());
                    }
                    catch (InvalidOperationException)
                    {

                    }

                    possibleRelationshipsList.Add("\n" + GetRelationship(yMrca - _zeroRelative.Y, yMrca - _firstRelative.Y, _relativesList));
                }
            }

            if (((_zeroRelative.X > 1) && (_firstRelative.X > 1)) ||
                ((_zeroRelative.Y > 0) && (_firstRelative.Y > 0)) ||
                ((_zeroRelative.Y > 0) && (_firstRelative.X > 1) || (_firstRelative.Y > 0) && (_zeroRelative.X > 1)))
            {
                possibleRelationshipsList.Add("\nРодства нет.");
            }

            if ((0 > y0Result) || (0 > y1Result))
            {
                possibleRelationshipsList.Add("Ошибка!");
            }

            return possibleRelationshipsList;
        }

        public void SaveMatricesToFiles(List<Relative> usefulRelatives, List<Relative> _relativesList)
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
                    int numberOfGenerationOfMrca = GetNumberOfGenerationOfMRCA(zeroRelative, firstRelative);

                    int numberOfGenerationsBetweenMrcaAndFirstRelative = numberOfGenerationOfMrca - zeroRelative.Y,
                        numberOfGenerationsBetweenMrcaAndSecondRelative = numberOfGenerationOfMrca - firstRelative.Y;

                    /*
                     * Определение основной степени родства.
                     */
                    relationshipsMatrix[person, relative] = new List<string>
                    {
                        GetRelationship(
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
                                numberOfGenerationOfMrca = GetNumberOfGenerationOfMRCA(
                                    _relativesList.Where(rel => rel.X == zeroRelative.X && rel.Y == zeroY).Single(),
                                    _relativesList.Where(rel => rel.X == firstRelative.X && rel.Y == firstY).Single());
                            }
                            catch (InvalidOperationException)
                            {

                            }

                            relationshipsMatrix[person, relative].Add(GetRelationship(
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
    }
}