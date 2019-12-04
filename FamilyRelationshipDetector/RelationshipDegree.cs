using System.Collections.Generic;

namespace FamilyRelationshipDetector
{
    public class RelationshipDegree
    {
        public int RelationshipDegreeNumber { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string RelationName { get; set; }
        public int ClusterNumber { get; set; }
        public float CommonCm { get; set; }
        public bool IsAncestorOfProband { get; set; }
        public bool IsDescendantOfProband { get; set; }
        public int RelationshipMaxCount { get; set; }
        public Dictionary<int, List<string>> PossibleRelationships { get; set; }

        public RelationshipDegree(int relationshipDegreeNumber, int x, int y, string relationName, int clusterNumber,
            float commonCm, bool isAncestorOfProband, bool isDescendantOfProband, int relationshipMaxCount, Dictionary<int, List<string>> possibleRelationships)
        {
            RelationshipDegreeNumber = relationshipDegreeNumber;
            X = x;
            Y = y;
            RelationName = relationName;
            ClusterNumber = clusterNumber;
            CommonCm = commonCm;
            IsAncestorOfProband = isAncestorOfProband;
            IsDescendantOfProband = isDescendantOfProband;
            RelationshipMaxCount = relationshipMaxCount;
            PossibleRelationships = possibleRelationships;
        }
    }

}
