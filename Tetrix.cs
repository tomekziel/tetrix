using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetrix
{
    class Tetrix
    {

        readonly static int wellWidth = 10;
        readonly static int wellHeight = 20;

        int[,] well = new int[wellHeight, wellWidth];

        static void Main(string[] args)
        {
            new Tetrix().MainLoop();
        }

        public void MainLoop()
        {
            Init();
            Console.WriteLine("Tetrix!");
            DrawScreen();
            Console.ReadLine();

        }

        private void Init()
        {
            well[0, 1] = well[0, 2] = well[0, 3] = well[0, 4] = well[1, 4] = well[1, 5] = 1;
        }

        public void DrawScreen()
        {
            for (int y = 0; y < wellHeight; y++)
            {
                Console.SetCursorPosition(10, 22 - y);
                Console.Write("##");
                for (int x=0; x<wellWidth; x++)
                {
                    Console.Write(well[y, x] == 0 ? "  " : "[]");
                }
                Console.Write("## "+y);
            }
            Console.SetCursorPosition(10, 23);
            Console.Write("########################");


        }


    }
}


