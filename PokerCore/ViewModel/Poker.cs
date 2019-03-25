using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using ReactiveUI;
using System.Linq;
using PokerCore.Model;
//using PokerCore.Model.DataBase;
using System.ComponentModel;

namespace PokerCore.ViewModel
{
    public class Poker : ReactiveObject
    {
        public Dictionary<int, Player> _players;
        GameRules _gameRules;
        //List<Card> _boardCards;
        //List<(Card, Visibility)> _boardCards;
        (Card, Visibility)[] _boardCards;
        CardDeck _cardDeck;
        int _curPlayer;
        int _dealer;
        readonly int _smallBlind;
        readonly int _bigBlind;
        int _curBet;
        int _curRaise;
        int _allBank;
        //bool _gameStart = true;
        List<(int, int)> _dividedBanks;

        public Poker(string name, int cash, int smallBlind, int bigBlind)
        {
            _players = new Dictionary<int, Player>();
            _gameRules = new GameRules(10);
            _boardCards = new (Card, Visibility)[5];
            _dividedBanks = new List<(int, int)>();
            _cardDeck = new CardDeck();

            Player player = new Player(name, cash, this);

            _cardDeck.Shuffle();
            for (int i = 0; i < 5; i++)
            {
                _boardCards[i].Item1 = null;
                _boardCards[i].Item2 = Visibility.Invisible;
            }

            //AI Bot = new AI("bot", 1000, this);
            //_players.Add(1, Bot);
            //AI Bot1 = new AI("bot", 1000, this);
            //_players.Add(2, Bot1);
            _players.Add(0, player);
            _dealer = 0;
            _bigBlind = bigBlind;
            _smallBlind = smallBlind;
        }

        public void GameStart()
        {
            _cardDeck = new CardDeck();
            _cardDeck.Shuffle();
            for (int i = 0; i < 5; i++)
            {
                _boardCards[i].Item1 = _cardDeck.TakeCard();
                _boardCards[i].Item2 = Visibility.Invisible;
            }

            for ( int i = 0; i < _players.Count; i++)
            {
                (Card, Card) playerCards = (_cardDeck.TakeCard(), _cardDeck.TakeCard());
                _players[i].MyState.HandCards = playerCards;
                _players[i].MyState.State = PlayerGameState.In;
                HandCards.Add((i, playerCards));

                // set visibility of player cards
                for (int j = 0; j < i; j++)
                    _players[i].MyState.PlayersCardVisibility[j] = "Hidden";
                _players[i].MyState.PlayersCardVisibility[i] = "Visible";
                for (int j = i + 1; j < _players.Count; j++)
                    _players[i].MyState.PlayersCardVisibility[j] = "Hidden";
                for (int j = _players.Count; j < 10; j++)
                    _players[i].MyState.PlayersCardVisibility[j] = "Collapsed";
            }

            _allBank = 0;
            _curBet = 0;
            _curRaise = 0;

            _curPlayer = TakeNextKey(_dealer);
            _players[_curPlayer].Bet(_smallBlind);
            _curPlayer = TakeNextKey(_curPlayer);
            _players[_curPlayer].Raise(_bigBlind - _smallBlind);
            _curPlayer = TakeNextKey(_curPlayer);
            _curRaise = _bigBlind;
            //if (_gameStart)
                while (_curPlayer != 0)
                {
                    BotAction();
                    _curPlayer = TakeNextKey(_curPlayer);
                }
            //_gameStart = false;
            this.RaisePropertyChanged("PlayersCardVisibility");
        }

        #region Игроки
        public PlayerState player0
        { get => _players.ContainsKey(0) ? _players[0].MyState : null; }

        public string player0vis
        { get => _players.ContainsKey(0)?"Visible":"Hidden"; }
        public string player0connected
        { get => _players.ContainsKey(0) ? "False" : "True"; }
        public string player0connectedvis
        { get => _players.ContainsKey(0) ? "Hidden" : "Visible"; }

        public PlayerState player1
        { get => _players.ContainsKey(1)?_players[1].MyState:null; }
        public string player1vis
        { get => _players.ContainsKey(1)?"Visible":"Hidden"; }
        public string player1connected
        { get => _players.ContainsKey(1) ? "False" : "True"; }
        public string player1connectedvis
        { get => _players.ContainsKey(1) ? "Hidden" : "Visible"; }

        public PlayerState player2
        { get => _players.ContainsKey(2) ? _players[2].MyState : null; }
        public string player2vis
        { get => _players.ContainsKey(2)?"Visible":"Hidden"; }
        public string player2connected
        { get => _players.ContainsKey(2) ? "False" : "True"; }
        public string player2connectedvis
        { get => _players.ContainsKey(2) ? "Hidden" : "Visible"; }

        public PlayerState player3
        { get => _players.ContainsKey(3) ? _players[3].MyState : null; }
        public string player3vis
        { get => _players.ContainsKey(3)?"Visible":"Hidden"; }
        public string player3connected
        { get => _players.ContainsKey(3) ? "False" : "True"; }
        public string player3connectedvis
        { get => _players.ContainsKey(3) ? "Hidden" : "Visible"; }

        public PlayerState player4
        { get => _players.ContainsKey(4) ? _players[4].MyState : null; }
        public string player4vis
        { get => _players.ContainsKey(4)?"Visible":"Hidden"; }
        public string player4connected
        { get => _players.ContainsKey(4) ? "False" : "True"; }
        public string player4connectedvis
        { get => _players.ContainsKey(4) ? "Hidden" : "Visible"; }

        public PlayerState player5
        { get => _players.ContainsKey(5) ? _players[5].MyState : null; }
        public string player5vis
        { get => _players.ContainsKey(5)?"Visible":"Hidden"; }
        public string player5connected
        { get => _players.ContainsKey(5) ? "False" : "True"; }
        public string player5connectedvis
        { get => _players.ContainsKey(5) ? "Hidden" : "Visible"; }

