using System.Collections.Generic;

namespace FamilyRelationshipDetector
{
    public class RelationshipSelector
    {
        public string FindTypeOfRelationship(int x, int y, List<Relative> relatives)
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

        public string DetectRelationship(int distanceBetweenMrcaAndNullPerson, int distanceBetweenMrcaAndFirstPerson,
            List<Relative> relatives)
        {
            string typeOfRelationship;

            /*
             * Определение степени родства между парой персон по данным о расстоянии от каждого из них до БОП.
             */
            if (0 == distanceBetweenMrcaAndFirstPerson)
            {
                typeOfRelationship = FindTypeOfRelationship(0, distanceBetweenMrcaAndNullPerson, relatives);
            }
            else
            {
                typeOfRelationship = FindTypeOfRelationship(distanceBetweenMrcaAndNullPerson,
                    distanceBetweenMrcaAndNullPerson - distanceBetweenMrcaAndFirstPerson,
                    relatives);
            }

            return typeOfRelationship;
        }
    }
}