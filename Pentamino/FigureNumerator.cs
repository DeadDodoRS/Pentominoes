using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentamino
{
    public enum PentaminoSymbols { F, I, L, N, P, T, U, V, W, X, Y, Z };

    class FigureNumerator
    {
        List<Figure> AllFigureSequence = new List<Figure>();

        //Указатель на каком символе остановились
        public int Numerator { get; private set; } = 0;
        public int Count { get; private set; } = 0;

        public FigureNumerator()
        {
            foreach (PentaminoSymbols symbol in Enum.GetValues(typeof(PentaminoSymbols)))
            {
                AllFigureSequence.Add(new Figure(symbol));
                Count += 1;
            }
        }

        public Figure GetCurrent()
        {
            return AllFigureSequence[Numerator];
        }

        //Переход на разряд ниже (в сторону последнего)
        public void MoveNextDigit()
        {
            Numerator++;
        }

        //Переход на разряд выше (в сторону первого символа)
        public void MoveBackDigit()
        {
            Numerator--;
        }

        //Возврат к предыдущему символу
        public void MovePreviousDigit()
        {
            ReplaceCurrentWithLast();
            MoveBackDigit();
        }

        //Увеличение на 1 текущего разряда
        public bool MoveNextThisDigit()
        {
            for (int i = Numerator; i < AllFigureSequence.Count; i++)
            {
                if ((int)AllFigureSequence[i].Symbol > (int)AllFigureSequence[Numerator].Symbol)
                {
                    var temp = AllFigureSequence[Numerator];

                    AllFigureSequence[Numerator] = AllFigureSequence[i];
                    AllFigureSequence[i] = temp;

                    return true;
                }
            }

            //Разряды закончились
            return false;
        }

        //Выставляем текущий разряд в конец
        public void ReplaceCurrentWithLast()
        {
            for (int i = Numerator; i < AllFigureSequence.Count - 1; i++)
            {
                var temp = AllFigureSequence[i];

                AllFigureSequence[i] = AllFigureSequence[i + 1];
                AllFigureSequence[i + 1] = temp;
            }
        }

        public override string ToString()
        {
            StringBuilder anw = new StringBuilder();

            foreach (Figure figure in AllFigureSequence)
            {
                anw.Append((figure.Symbol));
            }

            return anw.ToString();
        }

    }
}
