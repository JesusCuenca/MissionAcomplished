using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flush3StraightValidator : BaseValidator
{
    public Flush3StraightValidator(string type) : base(type) {}

    public override string GetCardText()
    {
        return "Hay escalera de tres cartas en orden";
    }

    protected override bool ValidatePiles()
    {
        int[] ranks = this.piles.GetRanks();
        int index = 0;
        if ((ranks[index] + 1) == ranks[index + 1]
            && (ranks[index + 1] + 1) == ranks[index + 2]
        ) {
            return true;
        }
        
        index = 1;
        if ((ranks[index] + 1) == ranks[index + 1]
            && (ranks[index + 1] + 1) == ranks[index + 2]
        ) {
            return true;
        }

        return false;
    }
}
