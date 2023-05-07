using System;

namespace Ex02
{
    class Cell
    {
        private int m_Row;
        private int m_Column;
        private eSymbol m_Symbol;

        public Cell(int i_Row, int i_Column, eSymbol i_Symbol)
        {
            this.m_Row = i_Row;
            this.m_Column = i_Column;
            this.m_Symbol = i_Symbol;
        }

        public int Row
        {
            get { return m_Row; }
            set { m_Row = value; }
        }

        public int Column
        {
            get { return this.m_Column; }
            set { this.m_Column = value; }
        }

        public eSymbol SymbolCell
        {
            get { return m_Symbol; }
            set { m_Symbol = value; }
        }

    }
 
}
