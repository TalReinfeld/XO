using System;
using Ex02.ConsoleUtils;

namespace Ex02
{
    class UI
    {
        const int k_IsComputer = 1;
        const int k_FirstPlayer = 1;
        const int k_SecondPlayer = 2;
        const int k_ContinueToPlay = 1;
        const int k_LeaveGame = 2;
        public static void RunGame()
        {
            int boardSize = getBoardSize();
            bool createAIPlayer = false;
            string secondUserName = "Default Player";
            createAIPlayer = ChooseOpponent();
            Board checkBoard = new Board(boardSize);
            Player firstPlayer = new Player(eSymbol.X, getUserName(k_FirstPlayer), k_FirstPlayer);
            if(!createAIPlayer)
            {
                secondUserName = getUserName(k_SecondPlayer);
                Screen.Clear();
            }
            Player secondPlayer = new Player(eSymbol.O, secondUserName, k_SecondPlayer);
            printBoard(checkBoard);
            Engine gameEngine = new Engine(checkBoard, firstPlayer, secondPlayer, createAIPlayer);
            eSymbol o_WinnerShape;
            do
            {
                runRound(gameEngine, out o_WinnerShape);
                printEndGame(o_WinnerShape);
                printScores(gameEngine.firstPlayer.Score, gameEngine.secondPlayer.Score);
            } while (continueToPlay(gameEngine));

        }

        public static string getUserName(int i_PlayerNumber)
        {
            string userName;
            Console.WriteLine($"Please enter User #{i_PlayerNumber} name:");
            userName = Console.ReadLine();
            Screen.Clear();
            return userName;
        }

        public static void printEndGame(eSymbol i_WinnerShape)
        {
            Console.WriteLine(string.Empty);
            if (i_WinnerShape != eSymbol.Empty)
            {
                int winnerPlayerId;
                if(i_WinnerShape != eSymbol.X)
                {
                    winnerPlayerId = k_FirstPlayer;
                }
                else
                {
                    winnerPlayerId = k_SecondPlayer;
                }
                Console.WriteLine($"Player #{winnerPlayerId} won!");
            }
            else
            {
                Console.WriteLine("There was a Tie!");
            }
        }

        private static void runRound(Engine i_GameEngine, out eSymbol o_WinnerShape)
        {
            Player currentPlayer = i_GameEngine.secondPlayer;
            Player nextPlayer = i_GameEngine.firstPlayer;
            do
            {
                i_GameEngine.updateCurrentPlayer(ref currentPlayer, ref nextPlayer);
                turn(i_GameEngine, currentPlayer.Symbol);
            } while (!i_GameEngine.isGameOver(currentPlayer.Symbol, out o_WinnerShape));
        }

        private static bool continueToPlay(Engine i_GameEngine)
        {
            int continueToPlay = 0;
            Console.WriteLine(string.Empty);
            do
            {
                Console.WriteLine($"Please enter '{k_ContinueToPlay}' if you want to continue playing otherwise enter '{k_LeaveGame}':");
                int.TryParse(Console.ReadLine(), out continueToPlay);
            }
            while (continueToPlay != k_ContinueToPlay && continueToPlay != k_LeaveGame);
            Screen.Clear();

            if(continueToPlay == k_ContinueToPlay)
            {
                i_GameEngine.GameOver = false;
                printBoard(i_GameEngine.board);
            }

            return continueToPlay == 1 ? true : false;
        }

        private static void printScores(int i_FirstPlayerScore, int i_SecondPlayerScore)
        {
            Console.WriteLine($"Player #{k_FirstPlayer} - {i_FirstPlayerScore}  |  Player #{k_SecondPlayer} - {i_SecondPlayerScore}");
        }

        private static void turn(Engine i_GameEngine, eSymbol i_PlayerSymbol)
        {
            Cell nextCellToDraw = i_GameEngine.getMove();
            if(nextCellToDraw == null)
            {
                nextCellToDraw = getPlayerInputCell(i_GameEngine, i_PlayerSymbol);
            }

            if(nextCellToDraw != null)
            {
                i_GameEngine.updateCell(nextCellToDraw);
                ConsoleUtils.Screen.Clear();
                printBoard(i_GameEngine.board);
            }
        }

        private static Cell getPlayerInputCell(Engine i_GameEngine, eSymbol i_CurrSymbol)
        {
            int row = 0, column = 0;
            Cell createdCell = null;
            Console.WriteLine(string.Empty);
            do
            {
                row = getUserInput(i_GameEngine);
                if(i_GameEngine.GameOver)
                {
                    break;
                }
                column = getUserInput(i_GameEngine);
                if (i_GameEngine.GameOver)
                {
                    break;
                }

            }
            while (!i_GameEngine.cellInsideBoard(row, column, i_GameEngine.boardSize) || !i_GameEngine.checkIfLegalMove(row - 1, column - 1));

            if (!i_GameEngine.GameOver)
            {
                createdCell = new Cell(row - 1, column - 1, i_CurrSymbol);
            }
            
            return createdCell;
        }

        private static int getUserInput(Engine i_GameEngine)
        {
            string input;
            int inputNumber = 0;
            do
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine($"Please enter an integer value between 1 to {i_GameEngine.boardSize} or 'Q' to quit:");
                input = Console.ReadLine();
            } while (input != "Q" && input != "q" && !int.TryParse(input, out inputNumber));

            if (input == "Q" || input == "q")
            {
                i_GameEngine.GameOver = true;
            }

            return inputNumber;
        }

     
        public static int getBoardSize()
        {
            int boarsSize = 0;

            do
            {
                Console.WriteLine("Please enter an integer value between 3 to 9 for the size of the board:");
                int.TryParse(Console.ReadLine(), out boarsSize);
            }
            while (boarsSize < 3 || boarsSize > 9);
            Screen.Clear();

            return boarsSize;
        }

        public static bool ChooseOpponent()
        {
            int opponent = 0;

            do
            {
                Console.WriteLine(string.Format(
@"Please choose ypur opponent:
1. Computer
2. 2 Players
"));
                int.TryParse(Console.ReadLine(), out opponent);
            }
            while (opponent != 1 && opponent != 2);
            Screen.Clear();

            return (opponent == k_IsComputer ? true : false);
        }

        private static void printBoard(Board i_Board)
        {
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
