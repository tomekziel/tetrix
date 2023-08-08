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

        int[,] arena = new int[wellHeight, wellWidth];

        static void Main(string[] args)
        {
            new Tetrix().MainLoop();
        }

        public void MainLoop()
        {
            Console.WriteLine("Tetrix!");
            DrawScreen();
            Console.ReadLine();

        }

        public void DrawScreen()
        {
            for (int y = 0; y < wellHeight; y++)
            {
                Console.SetCursorPosition(10, 22 - y);
                Console.Write("##                    ## "+y);
            }
            Console.SetCursorPosition(10, 23);
            Console.Write("########################");


        }


    }
}


