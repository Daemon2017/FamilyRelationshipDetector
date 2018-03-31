using System.Windows.Forms;

namespace FamilyRelationshipDetector
{
    public partial class Form1 : Form
    {
        private class Relative : Button
        {
            public int RelationNumber { get; set; }
            public int Vertical { get; set; }
            public int Horizontal { get; set; }
            public string RelationName { get; set; }
            public int WidthMultiplier { get; set; }
            public string ClusterColor { get; set; }
        }
    }
}