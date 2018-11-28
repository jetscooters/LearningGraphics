using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Engine
{
    public static class DataControl
    {
        public static Bitmap LoadBitmap(string filename)
        {
            Bitmap bmp = null;
            try
            {
                bmp = new Bitmap(filename);
                Console.WriteLine("Found " + filename);
            }
            catch { }

            return bmp;
        }
    }
}