        public PlayerState player6
        { get => _players.ContainsKey(6) ? _players[6].MyState : null; }
        public string player6vis
        { get => _players.ContainsKey(6)?"Visible":"Hidden"; }
        public string player6connected
        { get => _players.ContainsKey(6) ? "False" : "True"; }
        public string player6connectedvis
        { get => _players.ContainsKey(6) ? "Hidden" : "Visible"; }

        public PlayerState player7
        { get => _players.ContainsKey(7) ? _players[7].MyState : null; }
        public string player7vis
        { get => _players.ContainsKey(7)?"Visible":"Hidden"; }
        public string player7connected
        { get => _players.ContainsKey(7) ? "False" : "True"; }
        public string player7connectedvis
        { get => _players.ContainsKey(7) ? "Hidden" : "Visible"; }

        public PlayerState player8
        { get => _players.ContainsKey(8) ? _players[8].MyState : null; }
        public string player8vis
        { get => _players.ContainsKey(8)?"Visible":"Hidden"; }
        public string player8connected
        { get => _players.ContainsKey(8) ? "False" : "True"; }
        public string player8connectedvis
        { get => _players.ContainsKey(8) ? "Hidden" : "Visible"; }

        public PlayerState player9
        { get => _players.ContainsKey(9) ? _players[9].MyState : null; }
        public string player9vis
        { get => _players.ContainsKey(9)?"Visible":"Hidden"; }
        public string player9connected
        { get => _players.ContainsKey(9) ? "False" : "True"; }
        public string player9connectedvis
        { get => _players.ContainsKey(9) ? "Hidden" : "Visible"; }
        #endregion

        #region Поля стола
        public int Dealer { get => _dealer; }

        public int CurPlayer { get => _curPlayer; }

        public int SmallBlind { get => _smallBlind; }

        public int BigBlind { get => _bigBlind; }

        public int CurrentRaise { get => _curRaise; set => this.RaiseAndSetIfChanged(ref _curRaise, value); }

        public int CurrentBet { get => _curBet; set => this.RaiseAndSetIfChanged(ref _curBet, value); }

        public int AllBank { get => _allBank; set => this.RaiseAndSetIfChanged(ref _allBank, value); }

        public List<(int, int)> DividedBanks { get => _dividedBanks; set => this.RaiseAndSetIfChanged(ref _dividedBanks, value); }

        public CardDeck Deck { get => _cardDeck; }

        public GameRules Rules { get => _gameRules; }

        public Dictionary<int, Player> Players { get => _players; }

        public string[] DealerChip
        {
            get
            {
                string[] ans = new string[10];
                for (int i = 0; i < 10; i++)
                    ans[i] = "Hidden";
                ans[_dealer] = "Visible";
                return ans;
            }
        }

        public string ButtonActivity
        {
            get
            {
                if (_curPlayer == 0)
                    return "True";
                else return "False";
            }
        }

        public List<string> BoardCards { get
            {
                List<string> opened = new List<string>();
                int visibleCardCount = 0;
                foreach ((Card, Visibility) card in _boardCards)
                    if (card.Item2 == Visibility.Visible)
                    {
                        opened.Add(card.Item1.GetTextureName());
                    } 
                    else opened.Add("Resources/"+"cardBack_blue1.png");
                //Card[] opened = new Card[visibleCardCount];
                //for (int i = 0; i < visibleCardCount; i++)
                //    opened[i] = _boardCards[i].Item1;
                return opened;
            }
        }

        List<(int, (Card, Card))> HandCards = new List<(int, (Card, Card))>();
        #endregion

        #region Определение сильнейшей комбинации
        private Dictionary<CardRank, String> m_dicWeightCardRank;
        private Dictionary<String, String> m_dicNumberCombination;
        private List<CardRank> m_ls4CardsEqualRank;
        private List<CardRank> m_ls3CardsEqualRank;
        private List<CardRank> m_ls2CardsEqualRank;
        private List<CardRank> m_lsOnlyCards;
        public IEnumerable<int> GetStrongestCombination()
        {
            List<(int, UInt64)> lsCombinationCardsPlayers = new List<(int, UInt64)>();
            CreateNumberCombination();
            CreateWeightCardRank();
            foreach (var pairCard in HandCards)
            {
                String strIDCombination = "";
                String strIDCombinationFlush = "";
                List<Card> lsSetCards = new List<Card>();
                foreach ((Card, Visibility) boardCard in _boardCards)
                    lsSetCards.Add(boardCard.Item1);
                lsSetCards.Add(pairCard.Item2.Item1);
                lsSetCards.Add(pairCard.Item2.Item2);
                //Считаем количество карт с одной мастью
                Dictionary<CardSuit, int> dicCountSuit = new Dictionary<CardSuit, int>(CountCardsThisSuit(lsSetCards));
                //Сортирум карты по рангу
                CardRankCompare comp = new CardRankCompare();
                lsSetCards.Sort(comp);
                foreach (var suit in dicCountSuit)
                {
                    //Если масть у карт встречается более 4 раз, то роял флеш, стрит флеш или флеш
                    if (suit.Value < 5)
                    {
                        continue;
                    }
                    //Если первая карта туз, карты идут подряд и все одной масти, то роял флеш
                    if (lsSetCards[0].Rank == CardRank.A && IsInOrderAndEqualSuit(lsSetCards.GetRange(0, 5)))
                    {
                        strIDCombination = m_dicNumberCombination["RoualStraightFlush"] + "0000000000";
                        break;
                    }
                    //Если карты идут подряд и одной масти то стрит флеш
                    List<Card> lsStraightFlushCombination =
                        new List<Card>(GetStraightFlushCombination(lsSetCards));
                    if (lsStraightFlushCombination.Count != 0)
                    {
                        strIDCombination = m_dicNumberCombination["StraightFlush"]
                            + CreateCardsCombimationInStringForStraightFlush(lsSetCards, lsStraightFlushCombination);
                        break;
                    }
                    //иначе флэш, просто запоминаем комбинации, т.к. есть более сильные комбинации
                    strIDCombinationFlush = m_dicNumberCombination["Flush"]
                        + CreateCardsCombimationInStringForFlush(lsSetCards, suit.Key);
                    break;
                }
                //Если до сих пор не определили комбинацию, то ищем дальше
                if (strIDCombination.Length == 0)
                {
                    strIDCombination = DeterminationTheStrongestCombination(
                        lsSetCards,
                        strIDCombinationFlush);

                }
                lsCombinationCardsPlayers.Add((pairCard.Item1, Convert.ToUInt64(strIDCombination)));
            }
            //Находим самую сильную комбинацию
            return FindStrongestCombination(lsCombinationCardsPlayers);
        }

