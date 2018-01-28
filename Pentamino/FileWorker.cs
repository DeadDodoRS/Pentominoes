using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pentamino
{
    class FileWorker
    {
        public List<string> ReadFile(ref int jArray, ref List<string> strList, string path)
        {
            if (!File.Exists(path))
                return null;


            string line;
            //Поиск самой длинной строки(j-переменная of array)
            int maxLength = 0;

            //Построчное считывание и запись в лист
            StreamReader file = new StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                //Поиск самой длинной строки
                maxLength = line.Length > maxLength ? line.Length : maxLength;
                strList.Add(line);
            }

            file.Close();
            Console.WriteLine("Чтение прошло успешно");

            jArray = maxLength;
            return strList;
        }

        public void WriteFile(string writePath, GameBoard gameboard)
        {
            using (StreamWriter sw = new StreamWriter(writePath, false, Encoding.Default))
            {
                sw.WriteLine(gameboard);
            }

            Console.WriteLine("Запись в файл завершена");
        }
    }
}
