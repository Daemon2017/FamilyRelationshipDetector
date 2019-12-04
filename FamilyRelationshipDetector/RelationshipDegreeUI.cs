using System.Drawing;
using System.Windows.Forms;

namespace FamilyRelationshipDetector
{
    public class RelationshipDegreeUI : Button
    {
        public int RelationshipDegreeNumber { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string RelationName { get; set; }
        public int ClusterNumber { get; set; }
        public float CommonCm { get; set; }
        public int WidthMultiplier { get; set; }

        public RelationshipDegreeUI(int relationshipDegreeNumber, int x, int y, string relationName, int widthMultiplier, int clusterNumber,
            float commonCm, int maxHorizontal)
        {
            RelationshipDegreeNumber = relationshipDegreeNumber;
            X = x;
            Y = y;
            RelationName = relationName;
            WidthMultiplier = widthMultiplier;
            ClusterNumber = clusterNumber;
            CommonCm = commonCm;

            Left = 100 * x;
            Top = 50 * maxHorizontal + 50 * -y;
            Width = 100 * widthMultiplier;
            Height = 50;
            Text = relationshipDegreeNumber + ". [" + x + ";" + y + "] " + relationName;

            switch (clusterNumber)
            {
                case 0:
                    BackColor = Color.LightBlue;
                    break;
                case 1:
                    BackColor = Color.LightCoral;
                    break;
                case 2:
                    BackColor = Color.LightCyan;
                    break;
                case 3:
                    BackColor = Color.LightGoldenrodYellow;
                    break;
                case 4:
                    BackColor = Color.LightGray;
                    break;
                case 5:
                    BackColor = Color.LightGreen;
                    break;
                case 6:
                    BackColor = Color.LightPink;
                    break;
                case 7:
                    BackColor = Color.LightSalmon;
                    break;
                case 8:
                    BackColor = Color.LightSeaGreen;
                    break;
                case 9:
                    BackColor = Color.LightSkyBlue;
                    break;
                case 10:
                    BackColor = Color.LightBlue;
                    break;
                case 11:
                    BackColor = Color.LightCoral;
                    break;
                case 12:
                    BackColor = Color.LightCyan;
                    break;
                case 13:
                    BackColor = Color.LightGoldenrodYellow;
                    break;
                case 14:
                    BackColor = Color.LightGray;
                    break;
                case 15:
                    BackColor = Color.LightGreen;
                    break;
                case 16:
                    BackColor = Color.LightPink;
                    break;
                case 17:
                    BackColor = Color.LightSalmon;
                    break;
                case 18:
                    BackColor = Color.LightSeaGreen;
                    break;
                case 19:
                    BackColor = Color.LightSkyBlue;
                    break;
                case 20:
                    BackColor = Color.LightSlateGray;
                    break;
                case 21:
                    BackColor = Color.LightSteelBlue;
                    break;
                case 22:
                    BackColor = Color.LightYellow;
                    break;
                case 23:
                    BackColor = Color.Lime;
                    break;
                case 24:
                    BackColor = Color.Fuchsia;
                    break;
            }
        }
    }
}