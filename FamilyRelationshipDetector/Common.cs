using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilyRelationshipDetector
{
    public partial class Tools
    {
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

        public RelationshipDegreeUI GetRelationship(int distanceBetweenMrcaAndZeroPerson, int distanceBetweenMrcaAndFirstPerson,
            List<RelationshipDegreeUI> relationshipDegreesList)
        {
            /*
             * Определение степени родства между парой персон по данным о расстоянии от каждого из них до БОП.
             */
            if (0 == distanceBetweenMrcaAndFirstPerson)
            {
                return relationshipDegreesList.Where(rel =>
                rel.X == 0 &&
                rel.Y == distanceBetweenMrcaAndZeroPerson).SingleOrDefault();
            }
            else
            {
                return relationshipDegreesList.Where(rel =>
                rel.X == distanceBetweenMrcaAndZeroPerson &&
                rel.Y == distanceBetweenMrcaAndZeroPerson - distanceBetweenMrcaAndFirstPerson).SingleOrDefault();
            }
        }

        public List<RelationshipDegreeUI> GetPossibleRelationshipsList(int yOfMrca,
            int numberOfGenerationsBetweenMrcaAndZeroRelative, int numberOfGenerationsBetweenMrcaAndFirstRelative,
            RelationshipDegreeUI _zeroRelationshipDegree, RelationshipDegreeUI _firstRelationshipDegree,
            List<RelationshipDegreeUI> _relationshipDegreesList)
        {
            /*
             * Определение основной степени родства.
             */
            List<RelationshipDegreeUI> possibleRelationshipDegreesList = new List<RelationshipDegreeUI>
            {
                GetRelationship(
                    numberOfGenerationsBetweenMrcaAndZeroRelative,
                    numberOfGenerationsBetweenMrcaAndFirstRelative,
                    _relationshipDegreesList)
            };

            /*
             * Определение дополнительных степеней родства, которые могут возникать от того, что 1-я и 2-я личности
             * находятся в одной вертикали.
             */
            if (_zeroRelationshipDegree.X == _firstRelationshipDegree.X &&
                !((_zeroRelationshipDegree.X == 0 && _zeroRelationshipDegree.Y >= 0) || (_firstRelationshipDegree.X == 0 && _firstRelationshipDegree.Y >= 0)))
            {
                int y0New = _zeroRelationshipDegree.Y;
                int y1New = _firstRelationshipDegree.Y;

                while (y0New < _zeroRelationshipDegree.X && y1New < _firstRelationshipDegree.X)
                {
                    try
                    {
                        yOfMrca = GetYOfMRCA(_zeroRelationshipDegree.X, ++y0New, _firstRelationshipDegree.X, ++y1New);
                        possibleRelationshipDegreesList.Add(GetRelationship(
                            yOfMrca - _zeroRelationshipDegree.Y,
                            yOfMrca - _firstRelationshipDegree.Y,
                            _relationshipDegreesList));
                    }
                    catch (InvalidOperationException)
                    {

                    }
                }
            }

            /*
             * Определение возможности отсутствия родства между 1-й и 2-й личностями. 
             */
            if (((_zeroRelationshipDegree.X > 1) && (_firstRelationshipDegree.X > 1)) ||
                ((_zeroRelationshipDegree.Y > 0) && (_firstRelationshipDegree.Y > 0)) ||
                ((_zeroRelationshipDegree.Y > 0) && (_firstRelationshipDegree.X > 1) || (_firstRelationshipDegree.Y > 0) && (_zeroRelationshipDegree.X > 1)))
            {
                possibleRelationshipDegreesList.Add(_relationshipDegreesList.Where(rel => rel.X == -1 && rel.Y == -1).Single());
            }

            return possibleRelationshipDegreesList;
        }
    }
}
