using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fia_med_krock.GameLogic;
using Windows.UI.Xaml.Controls;

namespace Fia_med_krock.GameLogic
{
    public class Player
    {
        public string Color { get; set; }
        public List<Cars> Cars { get; set; }


        public Player(string color, List<Windows.UI.Xaml.Shapes.Rectangle> carUIs, Grid playBoard)
        {
            Color = color;
            Cars = new List<Cars>
            {
                new Cars(color, -1, playBoard, 1) { CarUI = carUIs[0] },
                new Cars(color, -1, playBoard, 2) { CarUI = carUIs[1] },
                new Cars(color, -1, playBoard, 3) { CarUI = carUIs[2] },
                new Cars(color, -1, playBoard, 4) { CarUI = carUIs[3] }
            };
        }
    }
}