        //Определяем самую сильную комбинацию среди игроков
        private IEnumerable<int> FindStrongestCombination(List<(int, UInt64)> lsCombinationCardsPlayers)
        {
            List<int> lsWinPlayer = new List<int>();
            //Сордировка определенных комбинаций по убыванию
            CombinationCompare comp = new CombinationCompare();
            lsCombinationCardsPlayers.Sort(comp);
            lsWinPlayer.Add(lsCombinationCardsPlayers[0].Item1);
            for (int i = 0; i < lsCombinationCardsPlayers.Count - 1; i++)
            {
                //Если комбинации одинаковые, то добавляем еще одного игрока
                if (lsCombinationCardsPlayers[i].Item2 == lsCombinationCardsPlayers[i + 1].Item2)
                {
                    lsWinPlayer.Add(lsCombinationCardsPlayers[i + 1].Item1);
                }
            }
            return lsWinPlayer;
        }
        //Определяем сильную комбинацию
        private String DeterminationTheStrongestCombination(
            List<Card> lsSetCards,
            String strIDCombinationFlush)
        {
            String strIDCombination;
            //Подсчитываем количество карт с одинаковым рангом
            SortedCardsOnRank(lsSetCards);
            //Если карта с одним рангом встречается 4 раз, то каре
            if (m_ls4CardsEqualRank.Count != 0)
            {
                strIDCombination = m_dicNumberCombination["Care"]
                    + CreateCardsCombimationInStringForCare(lsSetCards, m_ls4CardsEqualRank[0]);
                return strIDCombination;
            }
            //Если карта с одним рангом встречается 3 раза и карта с другим рангом встречается 2 раза,
            //то фулхаус
            if (m_ls3CardsEqualRank.Count != 0 && m_ls2CardsEqualRank.Count != 0)
            {
                strIDCombination = m_dicNumberCombination["FullHouse"]
                    + CreateCardsCombimationInStringForFullHause32(lsSetCards);
                return strIDCombination;
            }
            //Если карта с одним рангом встречается 3 раза и карта с другим рангом встречается 3 раза,
            //то фулхаус

            if (m_ls3CardsEqualRank.Count == 2)
            {
                strIDCombination = m_dicNumberCombination["FullHouse"]
                    + CreateCardsCombimationInStringForFullHause33(lsSetCards);
                return strIDCombination;
            }
            //Если ранее определили флеш, то возвращаем его
            if (strIDCombinationFlush.Length != 0)
            {
                return strIDCombinationFlush;
            }
            //Если есть 5 карт, идущих по порядку, то стрит
            List<Card> lsStraightCombination =
                       new List<Card>(GetStraightCombination(lsSetCards));
            if (lsStraightCombination.Count != 0)
            {
                strIDCombination = m_dicNumberCombination["Straight"]
                    + CreateCardsCombimationInStringForStraight(lsSetCards, lsStraightCombination);
                return strIDCombination;
            }
            //Если карта с одним рангом встречается 3 раза, то трипс
            if (m_ls3CardsEqualRank.Count != 0)
            {
                strIDCombination = m_dicNumberCombination["ThreeCards"]
                    + CreateCardsCombimationInStringForThreeCards(lsSetCards);
                return strIDCombination;
            }
            //Если карта с одним рангом встречается 2 раза и карта с другим рангом встречается 2 раза,
            //то две пары
            if (m_ls2CardsEqualRank.Count > 1)
            {
                strIDCombination = m_dicNumberCombination["TwoPairs"]
                    + CreateCardsCombimationInStringForTwoPairs(lsSetCards);
                return strIDCombination;
            }
            //Если карта с одним рангом встречается 2 раза, то пара
            if (m_ls2CardsEqualRank.Count == 1)
            {
                strIDCombination = m_dicNumberCombination["Pair"]
                    + CreateCardsCombimationInStringForPair(lsSetCards);
                return strIDCombination;
            }
            //Если ничего не нашли, то запоминаем 5 старших карт
            strIDCombination = m_dicNumberCombination["Oldest"]
                    + CreateCardsCombimationInStringForOldest(lsSetCards.GetRange(0, 5));
            return strIDCombination;
        }
        //Считаем количество карт с одинаковым рангом
        private void SortedCardsOnRank(List<Card> lsCards)
        {
            Dictionary<CardRank, int> dicCountRank = new Dictionary<CardRank, int>(CountCardsThisRank(lsCards));
            m_ls4CardsEqualRank = new List<CardRank>();
            m_ls3CardsEqualRank = new List<CardRank>();
            m_ls2CardsEqualRank = new List<CardRank>();
            m_lsOnlyCards = new List<CardRank>();
            foreach (var rank in dicCountRank)
            {
                switch (rank.Value)
                {
                    case 4:
                        m_ls4CardsEqualRank.Add(rank.Key);
                        break;
                    case 3:
                        m_ls3CardsEqualRank.Add(rank.Key);
                        break;
                    case 2:
                        m_ls2CardsEqualRank.Add(rank.Key);
                        break;
                    case 1:
                        m_lsOnlyCards.Add(rank.Key);
                        break;
                    default:
                        break;
                }
            }
            RankCompare comp = new RankCompare();
            m_ls4CardsEqualRank.Sort(comp);
            m_ls3CardsEqualRank.Sort(comp);
            m_ls2CardsEqualRank.Sort(comp);
            m_lsOnlyCards.Sort(comp);
        }
        //Задаем веса каждому рангу карты
        private void CreateWeightCardRank()
        {
            m_dicWeightCardRank = new Dictionary<CardRank, String>(13)
            {
                { CardRank.c2, "02" },
                { CardRank.c3, "03" },
                { CardRank.c4, "04" },
                { CardRank.c5, "05" },
                { CardRank.c6, "06" },
                { CardRank.c7, "07" },
                { CardRank.c8, "08" },
                { CardRank.c9, "09" },
                { CardRank.c10, "10" },
                { CardRank.J, "11" },
                { CardRank.Q, "12" },
                { CardRank.K, "13" },
                { CardRank.A, "14" }
            };
        }
        //Задаем веса комбинациям
        private void CreateNumberCombination()
        {
            m_dicNumberCombination = new Dictionary<String, String>(10)
            {
                { "Oldest", "0" },
                { "Pair", "1" },
                { "TwoPairs", "2" },
                { "ThreeCards", "3" },
                { "Straight", "4" },
                { "Flush", "5" },
                { "FullHouse", "6" },
                { "Care", "7" },
                { "StraightFlush", "8" },
                { "RoualStraightFlush", "9" }
            };
        }
        //Считаем количество карт с одинаковой мастью
        private Dictionary<CardSuit, int> CountCardsThisSuit(List<Card> lsCards)
        {
            Dictionary<CardSuit, int> dicCountSuit = new Dictionary<CardSuit, int>
            {
                { CardSuit.Clubs, 0 },
                { CardSuit.Diamonds, 0 },
                { CardSuit.Hearts, 0 },
                { CardSuit.Spades, 0 }
            };
            foreach (var card in lsCards)
            {
                dicCountSuit[card.Suit]++;
            }
            return dicCountSuit;
        }
        //Считаем количество карт с одинаковым рангом
        private Dictionary<CardRank, int> CountCardsThisRank(List<Card> lsCards)
        {
            Dictionary<CardRank, int> dicCountRank = new Dictionary<CardRank, int>
            {
                { CardRank.c2, 0 },
                { CardRank.c3, 0 },
                { CardRank.c4, 0 },
                { CardRank.c5, 0 },
                { CardRank.c6, 0 },
                { CardRank.c7, 0 },
                { CardRank.c8, 0 },
                { CardRank.c9, 0 },
                { CardRank.c10, 0 },
                { CardRank.J, 0 },
                { CardRank.Q, 0 },
                { CardRank.K, 0 },
                { CardRank.A, 0 }
            };

            foreach (var card in lsCards)
            {
                dicCountRank[card.Rank]++;
            }
            return dicCountRank;
        }
        //Проверка: карты идут подрят и имеют одинаковую масть
        private bool IsInOrderAndEqualSuit(List<Card> lsCards)
        {
            for (int i = 0; i < lsCards.Count - 1; i++)
            {
                if (EnumRankToInt(lsCards[i].Rank) != EnumRankToInt(lsCards[i + 1].Rank) + 1
                    || lsCards[i].Suit != lsCards[i + 1].Suit)
                {
                    return false;
                }
            }
            return true;
        }
        //Проверка: карты идут по порядку
        private bool IsInOrder(List<Card> lsCards)
        {
            for (int i = 0; i < lsCards.Count - 1; i++)
            {
                if (EnumRankToInt(lsCards[i].Rank) != EnumRankToInt(lsCards[i + 1].Rank) + 1)
                {
                    return false;
                }
            }
            return true;
        }

