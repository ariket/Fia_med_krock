using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Fia_med_krock.MainPage;

namespace Fia_med_krock.GameLogic
{
    internal class MainAnimations
    {
        //FIXME: Gör så att funktionen inte är världens största if-else träd.
        //bounce får sitt värde från goForward variabeln i Animatecarasync funktionen.
        public static void PickAnimation(Windows.UI.Xaml.Shapes.Rectangle carToMove, int columnNum, int car_steps, GameState active_player, bool moveForward)
        {
            //Väljer olika animationer beroende på vilken spelare som kör.
            if (active_player == GameState.PlayerRed)
            {
                if (moveForward == false)
                {
                    if (car_steps > 30)
                        MoveHelper.AnimateCarLeft(carToMove, columnNum);
                    //Om man står på rutan bredvid mål och slår 6 så måste man ändra riktning igen.
                    //Animationen blir rätt skum, men den hamnar på rätt plats iallafall.
                    else
                    {
                        MoveHelper.AnimateCarDown(carToMove, columnNum);
                    }
                }
                else if (car_steps <= 3)
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
                if (moveForward == false)
                {
                    if (car_steps > 30)
                        MoveHelper.AnimateCarUp(carToMove, columnNum);
                    //Om man står på rutan bredvid mål och slår 6 så måste man ändra riktning igen.
                    else
                    {
                        MoveHelper.AnimateCarLeft(carToMove, columnNum);
                    }
                }
                else if (car_steps <= 3)
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
                if (moveForward == false)
                {
                    if (car_steps > 30)
                        MoveHelper.AnimateCarRight(carToMove, columnNum);
                    //Om man står på rutan bredvid mål och slår 6 så måste man ändra riktning igen.
                    else
                    {
                        MoveHelper.AnimateCarUp(carToMove, columnNum);
                    }
                }
                else if (car_steps <= 3)
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
                if (moveForward == false)
                {
                    if (car_steps > 30)
                        MoveHelper.AnimateCarDown(carToMove, columnNum);
                    //Om man står på rutan bredvid mål och slår 6 så måste man ändra riktning igen.
                    else
                    {
                        MoveHelper.AnimateCarRight(carToMove, columnNum);
                    }
                }
                else if (car_steps <= 3)
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
    }
}
