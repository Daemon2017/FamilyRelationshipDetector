using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FamilyRelationshipDetector
{
    public partial class Tools
    {
        public void SaveMatricesToFiles(List<RelationshipDegreeUI> usefulRelationshipDegreesUIList, List<RelationshipDegreeUI> _relationshipDegreesUIList)
        {
            usefulRelationshipDegreesUIList.Remove(_relationshipDegreesUIList.Where(rel => rel.X == -1 && rel.Y == -1).Single());

            /*
             * Построение словаря "степень родства предка : максимальное количество представителей"
             */
            Dictionary<int, int> ancestorsMaxCountDictionary = new Dictionary<int, int>();
            foreach (var rel in _relationshipDegreesUIList)
            {
                if (rel.X.Equals(0) && rel.Y > 0)
                {
                    ancestorsMaxCountDictionary.Add(rel.RelationshipDegreeNumber, (int)Math.Pow(2, rel.Y));
                }
            }

            /*
             * Построение списка потомков пробанда
             */
            List<string> descendantsList = new List<string>();
            foreach (var rel in _relationshipDegreesUIList)
            {
                if (rel.X.Equals(0) && rel.Y < 0)
                {
                    descendantsList.Add(rel.RelationshipDegreeNumber.ToString());
                }
            }

            /*
             * Построение JSON'а с основными сведениями о каждой степени родства
             */
            List<RelationshipDegree> usefulRelationDegreesList = new List<RelationshipDegree>();

            usefulRelationDegreesList.Add(new RelationshipDegree(
                0,
                -1,
                -1,
                "Нет родства",
                0,
                0,
                false,
                false,
                int.MaxValue,
                new Dictionary<int, List<string>>()));

            foreach (RelationshipDegreeUI usefulRelationshipDegreesUI in usefulRelationshipDegreesUIList)
            {
                int relationshipMaxCount = 0;
                if (ancestorsMaxCountDictionary.ContainsKey(usefulRelationshipDegreesUI.RelationshipDegreeNumber))
                {
                    relationshipMaxCount = ancestorsMaxCountDictionary[usefulRelationshipDegreesUI.RelationshipDegreeNumber];
                }
                else
                {
                    relationshipMaxCount = int.MaxValue;
                }

                Dictionary<int, List<string>> possibleRelationships = new Dictionary<int, List<string>>();
                foreach (var usefulRelationshipDegree in usefulRelationshipDegreesUIList)
                {
                    int yOfMrca = GetYOfMRCA(usefulRelationshipDegreesUI.X, usefulRelationshipDegreesUI.Y, usefulRelationshipDegree.X, usefulRelationshipDegree.Y);

                    int numberOfGenerationsBetweenMrcaAndZeroRelative = yOfMrca - usefulRelationshipDegreesUI.Y,
                        numberOfGenerationsBetweenMrcaAndFirstRelative = yOfMrca - usefulRelationshipDegree.Y;

                    possibleRelationships.Add(usefulRelationshipDegree.RelationshipDegreeNumber,
                                              GetPossibleRelationshipsList(yOfMrca,
                                                                           numberOfGenerationsBetweenMrcaAndZeroRelative,
                                                                           numberOfGenerationsBetweenMrcaAndFirstRelative,
                                                                           usefulRelationshipDegreesUI,
                                                                           usefulRelationshipDegree,
                                                                           _relationshipDegreesUIList).Select(x => x.RelationshipDegreeNumber.ToString()).ToList());
                }

                RelationshipDegree relationshipDegree = new RelationshipDegree(
                    usefulRelationshipDegreesUI.RelationshipDegreeNumber,
                    usefulRelationshipDegreesUI.X,
                    usefulRelationshipDegreesUI.Y,
                    usefulRelationshipDegreesUI.RelationName,
                    usefulRelationshipDegreesUI.ClusterNumber,
                    usefulRelationshipDegreesUI.CommonCm,
                    ancestorsMaxCountDictionary.ContainsKey(usefulRelationshipDegreesUI.RelationshipDegreeNumber),
                    descendantsList.Contains(usefulRelationshipDegreesUI.RelationshipDegreeNumber.ToString()),
                    relationshipMaxCount,
                    possibleRelationships);
                usefulRelationDegreesList.Add(relationshipDegree);
            }

            File.WriteAllText("relatives.json", JsonConvert.SerializeObject(usefulRelationDegreesList));
        }
    }
}