        private int EnumRankToInt(CardRank cardRank)
        {
            return Convert.ToInt32(cardRank);
        }
        //Создание идентификатора комбинации для стрит флеша
        private String CreateCardsCombimationInStringForStraightFlush(
            List<Card> lsCards,
            List<Card> lsStraightFlushCombination)
        {
            String strCombinationString = "";
            foreach (var card in lsStraightFlushCombination)
            {
                strCombinationString += m_dicWeightCardRank[card.Rank];
            }
            return strCombinationString;
        }
        //Создание идентификатора комбинации для стрита
        private String CreateCardsCombimationInStringForStraight(
            List<Card> lsCards,
            List<Card> lsStraightCombination)
        {
            String strCombinationString = "";
            foreach (var card in lsStraightCombination)
            {
                strCombinationString += m_dicWeightCardRank[card.Rank];
            }
            return strCombinationString;
        }
        //Создание идентификатора комбинации для каре
        private String CreateCardsCombimationInStringForCare(List<Card> lsCards, CardRank Rank)
        {
            String strCombinationString =
                m_dicWeightCardRank[Rank]
                + m_dicWeightCardRank[Rank]
                + m_dicWeightCardRank[Rank]
                + m_dicWeightCardRank[Rank];

            foreach (var card in lsCards)
            {
                //ищем кикер - одна карта не из комбинации
                if (card.Rank != Rank)
                {
                    strCombinationString += m_dicWeightCardRank[card.Rank];
                    break;
                }
            }
            return strCombinationString;
        }
        //Определяем есть ли кобминация карт для стрит флеша
        private List<Card> GetStraightFlushCombination(List<Card> lsCards)
        {
            int j = 5;
            for (int i = 0; i < lsCards.Count + 1 - j; i++)
            {
                //5 карт идут по порядку и имеют одинаковую масть
                if (IsInOrderAndEqualSuit(lsCards.GetRange(i, j)))
                {
                    return lsCards.GetRange(i, j);
                }
            }
            return lsCards.GetRange(0, 0);
        }
        //Определяем есть ли кобминация карт для стрита
        private List<Card> GetStraightCombination(List<Card> lsCards)
        {
            int j = 5;
            for (int i = 0; i < lsCards.Count + 1 - j; i++)
            {
                //Ищем 5 карт идущих по порядку
                if (IsInOrder(lsCards.GetRange(i, j)))
                {
                    return lsCards.GetRange(i, j);
                }
            }
            return lsCards.GetRange(0, 0);
        }
        //Создание идентификатора комбинации для флеша
        private String CreateCardsCombimationInStringForFlush(List<Card> lsCards, CardSuit enSuit)
        {
            String strCombinationString = "";
            foreach (var card in lsCards)
            {
                if (card.Suit == enSuit)
                {
                    strCombinationString += m_dicWeightCardRank[card.Rank];
                }
            }
            return strCombinationString;
        }
        //Создание идентификатора комбинации для фулхауза если имеем пару трех карт
        private String CreateCardsCombimationInStringForFullHause33(List<Card> lsCards)
        {
            String strCombinationString = "";
            String RankInString = m_dicWeightCardRank[m_ls3CardsEqualRank[0]];
            strCombinationString = RankInString + RankInString + RankInString;
            RankInString = m_dicWeightCardRank[m_ls3CardsEqualRank[1]];
            strCombinationString += RankInString + RankInString;
            return strCombinationString;
        }
        //Создание идентификатора комбинации для фулхауза если имеем три карты и пару
        private String CreateCardsCombimationInStringForFullHause32(List<Card> lsCards)
        {
            String strCombinationString = "";
            String RankInString = m_dicWeightCardRank[m_ls3CardsEqualRank[0]];
            strCombinationString = RankInString + RankInString + RankInString;
            RankInString = m_dicWeightCardRank[m_ls2CardsEqualRank[0]];
            strCombinationString += RankInString + RankInString;
            return strCombinationString;
        }
        //Создание идентификатора комбинации для фулхауза если имеем три карты

