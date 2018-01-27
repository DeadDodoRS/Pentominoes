using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentamino
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Введите имя файла для загрузки игрового поля :");
            //string pathFrom = Console.ReadLine();
            //Console.WriteLine("Введите имя файла для сохранения результата :");
            //string pathTo = Console.ReadLine();


            GameBoard gm = new GameBoard("C:/Junior/9.in");

            //while (true)
            //{
            //    Console.WriteLine("Игровое поле: ");
            //    Console.WriteLine(gm.ToString());

            //    string str = Console.ReadLine();

            //    if (str == "d")
            //    {
            //        gm.RemoveLastFigure();
            //        continue;
            //    }

            //    if (str != null)
            //    {
            //        gm.Insert(new Figure(PentaminoFigurePattern.CharToPentaminoSymbol(Convert.ToChar(str))));
            //    }

            //    Console.ReadKey();
            //}

            gm.CompliteBoard();

            //FileWorker fr = new FileWorker();
            //fr.WriteFile(pathTo, gm);
            //Console.WriteLine("Запись в файл завершена");

            Console.WriteLine("Игровое поле: ");
            Console.WriteLine(gm.ToString());


            Console.ReadKey();


        }

    }
}
