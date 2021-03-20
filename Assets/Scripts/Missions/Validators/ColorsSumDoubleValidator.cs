public class ColorsSumDoubleValidator: ColorsSumEqualValidator
{
    public ColorsSumDoubleValidator(string type, params string[] arguments): base(type, arguments) {}

    public override string GetCardText()
    {
        return string.Format("Las cartas {0} suman el doble que las {1}", this.GetColorTranslated(this.color1), this.GetColorTranslated(this.color2));
    }

    protected override bool ValidatePiles()
    {
        int sum1 = this.piles.SumColorRanks(this.color1);
        int sum2 = this.piles.SumColorRanks(this.color2);
        return sum1 > 0 && sum1 == (2 * sum2);
    }
}
