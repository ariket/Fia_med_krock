using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Fia_med_krock
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartPage : Page
    {
        public StartPage()
        {
            this.InitializeComponent();
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
        private void RulesButton_Click(Object sender, RoutedEventArgs e)
        {
            RulesPopup.IsOpen= true;
        }
        private void CloseRulesButton_Click(object sender, RoutedEventArgs e)
        {
            RulesPopup.IsOpen = false;
        }
        private void PlayerText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is TextBlock textBlock)
            {
                TextBox textBox = new TextBox
                {
                    Text = textBlock.Text,
                    FontSize = textBlock.FontSize,
                    Foreground = textBlock.Foreground,
                    HorizontalAlignment = textBlock.HorizontalAlignment,
                    VerticalAlignment = textBlock.VerticalAlignment,
                    Margin = textBlock.Margin,
                    Width = textBlock.ActualWidth,
                    Height = textBlock.ActualHeight,
                    BorderThickness = new Thickness(0)
                    
                };
                textBox.Tapped += PlayerText_Tapped;
                StackPanel parentStackPanel = textBlock.Parent as StackPanel;
                int index = parentStackPanel.Children.IndexOf(textBlock);
                parentStackPanel.Children.RemoveAt(index);
                parentStackPanel.Children.Insert(index, textBox);
                textBox.Focus(FocusState.Programmatic);
            }
        }
    }
}
