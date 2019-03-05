using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using ReactiveUI;
using System.Linq;

namespace PokerCore.Model
{
    public class PokerM
    {
        Dictionary<int, Player> _players;
        GameRules _gameRules;
        List<Card> _boardCards;
        CardDeck _cardDeck;
        int _curPlayer;
        int _dealer;
        int _smallBlind;
        int _bigBlind;
        int _curBet;
        int _curRaise;
        int _allBank;
        List<(int, int)> _dividedBanks;

        public PokerM(string name, int maxplayer, int startbank)
        {
            _allBank = startbank;
            //gameRules = new GameRules(maxplayer);
            //players = new Dictionary<int, Player>(maxplayer);
            //players[0] = new Player(name);
        }

        public int Dealer { get => _dealer; }

        public int CurPlayer { get => _curPlayer; }

        public int SmallBlind { get => _smallBlind; }

        public int BigBlind { get => _bigBlind; }

        public int CurrentRaise { get => _curRaise; set => _curRaise = value; }

        public int CurrentBet { get => _curBet; set => _curBet = value; }

        public int AllBank { get => _allBank; }

        public List<(int, int)> DividedBanks { get => _dividedBanks; set => _dividedBanks = value; }

        public CardDeck Deck { get => _cardDeck; }

        public GameRules Rules { get => _gameRules; }

        public Dictionary<int, Player> Players { get => _players; }

        public List<Card> BoardCards { get => _boardCards; }

        List<(int, (Card, Card))> HandCards;


