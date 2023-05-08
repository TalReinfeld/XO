using System;

namespace Ex02
{
    class Board
    {
        private Cell[,] m_Board = null;
        private int m_BoardSize = 0;

        public Board(int i_BoardSize)
        {
            CreateNewBoard(i_BoardSize);
        }

        public void CreateNewBoard(int i_BoardSize)
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

        public eSymbol this[int i_Row, int i_Col]
        {
            get { return m_Board[i_Row, i_Col].SymbolCell; }
            set { m_Board[i_Row, i_Col].SymbolCell = value; }
        }

    }
}
