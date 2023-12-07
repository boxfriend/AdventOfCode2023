using System.Text.RegularExpressions;

namespace AdventOfCode2023;
internal class CamelCards : IAdventSolution
{
    public static string Evaluate (string[] data)
    {
        var hands = new SortedSet<Hand>();
        foreach(var line in data)
        {
            if (!hands.Add(ExtractInfo(line)))
                throw new InvalidOperationException("Duplicates?");
        }

        var total = 0L;
        var i = 0;
        foreach (var v in hands)
        {
            total += v.Bid * ++i;
        }

        return total.ToString();
    }

    private static Hand ExtractInfo(string line)
    {
        var data = line.Trim().Split(' ');
        var bid = int.Parse(data[1].Trim());
        return new(data[0].Trim(), bid);
    }


    private static readonly Dictionary<char, int> _cardValues = new()
    {
        ['A'] = 12,
        ['K'] = 11,
        ['Q'] = 10,
        ['J'] = -1,
        ['T'] = 8,
        ['9'] = 7,
        ['8'] = 6,
        ['7'] = 5,
        ['6'] = 4,
        ['5'] = 3,
        ['4'] = 2,
        ['3'] = 1,
        ['2'] = 0
    };
    private static int CompareHands(Hand left, Hand right)
    {
        if (left.Rank > right.Rank)
            return 1;
        if (left.Rank < right.Rank)
            return -1;
        if (left.Cards == right.Cards)
            return 0;

        for(var i = 0; i < left.Cards.Length; i++)
        {
            if (left.Cards[i] == right.Cards[i])
                continue;

            return _cardValues[left.Cards[i]].CompareTo(_cardValues[right.Cards[i]]);
        }

        return left.Bid.CompareTo(right.Bid);
    }



    private static readonly Dictionary<char, int> _charCount = [];
    private static Rank HandRank(string cards)
    {
        _charCount.Clear();
        foreach(var ch in cards)
        {
            if (!_charCount.TryAdd(ch, 1))
                _charCount[ch]++;
        }

        if (_charCount.Count > 1 && _charCount.Remove('J', out var x))
        {
            var biggest = _charCount.MaxBy(card => card.Value).Key;
            _charCount[biggest] += x;
        }

        if (_charCount.Count == 1)
            return Rank.FiveOfAKind;

        if (_charCount.Count == 5)
            return Rank.HighCard;

        if (_charCount.Values.Any(x => x == 4))
            return Rank.FourOfAKind;

        if (_charCount.Values.Any(x => x == 3))
            return _charCount.Values.Any(x => x == 2) ?  Rank.FullHouse : Rank.ThreeOfAKind;

        return _charCount.Values.Count(x => x == 2) > 1 ? Rank.TwoPair : Rank.OnePair;
    }


    private enum Rank
    {
        None = 0,
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind
    }
    private record struct Hand (string Cards, int Bid) : IComparable<Hand>
    {
        private Rank _rank;
        public Rank Rank
        {
            get
            {
                if(_rank is Rank.None)
                    _rank = HandRank(Cards);
                return _rank;
            }
        }
        public readonly int CompareTo (Hand other) => CompareHands(this, other);
    }
}
