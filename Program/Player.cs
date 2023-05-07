using System;

namespace Ex02
{
    class Player
    {
        private readonly eSymbol m_PlayerSymbol;
        private readonly string m_PlayerName;

        public Player(eSymbol i_PlayerSymbol, string i_PlayerName)
        {
            m_PlayerSymbol = i_PlayerSymbol;
            m_PlayerName = i_PlayerName;
        }
    }
}
