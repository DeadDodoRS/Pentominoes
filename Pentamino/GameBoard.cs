using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentamino
{
    public enum Colors { WHITE = 0, BLACK, COLORFUL }

    class GameBoard
    {
        public Stack<Figure> listFigureOnBoard = new Stack<Figure>();
        public StringBuilder AnswerString;

        private int[,] gameBoard;
        private const int CountRotate = 4;

        FigureNumerator figureNumerator;

        public GameBoard(string filePath)
        {
            figureNumerator = new FigureNumerator();
            AnswerString = new StringBuilder();

            ReadGameBoard(filePath);

            Console.WriteLine(figureNumerator);
        }


        public void Insert(Figure figure)
        {
            int i, j;
            FindEmptyCell(out i, out j);
            //Console.WriteLine($"Вставка в {i} , {j}");

            TryInsertRandomRotate(figure, i, j);
        }

        public void CompliteBoard()
        {
            while (AnswerString.Length <= 11)
            {
                //Находим координаты поля
                int i, j;
                FindEmptyCell(out i, out j);

                //Поиск фигуры для подстановки
                TryInsertRandomFigure(i, j);
            }


            Console.WriteLine("Готово");

        }

        public void TryInsertRandomFigure(int i, int j)
        {

            if (TryInsertRandomRotate(figureNumerator.GetCurrent(), i, j))
            {
                //Запись
                InsertByCord(i, j, figureNumerator.GetCurrent());

                AnswerString.Append(figureNumerator.GetCurrent().Symbol);
                Console.WriteLine(AnswerString);

                figureNumerator.MoveNextDigit();
            }
            else
            {
                while (figureNumerator.MoveNextThisDigit() == false)
                {
                    ////Второй шанс фигуры
                    //figureNumerator.SetCurrent(listFigureOnBoard.Peek());
                    //figureNumerator.RotateCurrent();

                    figureNumerator.MovePriviosDigit();
                    RemoveLastFigure();

                    //if (TryInsertRandomRotate(figureNumerator.GetCurrent(), i, j))
                    //{

                    //    Console.WriteLine("Второй шанс");
                    //    InsertByCord(i, j, figureNumerator.GetCurrent());
                    //} else

                    //{

                        Console.WriteLine("удалить");
                        //figureNumerator.MovePriviosDigit();

                        //RemoveLastFigure();
                        AnswerString.Length--;
                    //}

                }
            }

            return;

        }


        //Проверка всех комбинаций фигуры с поворотами и отзеркаливанием
        public bool TryInsertRandomRotate(Figure figure, int i, int j)
        {

            for (int n = 0; n < CountRotate * 2; n++)
            {
                //Как пройдет половина поворотов - зеркалим и проходим цикл ещё раз
                if (n == CountRotate)
                {
                    //Вставка не прошла - делаем поворот
                    figure.Mirror();
                }

                if (TryInsert(i, j, figure))
                {
                    //Если вставка прошла успешно - выходим из метода
                    return true;
                }

                //Вставка не прошла - зеркалим
                figure.Rotate();

            }

            return false;
        }


        //Пытаемся совместить остальные ячейки фигуры
        private bool TryInsert(int y, int x, Figure figure)
        {
            //Берем самую левую ячейку в фигуре
            int iFig = figure.GetFirstCell();

            //Наложение массива фигуры на массив поля 
            for (int i = y - iFig; i < figure.GetLength(0) + y - iFig; i++)
            {
                for (int j = x; j < figure.GetLength(1) + x; j++)
                {
                    //Если работаем на проверку, то работают алгоритмы проверки

                    if (CheckMapBounds(i, j))
                    {
                        return false;
                    }

                    //Если в эту ячейку не нужно записывать, а у фигуры есть ячейка
                    if (gameBoard[i, j] == 0 && figure[i - y + iFig, j - x])
                    {
                        return false;
                    }

                    // > 1, значит ячейка закрашена, а у фигуры есть ячейка
                    if (gameBoard[i, j] > 1 && figure[i - y + iFig, j - x])
                    {
                        return false;
                    }
                }
            }

            //Фигура вписывается в поле
            return true;
        }

        private void InsertByCord(int y, int x, Figure figure)
        {
            //Берем самую левую ячейку в фигуре
            int jFig = figure.GetFirstCell();

            //Проверка прошла => запись
            for (int i = y - jFig; i < figure.GetLength(0) + y - jFig; i++)
            {
                for (int j = x; j < figure.GetLength(1) + x; j++)
                {
                    if (figure[i - y + jFig, j - x])
                    {
                        //Count + 2 это псевдо-цвет
                        gameBoard[i, j] = listFigureOnBoard.Count + 2;
                    }
                }
            }

            //Запись буквы в строку-решение
            figure.SetCoordinates(x, y);
            //Добавление в список
            listFigureOnBoard.Push(figure);
        }



        public bool CheckMapBounds(int y, int x)
        {
            if (y < 0 || y >= gameBoard.GetLength(0))
            {
                return true;
            }

            if (x < 0 || x >= gameBoard.GetLength(1))
            {
                return true;
            }

            return false;
        }

        //Поиск пустой ячейки
        public void FindEmptyCell(out int x, out int y)
        {
            //заполняем поле слева направо по столбцам
            for (int j = 0; j < gameBoard.GetLength(1); j++)
            {
                for (int i = 0; i < gameBoard.GetLength(0); i++)
                {
                    if (gameBoard[i, j] == 1)
                    {
                        //Передаем по ссылке координаты пустой ячейки
                        x = i;
                        y = j;
                        return;
                    }
                }
            }

            //Если свободных ячеек нет
            x = y = 0;
            return;
        }

        //Удаление с поля последней фигуры
        private void RemoveLastFigure()
        {
            Figure figureToDelete = listFigureOnBoard.Pop();

            //Берем самую левую ячейку в фигуре
            int jFig = figureToDelete.GetFirstCell();
            int y = figureToDelete.y;
            int x = figureToDelete.x;

            //Проверка прошла => запись
            for (int i = y - jFig; i < figureToDelete.GetLength(0) + y - jFig; i++)
            {
                for (int j = x; j < figureToDelete.GetLength(1) + x; j++)
                {
                    if (figureToDelete[i - y + jFig, j - x])
                    {
                        //1 => ячейка пуста
                        gameBoard[i, j] = 1;
                    }
                }
            }


        }

        //Для доступа к ячейкам поля
        public int this[int i, int j]
        {
            get
            {
                return gameBoard[i, j];
            }

            private set
            {
                gameBoard[i, j] = value;
            }
        }

        private void ReadGameBoard(string path)
        {
            FileReader fr = new FileReader();
            int jArray = 0;
            List<string> strList = new List<string>();
            //Считывание поля в список
            fr.ReadFile(ref jArray, ref strList, path);

            //Запись элементов в игровое поле
            SetGameBoard(ref jArray, ref strList);
        }

        public void SetGameBoard(ref int jArray, ref List<string> strArray)
        {
            //Инициализация игрового поля
            gameBoard = new int[strArray.Count, jArray];

            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                //Считывание строки 
                string line = strArray[i];

                for (int j = 0; j < jArray; j++)
                {
                    //Запись 0 если символ строки не пустой 
                    if (j < line.Length && line[j] == 'o')
                    {
                        gameBoard[i, j] = 1;
                    }
                    else
                    {
                        gameBoard[i, j] = 0;
                    }
                }
            }
        }

        public override string ToString()
        {
            //Используется StringBuilder тк происходит множественное изменение строки
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    sb.Append($"{ Convert.ToInt32(gameBoard[i, j])} ");
                }
                sb.Append(Environment.NewLine);
            }

            //Обратное преобразование
            string anw = sb.ToString();
            return anw;
        }
    }
}
