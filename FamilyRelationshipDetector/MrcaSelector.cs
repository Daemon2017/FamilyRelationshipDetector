using System.Windows.Forms;

namespace FamilyRelationshipDetector
{
    public partial class Form1 : Form
    {
        private int MrcaSelector(int x0, int x1, int y0, int y1)
        {
            int yMRCA = 0; 

            if (x0 > x1)
            {
                if (0 == x1)
                {
                    if ((0 < x0) && (x0 < y1))
                    {
                        yMRCA = y1;
                    }
                    else
                    {
                        yMRCA = x0;
                    }
                }
                else
                {
                    yMRCA = x0;
                }
            }
            else if (x0 == x1)
            {
                if (y0 >= y1)
                {
                    yMRCA = y0;
                }
                else
                {
                    yMRCA = y1;
                }
            }
            else if (x0 < x1)
            {
                if (0 == x0)
                {
                    if ((0 < x1) && (x1 < y0))
                    {
                        yMRCA = y0;
                    }
                    else
                    {
                        yMRCA = x1;
                    }
                }
                else
                {
                    yMRCA = x1;
                }
            }

            return yMRCA;
        }
    }
}
