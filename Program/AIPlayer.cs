using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    class AIPlayer
    {
        private Random m_random;
        private Player m_defult;
        private int m_BoardSize;
        public AIPlayer(int i_BoardSize)
        {
            m_BoardSize = i_BoardSize;
            m_random = new Random();
        }
        public Player defultPlayer
        {
            get { return m_defult; }
        }

        public Cell getNextMove()
        {
            int row = m_random.Next(m_BoardSize);
            int column = m_random.Next(m_BoardSize);
            Cell nextMove = new Cell(row, column, eSymbol.O);
            return nextMove;
        }

        static int Minimax(char[,] board, int depth, bool isMaximizingPlayer)
        {
            if (IsGameOver(board))
            {
                int score = EvaluateScore(board);
                return score;
            }

            if (isMaximizingPlayer)
            {
                int bestScore = int.MinValue;

                for (int row = 0; row < board.GetLength(0); row++)
                {
                    for (int col = 0; col < board.GetLength(1); col++)
                    {
                        if (board[row, col] == ' ')
                        {
                            board[row, col] = 'O';
                            int score = Minimax(board, depth + 1, false);
                            board[row, col] = ' ';

                            bestScore = Math.Max(score, bestScore);
                        }
                    }
                }

                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;

                for (int row = 0; row < board.GetLength(0); row++)
                {
                    for (int col = 0; col < board.GetLength(1); col++)
                    {
                        if (board[row, col] == ' ')
                        {
                            board[row, col] = 'X';
                            int score = Minimax(board, depth + 1, true);
                            board[row, col] = ' ';

                            bestScore = Math.Min(score, bestScore);
                        }
                    }
                }

                return bestScore;
            }
        }

        static int EvaluateScore(char[,] board)
        {
            if (IsWinning(board, 'O'))
            {
                return 1; // AI wins
            }

            if (IsWinning(board, 'X'))
            {
                return -1; // Player wins
            }

            return 0; // Draw
        }

        static bool IsWinning(char[,] board, char player)
        {
            int rows = board.GetLength(0);
            int cols = board.GetLength(1);

            // Check rows
            for (int row = 0; row < rows; row++)
            {
                bool win = true;
                for (int col = 0; col < cols; col++)
                {
                    if (board[row, col] != player)
                    {
                        win = false;
                        break;
                    }
                }
                if (win) return true;
            }

            // Check columns
            for (int col = 0; col < cols; col++)
            {
                bool win = true;
                for (int row = 0; row < rows; row++)
                {
                    if (board[row, col] != player)
                    {
                        win = false;
                        break;
                    }
                }
                if (win) return true;
            }

            // Check diagonals
            bool diagonalWin1 = true;
            bool diagonalWin2 = true;
            for (int i = 0; i < rows; i++)
            {
                if (board[i, i] != player)
                {
                    diagonalWin1 = false;
                }

                if (board[i, cols - 1 - i] != player)
                {
                    diagonalWin2 = false;
                }
            }

            if (diagonalWin1 || diagonalWin2)
            {
                return true;
            }

            return false;
        }

        public int FindBestMove(char[] board)
        { 
            int bestScore = int.MinValue;
            int bestMove = -1;
            for (int i = 0; i < board.Length; i++) 
            { 
                if (board[i] == '-') 
                {
                    board[i] = playerChar;
                    int score = AlphaBeta(board, 0, int.MinValue, int.MaxValue, false);
                    board[i] = '-';
                    if (score > bestScore)
                    { 
                        bestScore = score; bestMove = i; 
                    }
                } 
            } 
            return bestMove; 
        }
        private int AlphaBeta(Board board, int depth, int alpha, int beta, bool isMaximizingPlayer) 
        { 
            int score = EvaluateBoard(board);
            if (score != 0)
                return score;
            if (!IsMovesLeft(board))
                return 0;
            if (isMaximizingPlayer)
            {
                int bestScore = int.MinValue;
                for (int i = 0; i < board.BoardSize; i++)
                {
                    for (int j = 0; j < board.BoardSize; j++)
                    {
                        if (board[i,j] == eSymbol.Empty)
                        {
                            board[i, j] = eSymbol.O;
                            bestScore = Math.Max(bestScore, AlphaBeta(board, depth + 1, alpha, beta, !isMaximizingPlayer));
                            alpha = Math.Max(alpha, bestScore);
                            board[i, j] = eSymbol.Empty;
                            if (beta <= alpha)
                                break;
                        }
                    }
                } 
                return bestScore;
            } 
            else
            { 
                int bestScore = int.MaxValue;
                for (int i = 0; i < board.BoardSize; i++)
                {
                    for (int j = 0; j < board.BoardSize; j++)
                    {
                        if (board[i, j] == eSymbol.Empty)
                        {
                            board[i, j] = eSymbol.X;
                            bestScore = Math.Min(bestScore, AlphaBeta(board, depth + 1, alpha, beta, !isMaximizingPlayer));
                            beta = Math.Min(beta, bestScore);
                            board[i, j] = eSymbol.Empty;
                            if (beta <= alpha)
                                break;
                        }
                    }
                } 
                return bestScore;
            }
        }
        private bool IsMovesLeft(Board i_GameBoard)
        {
            bool foundEmptyCell = false;
            for (int i = 0; i < i_GameBoard.BoardSize; i++)
            {
                for (int j = 0; j < i_GameBoard.BoardSize; j++)
                {
                    if(i_GameBoard[i, j] == eSymbol.Empty)
                    {
                        foundEmptyCell = true;
                    }
                }
            }

            return foundEmptyCell;
        }


    }
}
