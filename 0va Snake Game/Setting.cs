using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0va_Snake_Game
{
    public enum Directions { Left, Right, Up, Down };

    class Setting
    {
        public static int Width;
        public static int Height;
        public static int Speed;
        public static int Score;
        public static int Points;
        public static bool GameOver;
        public static Directions directions;

        public Setting()
        {
            // Set the default setting function

            Width = 16;
            Height = 16;
            Speed = 15;
            Score = 0;
            Points = 100;
            GameOver = false;
            directions = Directions.Down;
        }
    }
}
