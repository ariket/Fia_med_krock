using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Capture;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using static Fia_med_krock.MainPage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409




namespace Fia_med_krock
{

    /// <summary>
    /// GamePage.
    /// </summary>
    public partial class MainPage : Page

    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        //RedCarsRoad är en array som redovisar vilken väg dom röda bilarna ska köra.
        //Sen har man en int (Cars.steps) för varje bil som anger bilen position mha RedCarsRoad[] 
        //Road för red cars, {column,row}
        public static string[] RedCarsRoad = { "0003", "0103", "0203", "0303", "0302", "0301", "0300", "0400", "0500", "0501", "0502", "0503", "0603", "0703", "0803", "0804", "0805", "0705", "0605", "0505", "0506", "0507", "0508", "0408", "0308", "0307", "0306", "0305", "0205", "0105", "0005", "0004", "0104", "0204", "0304", "0404" };
        
        public bool goForward = true;

        public class Cars
        {
            //color används för att veta vilken spelare pjäsen tillhör.
            //Just nu används inte col eller row pos, kan nog ta bort de?
            //steps har samma funktion som gamla positionRedCarX variablerna.
            public string color;
            public int col_pos;
            public int row_pos;
            public int steps;

            //Konstruktor för objekten
            public Cars(string car_color, int car_col_pos, int car_row_pos, int total_steps)
            {
                color = car_color;
                col_pos = car_col_pos;
                row_pos = car_row_pos;
                steps = total_steps;
            }

            public void StepCar()
            {
                steps++;
            }

            public void StepCarBack()
            {
                steps--;
            }


        }

        //Skapar pjäserna som behövs
        Cars redCar1 = new Cars("Red", 0, 0, -1);
        Cars redCar2 = new Cars("Red", 0, 0, -1);
        Cars redCar3 = new Cars("Red", 0, 0, -1);
        Cars redCar4 = new Cars("Red", 0, 0, -1);

        Cars blueCar1 = new Cars("Blue", 0, 0, 0);
        Cars blueCar2 = new Cars("Blue", 0, 0, 0);
        Cars blueCar3 = new Cars("Blue", 0, 0, 0);
        Cars blueCar4 = new Cars("Blue", 0, 0, 0);

        Cars greenCar1 = new Cars("Green", 0, 0, 0);
        Cars greenCar2 = new Cars("Green", 0, 0, 0);
        Cars greenCar3 = new Cars("Green", 0, 0, 0);
        Cars greenCar4 = new Cars("Green", 0, 0, 0);

        Cars yellowCar1 = new Cars("Yellow", 0, 0, 0);
        Cars yellowCar2 = new Cars("Yellow", 0, 0, 0);
        Cars yellowCar3 = new Cars("Yellow", 0, 0, 0);
        Cars yellowCar4 = new Cars("Yellow", 0, 0, 0);
        //Egen klass för spelplanen?

      


        private void MoveCar(Windows.UI.Xaml.Shapes.Rectangle carToMove, int columnNum, int rowNum)
        {
            PlayBoard.Children.Remove(carToMove);
            //await System.Threading.Tasks.Task.Delay(10);
            PlayBoard.Children.Add(carToMove);
            Grid.SetRow(carToMove, rowNum);
            Grid.SetColumn(carToMove, columnNum);
        }

        //Finns inte globala variabler i c#, så gjorde en ful lösning från https://stackoverflow.com/questions/14368129/how-to-use-global-variables-in-c
        public static class Globals
        {
            public static int dice_result = 0;
            //RedCarsRoad är en array som redovisar vilken väg dom röda bilarna ska köra, bara dom första 7 positionerna finns än så länge.
            //Road för red cars, {column,row}
            public static string[] RedCarsRoad = { "0003", "0103", "0203", "0303", "0302", "0301", "0300", "0400", "0500", "0501", "0502", "0503", "0603", "0703", "0803", "0804", "0805", "0705", "0605", "0505", "0506", "0507", "0508", "0408", "0308", "0307", "0306", "0305", "0205", "0105", "0005", "0004", "0104", "0204", "0304", "0404" };

        }

