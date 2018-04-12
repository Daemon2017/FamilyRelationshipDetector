using System.Windows.Forms;

namespace FamilyRelationshipDetector
{
    public partial class Form1 : Form
    {
        private string FindRelationship(int x, int y)
        {
            string relationship = "";

            foreach (var Relative in relatives)
            {
                if (Relative.Vertical == x && Relative.Horizontal == y)
                {
                    relationship = Relative.RelationNumber + ". [" + Relative.Vertical + ";" + Relative.Horizontal + "] " + Relative.RelationName;
                }
            }

            return relationship;
        }

        private string RelationshipSelector(int y0Result, int y1Result)
        {
            string relationship = "";

            if(0 == y1Result)
            {
                relationship = FindRelationship(0, y0Result);
            }
            else
            {
                relationship = FindRelationship(y0Result, y0Result + (-1) * y1Result);
            }           

            return relationship;
        }
    }
}