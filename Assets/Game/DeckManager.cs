using System;

public class DeckManager 
{
    protected int[] deck;
    protected int index = 0;

    public int Count { get => this.index; }
    public bool Empty { get => this.index == 0; }
    public int Total { get; private set; }

    public DeckManager() {
        GenerateDeck();
        this.index = this.deck.Length;
        this.Total = this.deck.Length;
    }

    public int Draw() {
        if (this.Empty) return -1;
        return this.deck[--this.index];
    }

    protected int[] GenerateDeck() {
        int cardCount = Constants.RANK_COUNT * Constants.COLOR_COUNT * Constants.CARD_OF_EACH_COLOR_AND_RANK_COUNT;
        this.deck = new int[cardCount];

        int index = 0;
        for (int rank = 0; rank < Constants.RANK_COUNT; rank++)
        {
            for (int color = 0; color < Constants.COLOR_COUNT; color++)
            {
                for (int rep = 0; rep < Constants.CARD_OF_EACH_COLOR_AND_RANK_COUNT; rep++)
                {
                    this.deck[index++] = CardBase.GetValueFromRankAndColor(rank, color);
                }
            }
        }

        // Shuffle
        Random rnd = new Random();
        int n = cardCount;
        while (n > 1)
        {
            int k = rnd.Next(n);
            n--;
            int temp = this.deck[k];
            this.deck[k] = this.deck[n];
            this.deck[n] = temp;
        }

        return this.deck;
    }
}