        //Lagt till så att det slumpas ett värde.
        private int roll_dice()
        {
            Random dice_roll = new Random();
            //Slumpar ett värde mellan 1 och 6. Maxvärdet 7 kan inte slumpas.
            int roll_result = Convert.ToInt32(dice_roll.Next(1, 7));
            Globals.dice_result = roll_result;
            return roll_result;
        }

        private void RollDice_Click(object sender, RoutedEventArgs e)
        {

            int dice = roll_dice();
            RollDice.Content = dice;

            if(dice == 1 || dice == 6)
            {
                Red1.IsTapEnabled = true;
                Red2.IsTapEnabled = true;
                Red3.IsTapEnabled = true;
                Red4.IsTapEnabled = true;
                Red1.Opacity = 1;
                Red2.Opacity = 1;
                Red3.Opacity = 1;
                Red4.Opacity = 1;
            }
        }


        bool checkCarPosition(int movingCarPosition)
        {
            bool check = true;
            if (movingCarPosition > 0)
            {
                movingCarPosition++;
                if (movingCarPosition == redCar1.steps || movingCarPosition == redCar2.steps || movingCarPosition == redCar3.steps || movingCarPosition == redCar4.steps) check = false;
            }
            return check;
        }


        async void tappedCar(Windows.UI.Xaml.Shapes.Rectangle carToMove, string[] CarsRoad, Cars car)
        {
            
            int dice = Globals.dice_result;
            int movNum = 0;
            goForward = true;

            while (movNum < dice)
            {
                if (!checkCarPosition(car.steps)) break;
          
                if (car.steps == 35)
                {
                    goForward = false;
                }

                if (goForward == true)
                {
                    car.StepCar();
                }
                else
                {
                    car.StepCarBack();
                }
                int columnNum = Convert.ToInt32(CarsRoad[car.steps].Substring(0, 2));
                int rowNum = Convert.ToInt32(CarsRoad[car.steps].Substring(2, 2));
                MoveCar(carToMove, columnNum, rowNum);
                movNum++;
                await System.Threading.Tasks.Task.Delay(200);

                if (movNum == dice && car.steps == 35)
                {
                    car.StepCar();
                    car.StepCar();
                    carToMove.Visibility = Visibility.Collapsed;
                    break;
                }
            }

            

            Red1.IsTapEnabled = false;
            Red2.IsTapEnabled = false;
            Red3.IsTapEnabled = false;
            Red4.IsTapEnabled = false;
            Red1.Opacity = 0.3;
            Red2.Opacity = 0.3;
            Red3.Opacity = 0.3;
            Red4.Opacity = 0.3;
        }


        private void RedCar1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            tappedCar(RedCar1, RedCarsRoad, redCar1);
            
        }

        private void RedCar2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            tappedCar(RedCar2, RedCarsRoad, redCar2);
        }

        private void RedCar3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            tappedCar(RedCar3, RedCarsRoad, redCar3);

      
        }

        private void RedCar4_Tapped(object sender, TappedRoutedEventArgs e)
        {
            tappedCar(RedCar4, RedCarsRoad, redCar4);
        }



        private void Red1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Red1.Visibility = Visibility.Collapsed;        
            RedCar1.Visibility = Visibility.Visible;

            tappedCar(RedCar1, RedCarsRoad, redCar1);         
        }


        private void Red2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Red2.Visibility = Visibility.Collapsed;       
            RedCar2.Visibility = Visibility.Visible;

            tappedCar(RedCar2, RedCarsRoad, redCar2);
        }

        private void Red3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Red3.Visibility = Visibility.Collapsed;
            RedCar3.Visibility = Visibility.Visible;

            tappedCar(RedCar3, RedCarsRoad, redCar3);
        }


        private void Red4_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Red4.Visibility = Visibility.Collapsed;
            RedCar4.Visibility = Visibility.Visible;

            tappedCar(RedCar4, RedCarsRoad, redCar4);         
        }
    }
}