        private String CreateCardsCombimationInStringForThreeCards(List<Card> lsCards)
        {
            String strCombinationString = "";
            String strHelpCombinationString = "";
            int fl = 0;
            foreach (var card in lsCards)
            {
                if (card.Rank == m_ls3CardsEqualRank[0])
                {
                    strCombinationString += m_dicWeightCardRank[card.Rank];
                }
                //Ищем два кикера
                if (fl < 2)
                {
                    strHelpCombinationString += m_dicWeightCardRank[card.Rank];
                    fl++;
                }
            }
            return strCombinationString + strHelpCombinationString;
        }
        //Создание идентификатора комбинации для фулхауза если имеем две пары
        private String CreateCardsCombimationInStringForTwoPairs(List<Card> lsCards)
        {
            String strCombinationString = "";
            String strHelpCombinationString1 = "";
            String strHelpCombinationString2 = "";
            bool fl = true;
            foreach (var card in lsCards)
            {
                if (card.Rank == m_ls2CardsEqualRank[0])
                {
                    strCombinationString += m_dicWeightCardRank[card.Rank];
                    continue;
                }
                if (card.Rank == m_ls2CardsEqualRank[1])
                {
                    strHelpCombinationString1 += m_dicWeightCardRank[card.Rank];
                    continue;
                }
                //Ищем кикер
                if (fl)
                {
                    strHelpCombinationString2 += m_dicWeightCardRank[card.Rank];
                    fl = false;
                }
            }
            return strCombinationString
                + strHelpCombinationString1
                + strHelpCombinationString2;
        }
        //Создание идентификатора комбинации для фулхауза если имеем пару
        private String CreateCardsCombimationInStringForPair(List<Card> lsCards)
        {
            String strCombinationString = "";
            String strHelpCombinationString = "";
            int fl = 0;
            foreach (var card in lsCards)
            {
                if (card.Rank == m_ls2CardsEqualRank[0])
                {
                    strCombinationString += m_dicWeightCardRank[card.Rank];
                    continue;
                }
                //Ищем три кикера
                if (fl < 3)
                {
                    strHelpCombinationString += m_dicWeightCardRank[card.Rank];
                    fl++;
                }
            }
            return strCombinationString + strHelpCombinationString;
        }
        //Запоманием 5 страрших карт
        private String CreateCardsCombimationInStringForOldest(List<Card> lsCards)
        {
            String strCombinationString = "";

            foreach (var card in lsCards)
            {

                strCombinationString += m_dicWeightCardRank[card.Rank];
            }
            return strCombinationString;
        }

        #endregion
        public void AddBank()
        {

        }

        public void BankDivision()
        {
            List<int> pretendents = new List<int>();
            foreach (KeyValuePair<int, Player> player in _players)
                pretendents.Add(player.Key);
            List<int> winners;
            int personCash;
            bool findSomebody;

            do
            {
                findSomebody = false;
                winners = GetStrongestCombination().ToList();
                for (int i = 0; i < _dividedBanks.Count && !findSomebody; i++)
                    for (int j = 0; j < winners.Count && !findSomebody; j++)
                        if (_dividedBanks[i].Item1.Equals(winners[j]))
                        {
                            // get winning for each player
                            personCash = _dividedBanks[i].Item2 / winners.Count;
                            // delete it from bank
                            _allBank -= DividedBanks[i].Item2;
                            // gave every winner his money
                            foreach (int winner in winners)
                                _players[winner].MyState.Cash += personCash;
                            // clear the winners list to start new iteration
                            winners.Clear();

                            // remove players, that wouldn't win from pretendents and _dividedBanks
                            for (int c = 0; c < i; c++)
                            {
                                _dividedBanks.RemoveAt(c);
                                pretendents.Remove(_dividedBanks[c].Item1);

                            }
                            // Correct _dividedBanks for next stages
                            _dividedBanks.ForEach(delegate ((int, int) banks) { banks.Item2 -= _dividedBanks[0].Item2; });
                            _dividedBanks.RemoveAt(0);

                            // marks that bank was divided
                            findSomebody = true;
                        }
                if (!findSomebody)
                {
                    _dividedBanks.Clear();
                    personCash = _allBank / winners.Count;
                    foreach (int winner in winners)
                        _players[winner].MyState.Cash += personCash;
                }
            } while (_dividedBanks.Count != 0);
        }
        #region Чисто по человечески оставил старый EndAction, чтобы потом БД по новой не писать
        //public bool EndAction()
        //{
        //    int visibleCardCount = 0;
        //    //добавляем в бд игроков и игру
        //    //using (ApplicationContext db = new ApplicationContext())
        //    //{
        //    //    foreach (var player in _players)
        //    //    {
        //    //        db.Players.Add(new DBPlayer
        //    //        {
        //    //            Name = player.Value.MyState.Name,
        //    //            StartCash = player.Value.MyState.Cash,
        //    //            FirstCard = new DBCard { Rank = player.Value.HandCards.Item1.Rank, Suit = player.Value.HandCards.Item1.Suit },
        //    //            SecondCard = new DBCard { Rank = player.Value.HandCards.Item2.Rank, Suit = player.Value.HandCards.Item2.Suit }
        //    //        });
        //    //    }
        //    //    db.Games.Add(new DBGame { });
        //    //    db.SaveChanges();
        //    //}

