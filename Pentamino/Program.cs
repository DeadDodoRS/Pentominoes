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

            //gm.Insert(new Figure(PentaminoSymbols.F));
            //gm.Insert(new Figure(PentaminoSymbols.I));
            //gm.Insert(new Figure(PentaminoSymbols.I));
            //gm.Insert(new Figure(PentaminoSymbols.T));

            //while (true)
            //{
            //    Console.WriteLine("Игровое поле: ");
            //    Console.WriteLine(gm.ToString());

            //    if (Console.ReadLine() == "k")
            //    {
            //        gm.ReinsertCurrentFigure();                    
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
