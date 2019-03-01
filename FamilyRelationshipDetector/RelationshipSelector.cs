namespace FamilyRelationshipDetector
{
    public partial class Form1
    {
        private string FindTypeOfRelationship(int x, int y)
        {
            string foundRelationship = "";

            /*
             * Поиск степени родства, имеющей соответствующие координаты по X и Y.
             */
            foreach (var relative in _relatives)
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

        private string DetectRelationship(int distanceBetweenMrcaAndNullPerson, int distanceBetweenMrcaAndFirstPerson)
        {
            string typeOfRelationship;

            /*
             * Определение степени родства между парой персон по данным о расстоянии от каждого из них до БОП.
             */
            if (0 == distanceBetweenMrcaAndFirstPerson)
            {
                typeOfRelationship = FindTypeOfRelationship(0, distanceBetweenMrcaAndNullPerson);
            }
            else
            {
                typeOfRelationship = FindTypeOfRelationship(distanceBetweenMrcaAndNullPerson,
                    distanceBetweenMrcaAndNullPerson - distanceBetweenMrcaAndFirstPerson);
            }

            return typeOfRelationship;
        }
    }
}