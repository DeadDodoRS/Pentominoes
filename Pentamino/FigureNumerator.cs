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
        List<Figure> PentaminoList = new List<Figure>();
        public int numerator { get; private set; } = 0;

        //На какое количество элементов передвигаем неподходящие фигуры назад в списке
        public int dellayReplace { get; private set; } = 1;

        public FigureNumerator()
        {
            foreach (PentaminoSymbols s in Enum.GetValues(typeof(PentaminoSymbols)))
            {
                PentaminoList.Add(new Figure(s));
            }
        }


        public Figure GetCurrent()
        {
            return PentaminoList[numerator];
        }

        public void SetCurrent(Figure value)
        {
            PentaminoList[numerator] = value;
        }

        public void RotateCurrent()
        {
            PentaminoList[numerator].Rotate();
        }

        public void MoveNextDigit()
        {
            numerator++;
        }

        public Figure this[int i]
        {
            get
            {
                return PentaminoList[numerator];
            }

            private set
            {
                PentaminoList[i] = value;
            }
        }

        public bool MoveNextThisDigit()
        {
            for (int i = numerator; i < PentaminoList.Count; i++)
            {
                if ((int)PentaminoList[i].Symbol > (int)PentaminoList[numerator].Symbol)
                {
                    var temp = PentaminoList[numerator];

                    PentaminoList[numerator] = PentaminoList[i];
                    PentaminoList[i] = temp;


                    return true;
                }
            }

            //Если не удалось подставить
            return false;
        }

        public void MovePriviosDigit()
        {
            ReplaceCurrentWithLast();
            MoveBack();
        }

        public void MoveBack()
        {
            numerator--;
        }

        public void MoveDellayReplace()
        {
            dellayReplace++;
        }

        public void ReturnDellayReplace()
        {
            dellayReplace = 1;
        }

        public void ReplaceCurrentWithNext()
        {
            var temp = PentaminoList[numerator];

            PentaminoList[numerator] = PentaminoList[numerator + 1];
            PentaminoList[numerator + 1] = temp;
        }

        public void ReplaceCurrentWithNextTen()
        {
            for (int i = numerator; i < PentaminoList.Count - 1; i++)
            {
                if ((int)PentaminoList[i + 1].Symbol < (int)PentaminoList[i + 1].Symbol)
                {
                    var temp = PentaminoList[numerator];

                    PentaminoList[numerator] = PentaminoList[numerator + dellayReplace];
                    PentaminoList[numerator + dellayReplace] = temp;
                    return;
                }
            }

            ReplaceCurrentWithNext();
        }

        public void ReplaceCurrentWithLast()
        {
            for (int i = numerator; i < PentaminoList.Count - 1; i++)
            {
                var temp = PentaminoList[i];

                PentaminoList[i] = PentaminoList[i + 1];
                PentaminoList[i + 1] = temp;
            }

        }


        public override string ToString()
        {
            StringBuilder anw = new StringBuilder();

            foreach (Figure s in PentaminoList)
            {
                anw.Append((s.Symbol));
            }

            return anw.ToString();
        }

        public string OtherString()
        {
            string anw = string.Empty;

            for (int i = numerator; i < PentaminoList.Count; i++)
            {
                anw += PentaminoList[i].Symbol;
            }

            return anw;
        }

    }
}
