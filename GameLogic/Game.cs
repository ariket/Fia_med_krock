using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fia_med_krock.GameLogic
{
    /// <summary>
    /// Game class
    /// players is a list of all Players that belong to a specific game
    /// </summary>
    public class Game
    {
        private List<Player> players;

        //Constructor
        public Game(List<Player> players)
        {
            this.players = players;
        }

        public void Start()
        {
            // Implement game start logic
        }

        public void PlayTurn()
        {
            // Implement turn logic
        }
    }
}
