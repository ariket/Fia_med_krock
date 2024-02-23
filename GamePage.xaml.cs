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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409




namespace Fia_med_krock
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page

    {
        //RedCarsRoad är en array som redovisar vilken väg dom röda bilarna ska köra, bara dom första 7 positionerna finns än så länge.
        //Sen har man en int (RedCar1Position)för varje bil som anger bilen position mha RedCarsRoad[] 
        //Road för red cars, {column,row}
        public static string[] RedCarsRoad = {"0003","0103","0203","0303","0302","0301","0300" };
        public int RedCar1Position = 6;


        public MainPage()
        {
            this.InitializeComponent();
        }

        private async Task MoveCarRedCar1()
        {
            int ColumnNum = Convert.ToInt32(RedCarsRoad[RedCar1Position].Substring(0, 2));
            int rowNum = Convert.ToInt32(RedCarsRoad[RedCar1Position].Substring(2, 2));
            PlayBoard.Children.Remove(RedCar1);
            await System.Threading.Tasks.Task.Delay(500);
            PlayBoard.Children.Add(RedCar1);
            Grid.SetRow(RedCar1, rowNum);
            Grid.SetColumn(RedCar1, ColumnNum);
        }

        private async void RollDice_Click(object sender, RoutedEventArgs e)
        {
            await MoveCarRedCar1();
            //await MoveCarRedCar1(1,5);
            //await MoveCarRedCar1(2,5);

        }
    }
}
