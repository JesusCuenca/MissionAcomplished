﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsSumEqualValidator : BaseValidator
{
    protected Colors color1;
    protected Colors color2;

    public ColorsSumEqualValidator(string type, params string[] arguments): base(type) {
        this.ValidateArgumentCount(arguments, 2);
        this.color1 = this.ArgumentToColor(arguments[0], 0);
        this.color2 = this.ArgumentToColor(arguments[1], 1);
    }

    public override string GetCardText()
    {
        return string.Format("Las cargas {0} suman el doble que las {1}", this.GetColorTranslated(this.color1), this.GetColorTranslated(this.color2));
    }

    protected override bool ValidatePiles()
    {
        return this.piles.SumColorRanks(this.color1) == this.piles.SumColorRanks(this.color2);
    }
}
