using System.Linq;

public class TwoAdjacentColorsValidator : BaseValidator
{
    protected Colors color;

    public TwoAdjacentColorsValidator(string type, params string[] arguments) :
        base(type)
    {
        this.ValidateArgumentCount(arguments, 1);
        this.color = this.ArgumentToColor(arguments[0], 1);
    }

    public override string GetCardText()
    {
        return string
            .Format("Hay dos cartas {0} adyacentes",
            this.GetColorTranslated(this.color));
    }

    protected override bool ValidatePiles()
    {
        int[] indexes = this.piles.cards
            .Select((card, index) => new { card, index })
            .Where(obj => obj.card.color == this.color)
            .Select(obj => obj.index)
            .ToArray();

        if (indexes.Length != 2) return false;

        return (indexes[1] - indexes[0]) == 1;
    }
}
