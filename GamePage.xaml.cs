using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public enum GameState
        {
            PlayerRed,
            PlayerBlue,
            PlayerGreen,
            PlayerYellow
        }

        private GameState currentPlayer;

        public MainPage()
        {
            this.InitializeComponent();
            currentPlayer = GameState.PlayerRed;
        }

        //RedCarsRoad är en array som redovisar vilken väg dom röda bilarna ska köra.
        //Sen har man en int (Cars.steps) för varje bil som anger bilen position mha RedCarsRoad[] 
        //Road för red cars, {column,row}
        public static string[] RedCarsRoad = { "0003", "0103", "0203", "0303", "0302", "0301", "0300", "0400", "0500", "0501", "0502", "0503", "0603", "0703", "0803", "0804", "0805", "0705", "0605", "0505", "0506", "0507", "0508", "0408", "0308", "0307", "0306", "0305", "0205", "0105", "0005", "0004", "0104", "0204", "0304", "0404" };
        public static string[] BlueCarsRoad = { "0500", "0501", "0502", "0503", "0603", "0703", "0803", "0804", "0805", "0705", "0605", "0505", "0506", "0507", "0508", "0408", "0308", "0307", "0306", "0305", "0205", "0105", "0005", "0004", "0003", "0103", "0203", "0303", "0302", "0301", "0300", "0400", "0401", "0402", "0403", "0404" };
        public static string[] GreenCarsRoad = { "0805", "0705", "0605", "0505", "0506", "0507", "0508", "0408", "0308", "0307", "0306", "0305", "0205", "0105", "0005", "0004", "0003", "0103", "0203", "0303", "0302", "0301", "0300", "0400", "0500", "0501", "0502", "0503", "0603", "0703", "0803", "0804", "0704", "0604", "0504", "0404" };
        public static string[] YellowCarsRoad = { "0308", "0307", "0306", "0305", "0205", "0105", "0005", "0004", "0003", "0103", "0203", "0303", "0302", "0301", "0300", "0400", "0500", "0501", "0502", "0503", "0603", "0703", "0803", "0804", "0805", "0705", "0605", "0505", "0506", "0507", "0508", "0408", "0407", "0406", "0405", "0404" };
        
        public bool goForward = true;

        public class Cars
        {
            //color används för att veta vilken spelare pjäsen tillhör.
            //steps har samma funktion som gamla positionRedCarX variablerna.
            public string color;
            public int steps;

            //Konstruktor för objekten
            public Cars(string car_color, int total_steps)
            {
                color = car_color;
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
        Cars redCar1 = new Cars("Red", -1);
        Cars redCar2 = new Cars("Red", -1);
        Cars redCar3 = new Cars("Red", -1);
        Cars redCar4 = new Cars("Red", -1);

        Cars blueCar1 = new Cars("Blue", -1);
        Cars blueCar2 = new Cars("Blue", -1);
        Cars blueCar3 = new Cars("Blue", -1);
        Cars blueCar4 = new Cars("Blue", -1);

        Cars greenCar1 = new Cars("Green", -1);
        Cars greenCar2 = new Cars("Green", -1);
        Cars greenCar3 = new Cars("Green", -1);
        Cars greenCar4 = new Cars("Green", -1);

        Cars yellowCar1 = new Cars("Yellow", -1);
        Cars yellowCar2 = new Cars("Yellow", -1);
        Cars yellowCar3 = new Cars("Yellow", -1);
        Cars yellowCar4 = new Cars("Yellow", -1);
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
            // public static string[] RedCarsRoad = { "0003", "0103", "0203", "0303", "0302", "0301", "0300", "0400", "0500", "0501", "0502", "0503", "0603", "0703", "0803", "0804", "0805", "0705", "0605", "0505", "0506", "0507", "0508", "0408", "0308", "0307", "0306", "0305", "0205", "0105", "0005", "0004", "0104", "0204", "0304", "0404" };
           
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

            setCurrentPlayerCarsState(dice);

            //bool anyCarsEnabled = CheckAnyCarsEnabled();

            //if (!anyCarsEnabled)
            //{
            //    // Ingen bil kan röras och därav byts det tur
            //    SwitchToNextPlayer();  
            //}

            //if(dice == 1 || dice == 6)
            //{
            //    Red1.IsTapEnabled = true;
            //    Red2.IsTapEnabled = true;
            //    Red3.IsTapEnabled = true;
            //    Red4.IsTapEnabled = true;
            //    Red1.Opacity = 1;
            //    Red2.Opacity = 1;
            //    Red3.Opacity = 1;
            //    Red4.Opacity = 1;
            //}
        }


        //tanken att kolla så att om ingen pjös kan flyttas skiftas turen till nästa spelare
        private bool CheckAnyCarsEnabled()
        {
            switch (currentPlayer)
            {
                case GameState.PlayerRed:
                    return Red1.IsTapEnabled || Red2.IsTapEnabled || Red3.IsTapEnabled || Red4.IsTapEnabled;

                case GameState.PlayerBlue:
                    return Blue1.IsTapEnabled || Blue2.IsTapEnabled || Blue3.IsTapEnabled || Blue4.IsTapEnabled;

                case GameState.PlayerGreen:
                    return Green1.IsTapEnabled || Green2.IsTapEnabled || Green3.IsTapEnabled || Green4.IsTapEnabled;

                case GameState.PlayerYellow:
                    return Yellow1.IsTapEnabled || Yellow2.IsTapEnabled || Yellow3.IsTapEnabled || Yellow4.IsTapEnabled;

                default:
                    return false;
            }
        }

        private void SwitchToNextPlayer()
        {
            //DisableAllCarsForCurrentPlayer();
            switch (currentPlayer)
            {
                case GameState.PlayerRed:
                    currentPlayer = GameState.PlayerBlue;
                    RedCar1.IsTapEnabled = false;
                    RedCar2.IsTapEnabled = false;
                    RedCar3.IsTapEnabled = false;
                    RedCar4.IsTapEnabled = false;
                    Red1.IsTapEnabled = false;
                    Red2.IsTapEnabled = false;
                    Red3.IsTapEnabled = false;
                    Red4.IsTapEnabled = false;
                    break;
                case GameState.PlayerBlue:
                    currentPlayer = GameState.PlayerGreen;
                    BlueCar1.IsTapEnabled =false;
                    BlueCar2.IsTapEnabled =false;
                    BlueCar3.IsTapEnabled =false;
                    BlueCar4.IsTapEnabled =false;
                    Blue1.IsTapEnabled = false;
                    Blue2.IsTapEnabled = false;
                    Blue3.IsTapEnabled = false;
                    Blue4.IsTapEnabled = false;
                    break;
                case GameState.PlayerGreen:
                    currentPlayer = GameState.PlayerYellow;
                    GreenCar1.IsTapEnabled =false;
                    GreenCar2.IsTapEnabled =false;
                    GreenCar3.IsTapEnabled =false;
                    GreenCar4.IsTapEnabled =false;
                    Green1.IsTapEnabled = false;
                    Green2.IsTapEnabled = false;
                    Green3.IsTapEnabled = false;
                    Green4.IsTapEnabled = false;
                    break;
                case GameState.PlayerYellow:
                    currentPlayer = GameState.PlayerRed;
                    YellowCar1.IsTapEnabled=false;
                    YellowCar2.IsTapEnabled=false;
                    YellowCar3.IsTapEnabled=false;
                    YellowCar4.IsTapEnabled=false;
                    Yellow1.IsTapEnabled = false;
                    Yellow2.IsTapEnabled = false;
                    Yellow3.IsTapEnabled = false;
                    Yellow4.IsTapEnabled = false;
                    break;

            }
        }


        private void setCurrentPlayerCarsState(int dice)
        {
            switch (currentPlayer)
            {
                case GameState.PlayerRed:
                    SetTapEnabledForPlayer(redCar1, Red1, dice);
                    SetTapEnabledForPlayer(redCar2, Red2, dice);
                    SetTapEnabledForPlayer(redCar3, Red3, dice);
                    SetTapEnabledForPlayer(redCar4, Red4, dice);
                    break;

                case GameState.PlayerBlue:
                    SetTapEnabledForPlayer(blueCar1, Blue1, dice);
                    SetTapEnabledForPlayer(blueCar2, Blue2, dice);
                    SetTapEnabledForPlayer(blueCar3, Blue3, dice);
                    SetTapEnabledForPlayer(blueCar4, Blue4, dice);
                    break;
                case GameState.PlayerGreen:
                    SetTapEnabledForPlayer(greenCar1, Green1, dice);
                    SetTapEnabledForPlayer(greenCar2, Green2, dice);
                    SetTapEnabledForPlayer(greenCar3, Green3, dice);
                    SetTapEnabledForPlayer(greenCar4, Green4, dice);
                    break;
                case GameState.PlayerYellow:
                    SetTapEnabledForPlayer(yellowCar1, Yellow1, dice);
                    SetTapEnabledForPlayer(yellowCar2, Yellow2, dice);
                    SetTapEnabledForPlayer(yellowCar3, Yellow3, dice);
                    SetTapEnabledForPlayer(yellowCar4, Yellow4, dice);
                    break;
            }

        }
        private void SetTapEnabledForPlayer(Cars car, Windows.UI.Xaml.Shapes.Rectangle carToMove, int dice)
        {

            carToMove.IsTapEnabled = true; /*(dice == 1 || dice == 6 || car.steps != -1);*/
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
            Debug.WriteLine("tappedcar körs ");
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



            DisableAllCarsForCurrentPlayer();
            if (dice != 6)
            {
                SwitchToNextPlayer();
            }


        }
        private void DisableAllCarsForCurrentPlayer()
        {
            switch (currentPlayer)
            {
                case GameState.PlayerRed:
                    RedCar1.IsTapEnabled = false;
                    RedCar2.IsTapEnabled = false;
                    RedCar3.IsTapEnabled = false;
                    RedCar4.IsTapEnabled = false;
                    RedCar1.Opacity = 0.3;
                    RedCar2.Opacity = 0.3;
                    RedCar3.Opacity = 0.3;
                    RedCar4.Opacity = 0.3;
                    break;
                case GameState.PlayerBlue:
                    BlueCar1.IsTapEnabled = false;
                    BlueCar2.IsTapEnabled = false;
                    BlueCar3.IsTapEnabled = false;
                    BlueCar4.IsTapEnabled = false;
                    BlueCar1.Opacity = 0.3;
                    BlueCar2.Opacity = 0.3;
                    BlueCar3.Opacity = 0.3;
                    BlueCar4.Opacity = 0.3;
                    break;
                case GameState.PlayerGreen:
                    GreenCar1.IsTapEnabled = false;
                    GreenCar2.IsTapEnabled = false;
                    GreenCar3.IsTapEnabled = false;
                    GreenCar4.IsTapEnabled = false;
                    GreenCar1.Opacity = 0.3;
                    GreenCar2.Opacity = 0.3;
                    GreenCar3.Opacity = 0.3;
                    GreenCar4.Opacity = 0.3;
                    break;
                case GameState.PlayerYellow:
                    YellowCar1.IsTapEnabled = false;
                    YellowCar2.IsTapEnabled = false;
                    YellowCar3.IsTapEnabled = false;
                    YellowCar4.IsTapEnabled = false;
                    YellowCar1.Opacity = 0.3;
                    YellowCar2.Opacity = 0.3;
                    YellowCar3.Opacity = 0.3;
                    YellowCar4.Opacity = 0.3;
                    break;

                default:
                    break;
            }
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

        private void BlueCar1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            tappedCar(BlueCar1, BlueCarsRoad, blueCar1);
        }

        private void BlueCar2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            tappedCar(BlueCar2, BlueCarsRoad, blueCar2);
        }

        private void BlueCar3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            tappedCar(BlueCar3, BlueCarsRoad, blueCar3);
        }

        private void BlueCar4_Tapped(object sender, TappedRoutedEventArgs e)
        {
            tappedCar(BlueCar4, BlueCarsRoad, blueCar4);
        }
        private void Blue1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Blue1.Visibility = Visibility.Collapsed;
            BlueCar1.Visibility = Visibility.Visible;

            tappedCar(BlueCar1, BlueCarsRoad, blueCar1);
        }

        private void Blue2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Blue2.Visibility = Visibility.Collapsed;
            BlueCar2.Visibility = Visibility.Visible;

            tappedCar(BlueCar2, BlueCarsRoad, blueCar2);
        }

        private void Blue3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Blue3.Visibility = Visibility.Collapsed;
            BlueCar3.Visibility = Visibility.Visible;

            tappedCar(BlueCar3, BlueCarsRoad, blueCar3);
        }

        private void Blue4_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Blue4.Visibility = Visibility.Collapsed;
            BlueCar4.Visibility = Visibility.Visible;

            tappedCar(BlueCar4, BlueCarsRoad, blueCar4);
        }



        private void GreenCar1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            tappedCar(GreenCar1, GreenCarsRoad, greenCar1);
        }

        private void GreenCar2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            tappedCar(GreenCar2, GreenCarsRoad, greenCar2);
        }

        private void GreenCar3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            tappedCar(GreenCar3, GreenCarsRoad, greenCar3);
        }

        private void GreenCar4_Tapped(object sender, TappedRoutedEventArgs e)
        {
            tappedCar(GreenCar4, GreenCarsRoad, greenCar4);
        }


        private void Green1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Green1.Visibility = Visibility.Collapsed;
            GreenCar1.Visibility = Visibility.Visible;

            tappedCar(GreenCar1, GreenCarsRoad, greenCar1);
        }

        private void Green2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Green2.Visibility = Visibility.Collapsed;
            GreenCar2.Visibility = Visibility.Visible;

            tappedCar(GreenCar2, GreenCarsRoad, greenCar2);
        }

        private void Green3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Green3.Visibility = Visibility.Collapsed;
            GreenCar3.Visibility = Visibility.Visible;

            tappedCar(GreenCar3, GreenCarsRoad, greenCar3);
        }

        private void Green4_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Green4.Visibility = Visibility.Collapsed;
            GreenCar4.Visibility = Visibility.Visible;

            tappedCar(GreenCar4, GreenCarsRoad, greenCar4);
        }


        private void YellowCar1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            tappedCar(YellowCar1, YellowCarsRoad, yellowCar1);
        }

        private void YellowCar2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            tappedCar(YellowCar2, YellowCarsRoad, yellowCar2);
        }

        private void YellowCar3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            tappedCar(YellowCar3, YellowCarsRoad, yellowCar3);
        }

        private void YellowCar4_Tapped(object sender, TappedRoutedEventArgs e)
        {
            tappedCar(YellowCar4, YellowCarsRoad, yellowCar4);
        }

        private void Yellow1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Yellow1.Visibility = Visibility.Collapsed;
            YellowCar1.Visibility = Visibility.Visible;

            tappedCar(YellowCar1, YellowCarsRoad, yellowCar1);
        }

        private void Yellow2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Yellow2.Visibility = Visibility.Collapsed;
            YellowCar2.Visibility = Visibility.Visible;

            tappedCar(YellowCar2, YellowCarsRoad, yellowCar2);
        }

        private void Yellow3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Yellow3.Visibility = Visibility.Collapsed;
            YellowCar3.Visibility = Visibility.Visible;

            tappedCar(YellowCar3, YellowCarsRoad, yellowCar3);
        }

        private void Yellow4_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Yellow4.Visibility = Visibility.Collapsed;
            YellowCar4.Visibility = Visibility.Visible;

            tappedCar(YellowCar4, YellowCarsRoad, yellowCar4);
        }



    
    }
}