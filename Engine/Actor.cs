using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Actor
    {
        int posX;
        int posY;

        int sizeX;
        int sizeY;

        public Actor(int _posX, int _posY, int _sizeX, int _sizeY)
        {
            posX = _posX;
            posY = _posY;

            sizeX = _sizeX;
            sizeY = _sizeY;
        }

        public void Move(int x, int y)
        {
            posX += x;
            posY += y;
        }
    }
}
