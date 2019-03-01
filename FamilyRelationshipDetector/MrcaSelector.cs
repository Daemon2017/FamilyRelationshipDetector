﻿namespace FamilyRelationshipDetector
{
    public partial class Form1
    {
        private int MrcaSelector(int startX, int startY, int endX, int endY)
        {
            int numberOfGenerationOfMrca = 0;

            /*
             * Определение количества поколений до БОП.
             */
            if (startX > endX)
            {
                if (0 == endX)
                {
                    if (0 < startX && startX < endY)
                    {
                        numberOfGenerationOfMrca = endY;
                    }
                    else
                    {
                        numberOfGenerationOfMrca = startX;
                    }
                }
                else
                {
                    numberOfGenerationOfMrca = startX;
                }
            }
            else if (startX == endX)
            {
                numberOfGenerationOfMrca = startY >= endY ? startY : endY;
            }
            else if (startX < endX)
            {
                if (0 == startX)
                {
                    if (0 < endX && endX < startY)
                    {
                        numberOfGenerationOfMrca = startY;
                    }
                    else
                    {
                        numberOfGenerationOfMrca = endX;
                    }
                }
                else
                {
                    numberOfGenerationOfMrca = endX;
                }
            }

            return numberOfGenerationOfMrca;
        }
    }
}