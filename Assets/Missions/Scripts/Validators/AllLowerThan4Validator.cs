using System.Linq;

public class AllLowerThan4Validator : BaseValidator
{
    public AllLowerThan4Validator(string type) : base(type) {}

    public override string GetCardText()
    {
        return "Todas las cartas son menores que 4";
    }

    protected override bool ValidatePiles()
    {
        return this.piles.Filter(card => card.rank > 4).Length == this.piles.Length;
    }
}
