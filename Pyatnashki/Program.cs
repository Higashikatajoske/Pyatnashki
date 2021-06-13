using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyatnashki
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            bool exit = true;
            bool endOfGame;
            int size = 0;
            bool newRecord = false;
            while (exit)
            {
                menu.CreateMenu();
                switch (menu.line)
                {
                    case 0:
                        endOfGame = true;
                        size = menu.CreateSizeChoiceWindow();
                        menu.DrawingStartOfGame();
                        Field field = new Field();
                        field.MakeBlocks(size);
                        while (endOfGame)
                        {
                            if (Console.KeyAvailable)
                            {
                                ConsoleKeyInfo key;
                                key = Console.ReadKey();
                                if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.DownArrow
                                    || key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.UpArrow
                                    || key.Key == ConsoleKey.A || key.Key == ConsoleKey.S || key.Key == ConsoleKey.D
                                    || key.Key == ConsoleKey.W)
                                {
                                    field.KeyboardMove(key.Key);
                                }
                                else if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar)
                                {
                                    field.PushBlock();
                                }
                                else if (key.Key == ConsoleKey.P)
                                    menu.MakePause();

                                if (field.GameOverCondition())
                                {
                                    menu.GameOver(field.steps);
                                    if (menu.record.CheckRecords(menu.finalScores))
                                    {
                                        menu.ShowWriteNewRecordWindow();
                                        endOfGame = false;
                                        newRecord = true;
                                    }
                                }
                            }
                            menu.Secund();
                        }
                        break;

                    case 1:
                        menu.CreateIntroductionWindow();
                        break;

                    case 2:
                        menu.CreateRecordWindow();
                        break;

                    case 3:
                        exit = false;
                        if (newRecord)
                            menu.record.WriteInFile();
                        break;
                }
            

            }
        }
    }
}
