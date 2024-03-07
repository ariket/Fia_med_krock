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
    /// <summary>
    /// Cars class
    /// color is used to separate the 4 players
    /// steps holds the positions of the game piece
    /// </summary>
    public class Cars
    {
        public string color;
        public int steps;
        public Windows.UI.Xaml.Shapes.Rectangle CarUI { get; set; }
        public Grid PlayBoard { get; set; }
        public int CarNumber { get; set; }

        //Constructor
        public Cars(string car_color, int total_steps, Grid playBoard, int carNumber)
        {
            color = car_color;
            steps = total_steps;
            CarUI = null;
            PlayBoard = playBoard;
            CarNumber = carNumber;
        }

        //Move the car forward
        public void StepCar()
        {
            steps++;
        }

        //Move the car back
        public void StepCarBack()
        {
            steps--;
        }

        //Sets the steps to 37 when the game peice reaches the goal
        public void StepCarToGoal()
        {
            steps = 37;
        }

        //Sets the steps to -1 when a pame piece is crached out
        public void CarCrasced()
        {
            steps = -1;
            int targetColumn = 0;
            int targetRow = 0;

            switch (CarNumber)
            {
                case 1:
                    break; 
                case 2:
                    targetColumn = 1;
                    break;
                case 3:
                    targetRow = 1;
                    break; 
                case 4:
                    targetColumn = 1;
                    targetRow = 1;
                    break; 
                default:
                    Debug.WriteLine($"Unsupported car number: {CarNumber}");
                    return;
            }

            Grid colorGrid = FindColorGrid(color);
            if (colorGrid != null)
            {
                MoveHelper.MoveCar(CarUI, colorGrid, targetColumn, targetRow);
                CarUI.Opacity = 0.3;
            }
        }
        private Grid FindColorGrid(string color)
        {
            string gridName = $"{color}";
            Debug.WriteLine(gridName);
            Debug.WriteLine("FindcolorGrid: ");
            Debug.WriteLine(PlayBoard.FindName(gridName));
            return PlayBoard.FindName(gridName) as Grid;
        }
    }
}
