using System;

namespace Ex02
{
    class Board
    {
        private readonly Cell[,] m_Board;
        private readonly int m_BoardSize;

        public Board(int i_BoardSize)
        {
            m_Board = new Cell[i_BoardSize, i_BoardSize];
            m_BoardSize = i_BoardSize;

            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    m_Board[i, j] = new Cell(i, j, (int)eSymbol.Empty);
                }
            }
        }

        public int BoardSize
        {
            get { return m_BoardSize; }
        }

    }
}