        //    (Card, Card) playerCards;

        //    int addKey;
        //    int bet = _players[0].MyState.PlayerBet;
        //    bool lastStage = true;
        //    // Check, if this Action was last in round
        //    foreach (KeyValuePair<int, Player> player in _players)
        //    {
        //        if (player.Value.MyState.State == PlayerGameState.In && player.Value.MyState.PlayerBet != bet)
        //        {
        //            lastStage = false;
        //            break;
        //        }
        //    }

        //    if (lastStage)
        //    {
        //        foreach ((Card, Visibility) card in _boardCards)
        //            if (card.Item2 == Visibility.Visible)
        //                visibleCardCount++;
        //            else break;

        //        switch (visibleCardCount)
        //        {
        //            case 0:
        //                // Lay out 3 cards on board
        //                for (int i = 0; i < 3; i++)
        //                    _boardCards[i].Item2 = Visibility.Visible;

        //                //добавляем в бд карты со стола
        //                //using (ApplicationContext db = new ApplicationContext())
        //                //{ 
        //                //    db.TableCards.Add(new DBTableCards { FirstCard = new DBCard { Rank = _boardCards[0].Item1.Rank, Suit = _boardCards[0].Item1.Suit },
        //                //                                        SecondCard = new DBCard { Rank = _boardCards[1].Item1.Rank, Suit = _boardCards[1].Item1.Suit },
        //                //                                        ThirdCard = new DBCard { Rank = _boardCards[2].Item1.Rank, Suit = _boardCards[2].Item1.Suit },
        //                //                                        DBGameId = db.Games.Last().Id });
        //                //    db.SaveChanges();
        //                //}

        //                NewStageStart();
        //                break;

        //            case 5:
        //                // determinate the winners and give them cash
        //                BankDivision();

        //                //добавляем в бд кэш игроков после окончания игры
        //                //using (ApplicationContext db = new ApplicationContext())
        //                //{
        //                //    var players = db.Players.ToList();

        //                //    foreach (var player in _players)
        //                //    {
        //                //        int i = 0;
        //                //        players[i].EndCash = player.Value.MyState.Cash;
        //                //        db.Players.Update(players[i]);
        //                //        i++;
        //                //    }
        //                //    db.SaveChanges();
        //                //}

        //                // Get new deck andd shuffle cards to start new game
        //                _cardDeck = new CardDeck();
        //                _cardDeck.Shuffle();

        //                // draw new cards for next game
        //                for (int i = 0; i < 5; i++)
        //                {
        //                    _boardCards[i].Item1 = _cardDeck.TakeCard();
        //                    _boardCards[i].Item2 = Visibility.Invisible;
        //                }

        //                // give each player new cards if he have cash snd  set state of each player to InGame, else cick him 
        //                foreach (KeyValuePair<int, Player> player in _players)
        //                    if (player.Value.MyState.Cash < _bigBlind)
        //                        Disconnect(player.Key);
        //                    else
        //                    {
        //                        playerCards = (_cardDeck.TakeCard(), _cardDeck.TakeCard());
        //                        HandCards.Add((player.Key, playerCards));
        //                        //player.Value.HandCards = (playerCards);
        //                        player.Value.MyState.State = PlayerGameState.In;
        //                    }

        //                // set new dealer left to old dealer
        //                _dealer = TakeNextKey(_dealer);

        //                // each player's bet and board max bet set to 0, also Raise set to bigBlind
        //                NewStageStart();
        //                _curRaise = _bigBlind;

        //                // make mandatory bets
        //                addKey = TakeNextKey(_dealer);
        //                _players[addKey].MyState.Cash -= _smallBlind;
        //                _players[addKey].MyState.PlayerBet = _smallBlind;

        //                //добавление в бд малого блайнда
        //                //using (ApplicationContext db = new ApplicationContext())
        //                //{
        //                //    DBGame game = db.Games.Last();
        //                //    DBPlayer player = db.Players.Where(p => p.Name == _players[addKey].MyState.Name).Last();

        //                //    db.Rounds.Add(new DBRound
        //                //    {
        //                //        Name = "Blinds",
        //                //        Game = game.Id,
        //                //        Player = player.Id,
        //                //        ActionName = "Small blind",
        //                //        BetSize = _smallBlind,
        //                //        //DecisionTime
        //                //    });
        //                //    db.SaveChanges();
        //                //}

        //                addKey = TakeNextKey(addKey);
        //                _players[addKey].MyState.Cash -= _bigBlind;
        //                _players[addKey].MyState.PlayerBet = _bigBlind;

        //                //добавление в бд большого блайнда
        //                //using (ApplicationContext db = new ApplicationContext())
        //                //{
        //                //    DBGame game = db.Games.Last();
        //                //    DBPlayer player = db.Players.Where(p => p.Name == _players[addKey].MyState.Name).Last();

        //                //    db.Rounds.Add(new DBRound
        //                //    {
        //                //        Name = "Blinds",
        //                //        Game = game.Id,
        //                //        Player = player.Id,
        //                //        ActionName = "Big blind",
        //                //        BetSize = _bigBlind,
        //                //        //DecisionTime
        //                //    });
        //                //    db.SaveChanges();
        //                //}

        //                // choose first player
        //                _curPlayer = TakeNextKey(addKey);

        //                break;

        //            default:
        //                // Lay out 1 card on board 
        //                _boardCards[visibleCardCount].Item2 = Visibility.Visible;

