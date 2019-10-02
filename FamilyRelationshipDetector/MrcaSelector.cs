namespace FamilyRelationshipDetector
{
    public class MrcaSelector
    {
        public int SelectMrca(Relative start, Relative end)
        {
            int numberOfGenerationOfMrca = 0;

            /*
             * Определение количества поколений до БОП.
             */
            if (start.X > end.X)
            {
                if (0 == end.X)
                {
                    if (0 < start.X && start.X < end.Y)
                    {
                        numberOfGenerationOfMrca = end.Y;
                    }
                    else
                    {
                        numberOfGenerationOfMrca = start.X;
                    }
                }
                else
                {
                    numberOfGenerationOfMrca = start.X;
                }
            }
            else if (start.X == end.X)
            {
                numberOfGenerationOfMrca = start.Y >= end.Y ? start.Y : end.Y;
            }
            else if (start.X < end.X)
            {
                if (0 == start.X)
                {
                    if (0 < end.X && end.X < start.Y)
                    {
                        numberOfGenerationOfMrca = start.Y;
                    }
                    else
                    {
                        numberOfGenerationOfMrca = end.X;
                    }
                }
                else
                {
                    numberOfGenerationOfMrca = end.X;
                }
            }

            return numberOfGenerationOfMrca;
        }
    }
}