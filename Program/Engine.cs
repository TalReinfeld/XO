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
        private AIPlayer m_AIPlayer = null;
        private bool m_GameOver = false;
        private int m_AmountOfMoves = 0;
        private bool m_IsAIPlayer;

        public Engine(Board i_Board, Player i_FirstPlayer, Player i_SecondPlayer, bool i_IsAIPlayer)
        {
            setEngine(i_Board, i_FirstPlayer, i_SecondPlayer, i_IsAIPlayer);
        }

        public void setEngine(Board i_Board, Player i_FirstPlayer, Player i_SecondPlayer, bool i_IsAIPlayer)
        {
            m_IsAIPlayer = i_IsAIPlayer;
            m_Board = i_Board;
            m_FirstPlayer = i_FirstPlayer;
            if(m_IsAIPlayer)
            {
                m_AIPlayer = new AIPlayer(i_Board.BoardSize);
            }
            m_SecondPlayer = i_SecondPlayer;
        }

        public Cell getMove()
        {
            Cell nextMove = null;
            if(m_IsAIPlayer && (m_AmountOfMoves % 2 == 1))
            {
                do
                {
                    nextMove = m_AIPlayer.getNextMove();
                }
                while (!checkIfLegalMove(nextMove.Row, nextMove.Column));
            }

            return nextMove;
        }

        public bool checkIfLegalMove(int i_Row, int i_Column)
        {
            bool isLegalMove = true;
            if(board[i_Row, i_Column] != eSymbol.Empty)
            {
                isLegalMove = false;
            }

            return isLegalMove;
        }

        internal bool isGameOver(eSymbol i_currentPlayerSymbol, out eSymbol o_WinnerShape)
        {
            o_WinnerShape = i_currentPlayerSymbol;
            
            m_GameOver = checkRowOrColumnSequence(i_currentPlayerSymbol) || checkForDiagonalsSequence(i_currentPlayerSymbol) || m_GameOver;
            if(!m_GameOver)
            {
                m_GameOver = checkIfTie();
                o_WinnerShape = eSymbol.Empty;
            }
            
            if(m_GameOver)
            {
                increaseScore(o_WinnerShape);
                clearAmountOfMoves();
                this.board.CreateNewBoard(this.boardSize);
            }
            return m_GameOver;
        }

        internal void increaseScore(eSymbol i_LoserShape)
        {
            if(i_LoserShape == eSymbol.O)
            {
                this.firstPlayer.Score++;
            }
            else
            {
                this.secondPlayer.Score++;
            }
        }

        private int Moves
        {
            get { return m_AmountOfMoves; }
            set { m_AmountOfMoves = value; }
        }

        internal bool GameOver
        {
            get { return m_GameOver; }
            set { m_GameOver = value; }
        }

        public void increaseAmountOfMoves()
        {
            this.Moves++;
        }

        public void clearAmountOfMoves()
        {
            this.Moves = 0;
        }

        public bool checkIfTie()
        {
            return this.Moves == (this.boardSize * this.boardSize) ? true : false;
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
            if (leftDiagonal == m_Board.BoardSize || rightDiagonal == m_Board.BoardSize)
            {
                hasSequence = true;
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
            increaseAmountOfMoves();
        }

        public Player firstPlayer
        {
            get { return m_FirstPlayer; }
        }

        public Player secondPlayer
        {
            get { return m_SecondPlayer; }
        }

        internal void updateCurrentPlayer(ref Player i_currentPlayer, ref Player i_nextPlayer)
        {
            Player temporaryPlayer = i_currentPlayer;
            i_currentPlayer = i_nextPlayer;
            i_nextPlayer = temporaryPlayer;
        }

        internal bool cellInsideBoard(int i_Row, int i_Col, int i_BoardSize)
        {
            return ((i_Row > 0 && (i_Row <= i_BoardSize)) && (i_Col > 0 && (i_Col <= i_BoardSize))) ? true : false;
        }
    }
}