        //                //добавляем в бд 4 и 5 карту со стола
        //                //if (visibleCardCount == 4)
        //                //    using (ApplicationContext db = new ApplicationContext())
        //                //    {
        //                //        DBTableCards table = db.TableCards.Last();    
        //                //        table.FourthCard = new DBCard { Rank = _boardCards[3].Item1.Rank, Suit = _boardCards[3].Item1.Suit };
        //                //        table.FifthCard = new DBCard { Rank = _boardCards[4].Item1.Rank, Suit = _boardCards[4].Item1.Suit };    
        //                //        db.TableCards.Update(table);
        //                //        db.SaveChanges();
        //                //    }

        //                NewStageStart();

        //                break;
        //        }

        //    }
        //    else
        //    {
        //        for (int i = _curPlayer; i < _players.Count - 1; i++)
        //        {
        //            _curPlayer = TakeNextKey(_curPlayer);
        //            if (_players[_curPlayer].MyState.State == PlayerGameState.In)
        //            {
        //                List<Card> tmp = new List<Card>();
        //                tmp.Add(_players[_curPlayer].MyState.HandCards.Item1);
        //                tmp.Add(_players[_curPlayer].MyState.HandCards.Item2);
        //                tmp.AddRange(_boardCards.Select(x => x.Item1).ToArray());

        //                GameState aiState;
        //                aiState = ((AI)_players[_curPlayer]).GetOptimalMove(tmp);
        //                switch (aiState)
        //                {
        //                    case GameState.call:
        //                        _players[_curPlayer].Call();
        //                        break;

        //                    case GameState.check:
        //                        _players[_curPlayer].Check();
        //                        break;

        //                    case GameState.fold:
        //                        _players[_curPlayer].Fold();
        //                        break;

        //                    case GameState.raise:
        //                        _players[_curPlayer].Raise(_curRaise);
        //                        break;
        //                }
        //            }
        //        }

        //    }
        //    this.RaisePropertyChanged("BoardCards");
        //    return true;


        //    void NewStageStart()
        //    {
        //        // each player's bet and board max bet set to 0
        //        foreach (KeyValuePair<int, Player> player in _players)
        //            player.Value.MyState.PlayerBet = 0;
        //        _curBet = 0;

        //        // Give a turn to a player left to dealer
        //        _curPlayer = TakeNextKey(_dealer);
        //    }
        //}
        #endregion

        public void EndAction()
        {
            (Card, Card) playerCards;
            int visibleCardCount = 0;
            System.Type type = typeof(Player);
            bool botTurn = true;
            bool lastStage = true;
            int bet = 0;
            do
            {
                foreach (KeyValuePair<int, Player> player in _players)
                    if (player.Value.MyState.State == PlayerGameState.In)
                    {
                        bet = player.Value.MyState.PlayerBet;
                        break;
                    }
                // Check, if this Action was last in round
                if (_players[_curPlayer].MyState.State == PlayerGameState.Check && _players[TakePreviousKey(_curPlayer)].MyState.State == PlayerGameState.Check)
                {
                    lastStage = true;
                }
                else
                    foreach (KeyValuePair<int, Player> player in _players)
                    {
                        if (player.Value.MyState.State == PlayerGameState.In && (player.Value.MyState.PlayerBet != bet || player.Value.MyState.PlayerBet == 0))
                        {
                            lastStage = false;
                            break;
                        }
                    }

                if (lastStage)
                {
                    foreach ((Card, Visibility) card in _boardCards)
                        if (card.Item2 == Visibility.Visible)
                            visibleCardCount++;
                    switch (visibleCardCount)
                    {
                        case 0:
                            // Lay out 3 cards on board
                            for (int i = 0; i < 3; i++)
                                _boardCards[i].Item2 = Visibility.Visible;

                            NewStageStart();
                            if (type == _players[_curPlayer].GetType())
                                botTurn = false;
                            else
                            {
                                BotAction();
                            }
                            break;

                        case 5:
                            // determinate the winners and give them cash
                            BankDivision();

                            _dealer = TakeNextKey(_dealer);
                            

                            // give each player new cards if he have cash snd  set state of each player to InGame, else cick him 
                            foreach (KeyValuePair<int, Player> player in _players)
                                if (player.Value.MyState.Cash < _bigBlind)
                                    Disconnect(player.Key);

                            GameStart();

                            //if (type == _players[_curPlayer].GetType())
                                botTurn = false;
                            //else
                            //{
                            //    BotAction();
                                
                            //}
                            break;

                        default:
                            // Lay out 1 card on board 
                            _boardCards[visibleCardCount].Item2 = Visibility.Visible;
                            NewStageStart();

                            if (type == _players[_curPlayer].GetType())
                                botTurn = false;
                            else
                            {
                                BotAction();
                                
                            }
                            break;
                    }
                }
                else
                {
                    _curPlayer = TakeNextKey(_curPlayer);
                    while (_players[_curPlayer].MyState.State != PlayerGameState.In)
                        _curPlayer = TakeNextKey(_curPlayer);
                    if (type == _players[_curPlayer].GetType() && _players[_curPlayer].MyState.State == PlayerGameState.In)
                        botTurn = false;
                    else
                    {
                        BotAction();
                    }
                }
                lastStage = true;
            } while (botTurn);
            this.RaisePropertyChanged("DealerChip"); 
            this.RaisePropertyChanged("BoardCards");
            this.RaisePropertyChanged("ButtonActivity");
        }

        void BotAction()
        {
            if (_players[_curPlayer].MyState.State == PlayerGameState.In)
            {
                List<Card> tmp = new List<Card>();
                tmp.Add(_players[_curPlayer].MyState.HandCards.Item1);
                tmp.Add(_players[_curPlayer].MyState.HandCards.Item2);
                tmp.AddRange(_boardCards.Select(x => x.Item1).ToArray());

                GameState aiState;
                aiState = ((AI)_players[_curPlayer]).GetOptimalMove(tmp);
                switch (aiState)
                {
                    case GameState.call:
                        _players[_curPlayer].Call();
                        break;

                    case GameState.check:
                        _players[_curPlayer].Check();
                        break;

                    case GameState.fold:
                        _players[_curPlayer].Fold();
                        break;

                    case GameState.raise:
                        if (_players[1].MyState.Cash == 0)
                        {
                            _players[1].Fold();
                            break;
                        }

                        _players[1].Raise(((AI)_players[1]).GetOptimalRaise(_players[1].MyState.Cash, tmp));
                        break;
                }
            }
        }

