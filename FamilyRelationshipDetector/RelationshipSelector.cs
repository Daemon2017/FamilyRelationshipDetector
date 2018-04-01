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
            string relationship = "?.";

            if (0 == y0Result)
            {
                if (0 == y1Result)
                {
                    relationship = null;
                }
                else if (1 == y1Result)
                {
                    relationship = FindRelationship(0, -1);
                }
                else if (2 == y1Result)
                {
                    relationship = FindRelationship(0, -2);
                }
                else if (3 == y1Result)
                {
                    relationship = FindRelationship(0, -3);
                }
                else if (4 == y1Result)
                {
                    relationship = FindRelationship(0, -4);
                }
                else if (5 == y1Result)
                {
                    relationship = FindRelationship(0, -5);
                }
                else if (6 == y1Result)
                {
                    relationship = FindRelationship(0, -6);
                }
                else if (7 == y1Result)
                {
                    relationship = FindRelationship(0, -7);
                }
                else if (8 == y1Result)
                {
                    relationship = FindRelationship(0, -8);
                }
                else if (9 == y1Result)
                {
                    relationship = FindRelationship(0, -9);
                }
            }
            else if (1 == y0Result)
            {
                if (0 == y1Result)
                {
                    relationship = FindRelationship(0, 1);
                }
                else if (1 == y1Result)
                {
                    relationship = FindRelationship(1, 0);
                }
                else if (2 == y1Result)
                {
                    relationship = FindRelationship(1, -1);
                }
                else if (3 == y1Result)
                {
                    relationship = FindRelationship(1, -2);
                }
                else if (4 == y1Result)
                {
                    relationship = FindRelationship(1, -3);
                }
                else if (5 == y1Result)
                {
                    relationship = FindRelationship(1, -4);
                }
                else if (6 == y1Result)
                {
                    relationship = FindRelationship(1, -5);
                }
                else if (7 == y1Result)
                {
                    relationship = FindRelationship(1, -6);
                }
                else if (8 == y1Result)
                {
                    relationship = FindRelationship(1, -7);
                }
                else if (9 == y1Result)
                {
                    relationship = FindRelationship(1, -8);
                }
            }
            else if (2 == y0Result)
            {
                if (0 == y1Result)
                {
                    relationship = FindRelationship(0, 2);
                }
                else if (1 == y1Result)
                {
                    relationship = FindRelationship(2, 1);
                }
                else if (2 == y1Result)
                {
                    relationship = FindRelationship(2, 0);
                }
                else if (3 == y1Result)
                {
                    relationship = FindRelationship(2, -1);
                }
                else if (4 == y1Result)
                {
                    relationship = FindRelationship(2, -2);
                }
                else if (5 == y1Result)
                {
                    relationship = FindRelationship(2, -3);
                }
                else if (6 == y1Result)
                {
                    relationship = FindRelationship(2, -4);
                }
                else if (7 == y1Result)
                {
                    relationship = FindRelationship(2, -5);
                }
                else if (8 == y1Result)
                {
                    relationship = FindRelationship(2, -6);
                }
                else if (9 == y1Result)
                {
                    relationship = FindRelationship(2, -7);
                }
            }
            else if (3 == y0Result)
            {
                if (0 == y1Result)
                {
                    relationship = FindRelationship(0, 3);
                }
                else if (1 == y1Result)
                {
                    relationship = FindRelationship(3, 2);
                }
                else if (2 == y1Result)
                {
                    relationship = FindRelationship(3, 1);
                }
                else if (3 == y1Result)
                {
                    relationship = FindRelationship(3, 0);
                }
                else if (4 == y1Result)
                {
                    relationship = FindRelationship(3, -1);
                }
                else if (5 == y1Result)
                {
                    relationship = FindRelationship(3, -2);
                }
                else if (6 == y1Result)
                {
                    relationship = FindRelationship(3, -3);
                }
                else if (7 == y1Result)
                {
                    relationship = FindRelationship(3, -4);
                }
                else if (8 == y1Result)
                {
                    relationship = FindRelationship(3, -5);
                }
                else if (9 == y1Result)
                {
                    relationship = FindRelationship(3, -6);
                }
            }
            else if (4 == y0Result)
            {
                if (0 == y1Result)
                {
                    relationship = FindRelationship(0, 4);
                }
                else if (1 == y1Result)
                {
                    relationship = FindRelationship(4, 3);
                }
                else if (2 == y1Result)
                {
                    relationship = FindRelationship(4, 2);
                }
                else if (3 == y1Result)
                {
                    relationship = FindRelationship(4, 1);
                }
                else if (4 == y1Result)
                {
                    relationship = FindRelationship(4, 0);
                }
                else if (5 == y1Result)
                {
                    relationship = FindRelationship(4, -1);
                }
                else if (6 == y1Result)
                {
                    relationship = FindRelationship(4, -2);
                }
                else if (7 == y1Result)
                {
                    relationship = FindRelationship(4, -3);
                }
                else if (8 == y1Result)
                {
                    relationship = FindRelationship(4, -4);
                }
                else if (9 == y1Result)
                {
                    relationship = FindRelationship(4, -5);
                }
            }
            else if (5 == y0Result)
            {
                if (0 == y1Result)
                {
                    relationship = FindRelationship(0, 5);
                }
                else if (1 == y1Result)
                {
                    relationship = FindRelationship(5, 4);
                }
                else if (2 == y1Result)
                {
                    relationship = FindRelationship(5, 3);
                }
                else if (3 == y1Result)
                {
                    relationship = FindRelationship(5, 2);
                }
                else if (4 == y1Result)
                {
                    relationship = FindRelationship(5, 1);
                }
                else if (5 == y1Result)
                {
                    relationship = FindRelationship(5, 0);
                }
                else if (6 == y1Result)
                {
                    relationship = FindRelationship(5, -1);
                }
                else if (7 == y1Result)
                {
                    relationship = FindRelationship(5, -2);
                }
                else if (8 == y1Result)
                {
                    relationship = FindRelationship(5, -3);
                }
                else if (9 == y1Result)
                {
                    relationship = FindRelationship(5, -4);
                }
            }
            else if (6 == y0Result)
            {
                if (0 == y1Result)
                {
                    relationship = FindRelationship(0, 6);
                }
                else if (1 == y1Result)
                {
                    relationship = FindRelationship(6, 5);
                }
                else if (2 == y1Result)
                {
                    relationship = FindRelationship(6, 4);
                }
                else if (3 == y1Result)
                {
                    relationship = FindRelationship(6, 3);
                }
                else if (4 == y1Result)
                {
                    relationship = FindRelationship(6, 2);
                }
                else if (5 == y1Result)
                {
                    relationship = FindRelationship(6, 1);
                }
                else if (6 == y1Result)
                {
                    relationship = FindRelationship(6, 0);
                }
                else if (7 == y1Result)
                {
                    relationship = FindRelationship(6, -1);
                }
                else if (8 == y1Result)
                {
                    relationship = FindRelationship(6, -2);
                }
                else if (9 == y1Result)
                {
                    relationship = FindRelationship(6, -3);
                }
            }
            else if (7 == y0Result)
            {
                if (0 == y1Result)
                {
                    relationship = FindRelationship(0, 7);
                }
                else if (1 == y1Result)
                {
                    relationship = FindRelationship(7, 6);
                }
                else if (2 == y1Result)
                {
                    relationship = FindRelationship(7, 5);
                }
                else if (3 == y1Result)
                {
                    relationship = FindRelationship(7, 4);
                }
                else if (4 == y1Result)
                {
                    relationship = FindRelationship(7, 3);
                }
                else if (5 == y1Result)
                {
                    relationship = FindRelationship(7, 2);
                }
                else if (6 == y1Result)
                {
                    relationship = FindRelationship(7, 1);
                }
                else if (7 == y1Result)
                {
                    relationship = FindRelationship(7, 0);
                }
                else if (8 == y1Result)
                {
                    relationship = FindRelationship(7, -1);
                }
                else if (9 == y1Result)
                {
                    relationship = FindRelationship(7, -2);
                }
            }
            else if (8 == y0Result)
            {
                if (0 == y1Result)
                {
                    relationship = FindRelationship(0, 8);
                }
                else if (1 == y1Result)
                {
                    relationship = FindRelationship(8, 7);
                }
                else if (2 == y1Result)
                {
                    relationship = FindRelationship(8, 6);
                }
                else if (3 == y1Result)
                {
                    relationship = FindRelationship(8, 5);
                }
                else if (4 == y1Result)
                {
                    relationship = FindRelationship(8, 4);
                }
                else if (5 == y1Result)
                {
                    relationship = FindRelationship(8, 3);
                }
                else if (6 == y1Result)
                {
                    relationship = FindRelationship(8, 2);
                }
                else if (7 == y1Result)
                {
                    relationship = FindRelationship(8, 1);
                }
                else if (8 == y1Result)
                {
                    relationship = FindRelationship(8, 0);
                }
                else if (9 == y1Result)
                {
                    relationship = FindRelationship(8, -1);
                }
            }
            else if (9 == y0Result)
            {
                if (0 == y1Result)
                {
                    relationship = FindRelationship(0, 9);
                }
                else if (1 == y1Result)
                {
                    relationship = FindRelationship(9, 8);
                }
                else if (2 == y1Result)
                {
                    relationship = FindRelationship(9, 7);
                }
                else if (3 == y1Result)
                {
                    relationship = FindRelationship(9, 6);
                }
                else if (4 == y1Result)
                {
                    relationship = FindRelationship(9, 5);
                }
                else if (5 == y1Result)
                {
                    relationship = FindRelationship(9, 4);
                }
                else if (6 == y1Result)
                {
                    relationship = FindRelationship(9, 3);
                }
                else if (7 == y1Result)
                {
                    relationship = FindRelationship(9, 2);
                }
                else if (8 == y1Result)
                {
                    relationship = FindRelationship(9, 1);
                }
                else if (9 == y1Result)
                {
                    relationship = FindRelationship(9, 0);
                }
            }

            return relationship;
        }
    }
}