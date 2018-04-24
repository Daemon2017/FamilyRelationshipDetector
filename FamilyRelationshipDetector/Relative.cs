using System.Drawing;
using System.Windows.Forms;

namespace FamilyRelationshipDetector
{
    class Relative : Button
    {
        public int RelationNumber { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string RelationName { get; set; }
        public int WidthMultiplier { get; set; }
        public int ClusterNumber { get; set; }

        public Relative(int RelationNumber, int X, int Y, string RelationName, int WidthMultiplier, int ClusterNumber, int maxHorizontal)
        {
            this.RelationNumber = RelationNumber;
            this.X = X;
            this.Y = Y;
            this.RelationName = RelationName;
            this.WidthMultiplier = WidthMultiplier;
            this.ClusterNumber = ClusterNumber;

            Left = 100 * X;
            Top = (50 * maxHorizontal) + (50 * -Y);
            Width = 100 * WidthMultiplier;
            Height = 50;
            Text = RelationNumber + ". [" + X + ";" + Y + "] " + RelationName;

            switch(ClusterNumber)
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
            }
        }
    }
}
