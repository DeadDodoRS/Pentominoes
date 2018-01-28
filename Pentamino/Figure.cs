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

        //Храним количество совершенных оборотов
        public int RotateCount { get; private set; } = 0;

        //Количество поворотов, совершив которые фигура вернется в исходное положение
        public int RotateToFullTurn { get; private set; }

        //Координаты на поле
        public int x { get; private set; }
        public int y { get; private set; }

        public Figure(PentaminoSymbols symbol)
        {
            Symbol = symbol;
            //Задаем массив
            SymbolArray = PentaminoFigurePattern.GetArrayBySymbol(symbol);

            SetRotateToFullTurn();
        }

        //Изменение поворота или ориентации фигуры в зависимости от уже произведенных манипуляций над фигурой
        public void SetAnotherLocation()
        {
            Rotate();

            //Для Z зекралить фигуру чаще
            if (Symbol == PentaminoSymbols.T && RotateCount == RotateToFullTurn / 4)
                Mirror();

            if (RotateCount == RotateToFullTurn / 2 || RotateCount == RotateToFullTurn)
                Mirror();

        }


        //Сколько поворотов нужно каждой фигуре для полного поворота
        private void SetRotateToFullTurn()
        {
            if (Symbol == PentaminoSymbols.T || Symbol == PentaminoSymbols.V || Symbol == PentaminoSymbols.U 
                || Symbol == PentaminoSymbols.W || Symbol == PentaminoSymbols.Z)
            {
                RotateToFullTurn = 4;
                return;
            }

            if (Symbol == PentaminoSymbols.I)
            {
                RotateToFullTurn = 2;
                return;
            }

            if (Symbol == PentaminoSymbols.X)
            {
                RotateToFullTurn = 1;
                return;
            }

            RotateToFullTurn = 8;
        }

        //При установке на поле
        public void SetCoordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        //Поворот происходит против часовой стрелке
        private void Rotate()
        {
            RotateCount += 1;

            //Вводим исключения для неповоротных фигур
            if (!CanRotate())
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

        private bool CanRotate()
        {
            //Если символ - Х, то при повороте он не изменится
            if (Symbol == PentaminoSymbols.X)
                return false;

            return true;
        }

        //Отзеркалить фигуру
        private void Mirror()
        {
            //Вводим исключения для незекральных фигур
            if (!CanMirror())
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

        private bool CanMirror()
        {
            if (Symbol == PentaminoSymbols.X || Symbol == PentaminoSymbols.I || Symbol == PentaminoSymbols.T ||
                Symbol == PentaminoSymbols.V || Symbol == PentaminoSymbols.U || Symbol == PentaminoSymbols.W)
                return false;

            return true;
        }

        //Фигура сделала полный оборот
        public void FigureInBasePosition()
        {
            RotateCount = 0;
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
