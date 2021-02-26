using System.Linq;

public class AllGreaterThan4Validator : BaseValidator
{
    public AllGreaterThan4Validator(string type) : base(type) {}

    public override string GetCardText()
    {
        return "Todas las cartas son mayores que 4";
    }

    protected override bool ValidatePiles()
    {
        return this.piles.Filter(card => card.rank > 4).Length == this.piles.Length;
    }
}
