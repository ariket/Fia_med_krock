using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;


namespace Fia_med_krock.GameLogic
{
    public class Cars
    {
        //color används för att veta vilken spelare pjäsen tillhör.
        //steps har samma funktion som gamla positionRedCarX variablerna.
        public string color;
        public int steps;
        public Windows.UI.Xaml.Shapes.Rectangle CarUI { get; set; }
        public Grid PlayBoard { get; set; }
        public int CarNumber { get; set; }

        //Konstruktor för objekten
        public Cars(string car_color, int total_steps, Grid playBoard, int carNumber)
        {
            color = car_color;
            steps = total_steps;
            CarUI = null;
            PlayBoard = playBoard;
            CarNumber = carNumber;
        }

        public void StepCar()
        {
            steps++;
        }

        public void StepCarBack()
        {
            steps--;
        }

        public void CarCrasced()
        {
            steps = -1;

            Rectangle homeGridRectangle = GetHomeGridRectangle(color, CarNumber);

            if (homeGridRectangle != null)
            {
                // Set the visibility of the home grid rectangle
               // homeGridRectangle.Visibility = Visibility.Visible;
                
                int targetColumn = Grid.GetColumn(homeGridRectangle);
                int targetRow = Grid.GetRow(homeGridRectangle);
                Debug.WriteLine(homeGridRectangle.Name);   
                Debug.WriteLine(homeGridRectangle.Visibility);   
                MoveHelper.MoveCar(CarUI, PlayBoard, targetColumn, targetRow);
            }

            //CarUI.Visibility = Visibility.Collapsed;
            //CarUI.Opacity = 0.3;
           /// CarUI.IsTapEnabled = false;
            

        }

        private Rectangle GetHomeGridRectangle(string color, int carNumber)
        {
            string rectangleName = $"{color}{carNumber}";

            return PlayBoard.FindName(rectangleName) as Rectangle;

        }


    }
}
