using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace _0va_Snake_Game
{
    class Input
    {
        private static Hashtable keyTable = new Hashtable();
        //Used to optimize the keys inserted inside the class.

        public static bool KeyPress(Keys key)
        {
            if(keyTable[key] == null)
            {
                return false;
            }
            else
            {
                return (bool)keyTable[key];
            }
        }

        public static void ChangeState(Keys key, bool state)
        {
            // Change the state of the key and the snake.
            keyTable[key] = state;
        }

    }
}
