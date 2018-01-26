using System;
using System.Collections.Generic;
using System.Text;

namespace Pentamino
{
    public enum CellStates { NONE, NONINSERT}

    class GameBoard
    {
        public Stack<Figure> FiguresOnBoard = new Stack<Figure>();
        public StringBuilder AnswerString = new StringBuilder();

        private int[,] gameBoard;
        private const int CountRotate = 4;

        FigureNumerator figureNumerator = new FigureNumerator();

        public GameBoard(string filePath)
        {
            ReadGameBoard(filePath);
        }

        public void Insert(Figure figure)
        {
            //Поиск координат для вставки
            FindEmptyCell(out int i, out int j);

            if (CheckCanInsertAllRotate(figure, i, j)){

                InsertByCord(i, j, figure);
            }
        }

        public void CompliteBoard()
        {
            //Пока строка-ответ не будет содержать все символы
            while (AnswerString.Length < figureNumerator.Count)
            {
                //Находим координаты поля
                FindEmptyCell(out int i, out int j);

                //Поиск фигуры для подстановки
                TryInsertRandomFigure(i, j);
            }

            Console.WriteLine("Подстановка завершена");
        }

        public void TryInsertRandomFigure(int i, int j)
        {
            //Если текущий символ подходит, то устанавливаем на поле и записываем
            if (CheckCanInsertAllRotate(figureNumerator.GetCurrent(), i, j))
            {
                //Запись
                InsertByCord(i, j, figureNumerator.GetCurrent());
                figureNumerator.MoveNextDigit();
            }
            else
            {
                //Если символ не подошел, переходим к следующему 
                while (figureNumerator.MoveNextThisDigit() == false)
                {
                    //Если перейти к следующему не получится (тк закончились), 
                    //переходим к предыдущему символу, удаляем, подставляем новую фигуру 

                    if (false)
                    {
                        //Код для попытки поворота уже вставленной фигуры
                        Figure figure = FiguresOnBoard.Peek();
                        figure.Rotate();

                        RemoveLastFigure();

                        if (CheckCanInsertAllRotate(figure, i, j)){
                            //TODO
                        }

                    }


                    figureNumerator.MovePreviousDigit();
                    RemoveLastFigure();

                    Console.WriteLine("Удаление");
                }
            }
        }

        //Пытаемся вставить текущую фигуру другой стороной
        public void ReinsertCurrentFigure()
        {
            Figure figure = FiguresOnBoard.Peek();
            RemoveLastFigure();
            figure.Rotate();

            Insert(figure);
        }


        //Проверка всех комбинаций фигуры с поворотами и отзеркаливанием
        public bool CheckCanInsertAllRotate(Figure figure, int i, int j)
        {
            for (int n = 0; n < CountRotate * 2; n++)
            {
                //Как пройдет половина поворотов - зеркалим и проходим цикл ещё раз
                if (n == CountRotate)
                {
                    //Вставка не прошла - делаем поворот
                    figure.Mirror();
                }

                if (CheckCanInsert(i, j, figure))
                {
                    //Если вставка прошла успешно - выходим из метода
                    return true;
                }

                //Вставка не прошла - зеркалим
                figure.Rotate();
            }
            return false;
        }


        //Пытаемся совместить ячейки фигуры в игровое поле
        private bool CheckCanInsert(int y, int x, Figure figure)
        {
            //Значение отступа в первом столбце
            int iIndent = figure.GetIndent();

            //Наложение массива фигуры на массив поля 
            for (int i = y - iIndent; i < figure.GetLength(0) + y - iIndent; i++)
            {
                for (int j = x; j < figure.GetLength(1) + x; j++)
                {
                    //Проверка выхода за пределы поля
                    if (CheckMapBounds(i, j))
                        return false;

                    //Если в эту ячейку не нужно записывать, а у фигуры ячейка заполнена
                    if ((gameBoard[i, j] == (int)CellStates.NONE || gameBoard[i, j] > 1) && figure[i - y + iIndent, j - x])
                        return false;

                }
            }

            //Фигура вписывается в поле
            return true;
        }

        public bool CheckMapBounds(int i, int j)
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
            int jFig = figure.GetIndent();

            //Проверка прошла => запись
            for (int i = y - jFig; i < figure.GetLength(0) + y - jFig; i++)
            {
                for (int j = x; j < figure.GetLength(1) + x; j++)
                {
                    if (figure[i - y + jFig, j - x])
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
            AnswerString.Append(figureNumerator.GetCurrent().Symbol);
            Console.WriteLine(AnswerString);
        }


        //Поиск пустой ячейки
        public void FindEmptyCell(out int x, out int y)
        {
            //заполняем поле слева направо по столбцам
            for (int j = 0; j < gameBoard.GetLength(1); j++)
            {
                for (int i = 0; i < gameBoard.GetLength(0); i++)
                {
                    if (gameBoard[i, j] == (int) CellStates.NONINSERT)
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
        private void RemoveLastFigure()
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
                        gameBoard[i, j] = (int) CellStates.NONINSERT;
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
                        gameBoard[i, j] = (int) CellStates.NONINSERT;
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
                    if (gameBoard[i, j] != (int) CellStates.NONE && gameBoard[i, j] != (int)CellStates.NONINSERT)
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
