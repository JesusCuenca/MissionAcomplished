using System;

public class AllSumValidator : BaseValidator
{
    private int sum;

    public AllSumValidator(string type, params string[] arguments) :
        base(type)
    {
        // if (arguments.Length == 0) {
        //     throw new System.Exception("AllSumValidator expects at least one argument.");
        // }
        // try {
        //     this.sum = int.Parse(arguments[0]);
        // } catch (FormatException e) {
        //     throw new System.Exception("AllSumValidator expects the first parameter to be an integer.");
        // }
    }

    public override string GetCardText()
    {
        return "Todas las cartas suman " + this.sum;
    }

    protected override bool ValidatePiles()
    {
        return true;
    }
}
