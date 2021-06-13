using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Pyatnashki
{
    class Block
    {
        public int xLeft;
        public int xRight;
        public int yUp;
        public int yDown;
        public int xMiddle;
        public int yMiddle;
        private Lines horizontalLine;
        private Lines horizontalLine2;
        private Lines verticalLine;
        private Lines verticalLine2;
        private string strnumber;
        public int size;

        public Block (int sizeOfBox)
        {
            size = sizeOfBox;
        }

        public void CreateBlock(int x0, int y0, int number)
        {
            xLeft = x0;
            yUp = y0;
            xRight = x0 + 7;
            yDown = y0 + 4;
            yMiddle = (yUp + yDown) / 2;
            xMiddle = (xLeft + xRight) / 2;
            horizontalLine = new Lines();
            horizontalLine.CreateHorizontalLine(xLeft + 1, xRight - 1, yUp, '*');
            horizontalLine.DrawPoints();
            verticalLine = new Lines();
            verticalLine.CreateVerticalLine(xLeft, yUp, yDown, '*');
            verticalLine.DrawPoints();
            horizontalLine2 = new Lines();
            horizontalLine2.CreateHorizontalLine(xLeft + 1, xRight - 1, yDown, '*');
            horizontalLine2.DrawPoints();
            verticalLine2 = new Lines();
            verticalLine2.CreateVerticalLine(xRight, yUp, yDown, '*');
            verticalLine2.DrawPoints();
            for (int i = xLeft + 1; i < xRight; i++)
            {
                for (int j = yUp + 1; j < yDown; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(' ');
                }
            }
            Console.SetCursorPosition(xMiddle, yMiddle);
            if (number < 10)
            {
                strnumber = "0" + Convert.ToString(number);
                Console.WriteLine(strnumber);
            }
            else
                Console.Write(number);
            Console.SetCursorPosition(size * 8 + 15, size * 5 - 1);
        }

        public void BlockLight(int x0, int y0, int number)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            CreateBlock(x0, y0, number);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void EmptyBlock(int x0, int y0)
        {
            xLeft = x0;
            yUp = y0;
            xRight = x0 + 7;
            yDown = y0 + 4;
            for (int i = xLeft ; i <= xRight; i++)
            {
                for (int j = yUp ; j <= yDown; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(' ');
                }
            }
            Console.SetCursorPosition(size * 8 + 15, size * 5 - 1);
        }

        public void LightEmptyBlock(int x0, int y0)
        {
            Console.BackgroundColor = ConsoleColor.White;
            EmptyBlock(x0, y0);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(x0, y0);
        }

        public void BlockMovingX(int x, int y, Direction direction, int number)
        {
            switch (direction)
            {
                case Direction.Right:
                    for (int i = x; i < x + 8; i++)
                    {
                        EmptyBlock(i, y);
                        CreateBlock(i + 1, y, number);
                        Thread.Sleep(50);

                    }
                    break;

                case Direction.Left:
                    for (int i = x; i > x - 8; i--)
                    {
                        EmptyBlock(i, y);
                        CreateBlock(i - 1, y, number);
                        Thread.Sleep(50);
                    }
                    break;

                case Direction.Up:
                    for (int i = y; i > y - 5; i--)
                    {
                        EmptyBlock(x, i);
                        CreateBlock(x, i-1, number);
                        Thread.Sleep(80);
                    }
                    break;

                case Direction.Down:
                    for (int i = y; i < y + 5; i++)
                    {
                        EmptyBlock(x, i);
                        CreateBlock(x, i+1, number);
                        Thread.Sleep(80);
                    }
                    break;
            }
        }
    }
}
