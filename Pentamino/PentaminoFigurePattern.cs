using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentamino
{
    static class PentaminoFigurePattern
    {
        public static bool[,] GetF()
        {
            bool[,] F = new bool[3,3];
            F[0, 0] = false;
            F[0, 1] = true;
            F[0, 2] = true;
            F[1, 0] = true;
            F[1, 1] = true;
            F[1, 2] = false;
            F[2, 0] = false;
            F[2, 1] = true;
            F[2, 2] = false;
            return F;
        }

        public static bool[,] GetI()
        {
            bool[,] arr = new bool[5, 1];
            arr[0, 0] = true;
            arr[1, 0] = true;
            arr[2, 0] = true;
            arr[3, 0] = true;
            arr[4, 0] = true;
            return arr;
        }

        public static bool[,] GetL()
        {
            bool[,] arr = new bool[4, 2];
            arr[0, 0] = true;
            arr[0, 1] = false;
            arr[1, 0] = true;
            arr[1, 1] = false;
            arr[2, 0] = true;
            arr[2, 1] = false;
            arr[3, 0] = true;
            arr[3, 1] = true;
            return arr;
        }

        public static bool[,] GetN()
        {
            bool[,] arr = new bool[4, 2];
            arr[0, 0] = false;
            arr[0, 1] = true;
            arr[1, 0] = false;
            arr[1, 1] = true;
            arr[2, 0] = true;
            arr[2, 1] = true;
            arr[3, 0] = true;
            arr[3, 1] = false;
            return arr;
        }

        public static bool[,] GetP()
        {
            bool[,] arr = new bool[3, 2];
            arr[0, 0] = true;
            arr[0, 1] = true;
            arr[1, 0] = true;
            arr[1, 1] = true;
            arr[2, 0] = true;
            arr[2, 1] = false;
            return arr;
        }

        public static bool[,] GetT()
        {
            bool[,] arr = new bool[3, 3];
            arr[0, 0] = true;
            arr[0, 1] = true;
            arr[0, 2] = true;
            arr[1, 0] = false;
            arr[1, 1] = true;
            arr[1, 2] = false;
            arr[2, 0] = false;
            arr[2, 1] = true;
            arr[2, 2] = false;
            return arr;
        }

        public static bool[,] GetU()
        {
            bool[,] arr = new bool[2, 3];
            arr[0, 0] = true;
            arr[0, 1] = false;
            arr[0, 2] = true;
            arr[1, 0] = true;
            arr[1, 1] = true;
            arr[1, 2] = true;
            return arr;
        }

        public static bool[,] GetV()
        {
            bool[,] arr = new bool[3, 3];
            arr[0, 0] = false;
            arr[0, 1] = false;
            arr[0, 2] = true;
            arr[1, 0] = false;
            arr[1, 1] = false;
            arr[1, 2] = true;
            arr[2, 0] = true;
            arr[2, 1] = true;
            arr[2, 2] = true;
            return arr;
        }

        public static bool[,] GetW()
        {
            bool[,] arr = new bool[3, 3];
            arr[0, 0] = false;
            arr[0, 1] = false;
            arr[0, 2] = true;
            arr[1, 0] = false;
            arr[1, 1] = true;
            arr[1, 2] = true;
            arr[2, 0] = true;
            arr[2, 1] = true;
            arr[2, 2] = false;
            return arr;
        }

        public static bool[,] GetX()
        {
            bool[,] arr = new bool[3, 3];
            arr[0, 0] = false;
            arr[0, 1] = true;
            arr[0, 2] = false;
            arr[1, 0] = true;
            arr[1, 1] = true;
            arr[1, 2] = true;
            arr[2, 0] = false;
            arr[2, 1] = true;
            arr[2, 2] = false;
            return arr;
        }

        public static bool[,] GetY()
        {
            bool[,] arr = new bool[4, 2];
            arr[0, 0] = false;
            arr[0, 1] = true;
            arr[1, 0] = true;
            arr[1, 1] = true;
            arr[2, 0] = false;
            arr[2, 1] = true;
            arr[3, 0] = false;
            arr[3, 1] = true;
            return arr;
        }

        public static bool[,] GetZ()
        {
            bool[,] arr = new bool[3, 3];
            arr[0, 0] = true;
            arr[0, 1] = true;
            arr[0, 2] = false;
            arr[1, 0] = false;
            arr[1, 1] = true;
            arr[1, 2] = false;
            arr[2, 0] = false;
            arr[2, 1] = true;
            arr[2, 2] = true;
            return arr;
        }

        public static bool[,] GetArrayBySymbol(PentaminoSymbols symbol)
        {
            bool[,] arr = null;

            switch (symbol)
            {
                case (PentaminoSymbols.F): arr = GetF(); break;
                case (PentaminoSymbols.I): arr = GetI(); break;
                case (PentaminoSymbols.L): arr = GetL(); break;
                case (PentaminoSymbols.N): arr = GetN(); break;
                case (PentaminoSymbols.P): arr = GetP(); break;
                case (PentaminoSymbols.T): arr = GetT(); break;
                case (PentaminoSymbols.U): arr = GetU(); break;
                case (PentaminoSymbols.V): arr = GetV(); break;
                case (PentaminoSymbols.W): arr = GetW(); break;
                case (PentaminoSymbols.X): arr = GetX(); break;
                case (PentaminoSymbols.Y): arr = GetY(); break;
                case (PentaminoSymbols.Z): arr = GetZ(); break;
            }

            return arr;
        }

        public static PentaminoSymbols CharToPentaminoSymbol(char symbol)
        {
            switch (symbol)
            {
                case ('F'): return PentaminoSymbols.F;
                case ('I'): return PentaminoSymbols.I;
                case ('L'): return PentaminoSymbols.L;
                case ('N'): return PentaminoSymbols.N;
                case ('P'): return PentaminoSymbols.P;
                case ('T'): return PentaminoSymbols.T;
                case ('U'): return PentaminoSymbols.U;
                case ('V'): return PentaminoSymbols.V;
                case ('W'): return PentaminoSymbols.W;
                case ('X'): return PentaminoSymbols.X;
                case ('Y'): return PentaminoSymbols.Y;
                case ('Z'): return PentaminoSymbols.Z;
            }

            return 0;
        }
    }
}
