using System.Windows.Forms;

namespace FamilyRelationshipDetector
{
    class Relative : Button
    {
        public int RelationNumber { get; set; }
        public int Vertical { get; set; }
        public int Horizontal { get; set; }
        public string RelationName { get; set; }
        public int WidthMultiplier { get; set; }
        public string ClusterColor { get; set; }

        public Relative(int RelationNumber, int Vertical, int Horizontal, string RelationName, int WidthMultiplier, string ClusterColor, int maxHorizontal)
        {
            this.RelationNumber = RelationNumber;
            this.Vertical = Vertical;
            this.Horizontal = Horizontal;
            this.RelationName = RelationName;
            this.WidthMultiplier = WidthMultiplier;
            this.ClusterColor = ClusterColor;
            Left = 100 * Vertical;
            Top = (50 * maxHorizontal) + (50 * -Horizontal);
            Width = 100 * WidthMultiplier;
            Height = 50;
            Text = RelationNumber + ". [" + Vertical + ";" + Horizontal + "] " + RelationName;
        }
    }
}
