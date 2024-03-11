using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fia_med_krock.GameLogic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Fia_med_krock.GameLogic
{
    /// <summary>
    /// Player class
    /// Color is used to specify the Palyers color
    /// Cars is a list of all car that belong to a specific player
    /// IsAi are true if specific player are an AI player
    /// </summary>
    public class Player
    {
        public string Color { get; set; }
        public List<Cars> Cars { get; set; }
        public bool IsAi { get; set; }
        
        //Constructor
        public Player(string color, List<Windows.UI.Xaml.Shapes.Rectangle> carUIs, Grid playBoard, bool isAi = false)
        {
            Color = color;
            IsAi = isAi;

            Cars = new List<Cars>
            {
                new Cars(color, -1, playBoard, 1) { CarUI = carUIs[0] },
                new Cars(color, -1, playBoard, 2) { CarUI = carUIs[1] },
                new Cars(color, -1, playBoard, 3) { CarUI = carUIs[2] },
                new Cars(color, -1, playBoard, 4) { CarUI = carUIs[3] }
            };
        }

        public async Task PlayTurnAsync()
        {
            if (IsAi)
            {
                await Task.Delay(2000);

            }
        }

        public bool CheckIfWinner()
        {
            bool winner = false;
            //bool winner = true;

            if (Cars[0].steps == 37)
            if (Cars[0].steps == 37 && Cars[1].steps == 37 && Cars[2].steps == 37 && Cars[3].steps == 37)
            {
                winner = true;
            }         
            return winner;
        }

    }
}
