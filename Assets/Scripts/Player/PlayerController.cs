using UnityEngine;

namespace TEDinc.LinesRunner
{
    public class PlayerController
    {
        private readonly IPlayer player;

        public void Move()
        {
            
        }


        public PlayerController(IPlayer player)
        {
            this.player = player;
        }
    }
}