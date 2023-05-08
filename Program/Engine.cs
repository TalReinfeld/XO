using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    class Engine
    {
        private Board m_Board = null;
        private Player m_FirstPlayer = null;
        private Player m_SecondPlayer = null;

        public Engine(Board i_Board, Player i_FirstPlayer, Player i_SecondPlayer)
        {
            m_Board = i_Board;
            m_FirstPlayer = i_FirstPlayer;
            m_SecondPlayer = i_SecondPlayer;
        }

        internal bool gameOver(eSymbol i_currentPlayerSymbol)
        {
            bool gameOver = false;
            gameOver = checkRowOrColumnSequence(i_currentPlayerSymbol) || checkForDiagonalsSequence(i_currentPlayerSymbol);
            
            return gameOver;
        }

        private bool checkRowOrColumnSequence(eSymbol i_PlayerSymbol)
        {
            bool hasSequence = false;
            for (int row = 0; row < m_Board.BoardSize; row++)
            {
                int rowCount = 0;
                int columnCount = 0;
                for (int column = 0; column < m_Board.BoardSize; column++)
                {
                    if (m_Board[row, column] == i_PlayerSymbol)
                    {
                        rowCount++;
                    }
                    if (m_Board[column, row] == i_PlayerSymbol)
                    {
                        columnCount++;
                    }
                }
                if (rowCount == m_Board.BoardSize || columnCount == m_Board.BoardSize)
                {
                    hasSequence = true;
                }
            }

            return hasSequence;
        }

        private bool checkForDiagonalsSequence(eSymbol i_PlayerSymbol)
        {
            bool hasSequence = false;
            int leftDiagonal = 0, rightDiagonal = 0;
            for (int i = 0; i < m_Board.BoardSize; i++)
            {
                if (board[i, i] == i_PlayerSymbol)
                {
                    leftDiagonal++;
                }
                if (board[i, m_Board.BoardSize - 1 - i] == i_PlayerSymbol)
                {
                    rightDiagonal++;
                }
            }
            if (leftDiagonal == 9 || rightDiagonal == 9)
            {
                hasSequence = true; // player has won
            }

            return hasSequence;
        }

        public int boardSize
        {
            get { return m_Board.BoardSize; }
        }

        public Board board
        {
            get { return m_Board; }
        }

        public void updateCell(Cell i_cellToUpdate)
        {
            m_Board[i_cellToUpdate.Row, i_cellToUpdate.Column] = i_cellToUpdate.SymbolCell;
        }

        public Player firstPlayer
        {
            get { return m_FirstPlayer; }
        }

        public Player secondPlayer
        {
            get { return m_SecondPlayer; }
        }
    }
}
