using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentamino
{
    class Figure
    {
        //Представление символа в виде массива
        public bool[,] SymbolArray { get; private set; }
        public PentaminoSymbols Symbol { get; private set; }

        //Координаты на поле
        public int x { get; private set; }
        public int y { get; private set; }

        public Figure(PentaminoSymbols symbol)
        {
            Symbol = symbol;
            //Задаем массив
            SymbolArray = PentaminoFigurePattern.GetArrayBySymbol(symbol);
        }

        //При установке на поле
        public void SetCoordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        //Поворот происходит против часовой стрелке
        public void Rotate()
        {
            //Если символ - Х, то при повороте он не изменится
            if (Symbol == PentaminoSymbols.X)
                return;

            //Создаем новый массив, в котором размерность X = Y и Y = X
            bool[,] newArray = new bool[SymbolArray.GetLength(1), SymbolArray.GetLength(0)];

            for (int i = 0; i < newArray.GetLength(0); i++)
            {
                for (int j = 0; j < newArray.GetLength(1); j++)
                {
                    //Выполняем поворот на 90 против часовой
                    newArray[i, j] = SymbolArray[j, SymbolArray.GetLength(1) - 1 - i];
                }
            }

            //Меняем текущий массив на новый
            SymbolArray = newArray;
        }

        //Отзеркалить фигуру
        public void Mirror()
        {
            //Вводим исключения для незекральных фигур
            if (Symbol == PentaminoSymbols.X || Symbol == PentaminoSymbols.I || Symbol == PentaminoSymbols.T ||
                Symbol == PentaminoSymbols.V || Symbol == PentaminoSymbols.U || Symbol == PentaminoSymbols.W)
                return;

            for (int j = 0; j < SymbolArray.GetLength(1) / 2; j++)
            {
                for (int i = 0; i < SymbolArray.GetLength(0); i++)
                {
                    bool temp = SymbolArray[i, j];
                    SymbolArray[i, j] = SymbolArray[i, SymbolArray.GetLength(1) - 1 - j];
                    SymbolArray[i, SymbolArray.GetLength(1) - 1 - j] = temp;
                }
            }
        }

        //Координаты непустой ячейки в первом столбце
        //Координата по которой будем делать проверку вставки в поле
        public int GetIndent()
        {
            //Тк в первая столбец не может быть пустым => цикл один
            for (int i = 0; i < SymbolArray.GetLength(0); i++)
            {
                if (SymbolArray[i, 0])
                {
                    return i;
                }
            }

            //Поиск должен дать результат, строка не выполнится
            return 0;
        }

        //Для доступа к ячейкам поля
        public bool this[int i, int j]
        {
            get
            {
                return SymbolArray[i, j];
            }

            private set
            {
                SymbolArray[i, j] = value;
            }
        }

        public override string ToString()
        {
            //Используется StringBuilder тк происходит множественное изменение строки
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < SymbolArray.GetLength(0); i++)
            {
                for (int j = 0; j < SymbolArray.GetLength(1); j++)
                {
                    sb.Append($"{Convert.ToInt32(SymbolArray[i, j])} ");
                }
                sb.Append(Environment.NewLine);
            }

            //Обратное преобразование
            return sb.ToString();
        }

        //Проверка является ли данный символ символом из Пентамино
        private static bool isPentaminoSymbol(char symbol)
        {
            string symb = symbol.ToString();

            foreach (PentaminoSymbols s in Enum.GetValues(typeof(PentaminoSymbols)))
            {
                if (symb == Enum.GetName(typeof(PentaminoSymbols), s))
                {
                    return true;
                }
            }

            return false;
        }

        //Возвращаем размерность массива
        public int GetLength(int side)
        {
            if (side == 0)
                return SymbolArray.GetLength(0);


            return SymbolArray.GetLength(1);
        }
    }
}
