public class AllSumValidator : BaseValidator
{
    private int sum;

    public AllSumValidator(string type, params string[] arguments) :
        base(type)
    {
        this.ValidateArgumentCount(arguments, 1);
        this.sum = this.ArgumentToInt(arguments[0], 0);
    }

    public override string GetCardText()
    {
        return "Todas las cartas suman " + this.sum;
    }

    protected override bool ValidatePiles()
    {
        return this.piles.SumAllRanks() == this.sum;
    }
}
