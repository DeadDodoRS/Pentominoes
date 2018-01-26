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
            Console.WriteLine("Введите имя файла для загрузки игрового поля :");
            string pathFrom = Console.ReadLine();
            Console.WriteLine("Введите имя файла для сохранения результата :");
            string pathTo = Console.ReadLine();

            FileReader fr = new FileReader();
            GameBoard gm = new GameBoard(pathFrom);


            gm.CompliteBoard();
            fr.WriteFile(pathTo, gm);

            Console.WriteLine("Запись в файл завершена");

            Console.WriteLine("Игровое поле: ");
            Console.WriteLine(gm.ToString());

            Console.ReadKey();


        }

    }
}