        void NewStageStart()
                {
                // each player's bet and board max bet set to 0
                foreach (KeyValuePair<int, Player> player in _players)
                    player.Value.MyState.PlayerBet = 0;
                _curBet = 0;

                // Give a turn to a player left to dealer
                _curPlayer = TakeNextKey(_dealer);
                while (_players[_curPlayer].MyState.State != PlayerGameState.In)
                    _curPlayer = TakeNextKey(_curPlayer);
        }

        int TakeNextKey(int key) // return the key of next player
        {
            List<int> keys = _players.Keys.ToList();
            int nextKey;
            int playerInd = keys.IndexOf(key);
            if (playerInd < _players.Count - 1)
                nextKey = keys[playerInd + 1];
            else nextKey = keys[0];
            return nextKey;
        }

        int TakePreviousKey(int key) // return the key of next player
        {
            List<int> keys = _players.Keys.ToList();
            int prevKey;
            int playerInd = keys.IndexOf(key);
            if (playerInd > 0)
                prevKey = keys[playerInd - 1];
            else prevKey = keys[keys.Count - 1];
            return prevKey;
        }

        public (bool, Player) TryConnect(string name, int cash)
        {
            if (CheckUniqueName())
            {
                if (_players.Count < _gameRules.MaxPlayers)
                {
                    Player player = new Player(name, cash, this);
                    _players.Add(_players.Count, player);
                    this.RaisePropertyChanged($"player{_players.Count}vis");
                    return (true, player);
                }
                return (false, null);
            }
            else
                throw new Exception("Данное имя игрока уже существует!");

            //проверка имени на уникальность
            bool CheckUniqueName()
            {
                foreach (var player in _players)
                {
                    if (player.Value.MyState.Name == name)
                        return false;
                }
                return true;
            }
        }
        public (bool, Player) TryConnectBot(int number, int cash)//не очень безопасно ну и ладно
        {
            if (_players.Count < _gameRules.MaxPlayers)
            {
                Player player = new AI($"Bot{number.ToString()}", cash, this);
                _players.Add(number, player);//////WARNING ПО ИДЕЕ ЕМУ СЕЙЧАС КАРТЫ НЕ НУЖНО ВЫДАВАТЬ
                (Card, Card) playerCards = (_cardDeck.TakeCard(), _cardDeck.TakeCard());
                _players[number].MyState.HandCards = playerCards;
                HandCards.Add((number, playerCards));
                this.RaisePropertyChanged($"player{number}vis");
                this.RaisePropertyChanged($"player{number}connected");
                this.RaisePropertyChanged($"player{number}connectedvis");
                this.RaisePropertyChanged($"player{number}");
                return (true, player);
            }
            return (false, null);
        }

        public void Disconnect(int key)
        {
            _players.Remove(key);
        }

        public List<Card> GetAllDeckWithoutMineCards(List<Card> exclude)
        {
            List<Card> result = new List<Card>();

            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
                    if (!exclude.Exists(x => x.Rank == rank && x.Suit == suit))
                        result.Add(new Card(rank, suit));

            return result;
        }

        public bool IsCombination(List<Card> cards)
        {
            //Pair
            for (int i = 0; i < cards.Count; i++)
                for (int j = i + 1; j < cards.Count; j++)
                    if (cards[i].Rank == cards[j].Rank)
                        return true;

            //Flush
            List<CardSuit> suits = new List<CardSuit>();
            foreach (Card c in cards)
                suits.Add(c.Suit);

            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                if (suits.FindAll(s => s.Equals(suit)).Count >= 5)
                    return true;

            //Straight
            cards.Sort(new CardRankCompare());
            List<Card> comb = new List<Card>(GetStraightCombination(cards));
            if (comb.Count != 0)
            {
                return true;
            }

            return false;
        }

        public int countOuts(List<Card> cards)
        {
            int result = 0;
            List<Card> allCards = GetAllDeckWithoutMineCards(cards);
            foreach (Card c in allCards)
            {
                List<Card> buf = new List<Card>();
                buf.AddRange(cards);
                buf.Add(c);
                if (IsCombination(buf))
                {
                    result++;
                }
            }
            int riverCount = 0;
            foreach (Card c in allCards)
            {
                List<Card> buf = new List<Card>();
                buf.AddRange(cards.GetRange(2, cards.Count - 2));
                buf.Add(c);
                if (IsCombination(buf))
                {
                    riverCount++;
                }
            }
            return result - riverCount;
        }

        //List<DBRequest> DbRequest(string PlayerName)
        //{
        //    List<DBRequest> request = new List<DBRequest>();

        //    using (ApplicationContext db = new ApplicationContext())
        //    {
        //        var player = db.Players.Where(p => p.Name == PlayerName);
        //        var tableCards = db.TableCards.ToList();

        //        foreach (var _player in player)
        //        {
        //            int i = 0;
        //            DBRound lastRound = db.Rounds.Where(p => p.Player == _player.Id).Last();

        //            request[i] = new DBRequest { LastBet = lastRound.BetSize, StartCash = _player.StartCash };
        //            request[i].DBHandCards.Add(_player.FirstCard);
        //            request[i].DBHandCards.Add(_player.SecondCard);

        //            i++;
        //        }

        //        foreach (var _tableCards in tableCards)
        //        {
        //            int i = 0;

        //            request[i].DBTableCards.Add(_tableCards.FirstCard);
        //            request[i].DBTableCards.Add(_tableCards.SecondCard);
        //            request[i].DBTableCards.Add(_tableCards.ThirdCard);
        //            request[i].DBTableCards.Add(_tableCards.FourthCard);
        //            request[i].DBTableCards.Add(_tableCards.FifthCard);

        //            i++;
        //        }
        //    }

        //    return request;
        //}

        public event PropertyChangedEventHandler PropertyChange;

        public virtual void OnPropertyChanged(string propertyName)
        {
            var propertyChanged = PropertyChange;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}