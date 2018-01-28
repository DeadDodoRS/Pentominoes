using System;
using System.Collections.Generic;
using System.Text;

namespace Pentamino
{
    public enum CellStates { NONE, NONINSERT }

    class GameBoard
    {
        public Stack<Figure> FiguresOnBoard = new Stack<Figure>();
        public StringBuilder AnswerString = new StringBuilder();

        private int[,] gameBoard;
        private bool isNoSolution = false;

        FigureNumerator figureNumerator = new FigureNumerator();

        public GameBoard(string filePath)
        {
            ReadGameBoard(filePath);
        }

        public void Insert(Figure figure)
        {
            //Поиск координат для вставки
            FindEmptyCell(out int i, out int j);

            if (CheckCanInsertAllRotate(figure, i, j))
            {

                InsertByCord(i, j, figure);
            }
        }

        public void CompliteBoard()
        {
            //Пока строка-ответ не будет содержать все символы

            while (AnswerString.Length < figureNumerator.Count && !isNoSolution)
            {
                //Находим координаты поля
                FindEmptyCell(out int i, out int j);

                //Поиск фигуры для подстановки
                TryInsertRandomFigure(i, j);
            }

            ReturnResult();
        }

        private void ReturnResult()
        {
            if (!isNoSolution)
            {
                Console.WriteLine("Подстановка завершена");
            }
            else
            {
                Console.WriteLine("Нет решений");
            }
        }

        private void TryInsertRandomFigure(int i, int j)
        {
            //Console.WriteLine($"Вставка в : {i} {j}");

            //Если текущий символ подходит, то устанавливаем на поле и записываем
            if (CheckCanInsertAllRotate(figureNumerator.GetCurrent(), i, j))
            {
                //Запись
                figureNumerator.MoveNextDigit();
            }
            else
            {
                //Если символ не подошел, переходим к следующему 
                while (figureNumerator.MoveNextThisDigit() == false)
                {
                    //Если перейти к следующему не получится (тк закончились), 
                    //переходим к предыдущему символу, удаляем, подставляем новую фигуру 

                    //Сначала пытаемся подставить эту же фигуру, но другой стороной
                    if (TryInsertPreviousFigureAnotherSide())
                    {
                        return;
                    }

                    //если перебрали все комбинации и в стеке нет фигур
                    if (FiguresOnBoard.Count > 0)
                    {
                        //Если не удалось вставить текущую фигуру другой стороной - 
                        //возвращаемся к предыдущему символу
                        figureNumerator.MovePreviousDigit();
                    } else
                    {
                        isNoSolution = true;
                        return;
                    }
                }
            }

            //Console.WriteLine(this);
        }

        private bool TryInsertPreviousFigureAnotherSide()
        {
            if (FiguresOnBoard.Count == 0)
                return false;

            Figure figure = FiguresOnBoard.Peek();

            RemoveLastFigure();

            //Используем следующую вариацию фигуры
            figure.SetAnotherLocation();

            int y = figure.y;
            int x = figure.x;

            FindEmptyCell(out y, out x);

            //Если у фигуры есть другое возможное размещение
            if (CheckCanInsertAllRotate(figure, y, x))
            {
                //Проходим этот разряд ещё раз
                figureNumerator.ReplaceCurrentWithLast();
                return true;
            }

            return false;
        }


        //Проверка всех комбинаций фигуры с поворотами и отзеркаливанием
        private bool CheckCanInsertAllRotate(Figure figure, int i, int j)
        {

            for (int k = 0; k < figure.RotateToFullTurn; k++)
            {

                //Если фигура сделала полный оборот и вернулась в базовое положение
                if (figure.RotateCount == figure.RotateToFullTurn)
                {
                    figure.FigureInBasePosition();
                    return false;
                }


                if (CheckCanInsert(i, j, figure))
                {
                    //Если вставка прошла успешно - выходим из метода
                    return true;
                }

                //На последнем проходе цикла этот метод вернет уже базовое расположение фигуры
                figure.SetAnotherLocation();
            }

            //Если фигура прошла весь цикл => она вернулась в исходную позицию
            figure.FigureInBasePosition();

            return false;
        }

