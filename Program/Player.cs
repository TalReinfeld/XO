using System;

namespace Ex02
{
    class Player
    {
        private readonly eSymbol m_PlayerSymbol;
        private readonly string m_PlayerName;
        private readonly int m_PlayerID;
        private int m_score;

        public Player(eSymbol i_PlayerSymbol, string i_PlayerName, int i_PlayerID)
        {
            m_PlayerSymbol = i_PlayerSymbol;
            m_PlayerName = i_PlayerName;
            m_PlayerID = i_PlayerID;
            m_score = 0;
        }

        public eSymbol Symbol
        {
            get { return m_PlayerSymbol; }
        }

        public string PlayerName
        {
            get { return m_PlayerName; }
        }

        public int Score
        {
            get { return m_score; }
            set { m_score = value; }
        }
    }
}
