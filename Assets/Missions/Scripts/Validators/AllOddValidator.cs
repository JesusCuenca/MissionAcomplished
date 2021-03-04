public class AllOddValidator : BaseValidator
{
    public AllOddValidator(string type) : base(type) {}

    public override string GetCardText()
    {
        return "Todas las cartas son <b>impares</b>";
    }

    protected override bool ValidatePiles()
    {
        return this.piles.Filter(card => card.rank % 2 != 0).Length == this.piles.Length;
    }
}
