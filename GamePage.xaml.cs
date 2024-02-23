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
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async Task MoveCar(int romNum, int ColumnNum)
        {
            PlayBoard.Children.Remove(RedCar1);
            await System.Threading.Tasks.Task.Delay(500);
            PlayBoard.Children.Add(RedCar1);
            Grid.SetRow(RedCar1, romNum);
            Grid.SetColumn(RedCar1, ColumnNum);
        }

        private async void RollDice_Click(object sender, RoutedEventArgs e)
        {
            await MoveCar(0,5);
            await MoveCar(1,5);
            await MoveCar(2,5);

        }
    }
}
