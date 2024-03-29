﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static Fia_med_krock.StartPage;
using Fia_med_krock.GameLogic;
using Windows.UI.Xaml.Controls.Maps;



namespace Fia_med_krock
{
    /// <summary>
    /// GamePage
    /// Mainpage for the game
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
        public bool goForward = true;
        public bool turnActive = true;
        public bool gameOver = false; 

        /// <summary>
        /// RedCarsRoad - YellowCarsRoad an array that holds gridnumber for each position of each color
        /// {column,row}
        /// </summary>
        public static string[] RedCarsRoad = { "0003", "0103", "0203", "0303", "0302", "0301", "0300", "0400", "0500", "0501", "0502", "0503", "0603", "0703", "0803", "0804", "0805", "0705", "0605", "0505", "0506", "0507", "0508", "0408", "0308", "0307", "0306", "0305", "0205", "0105", "0005", "0004", "0104", "0204", "0304", "0404", "0000", "0000" };
        public static string[] BlueCarsRoad = { "0500", "0501", "0502", "0503", "0603", "0703", "0803", "0804", "0805", "0705", "0605", "0505", "0506", "0507", "0508", "0408", "0308", "0307", "0306", "0305", "0205", "0105", "0005", "0004", "0003", "0103", "0203", "0303", "0302", "0301", "0300", "0400", "0401", "0402", "0403", "0404", "0000", "0000" };
        public static string[] GreenCarsRoad = { "0805", "0705", "0605", "0505", "0506", "0507", "0508", "0408", "0308", "0307", "0306", "0305", "0205", "0105", "0005", "0004", "0003", "0103", "0203", "0303", "0302", "0301", "0300", "0400", "0500", "0501", "0502", "0503", "0603", "0703", "0803", "0804", "0704", "0604", "0504", "0404", "0000", "0000" };
        public static string[] YellowCarsRoad = { "0308", "0307", "0306", "0305", "0205", "0105", "0005", "0004", "0003", "0103", "0203", "0303", "0302", "0301", "0300", "0400", "0500", "0501", "0502", "0503", "0603", "0703", "0803", "0804", "0805", "0705", "0605", "0505", "0506", "0507", "0508", "0408", "0407", "0406", "0405", "0404", "0000", "0000" };

        //Gloabal variables
        public static class Globals
        {
            public static int dice_result = 0;
        }


