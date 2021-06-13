using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyatnashki
{
    class Field
    {
        public int steps = 0;
        public int[,] placeField ;
        public bool[,] boolField ;
        public int[] randomMas;
        private Block block;
        private Menu menu = new Menu();
        private int currentBlockNumber;
        public int curX = 0;
        public int curY = 0;
        public bool randomCheck = false;
        int random;
        private int sizeOfBox = 0;

        public void MakeBlocks(int size, int x = 1, int y = 1, bool rightVersion = false)
        {
            sizeOfBox = size;
            menu.sizeOfBox = size;
            block = new Block(size);
            Random blockRandom = new Random();
            placeField = new int[size, size];
            boolField = new bool[size, size];
            randomMas = new int[size*size - 1];
            int randomMasLength = 0;
            for (int i = 0; i < randomMas.Length; i++)
            {
                while (randomCheck == false)
                {
                    random = blockRandom.Next(1, size*size);
                    randomCheck = true;
                    for (int k = 0; k < randomMasLength; k++)
                    {
                        if (randomMas[k] == random)
                            randomCheck = false;
                    }
                }
                randomMas[randomMasLength] = random;
                randomMasLength++;
                randomCheck = false;
            }

                    for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    if (i == size - 1 && j == size - 1)
                    {
                        block.EmptyBlock(x + j * 8, y + i * 5);
                    }
                    else
                    {
                        if (rightVersion)
                            placeField[i, j] = size * i + (j + 1);
                        else
                           //placeField[i, j] = size * i + (j + 1);
                        
                       placeField[i, j] = randomMas[size * i + j];
                        block.CreateBlock(x + j * 8, y + i * 5, placeField[i, j]);
                    }
                }
            boolField[size - 1, size - 1] = true;
            currentBlockNumber = 1;
            block.BlockLight(x, y, placeField[0, 0]);
        }

        public void KeyboardMove(ConsoleKey key, int x0 = 1 , int y0 = 1)
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i < sizeOfBox; i++)
                for (int j = 0; j < sizeOfBox; j++)
                {
                    if (currentBlockNumber == sizeOfBox * i + (j + 1))
                    {
                        x = i;
                        y = j;
                    }
                }
            if (boolField[curX, curY] == true)
                block.EmptyBlock(x0 + y * 8, y0 + x * 5);
            else
                block.CreateBlock(x0 + y * 8, y0 + x * 5, placeField[x,y]);

            if (key == ConsoleKey.RightArrow || key == ConsoleKey.D)
            {
                currentBlockNumber++;
                if (currentBlockNumber % sizeOfBox == 1)
                    currentBlockNumber -= sizeOfBox;

            }
            else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.A)
            {
                currentBlockNumber--;
                if (currentBlockNumber % sizeOfBox == 0)
                    currentBlockNumber += sizeOfBox;
            }
            else if (key == ConsoleKey.UpArrow || key == ConsoleKey.W)
            {
                currentBlockNumber -= sizeOfBox;
                if (currentBlockNumber <= 0)
                    currentBlockNumber += sizeOfBox * sizeOfBox;
            }
            
            else if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
            {
                currentBlockNumber += sizeOfBox;
                if (currentBlockNumber >= sizeOfBox * sizeOfBox + 1)
                    currentBlockNumber -= sizeOfBox * sizeOfBox;
            }

            for (int i = 0; i < sizeOfBox; i++)
                for (int j = 0; j < sizeOfBox; j++)
                {
                    if (currentBlockNumber == sizeOfBox * i + (j + 1))
                    {
                        x = i;
                        y = j;
                        if (boolField[x, y] == true)
                            block.LightEmptyBlock(x0 + y * 8, y0 + x * 5);
                        else
                        block.BlockLight(x0 + y * 8, y0 + x * 5, placeField[x,y]);
                    }
                }
            curX = x;
            curY = y;

        }

        public void PushBlock(int x = 1, int y = 1)
        {
            int xDown;
            int xUp;
            int yRight;
            int yLeft;
            int boolX = 0;
            int boolY = 0;
            for (int i = 0; i < sizeOfBox; i++)
                for (int j = 0; j < sizeOfBox; j++)
                    if (boolField[i, j] == true)
                    {
                        boolX = i;
                        boolY = j;
                    }
            if (boolX != curX || boolY != curY)
            {
                xDown = curX + 1;
                xUp = curX - 1;
                yRight = curY + 1;
                yLeft = curY - 1;

                if (yLeft == boolY && curX == boolX)
                {
                    block.BlockMovingX(x + curY * 8, y + curX * 5, Direction.Left, placeField[curX, curY]);
                    SwapData(boolX, boolY, x, y);
                }
                else if (yRight == boolY && curX == boolX)
                {
                    block.BlockMovingX(x + curY * 8, y + curX * 5, Direction.Right, placeField[curX, curY]);
                    SwapData(boolX, boolY, x, y);
                }
                else if (xDown == boolX && curY == boolY)
                {
                    block.BlockMovingX(x + curY * 8, y + curX * 5, Direction.Down, placeField[curX, curY]);
                    SwapData(boolX, boolY, x, y);
                }
                else if (xUp == boolX && curY == boolY)
                {
                    block.BlockMovingX(x + curY * 8, y + curX * 5, Direction.Up, placeField[curX, curY]);
                    SwapData(boolX, boolY, x, y);
                }
            }
            steps++;
            menu.ChangeSteps(steps);
            Console.SetCursorPosition(sizeOfBox * 8 + 15, sizeOfBox * 5 - 1);
        }

        public bool GameOverCondition()
        {
            bool win = true;

            for (int i = 0; i < sizeOfBox; i++)
                for (int j = 0; j < sizeOfBox; j++)
                {
                    if (placeField[i, j] != i * sizeOfBox + (j + 1) && (i != sizeOfBox - 1 || j != sizeOfBox - 1))
                        win = false;
                }
            return win;
        }

        public void SwapData(int boolX, int boolY, int x = 1, int y = 1)
        {
            block.LightEmptyBlock(x + curY * 8, y + curX * 5);
            boolField[curX, curY] = true;
            boolField[boolX, boolY] = false;
            placeField[boolX, boolY] = placeField[curX, curY];
            placeField[curX, curY] = 0;
        }


    }
}
