﻿using System;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409




namespace Fia_med_krock
{

    /// <summary>
    /// GamePage.
    /// </summary>
    public sealed partial class MainPage : Page

    {
        //RedCarsRoad är en array som redovisar vilken väg dom röda bilarna ska köra, bara dom första 7 positionerna finns än så länge.
        //Sen har man en int (positionRedCar1)för varje bil som anger bilen position mha RedCarsRoad[] 
        //Road för red cars, {column,row}
        public static string[] RedCarsRoad = { "0003", "0103", "0203", "0303", "0302", "0301", "0300", "0400", "0500", "0501", "0502", "0503", "0603", "0703", "0803", "0804", "0805", "0705", "0605", "0505", "0506", "0507", "0508", "0408", "0308", "0307", "0306", "0305", "0205", "0105", "0005", "0004", "0104", "0204", "0304", "0404"};
        public int positionRedCar1 = 1;
        public int positionRedCar2 = 1;
        public int positionRedCar3 = 1;
        public int positionRedCar4 = 1;


        public MainPage()
        {
            this.InitializeComponent();
        }

        private void MoveRedCar1(int columnNum, int rowNum)
        {
            
            PlayBoard.Children.Remove(RedCar1);
            //await System.Threading.Tasks.Task.Delay(10);
            PlayBoard.Children.Add(RedCar1);
            Grid.SetRow(RedCar1, rowNum);
            Grid.SetColumn(RedCar1, columnNum);
        }

        private void MoveCar(Windows.UI.Xaml.Shapes.Rectangle carToMove, int columnNum, int rowNum)
        {
            PlayBoard.Children.Remove(carToMove);
            //await System.Threading.Tasks.Task.Delay(10);
            PlayBoard.Children.Add(carToMove);
            Grid.SetRow(carToMove, rowNum);
            Grid.SetColumn(carToMove, columnNum);
        }

        private void MoveCarStart(Windows.UI.Xaml.Shapes.Rectangle carToMove, int columnNum, int rowNum)
        {
            PlayBoard.Children.Remove(carToMove);
            //await System.Threading.Tasks.Task.Delay(10);
            PlayBoard.Children.Add(carToMove);
            Grid.SetRow(carToMove, rowNum);
            Grid.SetColumn(carToMove, columnNum);
        }


        private void MoveRedCar3(int columnNum, int rowNum)
        {

            PlayBoard.Children.Remove(RedCar3);
            //await System.Threading.Tasks.Task.Delay(10);
            PlayBoard.Children.Add(RedCar3);
            Grid.SetRow(RedCar3, rowNum);
            Grid.SetColumn(RedCar3, columnNum);
        }

        private void MoveRedCar4(int columnNum, int rowNum)
        {

            PlayBoard.Children.Remove(RedCar4);
            //await System.Threading.Tasks.Task.Delay(10);
            PlayBoard.Children.Add(RedCar4);
            Grid.SetRow(RedCar4, rowNum);
            Grid.SetColumn(RedCar4, columnNum);
        }

        private void RollDice_Click(object sender, RoutedEventArgs e)
        {
            //await MoveRedCar1();
            //await MoveRedCar1(1,5);
            //await MoveCRedCar1(2,5);

        }


        private async void RedCar1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int dice = 3;
            int movNum = 0;
            while (movNum < dice)
            {
                int columnNum = Convert.ToInt32(RedCarsRoad[positionRedCar1].Substring(0, 2));
                int rowNum = Convert.ToInt32(RedCarsRoad[positionRedCar1].Substring(2, 2));
                positionRedCar1++;
                MoveCar(RedCar1, columnNum, rowNum);
                movNum++;
                await System.Threading.Tasks.Task.Delay(200);
            }
        }

        private async void RedCar2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int dice = 3;
            int movNum = 0;
            while (movNum < dice)
            {
                int columnNum = Convert.ToInt32(RedCarsRoad[positionRedCar2].Substring(0, 2));
                int rowNum = Convert.ToInt32(RedCarsRoad[positionRedCar2].Substring(2, 2));
                positionRedCar2++;
                MoveCar(RedCar2, columnNum, rowNum);
                movNum++;
                await System.Threading.Tasks.Task.Delay(200);
            }
        }

        private async void RedCar3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int dice = 3;
            int movNum = 0;
            while (movNum < dice)
            {
                int columnNum = Convert.ToInt32(RedCarsRoad[positionRedCar3].Substring(0, 2));
                int rowNum = Convert.ToInt32(RedCarsRoad[positionRedCar3].Substring(2, 2));
                positionRedCar3++;
                MoveCar(RedCar3, columnNum, rowNum);
                movNum++;
                await System.Threading.Tasks.Task.Delay(200);
            }
        }

        private async void RedCar4_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int dice = 3;
            int movNum = 0;
            while (movNum < dice)
            {
                int columnNum = Convert.ToInt32(RedCarsRoad[positionRedCar4].Substring(0, 2));
                int rowNum = Convert.ToInt32(RedCarsRoad[positionRedCar4].Substring(2, 2));
                positionRedCar4++;
                MoveCar(RedCar4, columnNum, rowNum);
                movNum++;
                await System.Threading.Tasks.Task.Delay(200);
            }
        }

        private async void Red1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Red1.Visibility = Visibility.Collapsed;
            int dice = 3;
            int movNum = 0;

            positionRedCar1++;
            MoveCar(RedCar1, 0, 3);
            movNum++;
            RedCar1.Visibility = Visibility.Visible;
            await System.Threading.Tasks.Task.Delay(200);

            while (movNum < dice)
            {
                int columnNum = Convert.ToInt32(RedCarsRoad[positionRedCar1].Substring(0, 2));
                int rowNum = Convert.ToInt32(RedCarsRoad[positionRedCar1].Substring(2, 2));
                positionRedCar1++;
                MoveCar(RedCar1, columnNum, rowNum);
                movNum++;
                await System.Threading.Tasks.Task.Delay(200);
            }

        }

        private void Red2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Red2.Visibility = Visibility.Collapsed;
       
        }

        private void Red3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Red3.Visibility = Visibility.Collapsed;
   
        }

        private void Red4_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Red4.Visibility = Visibility.Collapsed;
 
        }



    }
}
