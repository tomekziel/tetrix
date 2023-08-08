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
                    {1,1},
                    {1,1}
                }
            ,
            new int[,]
                {
                    {0,1,1},
                    {1,1,0},
                    {0,0,0}
                }
            ,
            new int[,]
                {
                    {0,1,0},
                    {1,1,1},
                    {0,0,0}
                }
            ,
            new int[,]
                {
                    {1,1,0},
                    {0,1,1},
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

        int pieceNo = 2;
        int pieceX = 5;
        int pieceY = 15;

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
                var key = Console.ReadKey();
                HandleKey(key);
            }
        }

        private void HandleKey(ConsoleKeyInfo key)
        {
            switch (key.Key) {
                case ConsoleKey.LeftArrow:
                    if (false == CollisionDetected(pieceX - 1, pieceY)) {
                        pieceX--;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (false == CollisionDetected(pieceX + 1, pieceY))
                    {
                        pieceX++;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (false == CollisionDetected(pieceX, pieceY - 1))
                    {
                        pieceY--;
                    }

                    break;
                case ConsoleKey.UpArrow:
                    if (false == CollisionDetected(pieceX, pieceY + 1))
                    {
                        pieceY++;
                    }
                    
                    break;
                case ConsoleKey.Spacebar:
                    pieceNo++;
                    if (pieceNo == pieces.Count)
                    {
                        pieceNo = 0;
                    }
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;

            }
        }

        bool CollisionDetected(int pieceNewX, int pieceNewY)
        {
            var currentPiece = pieces[pieceNo];
            var dim = currentPiece.GetLength(0); // always square

            Console.SetCursorPosition(60, 12);

            // left and right
            for (var y = dim - 1; y >= 0; y--)
            {
                for (var x = 0; x < dim - 1; x++)
                {
                    if (
                        (currentPiece[y, x] == 1 && pieceNewX + x < 0)||
                        (currentPiece[y, x] == 1 && pieceNewX + x >= wellWidth-1))
                    {
                        Console.Write($"collision newX={pieceNewX} x={y}");
                        return true;
                    }
                }
            }

            // well bottom (y = 0)
            for (var y = dim - 1; y >= 0; y--)
            {
                for (var x = 0; x < dim - 1; x++)
                {
                    if (currentPiece[y, x] == 1 && pieceNewY - y < 0)
                    {
                        Console.Write($"collision newY={pieceNewX} y={y}");
                        return true;
                    }

                }
            }

            // other elements
            for (var y = dim - 1; y >= 0; y--)
            {
                for (var x = 0; x < dim; x++)
                {
                    if (currentPiece[y, x] == 1 && well[pieceNewY-y, pieceNewX+x]==1)
                    {
                        Console.Write($"collision newX={pieceNewX} newY={pieceNewY} x={x} y={y}");
                        return true;
                    }

                }
            }

            Console.Write("                                     ");
            return false;


        }

        private void Init()
        {
            well[0, 1] = well[0, 2] = well[0, 3] = well[0, 4] = well[1, 4] = well[1, 5] = 1;

            well[10, 8] = well[10, 9] = 1;
            well[12, 0] = well[12, 1] = 1;

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
                    int drawX = wellMariginX + pieceX*2 + (x+1)*2;
                    int drawY = wellMariginY - pieceY + y;
                    Console.SetCursorPosition(drawX, drawY);
                    Console.Write(currentPiece[y, x] == 0 ? "" : "()");
                }

            }
            Console.SetCursorPosition(60, 10);
            Console.Write($"piece x1={pieceX} y1={pieceY} x2={pieceX+dim-1} y2={pieceY-dim+1} ");
        }


    }
}


