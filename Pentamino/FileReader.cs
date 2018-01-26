using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentamino
{
    class FileReader
    {
        public List<string> ReadFile(ref int jArray, ref List<string> strList, string path)
        {
            string line;
            //Поиск самой длинной строки(j-переменная of array)
            int maxLength = 0;

            //Построчное считывание и запись в лист
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                //Поиск самой длинной строки
                maxLength = line.Length > maxLength ? line.Length : maxLength;
                strList.Add(line);
            }

            file.Close();

            jArray = maxLength;
            return strList;
        }
    }
}
