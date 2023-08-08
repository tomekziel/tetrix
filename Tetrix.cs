using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tetrix
{
    class Tetrix
    {

        readonly static int wellWidth = 10;
        readonly static int wellHeight = 20;

        int[,] well = new int[wellHeight, wellWidth];
        int score = 0;

        readonly static List<int [,,]> pieces = new List<int[,,]> {
            new int[,,] {
                {
                    {0,0,0,0},
                    {1,1,1,1},
                    {0,0,0,0},
                    {0,0,0,0}
                },{
                    {0,0,1,0},
                    {0,0,1,0},
                    {0,0,1,0},
                    {0,0,1,0}
                },{
                    {0,0,0,0},
                    {0,0,0,0},
                    {1,1,1,1},
                    {0,0,0,0}
                },{
                    {0,1,0,0},
                    {0,1,0,0},
                    {0,1,0,0},
                    {0,1,0,0}
                }
            }
            ,
            new int[,,]{
                {
                    {1,0,0},
                    {1,1,1},
                    {0,0,0}
                },{
                    {0,1,1},
                    {0,1,0},
                    {0,1,0}
                },{
                    {0,0,0},
                    {1,1,1},
                    {0,0,1}
                },{
                    {0,1,0},
                    {0,1,0},
                    {1,1,0}
                },
            }
            ,
            new int[,,]{
                {
                    {0,0,1},
                    {1,1,1},
                    {0,0,0}
                },
                {
                    {0,1,0},
                    {0,1,0},
                    {0,1,1}
                },
                {
                    {0,0,0},
                    {1,1,1},
                    {1,0,0}
                },
                {
                    {1,1,0},
                    {0,1,0},
                    {0,1,0}
                }
            }
            ,
            new int[,,]{
                {
                    {1,1},
                    {1,1}
                }
            }
            ,
            new int[,,]{
                {
                    {0,1,1},
                    {1,1,0},
                    {0,0,0}
                },{
                    {0,1,0},
                    {0,1,1},
                    {0,0,1}
                },{
                    {0,0,0},
                    {0,1,1},
                    {1,1,0}
                },{
                    {1,0,0},
                    {1,1,0},
                    {0,1,0}
                },
            }
            ,
            new int[,,]{
                {
                    {0,1,0},
                    {1,1,1},
                    {0,0,0}
                },
                {
                    {0,1,0},
                    {0,1,1},
                    {0,1,0}
                },
                {
                    {0,0,0},
                    {1,1,1},
                    {0,1,0}
                },
                {
                    {0,1,0},
                    {1,1,0},
                    {0,1,0}
                },
            }
            ,
            new int[,,]{
                {
                    {1,1,0},
                    {0,1,1},
                    {0,0,0}
                },
                {
                    {0,1,0},
                    {0,1,1},
                    {0,0,1}
                },
                {
                    {0,0,0},
                    {1,1,0},
                    {0,1,1}
                },
                {
                    {1,0,0},
                    {1,1,0},
                    {0,1,0}
                }
            }
            

        };

        int pieceNo = 0;
        int pieceRotate = 1;
        int pieceX = 3;
        int pieceY = 18;

        static void Main(string[] args)
        {
            new Tetrix().MainLoop();
        }

        public void MainLoop()
        {
            Console.WriteLine("Tetrix!");
            Console.ReadLine();
            Init();
            while (true) { 
                var key = Console.ReadKey();
                keyPressedRecently = true;
                HandleKey(key.Key);
            }
        }

        private void DrawEverything()
        {
            DrawScreen();
            DrawShadow();
            DrawPiece("()", pieceY);
        }

        private void DrawShadow()
        {
            var y = 0;
            while (false == CollisionDetected(pieceRotate, pieceX, pieceY - y))
            {
                y++;
            }
            y--;
            
            DrawPiece("..", pieceY - y);
        }

        bool keyPressedRecently = false;
        object syncObject = new object();
        private void HandleKey(ConsoleKey key)
        {
            lock (syncObject)
            {

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        if (false == CollisionDetected(pieceRotate, pieceX - 1, pieceY))
                        {
                            pieceX--;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (false == CollisionDetected(pieceRotate, pieceX + 1, pieceY))
                        {
                            pieceX++;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (false == CollisionDetected(pieceRotate, pieceX, pieceY - 1))
                        {
                            pieceY--;
                        }
                        else
                        {
                            FreezePiece();
                            CompactWell();

                        }

                        break;
                    case ConsoleKey.UpArrow:
                        if (false == CollisionDetected((pieceRotate + 1) % pieces[pieceNo].GetLength(0), pieceX, pieceY))
                        {
                            pieceRotate = (pieceRotate + 1) % pieces[pieceNo].GetLength(0);
                        }

                        break;
                    case ConsoleKey.Spacebar:
                        pieceRotate = 0;
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
                DrawEverything();
            }
        }

        private void CompactWell()
        {

            int lines = 0;

            var y = 0;
            while (y < wellHeight) 
            {
                var allFilled = true;
                for(var x=0; x<wellWidth; x++)
                {
                    if (well[y,x] == 0)
                    {
                        allFilled = false;
                        break;
                    }
                }
                if (false == allFilled){
                    y++;
                    continue;
                }

                lines++;

                for (var y2=y; y2<wellHeight-2; y2++)
                {
                    for (var x = 0; x < wellWidth; x++)
                    {
                        well[y2, x] = well[y2 + 1, x];
                    }
                }
                for (var x = 0; x < wellWidth; x++)
                {
                    well[wellHeight-1, x] = 0;
                }


            }

            switch (lines)
            {
                case 4:
                    score += 1200;
                    break;
                case 3:
                    score += 300;
                    break;
                case 2:
                    score += 100;
                    break;
                case 1:
                    score += 40;
                    break;
            }
        }

        private void FreezePiece()
        {
            var currentPiece = pieces[pieceNo];
            var dim = currentPiece.GetLength(1); // always square

            for (var y = dim - 1; y >= 0; y--)
            {
                for (var x = 0; x < dim; x++)
                {
                    if (currentPiece[pieceRotate, y, x] == 1 )
                    {
                        well[pieceY - y, pieceX + x] = 1;
                    }

                }
            }
            pieceX = 5;
            pieceY = 18;
        }

        bool CollisionDetected(int pieceNewRotation, int pieceNewX, int pieceNewY)
        {
            var currentPiece = pieces[pieceNo];
            var dim = currentPiece.GetLength(1); // always square

            Console.SetCursorPosition(60, 12);

            // left 
            for (var y = dim - 1; y >= 0; y--)
            {
                for (var x = 0; x < dim - 1; x++)
                {
                    if (currentPiece[pieceNewRotation, y, x] == 1 && pieceNewX + x < 0) 
                    {
                        Console.Write($"collision left newX={pieceNewX} x={x}");
                        return true;
                    }
                }
            }

            // right
            for (var y = dim - 1; y >= 0; y--)
            {
                for (var x = dim - 1; x >= 0; x--)
                {
                    if (currentPiece[pieceNewRotation, y, x] == 1 && pieceNewX + x >= wellWidth)
                    {
                        Console.Write($"collision right newX={pieceNewX} x={x}");
                        return true;
                    }
                }
            }

            // well bottom (y = 0)
            for (var y = dim - 1; y >= 0; y--)
            {
                for (var x = 0; x < dim - 1; x++)
                {
                    if (currentPiece[pieceNewRotation, y, x] == 1 && pieceNewY - y < 0)
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
                    if (currentPiece[pieceNewRotation, y, x] == 1 && well[pieceNewY-y, pieceNewX+x]==1)
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
            for (var y = 0; y < 6; y++)
            {
                for (var x = 0; x < 9; x++)
                {
                    well[y,x] = 1;  
                }
            }

            well[0, 1] = well[0, 2] = well[0, 3] = well[0, 4] = well[1, 4] = well[1, 5] = 1;
            well[10, 8] = well[10, 9] = 1;
            well[12, 0] = well[12, 1] = 1;

            Timer timer = new Timer(Tick, null, 0, 1000);

        }

        private void Tick(object state)
        {
            if (false == keyPressedRecently)
            {
                HandleKey(ConsoleKey.DownArrow);
            }
            keyPressedRecently = false;
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

            Console.SetCursorPosition(50, 5);
            Console.Write("SCORE "+score);

        }

        public void DrawPiece(string mark, int tempPieceY)
        {
            var currentPiece = pieces[pieceNo];
            var dim = currentPiece.GetLength(1); // always square
            for (int y = 0; y < dim; y++)
            {
                for (int x = 0; x < dim; x++)
                {
                    int drawX = wellMariginX + pieceX*2 + (x+1)*2;
                    int drawY = wellMariginY - tempPieceY + y;
                    Console.SetCursorPosition(drawX, drawY);
                    Console.Write(currentPiece[pieceRotate, y, x] == 0 ? "" : mark);
                }

            }

            //Console.SetCursorPosition(60, 10);
            //Console.Write($"piece x1={pieceX} y1={pieceY} x2={pieceX+dim-1} y2={pieceY-dim+1} ");
        }


    }
}


