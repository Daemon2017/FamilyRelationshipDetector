using System.Collections.Generic;

namespace FamilyRelationshipDetector
{
    public class Relative
    {
        public int RelationNumber { get; set; }
        public int Y { get; set; }
        public string RelationName { get; set; }
        public double CommonCm { get; set; }
        public bool Ancestor { get; set; }
        public bool Siblindant { get; set; }
        public int MaxCount { get; set; }
        public Dictionary<int, List<string>> PossibleRelationships { get; set; }

        public Relative(int relationNumber, int y, string relationName, double commonCm, 
            bool ancestor, bool siblidant, int maxCount, Dictionary<int, List<string>> possibleRelationships)
        {
            RelationNumber = relationNumber;
            Y = y;
            RelationName = relationName;
            CommonCm = commonCm;
            Ancestor = ancestor;
            Siblindant = siblidant;
            MaxCount = maxCount;
            PossibleRelationships = possibleRelationships;
        }
    }
}
