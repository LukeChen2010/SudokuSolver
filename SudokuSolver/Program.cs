using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] boardArr = { 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 6, 0, 0, 0, 0, 0, 0, 7, 0, 0, 9, 0, 2, 0, 0, 0, 5, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 4, 5, 7, 0, 0, 0, 0, 0, 1, 0, 0, 0, 3, 0, 0, 0, 1, 0, 0, 0, 0, 6, 8, 0, 0, 8, 5, 0, 0, 0, 1, 0, 0, 9, 0, 0, 0, 0, 4, 0, 0 };
            Board myBoard = new Board(boardArr);
            myBoard.PrintBoard();
            myBoard.SolveBoard();
            myBoard.PrintBoard();
            Console.ReadKey();
        }
    }

    class Board
    {
        public int[] boardArr { get; set; }

        public Board(int[] newBoard)
        {
            boardArr = newBoard;
        }

        private bool ComboValid(int a, int b, int c, int d, int e, int f, int g, int h, int i)
        {
            int[] combo = { boardArr[a], boardArr[b], boardArr[c], boardArr[d], boardArr[e], boardArr[f], boardArr[g], boardArr[h], boardArr[i] };

            for (int m = 1; m < 10; m++)
            {
                IEnumerable<int> numQuery = combo.Where(num => num == m);

                if (numQuery.Count() > 1)
                {
                    return false;
                }
            }

            return true;
        }

        public bool BoardValid()
        {
            for (int i = 0; i < 9; i++)
            {
                if (!ComboValid(0 + i * 9, 1 + i * 9, 2 + i * 9, 3 + i * 9, 4 + i * 9, 5 + i * 9, 6 + i * 9, 7 + i * 9, 8 + i * 9))
                {
                    return false;
                }

                if (!ComboValid(0 + i, 9 + i, 18 + i, 27 + i, 36 + i, 45 + i, 54 + i, 63 + i, 72 + i))
                {
                    return false;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!ComboValid(0 + 27 * i + 3 * j, 1 + 27 * i + 3 * j, 2 + 27 * i + 3 * j, 9 + 27 * i + 3 * j, 10 + 27 * i + 3 * j, 11 + 27 * i + 3 * j, 18 + 27 * i + 3 * j, 19 + 27 * i + 3 * j, 20 + 27 * i + 3 * j))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool BoardFull()
        {
            IEnumerable<int> numQuery = boardArr.Where(num => num == 0);

            if (numQuery.Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool BoardSolved()
        {
            if(BoardValid() && BoardFull())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void PrintBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine(boardArr[0 + i * 9] + " " + boardArr[1 + i * 9] + " " + boardArr[2 + i * 9] + "|" + 
                    boardArr[3 + i * 9] + " " + boardArr[4 + i * 9] + " " + boardArr[5 + i * 9] + "|" + 
                    boardArr[6 + i * 9] + " " + boardArr[7 + i * 9] + " " + boardArr[8 + i * 9]);
            }
        }

        public void SolveBoard()
        {
            RecurseSolveBoard(this, 0);
        }

        private void RecurseSolveBoard(Board myBoard, int start)
        {
            if (!myBoard.BoardValid())
            {
                myBoard = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                return;
            }

            myBoard.PrintBoard();
            Console.WriteLine();

            if (myBoard.BoardSolved())
            {
                boardArr = myBoard.boardArr;
                return;
            }

            for (int i = start; i < 81; i++)
            {
               if (myBoard.boardArr[i] == 0)
               {
                    for (int j = 1; j < 10; j++)
                    {
                        Board newBoard = new Board((int[])myBoard.boardArr.Clone());
                        newBoard.boardArr[i] = j;
                        RecurseSolveBoard(newBoard, i);                       
                    }
                    break;
               }
            }
        }
    }
}
