using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsSumDoubleValidator: ColorsSumEqualValidator
{
    public ColorsSumDoubleValidator(string type, params string[] arguments): base(type, arguments) {}

    public override string GetCardText()
    {
        return string.Format("Las cargas {0} suman el doble que las {1}", this.GetColorTranslated(this.color1), this.GetColorTranslated(this.color2));
    }

    protected override bool ValidatePiles()
    {
        return this.piles.SumColorRanks(this.color1) == (2 * this.piles.SumColorRanks(this.color2));
    }
}
