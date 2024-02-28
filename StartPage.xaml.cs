using System;
using System.Collections.Generic;
using System.Diagnostics;
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
       

        private PlayerAiStates playerAiStates = new PlayerAiStates();

        public class MainPageParameters
        {
            public List<string> PlayerNames { get; set; }
            public PlayerAiStates PlayerAiStates { get; set; }
        }
        public StartPage()
        {
            this.InitializeComponent();
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> playerNames = GetPlayerNames();
            Frame.Navigate(typeof(MainPage), new MainPageParameters
            {
                PlayerNames = playerNames,
                PlayerAiStates = playerAiStates
            });
        }
        private List<string> GetPlayerNames()
        {
            List<string> playerNames = new List<string>();

            playerNames.Add(Player1Text.Text);
            playerNames.Add(Player2Text.Text);
            playerNames.Add(Player3Text.Text);
            playerNames.Add(Player4Text.Text);

            foreach (var name in playerNames)
            {
                Debug.WriteLine($"Player Name: {name}");
            }

            return playerNames;
        }
        private void RulesButton_Click(Object sender, RoutedEventArgs e)
        {
            RulesDialog.ShowAsync();
        }
        private void CloseRulesButton_Click(object sender, RoutedEventArgs e)
        {
            RulesDialog.Hide();
        }
        public class PlayerAiStates
        {
            public bool IsPlayer1Ai { get; set; }
            public bool IsPlayer2Ai { get; set; }
            public bool IsPlayer3Ai { get; set; }
            public bool IsPlayer4Ai { get; set; }
            
        }

        private void PlayerText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is TextBlock textBlock)
            {
                
                TextBox textBox = new TextBox
                {
                    Text = "",
                    FontSize = 12,//textBlock.FontSize,
                    Foreground = textBlock.Foreground,
                    HorizontalAlignment = textBlock.HorizontalAlignment,
                    VerticalAlignment = textBlock.VerticalAlignment,
                    Margin = textBlock.Margin,
                    Width = textBlock.ActualWidth,
                    Height = textBlock.ActualHeight,
                    BorderThickness = new Thickness(0)
                };
                StackPanel parentStackPanel = textBlock.Parent as StackPanel;
                int index = parentStackPanel.Children.IndexOf(textBlock);

                object originalFontSize = textBlock.FontSize;
                object originalForeground = textBlock.Foreground;

                textBox.LostFocus += (s, args) =>
                {
                    parentStackPanel.Children.RemoveAt(index);
                    textBlock.Text = textBox.Text;
                    parentStackPanel.Children.Insert(index, textBlock);
                    textBlock.FontSize = (double)originalFontSize;
                    textBlock.Foreground = (Brush)originalForeground;
                };

                parentStackPanel.Children.RemoveAt(index);
                parentStackPanel.Children.Insert(index, textBox);
                textBox.Focus(FocusState.Programmatic);

            }

        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (sender is Slider slider)
            {
                string playerName = slider.Name.Replace("Slider", "");
                switch (playerName)
                {
                    case "Player1":
                        playerAiStates.IsPlayer1Ai = slider.Value == 1;
                        break;

                    case "Player2":
                        playerAiStates.IsPlayer2Ai = slider.Value == 1;
                        break;

                    case "Player3":
                        playerAiStates.IsPlayer3Ai = slider.Value == 1;
                        break;

                    case "Player4":
                        playerAiStates.IsPlayer4Ai = slider.Value == 1;
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
