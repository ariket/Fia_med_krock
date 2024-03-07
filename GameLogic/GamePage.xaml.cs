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
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using static Fia_med_krock.MainPage;
using static Fia_med_krock.StartPage;
using Fia_med_krock.GameLogic;
using System.Runtime.ConstrainedExecution;
using Windows.Media.PlayTo;
using System.Collections;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409




namespace Fia_med_krock
{
    public static class MoveHelper
    {
        //Tar samma argument som 'movecar' funktionen
        public static void AnimateCarRight(Windows.UI.Xaml.Shapes.Rectangle carToMove, int columnNum)
        {
            //Animering för bilar
            //Skapar objekten som behövs för att göra animeringen
            carToMove.RenderTransform = new CompositeTransform();
            Storyboard oStoryboard = new Storyboard();
            DoubleAnimation oDoubleAnimation = new DoubleAnimation();

            //Värden som bestämmer vart elementet ska flyttas och hur snabbt.
            int startPosition = columnNum - 50;
            oDoubleAnimation.From = startPosition;
            oDoubleAnimation.To = startPosition + 50;
            oDoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(100));
            //oDoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(durationTime*200));

            //Applicerar animationen
            Storyboard.SetTarget(oDoubleAnimation, carToMove);
            //(CompositeTransform.TranslateY) flyttar vertikalt istället!
            Storyboard.SetTargetProperty(oDoubleAnimation, "(UIElement.RenderTransform).(CompositeTransform.TranslateX)");

            //Lägger till animationen i storyboard objektet
            oStoryboard.Children.Add(oDoubleAnimation);
            //Kör animering
            oStoryboard.Begin();
        }

