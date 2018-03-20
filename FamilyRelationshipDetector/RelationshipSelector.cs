using System.Windows.Forms;

namespace FamilyRelationshipDetector
{
    public partial class Form1 : Form
    {
        private string RelationshipSelector(int y0Result, int y1Result)
        {
            string relationship = "?.";

            if (0 == y0Result)
            {
                if (0 == y1Result)
                {
                    relationship = null;
                }
                else if (1 == y1Result)
                {
                    relationship = button28.Text;
                }
                else if (2 == y1Result)
                {
                    relationship = button16.Text;
                }
                else if (3 == y1Result)
                {
                    relationship = button35.Text;
                }
                else if (4 == y1Result)
                {
                    relationship = button51.Text;
                }
                else if (5 == y1Result)
                {
                    relationship = button54.Text;
                }
                else if (6 == y1Result)
                {
                    relationship = button57.Text;
                }
                else if (7 == y1Result)
                {
                    relationship = button59.Text;
                }
                else if (8 == y1Result)
                {
                    relationship = button92.Text;
                }
                else if (9 == y1Result)
                {
                    relationship = button93.Text;
                }
            }
            else if (1 == y0Result)
            {
                if (0 == y1Result)
                {
                    relationship = button6.Text;
                }
                else if (1 == y1Result)
                {
                    relationship = button7.Text;
                }
                else if (2 == y1Result)
                {
                    relationship = button29.Text;
                }
                else if (3 == y1Result)
                {
                    relationship = button20.Text;
                }
                else if (4 == y1Result)
                {
                    relationship = button36.Text;
                }
                else if (5 == y1Result)
                {
                    relationship = button53.Text;
                }
                else if (6 == y1Result)
                {
                    relationship = button55.Text;
                }
                else if (7 == y1Result)
                {
                    relationship = button58.Text;
                }
                else if (8 == y1Result)
                {
                    relationship = button85.Text;
                }
                else if (9 == y1Result)
                {
                    relationship = button88.Text;
                }
            }
            else if (2 == y0Result)
            {
                if (0 == y1Result)
                {
                    relationship = button5.Text;
                }
                else if (1 == y1Result)
                {
                    relationship = button8.Text;
                }
                else if (2 == y1Result)
                {
                    relationship = button23.Text;
                }
                else if (3 == y1Result)
                {
                    relationship = button30.Text;
                }
                else if (4 == y1Result)
                {
                    relationship = button21.Text;
                }
                else if (5 == y1Result)
                {
                    relationship = button37.Text;
                }
                else if (6 == y1Result)
                {
                    relationship = button56.Text;
                }
                else if (7 == y1Result)
                {
                    relationship = button82.Text;
                }
                else if (8 == y1Result)
                {
                    relationship = button83.Text;
                }
                else if (9 == y1Result)
                {
                    relationship = button84.Text;
                }
            }
            else if (3 == y0Result)
            {
                if (0 == y1Result)
                {
                    relationship = button3.Text;
                }
                else if (1 == y1Result)
                {
                    relationship = button9.Text;
                }
                else if (2 == y1Result)
                {
                    relationship = button13.Text;
                }
                else if (3 == y1Result)
                {
                    relationship = button24.Text;
                }
                else if (4 == y1Result)
                {
                    relationship = button31.Text;
                }
                else if (5 == y1Result)
                {
                    relationship = button22.Text;
                }
                else if (6 == y1Result)
                {
                    relationship = button38.Text;
                }
                else if (7 == y1Result)
                {
                    relationship = button78.Text;
                }
                else if (8 == y1Result)
                {
                    relationship = button79.Text;
                }
                else if (9 == y1Result)
                {
                    relationship = button80.Text;
                }
            }
            else if (4 == y0Result)
            {
                if (0 == y1Result)
                {
                    relationship = button1.Text;
                }
                else if (1 == y1Result)
                {
                    relationship = button10.Text;
                }
                else if (2 == y1Result)
                {
                    relationship = button14.Text;
                }
                else if (3 == y1Result)
                {
                    relationship = button17.Text;
                }
                else if (4 == y1Result)
                {
                    relationship = button25.Text;
                }
                else if (5 == y1Result)
                {
                    relationship = button32.Text;
                }
                else if (6 == y1Result)
                {
                    relationship = button27.Text;
                }
                else if (7 == y1Result)
                {
                    relationship = button39.Text;
                }
                else if (8 == y1Result)
                {
                    relationship = button76.Text;
                }
                else if (9 == y1Result)
                {
                    relationship = button77.Text;
                }
            }
            else if (5 == y0Result)
            {
                if (0 == y1Result)
                {
                    relationship = button4.Text;
                }
                else if (1 == y1Result)
                {
                    relationship = button11.Text;
                }
                else if (2 == y1Result)
                {
                    relationship = button15.Text;
                }
                else if (3 == y1Result)
                {
                    relationship = button18.Text;
                }
                else if (4 == y1Result)
                {
                    relationship = button19.Text;
                }
                else if (5 == y1Result)
                {
                    relationship = button26.Text;
                }
                else if (6 == y1Result)
                {
                    relationship = button33.Text;
                }
                else if (7 == y1Result)
                {
                    relationship = button34.Text;
                }
                else if (8 == y1Result)
                {
                    relationship = button40.Text;
                }
                else if (9 == y1Result)
                {
                    relationship = "[5;-4] 5-ный 2*пра(внучатый племянник)";
                }
            }
            else if (6 == y0Result)
            {
                if (0 == y1Result)
                {
                    relationship = button41.Text;
                }
                else if (1 == y1Result)
                {
                    relationship = button42.Text;
                }
                else if (2 == y1Result)
                {
                    relationship = button43.Text;
                }
                else if (3 == y1Result)
                {
                    relationship = button44.Text;
                }
                else if (4 == y1Result)
                {
                    relationship = button45.Text;
                }
                else if (5 == y1Result)
                {
                    relationship = button46.Text;
                }
                else if (6 == y1Result)
                {
                    relationship = button47.Text;
                }
                else if (7 == y1Result)
                {
                    relationship = button48.Text;
                }
                else if (8 == y1Result)
                {
                    relationship = button49.Text;
                }
                else if (9 == y1Result)
                {
                    relationship = button50.Text;
                }
            }
            else if (7 == y0Result)
            {
                if (0 == y1Result)
                {
                    relationship = "[0;7] 5*пра(дед)";
                }
                else if (1 == y1Result)
                {
                    relationship = "[7;6] 2-ный 4*пра(дед)";
                }
                else if (2 == y1Result)
                {
                    relationship = "[7;5] 3-ный 3*пра(дед)";
                }
                else if (3 == y1Result)
                {
                    relationship = "[7;4] 4-ный 2*пра(дед)";
                }
                else if (4 == y1Result)
                {
                    relationship = "[7;3] 5-ный пра(дед)";
                }
                else if (5 == y1Result)
                {
                    relationship = "[7;2] 6-ный дед";
                }
                else if (6 == y1Result)
                {
                    relationship = "[7;1] 7-ный родитель";
                }
                else if (7 == y1Result)
                {
                    relationship = "[7;0] 7-ный брат";
                }
                else if (8 == y1Result)
                {
                    relationship = "[7;-1] 7-ный племянник";
                }
                else if (9 == y1Result)
                {
                    relationship = "[7;-2] 7-ный внучатый племянник";
                }
            }
            else if (8 == y0Result)
            {
                if (0 == y1Result)
                {
                    relationship = "[0;8] 6*пра(дед)";
                }
                else if (1 == y1Result)
                {
                    relationship = "[8;7] 2-ный 5*пра(дед)";
                }
                else if (2 == y1Result)
                {
                    relationship = "[8;6] 3-ный 4*пра(дед)";
                }
                else if (3 == y1Result)
                {
                    relationship = "[8;5] 4-ный 3*пра(дед)";
                }
                else if (4 == y1Result)
                {
                    relationship = "[8;4] 5-ный 2*пра(дед)";
                }
                else if (5 == y1Result)
                {
                    relationship = "[8;3] 6-ный прадед";
                }
                else if (6 == y1Result)
                {
                    relationship = "[8;2] 7-ный дед";
                }
                else if (7 == y1Result)
                {
                    relationship = "[8;1] 8-ный родитель";
                }
                else if (8 == y1Result)
                {
                    relationship = "[8;0] 8-ный брат";
                }
                else if (9 == y1Result)
                {
                    relationship = "[8;-1] 8-ный племянник";
                }
            }
            else if (9 == y0Result)
            {
                if (0 == y1Result)
                {
                    relationship = "[0;9] 7*пра(дед)";
                }
                else if (1 == y1Result)
                {
                    relationship = "[9;8] 2-ный 6*пра(дед)";
                }
                else if (2 == y1Result)
                {
                    relationship = "[9;7] 3-ный 5*пра(дед)";
                }
                else if (3 == y1Result)
                {
                    relationship = "[9;6] 4-ный 4*пра(дед)";
                }
                else if (4 == y1Result)
                {
                    relationship = "[9;5] 5-ный 3*пра(дед)";
                }
                else if (5 == y1Result)
                {
                    relationship = "[9;4] 6-ный 2*пра(дед)";
                }
                else if (6 == y1Result)
                {
                    relationship = "[9;3] 7-ный прадед";
                }
                else if (7 == y1Result)
                {
                    relationship = "[9;2] 8-ный дед";
                }
                else if (8 == y1Result)
                {
                    relationship = "[9;1] 9-ный родитель";
                }
                else if (9 == y1Result)
                {
                    relationship = "[9;0] 9-ный брат";
                }
            }

            return relationship;
        }
    }
}