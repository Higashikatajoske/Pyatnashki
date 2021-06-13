using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pyatnashki
{
    class Record
    {
        public int[] recordScores = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public string[] nickName = new string[10];
        int countOfLines;
        int count;
        bool space;
        char[] fg;
        char test;

        public void CleanRecords() // функция очищения рекордов
        {
            recordScores = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            nickName = new string[10];
            WriteInFile();
            StreamWriter writer = new StreamWriter("Records.txt");
            writer.WriteLine("");
            writer.Close();
        }

        public bool CheckRecords(double scores)
        {
            bool newRecord = false;
            int[] scoresMas = recordScores;
            for (int i = 0; i < 10; i++)
                if (scores > scoresMas[i])
                    newRecord = true;
            return newRecord;
        }

        public void CreateNewRecord(double scores, string newNickName)
        {
            int newScores;
            newScores = Convert.ToInt32(scores);
            int changedScores;
            string changedName;
            for (int i = 0; i < 10; i++)
            {
                if (recordScores[i] == 0)
                {
                    recordScores[i] = newScores;
                    nickName[i] = newNickName;
                    break;
                }
                else if (recordScores[i] < newScores)
                {
                    changedScores = recordScores[i];
                    changedName = nickName[i];
                    recordScores[i] = newScores;
                    nickName[i] = newNickName;
                    newScores = changedScores;
                    newNickName = changedName;
                }
            }
        }

        public void ReadFormFile() // запись данных из файла во временную память
        {
            StreamReader reader;
            reader = new StreamReader("Records.txt", Encoding.Default);
            if (reader.ReadLine() != "")
            {
                reader.Close();
                reader = new StreamReader("Records.txt", Encoding.Default);
                while (reader.ReadLine() != null)
                    countOfLines++;
            }
            reader.Close();
            for (int i = 0; i < countOfLines; i++)
            {
                count = 0;
                space = false;
                fg = null;
                reader = new StreamReader("Records.txt", Encoding.Default);
                for (int j = 0; j < i; j++)
                    reader.ReadLine();
                while (space == false)
                {
                    test = Convert.ToChar(reader.Read());
                    if (test == ' ')
                        space = true;
                    else
                        count++;
                }
                reader.Close();
                reader = new StreamReader("Records.txt", Encoding.Default);
                for (int j = 0; j < i; j++)
                    reader.ReadLine();
                fg = new char[count];
                for (int j = 0; j < count; j++)
                    fg[j] = Convert.ToChar(reader.Read());
                nickName[i] = new string(fg);
                recordScores[i] = Convert.ToInt32(reader.ReadLine());
                reader.Close();
            }
        }

        public void WriteInFile() // запись данных временной памяти в файл
        {
            StreamWriter writer = new StreamWriter("Records.txt");
            for (int i = 0; i < 10; i++)
            {
                if (nickName[i] != null)
                    writer.WriteLine(nickName[i] + " " + recordScores[i] + " ");
                else
                    break;
            }
            writer.Close();
        }
    }
}