        //Samma som de andra
        public static void AnimateCarLeft(Windows.UI.Xaml.Shapes.Rectangle carToMove, int columnNum)
        {
            carToMove.RenderTransform = new CompositeTransform();
            Storyboard oStoryboard = new Storyboard();
            DoubleAnimation oDoubleAnimation = new DoubleAnimation();
            int startPosition = columnNum + 50;
            oDoubleAnimation.From = startPosition;
            oDoubleAnimation.To = startPosition - 50;
            oDoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(100));
            Storyboard.SetTarget(oDoubleAnimation, carToMove);
            Storyboard.SetTargetProperty(oDoubleAnimation, "(UIElement.RenderTransform).(CompositeTransform.TranslateX)");
            oStoryboard.Children.Add(oDoubleAnimation);
            oStoryboard.Begin();
        }

        //Samma som de andra
        public static void AnimateCarDown(Windows.UI.Xaml.Shapes.Rectangle carToMove, int columnNum)
        {
            carToMove.RenderTransform = new CompositeTransform();
            Storyboard oStoryboard = new Storyboard();
            DoubleAnimation oDoubleAnimation = new DoubleAnimation();
            int startPosition = columnNum - 50;
            oDoubleAnimation.From = startPosition;
            oDoubleAnimation.To = startPosition + 50;
            oDoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(100));
            Storyboard.SetTarget(oDoubleAnimation, carToMove);
            Storyboard.SetTargetProperty(oDoubleAnimation, "(UIElement.RenderTransform).(CompositeTransform.TranslateY)");
            oStoryboard.Children.Add(oDoubleAnimation);
            oStoryboard.Begin();
        }

        //Samma som de andra
        public static void AnimateCarUp(Windows.UI.Xaml.Shapes.Rectangle carToMove, int columnNum)
        {
            carToMove.RenderTransform = new CompositeTransform();
            Storyboard oStoryboard = new Storyboard();
            DoubleAnimation oDoubleAnimation = new DoubleAnimation();
            int startPosition = columnNum + 50;
            oDoubleAnimation.From = startPosition;
            oDoubleAnimation.To = startPosition - 50;
            oDoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(100));
            Storyboard.SetTarget(oDoubleAnimation, carToMove);
            Storyboard.SetTargetProperty(oDoubleAnimation, "(UIElement.RenderTransform).(CompositeTransform.TranslateY)");
            oStoryboard.Children.Add(oDoubleAnimation);
            oStoryboard.Begin();
        }


        public static void MoveCar(Windows.UI.Xaml.Shapes.Rectangle carToMove, Grid playBoard, int columnNum, int rowNum)
        {
            var parent = (Panel)carToMove.Parent;

            if (parent != null)
            {
                parent.Children.Remove(carToMove);
            }

            playBoard.Children.Add(carToMove);
            Grid.SetRow(carToMove, rowNum);
            Grid.SetColumn(carToMove, columnNum);
        }
    }
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

        public enum CarColor
        {
            Red,
            Blue,
            Green,
            Yellow
        }
        private Dictionary<CarColor, Player> players;

        
        
        private GameState currentPlayer;
       // private PlayerAiStates playerAiStates;

        public MainPage()
        {
            this.InitializeComponent();
            currentPlayer = GameState.PlayerRed;
            CenterOfGrid.Fill = new SolidColorBrush(Colors.Red);
            RollDice.Background = new SolidColorBrush(Windows.UI.Colors.Red);
 
            //InitializePlayers();

            //music.mp3 downloaded from https://pixabay.com/
            // Uri newuri = new Uri("ms-appx:///Assets/music.mp3");
            // myPlayer.Source = newuri;
            //  myPlayer.Volume = 0.1;
        }

        private void InitializePlayers(PlayerAiStates playerAiStates)
        {
            players = new Dictionary<CarColor, Player>
            {
                { CarColor.Red, new Player("Red", new List<Windows.UI.Xaml.Shapes.Rectangle> { Red1, Red2, Red3, Red4 }, PlayBoard, playerAiStates.IsPlayer1Ai) },
                { CarColor.Blue, new Player("Blue", new List<Windows.UI.Xaml.Shapes.Rectangle> { Blue1, Blue2, Blue3, Blue4 }, PlayBoard, playerAiStates.IsPlayer2Ai) },
                { CarColor.Green, new Player("Green", new List<Windows.UI.Xaml.Shapes.Rectangle> { Green1, Green2, Green3, Green4 }, PlayBoard, playerAiStates.IsPlayer3Ai) },
                { CarColor.Yellow, new Player("Yellow", new List<Windows.UI.Xaml.Shapes.Rectangle> { Yellow1, Yellow2, Yellow3, Yellow4 }, PlayBoard, playerAiStates.IsPlayer4Ai) },
                
            };
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is MainPageParameters parameters)
            {
                List<string> playerNames = parameters.PlayerNames;
                PlayerAiStates playerAiStates = parameters.PlayerAiStates;
                Debug.WriteLine("Player AI States:");
                Debug.WriteLine($"Player 1 AI: {playerAiStates.IsPlayer1Ai}");
                Debug.WriteLine($"Player 2 AI: {playerAiStates.IsPlayer2Ai}");
                Debug.WriteLine($"Player 3 AI: {playerAiStates.IsPlayer3Ai}");
                Debug.WriteLine($"Player 4 AI: {playerAiStates.IsPlayer4Ai}");
                InitializePlayers(playerAiStates);
            }
            
            MainGameLoop();
        }

        //RedCarsRoad är en array som redovisar vilken väg dom röda bilarna ska köra.
        //Sen har man en int (Cars.steps) för varje bil som anger bilen position m.h.a. RedCarsRoad[] 
        //Road för red cars, {column,row}
        public static string[] RedCarsRoad = { "0003", "0103", "0203", "0303", "0302", "0301", "0300", "0400", "0500", "0501", "0502", "0503", "0603", "0703", "0803", "0804", "0805", "0705", "0605", "0505", "0506", "0507", "0508", "0408", "0308", "0307", "0306", "0305", "0205", "0105", "0005", "0004", "0104", "0204", "0304", "0404", "0000", "0000" };
        public static string[] BlueCarsRoad = { "0500", "0501", "0502", "0503", "0603", "0703", "0803", "0804", "0805", "0705", "0605", "0505", "0506", "0507", "0508", "0408", "0308", "0307", "0306", "0305", "0205", "0105", "0005", "0004", "0003", "0103", "0203", "0303", "0302", "0301", "0300", "0400", "0401", "0402", "0403", "0404", "0000", "0000" };
        public static string[] GreenCarsRoad = { "0805", "0705", "0605", "0505", "0506", "0507", "0508", "0408", "0308", "0307", "0306", "0305", "0205", "0105", "0005", "0004", "0003", "0103", "0203", "0303", "0302", "0301", "0300", "0400", "0500", "0501", "0502", "0503", "0603", "0703", "0803", "0804", "0704", "0604", "0504", "0404", "0000", "0000" };
        public static string[] YellowCarsRoad = { "0308", "0307", "0306", "0305", "0205", "0105", "0005", "0004", "0003", "0103", "0203", "0303", "0302", "0301", "0300", "0400", "0500", "0501", "0502", "0503", "0603", "0703", "0803", "0804", "0805", "0705", "0605", "0505", "0506", "0507", "0508", "0408", "0407", "0406", "0405", "0404", "0000", "0000" };

        public bool goForward = true;
        public bool turnActive = true;



        //Finns inte globala variabler i c#, så gjorde en ful lösning från https://stackoverflow.com/questions/14368129/how-to-use-global-variables-in-c
        public static class Globals
        {
            public static int dice_result = 0;
        }




        private async Task MainGameLoop()
        {
            while (true) 
            {
  
                foreach (var playerKVP in players)
                {
                    turnActive = true;
                    Player player = playerKVP.Value;

                    if (playerKVP.Value.Color == "Red") currentPlayer = GameState.PlayerRed;
                    else if (playerKVP.Value.Color == "Blue") currentPlayer = GameState.PlayerBlue;
                    else if (playerKVP.Value.Color == "Green") currentPlayer = GameState.PlayerGreen;
                    else currentPlayer = GameState.PlayerYellow;
                    CenterOfGrid.Fill = GetColorForPlayer(player);
                    RollDice.Background = GetColorForPlayer(player);
                    

                    if (player.IsAi)
                    {
                        RollDice.Content = "AI";
                        Debug.WriteLine($"Simulating turn for {player.Color}");
                        await SimulateAiPlayerTurn(player);
                        //await TestTurn(player);
                    }
                    else
                    {
                        RollDice.Content = "Rulla Tärning";
                        await HandleHumanPlayerTurn(player);
                    }

                    
                }
            }
        }
        /// <summary>
        /// Randomizes a new dice roll,
        /// this is saved in Globals.dice_result
        /// </summary>
        /// <returns>A number between 1-6</returns>
        private int roll_dice()
        {
            Random dice_roll = new Random();
            //Slumpar ett värde mellan 1 och 6. Maxvärdet 7 kan inte slumpas.
            int roll_result = Convert.ToInt32(dice_roll.Next(1, 7));
            Globals.dice_result = roll_result;
            roll_dice_animation(roll_result);
            
            return roll_result;
        }

        public async void RollDice_Click(object sender, RoutedEventArgs e)
        {
            //diceroll.mp3 downloaded from https://pixabay.com/
            Uri newuri = new Uri("ms-appx:///Assets/diceroll.mp3");
            diceRoll.Source = newuri;
            int dice = roll_dice();
            RollDice.Content = dice;
            RollDice.IsEnabled = false;
            setCurrentPlayerCarsState(dice);
            bool anyCarsEnabled = CheckAnyCarsEnabled();
            
            if (!anyCarsEnabled)
            {
                // Ingen bil kan röras och därav byts det tur
                await Task.Delay(100);
                turnActive = false;
                
            }

        }

        private SolidColorBrush GetColorForPlayer(Player player)
        {
            switch (player.Color)
            {
                case "Red":
                    return new SolidColorBrush(Colors.Red);
                case "Blue":
                    return new SolidColorBrush(Colors.Blue);
                case "Green":
                    return new SolidColorBrush(Colors.Green);
                case "Yellow":
                    return new SolidColorBrush(Colors.Yellow);
                default:
                    return new SolidColorBrush(Colors.Transparent);
            }
        }


        //tanken att kolla så att om ingen pjäs kan flyttas skiftas turen till nästa spelare
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
                    return true;
            }
        }

        private async Task HandleHumanPlayerTurn(Player humanPlayer)
        {
            Debug.WriteLine($"Test human player: {humanPlayer.Cars}");
            RollDice.IsEnabled = true;
            while (turnActive)
            {
                await Task.Delay(1000);
            }
        }


        private async Task SimulateAiPlayerTurn (Player aiPlayer)
        {
            await Task.Delay(1500);
            int aiDiceValue = 0;
            while (turnActive)
            {
                Debug.WriteLine($"Starting simulation for {aiPlayer.Color}: SimulateAiPlayerTurn function");
                RollDice.IsEnabled = false;
                aiDiceValue = roll_dice();
                if (aiDiceValue != 6) turnActive = false;
                else
                {
                    await Task.Delay(200);
                    RollDice.Content = "AI";
                    setCurrentPlayerCarsState(aiDiceValue);
                    await SimulateMoveCar(aiPlayer, aiDiceValue);
                }    
            }
                    
            await Task.Delay(200);
            setCurrentPlayerCarsState(aiDiceValue);
            await SimulateMoveCar(aiPlayer, aiDiceValue);

            Debug.WriteLine($"exiting simulation {aiPlayer.Color}");
        }

        private async Task SimulateMoveCar(Player aiPlayer, int aiDiceValue)
        {
            Debug.WriteLine($"Simulating move for {aiPlayer.Color}");
            string[] carsRoad = GetCarsRoad(aiPlayer.Color);
            foreach (Cars car in aiPlayer.Cars) 
            { 
                if (car.CarUI.IsTapEnabled) 
                {
                    await AnimateCarAsync(car.CarUI, carsRoad, aiDiceValue,  car, aiPlayer, PlayBoard);
                    RollDice.Content = "AI";
                    RollDice.IsEnabled = false;
                    await Task.Delay(200);
                    break;
                }
                
            }
            await Task.Delay(200);
        }
 

        private void setCurrentPlayerCarsState(int dice)
        {
            Player currentPlayerObj = players[GetCarColor(currentPlayer)];
            SetTapEnabledForPlayer(currentPlayerObj.Cars[0], dice);
            SetTapEnabledForPlayer(currentPlayerObj.Cars[1], dice);
            SetTapEnabledForPlayer(currentPlayerObj.Cars[2], dice);
            SetTapEnabledForPlayer(currentPlayerObj.Cars[3], dice);
        }   

        private CarColor GetCarColor(GameState playerState)
        {
            switch (playerState)
            {
                case GameState.PlayerRed:
                    return CarColor.Red;
                case GameState.PlayerBlue:
                    return CarColor.Blue;
                case GameState.PlayerGreen:
                    return CarColor.Green;
                case GameState.PlayerYellow:
                    return CarColor.Yellow;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetTapEnabledForPlayer(Cars car, int dice)
        {
            Windows.UI.Xaml.Shapes.Rectangle carUI = car.CarUI;
            if (car.steps != -1)
            {
                carUI.IsTapEnabled = true;
                carUI.Opacity = 1;
            }
            else
            {
                if (dice == 1 || dice == 6)
                {
                    carUI.IsTapEnabled = true;
                    carUI.Opacity = 1;
                }
                else
                {
                    carUI.IsTapEnabled = false;
                    carUI.Opacity = 0.3;
                }
               
            }
            if(car.steps == 37)
            {
                carUI.IsTapEnabled = false;
            }
        }
        private void SetTapDisabeldForPlayer()
        {
            Player currentPlayerObj = players[GetCarColor(currentPlayer)];
            Windows.UI.Xaml.Shapes.Rectangle carUI = currentPlayerObj.Cars[0].CarUI;
            if (currentPlayerObj.Cars[0].steps == -1)
            {
                carUI.IsTapEnabled = false;
                carUI.Opacity = 0.3;
            }
            carUI = currentPlayerObj.Cars[1].CarUI;
            if (currentPlayerObj.Cars[1].steps == -1)
            {
                carUI.IsTapEnabled = false;
                carUI.Opacity = 0.3;
            }
            carUI = currentPlayerObj.Cars[2].CarUI;
            if (currentPlayerObj.Cars[2].steps == -1)
            {
                carUI.IsTapEnabled = false;
                carUI.Opacity = 0.3;
            }
            carUI = currentPlayerObj.Cars[3].CarUI;
            if (currentPlayerObj.Cars[3].steps == -1)
            {
                carUI.IsTapEnabled = false;
                carUI.Opacity = 0.3;
            }
        }

            Dictionary<CarColor, List<Cars>> GetCarsByColorDictionary()
        {
            return players.ToDictionary(entry => entry.Key, entry => entry.Value.Cars);
        }

        // Helper method to convert string color to enum
        CarColor GetCarColorEnum(string color)
        {
            return (CarColor)Enum.Parse(typeof(CarColor), color, true);
        }

        bool CheckMyOtherCarsPosition(Cars car, bool goForward) //You are not allowed to pass your own cars
        {
            bool check = true;
            int movingCarPosition = car.steps + 1;
            if (!goForward)
            {
                movingCarPosition = car.steps - 1;
            }

            if (movingCarPosition > 0)
            {
                Dictionary<CarColor,List <Cars>> carsByColor = GetCarsByColorDictionary();
                CarColor carColor = GetCarColorEnum(car.color);

                if (carsByColor.TryGetValue(carColor, out List<Cars> otherCars) && otherCars.Any(otherCar => movingCarPosition == otherCar.steps))
                {
                    check = false;
                }
            }

            return check;
        }

        void CheckCarPositionToCrash(Cars car)
        {
            string movingCarPosition = GetCarRoadPosition(car);

            foreach (var player in players.Values)
            {
                if (player.Color != GetCarColor(currentPlayer).ToString())
                {
                    foreach (var playerCar in player.Cars)
                    {
                        if (playerCar.steps > -1 && playerCar != car)
                        {
                            if (movingCarPosition == GetCarRoadPosition(playerCar))
                            {
                                playerCar.CarCrasced();
                                                                 
                            }
                        }
                    }
                }
            }
        }

        string GetCarRoadPosition(Cars car)
        {
            switch (car.color)
            {
                case "Red": return RedCarsRoad[car.steps];
                case "Blue": return BlueCarsRoad[car.steps];
                case "Green": return GreenCarsRoad[car.steps];
                case "Yellow": return YellowCarsRoad[car.steps];
                default: return "";
            }
        }
        public async void roll_dice_animation(int dice)
        {
            DiceAnimation.Source = new BitmapImage(new Uri("ms-appx:///Assets/dice2.png"));
         
            await System.Threading.Tasks.Task.Delay(150);
            DiceAnimation.Source = new BitmapImage(new Uri("ms-appx:///Assets/dice6.png"));
        
            await System.Threading.Tasks.Task.Delay(150);
            DiceAnimation.Source = new BitmapImage(new Uri("ms-appx:///Assets/dice3.png"));
         
            await System.Threading.Tasks.Task.Delay(150);
            DiceAnimation.Source = new BitmapImage(new Uri("ms-appx:///Assets/dice4.png"));
         
            await System.Threading.Tasks.Task.Delay(150);
            DiceAnimation.Source = new BitmapImage(new Uri("ms-appx:///Assets/dice6.png"));
         

            if (dice == 6)
            {
                DiceAnimation.Source = new BitmapImage(new Uri("ms-appx:///Assets/dice1.png"));
               
                await System.Threading.Tasks.Task.Delay(150);
                DiceAnimation.Source = new BitmapImage(new Uri("ms-appx:///Assets/dice6.png"));
               
            }
            else if (dice == 5)
            {
                DiceAnimation.Source = new BitmapImage(new Uri("ms-appx:///Assets/dice1.png"));
                
                await System.Threading.Tasks.Task.Delay(150);
                DiceAnimation.Source = new BitmapImage(new Uri("ms-appx:///Assets/dice5.png"));
                
            }
            else if (dice == 4)
            {
                DiceAnimation.Source = new BitmapImage(new Uri("ms-appx:///Assets/dice1.png"));
                
                await System.Threading.Tasks.Task.Delay(150);
                DiceAnimation.Source = new BitmapImage(new Uri("ms-appx:///Assets/dice4.png"));
               
            }
            else if (dice == 3)
            {
                DiceAnimation.Source = new BitmapImage(new Uri("ms-appx:///Assets/dice1.png"));
                
                await System.Threading.Tasks.Task.Delay(150);
                DiceAnimation.Source = new BitmapImage(new Uri("ms-appx:///Assets/dice3.png"));
                
            }
            else if (dice == 2)
            {
                DiceAnimation.Source = new BitmapImage(new Uri("ms-appx:///Assets/dice1.png"));
               
                await System.Threading.Tasks.Task.Delay(150);
                DiceAnimation.Source = new BitmapImage(new Uri("ms-appx:///Assets/dice2.png"));
             
            }
            else
            {
                DiceAnimation.Source = new BitmapImage(new Uri("ms-appx:///Assets/dice5.png"));
                
                await System.Threading.Tasks.Task.Delay(150);
                DiceAnimation.Source = new BitmapImage(new Uri("ms-appx:///Assets/dice1.png"));
            }
        }

        private async Task AnimateCarAsync(Windows.UI.Xaml.Shapes.Rectangle carToMove, string[] CarsRoad, int dice, Cars car, Player player, Grid playBoard)
        {
            DisableAllCarsForCurrentPlayer();
            //int destination = Math.Min(car.steps + dice, 35);
            goForward = true;
            int MovesToMake = 0;

            //for (int i = car.steps; i < destination; i++)
            while (MovesToMake < dice)
            {
                if (car.steps == 35)
                {
                    goForward = false;
                }

                if (!CheckMyOtherCarsPosition(car, goForward))
                {
                    Debug.WriteLine("CheckCarPosition failed");
                    //RollDice.IsEnabled = true;
                    break;
                }

                if (goForward == true)
                {
                    car.StepCar();
                }
                else
                {
                    car.StepCarBack();
                }

                Debug.WriteLine($"Car steps: {car.steps}");
                Debug.WriteLine($"Player: {players[0].Cars[0].steps}");
                Debug.WriteLine($"Player: {player.CheckIfWinner()}");
                SetTapDisabeldForPlayer();
                int columnNum = Convert.ToInt32(CarsRoad[car.steps].Substring(0, 2));
                int rowNum = Convert.ToInt32(CarsRoad[car.steps].Substring(2, 2));
                MoveHelper.MoveCar(carToMove, playBoard, columnNum, rowNum);
                //Kör animationen
                PickAnimation(carToMove, columnNum, car.steps, currentPlayer);
                carToMove.IsTapEnabled = false;  
                await Task.Delay(200);
                MovesToMake++;

                if (car.steps == 35 && MovesToMake == dice)
                {
                    car.StepCarToGoal();
                    carToMove.Visibility = Visibility.Collapsed;
                    carToMove.IsTapEnabled = false;
                    Debug.WriteLine("Car reached destination");
                    if (player.CheckIfWinner()) Debug.WriteLine("We have a winner");
                }
            }

            if (car.steps < 32)
            {
                CheckCarPositionToCrash(car);
            }
            //RollDice.IsEnabled = true;

            if (dice != 6)
            {
                if (dice == 1) SetTapDisabeldForPlayer();  //dummy call to set if dice == 1

                turnActive = false;
                
            }
            else
            {
                RollDice.Content = "Rulla Tärning";
                RollDice.IsEnabled = true;
                SetTapDisabeldForPlayer();
                
            }
        }

        //TODO: Gör så att animationen funkar 'baklänges' när man tar för många steg för att gå i mål.
        //FIXME: Gör så att funktionen inte är världens största if-else träd.
        private void PickAnimation(Windows.UI.Xaml.Shapes.Rectangle carToMove, int columnNum, int car_steps, GameState active_player)
        {
            //Väljer olika animationer beroende på vilken spelare som kör.
            if (active_player == GameState.PlayerRed)
            {
                if (car_steps <= 3)
                {
                    MoveHelper.AnimateCarRight(carToMove, columnNum);
                }
                else if (car_steps <= 6)
                {
                    MoveHelper.AnimateCarUp(carToMove, columnNum);
                }
                else if (car_steps <= 8)
                {
                    MoveHelper.AnimateCarRight(carToMove, columnNum);
                }
                else if (car_steps <= 11)
                {
                    MoveHelper.AnimateCarDown(carToMove, columnNum);
                }
                else if (car_steps <= 14)
                {
                    MoveHelper.AnimateCarRight(carToMove, columnNum);
                }
                else if (car_steps <= 16)
                {
                    MoveHelper.AnimateCarDown(carToMove, columnNum);
                }
                else if (car_steps <= 19)
                {
                    MoveHelper.AnimateCarLeft(carToMove, columnNum);
                }
                else if (car_steps <= 22)
                {
                    MoveHelper.AnimateCarDown(carToMove, columnNum);
                }
                else if (car_steps <= 24)
                {
                    MoveHelper.AnimateCarLeft(carToMove, columnNum);
                }
                else if (car_steps <= 27)
                {
                    MoveHelper.AnimateCarUp(carToMove, columnNum);
                }
                else if (car_steps <= 30)
                {
                    MoveHelper.AnimateCarLeft(carToMove, columnNum);
                }
                else if (car_steps <= 31)
                {
                    MoveHelper.AnimateCarUp(carToMove, columnNum);
                }
                else if (car_steps >= 32)
                {
                    MoveHelper.AnimateCarRight(carToMove, columnNum);
                }
            }
            if (active_player == GameState.PlayerBlue)
            {
                if (car_steps <= 3)
                {
                    MoveHelper.AnimateCarDown(carToMove, columnNum);
                }
                else if (car_steps <= 6)
                {
                    MoveHelper.AnimateCarRight(carToMove, columnNum);
                }
                else if (car_steps <= 8)
                {
                    MoveHelper.AnimateCarDown(carToMove, columnNum);
                }
                else if (car_steps <= 11)
                {
                    MoveHelper.AnimateCarLeft(carToMove, columnNum);
                }
                else if (car_steps <= 14)
                {
                    MoveHelper.AnimateCarDown(carToMove, columnNum);
                }
                else if (car_steps <= 16)
                {
                    MoveHelper.AnimateCarLeft(carToMove, columnNum);
                }
                else if (car_steps <= 19)
                {
                    MoveHelper.AnimateCarUp(carToMove, columnNum);
                }
                else if (car_steps <= 22)
                {
                    MoveHelper.AnimateCarLeft(carToMove, columnNum);
                }
                else if (car_steps <= 24)
                {
                    MoveHelper.AnimateCarUp(carToMove, columnNum);
                }
                else if (car_steps <= 27)
                {
                    MoveHelper.AnimateCarRight(carToMove, columnNum);
                }
                else if (car_steps <= 30)
                {
                    MoveHelper.AnimateCarUp(carToMove, columnNum);
                }
                else if (car_steps <= 31)
                {
                    MoveHelper.AnimateCarRight(carToMove, columnNum);
                }
                else if (car_steps >= 32)
                {
                    MoveHelper.AnimateCarDown(carToMove, columnNum);
                }
            }
            if (active_player == GameState.PlayerGreen)
            {
                if (car_steps <= 3)
                {
                    MoveHelper.AnimateCarLeft(carToMove, columnNum);
                }
                else if (car_steps <= 6)
                {
                    MoveHelper.AnimateCarDown(carToMove, columnNum);
                }
                else if (car_steps <= 8)
                {
                    MoveHelper.AnimateCarLeft(carToMove, columnNum);
                }
                else if (car_steps <= 11)
                {
                    MoveHelper.AnimateCarUp(carToMove, columnNum);
                }
                else if (car_steps <= 14)
                {
                    MoveHelper.AnimateCarLeft(carToMove, columnNum);
                }
                else if (car_steps <= 16)
                {
                    MoveHelper.AnimateCarUp(carToMove, columnNum);
                }
                else if (car_steps <= 19)
                {
                    MoveHelper.AnimateCarRight(carToMove, columnNum);
                }
                else if (car_steps <= 22)
                {
                    MoveHelper.AnimateCarUp(carToMove, columnNum);
                }
                else if (car_steps <= 24)
                {
                    MoveHelper.AnimateCarRight(carToMove, columnNum);
                }
                else if (car_steps <= 27)
                {
                    MoveHelper.AnimateCarDown(carToMove, columnNum);
                }
                else if (car_steps <= 30)
                {
                    MoveHelper.AnimateCarRight(carToMove, columnNum);
                }
                else if (car_steps <= 31)
                {
                    MoveHelper.AnimateCarDown(carToMove, columnNum);
                }
                else if (car_steps >= 32)
                {
                    MoveHelper.AnimateCarLeft(carToMove, columnNum);
                }
            }
            if (active_player == GameState.PlayerYellow)
            {
                if (car_steps <= 3)
                {
                    MoveHelper.AnimateCarUp(carToMove, columnNum);
                }
                else if (car_steps <= 6)
                {
                    MoveHelper.AnimateCarLeft(carToMove, columnNum);
                }
                else if (car_steps <= 8)
                {
                    MoveHelper.AnimateCarUp(carToMove, columnNum);
                }
                else if (car_steps <= 11)
                {
                    MoveHelper.AnimateCarRight(carToMove, columnNum);
                }
                else if (car_steps <= 14)
                {
                    MoveHelper.AnimateCarUp(carToMove, columnNum);
                }
                else if (car_steps <= 16)
                {
                    MoveHelper.AnimateCarRight(carToMove, columnNum);
                }
                else if (car_steps <= 19)
                {
                    MoveHelper.AnimateCarDown(carToMove, columnNum);
                }
                else if (car_steps <= 22)
                {
                    MoveHelper.AnimateCarRight(carToMove, columnNum);
                }
                else if (car_steps <= 24)
                {
                    MoveHelper.AnimateCarDown(carToMove, columnNum);
                }
                else if (car_steps <= 27)
                {
                    MoveHelper.AnimateCarLeft(carToMove, columnNum);
                }
                else if (car_steps <= 30)
                {
                    MoveHelper.AnimateCarDown(carToMove, columnNum);
                }
                else if (car_steps <= 31)
                {
                    MoveHelper.AnimateCarLeft(carToMove, columnNum);
                }
                else if (car_steps >= 32)
                {
                    MoveHelper.AnimateCarUp(carToMove, columnNum);
                }
            }
        }


        private void DisableAllCarsForCurrentPlayer()
        {
            switch (currentPlayer)
            {
                case GameState.PlayerRed:
                    Red1.IsTapEnabled = false;
                    Red2.IsTapEnabled = false;
                    Red3.IsTapEnabled = false;
                    Red4.IsTapEnabled = false;
                 //   Red1.Opacity = 0.3;
                 //   Red2.Opacity = 0.3;
                 //   Red3.Opacity = 0.3;
                 //   Red4.Opacity = 0.3;
                    break;
                case GameState.PlayerBlue:
                    Blue1.IsTapEnabled = false;
                    Blue2.IsTapEnabled = false;
                    Blue3.IsTapEnabled = false;
                    Blue4.IsTapEnabled = false;
                 //   Blue1.Opacity = 0.3;
                 //   Blue2.Opacity = 0.3;
                 //   Blue3.Opacity = 0.3;
                 //   Blue4.Opacity = 0.3;
                    break;
                case GameState.PlayerGreen:
                    Green1.IsTapEnabled = false;
                    Green2.IsTapEnabled = false;
                    Green3.IsTapEnabled = false;
                    Green4.IsTapEnabled = false;
                 //   Green1.Opacity = 0.3;
                 //   Green2.Opacity = 0.3;
                 //   Green3.Opacity = 0.3;
                 //   Green4.Opacity = 0.3;
                    break;
                case GameState.PlayerYellow:
                    Yellow1.IsTapEnabled = false;
                    Yellow2.IsTapEnabled = false;
                    Yellow3.IsTapEnabled = false;
                    Yellow4.IsTapEnabled = false;
                 //   Yellow1.Opacity = 0.3;
                 //   Yellow2.Opacity = 0.3;
                 //   Yellow3.Opacity = 0.3;
                 //   Yellow4.Opacity = 0.3;
                    break;

                default:
                    break;
            }
        }

        private async void CarRectangle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                Windows.UI.Xaml.Shapes.Rectangle carUI = (Windows.UI.Xaml.Shapes.Rectangle)sender;
                Cars tappedCar = GetCarFromUI(carUI);

                if (tappedCar != null)
                {
                    string[] carsRoad = GetCarsRoad(tappedCar.color);
                    Player player = GetPlayerByCarColor(tappedCar.color);

                    await AnimateCarAsync(carUI, carsRoad, Globals.dice_result, tappedCar, player, PlayBoard);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CarRectangle_Tapped Error: {ex.Message}");
            }
            
            DisableAllCarsForCurrentPlayer();

        }


        private Cars GetCarFromUI(Windows.UI.Xaml.Shapes.Rectangle carUI)
        {
            // Iterate through all players and their cars to find the matching UI element
            foreach (var player in players.Values)
            {
                foreach (var car in player.Cars)
                {
                    if (car.CarUI == carUI)
                    {
                        return car;
                    }
                }
            }

            return null;
        }

        private string[] GetCarsRoad(string carColor)
        {
            switch (carColor)
            {
                case "Red":
                    return RedCarsRoad;
                case "Blue":
                    return BlueCarsRoad;
                case "Green":
                    return GreenCarsRoad;
                case "Yellow":
                    return YellowCarsRoad;
                default:
                    return null; 
            }
        }

        private Player GetPlayerByCarColor(string carColor)
        {

            Player currentPlayer = players[CarColor.Yellow];
            for (int i = 0; i < 3; i++)
            {
                Debug.WriteLine($"Test:{i}  {players.ElementAt(i).Value.Color}");
                string colour = players.ElementAt(i).Value.Color;
                if (carColor == colour)
                {
                    if (i == 0)
                    { currentPlayer = players[CarColor.Red]; }
                    else if (i == 1)
                    { currentPlayer = players[CarColor.Blue]; }
                    else { currentPlayer = players[CarColor.Red]; }
                }
            }  
            return currentPlayer;
        }

    }
}