using System.Windows.Forms;

namespace FamilyRelationshipDetector
{
    public partial class Form1 : Form
    {
        private string FindTypeOfRelationship(int X, int Y)
        {
            string foundRelationship = "";

            /*
             * Поиск степени родства, имеющей соответствующие координаты по X и Y.
             */
            foreach (var Relative in relatives)
            {
                if (Relative.X == X &&
                    Relative.Y == Y)
                {
                    foundRelationship = Relative.RelationNumber + ". [" + Relative.X + ";" + Relative.Y + "] " + Relative.RelationName;
                }
            }

            return foundRelationship;
        }

        private string DetectRelationship(int distanceBetweenMrcaAndNullPerson, int distanceBetweenMrcaAndFirstPerson)
        {
            string typeOfRelationship = "";

            /*
             * Определение степени родства между парой персон по данным о расстоянии от каждого из них до БОП.
             */
            if (0 == distanceBetweenMrcaAndFirstPerson)
            {
                typeOfRelationship = FindTypeOfRelationship(0, distanceBetweenMrcaAndNullPerson);
            }
            else
            {
                typeOfRelationship = FindTypeOfRelationship(distanceBetweenMrcaAndNullPerson, distanceBetweenMrcaAndNullPerson - distanceBetweenMrcaAndFirstPerson);
            }

            return typeOfRelationship;
        }
    }
}