using System;
/**********************************ALI GHOLAMI**************************************/
namespace Sudoku
{
    class Program
    {
        static char[][] LoadPuzzle()
        {
            char[][] puzzle = new char[9][];
            Console.WriteLine("Enter Sudoku elemens rows by rows:");
            for (int i = 0; i < 9; i++)
            {
                string line = Console.ReadLine();
                puzzle[i] = line.PadRight(9).Substring(0, 9).ToCharArray();
                for (int j = 0; j < 9; j++)
                    if (puzzle[i][j] < '0' || puzzle[i][j] > '9')
                        puzzle[i][j] = '0';
            }
            return puzzle;
        }
        static void PrintTable(char[][] puzzle, int stepsCount)
        {
            Console.WriteLine();
            Console.WriteLine("Solved table after {0} steps:", stepsCount);
            for (int i = 0; i < 9; i++)
            Console.WriteLine("{0}", new string(puzzle[i]));
        }
        static char[] GetCandidates(char[][] puzzle, int row, int col)
        {
            string s = "";
            for (char c = '1'; c <= '9'; c++)
            {
                bool collision = false;
                for (int i = 0; i < 9; i++)
                {
                    if (puzzle[row][i] == c ||
                        puzzle[i][col] == c ||
                        puzzle[(row - row % 3) + i / 3][(col - col % 3) + i % 3] == c)
                    {
                        collision = true;
                        break;
                    }
                }
                if (!collision)
                    s += c;
            }
            return s.ToCharArray();
        }
        static bool Solve(char[][] puzzle, ref int stepsCount)
        {
            bool solved = false;
            int row = -1;
            int col = -1;
            char[] candidates = null;

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (puzzle[i][j] == '0')
                    {
                        char[] newCandidates = GetCandidates(puzzle, i, j);
                        if (row < 0 || newCandidates.Length < candidates.Length)
                        {
                            row = i;
                            col = j;
                            candidates = newCandidates;
                        }
                    }
            if (row < 0)
            {
                solved = true;
            }
            else
            {
                for (int i = 0; i < candidates.Length; i++)
                {
                    puzzle[row][col] = candidates[i];
                    stepsCount++;
                    if (Solve(puzzle, ref stepsCount))
                    {
                        solved = true;
                        break;
                    }
                    puzzle[row][col] = '0';
                }
            }
            return solved;
        }

        static void Main()
        {
        start:
            while (true)
            {
                
                char[][] puzzle = LoadPuzzle();
                int stepsCount = 0;
                if (Solve(puzzle, ref stepsCount))
                    PrintTable(puzzle, stepsCount);
                else
                    Console.WriteLine("this sudoku is wrong!");

                Console.WriteLine();
                Console.Write("do you want to try again, yes or no? ");
                if (Console.ReadLine().ToLower() == "yes")
                goto start;
                else break;
            }
        }
    }
}// منتظر نمرات خوب شما هستیم با تشکر از زحمات شما:)