        private Dictionary<CardRank, String> m_dicWeightCardRank;
        private Dictionary<String, String> m_dicNumberCombination;
        private List<CardRank> m_ls4CardsEqualRank;
        private List<CardRank> m_ls3CardsEqualRank;
        private List<CardRank> m_ls2CardsEqualRank;
        private List<CardRank> m_lsOnlyCards;
        public IEnumerable<int> GetStrongestCombination()
        {
            List<(int, UInt64)> lsCombinationCardsPlayers=new List<(int, UInt64)>();
            CreateNumberCombination();
            CreateWeightCardRank();
            foreach (var pairCard in HandCards)
            {
                String strIDCombination = "";
                String strIDCombinationFlush = "";
                List<ICard> lsSetCards = new List<ICard>();
                lsSetCards.AddRange(_boardCards);
                lsSetCards.Add(pairCard.Item2.Item1);
                lsSetCards.Add(pairCard.Item2.Item2);
                //Считаем количество карт с одной мастью
                Dictionary< CardSuit, int> dicCountSuit = new Dictionary<CardSuit, int>(CountCardsThisSuit(lsSetCards));
                //Сортирум карты по рангу
                CardRankCompare comp = new CardRankCompare();
                lsSetCards.Sort(comp);
                foreach ( var suit in dicCountSuit)
                {
                    //Если масть у карт встречается более 4 раз, то роял флеш, стрит флеш или флеш
                    if(suit.Value < 5)
                    {
                        continue;
                    }
                    //Если первая карта туз, карты идут подряд и все одной масти, то роял флеш
                    if(lsSetCards[0].Rank == CardRank.A && IsInOrderAndEqualSuit(lsSetCards.GetRange(0,5)))
                    {
                        strIDCombination = m_dicNumberCombination["RoualStraightFlush"] + "0000000000";
                        break;
                    }
                    //Если карты идут подряд и одной масти то стрит флеш
                    List<ICard> lsStraightFlushCombination =
                        new List<ICard>(GetStraightFlushCombination(lsSetCards));
                   if (lsStraightFlushCombination.Count != 0)
                    {
                        strIDCombination = m_dicNumberCombination["StraightFlush"] 
                            + CreateCardsCombimationInStringForStraightFlush(lsSetCards, lsStraightFlushCombination);
                        break;
                    }
                   //иначе флэш, просто запоминаем комбинации, т.к. есть более сильные комбинации
                    strIDCombinationFlush = m_dicNumberCombination["Flush"] 
                        + CreateCardsCombimationInStringForFlush(lsSetCards,suit.Key);
                        break;
                }
                //Если до сих пор не определили комбинацию, то ищем дальше
                if(strIDCombination.Length == 0)
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
            for ( int i=0; i< lsCombinationCardsPlayers.Count-1; i++)
            {
                //Если комбинации одинаковые, то добавляем еще одного игрока
                if (lsCombinationCardsPlayers[i].Item2 == lsCombinationCardsPlayers[i+1].Item2)
                {
                    lsWinPlayer.Add(lsCombinationCardsPlayers[i+1].Item1);
                }
            }
            return lsWinPlayer;
        }
        //Определяем сильную комбинацию
        private String DeterminationTheStrongestCombination(
            List<ICard> lsSetCards, 
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
            List<ICard> lsStraightCombination =
                       new List<ICard>(GetStraightCombination(lsSetCards));
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
                    + CreateCardsCombimationInStringForOldest(lsSetCards.GetRange(0,5));
            return strIDCombination;
        }
        //Считаем количество карт с одинаковым рангом
        private void SortedCardsOnRank(List<ICard> lsCards)
        {
            Dictionary<CardRank, int> dicCountRank = new Dictionary<CardRank, int>(CountCardsThisRank(lsCards));
            m_ls4CardsEqualRank = new List<CardRank>();
            m_ls3CardsEqualRank = new List<CardRank>();
            m_ls2CardsEqualRank = new List<CardRank>();
            m_lsOnlyCards = new List<CardRank>();
            foreach (var rank in dicCountRank)
            {
                switch(rank.Value)
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
            m_dicWeightCardRank = new Dictionary<CardRank, String>(13);
            m_dicWeightCardRank.Add(CardRank.c2, "02");
            m_dicWeightCardRank.Add(CardRank.c3, "03");
            m_dicWeightCardRank.Add(CardRank.c4, "04");
            m_dicWeightCardRank.Add(CardRank.c5, "05");
            m_dicWeightCardRank.Add(CardRank.c6, "06");
            m_dicWeightCardRank.Add(CardRank.c7, "07");
            m_dicWeightCardRank.Add(CardRank.c8, "08");
            m_dicWeightCardRank.Add(CardRank.c9, "09");
            m_dicWeightCardRank.Add(CardRank.c10, "10");
            m_dicWeightCardRank.Add(CardRank.J, "11");
            m_dicWeightCardRank.Add(CardRank.Q, "12");
            m_dicWeightCardRank.Add(CardRank.K, "13");
            m_dicWeightCardRank.Add(CardRank.A, "14");
        }
        //Задаем веса комбинациям
        private void CreateNumberCombination()
        {
            m_dicNumberCombination = new Dictionary<String, String>(10);
            m_dicNumberCombination.Add("Oldest", "0");
            m_dicNumberCombination.Add("Pair", "1");
            m_dicNumberCombination.Add("TwoPairs", "2");
            m_dicNumberCombination.Add("ThreeCards", "3");
            m_dicNumberCombination.Add("Straight", "4");
            m_dicNumberCombination.Add("Flush", "5");
            m_dicNumberCombination.Add("FullHouse", "6");
            m_dicNumberCombination.Add("Care", "7");
            m_dicNumberCombination.Add("StraightFlush", "8");
            m_dicNumberCombination.Add("RoualStraightFlush", "9");
        }
        //Считаем количество карт с одинаковой мастью
        private Dictionary<CardSuit, int> CountCardsThisSuit(List<ICard> lsCards)
        {
            Dictionary<CardSuit, int> dicCountSuit = new Dictionary<CardSuit, int>();
            dicCountSuit.Add(CardSuit.Clubs, 0);
            dicCountSuit.Add(CardSuit.Dimonds, 0);
            dicCountSuit.Add(CardSuit.Hearts, 0);
            dicCountSuit.Add(CardSuit.Spades, 0);
            foreach (var card in lsCards)
            {
                dicCountSuit[card.Suit]++;
            }
            return dicCountSuit;
        }
        //Считаем количество карт с одинаковым рангом
        private Dictionary<CardRank, int> CountCardsThisRank(List<ICard> lsCards)
        {
            Dictionary<CardRank, int> dicCountRank = new Dictionary<CardRank, int>();
            dicCountRank.Add(CardRank.c2, 0);
            dicCountRank.Add(CardRank.c3, 0);
            dicCountRank.Add(CardRank.c4, 0);
            dicCountRank.Add(CardRank.c5, 0);
            dicCountRank.Add(CardRank.c6, 0);
            dicCountRank.Add(CardRank.c7, 0);
            dicCountRank.Add(CardRank.c8, 0);
            dicCountRank.Add(CardRank.c9, 0);
            dicCountRank.Add(CardRank.c10, 0);
            dicCountRank.Add(CardRank.J, 0);
            dicCountRank.Add(CardRank.Q, 0);
            dicCountRank.Add(CardRank.K, 0);
            dicCountRank.Add(CardRank.A, 0);

            foreach (var card in lsCards)
            {
                dicCountRank[card.Rank]++;
            }
            return dicCountRank;
        }
        //Проверка: карты идут подрят и имеют одинаковую масть
        private bool IsInOrderAndEqualSuit(List<ICard> lsCards)
        {
            for(int i=0; i < lsCards.Count- 1; i++)
            {
                if(EnumRankToInt(lsCards[i].Rank) != EnumRankToInt(lsCards[i+1].Rank) + 1
                    || lsCards[i].Suit != lsCards[i+1].Suit)
                {
                    return false;
                }
            }
            return true;
        }
        //Проверка: карты идут по порядку
        private bool IsInOrder(List<ICard> lsCards)
        {
            for (int i = 0; i < lsCards.Count - 1; i++)
            {
                if (EnumRankToInt(lsCards[i].Rank)  != EnumRankToInt(lsCards[i + 1].Rank) + 1)
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
            List<ICard> lsCards, 
            List<ICard> lsStraightFlushCombination)
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
            List<ICard> lsCards,
            List<ICard> lsStraightCombination)
        {
            String strCombinationString="";
            foreach (var card in lsStraightCombination)
            {
                strCombinationString += m_dicWeightCardRank[card.Rank];
            }
            return strCombinationString;
        }
        //Создание идентификатора комбинации для каре
        private String CreateCardsCombimationInStringForCare(List<ICard> lsCards, CardRank Rank)
        {
            String strCombinationString = 
                m_dicWeightCardRank[Rank]
                + m_dicWeightCardRank[Rank]
                + m_dicWeightCardRank[Rank]
                + m_dicWeightCardRank[Rank];
           
            foreach (var card in lsCards)
            {
                //ищем кикер - одна карта не из комбинации
                if(card.Rank!= Rank)
                {
                    strCombinationString += m_dicWeightCardRank[card.Rank];
                    break;
                }
            }
            return strCombinationString;
        }
        //Определяем есть ли кобминация карт для стрит флеша
        private List<ICard> GetStraightFlushCombination(List<ICard> lsCards)
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
        private List<ICard> GetStraightCombination(List<ICard> lsCards)
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
        private String CreateCardsCombimationInStringForFlush(List<ICard> lsCards, CardSuit enSuit)
        {
            String strCombinationString = "";
            foreach (var card in lsCards)
            {
                if(card.Suit== enSuit)
                {
                    strCombinationString+= m_dicWeightCardRank[card.Rank];
                }
            }
            return strCombinationString;
        }
        //Создание идентификатора комбинации для фулхауза если имеем пару трех карт
        private String CreateCardsCombimationInStringForFullHause33(List<ICard> lsCards)
        {
            String strCombinationString = "";
            String RankInString = m_dicWeightCardRank[m_ls3CardsEqualRank[0]];
            strCombinationString = RankInString + RankInString + RankInString;
            RankInString = m_dicWeightCardRank[m_ls3CardsEqualRank[1]];
            strCombinationString += RankInString + RankInString;
            return strCombinationString;
        }
        //Создание идентификатора комбинации для фулхауза если имеем три карты и пару
        private String CreateCardsCombimationInStringForFullHause32(List<ICard> lsCards)
        {
            String strCombinationString = "";
            String RankInString = m_dicWeightCardRank[m_ls3CardsEqualRank[0]];
            strCombinationString = RankInString + RankInString + RankInString;
            RankInString = m_dicWeightCardRank[m_ls2CardsEqualRank[0]];
            strCombinationString += RankInString + RankInString;
            return strCombinationString;
        }
        //Создание идентификатора комбинации для фулхауза если имеем три карты

        private String CreateCardsCombimationInStringForThreeCards(List<ICard> lsCards)
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
                if(fl < 2)
                {
                    strHelpCombinationString += m_dicWeightCardRank[card.Rank];
                    fl++;
                }
            }
            return strCombinationString + strHelpCombinationString;
        }
        //Создание идентификатора комбинации для фулхауза если имеем две пары
        private String CreateCardsCombimationInStringForTwoPairs(List<ICard> lsCards)
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
        private String CreateCardsCombimationInStringForPair(List<ICard> lsCards)
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
                if(fl < 3)
                {
                    strHelpCombinationString += m_dicWeightCardRank[card.Rank];
                    fl++;
                }
            }
            return strCombinationString + strHelpCombinationString;
        }
        //Запоманием 5 страрших карт
        private String CreateCardsCombimationInStringForOldest(List<ICard> lsCards)
        {
            String strCombinationString = "";

            foreach (var card in lsCards)
            {

                strCombinationString += m_dicWeightCardRank[card.Rank];
            }
            return strCombinationString;
        }
        public void AddBank()
        {

        }

        public void BankDivision()
        {
            List<int> winners = GetStrongestCombination().AsList<int>;
            GetStrongestCombination();
        }

        public bool EndAction()
        {
            int bet = _players[0].MyState.PlayerBet;
            bool lastStage = true;
            // Check, if this Action was last in round
            foreach (KeyValuePair<int, Player> player in _players)
            {
                if (player.Value.MyState.State == PlayerGameState.In && player.Value.MyState.PlayerBet != bet)
                {
                    lastStage = false;
                    continue;
                }
            }

            if (lastStage)
            { switch (_boardCards.Count)
                {
                    case 0:
                        _cardDeck.Shuffle();
                        for (int i = 0; i < 3; i++)
                            _boardCards.Add(_cardDeck.TakeCard());
                        for (int i = _dealer; i < _players.Count; i++)
                        {

                        }
                        IEnumerator<Player> h = _players.GetEnumerator;
                        _curPlayer = ;
                        break;

                    case 5:

                        break;

                    default:
                        _boardCards.Add(_cardDeck.TakeCard());
                        break;
                }
                return true;
            }
            else
            {

            }
        }

        public bool TryConnect(string name, int cash)
        {
            if (_players.Count  < _gameRules.MaxPlayers)
            {
                _players.Add(_players.Count, new Player(name, cash));
                return true;
            }
            return false;
        }

        public void Disconnect(int key)
        {
            _players.Remove(key);
        }

        public string GetTextureName(CardRank rank, CardSuit suit)
        {
            string name;
            name = "card";

            foreach (string suits in Enum.GetNames(typeof(CardSuit)))
            {
                if (suits.Equals(suit.ToString()))
                {
                    name += suits;
                    foreach (string ranks in Enum.GetNames(typeof(CardRank)))
                    {
                        if (ranks.Equals(suit.ToString()))
                        {
                            name += ranks;
                        }
                    }
                }
            }

            name += ".png";
            return name;
        }
    }
}