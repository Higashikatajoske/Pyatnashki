using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Pyatnashki
{
    class Menu
    {
        public int line = 0;
        public int timesec = 0;
        private int time = 0;
        private Lines horizontalLine;
        private Lines verticalLine;
        private Lines horizontalLine2;
        private Lines verticalLine2;
        private Lines verticalLine3;
        public Stopwatch stopWatch;
        public int sizeOfBox = 0;
        private string elapsedTime = "00:00";
        private int stepsScores;
        private int timeScores;
        public double finalScores = 0;
        private TimeSpan ts;
        public Record record = new Record();
        private bool fileIsNotReaden = true;
        Field field;

        public void CleanWindow(int Width, int Height, int WidthStart = 0, int HeightStart = 0) // очищение интерефейса
        {
            for (int i = WidthStart; i <= Width; i++)
                for (int j = HeightStart; j <= Height; j++)
                {
                    Point CleanPoint = new Point(i, j, ' ');
                    CleanPoint.Clear();
                }
        }

        public void SwitchDefaultColor(int number, string name, string name1, string name2, int Xname, int Xname1, int Xname2
    , int Y, string name3 = "", int Xname3 = 0)  // изменяет цвет кпонки из выделенного на обычный
        {
            switch (number)
            {
                case 0:
                    SetPosAndWriteLine(Xname, Y, name);
                    break;
                case 1:
                    SetPosAndWriteLine(Xname1, Y + 1, name1);
                    break;
                case 2:
                    SetPosAndWriteLine(Xname2, Y + 2, name2);
                    break;
                case 3:
                    SetPosAndWriteLine(Xname3, Y + 3, name3);
                    break;
            }
        }

        private void ChangeLineColor(int x, int y, string word) // изменяет цвет определённой кпонки с обычного на выделенный
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            SetPosAndWriteLine(x, y, word);
            Console.ResetColor();
        }

        public void SwitchChangeColor(int number, string name, string name1, string name2, int Xname, int Xname1, int Xname2
    , int Y, string name3 = "", int Xname3 = 0) // изменяет цвет кпонки из обычного на выделенный
        {
            switch (number)
            {
                case 0:
                    ChangeLineColor(Xname, Y, name);
                    break;
                case 1:
                    ChangeLineColor(Xname1, Y + 1, name1);
                    break;
                case 2:
                    ChangeLineColor(Xname2, Y + 2, name2);
                    break;
                case 3:
                    ChangeLineColor(Xname3, Y + 3, name3);
                    break;
            }
        }

        public int SwitchButton(int rows, int number, ConsoleKey Key) // измение положение кнопки при нажатии
        {
            if (Key == ConsoleKey.UpArrow)
            {
                number--;
                if (number < 0)
                    number = rows - 1;
            }
            else if (Key == ConsoleKey.DownArrow)
            {
                number++;
                if (number == rows)
                    number = 0;
            }
            return number;
        }

        public void CreateMenu()
        {
            if (fileIsNotReaden)
            {
                record.ReadFormFile();
                fileIsNotReaden = false;
            }
            bool enterPress = false;
            verticalLine = new Lines();
            verticalLine2 = new Lines();
            verticalLine3 = new Lines();
            horizontalLine = new Lines();
            horizontalLine2 = new Lines();
            Console.SetWindowSize(1, 1);
            Console.SetBufferSize(22, 16);
            Console.SetWindowSize(22, 16);
            CreateRectangular(0, 15, 0, 20, '|', '-');
            SetPosAndWriteLine(7, 4, "Пятнашки");
            SetPosAndWriteLine(8, 7, "Играть");
            SetPosAndWriteLine(6, 8, "Инструкция");
            SetPosAndWriteLine(7, 9, "Рекорды");
            SetPosAndWriteLine(8, 10, "Выход");
            SwitchChangeColor(line, "Играть", "Инструкция", "Рекорды", 8, 6, 7, 7, "Выход", 8);
            Console.SetCursorPosition(14, 1);
            while (enterPress == false)
            {
                if (Console.KeyAvailable)
                {
                    SwitchDefaultColor(line, "Играть", "Инструкция", "Рекорды", 8, 6, 7, 7, "Выход", 8);

                    ConsoleKeyInfo key;
                    key = Console.ReadKey();
                    line = SwitchButton(4, line, key.Key);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        enterPress = true;
                        break;
                    }
                    SwitchChangeColor(line, "Играть", "Инструкция", "Рекорды", 8, 6, 7, 7, "Выход", 8);
                }
            }
            CleanWindow(20, 15);
        }

        public int CreateSizeChoiceWindow()
        {
            int leveLine = 0;
            bool enterPress = false;
            Console.SetWindowSize(22, 16);
            CreateRectangular(0, 15, 0, 20, '|', '-');
            SetPosAndWriteLine(3, 4, "Выберите размер");
            SetPosAndWriteLine(9, 5, "поля");
            SetPosAndWriteLine(9, 7, "3x3");
            SetPosAndWriteLine(9, 8, "4x4");
            SetPosAndWriteLine(9, 9, "5x5");
            SwitchChangeColor(leveLine, "3x3", "4x4", "5x5", 9, 9, 9, 7);

            Console.SetCursorPosition(14, 1);
            while (enterPress == false)
            {
                if (Console.KeyAvailable)
                {
                    SwitchDefaultColor(leveLine, "3x3", "4x4", "5x5", 9, 9, 9, 7);
                    ConsoleKeyInfo key;
                    key = Console.ReadKey();
                    leveLine = SwitchButton(3, leveLine, key.Key);

                    if (key.Key == ConsoleKey.Enter)
                    {
                        enterPress = true;
                        break;
                    }
                    SwitchChangeColor(leveLine, "3x3", "4x4", "5x5", 9, 9, 9, 7);
                }
            }
            sizeOfBox = 3 + leveLine;
            return sizeOfBox;
        }

        public void DrawingStartOfGame()
        {
            stepsScores = 0;
            timeScores = 0;
            Console.SetWindowSize(1, 1);
            Console.SetBufferSize(sizeOfBox * 8 + 19, sizeOfBox * 5 + 3);
            Console.SetWindowSize(sizeOfBox * 8 + 19, sizeOfBox * 5 + 3);
            verticalLine.CreateVerticalLine(sizeOfBox * 8 + 1, 0, sizeOfBox * 5, '|');
            verticalLine.DrawPoints();
            verticalLine2.CreateVerticalLine(sizeOfBox * 8 + 17, 0, sizeOfBox * 5, '|');
            verticalLine2.DrawPoints();
            verticalLine3.CreateVerticalLine(0, 0, sizeOfBox * 5, '|');
            verticalLine3.DrawPoints();
            horizontalLine.CreateHorizontalLine(0, sizeOfBox * 8 + 17, 0, '-');
            horizontalLine.DrawPoints();
            horizontalLine2.CreateHorizontalLine(0, sizeOfBox * 8 + 17, sizeOfBox * 5 + 1, '-');
            horizontalLine2.DrawPoints();
            SetPosAndWriteLine(sizeOfBox * 8 + 5, 2, "Ходы");
            SetPosAndWriteLine(sizeOfBox * 8 + 12, 2, "0");
            SetPosAndWriteLine(sizeOfBox * 8 + 7, 5, "Время");
            SetPosAndWriteLine(sizeOfBox * 8 + 7, 6, "00:00");
            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        public void ChangeSteps(int score)
        {
            Console.SetCursorPosition(sizeOfBox * 8 + 12, 2);
            Console.WriteLine(score);
        }

        public void MakePause()
        {
            stopWatch.Stop();
            SetPosAndWriteLine(sizeOfBox * 8 + 15, sizeOfBox * 5 - 1, " ");
            SetPosAndWriteLine(sizeOfBox * 8 + 7, (sizeOfBox - 1) * 5, "Пауза");
            Console.ReadKey();
            SetPosAndWriteLine(sizeOfBox * 8 + 7, (sizeOfBox - 1) * 5, "      ");
            stopWatch.Start();
        }

        public void ShowWriteNewRecordWindow()
        {
            switch (sizeOfBox)
            {
                case 3:
                    CreateRecordWindow(2, 13, 2, 38);
                    break;

                case 4:
                    CreateRecordWindow(5, 16, 7, 43);
                    break;

                case 5:
                    CreateRecordWindow(8, 19, 11, 47);
                    break;
            }
        }

        public void CreateRecordWindow(int yUp, int yDown, int xLeft, int xRight)
        {
            string newNickName;
            CleanWindow(xRight + 1, yDown + 1, xLeft - 1, yUp - 1);
            CreateRectangular(yUp, yDown, xLeft, xRight, '|', '=');
            Console.BackgroundColor = ConsoleColor.White;
            CreateRectangular(yUp - 1, yDown + 1, xLeft - 1, xRight + 1, ' ', ' ');
            Console.ResetColor();
            SetPosAndWriteLine(xLeft + 6, yUp + 2, "Поздравляем вы установили");
            SetPosAndWriteLine(xLeft + 13, yUp + 3, "новый рекорд!");
            SetPosAndWriteLine(xLeft + 10, yUp + 5, "Введите ваше имя");
            SetPosAndWriteLine(xLeft + 5, yUp + 6,  "(  Имя должно быть меньше  )");
            SetPosAndWriteLine(xLeft + 5, yUp + 7,  "(     или равно 10 букв    )");
            Console.SetCursorPosition(xLeft + 14, yUp + 9);
            newNickName = Console.ReadLine();
            while (newNickName.Length > 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                SetPosAndWriteLine(xLeft + 7, yUp + 8, "В имени больше 10 букв!!!");
                Console.ResetColor();
                Console.ReadKey();
                SetPosAndWriteLine(xLeft + 4, yUp + 8, "                               ");
                SetPosAndWriteLine(xLeft + 4, yUp + 9, "                               ");
                Console.SetCursorPosition(xLeft + 14, yUp + 9);
                newNickName = Console.ReadLine();
            }
            record.CreateNewRecord(finalScores, newNickName);
            CleanWindow(Console.WindowWidth - 1, Console.WindowHeight - 1);
            
        }

        public void Secund()
        {
            ts = stopWatch.Elapsed;
            if (ts.Seconds != timesec)
            {
                elapsedTime = String.Format("{0:00}:{1:00}",
                ts.Minutes, ts.Seconds);
                Console.SetCursorPosition(sizeOfBox * 8 + 7, 6);
                Console.WriteLine(elapsedTime);
                timesec = ts.Seconds;
            }
            Console.SetCursorPosition(sizeOfBox * 8 + 15, sizeOfBox * 5 - 1);
        }

        public void GameOver(int steps)
        {
            time = ts.Minutes * 60 + ts.Seconds;
            stopWatch.Stop();
            CountScores(steps);

            switch (sizeOfBox)
            {
                case 3:
                    CreateGameOverWindow(6, 1, 34, 15, 15, 9, 2, 31, steps);
                    break;

                case 4:
                    CreateGameOverWindow(8, 3, 42, 19, 20, 13, 5, 37, steps);
                    break;

                case 5:
                    CreateGameOverWindow(12, 5, 46, 21, 24, 17, 7, 41, steps);
                    break;

            }
        }

        public void CreateGameOverWindow(int leftUpX, int leftUpY, int rightUpX, int leftDownY, int centerWord, int wordsX, int wordsY, int scoresX, int steps)
        {
            CreateRectangular(leftUpY, leftDownY, leftUpX, rightUpX, '|', '=');
            CleanWindow(rightUpX - 1, leftDownY - 1, leftUpX + 1, leftUpY + 1);
            Console.BackgroundColor = ConsoleColor.White;
            CreateRectangular(leftUpY - 1, leftDownY + 1, leftUpX - 1, rightUpX + 1, ' ', ' ');
            Console.BackgroundColor = ConsoleColor.Black;
            SetPosAndWriteLine(centerWord, wordsY, "Поздравляем!");
            SetPosAndWriteLine(centerWord - 6, wordsY + 1, "Вы собрали пятнашки " + sizeOfBox + "х" + sizeOfBox);
            SetPosAndWriteLine(wordsX, wordsY + 3, "Время");
            SetPosAndWriteLine(wordsX, wordsY + 4, "Количество ходов");
            SetPosAndWriteLine(wordsX, wordsY + 5, "Очки");
            if (sizeOfBox == 3)
                SetPosAndWriteLine(wordsX, wordsY + 6, "------------------------");
            else if (sizeOfBox == 4)
                SetPosAndWriteLine(wordsX, wordsY + 6, "-------------------------");
            SetPosAndWriteLine(wordsX, wordsY + 7, "За время");
            SetPosAndWriteLine(wordsX, wordsY + 8, "За кол-во ходов");
            SetPosAndWriteLine(wordsX, wordsY + 9, "Итого:");
            SetPosAndWriteLineWithSleep(scoresX - 4, wordsY + 3, elapsedTime, 500);
            SetPosAndWriteLineWithSleep(scoresX - Indent(steps), wordsY + 4, steps, 500);
            SetPosAndWriteLineWithSleep(scoresX - Indent(timeScores), wordsY + 7, timeScores, 500);
            SetPosAndWriteLineWithSleep(scoresX - Indent(stepsScores), wordsY + 8, stepsScores, 500);
            Thread.Sleep(500);
            double factor = 1;
            if (sizeOfBox == 4)
                factor = 1.5;
            else if (sizeOfBox == 5)
                factor = 2;
            if (sizeOfBox == 3)
            {
                Console.SetCursorPosition(wordsX + 7, wordsY + 9);
                Console.WriteLine("{0} + {1}", timeScores, stepsScores);
                SetPosAndWriteLine(scoresX - Indent(finalScores), wordsY + 9, finalScores);
                ChangeLineColor(wordsX + 7, wordsY + 11, "Продолжить");
            }
            else
            {
                Console.SetCursorPosition(wordsX, wordsY + 10);
                Console.WriteLine("({0} + {1}) * {2}", timeScores, stepsScores, factor);
                SetPosAndWriteLine(scoresX - Indent(finalScores), wordsY + 10, finalScores);
                ChangeLineColor(wordsX + 8, wordsY + 12, "Продолжить");
            }
            Console.ReadKey();
        }

        public void ShowRecords()
        {
            for (int i = 0; i < 10; i++)
            {
                SetPosAndWriteLine(2, i + 2, i + 1 + ") ");
                Console.SetCursorPosition(6, i + 2);
                if (record.nickName[i] == null)
                {
                    Console.Write("X");
                    Console.SetCursorPosition(23, i + 2);
                    Console.WriteLine(record.recordScores[i]);
                }
                else
                {
                    Console.Write("          ");
                    SetPosAndWriteLine(6, i + 2, record.nickName[i]);
                    Console.SetCursorPosition(23 - Indent(record.recordScores[i]), i + 2);
                    Console.WriteLine(record.recordScores[i]);
                }
            }
        }

        public void CreateIntroductionWindow()
        {
            int page = 1;
            int button = 0;
            bool enterPress = true;
            Console.SetBufferSize(60, 29);
            Console.SetWindowSize(60, 29);
            CreateRectangular(0, 27, 0, 58, '|', '-');
            SetPosAndWriteLine(22, 23, "<--");
            SetPosAndWriteLine(27, 23, "1 стр");
            SetPosAndWriteLine(34, 23, "-->");
            ChangeLineColor(27, 25, "Назад");
            IntroductionPages(1);
            while (enterPress)
            {
                if (Console.KeyAvailable)
                {
                    switch (button)
                    {
                        case 0:
                            SetPosAndWriteLine(27, 25, "Назад");
                            break;
                        case 1:
                            SetPosAndWriteLine(22, 23, "<--");
                            break;
                        case 2:
                            SetPosAndWriteLine(34, 23, "-->");
                            break;
                    }
                    ConsoleKeyInfo key;
                    key = Console.ReadKey();

                    if ((key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W ) && button == 0)
                        button = 1;
                    else if ((key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S) && button != 0)
                        button = 0;
                    else if ((key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D || key.Key == ConsoleKey.A) && button != 0)
                    {
                        if (button == 1)
                            button = 2;
                        else
                            button = 1;
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        if (button == 0)
                        {
                            enterPress = false;
                            CleanWindow(58, 27);
                            break;
                        }
                        else if (button == 1)
                        {
                            page--;
                                if (page == 0)
                                page = 5;
                            CleanWindow(57, 21, 1, 1);
                            IntroductionPages(page);
                        }
                        else
                        {
                            page++;
                            if (page == 6)
                                page = 1;
                            CleanWindow(57, 21, 1, 1);
                            IntroductionPages(page);
                        }
                    }

                    switch (button)
                    {
                        case 0:
                            ChangeLineColor(27, 25, "Назад");
                            break;
                        case 1:
                            ChangeLineColor(22, 23, "<--");
                            break;
                        case 2:
                            ChangeLineColor(34, 23, "-->");
                            break;
                    }
                }
            }
        }

        public void IntroductionPages(int page)
        {
            SetPosAndWriteLine(27, 23, page);
            switch (page)
            {
                case 1:
                    SetPosAndWriteLine(14, 7, "Добро пожаловать в игру пятнашки.");
                    SetPosAndWriteLine(17, 8, "Правила игры очень просты.");
                    SetPosAndWriteLine(10, 9, "Вам дано поле Х на Х, где вам надо будет");
                    SetPosAndWriteLine(12, 10, "переместить квадратики, расположенные ");
                    SetPosAndWriteLine(8, 11, "в хаотичном порядке, по определённому правилу.");
                    SetPosAndWriteLine(9, 12, "Они должны быть расположенны по возрастанию");
                    SetPosAndWriteLine(6, 13, "от верхнего левого угла до нижнего правого угла,");
                    SetPosAndWriteLine(7, 14, "самая крайняя клетка должна оставаться пустой.");
                    SetPosAndWriteLine(7, 15, "Пример можете посмотреть на следующей странице.");
                    break;
                case 2:
                    field = new Field();
                    field.MakeBlocks(4, 14, 2, true);
                    break;
                case 3:
                    SetPosAndWriteLine(4, 8, "Перед игрой вам надо будет выбрать размерность поля.");
                    SetPosAndWriteLine(6, 9, "Чтобы перемещаться между квадратиками используйте");
                    SetPosAndWriteLine(7, 10, "клавиши A,S,W,D или стрелки влево, вниз, вверх");
                    SetPosAndWriteLine(5, 11, "и вправо. Выделенный квадрат окрашен в белый цвет.");
                    SetPosAndWriteLine(3, 12, "Чтобы переместить квадрат, нужно найти квадрат который");
                    SetPosAndWriteLine(15, 13, "расположен возле пустого места");
                    SetPosAndWriteLine(16, 14, "и нажать 'Пробел' или 'Enter' ");
                    SetPosAndWriteLine(7, 15, "Чтобы поставить игру на паузу игрок нажмите 'p'.");
                    SetPosAndWriteLine(7, 16, "Пример можете посмотреть на следующей странице.");
                    break;

                case 4:
                    field = new Field();
                    field.MakeBlocks(4, 14, 2);
                    Thread.Sleep(300);
                    for (int i = 0; i < 3; i++)
                    {
                        field.KeyboardMove(ConsoleKey.DownArrow, 14, 2);
                        Thread.Sleep(300);
                    }
                    field.KeyboardMove(ConsoleKey.RightArrow, 14, 2);
                    Thread.Sleep(300);
                    field.KeyboardMove(ConsoleKey.RightArrow, 14, 2);
                    Thread.Sleep(300);
                    field.PushBlock(14, 2);
                    break;
                case 5:
                    SetPosAndWriteLine(4, 8, "Когда вы собирёте пятнашки, игра посчитает выши очки");
                    SetPosAndWriteLine(5, 9, "исходя из того какой вы уровень выбрали, засколько");
                    SetPosAndWriteLine(8, 10, "минут собрали и сколько раз вы переместили");
                    SetPosAndWriteLine(8, 11, "квадратики. Если вы установили новый рекорд,");
                    SetPosAndWriteLine(12, 12, "то вы должны будете написать своё имя.");
                    SetPosAndWriteLine(17, 13, "Рекорды можно найти в меню.");
                    break;

            }

        }

        public void CreateRecordWindow()
        {
            int recordLine = 1;
            bool enterPress = true;
            Console.SetBufferSize(27, 17);
            Console.SetWindowSize(27, 17);
            CreateRectangular(0, 16, 0, 25, '|', '-');
            Console.SetCursorPosition(2, 2);
            SetPosAndWriteLine(5, 13, "Очистить рекорды");
            ShowRecords();
            while (true)
            {
                ChangeLineColor(11, 14, "Назад");
                while (enterPress)
                {
                    if (Console.KeyAvailable)
                    {
                        switch (recordLine)
                        {
                            case 0:
                                SetPosAndWriteLine(5, 13, "Очистить рекорды");
                                break;
                            case 1:
                                SetPosAndWriteLine(11, 14, "Назад");
                                break;
                        }

                        ConsoleKeyInfo key;
                        key = Console.ReadKey();

                        if (key.Key == ConsoleKey.UpArrow)
                        {
                            recordLine--;
                            if (recordLine == -1)
                                recordLine = 1;
                        }
                        else if (key.Key == ConsoleKey.DownArrow)
                        {
                            recordLine++;
                            if (recordLine == 2)
                                recordLine = 0;
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                            enterPress = false;
                            break;
                        }

                        switch (recordLine)
                        {
                            case 0:
                                ChangeLineColor(5, 13, "Очистить рекорды");
                                break;
                            case 1:
                                ChangeLineColor(11, 14, "Назад");
                                break;
                        }
                    }
                }

                if (recordLine == 1)
                {
                    CleanWindow(25, 16);
                    break;
                }
                else
                {
                    record.CleanRecords();
                    CleanWindow(24, 12, 2, 2);
                    ShowRecords();
                    enterPress = true;
                    recordLine = 1;
                }
            }
        }

        public void CountScores(int steps)
        {
            switch (sizeOfBox)
            {
                case 3:
                    if (steps > 250 && steps <= 300)
                        stepsScores = 100;
                    else if (steps > 225 && steps <= 250)
                        stepsScores = 200;
                    else if (steps > 200 && steps <= 225)
                        stepsScores = 300;
                    else if (steps > 175 && steps <= 200)
                        stepsScores = 400;
                    else if (steps > 150 && steps <= 175)
                        stepsScores = 500;
                    else if (steps <= 150)
                        stepsScores = 1000;

                    if (time < 60)
                        timeScores = 1000;
                    else if (time >= 60 && time < 80)
                        timeScores = 800;
                    else if (time >= 80 && time < 100)
                        timeScores = 600;
                    else if (time >= 100 && time < 120)
                        timeScores = 400;
                    else if (time >= 120 && time < 150)
                        timeScores = 300;
                    else if (time >= 150 && time < 180)
                        timeScores = 200;
                    else if (time >= 180 && time < 240)
                        timeScores = 100;
                    else if (time >= 240 && time < 300)
                        timeScores = 50;

                    finalScores = timeScores + stepsScores;
                    break;

                case 4:

                    if (steps > 400 && steps <= 500)
                        stepsScores = 100;
                    else if (steps > 350 && steps <= 400)
                        stepsScores = 200;
                    else if (steps > 325 && steps <= 350)
                        stepsScores = 300;
                    else if (steps > 300 && steps <= 325)
                        stepsScores = 400;
                    else if (steps > 275 && steps <= 300)
                        stepsScores = 500;
                    else if (steps <= 275)
                        stepsScores = 1000;

                    if (time < 90)
                        timeScores = 1000;
                    else if (time >= 90 && time < 110)
                        timeScores = 800;
                    else if (time >= 110 && time < 135)
                        timeScores = 600;
                    else if (time >= 135 && time < 160)
                        timeScores = 400;
                    else if (time >= 160 && time < 190)
                        timeScores = 300;
                    else if (time >= 190 && time < 220)
                        timeScores = 200;
                    else if (time >= 220 && time < 300)
                        timeScores = 100;
                    else if (time >= 300 && time < 420)
                        timeScores = 50;

                    finalScores = (timeScores + stepsScores) * 1.5;
                    break;

                case 5:
                    if (steps > 600 && steps <= 700)
                        stepsScores = 100;
                    else if (steps > 500 && steps <= 600)
                        stepsScores = 200;
                    else if (steps > 450 && steps <= 500)
                        stepsScores = 300;
                    else if (steps > 400 && steps <= 450)
                        stepsScores = 400;
                    else if (steps > 350 && steps <= 400)
                        stepsScores = 500;
                    else if (steps <= 350)
                        stepsScores = 1000;

                    if (time < 120)
                        timeScores = 1000;
                    else if (time >= 120 && time < 150)
                        timeScores = 800;
                    else if (time >= 150 && time < 180)
                        timeScores = 600;
                    else if (time >= 180 && time < 210)
                        timeScores = 400;
                    else if (time >= 210 && time < 270)
                        timeScores = 300;
                    else if (time >= 270 && time < 330)
                        timeScores = 200;
                    else if (time >= 330 && time < 420)
                        timeScores = 100;
                    else if (time >= 420 && time < 600)
                        timeScores = 50;

                    finalScores = (timeScores + stepsScores) * 2;
                    break;
            }
            
        }

        public int Indent(object number)
        {
            int numberNumbers = 0;
            numberNumbers = Convert.ToInt32(number);
            int numberPosition = 0;
            while (!(numberNumbers >= 0 && numberNumbers < 10))
            {
                numberNumbers = numberNumbers / 10;
                numberPosition++;
            }
            return numberPosition;
        }
        
        private void CreateRectangular(int up, int down, int left, int right, char symVert, char symHor)
        {
            verticalLine.CreateVerticalLine(left, up, down, symVert);
            verticalLine.DrawPoints();
            verticalLine.CreateVerticalLine(right, up, down, symVert);
            verticalLine.DrawPoints();
            horizontalLine.CreateHorizontalLine(left, right, up, symHor);
            horizontalLine.DrawPoints();
            horizontalLine.CreateHorizontalLine(left, right, down, symHor);
            horizontalLine.DrawPoints();
        }

        private void SetPosAndWriteLine(int x, int y, object word)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(word);
            
        }

        private void SetPosAndWriteLineWithSleep (int x, int y, object word, int sleepTime)
        {
            Thread.Sleep(sleepTime);
            Console.SetCursorPosition(x, y);
            Console.Write(word);
        }


    }
}