        //Пытаемся совместить ячейки фигуры в игровое поле
        private bool CheckCanInsert(int y, int x, Figure figure)
        {
            //Значение отступа в первом столбце
            int jIndent = figure.GetIndent();

            //Наложение массива фигуры на массив поля 
            for (int i = y - jIndent; i < figure.GetLength(0) + y - jIndent; i++)
            {
                for (int j = x; j < figure.GetLength(1) + x; j++)
                {
                    //Проверка выхода за пределы поля
                    if (CheckMapBounds(i, j))
                        return false;

                    //Если в эту ячейку не нужно записывать, а у фигуры ячейка заполнена
                    if ((gameBoard[i, j] == (int)CellStates.NONE || gameBoard[i, j] > 1) && figure[i - y + jIndent, j - x])
                        return false;

                }
            }



            //Проверка прошла успешно => вставка
            InsertByCord(y, x, figure);

            //Фигура вписывается в поле
            return true;
        }

        private bool CheckMapBounds(int i, int j)
        {
            if (i < 0 || i >= gameBoard.GetLength(0))
                return true;

            if (j < 0 || j >= gameBoard.GetLength(1))
                return true;

            return false;
        }

        private void InsertByCord(int y, int x, Figure figure)
        {
            //Берем самую левую ячейку в фигуре
            int jIndent = figure.GetIndent();

            //Проверка прошла => запись
            for (int i = y - jIndent; i < figure.GetLength(0) + y - jIndent; i++)
            {
                for (int j = x; j < figure.GetLength(1) + x; j++)
                {
                    if (figure[i - y + jIndent, j - x])
                    {
                        //Count + 2 это псевдо-цвет
                        gameBoard[i, j] = (int)figure.Symbol + 2;
                    }
                }
            }

            //Запись буквы в строку-решение
            figure.SetCoordinates(x, y);
            //Добавление в список
            FiguresOnBoard.Push(figure);

            //Добавление в строку 
            AnswerString.Append(figure.Symbol);
            Console.WriteLine(AnswerString);
        }

        //Поиск пустой ячейки
        private void FindEmptyCell(out int x, out int y)
        {
            //заполняем поле слева направо по столбцам
            for (int j = 0; j < gameBoard.GetLength(1); j++)
            {
                for (int i = 0; i < gameBoard.GetLength(0); i++)
                {
                    if (gameBoard[i, j] == (int)CellStates.NONINSERT)
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
        }

        //Удаление с поля последней фигуры
        public void RemoveLastFigure()
        {
            Figure figureToDelete = FiguresOnBoard.Pop();

            //Берем самую левую ячейку в фигуре
            int jFig = figureToDelete.GetIndent();
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
                        gameBoard[i, j] = (int)CellStates.NONINSERT;
                    }
                }
            }

            //Удаление символа из строки
            AnswerString.Length--;
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
            FileWorker fr = new FileWorker();

            int jArray = 0;
            List<string> strList = new List<string>();
            //Считывание поля в список
            fr.ReadFile(ref jArray, ref strList, path);

            if (strList.Count > 0)
            {
                //Запись элементов в игровое поле
                SetGameBoard(ref jArray, ref strList);
            } else
            {
                Console.WriteLine("Ошибка чтения");
            }
        }

        private void SetGameBoard(ref int jArray, ref List<string> strArray)
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
                        gameBoard[i, j] = (int)CellStates.NONINSERT;
                    }
                    else
                    {
                        gameBoard[i, j] = (int)CellStates.NONE;
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
                    if (gameBoard[i, j] != (int)CellStates.NONE && gameBoard[i, j] != (int)CellStates.NONINSERT)
                    {
                        sb.Append($"{ (PentaminoSymbols)Convert.ToInt32((gameBoard[i, j]) - 2)} ");
                    }
                    else if (gameBoard[i, j] == (int)CellStates.NONE)
                    {
                        sb.Append("- ");
                    }
                    else if (gameBoard[i, j] == (int)CellStates.NONINSERT)
                    {
                        sb.Append("0 ");
                    }
                }
                sb.Append(Environment.NewLine);
            }

            //Обратное преобразование
            return sb.ToString();
        }
    }
}
