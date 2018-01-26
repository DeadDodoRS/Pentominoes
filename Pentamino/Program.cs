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
            //Console.WriteLine("Введите Y поля");
            //int yLen = Convert.ToInt32(Console.ReadLine());

            GameBoard gm = new GameBoard("C://Junior//10.in");

            gm.CompliteBoard();
            Console.ReadKey();


        }

    }
}
