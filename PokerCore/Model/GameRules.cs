using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model
{
    public class GameRules
    {
        public GameRules(int maxPlayers)
        {
            _maxPlayers = maxPlayers;
        }
        string _rulesHelp => System.IO.File.ReadAllText(@"PokerCore\\resources\\Rules.txt", Encoding.Default).Replace("\n", " ");
        public string RulesHelp { get => _rulesHelp; }

        int _maxPlayers;
        public int MaxPlayers { get => _maxPlayers; }
    }
}
