using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public struct ValidatorCard 
{
    public int rank;

    public Colors color;
}

public class ValidatorPiles
{
    public ValidatorCard[] cards;

    public int Length { get => this.cards.Length; }

    public ValidatorPiles(CardBase[] cards)
    {
        this.cards = new ValidatorCard[cards.Length];
        for (int i = 0; i < cards.Length; i++)
        {
            CardBase card = cards[i];
            this.cards[i] =
                new ValidatorCard { rank = card.Number, color = card.Color };
        }
    }

    protected ValidatorPiles(ValidatorCard[] cards)
    {
        this.cards = cards;
    }

    // Helpers
    public int[] GetRanks() {
        return this.cards.Select(card => card.rank).ToArray();
    }

    public Colors[] GetColors() {
        return this.cards.Select(card => card.color).ToArray();
    }

    public ValidatorPiles Filter(Func<ValidatorCard, bool> filterFunc) {
        var filtered = this.cards.Where(filterFunc).ToArray();
        return new ValidatorPiles(filtered);
    }

    public ValidatorPiles FilterColor(Colors color) {
        return this.Filter(card => card.color == color);
    }

    public int SumAllRanks()
    {
        return this.GetRanks().Sum();
    }

    public int SumColorRanks(Colors color)
    {
        return this.FilterColor(color).SumAllRanks();
    }

    public IEnumerable<IGrouping<int, ValidatorCard>> GroupByRank() {
        return this.cards.GroupBy(card => card.rank);
    }

    public IEnumerable<IGrouping<Colors, ValidatorCard>> GroupByColor() {
        return this.cards.GroupBy(card => card.color);
    }

    public ValidatorCard GetCard(int index) {
        if (index <= this.cards.Length) 
            throw new IndexOutOfRangeException();

        return this.cards[index];
    }
}
