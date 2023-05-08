using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex02.ConsoleUtils;

namespace Ex02
{
    class UI
    {

        public static void RunGame()
        {

            int boardSize = getBoardSize();
            Board checkBoard = new Board(boardSize);
            printBoard(checkBoard);
            Player firstPlayer = new Player(eSymbol.X, "David", 1);
            //פונקציה שאומרת אם מחשב או בנאדם
            //choooseOpponent()
            Player secondPlayer = new Player(eSymbol.O, "Tal", 2);

            Engine gameEngine = new Engine(checkBoard, firstPlayer, secondPlayer);
            do
            {
                runRound(gameEngine);
            } while (continueToPlay());


        }

        private static void runRound(Engine i_GameEngine)
        {
            Player currentPlayer = i_GameEngine.secondPlayer;
            Player nextPlayer = i_GameEngine.firstPlayer;
            while (!i_GameEngine.gameOver(currentPlayer.Symbol))
            {
                updateCurrentPlayer(ref currentPlayer, ref nextPlayer);
                turn(i_GameEngine, currentPlayer.Symbol);
            }
        }

        private static bool continueToPlay()
        {
            int continueToPlay = 0;

            do
            {
                Console.WriteLine("Please enter '1' if you want to continue playing otherwise enter '2':");
                int.TryParse(Console.ReadLine(), out continueToPlay);
            }
            while (continueToPlay != 1 && continueToPlay != 2);

            return continueToPlay == 1 ? true : false;
        }

        private static void turn(Engine i_gameEngine, eSymbol i_playerSymbol)
        {
            Cell nextCellToDraw = getPlayerInputCell(i_gameEngine, i_playerSymbol);
            i_gameEngine.updateCell(nextCellToDraw);
            ConsoleUtils.Screen.Clear();
            printBoard(i_gameEngine.board);
        }

        private static Cell getPlayerInputCell(Engine i_gameEngine, eSymbol i_CurrSymbol)
        {
            int row = 0, column = 0;
            Console.WriteLine(string.Empty);
            do
            {
                Console.WriteLine($"Please enter an integer value between 1 to {i_gameEngine.boardSize} for your row:");
                int.TryParse(Console.ReadLine(), out row);
                Console.WriteLine($"Please enter an integer value between 1 to {i_gameEngine.boardSize} for your column:");
                int.TryParse(Console.ReadLine(), out column);
            }
            while (!cellInsideBoard(row, column, i_gameEngine.boardSize) || i_gameEngine.board[row - 1, column - 1] != eSymbol.Empty);

            return new Cell(row - 1, column - 1, i_CurrSymbol);
        }

        private static bool cellInsideBoard(int i_Row, int i_Col, int i_BoardSize)
        {
            return ((i_Row > 0 && (i_Row <= i_BoardSize)) && (i_Col > 0 && (i_Col <= i_BoardSize))) ? true : false;
        }

        private static void updateCurrentPlayer(ref Player i_currentPlayer, ref Player i_nextPlayer)
        {
            Player temporaryPlayer = i_currentPlayer;
            i_currentPlayer = i_nextPlayer;
            i_nextPlayer = temporaryPlayer;
        }

        private static int getBoardSize()
        {
            int boarsSize = 0;

            do
            {
                Console.WriteLine("Please enter an integer value between 3 to 9 for the size of the board:");
                int.TryParse(Console.ReadLine(), out boarsSize);
            }
            while (boarsSize < 3 || boarsSize > 9);

            return boarsSize;
        }

        private static eOpponent choooseOpponent()
        {
            int opponent = 0;

            do
            {
                Console.WriteLine(string.Format(
@"Please choose ypur opponent:
1. Computer
2. 2 Players"));
                int.TryParse(Console.ReadLine(), out opponent);
            }
            while (opponent != 1 && opponent != 2);

            return (eOpponent)opponent;
        }

        private enum eOpponent
        {
            Computer = 1,
            Person = 2,
        }

        private static void printBoard(Board i_Board)
        {
            //Screen.Clear();
            Console.Write(" ");
            for (int i = 0; i < i_Board.BoardSize; i++)
            {
                Console.Write(" {0}  ", (char)(i + '1'));
            }

            printLineOfSpaces(i_Board.BoardSize);
            for (int i = 0; i < i_Board.BoardSize; i++)
            {
                Console.WriteLine(string.Empty);
                Console.Write("{0}|", (char)(i + '1'));
                for (int j = 0; j < i_Board.BoardSize; j++)
                {
                    printSymbol(i_Board[i, j]);
                }

                printLineOfSpaces(i_Board.BoardSize);
            }
        }

        private static void printSymbol(eSymbol i_Symbol)
        {
            char symbolToPrint;
            Console.Write(" ");
            if(i_Symbol == eSymbol.Empty)
            {
                symbolToPrint = ' ';
            }
            else if(i_Symbol == eSymbol.X)
            {
                symbolToPrint = 'X';
            }
            else
            {
                symbolToPrint = 'O';
            }
            Console.Write(symbolToPrint);
            Console.Write(" |");
        }

        private static void printLineOfSpaces(int i_BoardSize)
        {
            Console.WriteLine(string.Empty);
            Console.Write(" =");
            for (int i = 0; i < i_BoardSize; i++)
            {
                Console.Write("====");
            }
        }
    }
}