        public MainPage()
        {
            
            this.InitializeComponent();
            currentPlayer = GameState.PlayerRed;
            CenterOfGrid.Fill = new SolidColorBrush(Colors.Red);
            RollDice.Background = new SolidColorBrush(Windows.UI.Colors.Red);

            //music.mp3 downloaded from https://pixabay.com/
            Uri backGroundMusic = new Uri("ms-appx:///Assets/music.mp3");
            myPlayer.Source = backGroundMusic;
            myPlayer.Volume = 0.07;
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
                InitializePlayers(playerAiStates);
            }     
            MainGameLoop();
        }

        private async void MainGameLoop()
        {
            while (!gameOver) 
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

        private void showWinnerScreen(Player winner)
        {
            string winnerName = "Gula";
            WinnerScreen.Background = new SolidColorBrush(Windows.UI.Colors.Yellow);
            if (winner.Color == "Red")
            {
                winnerName = "Röda";
                WinnerScreen.Background = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else if (winner.Color == "Blue")
            {
                winnerName = "Blåa";
                WinnerScreen.Background = new SolidColorBrush(Windows.UI.Colors.Blue);
            }
            else if (winner.Color == "Green")
            {
                winnerName = "Gröna";
                WinnerScreen.Background = new SolidColorBrush(Windows.UI.Colors.Green);
            }

            WinnerText.Text = $"{winnerName} spelaren har vunnit!";
            WinnerScreen.Visibility = Visibility.Visible;
            WinnerAnimation.Begin();
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
            MainAnimations.roll_dice_animation(roll_result, DiceAnimation);
            return roll_result;
        }


        /// <summary>
        /// When clicking the roll dice button plays the sound for the dice effect and 
        /// and uses roll_dice function. Uses CheckAnyCarsEnabled to see if turn should be ended.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void RollDice_Click(object sender, RoutedEventArgs e)
        {
            //diceroll.mp3 downloaded from https://pixabay.com/
            Uri diceRoller = new Uri("ms-appx:///Assets/diceroll.mp3");
            diceRoll.Source = diceRoller;
            diceRoll.Volume = 0.5;
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


        /// <summary>
        /// Used to see if player can move any piece, if not the idea is to be able to use this to change turns
        /// </summary>
        /// <returns>Boolean</returns>
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
            RollDice.IsEnabled = true;
            while (turnActive)
            {
                await Task.Delay(1000);
            }
        }


        /// <summary>
        /// Simulates a turn for the AI player. Makes a dice roll and moves the first available car in the list for the player according to dice roll.
        /// Ends turn according to rules e.g when 6 not rolleed. 
        /// </summary>
        /// <param name="aiPlayer">The Player object used to access and move the correct cars for example</param>
        private async Task SimulateAiPlayerTurn (Player aiPlayer)
        {
            await Task.Delay(1500);
            int aiDiceValue = 0;
            while (turnActive)
            {
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

        }

        private async Task SimulateMoveCar(Player aiPlayer, int aiDiceValue)
        {
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

        /// <summary>
        /// Helper method to convert string color to enum
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        CarColor GetCarColorEnum(string color)
        {
            return (CarColor)Enum.Parse(typeof(CarColor), color, true);
        }

        /// <summary>
        /// CheckMyOtherCarsPosition contols the position of players other cars on gameplan. To make sure you 
        /// cant move past your own piece.
        /// </summary>
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

        /// <summary>
        /// CheckCarPositionToCrash contols the position of all opponent cars and if match removes the opponent car from gameplan.
        /// </summary>
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
        /// <summary>
        /// AnimateCarAsync - method that handles moving the cars on gameplan.
        /// </summary>
        private async Task AnimateCarAsync(Windows.UI.Xaml.Shapes.Rectangle carToMove, string[] CarsRoad, int dice, Cars car, Player player, Grid playBoard)
        {
            DisableAllCarsForCurrentPlayer();
            goForward = true;
            int MovesToMake = 0;

            while (MovesToMake < dice)
            {
                if (car.steps == 35)
                {
                    goForward = false;
                }

                if (!CheckMyOtherCarsPosition(car, goForward))
                {
                    Debug.WriteLine("CheckCarPosition failed");
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

                SetTapDisabeldForPlayer();
                int columnNum = Convert.ToInt32(CarsRoad[car.steps].Substring(0, 2));
                int rowNum = Convert.ToInt32(CarsRoad[car.steps].Substring(2, 2));
                MoveHelper.MoveCar(carToMove, playBoard, columnNum, rowNum);
                //Run animation
                MainAnimations.PickAnimation(carToMove, columnNum, car.steps, currentPlayer, goForward);
                carToMove.IsTapEnabled = false;  
                await Task.Delay(200);
                MovesToMake++;

                if (car.steps == 35 && MovesToMake == dice)
                {
                    car.StepCarToGoal();
                    carToMove.Visibility = Visibility.Collapsed;
                    carToMove.IsTapEnabled = false;
                    if (player.CheckIfWinner()) 
                    { 
                        gameOver = true; 
                        showWinnerScreen(player); 
                    } 
                }
            }

            if (car.steps < 32)
            {
                CheckCarPositionToCrash(car);
            }

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

       

        private void DisableAllCarsForCurrentPlayer()
        {
            switch (currentPlayer)
            {
                case GameState.PlayerRed:
                    Red1.IsTapEnabled = false;
                    Red2.IsTapEnabled = false;
                    Red3.IsTapEnabled = false;
                    Red4.IsTapEnabled = false;
                    break;
                case GameState.PlayerBlue:
                    Blue1.IsTapEnabled = false;
                    Blue2.IsTapEnabled = false;
                    Blue3.IsTapEnabled = false;
                    Blue4.IsTapEnabled = false;
                    break;
                case GameState.PlayerGreen:
                    Green1.IsTapEnabled = false;
                    Green2.IsTapEnabled = false;
                    Green3.IsTapEnabled = false;
                    Green4.IsTapEnabled = false;
                    break;
                case GameState.PlayerYellow:
                    Yellow1.IsTapEnabled = false;
                    Yellow2.IsTapEnabled = false;
                    Yellow3.IsTapEnabled = false;
                    Yellow4.IsTapEnabled = false;
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


        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void GoToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(StartPage));
        }

        /// <summary>
        /// Iterate through all players and their cars to find the matching UI element
        /// </summary>
        /// <param name="carUI"></param>
        /// <returns>car</returns>

        private Cars GetCarFromUI(Windows.UI.Xaml.Shapes.Rectangle carUI)
        {
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