using System.Linq;

public class EvenOddAlternateValidator : BaseValidator
{
    public EvenOddAlternateValidator(string type) : base(type) {}

    public override string GetCardText()
    {
        return "Las cartas son pares e impares dos a dos";
    }

    protected override bool ValidatePiles()
    {
        var result = this.piles.cards
            .Select((card, index) => new { even = card.rank % 2 == 0, index })
            .GroupBy(obj => obj.even, obj => obj.index)
            .ToArray();

        if (result.Length != 2) return false;
        
        if (result[0].Count() != 2) return false;

        int[] indexes = result[0].ToArray();
        return (indexes[1] - indexes[0]) == 2;
    }
}
