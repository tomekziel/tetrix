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

        readonly static List<int [,]> pieces = new List<int[,]> {
            new int[,] 
                {
                    {0,0,0,0},
                    {1,1,1,1},
                    {0,0,0,0},
                    {0,0,0,0}
                }
            ,
            new int[,]
                {
                    {1,0,0},
                    {1,1,1},
                    {0,0,0}
                }
            ,
            new int[,]
                {
                    {0,0,1},
                    {1,1,1},
                    {0,0,0}
                }

        };

        int pieceNo = 0;
        int pieceX = 5;
        int pieceY = 20;

        static void Main(string[] args)
        {
            new Tetrix().MainLoop();
        }

        public void MainLoop()
        {
            Init();
            Console.WriteLine("Tetrix!");
            while (true) { 
                DrawScreen();
                DrawPiece();
                Console.ReadKey();
                pieceNo++;
                if (pieceNo == pieces.Count)
                {
                    pieceNo = 0;
                }
            }

        }

        private void Init()
        {
            well[0, 1] = well[0, 2] = well[0, 3] = well[0, 4] = well[1, 4] = well[1, 5] = 1;
        }

        int wellMariginX = 10;
        int wellMariginY = 22;

        public void DrawScreen()
        {
            for (int y = 0; y < wellHeight; y++)
            {
                Console.SetCursorPosition(wellMariginX, wellMariginY - y);
                Console.Write("##");
                for (int x=0; x<wellWidth; x++)
                {
                    Console.Write(well[y, x] == 0 ? "  " : "[]");
                }
                Console.Write("## "+y);
            }
            Console.SetCursorPosition(wellMariginX, wellMariginY+1);
            Console.Write("########################");
        }

        public void DrawPiece()
        {
            var currentPiece = pieces[pieceNo];
            var dim = currentPiece.GetLength(0); // always square
            for (int y = 0; y < dim; y++)
            {
                for (int x = 0; x < dim; x++)
                {
                    int drawX = wellMariginX + pieceX*2 + x*2;
                    int drawY = wellMariginY - pieceY + y;
                    Console.SetCursorPosition(drawX, drawY);
                    Console.Write(currentPiece[y, x] == 0 ? "  " : "()");
                }

            }

        }


    }
}


